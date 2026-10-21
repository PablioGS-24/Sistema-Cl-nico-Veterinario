using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaClinicoVeterinario.DAO;

namespace SistemaClinicoVeterinario.Forms
{
    public class FormFacturacion : Form
    {
        private ComboBox cbMascotas = new ComboBox();
        private ComboBox cbTipoAtencion = new ComboBox();
        private NumericUpDown numCostoAdicional = new NumericUpDown();
        private NumericUpDown numDiasHospital = new NumericUpDown();
        private Label lblEstadoVacunas = new Label();
        private Label lblTotalCalculado = new Label();
        private DataGridView dgvFacturas = new DataGridView();
        private Button btnFacturar = new Button();

        private FacturacionDAO facturacionDao = new FacturacionDAO();

        public FormFacturacion()
        {
            InicializarComponentes();
            CargarMascotas();
            CargarFacturas();
        }

        private void InicializarComponentes()
        {
            this.Text = "VetCare Enterprise - Módulo de Atención y Facturación";
            this.Size = new Size(800, 580);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Fila 1: Selección de Paciente y Estado de Vacunas
            Label lblMascota = new Label { Text = "Mascota / Paciente:", Location = new Point(20, 20), AutoSize = true };
            cbMascotas.Location = new Point(160, 18); cbMascotas.Width = 280;
            cbMascotas.SelectedIndexChanged += (s, e) => ActualizarCalculos();

            lblEstadoVacunas.Location = new Point(460, 20);
            lblEstadoVacunas.AutoSize = true;
            lblEstadoVacunas.Font = new Font("Arial", 9, FontStyle.Bold);

            // Fila 2: Tipo de Atención y Costo Adicional
            Label lblTipo = new Label { Text = "Tipo de Atención:", Location = new Point(20, 60), AutoSize = true };
            cbTipoAtencion.Location = new Point(160, 58); cbTipoAtencion.Width = 200;
            cbTipoAtencion.Items.AddRange(new string[] { "Consulta Médica (Q150)", "Vacunación (Q100)", "Cirugía General (Q500)", "Baño y Grooming (Q80)" });
            cbTipoAtencion.SelectedIndex = 0;
            cbTipoAtencion.SelectedIndexChanged += (s, e) => ActualizarCalculos();

            Label lblAdicional = new Label { Text = "Costo Adicional (Q):", Location = new Point(380, 60), AutoSize = true };
            numCostoAdicional.Location = new Point(510, 58); numCostoAdicional.Width = 90;
            numCostoAdicional.Maximum = 10000;
            numCostoAdicional.ValueChanged += (s, e) => ActualizarCalculos();

            // Fila 3: Hospitalización
            Label lblDias = new Label { Text = "Días Hospitalización (Q45/día):", Location = new Point(20, 100), AutoSize = true };
            numDiasHospital.Location = new Point(220, 98); numDiasHospital.Width = 80;
            numDiasHospital.Maximum = 365;
            numDiasHospital.ValueChanged += (s, e) => ActualizarCalculos();

            // Fila 4: Total General
            lblTotalCalculado.Location = new Point(20, 140);
            lblTotalCalculado.AutoSize = true;
            lblTotalCalculado.Font = new Font("Arial", 11, FontStyle.Bold);
            lblTotalCalculado.ForeColor = Color.DarkGreen;

            // Fila 5: Botón Emitir Factura (Ubicado en su propia fila)
            btnFacturar.Text = "Procesar y Emitir Factura";
            btnFacturar.Location = new Point(20, 175);
            btnFacturar.Size = new Size(260, 38);
            btnFacturar.BackColor = Color.LightSkyBlue;
            btnFacturar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnFacturar.Click += BtnFacturar_Click;

            // Fila 6: Tabla de Historial
            dgvFacturas.Location = new Point(20, 230);
            dgvFacturas.Size = new Size(740, 280);
            dgvFacturas.ReadOnly = true;
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Controls.Add(lblMascota); this.Controls.Add(cbMascotas); this.Controls.Add(lblEstadoVacunas);
            this.Controls.Add(lblTipo); this.Controls.Add(cbTipoAtencion);
            this.Controls.Add(lblAdicional); this.Controls.Add(numCostoAdicional);
            this.Controls.Add(lblDias); this.Controls.Add(numDiasHospital);
            this.Controls.Add(lblTotalCalculado); this.Controls.Add(btnFacturar);
            this.Controls.Add(dgvFacturas);
        }

        private void CargarMascotas()
        {
            var mascotas = facturacionDao.ObtenerMascotasConPropietario();
            cbMascotas.DataSource = mascotas;
            cbMascotas.DisplayMember = "DisplayText";
            cbMascotas.ValueMember = "MascotaId";
        }

        private void CargarFacturas()
        {
            dgvFacturas.DataSource = null;
            dgvFacturas.DataSource = facturacionDao.ObtenerHistorialFacturas();
        }

        private (double subAtencion, double subHosp, double recargoVacunas, double total) CalcularTotales()
        {
            if (cbMascotas.SelectedItem is not MascotaConPropietario mascota) return (0, 0, 0, 0);

            double costoBase = cbTipoAtencion.SelectedIndex switch
            {
                0 => 150.0,
                1 => 100.0,
                2 => 500.0,
                3 => 80.0,
                _ => 100.0
            };

            double subAtencion = costoBase + (double)numCostoAdicional.Value;
            double subHosp = (double)numDiasHospital.Value * 45.0;
            double recargoVacunas = mascota.VacunasAlDia ? 0.0 : 75.0;

            double total = subAtencion + subHosp + recargoVacunas;
            return (subAtencion, subHosp, recargoVacunas, total);
        }

        private void ActualizarCalculos()
        {
            if (cbMascotas.SelectedItem is MascotaConPropietario mascota)
            {
                if (mascota.VacunasAlDia)
                {
                    lblEstadoVacunas.Text = "Vacunas: AL DÍA (Sin recargo)";
                    lblEstadoVacunas.ForeColor = Color.Green;
                }
                else
                {
                    lblEstadoVacunas.Text = "Vacunas: PENDIENTES (+Q75 Recargo)";
                    lblEstadoVacunas.ForeColor = Color.DarkRed;
                }

                var (subA, subH, rec, total) = CalcularTotales();
                lblTotalCalculado.Text = $"Total General: Q{total:F2} (Atención: Q{subA:F2} | Hosp: Q{subH:F2} | Vacunas: Q{rec:F2})";
            }
        }

        private void BtnFacturar_Click(object? sender, EventArgs e)
        {
            if (cbMascotas.SelectedItem is not MascotaConPropietario mascota)
            {
                MessageBox.Show("Seleccione una mascota para facturar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (subA, subH, rec, total) = CalcularTotales();

            if (facturacionDao.RegistrarFactura(mascota.MascotaId, subA, subH, rec, total))
            {
                MessageBox.Show($"Factura emitida exitosamente para {mascota.NombreMascota} por un total de Q{total:F2}.", "Facturación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numCostoAdicional.Value = 0;
                numDiasHospital.Value = 0;
                CargarFacturas();
            }
            else
            {
                MessageBox.Show("Error al generar la factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}