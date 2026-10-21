using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaClinicoVeterinario.Forms
{
    public class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Text = "VetCare Enterprise - Menú Principal";
            this.Size = new Size(500, 420);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitulo = new Label
            {
                Text = "Sistema Clínico Veterinario",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(70, 30),
                AutoSize = true
            };

            Button btnPropietarios = new Button
            {
                Text = "1. Registro de Propietarios (Clientes)",
                Location = new Point(90, 85),
                Size = new Size(300, 45),
                Font = new Font("Arial", 10)
            };
            btnPropietarios.Click += (s, e) => new FormPropietarios().ShowDialog();

            Button btnMascotas = new Button
            {
                Text = "2. Registro de Mascotas (Pacientes)",
                Location = new Point(90, 145),
                Size = new Size(300, 45),
                Font = new Font("Arial", 10)
            };
            btnMascotas.Click += (s, e) => new FormMascotas().ShowDialog();

            Button btnFacturacion = new Button
            {
                Text = "3. Atención Médica y Facturación",
                Location = new Point(90, 205),
                Size = new Size(300, 45),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightGreen
            };
            btnFacturacion.Click += (s, e) => new FormFacturacion().ShowDialog();

            Button btnSalir = new Button
            {
                Text = "Salir",
                Location = new Point(180, 285),
                Size = new Size(120, 35)
            };
            btnSalir.Click += (s, e) => Application.Exit();

            this.Controls.Add(lblTitulo);
            this.Controls.Add(btnPropietarios);
            this.Controls.Add(btnMascotas);
            this.Controls.Add(btnFacturacion);
            this.Controls.Add(btnSalir);
        }
    }
}