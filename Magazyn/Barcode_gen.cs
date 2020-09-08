using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using BarcodeLib;

namespace Magazyn
{
    public partial class Barcode_gen : MetroFramework.Forms.MetroForm
    {
        public Barcode_gen()
        {
            InitializeComponent();
        }

        private void barcodeButton_Click(object sender, EventArgs e)
        {
            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            barcode.IncludeLabel = true;

            int barcode_length = txtBarcode.Text.Length;

            if (barcode_length == 11)
            {
                Image img = barcode.Encode(TYPE.UPCA, txtBarcode.Text, foreColor, backColor, (int)(picBarcode.Width * 0.3), (int)(picBarcode.Height * 0.3));
                picBarcode.Image = img;
            }
            else
            {
                MessageBox.Show("Prawidłowy kod kreskowy musi posiadać 11 cyfr!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if(picBarcode.Image == null)
            {
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "PNG|*.png" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picBarcode.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Zapisano!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Błąd!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }       
        }

    }
}
