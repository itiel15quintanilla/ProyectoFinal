using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProyectoFinal.Formularios
{
    public partial class Reports : Form
    {
        string server = "Data Source = LAPTOP-M9JG8B6B\\SQLEXPRESS02; Initial Catalog= BD; Integrated Security = True ";
        SqlConnection conectar = new SqlConnection();

        public Reports()
        {
            InitializeComponent();
        }
        DataTable dataTable = new DataTable();
        private void Form2_Load(object sender, EventArgs e)
        {
            txtNombrelenguaje.TextChanged += FiltrarDatos;
            txtNombrelenguaje.TextChanged += FiltrarDatos;
            conectar.ConnectionString = server;
            conectar.Open();
            SqlCommand cmd = new SqlCommand("REPORTE",conectar);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = cmd;
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            dtgvRegistros.DataSource = dataTable;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            if (txtNombrelenguaje.Text == "")
            {
                dataTable.DefaultView.RowFilter = "Usuario = '" + txtNombreusuario.Text + "'";
            }else if(txtNombreusuario.Text == " ")
            {
                dataTable.DefaultView.RowFilter = "Nombre_lenguaje = '" + txtNombrelenguaje.Text + "'";

            }

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            conectar.Close();
        }

        private void btntxt_Click(object sender, EventArgs e)
        {           
            string nombrearchivo= $"Reporte_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.txt";
            string ruta = Path.Combine(@"D:\Reportes\", nombrearchivo);
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                // Obtén la vista del DataGridView (filtrada o sin filtrar)
                DataView vista = ((DataTable)dtgvRegistros.DataSource).DefaultView;

                // Recorre las filas de la vista
                foreach (DataRowView rowView in vista)
                {
                    // Accede a los datos de cada columna de la fila
                    string usuario = rowView["Usuario"].ToString();
                    string nombreLenguaje = rowView["Nombre_Lenguaje"].ToString();
                    DateTime fechaHora = Convert.ToDateTime(rowView["fecha_hora"]);
                    string nombreArchivo = rowView["nombre_archivo"].ToString();

                    // Escribe los datos en el archivo
                    sw.WriteLine($"{usuario}\t{nombreLenguaje}\t{fechaHora}\t{nombreArchivo}");
                }
            }

            // Mensaje de confirmación
            MessageBox.Show("El archivo se ha exportado exitosamente.");
        }


        private void FiltrarDatos(object sender, EventArgs e)
        {
            // Obtén los valores de los controles de filtrado
            string usuario = txtNombreusuario.Text;
            string lenguaje = txtNombrelenguaje.Text;


            if (string.IsNullOrEmpty(usuario) && string.IsNullOrEmpty(lenguaje))
            {
                txtNombreusuario.Text = string.Empty;
                txtNombrelenguaje.Text = string.Empty;
            }
            // Aplica los filtros al DataGridView
            AplicarFiltros(usuario, lenguaje);
        }

        private void AplicarFiltros(string usuario, string lenguaje)
        {
            // Obtén la vista predeterminada del DataGridView
            DataView vista = ((DataTable)dtgvRegistros.DataSource).DefaultView;
            DataTable dataTableoriginal = new DataTable();
            dataTableoriginal = dataTable.Copy();

    
            if (string.IsNullOrEmpty(usuario) && string.IsNullOrEmpty(lenguaje))
            {
                vista.RowFilter = string.Empty; // Elimina cualquier filtro existente
                return;
            }

            // Construye la expresión de filtro combinando las condiciones
            string filtro = "";

            // Agrega la condición de filtro por usuario
            if (!string.IsNullOrEmpty(usuario))
            {
                filtro += $"Usuario = '{usuario}'";
            }

            // Agrega la condición de filtro por lenguaje
            if (!string.IsNullOrEmpty(lenguaje))
            {
                if (!string.IsNullOrEmpty(filtro))
                    filtro += " AND ";
                filtro += $"Nombre_lenguaje = '{lenguaje}'";
            }

            // Aplica el filtro a la vista
            vista.RowFilter = filtro;
        }
        private void btnexcel_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet as Excel.Worksheet;
            DataTable dataTable = (DataTable)dtgvRegistros.DataSource;
            int rowIndex = 1;

            // Recorre las filas del DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                int columnIndex = 1;

                // Recorre las columnas de cada fila
                foreach (var item in row.ItemArray)
                {
                    // Guarda el contenido de cada celda en el archivo
                    worksheet.Cells[rowIndex, columnIndex] = item.ToString();
                    columnIndex++;
                }

                rowIndex++;
            }
            string nombrearchivo = $"Reporte_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.xlsx";
            string ruta = Path.Combine(@"D:\Reportes\", nombrearchivo);
            workbook.SaveAs(ruta);
            workbook.Close();
            excelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            MessageBox.Show("Archivo creado correctamente");
        }
        private void btnCSV_Click(object sender, EventArgs e)
        {
            string nombrearchivo = $"Reporte_{DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")}.csv";
            string ruta = Path.Combine(@"D:\Reportes\", nombrearchivo);
            // Crea un objeto StreamWriter para escribir en el archivo
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                // Escribe los encabezados de las columnas en el archivo CSV
                for (int i = 0; i < dtgvRegistros.Columns.Count; i++)
                {
                    sw.Write(dtgvRegistros.Columns[i].HeaderText);
                    if (i < dtgvRegistros.Columns.Count - 1)
                    {
                        sw.Write(", ");
                    }
                }
                sw.WriteLine();

                // Obtén las filas del DataGridView (ya sea filtradas o sin filtrar)
                DataGridViewRowCollection rows = dtgvRegistros.Rows;

                // Recorre las filas del DataGridView
                foreach (DataGridViewRow row in rows)
                {
                    // Verifica si la fila está visible (si está filtrada)
                    if (!row.Visible)
                    {
                        continue; // Si la fila no está visible, pasa a la siguiente
                    }

                    // Recorre las celdas de cada fila
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        sw.Write(row.Cells[i].Value);
                        if (i < row.Cells.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
                MessageBox.Show("Archivo creado correctamente");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
