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
    public partial class Category : MetroFramework.Forms.MetroForm
    {

        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Database db = new Database();
        string str_category;


        public Category()
        {
            cn = new MySqlConnection();
            cn.ConnectionString = db.GetConnection();

            InitializeComponent();
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.Text == "")
                {
                    MessageBox.Show("Podaj kategorie!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tblkategoria (Kategoria) values(@Kategoria)", cn);
                cm.Parameters.AddWithValue("@Kategoria", txtCategory.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Kategoria została dodana do bazy!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                    
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void Clear()
        {
            txtCategory.Text = "";
        }

        public void LoadCategory()
        {
            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                cn.Open();

                if (txtCategory.Text == "")
                {
                    cm = new MySqlCommand("select * from tblkategoria", cn);
                }
                else
                {
                    cm = new MySqlCommand("select * from tblkategoria where Kategoria like '%" + txtCategory.Text + "%'", cn);
                }

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["Kategoria"].ToString());
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCategory_TextChanged(object sender, EventArgs e)
        {
            LoadCategory();
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            str_category = dataGridView1[1, i].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategory.Text = str_category;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategory.Text == str_category)
                {
                    if (MessageBox.Show("Czy chcesz usunąć ten wpis?", "Magazyn Item Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new MySqlCommand("delete from tblkategoria where Kategoria = @Kategoria", cn);
                        cm.Parameters.AddWithValue("@Kategoria", txtCategory.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Kategoria została usunięta!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                }
                else if (txtCategory.Text == "")
                {
                    MessageBox.Show("Wpisz kategorię!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Taka kategoria nie istnieje!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
