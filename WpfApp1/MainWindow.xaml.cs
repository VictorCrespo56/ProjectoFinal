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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using WpfApp1.Pops;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float[] cantidad = { 100, 100, 100, 100 };
        string[] alimento = new string[4];
        bool IsModifying = false;

        public MainWindow()
        {
            Login1 logIn = new Login1();
            logIn.ShowDialog();

            InitializeComponent();
            
            Load_datagrid();
            LoadCb();

        }

        private void BtNewPlato_Click(object sender, RoutedEventArgs e)
        {
            Plato1 nuevoPlato = new Plato1();
            nuevoPlato.ShowDialog();
        }

        private void BtBorrar_Click(object sender, RoutedEventArgs e)
        {
            cbDesayuno.Items.Clear();
            txtDesayuno.Text = "100";
            cbComida.Items.Clear();
            txtComida.Text = "100";
            cbMerienda.Items.Clear();
            txtMerienda.Text = "100";
            cbCena.Items.Clear();
            txtCena.Text = "100";
            txtNotas.Text = "";
            IsModifying = false;
            LoadCb();
        }

        private void Load_datagrid()
        {
            //string connectionString = WpfApp1.Properties.Settings.Default.proyectoConnectionString;
            //MySqlConnection sqlCon = new MySqlConnection(connectionString);

            try
            {
                Conexion sql = new Conexion();
                DataTable dtInfo = sql.Rellenar("SELECT FECHA,NOM_DESAYUNO,NOM_COMIDA,NOM_MERIENDA,NOM_CENA,CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL,NOTAS FROM `info_usuario` WHERE N_USUARIO LIKE '" + Usuario.username+"';", "INFO_USUARIO");

                //string resultado1 = "" + Convert.ToString(dtInfo.Rows[0]["USUARIO"]);
                //MessageBox.Show(resultado1);

                //dg_usuario.DataContext = dtInfo.AsDataView(); 
                dg.ItemsSource = dtInfo.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadCb()
        {
            CleanCb();
            Conexion sql = new Conexion();
            string query = "Select NOM_PLATO from plato WHERE N_USUARIO LIKE '" + Usuario.username + "';";
            MySqlDataReader reader = sql.CargarCb(query);
            while (reader.Read())
            {
                cbDesayuno.Items.Add(reader.GetString("NOM_PLATO"));
                cbComida.Items.Add(reader.GetString("NOM_PLATO"));
                cbMerienda.Items.Add(reader.GetString("NOM_PLATO"));
                cbCena.Items.Add(reader.GetString("NOM_PLATO"));
            }
        }

        private void CleanCb()
        {
            cbCena.Items.Clear();
            cbComida.Items.Clear();
            cbDesayuno.Items.Clear();
            cbMerienda.Items.Clear();
        }
        
        private void ActualizarCb(object sender, RoutedEventArgs e)
        {
            LoadCb();
        }






        private void ActualizarDatos1(object sender, SelectionChangedEventArgs e)
        {
            alimento[0] = cbDesayuno.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[0] + "'", alimento[0], "PLATO", "NOM_PLATO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[0] + "'", "PLATO");
                txtCal1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtGrasas1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtHC1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtAzucar1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtProteina1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                //txtTotal1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
            }
            else
            {
                txtCal1.Text = "00.0";
                txtGrasas1.Text = "00.0";
                txtHC1.Text = "00.0";
                txtAzucar1.Text = "00.0";
                txtProteina1.Text = "00.0";
                //txtTotal1.Text = "00.0";
            }
        }

        private void ActualizarDatos2(object sender, SelectionChangedEventArgs e)
        {
            alimento[1] = cbComida.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[1] + "'", alimento[1], "PLATO", "NOM_PLATO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[1] + "'", "PLATO");
                txtCal2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtGrasas2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtHC2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtAzucar2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtProteina2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                //txtTotal2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
            }
            else
            {
                txtCal2.Text = "00.0";
                txtGrasas2.Text = "00.0";
                txtHC2.Text = "00.0";
                txtAzucar2.Text = "00.0";
                txtProteina2.Text = "00.0";
                //txtTotal2.Text = "00.0";
            }
        }

        private void ActualizarDatos3(object sender, SelectionChangedEventArgs e)
        {
            alimento[2] = cbMerienda.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[2] + "'", alimento[2], "PLATO", "NOM_PLATO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[2] + "'", "PLATO");
                txtCal3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtGrasas3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtHC3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtAzucar3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtProteina3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                //txtTotal3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
            }
            else
            {
                txtCal3.Text = "00.0";
                txtGrasas3.Text = "00.0";
                txtHC3.Text = "00.0";
                txtAzucar3.Text = "00.0";
                txtProteina3.Text = "00.0";
                //txtTotal3.Text = "00.0";
            }
        }

        private void ActualizarDatos4(object sender, SelectionChangedEventArgs e)
        {
            alimento[3] = cbCena.SelectedItem as string;
            Conexion sql = new Conexion();
            if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[3] + "'", alimento[3], "PLATO", "NOM_PLATO") == true)
            {
                DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[3] + "'", "PLATO");
                txtCal4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtGrasas4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtHC4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtAzucar4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                txtProteina4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                //txtTotal4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
            }
            else
            {
                txtCal4.Text = "00.0";
                txtGrasas4.Text = "00.0";
                txtHC4.Text = "00.0";
                txtAzucar4.Text = "00.0";
                txtProteina4.Text = "00.0";
                //txtTotal4.Text = "00.0";
            }
        }

        private void ActualizarArrays1(object sender, TextChangedEventArgs e)
        {
            if (txtDesayuno.Text != "")
            { 
                cantidad[0] = Convert.ToSingle(txtDesayuno.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[0] + "'", alimento[0], "PLATO", "NOM_PLATO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[0] + "'", "PLATO");
                    txtCal1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtGrasas1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtHC1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtAzucar1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtProteina1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    //txtTotal1.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[0] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                }
            }
        }

        private void ActualizarArrays2(object sender, TextChangedEventArgs e)
        {
            if (txtComida.Text != "")
            {

                cantidad[1] = Convert.ToSingle(txtComida.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[1] + "'", alimento[1], "PLATO", "NOM_PLATO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[1] + "'", "PLATO");
                    txtCal2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtGrasas2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtHC2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtAzucar2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtProteina2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    //txtTotal2.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[1] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                }
            }
        }

        private void ActualizarArrays3(object sender, TextChangedEventArgs e)
        {
            if (txtMerienda.Text != "")
            {
                cantidad[2] = Convert.ToSingle(txtMerienda.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[2] + "'", alimento[2], "PLATO", "NOM_PLATO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[2] + "'", "PLATO");
                    txtCal3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtGrasas3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtHC3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtAzucar3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtProteina3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    //txtTotal3.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[2] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                }
            }
        }

        private void ActualizarArrays4(object sender, TextChangedEventArgs e)
        {
            if (txtCena.Text != "")
            {
                cantidad[3] = Convert.ToSingle(txtCena.Text);
                Conexion sql = new Conexion();
                if (sql.Comprobar("SELECT * FROM plato WHERE NOM_PLATO LIKE '" + alimento[3] + "'", alimento[3], "PLATO", "NOM_PLATO") == true)
                {
                    DataTable dt = sql.Rellenar("SELECT CALORIAS,GRASAS,CARBO_HIDRATOS,AZUCAR,PROTEINAS,TOTAL FROM plato WHERE NOM_PLATO LIKE '" + alimento[3] + "'", "PLATO");
                    txtCal4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CALORIAS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtGrasas4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["GRASAS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtHC4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["CARBO_HIDRATOS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtAzucar4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["AZUCAR"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    txtProteina4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["PROTEINAS"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                    //txtTotal4.Text = "" + Convert.ToString(Math.Round(Convert.ToDouble(dt.Rows[0]["TOTAL"]) * cantidad[3] / Convert.ToDouble(dt.Rows[0]["TOTAL"]), 2));
                }
            }
        }
        
        private void GuardarDia(object sender, RoutedEventArgs e)
        {
            string username = Usuario.username;
            string fecha = fechaDia.Text;
            string desayuno = cbDesayuno.SelectedItem as string;
            string comida = cbComida.SelectedItem as string;
            string merienda = cbMerienda.SelectedItem as string;
            string cena = cbCena.SelectedItem as string;
            decimal calorias = Convert.ToDecimal(txtCal1.Text) + Convert.ToDecimal(txtCal2.Text) + Convert.ToDecimal(txtCal3.Text) + Convert.ToDecimal(txtCal4.Text);
            decimal grasas = Convert.ToDecimal(txtGrasas1.Text) + Convert.ToDecimal(txtGrasas2.Text) + Convert.ToDecimal(txtGrasas3.Text) + Convert.ToDecimal(txtGrasas4.Text);
            decimal hidratos = Convert.ToDecimal(txtHC1.Text) + Convert.ToDecimal(txtHC2.Text) + Convert.ToDecimal(txtHC3.Text) + Convert.ToDecimal(txtHC4.Text);
            decimal azucar = Convert.ToDecimal(txtAzucar1.Text) + Convert.ToDecimal(txtAzucar2.Text) + Convert.ToDecimal(txtAzucar3.Text) + Convert.ToDecimal(txtAzucar4.Text);
            decimal proteinas = Convert.ToDecimal(txtProteina1.Text) + Convert.ToDecimal(txtProteina2.Text) + Convert.ToDecimal(txtProteina3.Text) + Convert.ToDecimal(txtProteina4.Text);
            float total = cantidad[0] + cantidad[1] + cantidad[2] + cantidad[3];
            string notas = txtNotas.Text + "-" + Convert.ToString(cantidad[0]) + "-" + Convert.ToString(cantidad[1]) + "-" + Convert.ToString(cantidad[2]) + "-" + Convert.ToString(cantidad[3]);
            
            //MessageBox.Show(fecha + ", " + desayuno + ", " + comida + ", " + merienda + ", " + cena + ", " + calorias + ", " + grasas + ", " + hidratos + ", " + azucar + ", " + proteinas + ", " + total);

            if ((fecha != "") && (desayuno != "") && (comida != "") && (merienda != "") && (cena != ""))
            {
                if(IsModifying == false)
                {
                    string query = "INSERT INTO info_usuario VALUES( '" + username + "', '" + fecha + "', '" + desayuno + "', '" + comida + "', '" + merienda + "', '" + cena + "', '" + calorias + "', '" + grasas + "', '" + hidratos + "', '" + azucar + "', '" + proteinas + "', '" + total + "', '" + notas + "');";
                    //MessageBox.Show(query);
                    Conexion sql = new Conexion();
                    if (sql.ModificarBD(query) == true)
                    {
                        MessageBox.Show("Se ha añadido correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load_datagrid();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido añadir.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    string query = "update info_usuario SET NOM_DESAYUNO = '" + desayuno + "', NOM_COMIDA = '" + comida + "', NOM_MERIENDA = '" + merienda + "', NOM_CENA = '" + cena + "', CALORIAS = '" + calorias + "', GRASAS = '" + grasas + "', CARBO_HIDRATOS = '" + hidratos + "', AZUCAR = '" + azucar + "', PROTEINAS = '" + proteinas + "', TOTAL = '" + total + "', Notas = '" + notas + "' WHERE FECHA LIKE '" + fecha + "';";
                    //MessageBox.Show(query);
                    Conexion sql = new Conexion();
                    if (sql.ModificarBD(query) == true)
                    {
                        MessageBox.Show("Se ha modificado correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        IsModifying = false;
                        Load_datagrid();
                    }
                    else
                    {
                        MessageBox.Show("No se ha podido modificado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Se tienen que rellenar todos los campos para poder guardar, excepto notas.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dg.SelectedItem;

            /* DataGrid de 0 a 11 posiciones */
            //if(Convert.ToString(dg.SelectedCells[0].Column.GetCellContent(item) as TextBlock) != "")
            if(item != null)
            {
                string fecha = (dg.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                string desayuno = (dg.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                string comida = (dg.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                string merienda = (dg.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                string cena = (dg.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                string texto = (dg.SelectedCells[11].Column.GetCellContent(item) as TextBlock).Text;

                string[] notas = texto.Split('-');

                //MessageBox.Show(texto);

                fechaDia.Text = fecha;
                cbDesayuno.SelectedItem = desayuno;
                cbComida.SelectedItem = comida;
                cbMerienda.SelectedItem = merienda;
                cbCena.SelectedItem = cena;
                txtNotas.Text = notas[0];
                txtDesayuno.Text = notas[1];
                txtComida.Text = notas[2];
                txtMerienda.Text = notas[3];
                txtCena.Text = notas[4];
                IsModifying = true;
                tabControl.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Selecciona una fila que tenga datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            

        }
    }
}
