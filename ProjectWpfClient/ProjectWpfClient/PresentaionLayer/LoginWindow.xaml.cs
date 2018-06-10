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

namespace ProjectWpfClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AdminHandler adminHandler;
        ExceptionHandler exHandler;
        public LoginWindow()
        {
            InitializeComponent();
            adminHandler = new AdminHandler();
            exHandler = new ExceptionHandler();
        }

        private void butLogin_Click(object sender, RoutedEventArgs e)
        {
            if(textboxUsername.Text == string.Empty || textboxPassword.Password.ToString() == string.Empty)
            {
                MessageBox.Show("Fill All Fields Plz!");
                return;
            }
            bool result = false;
            result = exHandler.IsValidUserName(textboxUsername.Text);
            if (!result)
            {
                MessageBox.Show("Enter a Valid Username plz!");
                return;
            }
            result = exHandler.IsValidUserName(textboxPassword.Password.ToString());
            if (!result)
            {
                MessageBox.Show("Enter a Valid Password plz!");
                return;
            }
            //End Exeption handleing

            
            var users = adminHandler.getALLSystemUsers();
           
            var user = users.FirstOrDefault(c => c.Username == textboxUsername.Text && Encryption.Decrypt(c.Password )==textboxPassword.Password.ToString() );

            if (user != null )
            {
                string name = user.FirstName + " " + user.LastName;
                if(user.UserType == 1)
                {
                    MainWindow main = new MainWindow(name);
                    App.Current.MainWindow = main;
                    Close();
                    main.Show();
                }
                else if(user.UserType == 2)
                {
                    EmployeeWindow employeeWindow = new EmployeeWindow(name ,user.Image);
                    App.Current.MainWindow = employeeWindow;
                    Close();
                    employeeWindow.Show();
                }
               
            }

            else
            {
                MessageBox.Show("User Not Exist.\n Try Again!");
            }

        }
    }
}
