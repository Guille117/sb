using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ta4.Utilidades
{
    internal class Uti
    {
        public static int navegador(int x, int y, int opciones)
        {
            x -= 3;
            y = y - opciones;
            int resultado = 1;
            ConsoleKeyInfo tecla;
            do
            {
                Console.SetCursorPosition(x, y); Console.ForegroundColor = ConsoleColor.Green; Console.Write(">>"); Console.ForegroundColor = ConsoleColor.White;

                tecla = Console.ReadKey();

                if (tecla.Key == ConsoleKey.UpArrow && resultado > 1)
                {
                    Console.SetCursorPosition(x, y); Console.WriteLine("   ");
                    Console.SetCursorPosition(x, y -= 1); Console.ForegroundColor = ConsoleColor.Green; Console.Write(">>"); Console.ForegroundColor = ConsoleColor.White;
                    resultado--;
                }
                else
                {
                    if (tecla.Key == ConsoleKey.DownArrow && resultado < opciones)
                    {
                        Console.SetCursorPosition(x, y); Console.Write("   ");
                        Console.SetCursorPosition(x, y+=1); Console.ForegroundColor = ConsoleColor.Green; Console.Write(">>"); Console.ForegroundColor = ConsoleColor.White;
                        resultado++;
                    }
                }
            } while (tecla.Key != ConsoleKey.Enter);
            

            return resultado;
        }
    }
}
