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
using System.IO;

namespace Abashon
{
    public partial class Registration : Form
    {
        public byte[] IV { get; private set; }

        public Registration()
        {
            InitializeComponent();
            textBox4Password.PasswordChar = '.';
        }
        string hash = "Criminal_of_War";

        private void AdminReg_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(textBox4Password.Text));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            String password1 = strBuilder.ToString();

            MySqlConnection DBconnect1 = new MySqlConnection("datasource=localhost;port=3306;username=root;password=''");
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();

            string InsertQuery = "INSERT INTO abashon.registration(Name,Username,Password,NID,Email,Mobile,Picture) Values(' " + textBox1.Text + "',' " + textBox2.Text + "','" + password1 + "',' " + textBox5.Text + "',' " + textBox3.Text + "',' " + textBox6.Text + "',' " + pictureBox1 + "')";
            MySqlCommand command = new MySqlCommand(InsertQuery, DBconnect1);
            MySqlDataReader mySqlData;
            DBconnect1.Open();
            mySqlData = command.ExecuteReader();
            

            String fromAddress = "shoaibaabesh@gmail.com";
            String toAddress = textBox3.Text;
            String password = "A@besh.1";
            MailMessage mail = new MailMessage();
            mail.Subject = "Abashon";
            mail.From = new MailAddress(fromAddress);
            mail.Body = "Congratulation";
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
            this.Hide();
            Login l1 = new Login();
            l1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }

        }
    }
}
