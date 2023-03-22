using Datos;
using Entidades;
using System;
using System.Data;
using System.Windows.Forms;

namespace Vista
{
    public partial class ClienteForm : Syncfusion.Windows.Forms.Office2010Form
    {
        public ClienteForm()
        {
            InitializeComponent();
        }

        string tipoOperacion;
        DataTable dt = new DataTable();
        ClienteDB ClienteDB = new ClienteDB();
        Cliente cliente = new Cliente();

        private void HabilitarControles()
        {
            IdentidadTextBox.Enabled = true;
            NombreClienteTextBox.Enabled = true;
            NTelefonoTextBox.Enabled = true;
            CorreoClienteTextBox.Enabled = true;
            DireccionCTextBox.Enabled = true;
            FNacimientoDateTimePicker.Enabled = true;
            EstaActivoCheckBox.Enabled = true;
            GuardarButton.Enabled = true;
            CancelarButton.Enabled = true;
            ModificarButton.Enabled = false;
        }

        private void DesahabilitarControles()
        {
            IdentidadTextBox.Enabled = false;
            NombreClienteTextBox.Enabled = false;
            NTelefonoTextBox.Enabled = false;
            CorreoClienteTextBox.Enabled = false;
            DireccionCTextBox.Enabled = false;
            FNacimientoDateTimePicker.Enabled = false;
            EstaActivoCheckBox.Enabled = false;
            GuardarButton.Enabled = false;
            CancelarButton.Enabled = false;
            ModificarButton.Enabled = true;
        }

        private void LimpiarControles()
        {
            IdentidadTextBox.Clear();
            NombreClienteTextBox.Clear();
            NTelefonoTextBox.Clear();
            CorreoClienteTextBox.Clear();
            DireccionCTextBox.Clear();
            FNacimientoDateTimePicker.Focus();
            EstaActivoCheckBox.Checked = false;
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            IdentidadTextBox.Focus();
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            DesahabilitarControles();
            LimpiarControles();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(IdentidadTextBox.Text))
                {
                    errorProvider1.SetError(IdentidadTextBox, "Ingrese una identificación");
                    IdentidadTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(NombreClienteTextBox.Text))
                {
                    errorProvider1.SetError(NombreClienteTextBox, "Ingrese un nombre");
                    NombreClienteTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(NTelefonoTextBox.Text))
                {
                    errorProvider1.SetError(NTelefonoTextBox, "Ingrese un número de teléfono");
                    NTelefonoTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(CorreoClienteTextBox.Text))
                {
                    errorProvider1.SetError(CorreoClienteTextBox, "Ingrese un correo");
                    CorreoClienteTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                if (string.IsNullOrEmpty(DireccionCTextBox.Text))
                {
                    errorProvider1.SetError(DireccionCTextBox, "Ingrese una dirección");
                    DireccionCTextBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                cliente.Identidad = IdentidadTextBox.Text;
                cliente.Nombre = NombreClienteTextBox.Text;
                cliente.Telefono = NTelefonoTextBox.Text;
                cliente.Correo = CorreoClienteTextBox.Text;
                cliente.Direccion = DireccionCTextBox.Text;
                cliente.EstaActivo = EstaActivoCheckBox.Checked;


                //Insertar en la base de datos

                bool inserto = ClienteDB.Insertar(cliente);
                if (inserto)
                {
                    LimpiarControles();
                    DesahabilitarControles();
                    TraerClientes();
                    MessageBox.Show("Registro guardado con éxito");
                }
                else
                {
                    MessageBox.Show("Guardado con éxito");
                }

                //Pendiente lo anterior

            }
            else if (tipoOperacion == "Modificar")
            {
                cliente.Identidad = IdentidadTextBox.Text;
                cliente.Nombre = NombreClienteTextBox.Text;
                cliente.Telefono = NTelefonoTextBox.Text;
                cliente.Correo = CorreoClienteTextBox.Text;
                cliente.Direccion = DireccionCTextBox.Text;
                cliente.FechaNacimiento = FNacimientoDateTimePicker.Value;
                cliente.EstaActivo = EstaActivoCheckBox.Checked;

                bool modifico = ClienteDB.Editar(cliente);
                if (modifico)
                {
                    HabilitarControles();
                    DesahabilitarControles();
                    TraerClientes();
                    MessageBox.Show("Registro actualizado correctamente");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }
            }
        }

        private void ModificarButton_Click(object sender, EventArgs e)
        {
            tipoOperacion = "Modificar";
            if (ClientesDataGridView.SelectedRows.Count > 0)
            {
                IdentidadTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Identidad"].Value.ToString();
                NombreClienteTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                NTelefonoTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Telefono"].Value.ToString();
                CorreoClienteTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                DireccionCTextBox.Text = ClientesDataGridView.CurrentRow.Cells["Direccion"].Value.ToString();
                FNacimientoDateTimePicker.Text = ClientesDataGridView.CurrentRow.Cells["Fecha Nacimiento"].Value.ToString();
                EstaActivoCheckBox.Checked = Convert.ToBoolean(ClientesDataGridView.CurrentRow.Cells["EstaActivo"].Value);

                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            TraerClientes();
        }

        private void TraerClientes()
        {
            dt = ClienteDB.DevolverClientes();
            ClientesDataGridView.DataSource = dt;
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            if (ClientesDataGridView.SelectedRows.Count > 0)
            {
                DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar el registro?", "Advertencia", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    bool elimino = ClienteDB.Eliminar(ClientesDataGridView.CurrentRow.Cells["Identidad"].Value.ToString());
                    if (elimino)
                    {
                        LimpiarControles();
                        DesahabilitarControles();
                        TraerClientes();
                        MessageBox.Show("Registro eliminado correctamente");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el registro");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }
    }
}
