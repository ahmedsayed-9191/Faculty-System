using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiDataAccessLayer;

namespace ProjectAPI.Controllers
{
    public class CourseController : ApiController
    {
        public IEnumerable<Course> Get()
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                // ده حل للايرور اللي طلع عيني ^_*
                _entites.Configuration.ProxyCreationEnabled = false;

                var courses = _entites.Courses.ToList();
                return courses;
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
                // ده حل للايرور اللي طلع عيني ^_*
                _entites.Configuration.ProxyCreationEnabled = false;

                var course = _entites.Courses.FirstOrDefault(c => c.ID == ID);
                if (course == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course is not Found with this ID : "+ID);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, course);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(Course course)
        {
            HttpResponseMessage msg ;
            using (ProjectAPIEntities _entites = new ProjectAPIEntities() )
            {
                
                try
                {   //prevent Add course with an exist Cod
                    var crs = _entites.Courses.FirstOrDefault(c => c.Code == course.Code);
                    if (crs == null)
                    {
                        _entites.Courses.Add(course);
                        _entites.SaveChanges();


                        msg = Request.CreateResponse(HttpStatusCode.Created, course);
                        msg.Headers.Location = new Uri(Request.RequestUri + "/" + course.ID);
                        
                    }
                    else
                    {
                        msg = Request.CreateErrorResponse(HttpStatusCode.Conflict, "Error! This Code : " + course.Code + " Is Not Available.");
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
/// <param name="course"></param>
/// <returns></returns>

        public HttpResponseMessage Put(int ID, Course course)
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                try
                {
                    //prevent update course with an exist Code
                    var crs = _entites.Courses.FirstOrDefault(c => c.Code == course.Code && c.ID != course.ID);

                    if (crs == null)
                    {
                        var mycourse = _entites.Courses.FirstOrDefault(c => c.ID == ID);
                        if (mycourse == null)
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                                "Course is not Found with this ID : " + ID);
                        }
                        else
                        {
                            mycourse.Code = course.Code;
                            mycourse.Describtion = course.Describtion;

                            _entites.SaveChanges();

                            return Request.CreateResponse(HttpStatusCode.OK, course);
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Error! This Code : " + course.Code + " Is Not Available.");
                    }


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
                var mycourse = _entites.Courses.FirstOrDefault(c => c.ID == ID);
                if (mycourse == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course is not Found with this ID : " + ID);
                }
                else
                {
                    _entites.Courses.Remove(mycourse);
                    _entites.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
    }
}
