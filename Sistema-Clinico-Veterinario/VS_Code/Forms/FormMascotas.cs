using System;
using System.Windows.Forms;
using SistemaClinicoVeterinario.DAO;

namespace SistemaClinicoVeterinario.Forms
{
    public class FormMascotas : Form
    {
        private ComboBox cbPropietarios = new ComboBox();
        private TextBox txtNombre = new TextBox();
        private ComboBox cbEspecie = new ComboBox();
        private TextBox txtRaza = new TextBox();
        private CheckBox chkVacunas = new CheckBox();
        private DataGridView dgvMascotas = new DataGridView();
        private Button btnGuardar = new Button();
        private Button btnEliminar = new Button();

        private PropietarioDAO propietarioDao = new PropietarioDAO();
        private MascotaDAO mascotaDao = new MascotaDAO();
        private int idMascotaSeleccionada = 0;

        public FormMascotas()
        {
            InicializarComponentes();
            CargarPropietarios();
        }

        private void InicializarComponentes()
        {
            this.Text = "VetCare Enterprise - Gestión de Pacientes (Mascotas)";
            this.Size = new System.Drawing.Size(750, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Controles de interfaz
            Label lblProp = new Label { Text = "Propietario:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            cbPropietarios.Location = new System.Drawing.Point(120, 18); cbPropietarios.Width = 250;
            cbPropietarios.SelectedIndexChanged += CbPropietarios_SelectedIndexChanged;

            Label lblNombre = new Label { Text = "Nombre Mascota:", Location = new System.Drawing.Point(20, 55), AutoSize = true };
            txtNombre.Location = new System.Drawing.Point(120, 53); txtNombre.Width = 180;

            Label lblEspecie = new Label { Text = "Especie:", Location = new System.Drawing.Point(20, 90), AutoSize = true };
            cbEspecie.Location = new System.Drawing.Point(120, 88); cbEspecie.Width = 180;
            cbEspecie.Items.AddRange(new string[] { "Perro", "Gato", "Ave", "Reptil / Exotico", "Otro" });
            cbEspecie.SelectedIndex = 0;

            Label lblRaza = new Label { Text = "Raza:", Location = new System.Drawing.Point(20, 125), AutoSize = true };
            txtRaza.Location = new System.Drawing.Point(120, 123); txtRaza.Width = 180;

            chkVacunas.Text = "¿Vacunas al día? (Aplica descuento)";
            chkVacunas.Location = new System.Drawing.Point(120, 155);
            chkVacunas.AutoSize = true;

            // Botones
            btnGuardar.Text = "Guardar / Actualizar";
            btnGuardar.Location = new System.Drawing.Point(120, 190); btnGuardar.Width = 150;
            btnGuardar.Click += BtnGuardar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new System.Drawing.Point(280, 190); btnEliminar.Width = 100;
            btnEliminar.Click += BtnEliminar_Click;

            // Tabla DataGridView
            dgvMascotas.Location = new System.Drawing.Point(20, 230);
            dgvMascotas.Size = new System.Drawing.Size(690, 210);
            dgvMascotas.ReadOnly = true;
            dgvMascotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMascotas.CellClick += DgvMascotas_CellClick;

            // Agregar a la ventana
            this.Controls.Add(lblProp); this.Controls.Add(cbPropietarios);
            this.Controls.Add(lblNombre); this.Controls.Add(txtNombre);
            this.Controls.Add(lblEspecie); this.Controls.Add(cbEspecie);
            this.Controls.Add(lblRaza); this.Controls.Add(txtRaza);
            this.Controls.Add(chkVacunas);
            this.Controls.Add(btnGuardar); this.Controls.Add(btnEliminar);
            this.Controls.Add(dgvMascotas);
        }

        private void CargarPropietarios()
        {
            var lista = propietarioDao.ObtenerTodos();
            cbPropietarios.DataSource = lista;
            cbPropietarios.DisplayMember = "Nombre";
            cbPropietarios.ValueMember = "Id";
        }

        private void CbPropietarios_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CargarTablaMascotas();
        }

        private void CargarTablaMascotas()
        {
            if (cbPropietarios.SelectedValue != null && int.TryParse(cbPropietarios.SelectedValue.ToString(), out int idProp))
            {
                dgvMascotas.DataSource = null;
                dgvMascotas.DataSource = mascotaDao.ObtenerPorPropietario(idProp);
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (cbPropietarios.SelectedValue == null || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Seleccione un propietario e ingrese el nombre de la mascota.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var mascota = new MascotaModelo
            {
                Id = idMascotaSeleccionada,
                PropietarioId = Convert.ToInt32(cbPropietarios.SelectedValue),
                Nombre = txtNombre.Text.Trim(),
                Especie = cbEspecie.SelectedItem?.ToString() ?? "Otro",
                Raza = txtRaza.Text.Trim(),
                VacunasAlDia = chkVacunas.Checked
            };

            bool exito = (idMascotaSeleccionada == 0) ? mascotaDao.Insertar(mascota) : mascotaDao.Actualizar(mascota);

            if (exito)
            {
                MessageBox.Show("Mascota registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarTablaMascotas();
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (idMascotaSeleccionada == 0) return;

            if (MessageBox.Show("¿Desea eliminar la mascota seleccionada?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (mascotaDao.Eliminar(idMascotaSeleccionada))
                {
                    LimpiarCampos();
                    CargarTablaMascotas();
                }
            }
        }

        private void DgvMascotas_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvMascotas.Rows[e.RowIndex];
                idMascotaSeleccionada = Convert.ToInt32(fila.Cells["Id"].Value);
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                cbEspecie.SelectedItem = fila.Cells["Especie"].Value.ToString();
                txtRaza.Text = fila.Cells["Raza"].Value?.ToString();
                chkVacunas.Checked = Convert.ToBoolean(fila.Cells["VacunasAlDia"].Value);
            }
        }

        private void LimpiarCampos()
        {
            idMascotaSeleccionada = 0;
            txtNombre.Clear();
            txtRaza.Clear();
            chkVacunas.Checked = false;
        }
    }
}