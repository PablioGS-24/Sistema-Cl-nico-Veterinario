using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using SistemaClinicoVeterinario.Database;

namespace SistemaClinicoVeterinario.DAO
{
    public class MascotaConPropietario
    {
        public int MascotaId { get; set; }
        public string NombreMascota { get; set; } = string.Empty;
        public string NombrePropietario { get; set; } = string.Empty;
        public bool VacunasAlDia { get; set; }
        public string DisplayText => $"{NombreMascota} - Dueño: {NombrePropietario}";
    }

    public class FacturaModelo
    {
        public int Id { get; set; }
        public string NombreMascota { get; set; } = string.Empty;
        public double SubtotalAtencion { get; set; }
        public double SubtotalHospitalizacion { get; set; }
        public double RecargoVacunas { get; set; }
        public double TotalGeneral { get; set; }
        public string FechaEmision { get; set; } = string.Empty;
    }

    public class FacturacionDAO
    {
        public List<MascotaConPropietario> ObtenerMascotasConPropietario()
        {
            var lista = new List<MascotaConPropietario>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"
                    SELECT m.Id, m.Nombre, p.Nombre, m.VacunasAlDia 
                    FROM Mascotas m 
                    INNER JOIN Propietarios p ON m.PropietarioId = p.Id;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new MascotaConPropietario
                        {
                            MascotaId = reader.GetInt32(0),
                            NombreMascota = reader.GetString(1),
                            NombrePropietario = reader.GetString(2),
                            VacunasAlDia = reader.GetInt32(3) == 1
                        });
                    }
                }
            }
            return lista;
        }

        public bool RegistrarFactura(int mascotaId, double subAtencion, double subHosp, double recargoVacunas, double total)
        {
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"INSERT INTO Facturas (MascotaId, SubtotalAtencion, SubtotalHospitalizacion, RecargoVacunas, TotalGeneral, FechaEmision)
                                    VALUES (@mId, @subA, @subH, @rec, @total, @fecha);";
                cmd.Parameters.AddWithValue("@mId", mascotaId);
                cmd.Parameters.AddWithValue("@subA", subAtencion);
                cmd.Parameters.AddWithValue("@subH", subHosp);
                cmd.Parameters.AddWithValue("@rec", recargoVacunas);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<FacturaModelo> ObtenerHistorialFacturas()
        {
            var lista = new List<FacturaModelo>();
            using (var conexion = DatabaseHelper.ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"
                    SELECT f.Id, m.Nombre, f.SubtotalAtencion, f.SubtotalHospitalizacion, f.RecargoVacunas, f.TotalGeneral, f.FechaEmision
                    FROM Facturas f
                    INNER JOIN Mascotas m ON f.MascotaId = m.Id
                    ORDER BY f.Id DESC;";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new FacturaModelo
                        {
                            Id = reader.GetInt32(0),
                            NombreMascota = reader.GetString(1),
                            SubtotalAtencion = reader.GetDouble(2),
                            SubtotalHospitalizacion = reader.GetDouble(3),
                            RecargoVacunas = reader.GetDouble(4),
                            TotalGeneral = reader.GetDouble(5),
                            FechaEmision = reader.GetString(6)
                        });
                    }
                }
            }
            return lista;
        }
    }
}