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

namespace Magazyn
{
    public partial class Produkty : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Database db = new Database();

        string skuCode;

        public Produkty()
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
            addButton.Enabled = false;
            saveButton.Enabled = true;
        }

        private void addCategoryButton_Click(object sender, EventArgs e)
        {
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
        }

        public void GetCategory()
        {
            cboCategory.Items.Clear();
            cboCategorySearch.Items.Clear();
            cboCategorySearch.Items.Add("ALL");
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
                GetUnit();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == "" || txtName.Text == "" || cboCategory.Text == "" || 
                    cboUnit.Text == "" || (txtQty.Text == "" || txtQty.Text== "0"))
                {
                    MessageBox.Show("Uzupełnij dane!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tbl_produkt(KodKreskowy, Nazwa, Kategoria, Jednostka, Ilosc) values(@KodKreskowy, @Nazwa, @Kategoria, @Jednostka, @Ilosc)", cn);
                cm.Parameters.AddWithValue("@KodKreskowy", txtCode.Text);
                cm.Parameters.AddWithValue("@Nazwa", txtName.Text);
                cm.Parameters.AddWithValue("@Kategoria", cboCategory.Text);
                cm.Parameters.AddWithValue("@Jednostka", cboUnit.Text);
                cm.Parameters.AddWithValue("@Ilosc", double.Parse(txtQty.Text));
                cm.ExecuteNonQuery();
                cn.Close();

                //wyczyszczenie okien po dodaniu produkty
                MessageBox.Show("Produkt został dodany do bazy!!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboCategory.Text = string.Empty;
                cboUnit.Text = string.Empty;
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                txtQty.Text = string.Empty;

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

            if(cboCategorySearch.Text == "ALL")
            {
                cm = new MySqlCommand("select * from tbl_produkt where Nazwa like '%" +  txtSearch.Text + "%'", cn);
            }
            else
            {
                cm = new MySqlCommand("select * from tbl_produkt where Kategoria like '" + cboCategorySearch.Text +
                                      "' and Nazwa like '" + txtSearch.Text + "%'", cn);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cn.Open();
            cm = new MySqlCommand("select * from tbl_produkt where KodKreskowy = '" + skuCode + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtCode.Text = dr["KodKreskowy"].ToString();

            }

            dr.Close();
            cn.Close();
        }

    }
}
