using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    internal class ControladorLibro
    {
        Libro libAux = null;
        List<string> datos = new List<string>();
        LibroRepository libRepo = new LibroRepository();
        public int menu()
        {
            Console.Clear();
            int op = 0, x = 10, y = 4;
            Console.SetCursorPosition(x, y); Console.WriteLine("          ----- Menu de opciones  -----");
            y += 2; x += 8;
            Console.SetCursorPosition(x, y++); Console.WriteLine("Guardar Libro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Mostrar Libros.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Actualizar Libro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Eliminar Libro.");
            Console.SetCursorPosition(x, y++); Console.WriteLine("Salir.");

            op = Uti.navegador(x, y, 5);

            return op;
        }

        public void iniciar()
        {
            int op;
            do
            {
                op = menu();
                switch (op) {
                    case 1: libroGuardar(); break;
                    case 2: librosMostrar(); break;
                    case 3: libroActualizar(); break;
                    case 4:  libroEliminar(); break;
                    case 5: Console.Clear(); break; 
                }
            }while( op != 5 );
            

            
        }


        // método para pedir datos al usuario, tanto para guardar como para actualizar registros
        public List<string> PedirDatos(bool actualizar)
        {
            datos.Clear();
            int op = 0, x = 10, y = 4;

            Console.SetCursorPosition(x, y); Console.WriteLine("Ingrese datos de Libro.\n");
            y += 2;
            Console.SetCursorPosition(x, y++); Console.Write("  Título: ");
            datos.Add(Console.ReadLine());
            Console.SetCursorPosition(x, y++); Console.Write("  Autor: ");
            datos.Add(Console.ReadLine());
            Console.SetCursorPosition(x, y++); Console.Write("  Año publicación: ");
            datos.Add(Console.ReadLine());
            Console.SetCursorPosition(x, y++); Console.Write("  Género: ");
            datos.Add(Console.ReadLine());

            if (actualizar) {
                Console.SetCursorPosition(x, y++); Console.WriteLine("  Disponibilidad: ");
                Console.SetCursorPosition(x, y++); Console.WriteLine("  SI.");
                Console.SetCursorPosition(x, y++); Console.WriteLine("  NO.");
                Console.SetCursorPosition(x, y++); Console.WriteLine("  Sin cambios.");
                op = Uti.navegador(x, y, 3);

                datos.Add(op.ToString());
            }
            return datos;
        }

        // validar datos para guardar
        public Libro validarDatosGuardar(List<string> d1)
        {
            // detectar si falta algun dato

            bool error = false;
            foreach (string d in d1)
            {
                if(string.IsNullOrEmpty(d)) error = true;
            }

            if (error)
            {
                throw new Exception("  Debe ingresar todos los datos.");
            }


            // validar año de publicación

            if (!(Regex.IsMatch(d1[2], @"^\d{4}$"))){
                throw new Exception("  Formato de año incorrecto.");
            }
            else
            {
                int año = int.Parse(d1[2]);
                if(año < 1450 || año > DateTime.Now.Year)
                {
                    throw new Exception("El año de publicación aceptado debe estar entre 1450 y " + DateTime.Now.Year);
                }
            }

            Libro lib = new Libro(d1[0], d1[1], int.Parse(d1[2]), d1[3]);
            return lib;

        }

        // validar datos para actualizar
        public Libro validarDatosAcutualizar(List<string> d1) {
            Libro lib = new Libro(null, null, null, null, null);

            lib.Titulo = string.IsNullOrEmpty(d1[0]) ? null : d1[0];
            lib.Autor = string.IsNullOrEmpty(d1[1]) ? null : d1[1];
            lib.Genero = string.IsNullOrEmpty(d1[3]) ? null : d1[3];

            if (string.IsNullOrEmpty(d1[2]))
            {
                lib.AñoPublicacion = null;
            }
            else
            {
                // validamos el año de publicación, y lanzamos excepciones de ser necesario
                if (!(Regex.IsMatch(d1[2], @"^\d{4}$")))
                {
                    throw new Exception("\n  Formato de año incorrecto.");
                }
                else
                {
                    int año = int.Parse(d1[2]);
                    if (año < 1450 || año > DateTime.Now.Year)
                    {
                        throw new Exception("\n  El año de publicación aceptado debe estar entre 1450 y " + DateTime.Now.Year);
                    }
                }
                lib.AñoPublicacion = int.Parse(d1[2]);
            }

            switch (d1[4])
            {
                case "1": lib.Disponibilidad = true; break;
                case "2": lib.Disponibilidad = false; break;
                case "3": lib.Disponibilidad = null; break;
            }

            return lib;
        }

        public void libroGuardar()
        {
            Console.Clear();
            try
            {
                libRepo.guardarLibro(validarDatosGuardar(PedirDatos(false)));
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

        public void librosMostrar()
        {
            int x=45, y=2;

            List<Libro> libros = libRepo.mostrarLibros();

            Console.Clear();

            Console.SetCursorPosition(x, y);  Console.WriteLine("Registro de Libros.\n\n");
            Console.SetCursorPosition(x=0, y +=3); Console.WriteLine("   Id        Título          Autor           Año Publicación     Género      Disponibilidad");
            Console.WriteLine();
            y += 2;
            foreach (Libro l in libros)
            {
                Console.SetCursorPosition(4, y);Console.WriteLine(l.IdLibro);
                Console.SetCursorPosition(9, y); Console.WriteLine(l.Titulo);
                Console.SetCursorPosition(27, y); Console.WriteLine(l.Autor);
                Console.SetCursorPosition(50, y); Console.WriteLine(l.AñoPublicacion);
                Console.SetCursorPosition(65, y); Console.WriteLine(l.Genero);
                Console.SetCursorPosition(80, y); Console.WriteLine(l.Disponibilidad);
                y++;
            }

            Console.ReadLine();
        }

        public void libroActualizar()
        {
            int id;
            string id2;
            Console.Clear();
            do
            {
                Console.Write("\n\n  Ingrese el id del Libro a Actualizar: ");
                id2 = Console.ReadLine();
            } while (string.IsNullOrEmpty(id2));

            id = int.Parse(id2);

            Console.Clear();
            Console.WriteLine("\n  Ingrese los datos de los campos a actualizar. Si hay un dato que no desea actualizar,");
            Console.WriteLine("  simplemente no ingrese nada y presione 'Enter'\n"); 


            try
            {
                libRepo.actualizarLibro(validarDatosAcutualizar(PedirDatos(true)), id);
                Console.WriteLine("\n\n Datos actualizados con éxito");
                Console.ReadLine();
            }
            catch (Exception ex) {
                Console.WriteLine("\n\n Error al momento de actualizar");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public void libroEliminar()
        {
            int id;
            string id2;
            Console.Clear();

            do
            {
                Console.Write("\n   Ingrese el id del Libro a Eliminar: ");
                id2 = Console.ReadLine();
            }while(string.IsNullOrEmpty(id2));

            id = int.Parse(id2);
            try
            {
                libRepo.eliminarLibro(id);
                Console.WriteLine("\n\n Registro eliminado con éxito");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\n Error al momento de eliminar");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
           
        }
    }
}
