using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace WpfApp1
{
    
    public class Conexion
    {

        MySqlConnection con = new MySqlConnection(WpfApp1.Properties.Settings.Default.proyectoConnectionString);

        public DataTable Rellenar(string query, string tabla)
        {
            try
            {
                con.Open();

                MySqlCommand createComm = new MySqlCommand(query, con);
                createComm.ExecuteNonQuery();

                MySqlDataAdapter dataAdp = new MySqlDataAdapter(createComm);
                DataTable dt = new DataTable(tabla);
                dataAdp.Fill(dt);
                con.Close();

                return dt;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable(tabla);
                dt = null;
                MessageBox.Show("Error: " + ex.Message);
                return dt;
            }
        }

        public bool Comprobar(string query, string buscado, string tabla, string columna)
        {
            //MessageBox.Show(query);
            try
            {
                //con.Open();

                //MySqlCommand comm = new MySqlCommand(query, con);
                //comm.ExecuteReader();
                //MySqlDataAdapter dataAdp = new MySqlDataAdapter(comm);

                DataTable dtUser = Rellenar(query, tabla);

                //dataAdp.Fill(dtUser);

                string resultado = "" + Convert.ToString(dtUser.Rows[0][columna]);

                //MessageBox.Show(resultado + "- ");

                if (resultado == buscado)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("No se han encontrado resultados");
                    return false;
                }
            }
            catch(Exception ex)
            {
                String error = "Error: " + ex;
                return false;
            }
        }

        public bool ModificarBD(string query)
        {
            try
            {
                con.Open();

                MySqlCommand createComm = new MySqlCommand(query, con);
                createComm.ExecuteNonQuery();

                con.Close();
                return true;
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public MySqlDataReader CargarCb(string query)
        {
            string connectionString = WpfApp1.Properties.Settings.Default.proyectoConnectionString;
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                return command.ExecuteReader();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
                return null;
            }
        }
    }

    

}
