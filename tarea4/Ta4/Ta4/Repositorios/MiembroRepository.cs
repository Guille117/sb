using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ta4.Clases;

namespace Ta4.Repositorios
{
    internal class MiembroRepository
    {       
        // conexión como cadena de caracteres
        string conexionCadena = "Data Source=DESKTOP-VILJA9B\\SQLEXPRESS;" +
            "Initial Catalog=BibliotecaDB;" +
            "User=sa;" +
            "Password=Reach117;";


        public void guardarMiembro(Miembro m)
        {
            string cadenaConsulta = "insert into Miembros (Nombre, Apellido, Fecha_Registro, Tipo_Membresia)" +
                " values (@nom, @ape, @registro, @membresia)";

            using (SqlConnection conectar = new SqlConnection(conexionCadena))
            {

                SqlCommand consulta = new SqlCommand(cadenaConsulta, conectar);

                conectar.Open();

                // pasamos argumentos
                consulta.Parameters.AddWithValue("@nom", m.Nombre);
                consulta.Parameters.AddWithValue("@ape", m.Apellido);
                consulta.Parameters.AddWithValue("@registro", m.FechaRegistro);
                consulta.Parameters.AddWithValue("@membresia", m.TipoMembresia);

                try
                {
                    consulta.ExecuteNonQuery();
                    conectar.Close();
                }
                catch (Exception ex)
                {
                    conectar.Close();
                    Console.WriteLine(ex.ToString());
                }
            }

        }

        public List<Miembro> mostrarMiembro()
        {
            string cadenaConsulta = "select * from Miembros;";
            List<Miembro> miembros = new List<Miembro>();

            using (SqlConnection conectar = new SqlConnection(conexionCadena))
            {
                SqlCommand consulta = new SqlCommand(cadenaConsulta, conectar);

                conectar.Open();

                SqlDataReader lector = consulta.ExecuteReader();

                while (lector.Read())
                {
                    int id = lector.GetInt32(0);
                    string nombre = lector.GetString(1);
                    string apellido = lector.GetString(2);
                    DateTime fechRegistro = lector.GetDateTime(3);
                    int idEnum = lector.GetInt32(4);
                    MembresiaEnum tipoMem = (MembresiaEnum)idEnum;

                    Miembro m = new Miembro(id, nombre, apellido, fechRegistro, tipoMem);
                    miembros.Add(m);
                }

                lector.Close();
                conectar.Close();
            }
            return miembros;
        }

        public void eliminarMiembro(int idMiembro)
        {
            string consultaCadena = "delete from Miembros where ID_Miembro = @id;";

            using (SqlConnection conectar = new SqlConnection(conexionCadena))
            {
                SqlCommand consulta = new SqlCommand(consultaCadena, conectar);

                conectar.Open();

                consulta.Parameters.AddWithValue("@id", idMiembro);

                try
                {
                    consulta.ExecuteNonQuery();
                    conectar.Close();
                }
                catch (Exception ex)
                {
                    conectar.Close();
                    Console.WriteLine(ex.ToString());
                }
            }


        }

        public void actualizarMiembro(Miembro m, int id) {

            bool aux = false;

            // creamos una consulta por partes para, agregar datos según el usuario pida los datos a modificar
            StringBuilder consultaCadena = new StringBuilder("update Miembros set ");

            using (SqlConnection conectar = new SqlConnection(conexionCadena))
            {
                SqlCommand consulta = new SqlCommand();

                try
                {
                    conectar.Open();

                    if (m.Nombre != null)
                    {
                        consultaCadena.Append("Nombre = @nom");
                        consulta.Parameters.AddWithValue("@nom", m.Nombre);
                        aux = true;
                    }

                    if (m.Apellido != null)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Apellido = @ape");
                        }
                        else
                        {
                            consultaCadena.Append(" Apellido = @ape");
                        }
                        consulta.Parameters.AddWithValue("@ape", m.Apellido);
                        aux = true;
                    }

                    if (m.FechaRegistro != null)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Fecha_Registro = @reg");
                        }
                        else
                        {
                            consultaCadena.Append(" Fecha_Registro = @reg");
                        }
                        consulta.Parameters.AddWithValue("@reg", m.FechaRegistro);
                        aux = true;
                    }

                    if (m.TipoMembresia != null)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Tipo_Membresia = @mem");
                        }
                        else
                        {
                            consultaCadena.Append(" Tipo_Membresia = @mem");
                        }
                        consulta.Parameters.AddWithValue("@mem", m.TipoMembresia);
                        aux = true;
                    }

                    consultaCadena.Append(" where ID_Miembro = @id");
                    consulta.Parameters.AddWithValue("@id", id);

                    consulta.CommandText = consultaCadena.ToString();
                    consulta.Connection = conectar;


                    consulta.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    conectar.Close();
                }

            }

        }
    }
}
