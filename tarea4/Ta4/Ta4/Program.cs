using Ta4.Clases;
using Ta4.Repositorios;

namespace Ta4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibroRepository libRepo = new LibroRepository();

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            // inicio de operaciones cruud para libro
            /* Guardar libro
             Libro lib = new Libro("20 Días sin ti", "Luisa Costa", 2007, "Romántica");
            libRepo.guardarLibro(lib);*/

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            /*Mostrar libro
            List<Libro> libs = libRepo.mostrarLibros();
            foreach (Libro l in libs) { 
                Console.Write(l.IdLibro + "   ");
                Console.Write(l.Titulo + "   ");
                Console.Write(l.Autor + "   ");
                Console.Write(l.AñoPublicacion + "   ");
                Console.Write(l.Genero + "   ");
                Console.Write(l.Disponibilidad + "   ");
                Console.WriteLine();
            }
            */

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            /* eliminar libro
            libRepo.eliminarLibro(9);
            */

            // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            /* para actualizar, dato que no se desea actualizar se envia como null
            Libro libroActualizar = new Libro(null, null, null, null, null);
            libRepo.actualizarLibro(libroActualizar, 8);
            */

            // fin de operaciones crud para libro

            //{-----------------------------------------------------------------------------------------------------------------

            // inicio de operaciones crud para miembro

            MiembroRepository miembroRepo = new MiembroRepository();

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            /* guardar miembro 
            Miembro m = new Miembro("Lucas", "Herrera", (MembresiaEnum)0);
            miembroRepo.guardarMiembro(m);*/

            // nota: en el ultimo parametro el solo pueden ir tres opciones de 0 a 2 y "(MembresiaEnum) es obligatorio lo que varia es el número que lo acompaña"
            // o puede ir el nombre de una variable que contenga de 0 a 2

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            /*mostrar miembros
            List<Miembro> miembros = miembroRepo.mostrarMiembro();
            foreach (Miembro m in miembros) {
                Console.Write(m.IdMiembro + "   ");
                Console.Write(m.Nombre + "   ");
                Console.Write(m.Apellido + "   ");
                Console.Write(m.FechaRegistro + "   ");
                Console.Write(m.TipoMembresia + "   ");
                Console.WriteLine();
            }*/

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            /* Eliminar miembro
            miembroRepo.eliminarMiembro(2);
            */

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            /*Actualizar meimbro
            DateTime fecha = new DateTime(2024, 09, 11);  // si se quiere cambiar una fecha 
            Miembro m = new Miembro(null, null, fecha, null);
            miembroRepo.actualizarMiembro(m, 3);*/

            // fin de operaciones crud para miembro

        }
    }
}
