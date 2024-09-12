using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ta4.Clases
{
    internal class Libro
    {
        public int IdLibro { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int? AñoPublicacion { get; set; }
        public string Genero { get; set; }
        public bool? Disponibilidad { get; set; }

        public Libro(int id, string titulo, string autor, int? añoP, string genero, bool? dispo)      // 1er constructor para mostrar datos
        {
            IdLibro = id;
            Titulo = titulo;
            Autor = autor;
            AñoPublicacion = añoP;
            Genero = genero;
            Disponibilidad = dispo;
        }

        public Libro(string titulo, string autor, int añoP, string genero)               // 2do constructor  para guardar datos
        {
            Titulo = titulo;
            Autor = autor;
            AñoPublicacion = añoP;
            Genero = genero;
        }

        public Libro(string? titulo, string? autor, int? añoP, string? genero, bool? dispo)           // 3er constructor  para actualizar
        {
            Titulo = titulo;
            Autor = autor;
            AñoPublicacion = añoP;
            Genero = genero;
            Disponibilidad = dispo;
        }
    }
}
