using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCrud.DbCtx;
using System.Data.Entity;

namespace WebApiCrud.Controllers
{
    public class TaskController : ApiController
    {
        MukeshDb db = new MukeshDb();
        [HttpGet]
        [Route("api/getData")]
        public List<Friend> GetData()
        {
            var res = db.Friends.ToList();
            return res;
        }

        [HttpDelete]
        [Route("api/delete")]
        public void Delete(int id)
        {
            var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();
            if(res != null)
            {
                db.Friends.Remove(res);
                db.SaveChanges();
            }
        }

        [HttpPost]
        [Route("api/addOrEdit")]
        public void AddOrEdit(Friend f)
        {
            if (f.Id == 0)
            {
                db.Friends.Add(f);
                db.SaveChanges();
            }
            else
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        [HttpGet]
        [Route("api/getUserById")]
        public Friend GetUserById(int id)
        {
            var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();
            return res;
        }
    }
}