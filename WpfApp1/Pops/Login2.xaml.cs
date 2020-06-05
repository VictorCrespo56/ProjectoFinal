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

namespace WpfApp1.Pops
{
    /// <summary>
    /// Lógica de interacción para Login2.xaml
    /// </summary>
    public partial class Login2 : Window
    {
        public Login2()
        {
            InitializeComponent();
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtCrear_Click(object sender, RoutedEventArgs e)
        {
            string nom = txtNom.Text;
            string ap = txtAp.Text;
            string username = txtUser.Text;
            string password = txtPwd.Password;
            Conexion sql = new Conexion();

            if(sql.Comprobar("SELECT * FROM USUARIO WHERE USUARIO LIKE '" + username + "'", username, "USUARIO", "USUARIO") == false)
            {
                string query = "INSERT INTO USUARIO (NOMBRE, APELLIDO, USUARIO, PASSWORD, ROL) VALUES ( '" + nom + "', '" + ap + "', '" + username + "', '" + password + "', 2);";
                if (sql.ModificarBD(query) == true)
                {
                    MessageBox.Show("Se ha creado con éxito su cuenta.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("No se ha podido crear su cuenta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("El nombre de usuario ya existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BorrartxtNom(object sender, MouseEventArgs e)
        {
            if (txtNom.Text == "Nombre")
            {
                txtNom.Text = "";
            }
        }

        private void BorrartxtAp(object sender, MouseEventArgs e)
        {
            if (txtAp.Text == "Apellidos")
            {
                txtAp.Text = "";
            }
        }

        private void BorrartxtUser(object sender, MouseEventArgs e)
        {
            if (txtUser.Text == "Usuario")
            {
                txtUser.Text = "";
            }
        }

        private void BorrartxtPwd(object sender, MouseEventArgs e)
        {
            if (txtPwd.Password == "Contraseña")
            {
                txtPwd.Password = "";
            }
        }
    }
}
