using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeForTest.DbCtx;
using PracticeForTest.Models;
using System.Data.Entity;
using System.Net.Http;
using Newtonsoft.Json;

namespace PracticeForTest.Controllers
{
    [Authorize]
    public class NameController : Controller
    {
        // Instantiate MukeshDb with a specific initial catalog
        MukeshDb db = new MukeshDb();

        HttpClient clt = new HttpClient();
        string baseUrl = "http://localhost:59813/api/";
        // GET: Name
        /*public ActionResult HomePage()
        {
            var res = db.Friends.ToList();
            List<NameModel> list = new List<NameModel>();
            foreach (var item in res)
            {
                list.Add(new NameModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                });
            }
            return View(list);
        }

        public ActionResult Delete(int id)
        {
            var res = db.Friends.Where(f => f.Id == id).FirstOrDefault();
            if (res != null)
            {
                db.Friends.Remove(res);
                db.SaveChanges();
            }
            return RedirectToAction("HomePage");
        }

        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(NameModel m)
        {
            ViewBag.Name = "e";
            Friend f = new Friend();
            f.Name = m.Name;
            f.Id = m.Id;
            if(m.Id == 0)
            {
                db.Friends.Add(f);
                db.SaveChanges();
            }
            else
            {
                db.Entry(f).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("HomePage");
        }

        public ActionResult Edit(int id)
        {
            var res = db.Friends.Where(m => m.Id == id).FirstOrDefault();
            NameModel name = new NameModel();
            if(res != null)
            {
                name.Name = res.Name;  
                name.Id = res.Id;
            }
            return View("AddOrEdit",name);
        }*/

        /*-----------------------webapi----------------------------*/
        public ActionResult HomePage()
        {
            var get = clt.GetAsync(baseUrl + "getData").Result;

            var read = get.Content.ReadAsStringAsync().Result;

            var list = JsonConvert.DeserializeObject<List<NameModel>>(read);

            return View(list);
        }

        public ActionResult Delete(int id)
        {
            var get = clt.DeleteAsync(baseUrl + "delete?id=" + id).Result;
            return RedirectToAction("HomePage");
        }

        public ActionResult AddOrEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(NameModel m)
        {
            ViewBag.Name = "e";
            var get = JsonConvert.SerializeObject(m);
            StringContent sc = new StringContent(get,System.Text.Encoding.UTF8,"application/json");
            var data = clt.PostAsync(baseUrl + "addOrEdit", sc);
            return RedirectToAction("HomePage");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = clt.GetAsync(baseUrl + "getUserById?id=" + id).Result;
            var read = data.Content.ReadAsStringAsync().Result;
            var put = JsonConvert.DeserializeObject<NameModel>(read);
            return View("AddOrEdit", put);
        }
    }
}