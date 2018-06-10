
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.Net.Http.Formatting;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.Net;
using System.IO;

namespace ProjectWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AdminHandler _handler;
        private readonly ExceptionHandler _exHandler;
        private  BitmapImage _userImage;

        public MainWindow(string name)
        {

            InitializeComponent();
            List<string> genderList = new List<string>();
            genderList.Add("Male");
            genderList.Add("Female");
            cbxGender.ItemsSource = genderList;
            _handler = new AdminHandler();
            _exHandler = new ExceptionHandler();
            _userImage = null;
            welcomelabel.Content = welcomelabel.Content.ToString() + " " + name;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddNewUser_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                bool result = false;
            if (textBoxfirstname.Text == string.Empty || textBoxlastname.Text == string.Empty || textBoxemail.Text == string.Empty
               || textBoxpassword.Password.ToString() == string.Empty || cbxGender.Text == "--Select a Gender--" || textBoxusername.Text == string.Empty)
            {
                                 
                        MessageBox.Show("Fill All Fields Plz!");
                        return;
            }

            else
            {
                result = _exHandler.IsValidString(textBoxfirstname.Text);
                result = _exHandler.IsValidString(textBoxlastname.Text);
                if (!result)
                {
                    MessageBox.Show("Enter A Valid Name plz!");
                    return;
                }

                result = _exHandler.IsValidEmail(textBoxemail.Text);

                if (!result)
                {
                    MessageBox.Show("Enter A Valid Email plz!");
                    return;
                }

                result = _exHandler.IsValidUserName(textBoxusername.Text);

                if (!result || textBoxusername.Text.Length < 6)
                {
                    MessageBox.Show("Enter A Valid Username plz!('Characters Mustn't be less than 6')");
                    return;
                }

                if(textBoxpassword.Password.ToString().Length <6)

                {
                    MessageBox.Show("Password Characters Mustn't be less than 6");
                    return;
                }
                if(_userImage == null)
                    {
                        MessageBox.Show("Upload Photo plz!");
                        return;
                    }
               
                HttpResponseMessage response = _handler.AddNewUser(textBoxfirstname.Text, textBoxlastname.Text, textBoxemail.Text, textBoxusername.Text, textBoxpassword.Password.ToString(),cbxGender.SelectedValue.ToString(),getImageByteArray(_userImage));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("New User Added Successfully.");
                    clearTextBoxes();
                    buttonShowAllCustomers.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        MessageBox.Show(response.ReasonPhrase + "Error! This UserName : " + textBoxusername.Text + " Is Not Available.");
                    }
                else 
                {
                    MessageBox.Show(response.StatusCode+"With Message : "+ response.ReasonPhrase);
                }


            }

            }
            catch (Exception ex)

            {
                MessageBox.Show(" ERROR !" + ex.Message);
            }

        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = false;
                if (textBoxfirstname.Text == string.Empty || textBoxlastname.Text == string.Empty || textBoxemail.Text == string.Empty
                   || textBoxpassword.Password.ToString() == string.Empty || cbxGender.Text == "--Select a Gender--" || textBoxusername.Text == string.Empty)
                {

                    MessageBox.Show("Fill All Fields Plz!");
                    return;
                }

                else
                {
                    result = _exHandler.IsValidString(textBoxfirstname.Text);
                    result = _exHandler.IsValidString(textBoxlastname.Text);
                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Name plz!");
                        return;
                    }

                    result = _exHandler.IsValidEmail(textBoxemail.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Email plz!");
                        return;
                    }

                    result = _exHandler.IsValidUserName(textBoxusername.Text);

                    if (!result || textBoxusername.Text.Length < 6)
                    {
                        MessageBox.Show("Enter A Valid Username plz!('Characters Mustn't be less than 6')");
                        return;
                    }

                    if (textBoxpassword.Password.ToString().Length < 6)

                    {
                        MessageBox.Show("Password Characters Mustn't be less than 6");
                        return;
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    
                    if (_userImage != null)
                    {
                         response = _handler.UpdateUser(int.Parse(textBoxCustID.Text.Trim()), textBoxfirstname.Text, textBoxlastname.Text, textBoxemail.Text, textBoxusername.Text, textBoxpassword.Password.ToString(), cbxGender.SelectedValue.ToString(),getImageByteArray(_userImage));
                    }
                    else
                    {
                         response = _handler.UpdateUser(int.Parse(textBoxCustID.Text.Trim()), textBoxfirstname.Text, textBoxlastname.Text, textBoxemail.Text, textBoxusername.Text, textBoxpassword.Password.ToString(), cbxGender.SelectedValue.ToString(), null);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("User Data Updated Successfully.");
                        clearTextBoxes();
                        buttonShowAllCustomers.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

                    }
                    else if (response.StatusCode == HttpStatusCode.Conflict)
                    {
                        MessageBox.Show(response.ReasonPhrase + "Error! This UserName : " + textBoxusername.Text + " Is Not Available.");
                    }
                    else
                    {
                        MessageBox.Show(response.StatusCode + "With Message : " + response.ReasonPhrase);
                    }


                }

            }
            catch (Exception ex)

            {
                MessageBox.Show(" ERROR !" + ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var custId = textBoxID.Text.Trim();

            HttpResponseMessage response = _handler.DeleteUser(custId);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User Deleted Successfully.");
                clearTextBoxes();
                buttonShowAllCustomers.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

                buttonUpdate.Visibility = Visibility.Visible;
                buttonDelete.Visibility = Visibility.Visible;

            }
            else
                MessageBox.Show(response.StatusCode+" With Message : "+ response.ReasonPhrase);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                int id =int.Parse( textBoxCustID.Text.Trim());
                User user = _handler.getUser(id);
                if (user == null )
                {
                    if (textBoxID.Text != null)
                    {
                        clearTextBoxes();
                       // buttonUpdate.Visibility = Visibility.Visible;
                        //buttonDelete.Visibility = Visibility.Visible;
                    }
                    MessageBox.Show("There is no User found with this ID ! : ");
                }
                else
                {
                    textBoxfirstname.Text = user.FirstName;
                    textBoxlastname.Text = user.LastName;
                    textBoxemail.Text = user.Email;
                    textBoxusername.Text = user.Username;
                    textBoxpassword.Password = user.Password;
                    imagecustomer.Source = LoadImage(user.Image);
                    //image
                    cbxGender.SelectedValue = user.Gender;
                    textBoxID.Text = user.ID.ToString() ;
                    //buttonUpdate.Visibility = Visibility.Visible;
                   
                    //buttonDelete.Visibility = Visibility.Visible;
                }
                
            }
            catch(Exception ex)
            {
                if (textBoxID.Text != null)
                {
                    clearTextBoxes();
                  
                }
                MessageBox.Show(ex.Message + "Enter A Valid ID plz! : ");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowAllCustomer_Click(object sender, RoutedEventArgs e)
        {
            var users = _handler.getALLEmployees();
            if (users == null)
                MessageBox.Show("Error Accured When Trying To Get Users Data!");
            else if(users.Count()<1)
                MessageBox.Show("There is no Users to Display!");
            else
            customerdataGrid.ItemsSource = users;
            customerdataGrid.Columns.RemoveAt(8);
        }

        private void clearTextBoxes()
        {
            textBoxfirstname.Text = "";
            textBoxlastname.Text = "";
            textBoxemail.Text = "";
            textBoxusername.Text = "";
            //image
            imagecustomer.Source =null ;
            textBoxpassword.Password = "";
            cbxGender.Text = "--Select a Gender--";
            textBoxID.Text = "";
            textBoxCustID.Text = "Enter Cust ID";
        }

        private void buttonUpload_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                try {
                    //Image Of Employee one of his data
                    _userImage = new BitmapImage();
                    _userImage.BeginInit();
                    _userImage.UriSource = new Uri(op.FileName);
                    //Rotate Image Before Saving
                    _userImage.Rotation = Rotation.Rotate90;
                    _userImage.EndInit();
                    
                    //Image Control 
                    imagecustomer.Source = _userImage;
                 
                }catch (Exception ex)
                {
                    MessageBox.Show("Error in Uplading Photo! :" + ex.Message);
                }
            }
        }

        private byte[] getImageByteArray(BitmapImage image)
        {
            MemoryStream memSt = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memSt);
            return memSt.ToArray();
        }

        private  BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            App.Current.MainWindow = loginWindow;
            Close();
            loginWindow.Show();
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
        }
    }
}
