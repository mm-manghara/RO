using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROServiceApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProduct product=new AddProduct();
            product.Show();
            product.MdiParent = this;
            product.WindowState = FormWindowState.Maximized;

        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddComponent component = new AddComponent();
            component.Show();
            component.MdiParent = this;
        }

        private void productToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Sale sale= new Sale();
            sale.Show();
            sale.MdiParent = this;
            sale.WindowState = FormWindowState.Maximized;
        }
    }
}
