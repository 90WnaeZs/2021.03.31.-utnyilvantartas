using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace utnyilvan2_orai
{
    public partial class Rögzítés : Form
    {
        private int UID;
        private MySqlConnection conn;
        public Rögzítés(int ID)
        {
            InitializeComponent();
            mysqlkapcsolodas();
            UID = ID;
            id.Text += ID;
        }

        private void mysqlkapcsolodas()
        {
            MySqlConnectionStringBuilder db = new MySqlConnectionStringBuilder();
            db.Server = "localhost";
            db.Database = "utnyilvantartas";
            db.UserID = "root";
            db.Password = "";

            conn = new MySqlConnection(db.ToString());
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (tbxHonnan.Text != "" && tbxHova.Text != "" && numKm.Text != "")
            {
                using (MySqlCommand insert = new MySqlCommand($"INSERT INTO `utak`(`ID_user`, `Datum`, `Honnan`, `Hova`, `km`) VALUES(@id,@datum,@honnan,@hova,@km)", conn))
                {
                    insert.Parameters.Add("@id",MySqlDbType.Int32).Value = UID;
                    insert.Parameters.Add("@datum", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                    insert.Parameters.Add("@honnan", MySqlDbType.String).Value = tbxHonnan.Text;
                    insert.Parameters.Add("@hova", MySqlDbType.String).Value = tbxHova.Text;
                    insert.Parameters.Add("@km", MySqlDbType.Int32).Value = UID;

                    insert.ExecuteNonQuery();
                    MessageBox.Show("Bevitel");
                }
            }
        }

        private void btnSzures_Click(object sender, EventArgs e)
        {
            if(tbxHon.Text!="" && tbxHov.Text!="")
            {
                using (MySqlCommand olvas = new MySqlCommand($"SELECT `Datum`, `Honnan`, `Hova`, `km` FROM `utak` WHERE ID_user=@userid AND Honnan=@honnan AND Hova=@hova", conn))
                {
                    olvas.Parameters.Add("@userid", MySqlDbType.Int32).Value = UID;
                    olvas.Parameters.Add("@honnan", MySqlDbType.String).Value = tbxHon.Text;
                    olvas.Parameters.Add("@hova", MySqlDbType.String).Value = tbxHov.Text;
                    try
                    {
                        DataTable kimutat = new DataTable();
                        MySqlDataReader dr = olvas.ExecuteReader();
                        kimutat.Load(dr);
                        dr.Close();
                        dataGridView1.DataSource = kimutat;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
