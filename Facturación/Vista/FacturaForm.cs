using Datos;
using Entidades;
using System.Windows.Forms;

namespace Vista
{
    public partial class FacturaForm : Form
    {
        public FacturaForm()
        {
            InitializeComponent();
        }
        Cliente miCliente = null;
        ClienteDB clienteDB = new ClienteDB();

        private void IdentidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                miCliente = new Cliente();
                miCliente = clienteDB.DevolverClientesPorIdentidad(IdentidadTextBox.Text);
                NombreClienteTextBox.Text = miCliente.Nombre;
            }
            else
            {
                miCliente = null;
                NombreClienteTextBox.Clear();
            }
        }
        private void BuscarClienteButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}
