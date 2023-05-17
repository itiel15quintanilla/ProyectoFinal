using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProyectoFinal.Formularios
{
    public partial class Compilador : Form {
        private int[,] matriz;
        private int renglon;
        private string wlinea;
        private int direccion;
        private bool espalreservada;
        private int estado;
        private string token, wsalida;
        private int posicion;
        private char Caracter;
        private int columna;
        string nombreArchivo = "";
        string fechaformat = "";
        int id;
        string[] vectorPalabrasReservadas;
        string server = "Data Source = LAPTOP-M9JG8B6B\\SQLEXPRESS02; Initial Catalog= BD; Integrated Security = True ";
        SqlConnection conectar = new SqlConnection();
        string usuario;
        public Compilador(string usuario)
        {

            InitializeComponent();
            this.usuario = usuario;

        }
        public System.Data.DataTable Cargargcombo()
        {
            conectar.ConnectionString = server;
            conectar.Open();
            SqlDataAdapter da = new SqlDataAdapter("CARGARBOX", conectar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conectar.Close();
            conectar.Open();
            DateTime fechaactual= DateTime.Now;
            fechaformat =fechaactual.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "Select Nombre_lenguaje from Lenguajes where Id_lenguaje=@Id_lenguaje";
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@Id_lenguaje", id.ToString());
            object resultado = cmd.ExecuteScalar();
            nombreArchivo = $"Output_{resultado}_{usuario}_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.txt";
            string rutaArchivo = Path.Combine(@"D:\OutPuts\", nombreArchivo);
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                foreach (var item in ListSalida.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            conectar.Close();
            string idusuario = sacaridusuario(usuario);
            string idlenguaje = id.ToString();
            registro(idusuario, idlenguaje, fechaformat, nombreArchivo);
            ListEntrada.Items.Clear();
            ListSalida.Items.Clear();
            ListPreservadas.Items.Clear();
        }
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Cargargcombo();
            comboBox1.DisplayMember = "Nombre_lenguaje";
            comboBox1.ValueMember = "Id_lenguaje";
            
        }
        private void BuscarTokens()
        {
            string apoyo;
            estado = 0;
            token = "";
            posicion = 1;
            while (posicion <= wlinea.Length)
            {
                apoyo = wlinea.Substring(posicion - 1, 1); // Extrae un carácter de wlinea
                Caracter = apoyo.FirstOrDefault();
                CalcularColumna();
                estado = matriz[estado, columna];
                if (estado >= 100)
                {
                    if (token.Length > 0)
                    {
                        ReconoceTokens();
                    }
                    else if (token.Length == 0) // Únicamente caracteres especiales de un carácter
                    {
                        token = token + Caracter;
                        ReconoceTokens();
                    }
                    estado = 0;
                    token = "";
                }
                else
                {
                    if (estado != 0) // Mientras sea diferente de 0, sigue agregando caracteres
                    {
                        token = token + Caracter;
                    }
                }
                posicion++;
            }
            if (token.Length > 0)
            {
                Caracter = ' ';
                CalcularColumna();
                estado = matriz[estado, columna];
                ReconoceTokens();
            }
        }
        private void CalcularColumna()
        {
            if (Caracter >= 'A' && Caracter <= 'Z')
            {
                columna = 0;
            }
            else if (Caracter >= 'a' && Caracter <= 'z')
            {
                columna = 0;
            }
            else if (Caracter >= '0' && Caracter <= '9')
            {
                columna = 1;
            }
            else if (Caracter == '.')
            {
                columna = 2;
            }
            else if (Caracter == '\"')
            {
                columna = 3;
            }
            else if (Caracter == '/')
            {
                columna = 4;
            }
            else if (Caracter == '*')
            {
                columna = 5;
            }
            else if (Caracter == '+')
            {
                columna = 6;
            }
            else if (Caracter == '-')
            {
                columna = 7;
            }
            else if (Caracter == '<')
            {
                columna = 8;
            }
            else if (Caracter == '>')
            {
                columna = 9;
            }
            else if (Caracter == '(')
            {
                columna = 10;
            }
            else if (Caracter == ')')
            {
                columna = 11;
            }
            else if (Caracter == '[')
            {
                columna = 12;
            }
            else if (Caracter == ']')
            {
                columna = 13;
            }
            else if (Caracter == '{')
            {
                columna = 14;
            }
            else if (Caracter == '}')
            {
                columna = 15;
            }
            else if (Caracter == ';')
            {
                columna = 16;
            }
            else if (Caracter == ' ')
            {
                columna = 17;
            }
            else if (Caracter == '=')
            {
                columna = 18;
            }
            else if (Caracter == '_')
            {
                columna = 19;
            }

        }
        private void ReconoceTokens()
        {
            if (estado == 100)
            {
                espalreservada = false;
                BuscapalReservada();
                if (espalreservada)
                {
                    wsalida = token + "   PalReserv   " + direccion.ToString();
                }
                else
                {
                    //Buscaidentificadores();
                    wsalida = token + " Ident  ";
                }
                posicion = posicion - 1; // Regresa una posición requirió de un delimitador
            }

            if (estado == 101)
            {
                //BuscarEnteras();
                wsalida = token + " Cte. Enteras ";
                posicion = posicion - 1;
            }
            else if (estado == 102)
            {
                //BuscarReales();
                wsalida = token + " Cte. Real";
                posicion = posicion - 1;
            }

            if (estado == 105)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 106)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 107)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 108)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 109)
            {
                wsalida = token + " Car. Esp";
                posicion = posicion - 1;
            }
            else if (estado == 110)
            {
                token = token + Caracter;
                wsalida = token + " Car. Esp";
            }
            else if (estado == 111)
            {
                wsalida = token + " Car. Esp";
                posicion = posicion - 1;
            }
            else if (estado == 112)
            {
                token = token + Caracter;
                wsalida = token + " Car. Esp";
            }
            else if (estado == 113)
            {
                wsalida = token + " Car. Esp";
            }
            else if (estado == 104)
            {
                token = token + Caracter;
                wsalida = token + " Comment";
            }

            if (estado == 103)
            {
                token = token + Caracter;
                //uscarStrings()
                wsalida = token + " Cte. String ";
            }

            ListSalida.Items.Add(wsalida);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnAbrirarchivo_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string archivo = openFileDialog1.FileName;
            using (StreamReader fileReader = new StreamReader(archivo))
            {
                string stringReader;
                while ((stringReader = fileReader.ReadLine()) != null)
                {
                    ListEntrada.Items.Add(stringReader);
                }
            }
        }
        private void btnCompilar_Click(object sender, EventArgs e)
        {
            renglon = 0;
            while (renglon < ListEntrada.Items.Count)
            {
                ListEntrada.SelectedIndex = renglon;
                wlinea = ListEntrada.Text;
                BuscarTokens();
                renglon++;
            }
        }
        private void BuscapalReservada()
        {
                int renglon2 = 0;
                direccion = -1;
                while (!espalreservada && renglon2 < vectorPalabrasReservadas.Length)
                {
                    if (token.ToUpper() == vectorPalabrasReservadas[renglon2].ToUpper())
                    {
                        espalreservada = true;
                        direccion = renglon2;
                    }
                    renglon2 = renglon2 + 1;
                }
            
        }
        public void Visualbasic()
        {
            matriz = new int[10, 14];
            string rutaArchivo = @"D:\Matrices\MatrizVB.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_VisualBasic.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }
                vectorPalabrasReservadas = new string[37];
                string renglonvector;
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();


                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Fortrain()
        {
            matriz = new int[10, 14];
            string rutaArchivo = @"D:\Matrices\mtz fortan.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Fortran.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }
   
                }
                
                string renglonvector;
                vectorPalabrasReservadas = new string[30];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();
       
                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Pascal()
        {

            matriz = new int[13, 27];
            string rutaArchivo = @"D:\Matrices\Matriz Pascal.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Pascal.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas=new string[45];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Basic()
        {
            matriz = new int[10, 16];
            string rutaArchivo = @"D:\Matrices\matriz de estados basic.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Basic.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[30]; 
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Cobol()
        {
            matriz = new int[12, 20];
            string rutaArchivo = @"D:\Matrices\BlocMatriz COBOL.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Cobol.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas=new string[27];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void C()
        {
            matriz = new int[12, 20];
            string rutaArchivo = @"D:\Matrices\Matriz_C.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_C.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas=new string[31];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void VisualFoxpro()
        {
            matriz = new int[9, 19];
            string rutaArchivo = @"D:\Matrices\Matriz visual foxpro.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Visual_FoxPro.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[20]; 
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Clipper()
        {
            matriz = new int[12, 20];
            string rutaArchivo = @"D:\Matrices\Matriz Clipper.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Clipper.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[74];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Dbase()
        {
            matriz = new int[16, 25];
            string rutaArchivo = @"D:\Matrices\MatrizDbase.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Dbase.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[37];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }
        public void Java()
        {
            matriz = new int[12, 24];
            string rutaArchivo = @"D:\Matrices\matriz java.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Java.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[50]; 
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }

        public void Python()
        {
            matriz = new int[12, 24];
            string rutaArchivo = @"D:\Matrices\MatrizEstadosPython.txt"; // Ruta completa del archivo CSV
            string rutaPalabrasReservadas = @"D:\Palabras Reservadas\Palabras_R_Python.csv";
            string renglon;
            string[] datosrenglon;
            int r = 0;
            try
            {
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    while (!sr.EndOfStream)
                    {
                        renglon = sr.ReadLine();
                        datosrenglon = renglon.Split(',');
                        for (int c = 0; c < datosrenglon.Length; c++)
                        {
                            matriz[r, c] = Convert.ToInt32(datosrenglon[c]);
                        }
                        r++;
                    }

                }

                string renglonvector;
                vectorPalabrasReservadas = new string[50];
                using (StreamReader lector = new StreamReader(rutaPalabrasReservadas))
                {
                    renglonvector = lector.ReadLine();

                }
                vectorPalabrasReservadas = renglonvector.Split(',');

                for (int c = 0; c < vectorPalabrasReservadas.Length; c++)
                {
                    ListPreservadas.Items.Add(vectorPalabrasReservadas[c] + "");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar el archivo" + ex.Message);
            }
        }


        public void registro(string usuario,string id, string fechaformat, string nombreArchvo)
        {
            conectar.Open();
            SqlCommand cmd = new SqlCommand("GUARDARREGISTRO", conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id_Usuarios", usuario);
            cmd.Parameters.AddWithValue("@Id_lenguaje", id);
            cmd.Parameters.AddWithValue("@Fecha_Hora", fechaformat);
            cmd.Parameters.AddWithValue("@Nombre_Archivo", nombreArchivo);
            try
            {
                MessageBox.Show("Registro agregado correctamente");
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            conectar.Close();


        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {   
            Login frm1 = new Login();
            conectar.Close();
            frm1.Show();
            this.Close();
        }
        public string sacaridusuario(string usuario)
        {
            string idusuario;
            string query = "Select Id_Usuario from Usuarios where Usuario=@Usuario";
            conectar.Open();
            SqlCommand cmd = new SqlCommand(query, conectar);
            cmd.Parameters.AddWithValue("@Usuario", usuario);
            object result = cmd.ExecuteScalar();
            idusuario = result.ToString();
            conectar.Close();
            return idusuario;
          
        }
        public int palabrasreserv()
        {
            int ids=comboBox1.SelectedIndex+1;
            return ids;
        }
        private void btnRegistros_Click(object sender, EventArgs e)
        {
            Reports frmreportes = new Reports();  
            frmreportes.Show();
        }
        private void btnsaveexcel_Click(object sender, EventArgs e)
        {
          
        }

        private void btnsavecsv_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            ListPreservadas.Items.Clear();
            id = 0;
            id = palabrasreserv();
            if (comboBox1.Text == "VISUALBASI")
            {
                Visualbasic();
            }
            else if (comboBox1.Text == "FORTAN")
            {
                Fortrain();
            }
            else if (comboBox1.Text == "PASCAL")
            {
                Pascal();
            }
            else if (comboBox1.Text == "BASIC")
            {
                Basic();
            }
            else if (comboBox1.Text == "COBOL")
            {
                Cobol();
            }
            else if (comboBox1.Text == "C")
            {
                C();
            }
            else if (comboBox1.Text == "FOXPRO")
            {
                VisualFoxpro();
            }
            else if (comboBox1.Text == "CLIPPER")
            {
                Clipper();
            }
            else if (comboBox1.Text == "DBASE")
            {
                Dbase();
            }
            else if (comboBox1.Text == "JAVA")
            {
                Java();
            }
            else if (comboBox1.Text == "PYTHON")
            {
                Python();
            }

        }
    }
 }


