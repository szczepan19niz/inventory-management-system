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
    public partial class Unit : MetroFramework.Forms.MetroForm
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Database db = new Database();
        string str_unit;

        public Unit()
        {
            cn = new MySqlConnection();
            cn.ConnectionString = db.GetConnection();

            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUnit.Text == "")
                {
                    MessageBox.Show("Podaj jednostkę!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tbljednostka (Jednostka) values(@Jednostka)", cn);
                cm.Parameters.AddWithValue("@Jednostka", txtUnit.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Jednostka została dodana do bazy!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtUnit.Text = "";
        }

        public void LoadUnit()
        {
            try
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                cn.Open();

                if (txtUnit.Text == "")
                {
                    cm = new MySqlCommand("select * from tbljednostka", cn);
                }
                else
                {
                    cm = new MySqlCommand("select * from tbljednostka where Jednostka like '%" + txtUnit.Text + "%'", cn);
                }

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView1.Rows.Add(i, dr["Jednostka"].ToString());
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            LoadUnit();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            str_unit = dataGridView1[1, i].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUnit.Text = str_unit;
        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUnit.Text == str_unit)
                {
                    if (MessageBox.Show("Czy chcesz usunąć ten wpis?", "Magazyn Item Service", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new MySqlCommand("delete from tbljednostka where Jednostka = @Jednostka", cn);
                        cm.Parameters.AddWithValue("@Jednostka", txtUnit.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Jednostka została usunięta!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                }
                else if (txtUnit.Text == "")
                {
                    MessageBox.Show("Wpisz jednostkę!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Taka jednostka nie istnieje!", "Magazyn Item Service", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            



