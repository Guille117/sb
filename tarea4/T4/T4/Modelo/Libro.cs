using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4.Modelo
{
    internal class Libro
    {
        private int ID_ibro;
        private string titulo;
        private string autor;
        private int año_Publicación;
        private bool disponibilidad;

        public int IdLibro
        {
            get { return ID_ibro; }
            set { ID_ibro = value; }  
        }

        public string Titulo{
            get { return titulo; }
            set { titulo = value; }
        }

        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        public int AñoPublicacion
        {
            get { return año_Publicación; }
            set { if (value > 1440 && value <= DateTime.Now.Year)
                {
                    año_Publicación = value;
                }
                else
                {
                    throw new ArgumentException("Año de publicación incorrecta, debe ser posterior a 1440 e inferior o igual al año acrual.");
                }
            }
        }

        public bool Disponibilidad
        {
            get { return disponibilidad; }
            set { disponibilidad = value; }
        }

        public Libro(int id, string titulo, string autor, int añoP, bool dispo) { 
           IdLibro = id; 
           Titulo = titulo;
           Autor = autor  ;
           AñoPublicacion = añoP;
           Disponibilidad = dispo;
        }

        public Libro(string titulo, string autor, int añoP, bool dispo)
        {
            Titulo = titulo;
            Autor = autor;
            AñoPublicacion = añoP;
            Disponibilidad = dispo;
        }
    }
}
