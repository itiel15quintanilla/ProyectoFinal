using ProyectoFinal.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Login : Form
    {
        string server = "Data Source = LAPTOP-M9JG8B6B\\SQLEXPRESS02; Initial Catalog= BD; Integrated Security = True ";
        SqlConnection conectar = new SqlConnection();
        string usuario;
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UsuarioNuevo agregarUsuario = new UsuarioNuevo();
            this.Hide();
            agregarUsuario.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            conectar.ConnectionString = server;
            conectar.Open();
            string query = "Select Contrasena from Usuarios where Usuario=@usuario";
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@usuario", textBox1.Text);
            object resultado = cmd.ExecuteScalar();
            string hashedpassword = HashPassword(password);
            bool soniguales = string.Equals(resultado, hashedpassword);
            if (soniguales)
            {
                usuario = textBox1.Text;
                MessageBox.Show("Login exitoso");
                Compilador formpr = new Compilador(usuario);
                formpr.Show();
                this.Hide();
                conectar.Close();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrectos");
            }
            conectar.Close();
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la contraseña en un arreglo de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el arreglo de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
