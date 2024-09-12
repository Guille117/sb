using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ta4.Clases
{
    internal class Miembro
    {
        public int IdMiembro { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public MembresiaEnum? TipoMembresia { get; set; }

        public Miembro(int id, string nombre, string apellido, DateTime fechRegistro, MembresiaEnum tipoMem)
        {
            IdMiembro = id;
            Nombre = nombre;
            Apellido = apellido;
            FechaRegistro = fechRegistro;
            TipoMembresia = tipoMem;
        }

        public Miembro(string nombre, string apellido, MembresiaEnum tipoMem)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaRegistro = DateTime.Now;
            TipoMembresia = tipoMem;
        }

        public Miembro(string? nombre, string? apellido, DateTime? fechRegistro, MembresiaEnum? tipoMem)
        {
            Nombre = nombre;
            Apellido = apellido;
            FechaRegistro = fechRegistro;
            TipoMembresia = tipoMem;
        }
    }
}
