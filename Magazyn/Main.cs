using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazyn
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            int int_X = Screen.PrimaryScreen.Bounds.Width;
            int int_Y = Screen.PrimaryScreen.Bounds.Height;
            this.Width = int_X;
            this.Height = int_Y - 40;
            this.Top = 0;
            this.Left = 0;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Produkty p = new Produkty();
            p.TopLevel = false;
            panel1.Controls.Add(p);
            p.BringToFront();
            p.GetCategory();
            p.GetUnit();
            p.Show();
            p.LoadProduct();
        }
    }
}
