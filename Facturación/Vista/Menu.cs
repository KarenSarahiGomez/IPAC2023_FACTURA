using System;
using System.Windows.Forms;

namespace Vista
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void UsuariostoolStripButton1_Click(object sender, EventArgs e)
        {
            UsuariosForm userForm = new UsuariosForm();
            userForm.MdiParent = this;  //formulario hijo
            userForm.Show();
        }

        private void ProductostoolStripButton_Click(object sender, EventArgs e)
        {
            ProductosForm productosForm = new ProductosForm();
            productosForm.MdiParent = this;
            productosForm.Show();
        }

        private void ClientestoolStripButton_Click(object sender, EventArgs e)
        {
            ClienteForm clienteForm = new ClienteForm();
            clienteForm.MdiParent = this;
            clienteForm.Show();
        }

        private void NuevaFacturaToolStripButton_Click(object sender, EventArgs e)
        {
            FacturaForm facturaForm = new FacturaForm();
            facturaForm.MdiParent = this;
            facturaForm.Show();
        }
    }
}
