using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProjectWpfClient
{
    class EmployeeHandler
    {
        HttpClient client;
        public EmployeeHandler()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost/ProjectAPI/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

       
        public IEnumerable<Doctor> GetAllDoctors()
        {
            HttpResponseMessage response = this.client.GetAsync("api/Doctor").Result;

            if (response.IsSuccessStatusCode)
            {
                var doctors = response.Content.ReadAsAsync<IEnumerable<Doctor>>().Result;
                return doctors;
            }
            else
            {
                return null;
            }
        }

        public Doctor GetADoctor(int id)
        {
            HttpResponseMessage response = this.client.GetAsync("api/Doctor/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var doctor = response.Content.ReadAsAsync<Doctor>().Result;

                return doctor;
            }
            else
            {
                return null;
            }
        }

        public HttpResponseMessage AddNewDoctor(string name, string title, byte[] image)
        {
            var doctor = new Doctor();
            doctor.Name = name;
            doctor.Title = title;
            doctor.Image = image;

            var response = this.client.PostAsJsonAsync("api/Doctor", doctor).Result;
            return response;
        }

        public HttpResponseMessage UpdateDoctor(int id, string name, string title, byte[] image)
        {
            var doctor = new Doctor();
            doctor.Id = id;
            doctor.Name = name;
            doctor.Title = title;
            doctor.Image = image;

            HttpResponseMessage response = this.client.PutAsJsonAsync("api/Doctor/" + id, doctor).Result;

            return response;
        }

        public HttpResponseMessage DeleteDoctor(int id)
        {

            HttpResponseMessage response = this.client.DeleteAsync("api/Doctor/" + id).Result;

            return response;
        }


        public IEnumerable<Course> GetAllCourses(int doctorID)
        {
            HttpResponseMessage response = this.client.GetAsync("api/Course").Result;

            if (response.IsSuccessStatusCode)
            {
                var courses = response.Content.ReadAsAsync<IEnumerable<Course>>().Result;
                return courses.Where(c => c.DoctorID == doctorID);
            }
            else
            {
                return null;
            }
        }


        public Course GetACourse(int id)
        {
            HttpResponseMessage response = this.client.GetAsync("api/Course/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var course = response.Content.ReadAsAsync<Course>().Result;

                return course;
            }
            else
            {
                return null;
            }
        }


        public HttpResponseMessage AddNewCourse(int dcotorID, string code, string description)
        {
            var course = new Course();
            course.DoctorID = dcotorID;
            course.Code = code;
            course.Describtion = description;

            var response = this.client.PostAsJsonAsync("api/Course", course).Result;
            return response;
        }

        public HttpResponseMessage UpdateCourse(int courseId , int dcotorId, string code, string description)
        {
            var course = new Course();
            course.ID = courseId;
            course.DoctorID = dcotorId;
            course.Code = code;
            course.Describtion = description;

            var response = this.client.PutAsJsonAsync("api/Course/"+courseId, course).Result;
            return response;
        }


        public HttpResponseMessage DeleteCourse(int id)
        {

            HttpResponseMessage response = this.client.DeleteAsync("api/Course/" + id).Result;

            return response;
        }


    }
}
