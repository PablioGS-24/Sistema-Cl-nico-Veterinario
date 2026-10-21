using System;
using System.Windows.Forms;
using SistemaClinicoVeterinario.Database;
using SistemaClinicoVeterinario.Forms;

namespace SistemaClinicoVeterinario
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            // Inicializar la base de datos SQLite con todas las tablas
            DatabaseHelper.InicializarBaseDeDatos();

            // Desplegar el menú principal del sistema
            Application.Run(new FormPrincipal());
        }
    }
}