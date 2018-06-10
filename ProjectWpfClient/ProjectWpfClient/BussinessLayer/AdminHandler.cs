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
    class AdminHandler
    {
        HttpClient client;
        public AdminHandler()
        {
             this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost/ProjectAPI/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public IEnumerable<User> getALLEmployees()
        {
           
            HttpResponseMessage response = this.client.GetAsync("api/admin").Result;

            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

               

                return users.Where(c => c.UserType == 2);
            }
            else
            {
              
                return null;
            } 


        }
        public IEnumerable<User> getALLSystemUsers()
        {

            HttpResponseMessage response = this.client.GetAsync("api/admin").Result;

            if (response.IsSuccessStatusCode)
            {
                var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;



                return users;
            }
            else
            {

                return null;
            }


        }

        public User getUser(int id)
        {

            HttpResponseMessage response = this.client.GetAsync("api/admin/"+id).Result;

            if (response.IsSuccessStatusCode)
            {
                User user = response.Content.ReadAsAsync<User>().Result;
                user.Password =  Encryption.Decrypt(user.Password);

                return user;
            }
            else return null;


        }

        public HttpResponseMessage AddNewUser(string fn, string ln , string em , string userna, string pass , string gend,byte[] image)
        {
            var user = new User();
            user.FirstName = fn;
            user.LastName = ln;
            user.Email = em;
            user.Username = userna;
            user.Password = Encryption.Encrypt(pass);
            user.UserType = 2;
            user.Gender = gend;
            user.Image = image;

            var response = this.client.PostAsJsonAsync("api/admin/", user).Result;
            return response;

        }

        public HttpResponseMessage UpdateUser(int id,string fn, string ln, string em, string userna, string pass, string gend,byte[] photo)
        {
            var user = new User();
            user.ID = id;
            user.FirstName = fn;
            user.LastName = ln;
            user.Email = em;
            user.Username = userna;
            user.Password = Encryption.Encrypt(pass);
            user.UserType = 2;
            user.Gender = gend;
            user.Image = photo;
            //image///////////////////////////

            var response = this.client.PutAsJsonAsync("api/admin/"+ id
                , user).Result;
            return response;

        }

        public HttpResponseMessage DeleteUser(string id)
        {
           
            //image///////////////////////////

            var response = this.client.DeleteAsync("api/admin/"+id).Result;
            return response;

        }


    }
}
