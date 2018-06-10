using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace ProjectWpfClient
{
    /// <summary>
    /// Interaction logic for Courses.xaml
    /// </summary>
    public partial class CoursesWindow : Window
    {
        private EmployeeHandler employeeHandler;
        private ExceptionHandler exHandler;
        private int doctorId;
        public CoursesWindow(int docId)
        {
            InitializeComponent();
            this.doctorId = docId;
            employeeHandler = new EmployeeHandler();
            exHandler = new ExceptionHandler();
            
        }

        private void buttonShowAllCourses_Click(object sender, RoutedEventArgs e)
        {
            var courses = employeeHandler.GetAllCourses(doctorId);
            if (courses == null)
            {
                MessageBox.Show("Error Accured When Trying To Get Users Data!");
            }
            else if (courses.Count() < 1)
            {
                MessageBox.Show("There is no Courses to Display!");
            }
            else
            {
                dataGridCourses.ItemsSource = courses;
                //Remove Column with name Doctor
                dataGridCourses.Columns.RemoveAt(4);
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonGetACourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(textBoxCourseIdSearch.Text.Trim());
                var course = employeeHandler.GetACourse(id);
                if (course == null)
                {
                    if (textBoxCourseId.Text != null)
                    {
                        clearTextBoxes();
                    }
                    MessageBox.Show("There is no Course found with this ID ! : ");
                }
                else
                {
                    textBoxCourseId.Text = course.ID.ToString();
                    textBoxCourseCode.Text = course.Code;
                    textBoxCourseDescription.Text = course.Describtion;
                }
            }
            catch (Exception exception)
            {
                if (textBoxCourseId.Text != null)
                {
                    clearTextBoxes();
                }
                MessageBox.Show(exception.Message + "Enter A Valid ID plz! : ");
            }
        }


        private void clearTextBoxes()
        {
            textBoxCourseId.Text = "";
            textBoxCourseCode.Text = "";
            textBoxCourseDescription.Text ="";
            textBoxCourseIdSearch.Text = "Course Id ?";
        }

        private void buttonAddNewCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool result = false;

                if (textBoxCourseCode.Text == string.Empty || textBoxCourseDescription.Text == string.Empty)
                {
                    MessageBox.Show("Fill All Fields Plz!");
                    return;
                }

                else
                {
                    result = exHandler.IsValidUserName(textBoxCourseCode.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Code plz!");
                        return;
                    }

                    result = exHandler.IsValidDescription(textBoxCourseDescription.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Description plz!");
                        return;
                    }

                }

                HttpResponseMessage response = employeeHandler.AddNewCourse(doctorId,textBoxCourseCode.Text, textBoxCourseDescription.Text);


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("New Course Added Successfully.");
                    clearTextBoxes();
                    buttonShowAllCourses.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show(response.ReasonPhrase + "Error! This Code : " + textBoxCourseCode.Text + " Is Not Available.");
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

        private void buttonUpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (textBoxCourseId.Text == "")
                {
                    MessageBox.Show("Please Get a Course first !");
                    return;
                }

                bool result = false;

                if (textBoxCourseCode.Text == string.Empty || textBoxCourseDescription.Text == string.Empty)
                {
                    MessageBox.Show("Fill All Fields Plz!");
                    return;
                }

                else
                {
                    result = exHandler.IsValidUserName(textBoxCourseCode.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Code plz!");
                        return;
                    }

                    result = exHandler.IsValidDescription(textBoxCourseDescription.Text);

                    if (!result)
                    {
                        MessageBox.Show("Enter A Valid Description plz!");
                        return;
                    }

                }

                HttpResponseMessage response = employeeHandler.UpdateCourse(int.Parse(textBoxCourseId.Text),doctorId, textBoxCourseCode.Text, textBoxCourseDescription.Text);


                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Course Updated Successfully !.");
                    clearTextBoxes();
                    buttonShowAllCourses.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show(response.ReasonPhrase + "Error! This Code : " + textBoxCourseCode.Text + " Is Not Available.");
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

        private void buttonDeleteCourse_Click(object sender, RoutedEventArgs e)
        {

            int id;
            if (textBoxCourseId.Text == "")
            {
                MessageBox.Show("Please Get a Course first !");
                return;
            }
            else
            {
                id = int.Parse(textBoxCourseId.Text);
            }

            HttpResponseMessage response = employeeHandler.DeleteCourse(id);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Course Deleted Successfully !");
                clearTextBoxes();
                buttonShowAllCourses.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));

            }
            else
            {
                MessageBox.Show(response.StatusCode + " With Message : " + response.ReasonPhrase);
            }
        }
    }
}
