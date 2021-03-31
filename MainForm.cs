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
    public partial class MainForm : Form
    {
        private MySqlConnection conn;
        private int UserID;
        public MainForm()
        {
            InitializeComponent();
            mysqlkapcsolodas();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Rögzítés r = new Rögzítés(UserID);
            r.Show();
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

        private void tbxUsername_Leave(object sender, EventArgs e)
        {
            if(tbxUsername.Text!="" && tbxPassword.Text != "")
            {
                using (MySqlCommand olvas = new MySqlCommand($"SELECT ID_user FROM users WHERE Nev='{tbxUsername.Text}' AND Jelszo='{tbxPassword.Text}'", conn))
                {
                    try
                    {
                        UserID = Convert.ToInt32(olvas.ExecuteScalar().ToString());
                        btnLogin.Enabled = true;
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
