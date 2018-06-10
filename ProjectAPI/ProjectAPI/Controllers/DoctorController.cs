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
    public class DoctorController : ApiController
    {
        public IEnumerable<Doctor> Get()
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                // ده حل للايرور اللي طلع عيني ^_*
                _entites.Configuration.ProxyCreationEnabled = false;

                var doctors = _entites.Doctors.ToList();
                return doctors;
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

                var doctor = _entites.Doctors.FirstOrDefault(d => d.Id == ID);
                if (doctor == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Doctor is not Found with this ID : "+ID);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, doctor);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="doctor"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(Doctor doctor)
        {
            HttpResponseMessage msg ;
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                try
                {
                    _entites.Doctors.Add(doctor);
                    _entites.SaveChanges();

                    msg = Request.CreateResponse(HttpStatusCode.Created, doctor);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + doctor.Id);
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
/// <param name="doctor"></param>
/// <returns></returns>

        public HttpResponseMessage Put(int ID, Doctor doctor)
        {
            using (ProjectAPIEntities _entites = new ProjectAPIEntities())
            {
                try
                {
                    var myDoctor = _entites.Doctors.FirstOrDefault(d => d.Id == ID);
                    if (myDoctor == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not Found with this ID : " + ID);
                    }
                    else
                    {
                        myDoctor.Name = doctor.Name;
                        myDoctor.Title = doctor.Title;
            
                        if (doctor.Image != null)
                        {
                            myDoctor.Image = doctor.Image;
                        }

                        _entites.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, doctor);
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
                var doctor = _entites.Doctors.FirstOrDefault(d => d.Id == ID);
                if (doctor == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User is not Found with this ID : " + ID);
                }
                else
                {
                    _entites.Doctors.Remove(doctor);
                    _entites.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
        }
    }
}
