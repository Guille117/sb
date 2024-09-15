using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ta4.Clases;

namespace Ta4.Repositorios
{

    // esta clase contiene todos los métodos necesarios para manipular una base de datos
    internal class LibroRepository
    {
        // cadena de conexión
        string cadenaConexion = "Data Source=DESKTOP-VILJA9B\\SQLEXPRESS;" +
            "Initial Catalog=BibliotecaDB;" +
            "User=sa;" +
            "Password=Reach117;";

        public void guardarLibro(Libro lib)
        {
            // creamos una consulta como cadena de caracteres
            string consultaCadena = "Insert into Libros(Titulo, Autor, Año_Publicacion, Genero)" +
                " values (@titulo, @autor, @añoPu, @genero)";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand consulta = new SqlCommand(consultaCadena, conexion);

                conexion.Open();

                // pasamos parametros
                consulta.Parameters.AddWithValue("@titulo", lib.Titulo);
                consulta.Parameters.AddWithValue("@autor", lib.Autor);
                consulta.Parameters.AddWithValue("@añoPu", lib.AñoPublicacion);
                consulta.Parameters.AddWithValue("@genero", lib.Genero);

                try
                {
                    consulta.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    Console.WriteLine("Error");
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public List<Libro> mostrarLibros()
        {
            List<Libro> libros = new List<Libro>();
            string consultaCadena = "Select * from Libros;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand consulta = new SqlCommand(consultaCadena, conexion);

                conexion.Open();

                SqlDataReader lector = consulta.ExecuteReader();

                while (lector.Read())
                {
                    int id = lector.GetInt32(0);
                    string titulo = lector.GetString(1);
                    string autor = lector.GetString(2);
                    int añoP = lector.GetInt32(3);
                    string genero = lector.GetString(4);
                    bool disponible = lector.GetBoolean(5);

                    Libro lib = new Libro(id, titulo, autor, añoP, genero, disponible);
                    libros.Add(lib);
                }
                lector.Close();
                conexion.Close();
            }
            return libros;
        }

        public void eliminarLibro(int id)
        {
            string consultaCadena = "delete from Libros where ID_Libro = @id;";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand consulta = new SqlCommand(consultaCadena, conexion);

                conexion.Open();

                consulta.Parameters.AddWithValue("@id", id);

                try
                {
                    consulta.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    conexion.Close();
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void actualizarLibro(Libro libA, int id)
        {
            bool aux = false;
            StringBuilder consultaCadena = new StringBuilder("update Libros set ");

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand consulta = new SqlCommand();

                try
                {
                    conexion.Open();

                    // Verifica los campos y construye la consulta
                    if (libA.Titulo != null)
                    {
                        consultaCadena.Append("Titulo = @tit");
                        consulta.Parameters.AddWithValue("@tit", libA.Titulo);
                        aux = true;
                    }

                    if (libA.Autor != null)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Autor = @Autor");
                        }
                        else
                        {
                            consultaCadena.Append(" Autor = @Autor");
                        }

                        consulta.Parameters.AddWithValue("@Autor", libA.Autor);
                        aux = true;
                    }

                    if (libA.AñoPublicacion > -1)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Año_Publicacion = @añoP");
                        }
                        else
                        {
                            consultaCadena.Append(" Año_Publicacion = @añoP");
                        }
                        consulta.Parameters.AddWithValue("@añoP", libA.AñoPublicacion);
                        aux = true;
                    }

                    if (libA.Genero != null)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Genero = @genero");
                        }
                        else
                        {
                            consultaCadena.Append(" Genero = @genero");
                        }

                        consulta.Parameters.AddWithValue("@genero", libA.Genero);
                        aux = true;
                    }

                    if (libA.Disponibilidad != null)
                    {
                        if (aux)
                        {
                            consultaCadena.Append(", Disponibilidad = @disponible");
                        }
                        else
                        {
                            consultaCadena.Append(" Disponibilidad = @disponible");
                        }

                        consulta.Parameters.AddWithValue("@disponible", libA.Disponibilidad == true ? 1 : 0);
                    }

                    
                    consultaCadena.Append(" where ID_Libro = @id");
                    consulta.Parameters.AddWithValue("@id", id);

                    
                    consulta.CommandText = consultaCadena.ToString();
                    consulta.Connection = conexion;

                    
                    consulta.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    conexion.Close(); 
                }
            }
        }

    }
}
