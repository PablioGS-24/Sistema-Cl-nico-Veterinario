using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SistemaClinicoVeterinario.Database;

namespace SistemaClinicoVeterinario.DAO
{
    public class MascotaModelo
    {
        public int Id { get; set; }
        public int PropietarioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Raza { get; set; } = string.Empty;
        public bool VacunasAlDia { get; set; }
    }

    public class MascotaDAO
    {
        public bool Insertar(MascotaModelo m)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"INSERT INTO Mascotas (PropietarioId, Nombre, Especie, Raza, VacunasAlDia) 
                                    VALUES (@propId, @nombre, @especie, @raza, @vacunas);";
                cmd.Parameters.AddWithValue("@propId", m.PropietarioId);
                cmd.Parameters.AddWithValue("@nombre", m.Nombre);
                cmd.Parameters.AddWithValue("@especie", m.Especie);
                cmd.Parameters.AddWithValue("@raza", m.Raza);
                cmd.Parameters.AddWithValue("@vacunas", m.VacunasAlDia ? 1 : 0);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<MascotaModelo> ObtenerPorPropietario(int propietarioId)
        {
            var lista = new List<MascotaModelo>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "SELECT Id, PropietarioId, Nombre, Especie, Raza, VacunasAlDia FROM Mascotas WHERE PropietarioId = @propId;";
                cmd.Parameters.AddWithValue("@propId", propietarioId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new MascotaModelo
                        {
                            Id = reader.GetInt32(0),
                            PropietarioId = reader.GetInt32(1),
                            Nombre = reader.GetString(2),
                            Especie = reader.GetString(3),
                            Raza = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            VacunasAlDia = reader.GetInt32(5) == 1
                        });
                    }
                }
            }
            return lista;
        }

        public bool Actualizar(MascotaModelo m)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"UPDATE Mascotas 
                                    SET Nombre = @nombre, Especie = @especie, Raza = @raza, VacunasAlDia = @vacunas 
                                    WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", m.Id);
                cmd.Parameters.AddWithValue("@nombre", m.Nombre);
                cmd.Parameters.AddWithValue("@especie", m.Especie);
                cmd.Parameters.AddWithValue("@raza", m.Raza);
                cmd.Parameters.AddWithValue("@vacunas", m.VacunasAlDia ? 1 : 0);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Mascotas WHERE Id = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}