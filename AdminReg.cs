using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Net;
using System.Net.Mail;

namespace Abashon
{
    public partial class AdminReg : Form
    {
        public AdminReg()
        {
            InitializeComponent();
        }

        private void AdminReg_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection DBconnect1 = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''");
            DBconnect1.Open();
            Random rand = new Random();
            byte[] key = new byte[16];
            byte[] iv = new byte[16];
            for (int i = 0; i < 16; i++) key[i] = (byte)rand.Next(256);
            for (int i = 0; i < 16; i++) iv[i] = (byte)rand.Next(256);
            string password1 = textBox4Password.Text;
            byte[] data = Encoding.ASCII.GetBytes(password1);
            string InsertQuery = "INSERT INTO abashon.admin(Name,User_Name,Password,Email,Mobile) Values(' " + textBox1.Text + "',' " + textBox2.Text + "','" + password1 + "',' " + textBox3.Text + "',' " + textBox6.Text +"')";
            String fromAddress = "shoaibaabesh@gmail.com";
            String toAddress = textBox3.Text;
            String password = "A@besh.1";
            MailMessage mail = new MailMessage();
            mail.Subject = "Abashon";
            mail.From = new MailAddress(fromAddress);
            mail.Body = "Account Created";
            mail.To.Add(new MailAddress(toAddress));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nec = new NetworkCredential(fromAddress, password);
            smtp.Credentials = nec;
            smtp.Send(mail);
            MessageBox.Show("Account created. A confirmation mail is send on your email.");


        }
    }
}
