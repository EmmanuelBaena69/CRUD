using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CRUD
{
    public partial class Form1 : Form
    {
        //Algunos comandos necesarios para la conexion
        SQLiteConnection conexion_sqlite;
        SQLiteCommand cmd_sqlite;
        SQLiteDataReader datareader_sqlite;

        public Form1()
        {
            InitializeComponent();
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            //Para verificar campos vacios
            if (txtName.Text.Trim() != "" && txtPrecio.Text.Trim() != "")
            {
                try
                {
                    //Conexion a la base de datos
                    conexion_sqlite = new SQLiteConnection("Data Source=productos.db;Version=3;");

                    //Abrimos la conexion
                    conexion_sqlite.Open();

                    cmd_sqlite = conexion_sqlite.CreateCommand();


                    //Insertando valores, no se inserta el id ya que es auto incrementable
                    cmd_sqlite.CommandText = string.Format("INSERT INTO tbl_products (nom, precio) VALUES ('" + txtName.Text + "', " + txtPrecio.Text + ")");

                    datareader_sqlite = cmd_sqlite.ExecuteReader();

                    conexion_sqlite.Close();
                    MessageBox.Show("Registrado Correctamente");

                }
                catch (Exception iu)
                {

                    MessageBox.Show("Error al intentar registrarse" + iu.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor llene los campos");
            }
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            //Codigo para Leer los datos

            conexion_sqlite = new SQLiteConnection("Data Source=productos.db;Version=3;");
            conexion_sqlite.Open();
                try
                {
                    //Codigo para leer o ver la base de datos en el DataGridView
                    string sql = "SELECT * FROM tbl_products";
                    SQLiteCommand command = new SQLiteCommand(sql, conexion_sqlite);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Cierra la conexión
                    conexion_sqlite.Close();

                    /*llamamos el datatable que representa una tabla de datos
                    y nos permite buscar datos y mostralos en el dbgrd (DataGridView)*/
                    dbgrd.DataSource = dataTable;

                }
                catch (Exception a)
                {

                    MessageBox.Show("Error" + a.Message);
                }
            }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //Codigo para actualizar los datos

            conexion_sqlite = new SQLiteConnection("Data Source=productos.db;Version=3;");
            conexion_sqlite.Open();

            if (txtName.Text.Trim() != "" && txtPrecio.Text.Trim() != "")
            {
                try
                {
                    //lanzamos el comando update table que normalmente se usa en las DBS
                    SQLiteCommand cmd = new SQLiteCommand("UPDATE tbl_products SET nom=@Nombre WHERE precio=@Precio", conexion_sqlite);
                    cmd.Parameters.AddWithValue("@Nombre", txtName.Text);
                    cmd.Parameters.AddWithValue("@Precio", txtPrecio.Text);

                    cmd.ExecuteNonQuery();
                    conexion_sqlite.Close();

                    MessageBox.Show("Actualizado correctamente por favor lea los datos de nuevo");
                }
                catch (Exception l)
                {

                    MessageBox.Show("Error" + l.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor llene los campos");
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //Codigo para eliminar datos de la tabla

            conexion_sqlite = new SQLiteConnection("Data Source=productos.db;Version=3;");
            conexion_sqlite.Open();

            if (txtName.Text.Trim() != "")
            {
                try
                {
                    //lanzamos el comando delete from table que normalmente se usa en las DBS
                    SQLiteCommand cmd = new SQLiteCommand("DELETE FROM tbl_products WHERE nom=@Nombre", conexion_sqlite);
                    cmd.Parameters.AddWithValue("@Nombre", txtName.Text);
                    cmd.ExecuteNonQuery();
                    conexion_sqlite.Close();

                    MessageBox.Show("Se ha eliminado correctamente por favor lea los datos de nuevo");
                }
                catch (Exception l)
                {

                    MessageBox.Show("Error al borrar los datos " + l.Message);

                }
            }
            else
            {
                MessageBox.Show("Por favor llene los campos");
            }
        }
    }
}
