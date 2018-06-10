using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiDataAccessLayer;

namespace ProjectAPI.Controllers
{
    public class AdminController : ApiController
    {
        public IEnumerable<User> Get()
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
              var users =    _entites.Users.ToList();

                

                return users;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int ID)
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                var user = _entites.Users.FirstOrDefault(c => c.ID == ID && c.UserType != 1);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not Found with this ID : "+ID);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, user);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(User user)
        {
            HttpResponseMessage msg ;
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                try
                {
                    var u = _entites.Users.FirstOrDefault(c => c.Username == user.Username);
                    if (u == null)
                    {
                        _entites.Users.Add(user);
                        _entites.SaveChanges();

                        msg = Request.CreateResponse(HttpStatusCode.Created, user);
                        msg.Headers.Location = new Uri(Request.RequestUri + "/" + user.ID);
                    }
                    else
                    {
                        msg = Request.CreateErrorResponse(HttpStatusCode.Conflict, "Error! This UserName : " + user.Username + " Is Not Available.");
                    }
                    

                    return msg;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }

/// <summary>
/// 
/// </summary>
/// <param name="ID"></param>
/// <param name="myuser"></param>
/// <returns></returns>

        public HttpResponseMessage Put(int ID, User myuser)
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                try
                {
                    var u = _entites.Users.FirstOrDefault(c => c.Username == myuser.Username && c.ID != myuser.ID);

                    if (u == null)
                    {
                        var user = _entites.Users.FirstOrDefault(c => c.ID == ID);
                        if (user == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not Found with this ID : " + ID);
                        }
                        else
                        {
                            user.FirstName = myuser.FirstName;
                            user.LastName = myuser.LastName;
                            user.Email = myuser.Email;
                            user.Gender = myuser.Gender;
                            if(myuser.Image != null)
                            {
                                user.Image = myuser.Image;
                            }
                            
                            user.Username = myuser.Username;
                            user.Password = myuser.Password;

                            _entites.SaveChanges();
                            return Request.CreateResponse(HttpStatusCode.OK, myuser);
                        }
                    }
                    else return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Error! This UserName : "+ myuser.Username+" Is Not Available.");

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>

        public HttpResponseMessage Delete(int ID)
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                var user = _entites.Users.FirstOrDefault(c => c.ID == ID);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not Found with this ID : " + ID);
                }
                else
                {
                    _entites.Users.Remove(user);
                    _entites.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
    }
}
