using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BarcodeLib;
using System.IO;

namespace Magazyn
{
    public partial class Product : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Database db = new Database();

        string skuCode;

        public Product()
        {
            InitializeComponent();

            cn = new MySqlConnection();
            cn.ConnectionString = db.GetConnection();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            saveButton.Enabled = true;
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
            Category c = new Category();
            c.Show();

            /*
            try
            {
                if(cboCategory.Text == "")
                {
                    MessageBox.Show("Podaj kategorie", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tblkategoria (Kategoria) values(@Kategoria)", cn);
                cm.Parameters.AddWithValue("@Kategoria", cboCategory.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Kategoria została dodana do bazy!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetCategory();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
        }

        public void GetCategory()
        {
            cboCategory.Items.Clear();
            cboCategorySearch.Items.Clear();
            cboCategorySearch.Items.Add("Wszystko");
            cboCategory.Text = "";
            cn.Open();
            cm = new MySqlCommand("Select * from tblkategoria", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCategory.Items.Add(dr["Kategoria"].ToString());
                cboCategorySearch.Items.Add(dr["Kategoria"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void deleteCategoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCategory.Text == "")
                {
                    MessageBox.Show("Wybierz kategorie", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("delete from tblkategoria where Kategoria = @Kategoria", cn);
                cm.Parameters.AddWithValue("@Kategoria", cboCategory.Text);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("Kategoria została usunięta!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                GetCategory();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void addUnitButton_Click(object sender, EventArgs e)
        {
            Unit u = new Unit();
            u.Show();

            /*
            try
            {
                if (cboUnit.Text == "")
                {
                    MessageBox.Show("Podaj jednostkę", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tbljednostka (Jednostka) values(@Jednostka)", cn);
                cm.Parameters.AddWithValue("@Jednostka", cboUnit.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Jednostka została dodana do bazy!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetUnit();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
        }

        public void GetUnit()
        {
            cboUnit.Items.Clear();
            cboUnit.Text = "";
            cn.Open();
            cm = new MySqlCommand("Select * from tbljednostka", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboUnit.Items.Add(dr["Jednostka"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void deleteUnitButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboUnit.Text == "")
                {
                    MessageBox.Show("Wybierz jednostkę", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("delete from tbljednostka where Jednostka = @Jednostka", cn);
                cm.Parameters.AddWithValue("@Jednostka", cboUnit.Text);
                cm.ExecuteNonQuery();
                cn.Close();

                MessageBox.Show("Jednostka została usunięta!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                GetUnit();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void save_barcode()
        {
            if (picBarcode.Image == null)
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            int barcode_length = txtCode.Text.Length;

            try
            {
                if ((barcode_length < 12 || barcode_length > 12) || txtName.Text == "" || cboCategory.Text == "" ||
                    cboUnit.Text == "" || (txtQty.Text == "" || (txtQty.Text == "0")))
                {
                    MessageBox.Show("Uzupełnij dane!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tbl_produkt(KodKreskowy, Nazwa, Opis, Kategoria, Jednostka, Ilosc) values(@KodKreskowy, @Nazwa, @Opis, @Kategoria, @Jednostka, @Ilosc)", cn);
                cm.Parameters.AddWithValue("@KodKreskowy", txtCode.Text);
                cm.Parameters.AddWithValue("@Nazwa", txtName.Text);
                cm.Parameters.AddWithValue("@Opis", descriptionBox.Text);
                cm.Parameters.AddWithValue("@Kategoria", cboCategory.Text);
                cm.Parameters.AddWithValue("@Jednostka", cboUnit.Text);
                cm.Parameters.AddWithValue("@Ilosc", double.Parse(txtQty.Text));

                cm.ExecuteNonQuery();
                cn.Close();

                save_barcode();

                //wyczyszczenie okien po dodaniu produkty
                MessageBox.Show("Produkt został dodany do bazy!!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadProduct();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();

            if (cboCategorySearch.Text == "")
            {
                cm = new MySqlCommand("select * from tbl_produkt", cn);
            }
            else if (cboCategorySearch.Text == "Wszystko")
            {
                cm = new MySqlCommand("select * from tbl_produkt where Nazwa like '%" + txtSearch.Text + "%'", cn);
            }
            else
            {
                cm = new MySqlCommand("select * from tbl_produkt where Kategoria like '" + cboCategorySearch.Text +
                                      "' and Nazwa like '%" + txtSearch.Text + "%'", cn);
            }

            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["KodKreskowy"].ToString(), dr["Nazwa"].ToString(), dr["Ilosc"].ToString());
            }

            cn.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            skuCode = dataGridView1[1, i].Value.ToString();
        }


        //przycisk Anuluj
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Clear();
        }


        //czyszczenie okien
        public void Clear()
        {
            saveButton.Enabled = true;
            updateButton.Enabled = true;
            deleteButton.Enabled = true;
            txtCode.Enabled = true;

            txtCode.Text = "";
            txtName.Text = "";
            descriptionBox.Text = "";
            cboCategory.SelectedItem = null;
            cboUnit.SelectedItem = null;
            txtQty.Text = "0";
        }


        //edycja danych
        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Czy chcesz edytować ten wpis?", "Magazyn Item Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (txtName.Text == "" || (txtQty.Text == ""))
                    {
                        MessageBox.Show("Uzupełnij dane!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cn.Open();
                    cm = new MySqlCommand("update tbl_produkt set KodKreskowy=@KodKreskowy, Nazwa=@Nazwa, Opis=@Opis, Kategoria=@Kategoria, Jednostka=@Jednostka, Ilosc=@Ilosc where KodKreskowy=@KodKreskowy", cn);
                    cm.Parameters.AddWithValue("@KodKreskowy", txtCode.Text);
                    cm.Parameters.AddWithValue("@Nazwa", txtName.Text);
                    cm.Parameters.AddWithValue("@Opis", descriptionBox.Text);
                    cm.Parameters.AddWithValue("@Kategoria", cboCategory.Text);
                    cm.Parameters.AddWithValue("@Jednostka", cboUnit.Text);
                    cm.Parameters.AddWithValue("@Ilosc", double.Parse(txtQty.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();

                    //wyczyszczenie okien po dodaniu produkty
                    MessageBox.Show("Dane produktu zostały zaaktualizowane!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //przycisk usuń
        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Czy chcesz usunąć ten wpis?", "Magazyn Item Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int i = dataGridView1.CurrentRow.Index;
                    skuCode = dataGridView1[1, i].Value.ToString();

                    Console.Write(skuCode);

                    cn.Open();
                    cm = new MySqlCommand("delete from tbl_produkt where KodKreskowy = '" + skuCode + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Produkt został usunięty!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    LoadProduct();
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboCategorySearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtSearch.Text = "";
            LoadProduct();
        }


        //refresh
        public void refreshButton_Click(object sender, EventArgs e)
        {
            Clear();
            GetUnit();
            GetCategory();
            LoadProduct();
        }


        //wyświetlanie danych z okna wyszukiwania do okna dodawania


        
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            Barcode barcode = new Barcode();
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
            int barcode_length = txtCode.Text.Length;

            try
            {
                if (barcode_length == 12)
                {
                    Image img = barcode.Encode(TYPE.EAN13, txtCode.Text, foreColor, backColor, 200, 70);
                    picBarcode.Image = img;
                }
                else if (barcode_length > 12)
                {
                    picBarcode.Image = null;
                    MessageBox.Show("Prawidłowy kod kreskowy musi posiadać 12 cyfr!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    picBarcode.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prawidłowy kod musi zawierać 12 cyfr!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            cn.Open();
            cm = new MySqlCommand("select * from tbl_produkt where KodKreskowy = '" + skuCode + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtCode.Text = dr["KodKreskowy"].ToString();
                txtName.Text = dr["Nazwa"].ToString();
                descriptionBox.Text = dr["Opis"].ToString();
                cboCategory.Text = dr["Kategoria"].ToString();
                cboUnit.Text = dr["Jednostka"].ToString();
                txtQty.Text = dr["Ilosc"].ToString();

                updateButton.Enabled = true;
                saveButton.Enabled = false;
                txtCode.Enabled = false;
            }
            dr.Close();
            cn.Close();
        }
    }
}


