using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ProjectWpfClient
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
       private readonly EmployeeHandler _employeeHandler ;
       private  readonly ExceptionHandler _exHandler ;
       private BitmapImage _doctorImage;  //after Uploading image assign it to this variable
        public EmployeeWindow(string name ,byte[] image)  
        {
            InitializeComponent();

            _employeeHandler = new EmployeeHandler();
            _exHandler = new ExceptionHandler();
            _doctorImage = null;
            welcomelabel.Content = welcomelabel.Content.ToString() + name  ;
            //Load Image Employee
            try
            {
                employeeImage.Source = LoadImage(image);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Uplading Photo! :" + ex.Message);
            }

        }
 
        private void buttonLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            App.Current.MainWindow = loginWindow;
            Close();
            loginWindow.Show();
        }

        private void buttonShowAllDoctors_Click(object sender, RoutedEventArgs e)
        {
            var doctors = _employeeHandler.GetAllDoctors();
            if (doctors == null)
            {
                MessageBox.Show("Error Accured When Trying To Get Doctors Data!");
            }
            else if (doctors.Count() < 1)
            {
                MessageBox.Show("There is no Doctors to Display!");
            }
            else
            {
                doctorDataGrid.ItemsSource = doctors;
                //Remove Column with name Courses
                doctorDataGrid.Columns.RemoveAt(4);
            }
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
                try
                {
                    _doctorImage = new BitmapImage();
                    _doctorImage.BeginInit();
                    _doctorImage.UriSource = new Uri(op.FileName);
                    _doctorImage.EndInit();
                    imageDoctor.Source = _doctorImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error in Uplading Photo! :" + ex.Message);
                }
            }
        }

        private void buttonAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = false;

                if (textBoxName.Text == string.Empty || textBoxTitle.Text == string.Empty)
                {
                    MessageBox.Show("Fill All Fields Plz!");
                    return;
                }

                else
                {
                    result = _exHandler.IsValidString(textBoxName.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Name plz!");
                        return;
                    }

                    result = _exHandler.IsValidString(textBoxTitle.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Title plz!");
                        return;
                    }

                    if (_doctorImage == null)
                    {
                        MessageBox.Show("Upload Photo plz!");
                        return;
                    }
                }

                HttpResponseMessage response = _employeeHandler.AddNewDoctor(textBoxName.Text, textBoxTitle.Text, getImageByteArray(_doctorImage));

               
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("New Doctor Added Successfully.");
                    clearTextBoxes();
                    buttonShowAllDoctors.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));


                    //Get Doctor ID and Open CoursesWindow and path id to it.
                    var doctors = _employeeHandler.GetAllDoctors();
                    int doctorId=0;
                    if (doctors == null)
                    {
                        MessageBox.Show("Error Accured When Trying To Get Doctors Data!");
                    }
                    else if (doctors.Count() < 1)
                    {
                        MessageBox.Show("There is no Doctors to Display!");
                    }
                    else
                    {
                        List<Doctor> mydoctors = doctors.ToList();
                        doctorId = mydoctors.Max(item => item.Id);
                    }


                    CoursesWindow coursesWindow = new CoursesWindow(doctorId);
                    App.Current.MainWindow = coursesWindow;
                    coursesWindow.Show();

                }
                else
                {
                    MessageBox.Show(response.StatusCode + "With Message : " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" There is an ERROR ! with messamge :" + ex.Message);
            }
        }


        private void buttonGetADoctor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxIDSearch.Text.Trim());
                var doctor = _employeeHandler.GetADoctor(id);
                if (doctor == null)
                {
                    if (textBoxID.Text != "")
                    {
                        clearTextBoxes();
                    }
                    MessageBox.Show("There is no Doctor found with this ID ! : ");
                }
                else
                {
                    textBoxID.Text = doctor.Id.ToString();
                    textBoxName.Text = doctor.Name;
                    textBoxTitle.Text = doctor.Title;
                    imageDoctor.Source = LoadImage(doctor.Image);
                    
                    
                }
            }
            catch (Exception exception)
            {
                if (textBoxID.Text != null)
                {
                    clearTextBoxes();
                }
                MessageBox.Show(exception.Message + "Enter A Valid ID plz! : ");
            }
        }


        private void buttonUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                if (textBoxID.Text == "")
                {
                    MessageBox.Show("Please Get a Doctor first !");
                    return;
                }

                bool result = false;
                if (textBoxName.Text == string.Empty || textBoxTitle.Text == string.Empty)
                {
                    MessageBox.Show("Fill All Fields Plz!");
                    return;
                }

                else
                {
                    result = _exHandler.IsValidString(textBoxName.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Name plz!");
                        return;
                    }

                    result = _exHandler.IsValidString(textBoxTitle.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Title plz!");
                        return;
                    }

                    HttpResponseMessage response = new HttpResponseMessage();

                    if (_doctorImage != null)
                    {
                        response = _employeeHandler.UpdateDoctor(int.Parse(textBoxID.Text.Trim()),
                            textBoxName.Text, textBoxTitle.Text, getImageByteArray(_doctorImage));
                    }
                    else
                    {
                         response = _employeeHandler.UpdateDoctor(int.Parse(textBoxID.Text.Trim()),
                            textBoxName.Text, textBoxTitle.Text, null);
                    }
                   

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(" Doctor Data Updated Successfully !.");
                        clearTextBoxes();
                        buttonShowAllDoctors.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                       

                    }
                    else
                    {
                        MessageBox.Show(response.StatusCode + "With Message : " + response.ReasonPhrase);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" There is an ERROR ! with messamge :" + ex.Message);
            }
        }



        private void buttonDeleteUser_Click(object sender, RoutedEventArgs e)
        {

            int id;
            if (textBoxID.Text == "")
            {
                MessageBox.Show("Please Get a Doctor first !");
                return;
            }
            else
            {
                id = int.Parse(textBoxID.Text);
            }

            HttpResponseMessage response = _employeeHandler.DeleteDoctor(id);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Doctor Deleted Successfully !");
                clearTextBoxes();
                buttonShowAllDoctors.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

               

            }
            else
            {
                MessageBox.Show(response.StatusCode + " With Message : " + response.ReasonPhrase);
            }
        }


        private void buttonShowCourses_click(object sender, RoutedEventArgs e)
        {
            int id;
            if (textBoxID.Text == "")
            {
                MessageBox.Show("Please Get a Doctor first !");
                return;
            }
            else
            {
                id = int.Parse(textBoxID.Text);
            }


            CoursesWindow coursesWindow = new CoursesWindow(id);
            App.Current.MainWindow = coursesWindow;
            coursesWindow.Show();
        }

        //Function to load Image
        private BitmapImage LoadImage(byte[] imageData)
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

        //Function to convert bitmap image to array of bytes
        private byte[] getImageByteArray(BitmapImage image)
        {
            MemoryStream memSt = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memSt);
            return memSt.ToArray();
        }

        private void clearTextBoxes()
        {
            textBoxName.Text = "";
            textBoxTitle.Text = "";
            //image
            imageDoctor.Source = null;
            textBoxID.Text = "";
            textBoxIDSearch.Text = "Doctor ID ?";
            _doctorImage = null;
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
        }
    }
}
