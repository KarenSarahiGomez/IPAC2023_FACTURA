using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class BuscarClienteForm : Form
    {
        public BuscarClienteForm()
        {
            InitializeComponent();
        }
        ClienteDB ClienteDB = new ClienteDB();
        public Cliente Cliente = new Cliente();

        private void BuscarClienteForm_Load(object sender, EventArgs e)
        {
            ClienteDataGridView.DataSource = ClienteDB.DevolverClientes(); ;
        }
        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (ClienteDataGridView.SelectedRows.Count > 0)
            {
                Cliente.Identidad = ClienteDataGridView.CurrentRow.Cells["Identidad"].Value.ToString();
                Cliente.Nombre = ClienteDataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                Cliente.Telefono = ClienteDataGridView.CurrentRow.Cells["Telefono"].Value.ToString();
                Cliente.Correo = ClienteDataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                Cliente.Direccion = ClienteDataGridView.CurrentRow.Cells["Direccion"].Value.ToString();
                Cliente.FechaNacimiento = Convert.ToDateTime(ClienteDataGridView.CurrentRow.Cells["FechaNacimiento"].Value);
                Cliente.EstaActivo = Convert.ToBoolean(ClienteDataGridView.CurrentRow.Cells["EstaActivo"].Value);
                this.Close();
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NombreTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            ClienteDataGridView.DataSource = ClienteDB.DevolverClientesPorNombre(NombreTextBox.Text);
        }
    }
}
