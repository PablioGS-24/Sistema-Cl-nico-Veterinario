using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SistemaClinicoVeterinario.Database;

namespace SistemaClinicoVeterinario.DAO
{
    public class PropietarioModelo
    {
        public int Id { get; set; }
        public string DpiNit { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }

    public class PropietarioDAO
    {
        public bool Insertar(PropietarioModelo p)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"INSERT INTO Propietarios (DPI_NIT, Nombre, Telefono, Direccion) 
                                    VALUES (@dpi, @nombre, @telefono, @direccion);";
                cmd.Parameters.AddWithValue("@dpi", p.DpiNit);
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@direccion", p.Direccion);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<PropietarioModelo> ObtenerTodos()
        {
            var lista = new List<PropietarioModelo>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "SELECT Id, DPI_NIT, Nombre, Telefono, Direccion FROM Propietarios ORDER BY Id DESC;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new PropietarioModelo
                        {
                            Id = reader.GetInt32(0),
                            DpiNit = reader.GetString(1),
                            Nombre = reader.GetString(2),
                            Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            Direccion = reader.IsDBNull(4) ? "" : reader.GetString(4)
                        });
                    }
                }
            }
            return lista;
        }

        public bool Actualizar(PropietarioModelo p)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"UPDATE Propietarios 
                                    SET DPI_NIT = @dpi, Nombre = @nombre, Telefono = @telefono, Direccion = @direccion 
                                    WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", p.Id);
                cmd.Parameters.AddWithValue("@dpi", p.DpiNit);
                cmd.Parameters.AddWithValue("@nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@telefono", p.Telefono);
                cmd.Parameters.AddWithValue("@direccion", p.Direccion);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Propietarios WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}