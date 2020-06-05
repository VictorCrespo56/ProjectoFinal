using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp1.Pops;


namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para Login1.xaml
    /// </summary>
    public partial class Login1 : Window
    {
        bool IsAppClosing = true;

        public Login1()
        {
            InitializeComponent();
        }

        private void BtCrear_Click(object sender, RoutedEventArgs e)
        {
            Login2 singIn = new Login2();
            singIn.ShowDialog();
            
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void IsClosing(object sender, CancelEventArgs e)
        {
            if(IsAppClosing == true)
            {
                Environment.Exit(0);
            }
        }

        private void BtEntrar_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            //MessageBox.Show(username + ", " + password);

            Conexion sql = new Conexion();
            string query = "SELECT * FROM USUARIO WHERE USUARIO LIKE '" + username + "'";
            if(sql.Comprobar(query, username, "USUARIO", "USUARIO") == true)
            {
                query = "SELECT * FROM USUARIO WHERE PASSWORD LIKE '" + password + "'";
                if(sql.Comprobar(query, password, "USUARIO", "PASSWORD") == true)
                {
                    IsAppClosing = false;
                    Usuario.username = username;
                    Close();
                }
                else
                {
                    MessageBox.Show("La contraseña introducida no es correcta.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("El usuario introducido no existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BorrartxtPwd(object sender, MouseEventArgs e)
        {
            if (txtPassword.Password == "Contraseña")
            {
                txtPassword.Password = "";
            }
        }

        private void BorrartxtUser(object sender, MouseEventArgs e)
        {
            if (txtUsername.Text == "Usuario")
            {
                txtUsername.Text = "";
            }
        }
        
        private void BorrartxtUser2(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtUsername.Text == "Usuario")
            {
                txtUsername.Text = "";
            }
        }

        private void BorrartxtPwd2(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtPassword.Password == "Contraseña")
            {
                txtPassword.Password = "";
            }
        }

    }
}
