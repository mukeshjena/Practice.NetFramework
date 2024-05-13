using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.DbCtx;
using System.Data.Entity;

namespace WebApi.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/getdata")]
        public List<Friend> GetEmployees()
        {
            MukeshDb db = new MukeshDb();
            var res = db.Friends.ToList();
            return res;
        }

        [HttpDelete]
        [Route("api/delete")]
        public void Delete(int id)
        {
            MukeshDb db = new MukeshDb();

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
            MukeshDb db = new MukeshDb();
            if(f.Id == 0)
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
            MukeshDb db = new MukeshDb();
            var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();

            return res;
        }
    }
}