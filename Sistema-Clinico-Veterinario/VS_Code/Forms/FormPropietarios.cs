using System;
using System.Windows.Forms;
using SistemaClinicoVeterinario.DAO;

namespace SistemaClinicoVeterinario.Forms
{
    public class FormPropietarios : Form
    {
        private TextBox txtDpi = new TextBox();
        private TextBox txtNombre = new TextBox();
        private TextBox txtTelefono = new TextBox();
        private TextBox txtDireccion = new TextBox();
        private DataGridView dgvPropietarios = new DataGridView();
        private Button btnGuardar = new Button();
        private Button btnEliminar = new Button();
        private PropietarioDAO dao = new PropietarioDAO();
        private int idSeleccionado = 0;

        public FormPropietarios()
        {
            InicializarComponentes();
            CargarTabla();
        }

        private void InicializarComponentes()
        {
            this.Text = "VetCare Enterprise - Registro de Propietarios";
            this.Size = new System.Drawing.Size(750, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblDpi = new Label { Text = "DPI / NIT:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            txtDpi.Location = new System.Drawing.Point(120, 18); txtDpi.Width = 180;

            Label lblNombre = new Label { Text = "Nombre:", Location = new System.Drawing.Point(20, 55), AutoSize = true };
            txtNombre.Location = new System.Drawing.Point(120, 53); txtNombre.Width = 250;

            Label lblTelefono = new Label { Text = "Telefono:", Location = new System.Drawing.Point(20, 90), AutoSize = true };
            txtTelefono.Location = new System.Drawing.Point(120, 88); txtTelefono.Width = 180;

            Label lblDireccion = new Label { Text = "Direccion:", Location = new System.Drawing.Point(20, 125), AutoSize = true };
            txtDireccion.Location = new System.Drawing.Point(120, 123); txtDireccion.Width = 250;

            btnGuardar.Text = "Guardar / Actualizar";
            btnGuardar.Location = new System.Drawing.Point(120, 160);
            btnGuardar.Width = 140;
            btnGuardar.Click += BtnGuardar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(270, 160);
            btnEliminar.Width = 100;
            btnEliminar.Click += BtnEliminar_Click;

            dgvPropietarios.Location = new System.Drawing.Point(20, 210);
            dgvPropietarios.Size = new System.Drawing.Size(690, 210);
            dgvPropietarios.ReadOnly = true;
            dgvPropietarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPropietarios.CellClick += DgvPropietarios_CellClick;

            this.Controls.Add(lblDpi); this.Controls.Add(txtDpi);
            this.Controls.Add(lblNombre); this.Controls.Add(txtNombre);
            this.Controls.Add(lblTelefono); this.Controls.Add(txtTelefono);
            this.Controls.Add(lblDireccion); this.Controls.Add(txtDireccion);
            this.Controls.Add(btnGuardar); this.Controls.Add(btnEliminar);
            this.Controls.Add(dgvPropietarios);
        }

        private void CargarTabla()
        {
            dgvPropietarios.DataSource = null;
            dgvPropietarios.DataSource = dao.ObtenerTodos();
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDpi.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("DPI/NIT y Nombre son obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var propietario = new PropietarioModelo
            {
                Id = idSeleccionado,
                DpiNit = txtDpi.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Direccion = txtDireccion.Text.Trim()
            };

            bool resultado = (idSeleccionado == 0) ? dao.Insertar(propietario) : dao.Actualizar(propietario);

            if (resultado)
            {
                MessageBox.Show("Registro guardado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarTabla();
            }
            else
            {
                MessageBox.Show("Error al procesar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Desea eliminar el propietario seleccionado?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dao.Eliminar(idSeleccionado))
                {
                    LimpiarFormulario();
                    CargarTabla();
                }
            }
        }

        private void DgvPropietarios_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvPropietarios.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);
                txtDpi.Text = fila.Cells["DpiNit"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value?.ToString();
                txtDireccion.Text = fila.Cells["Direccion"].Value?.ToString();
            }
        }

        private void LimpiarFormulario()
        {
            idSeleccionado = 0;
            txtDpi.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
        }
    }
}