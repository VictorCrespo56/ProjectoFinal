using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace WpfApp1.Pops
{
    /// <summary>
    /// Lógica de interacción para Plato1.xaml
    /// </summary>
    public partial class Plato1 : Window
    {
        float[] cantidad = { 100, 100, 100, 100, 100 };
        string[] alimento = new string[5];

        public Plato1()
        {
            InitializeComponent();

            LoadCb1("Select TIPO_ALIMENTO from tipo_alimento", "TIPO_ALIMENTO");
            LoadCb2("Select NOM_ALIMENTO from alimentos", "NOM_ALIMENTO");
            
        }

        private void LoadCb1(string query, string columna)
        {
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb(query);
            int i = 0;
            while (reader.Read() && i <=16)
            {
                i++;
                cbTipo1.Items.Add(i + "-" + reader.GetString(columna));
                cbTipo2.Items.Add(i + "-" + reader.GetString(columna));
                cbTipo3.Items.Add(i + "-" + reader.GetString(columna));
                cbTipo4.Items.Add(i + "-" + reader.GetString(columna));
                cbTipo5.Items.Add(i + "-" + reader.GetString(columna));
            }
        }

        private void LoadCb2(string query, string columna)
        {
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb(query);
            while (reader.Read())
            {
                cbIngre1.Items.Add(reader.GetString(columna));
                cbIngre2.Items.Add(reader.GetString(columna));
                cbIngre3.Items.Add(reader.GetString(columna));
                cbIngre4.Items.Add(reader.GetString(columna));
                cbIngre5.Items.Add(reader.GetString(columna));
            }
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void borrarNomPlato(object sender, MouseEventArgs e)
        {
            if (txtNomPlato.Text == "Nombre del plato")
            {
                txtNomPlato.Text = "";
            } 
        }

        private void ActualizarCb1(object sender, SelectionChangedEventArgs e)
        {
            string text = cbTipo1.SelectedItem as string;
            string[] sArray = text.Split('-');
            string id = sArray[0];
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb("SELECT NOM_ALIMENTO FROM `alimentos` WHERE ID_TIPO_ALIMENTO like " + id);
            cbIngre1.Items.Clear();
            while (reader.Read())
            {
                cbIngre1.Items.Add(reader.GetString("NOM_ALIMENTO"));
            }
        }

        private void ActualizarCb2(object sender, SelectionChangedEventArgs e)
        {
            string text = cbTipo2.SelectedItem as string;
            string[] sArray = text.Split('-');
            string id = sArray[0];
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb("SELECT NOM_ALIMENTO FROM `alimentos` WHERE ID_TIPO_ALIMENTO like " + id);
            cbIngre2.Items.Clear();
            while (reader.Read())
            {
                cbIngre2.Items.Add(reader.GetString("NOM_ALIMENTO"));
            }
        }

        private void ActualizarCb3(object sender, SelectionChangedEventArgs e)
        {
            string text = cbTipo3.SelectedItem as string;
            string[] sArray = text.Split('-');
            string id = sArray[0];
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb("SELECT NOM_ALIMENTO FROM `alimentos` WHERE ID_TIPO_ALIMENTO like " + id);
            cbIngre3.Items.Clear();
            while (reader.Read())
            {
                cbIngre3.Items.Add(reader.GetString("NOM_ALIMENTO"));
            }
        }

        private void ActualizarCb4(object sender, SelectionChangedEventArgs e)
        {
            string text = cbTipo4.SelectedItem as string;
            string[] sArray = text.Split('-');
            string id = sArray[0];
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb("SELECT NOM_ALIMENTO FROM `alimentos` WHERE ID_TIPO_ALIMENTO like " + id);
            cbIngre4.Items.Clear();
            while (reader.Read())
            {
                cbIngre4.Items.Add(reader.GetString("NOM_ALIMENTO"));
            }
        }

        private void ActualizarCb5(object sender, SelectionChangedEventArgs e)
        {
            string text = cbTipo5.SelectedItem as string;
            string[] sArray = text.Split('-');
            string id = sArray[0];
            Conexion sql = new Conexion();
            MySqlDataReader reader = sql.CargarCb("SELECT NOM_ALIMENTO FROM `alimentos` WHERE ID_TIPO_ALIMENTO like " + id);
            cbIngre5.Items.Clear();
            while (reader.Read())
            {
                cbIngre5.Items.Add(reader.GetString("NOM_ALIMENTO"));
            }
        }

        private void ActualizarDatos1(object sender, SelectionChangedEventArgs e)
        {
            alimento[0] = cbIngre1.SelectedItem as string;
            Conexion sql = new Conexion();
            if(sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[0] + "'", alimento[0], "ALIMENTOS", "NOM_ALIMENTO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[0] + "'", "ALIMENTOS");
                txtCal1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[0] / 100, 2));
                txtGrasas1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[0] / 100, 2));
                txtHC1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[0] / 100, 2));
                txtAzucar1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[0] / 100, 2));
                txtProteina1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[0] / 100, 2));
            }
            else
            {
                txtCal1.Text = "00.0";
                txtGrasas1.Text = "00.0";
                txtHC1.Text = "00.0";
                txtAzucar1.Text = "00.0";
                txtProteina1.Text = "00.0";
            }
        }

        private void ActualizarDatos2(object sender, SelectionChangedEventArgs e)
        {
            alimento[1] = cbIngre2.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[1] + "'", alimento[1], "ALIMENTOS", "NOM_ALIMENTO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[1] + "'", "ALIMENTOS");
                txtCal2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[1] / 100, 2));
                txtGrasas2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[1] / 100, 2));
                txtHC2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[1] / 100, 2));
                txtAzucar2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[1] / 100, 2));
                txtProteina2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[1] / 100, 2));
            }
            else
            {
                txtCal2.Text = "00.0";
                txtGrasas2.Text = "00.0";
                txtHC2.Text = "00.0";
                txtAzucar2.Text = "00.0";
                txtProteina2.Text = "00.0";
            }
        }

        private void ActualizarDatos3(object sender, SelectionChangedEventArgs e)
        {
            alimento[2] = cbIngre3.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[2] + "'", alimento[2], "ALIMENTOS", "NOM_ALIMENTO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[2] + "'", "ALIMENTOS");
                txtCal3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[2] / 100, 2));
                txtGrasas3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[2] / 100, 2));
                txtHC3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[2] / 100, 2));
                txtAzucar3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[2] / 100, 2));
                txtProteina3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[2] / 100, 2));
            }
            else
            {
                txtCal3.Text = "00.0";
                txtGrasas3.Text = "00.0";
                txtHC3.Text = "00.0";
                txtAzucar3.Text = "00.0";
                txtProteina3.Text = "00.0";
            }
        }

        private void ActualizarDatos4(object sender, SelectionChangedEventArgs e)
        {
            alimento[3] = cbIngre4.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[3] + "'", alimento[3], "ALIMENTOS", "NOM_ALIMENTO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[3] + "'", "ALIMENTOS");
                txtCal4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[3] / 100, 2));
                txtGrasas4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[3] / 100, 2));
                txtHC4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[3] / 100, 2));
                txtAzucar4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[3] / 100, 2));
                txtProteina4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[3] / 100, 2));
            }
            else
            {
                txtCal4.Text = "00.0";
                txtGrasas4.Text = "00.0";
                txtHC4.Text = "00.0";
                txtAzucar4.Text = "00.0";
                txtProteina4.Text = "00.0";
            }
        }

        private void ActualizarDatos5(object sender, SelectionChangedEventArgs e)
        {
            alimento[4] = cbIngre5.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[4] + "'", alimento[4], "ALIMENTOS", "NOM_ALIMENTO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[4] + "'", "ALIMENTOS");
                txtCal5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[4] / 100, 2));
                txtGrasas5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[4] / 100, 2));
                txtHC5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[4] / 100, 2));
                txtAzucar5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[4] / 100, 2));
                txtProteina5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[4] / 100, 2));
            }
            else
            {
                txtCal5.Text = "00.0";
                txtGrasas5.Text = "00.0";
                txtHC5.Text = "00.0";
                txtAzucar5.Text = "00.0";
                txtProteina5.Text = "00.0";
            }
        }

        private void ActualizarArrays1(object sender, TextChangedEventArgs e)
        {
            if(txtCant1.Text != "")
            {

                cantidad[0] = Convert.ToSingle(txtCant1.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[0] + "'", alimento[0], "ALIMENTOS", "NOM_ALIMENTO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[0] + "'", "ALIMENTOS");
                    txtCal1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[0] / 100, 2));
                    txtGrasas1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[0] / 100, 2));
                    txtHC1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[0] / 100, 2));
                    txtAzucar1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[0] / 100, 2));
                    txtProteina1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[0] / 100, 2));
                }
            }
        }

        private void ActualizarArrays2(object sender, TextChangedEventArgs e)
        {
            if (txtCant2.Text != "")
            {

                cantidad[1] = Convert.ToSingle(txtCant2.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[1] + "'", alimento[1], "ALIMENTOS", "NOM_ALIMENTO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[1] + "'", "ALIMENTOS");
                    txtCal2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[1] / 100, 2));
                    txtGrasas2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[1] / 100, 2));
                    txtHC2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[1] / 100, 2));
                    txtAzucar2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[1] / 100, 2));
                    txtProteina2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[1] / 100, 2));
                }
            }
        }

        private void ActualizarArrays3(object sender, TextChangedEventArgs e)
        {
            if (txtCant3.Text != "")
            {
                cantidad[2] = Convert.ToSingle(txtCant3.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[2] + "'", alimento[2], "ALIMENTOS", "NOM_ALIMENTO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[2] + "'", "ALIMENTOS");
                    txtCal3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[2] / 100, 2));
                    txtGrasas3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[2] / 100, 2));
                    txtHC3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[2] / 100, 2));
                    txtAzucar3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[2] / 100, 2));
                    txtProteina3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[2] / 100, 2));
                }
            }
        }

        private void ActualizarArrays4(object sender, TextChangedEventArgs e)
        {
            if (txtCant4.Text != "")
            {
                cantidad[3] = Convert.ToSingle(txtCant4.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[3] + "'", alimento[3], "ALIMENTOS", "NOM_ALIMENTO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[3] + "'", "ALIMENTOS");
                    txtCal4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[3] / 100, 2));
                    txtGrasas4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[3] / 100, 2));
                    txtHC4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[3] / 100, 2));
                    txtAzucar4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[3] / 100, 2));
                    txtProteina4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[3] / 100, 2));
                }
            }
        }

        private void ActualizarArrays5(object sender, TextChangedEventArgs e)
        {
            if (txtCant5.Text != "")
            {
                cantidad[4] = Convert.ToSingle(txtCant5.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[4] + "'", alimento[4], "ALIMENTOS", "NOM_ALIMENTO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS FROM alimentos WHERE NOM_ALIMENTO LIKE '" + alimento[4] + "'", "ALIMENTOS");
                    txtCal5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[4] / 100, 2));
                    txtGrasas5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[4] / 100, 2));
                    txtHC5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[4] / 100, 2));
                    txtAzucar5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[4] / 100, 2));
                    txtProteina5.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[4] / 100, 2));
                }
            }
        }

        private void GuardarPlato(object sender, RoutedEventArgs e)
        {
            string user = Usuario.username;
            string plato = txtNomPlato.Text;
            decimal calorias = Convert.ToDecimal(txtCal1.Text) + Convert.ToDecimal(txtCal2.Text) + Convert.ToDecimal(txtCal3.Text) + Convert.ToDecimal(txtCal4.Text) + Convert.ToDecimal(txtCal5.Text);
            decimal grasas = Convert.ToDecimal(txtGrasas1.Text) + Convert.ToDecimal(txtGrasas2.Text) + Convert.ToDecimal(txtGrasas3.Text) + Convert.ToDecimal(txtGrasas4.Text) + Convert.ToDecimal(txtGrasas5.Text);
            decimal hidratos = Convert.ToDecimal(txtHC1.Text) + Convert.ToDecimal(txtHC2.Text) + Convert.ToDecimal(txtHC3.Text) + Convert.ToDecimal(txtHC4.Text) + Convert.ToDecimal(txtHC5.Text);
            decimal azucar = Convert.ToDecimal(txtAzucar1.Text) + Convert.ToDecimal(txtAzucar2.Text) + Convert.ToDecimal(txtAzucar3.Text) + Convert.ToDecimal(txtAzucar4.Text) + Convert.ToDecimal(txtAzucar5.Text);
            decimal proteina = Convert.ToDecimal(txtProteina1.Text) + Convert.ToDecimal(txtProteina2.Text) + Convert.ToDecimal(txtProteina3.Text) + Convert.ToDecimal(txtProteina4.Text) + Convert.ToDecimal(txtProteina5.Text);
            decimal total = 0;

            for(int i = 0; i < 5; i++)
            {
                total = total + Convert.ToDecimal(cantidad[i]);
            }
            MessageBox.Show(Convert.ToString(total));

            string query = null;
            if(cbIngre1.Text == "")
            {
                query = null;
            }
            else if(cbIngre2.Text == ""){
                query = "INSERT INTO plato (N_USUARIO, NOM_PLATO, NOM_ALIMENTO1, CALORIAS, GRASAS, CARBO_HIDRATOS, AZUCAR, PROTEINAS, TOTAL) VALUES ( '" + user + "', '" + plato + "', '" + cbIngre1.Text + "', '" + calorias + "', '" + grasas + "', '" + hidratos + "', '" + azucar + "', '" + proteina + "', '" + total + "' );";
            }
            else if(cbIngre3.Text == "")
            {
                query = "INSERT INTO plato (N_USUARIO, NOM_PLATO, NOM_ALIMENTO1, NOM_ALIMENTO2, CALORIAS, GRASAS, CARBO_HIDRATOS, AZUCAR, PROTEINAS, TOTAL) VALUES ( '" + user + "', '" + plato + "', '" + cbIngre1.Text + "', '" + cbIngre2.Text + "', '" + calorias + "', '" + grasas + "', '" + hidratos + "', '" + azucar + "', '" + proteina + "', '" + total + "' );";
            }
            else if(cbIngre4.Text == "")
            {
                query = "INSERT INTO plato (N_USUARIO, NOM_PLATO, NOM_ALIMENTO1, NOM_ALIMENTO2, NOM_ALIMENTO3, CALORIAS, GRASAS, CARBO_HIDRATOS, AZUCAR, PROTEINAS, TOTAL) VALUES ( '" + user + "', '" + plato + "', '" + cbIngre1.Text + "', '" + cbIngre2.Text + "', '" + cbIngre3.Text + "', '" + calorias + "', '" + grasas + "', '" + hidratos + "', '" + azucar + "', '" + proteina + "', '" + total + "' );";
            }else if(cbIngre5.Text == "")
            {
                query = "INSERT INTO plato (N_USUARIO, NOM_PLATO, NOM_ALIMENTO1, NOM_ALIMENTO2, NOM_ALIMENTO3, NOM_ALIMENTO4, CALORIAS, GRASAS, CARBO_HIDRATOS, AZUCAR, PROTEINAS, TOTAL) VALUES ( '" + user + "', '" + plato + "', '" + cbIngre1.Text + "', '" + cbIngre2.Text + "', '" + cbIngre3.Text + "', '" + cbIngre4.Text + "', '" + calorias + "', '" + grasas + "', '" + hidratos + "', '" + azucar + "', '" + proteina + "', '" + total + "' );";
            }
            else
            {
                query = "INSERT INTO plato VALUES ( '" + user + "', '" + plato + "', '" + cbIngre1.Text + "', '" + cbIngre2.Text + "', '" + cbIngre3.Text + "', '" + cbIngre4.Text + "', '" + cbIngre5.Text + "', '" + calorias + "', '" + grasas + "', '" + hidratos + "', '" + azucar + "', '" + proteina + "', '" + total + "' );";
            }

            //MessageBox.Show(query);

            if (query != null)
            {
                Conexion sql = new Conexion();
                if(sql.ModificarBD(query) == true)
                {
                    MessageBox.Show("Se ha añadido el plato correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("No se ha podido añadir el plato.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Para añadir un plato se ha de seleccionar por lo menos un ingrediente.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
