using System.Security.Cryptography.X509Certificates;
using Ta4.Clases;
using Ta4.Controladores;
using Ta4.Repositorios;
using Ta4.Utilidades;

namespace Ta4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // instanciar objetos de controladores
            ControladorLibro controlLibro = new ControladorLibro();
            ControladorMiembro controlMiembro = new ControladorMiembro();

            // menú de opciones para mostrar al usuario
            int op = 0;
            int menu()
            {
                int x = 10, y = 4;
                Console.Clear();

                Console.SetCursorPosition(x, y); Console.WriteLine("----- Menu de opciones  -----");
                y += 2; x += 8;
                Console.SetCursorPosition(x, y++); Console.WriteLine("Libro.");
                Console.SetCursorPosition(x, y++); Console.WriteLine("Miembros.");
                Console.SetCursorPosition(x, y++); Console.WriteLine("Salir.");
                op = Uti.navegador(x, y, 3);

                return op;
            }

            // menú de acciones
            do
            {
                op = menu();
                switch (op)
                {
                    case 1: controlLibro.iniciar(); break;
                    case 2: controlMiembro.iniciar(); break;
                    case 3: Console.Clear(); Console.WriteLine("\n\n\n           Fin de programa...\n\n\n"); break;
                }
            } while (op != 3);
        }
    }
}
