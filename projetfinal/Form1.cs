using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Policy;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.Remoting.Messaging;

namespace projetfinal
{
    public partial class Login : KryptonForm

    {
        
  
        private bool authenticated = false;
        private bool isTeamLeader = false;
        public Login()
        {
            InitializeComponent();
            
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Authenticate()
        {
            // Code to authenticate user
            authenticated = true; // Set to true if authentication is successful
            isTeamLeader = true; // Set to true if user is a team leader
            
            
        }
        private void CreateMeeting()
        {
            if (isTeamLeader)
            {
                // Code to create a new meeting
            }
            else
            {
                MessageBox.Show("You are not authorized to create a new meeting.");
            }
        }
        private void SelectParticipants()
        {
            if (isTeamLeader)
            {
                // Code to select participants for a meeting
            }
            else
            {
                MessageBox.Show("You are not authorized to select participants for a meeting.");
            }
        }
        private void kryptonPalette1_PalettePaint(object sender, ComponentFactory.Krypton.Toolkit.PaletteLayoutEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Username_text_enter(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "Username") {
                    kryptonTextBox1.Text = "";

            kryptonTextBox1.ForeColor = Color.Black;
        }
        }

        private void Username_text_leave(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "")
            {
                kryptonTextBox1.Text = "Username";

                kryptonTextBox1.ForeColor = Color.Silver;
            }

        }

        private void Password_text_enter(object sender, EventArgs e)
        {
            if (kryptonTextBox2.Text == "Password")
            {
                kryptonTextBox2.Text = "";
                

                kryptonTextBox2.ForeColor = Color.Black;
            }

        }
        private void Password_text_leave(object sender, EventArgs e)
        {
            if (kryptonTextBox2 .Text == "")
            {
                kryptonTextBox2.Text = "Password";
                

                kryptonTextBox2.ForeColor = Color.Silver;
            }

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            string mysqlCon = "Server=localhost; Port=3307; Database=projetnet; Uid=root; Pwd=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            string username = kryptonTextBox1.Text.ToString().Trim();
            string password = kryptonTextBox2.Text.ToString().Trim();
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))   
            {
                MessageBox.Show("No empty fields allowed");
            }
            else
            {   mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand("Select * from projetnet", mySqlConnection);
                MySqlDataReader  reader = mySqlCommand.ExecuteReader();
                bool foundMatch = false;
                while (reader.Read())
                {
                    if (username.Equals(reader.GetString("username")) && password.Equals(reader.GetString("password")))
                    {
                        foundMatch = true;
                        break;
                        
                    }
                    
                }
                reader.Close();
                if (foundMatch)
                {
                    string SQL = "Select Role from projetnet where Username='" + username + "'";
                    MySqlCommand mySqlCommand1 = new MySqlCommand(SQL, mySqlConnection);
                    MySqlDataReader reader2 = mySqlCommand1.ExecuteReader();
                    if (reader2.Read())
                    {
                        int role = reader2.GetInt32("Role");
                        if (role == 1)
                        {
                            this.Hide();
                            lead lead = new lead();
                            lead.Show();
                        }
                        else
                        {
                            this.Hide();
                            teamem teamem = new teamem();
                            teamem.Show();
                        }
                    }
                    reader2.Close();
                }

                else
                {
                    MessageBox.Show("Invalid Login");
                }
            }
        }
    }
}
