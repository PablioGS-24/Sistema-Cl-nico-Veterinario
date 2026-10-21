using System;
using Microsoft.Data.Sqlite;

namespace SistemaClinicoVeterinario.Database
{
    public static class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=VetCare.db;";

        public static SqliteConnection ObtenerConexion()
        {
            var conexion = new SqliteConnection(ConnectionString);
            conexion.Open();
            return conexion;
        }

        public static void InicializarBaseDeDatos()
        {
            using (var conexion = ObtenerConexion())
            {
                var cmd = conexion.CreateCommand();
                cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Propietarios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DPI_NIT TEXT NOT NULL UNIQUE,
                        Nombre TEXT NOT NULL,
                        Telefono TEXT,
                        Direccion TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Mascotas (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        PropietarioId INTEGER NOT NULL,
                        Nombre TEXT NOT NULL,
                        Especie TEXT NOT NULL,
                        Raza TEXT,
                        VacunasAlDia INTEGER NOT NULL DEFAULT 0,
                        FOREIGN KEY (PropietarioId) REFERENCES Propietarios(Id) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS Atenciones (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        MascotaId INTEGER NOT NULL,
                        TipoAtencion TEXT NOT NULL,
                        CostoBase REAL NOT NULL,
                        CostoAdicional REAL NOT NULL DEFAULT 0.0,
                        CostoTotal REAL NOT NULL,
                        Fecha TEXT NOT NULL,
                        FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS Hospitalizaciones (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        MascotaId INTEGER NOT NULL,
                        DiasInternado INTEGER NOT NULL,
                        CostoPorDia REAL NOT NULL DEFAULT 45.0,
                        TotalHospitalizacion REAL NOT NULL,
                        FechaIngreso TEXT NOT NULL,
                        FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id) ON DELETE CASCADE
                    );

                    CREATE TABLE IF NOT EXISTS Facturas (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        MascotaId INTEGER NOT NULL,
                        SubtotalAtencion REAL NOT NULL,
                        SubtotalHospitalizacion REAL NOT NULL,
                        RecargoVacunas REAL NOT NULL DEFAULT 0.0,
                        TotalGeneral REAL NOT NULL,
                        FechaEmision TEXT NOT NULL,
                        FOREIGN KEY (MascotaId) REFERENCES Mascotas(Id)
                    );
                ";
                cmd.ExecuteNonQuery();
            }
        }
    }
}