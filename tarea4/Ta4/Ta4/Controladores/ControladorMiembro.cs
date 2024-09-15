using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ta4.Clases;
using Ta4.Repositorios;
using Ta4.Utilidades;

namespace Ta4.Controladores
{
    internal class ControladorMiembro
    {
        // Esta clase controla todas las acciones necesarias antes de manipular los datos en la base de datos

        // creamos un objeto del repositorio de Miembro
        MiembroRepository miembroRepo = new MiembroRepository();
        
        // Creamon una lista para poder enviar los datos entre los distintos métodos 
        List<string> datos = new List<string>();

        // control de todas las acciones para miembro
        public void iniciar()
        {
            int op;
            do
            {
                op = menu();
                switch (op)
                {
                    case 1: miembroGuardar(); break;
                    case 2: miembroMostrar(); break;
                    case 3: miembroActualizar(); break;
                    case 4: miembroEliminar(); break;
                    case 5: Console.Clear();break;
                }
            } while (op != 5);
        }

        public int menu()
        {
            int op = 0, x = 10, y = 4;
            Console.Clear();
            Console.SetCursorPosition(x, y); Console.WriteLine("          ----- Menu de opciones  -----");
            y += 2; x += 8;
            Console.SetCursorPosition(x, y++); Console.WriteLine("Guardar Miembro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Mostrar Miembro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Actualizar Miembro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Eliminar Miembro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Salir.");
            op = Uti.navegador(x, y, 5);

            return op;
        }


        // este método nos permite preguntar y capturar datos al usuario, este metodo funciona para 2 acciones: guardar y actualizar
        // la manera en que distingue la acción requerida es por medio de una variabl booleana
        public List<string> pedirDatos(bool actualizar)
        {
            datos.Clear();
            int op = 0, x = 10, y = 4;

            Console.SetCursorPosition(x, y); Console.WriteLine("Ingrese datos de Miembro.");
            y += 2;

            Console.SetCursorPosition(x, y++); Console.Write("  Nombre: ");
            datos.Add(Console.ReadLine());
            Console.SetCursorPosition(x, y++); Console.Write("  Apellido: ");
            datos.Add(Console.ReadLine());

            if (!actualizar)
            {
                Console.SetCursorPosition(x, y++); Console.Write("  Tipo membresía: ");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Básico.");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Dual.");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Premium.");
                op = Uti.navegador(x, y, 3);

                datos.Add(op.ToString());
            }
            

            if (actualizar)
            {

                Console.SetCursorPosition(x, y++); Console.Write("  Tipo membresía: ");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Básico.");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Dual.");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Premium.");
                Console.SetCursorPosition(x, y++); Console.WriteLine(" Sin cambios.");
                op = Uti.navegador(x, y, 4);

                datos.Add(op.ToString());

                Console.SetCursorPosition(x, y++); Console.Write(" Fecha de registro: ");
                datos.Add(Console.ReadLine());
            }
            return datos;
        }

        // validar todos los datos antes de guardarlos en la base de datos, este método es específico para guardar un nuevo miembro
        public Miembro validarMiembroGuardar(List<string> ls)
        {
            bool error = false;

            foreach (string l in ls)
            {
                if (string.IsNullOrEmpty(l))
                {
                    error = true;
                }
            }

            if (error)
            {
                throw new Exception("  Debe ingresar todos los datos.");
            }

            Miembro m = new Miembro(ls[0], ls[1], (MembresiaEnum)int.Parse(ls[2])-1);

            return m;
        }


        // En este método se validan los datos para la actualizacón, asi también se determinan que campos se van a actualizar
        public Miembro validarMiembroActualizar(List<string> ls) {
            Miembro m = new Miembro(null, null, null, null);

            m.Nombre = string.IsNullOrEmpty(ls[0]) ? null : ls[0];
            m.Apellido = string.IsNullOrEmpty(ls[1]) ? null : ls[1];
            m.TipoMembresia = (int.Parse(ls[2]) == 4) ? null : (MembresiaEnum)(int.Parse(ls[2])-1);


            if (string.IsNullOrEmpty(ls[3]))
            {
                m.FechaRegistro = null;
            }
            else
            {
                if (Regex.IsMatch(ls[3], @"^(?:\d{4})\/(?:0[1-9]|1[0-2])\/(?:0[1-9]|[12][0-9]|3[01])$"))
                {
                    DateTime fecha = DateTime.Parse(ls[3]);
                    if(fecha < DateTime.Now)
                    {
                        m.FechaRegistro = fecha;
                    }
                    else
                    {
                        throw new Exception("Fecha de registro no aceptado");
                    }
                }
                else
                {
                    throw new Exception("Fecha de registro no aceptado");
                }
                
            }

            return m;
        }


        public void miembroGuardar()
        {
            Console.Clear();
            try
            {
                miembroRepo.guardarMiembro(validarMiembroGuardar(pedirDatos(false)));
                Console.WriteLine("\n\n Registro guardado con éxito");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n Error al momento de guardar");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public void miembroMostrar()
        {
            int x = 45, y = 2;

            List<Miembro> miembros = miembroRepo.mostrarMiembro();

            Console.Clear();

            Console.SetCursorPosition(x, y); Console.WriteLine("Registro de Miembros.\n\n");
            Console.SetCursorPosition(x = 0, y += 3); Console.WriteLine("   Id    Nombre      Apellido      Fecha Registro        Tipo Membresía");
            Console.WriteLine();
            y += 2;
            foreach (Miembro l in miembros)
            {
                Console.SetCursorPosition(4, y); Console.WriteLine(l.IdMiembro);
                Console.SetCursorPosition(9, y); Console.WriteLine(l.Nombre);
                Console.SetCursorPosition(21, y); Console.WriteLine(l.Apellido);
                Console.SetCursorPosition(35, y); Console.WriteLine(l.FechaRegistro);
                Console.SetCursorPosition(58, y); Console.WriteLine(l.TipoMembresia);
                y++;
            }

            Console.ReadLine();
        }

        public void miembroActualizar()
        {
            int id;
            string idAux;
            Console.Clear();

            do
            {
                Console.Write("\n\n  Ingrese el id del Miembro a Actualizar: ");
                idAux = Console.ReadLine();
            }while(string.IsNullOrEmpty(idAux));

            id = int.Parse(idAux);
            

            Console.Clear();
            Console.WriteLine("\n  Ingrese los datos de los campos a actualizar. Si hay un dato que no desea actualizar,");
            Console.WriteLine("  simplemente no ingrese nada y presione 'Enter'\n");


            try
            {
                miembroRepo.actualizarMiembro(validarMiembroActualizar(pedirDatos(true)), id);
                Console.WriteLine("\n\n Datos actualizados con éxito");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n Error al momento de actualizar");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public void miembroEliminar()
        {
            string idAux;
            int id;
            Console.Clear();
            do
            {
                Console.Write("\n   Ingrese el id del Miembro a Eliminar: ");
                idAux = Console.ReadLine();
            } while (string.IsNullOrEmpty(idAux));

            id = int.Parse(idAux);
            
            try
            {
                miembroRepo.eliminarMiembro(id);
                Console.WriteLine("\n\n Registro eliminado con éxito");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n Error al momento de eliminar");
                Console.WriteLine(ex.Message);
            }

        }
    }
}
