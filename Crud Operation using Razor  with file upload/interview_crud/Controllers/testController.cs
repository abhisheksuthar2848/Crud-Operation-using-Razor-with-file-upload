using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using interview_crud.EDM;

namespace interview_crud.Controllers
{
    public class testController : Controller
    {
        db_mvc2Entities dc = new db_mvc2Entities();
        public void GetCountry()
        {
            var p = dc.tblCountries.ToList();
            List<SelectListItem> objlist = new List<SelectListItem>();
            foreach (var item in p)
            {
                SelectListItem obj = new SelectListItem();
                obj.Value = item.Country_id.ToString();
                obj.Text = item.Country_name;

                objlist.Add(obj);

            }

            ViewData["country"] = objlist;
        }

        public void GetState()
        {
            var p = dc.tblStates.ToList();
            List<SelectListItem> objlist = new List<SelectListItem>();
            foreach (var item in p)
            {
                SelectListItem obj = new SelectListItem();
                obj.Value = item.State_id.ToString();
                obj.Text = item.State_name;

                objlist.Add(obj);

            }

            ViewData["state"] = objlist;
        }

        public void GetCity()
        {
            var p = dc.tblCities.ToList();
            List<SelectListItem> objlist = new List<SelectListItem>();
            foreach (var item in p)
            {
                SelectListItem obj = new SelectListItem();
                obj.Value = item.City_id.ToString();
                obj.Text = item.City_name;

                objlist.Add(obj);

            }

            ViewData["city"] = objlist;
        }

        public void GetCountry(int id)
        {
            var p = dc.tblCountries.ToList();
            List<SelectListItem> objlist = new List<SelectListItem>();
            foreach (var item in p)
            {
                if (item.Country_id==id)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = item.Country_id.ToString();
                    obj.Text = item.Country_name;
                    obj.Selected = true;

                    objlist.Add(obj);

                }
                else
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = item.Country_id.ToString();
                    obj.Text = item.Country_name;
                    obj.Selected = false;

                    objlist.Add(obj);

                }

            }

            ViewData["country"] = objlist;
        }

       public void GetState(int id)
        {
            var p = dc.tblStates.ToList();
            List<SelectListItem> objlist = new List<SelectListItem>();
            foreach (var item in p)
            {
                if (item.State_id== id)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = item.State_id.ToString();
                    obj.Text = item.State_name;
                    obj.Selected = true;

                    objlist.Add(obj);

                }
                else
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = item.State_id.ToString();
                    obj.Text = item.State_name;
                    obj.Selected = false;

                    objlist.Add(obj);

                }

            }

            ViewData["state"] = objlist;
        }
        public void GetCity(int id)
        {
            var p = dc.tblCities.ToList();
            List<SelectListItem> objlist = new List<SelectListItem>();
            foreach (var item in p)
            {
                if (item.City_id == id)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = item.City_id.ToString();
                    obj.Text = item.City_name;
                    obj.Selected = true;

                    objlist.Add(obj);

                }
                else
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Value = item.City_id.ToString();
                    obj.Text = item.City_name;
                    obj.Selected = false;

                    objlist.Add(obj);

                }

            }

            ViewData["city"] = objlist;
        }

        public ActionResult create()
        {
            GetCountry();
            GetCity();
            GetState();
           
            return View();
        }

        [HttpPost]
        public ActionResult create(tblemployee obj, FormCollection fc,HttpPostedFileBase file)
        {
            var allowextenction = new[] {".jpg",".png",".jpeg" };

            if (file!=null)
            {
                var ext = Path.GetExtension(file.FileName);
                if(allowextenction.Contains(ext))
                {
                    var filename = file.FileName;
                    var path = Path.Combine(Server.MapPath("~/image"),filename);

                    obj.emp_profile = filename;
                    file.SaveAs(path);

                    var Reading = Convert.ToBoolean(fc["Reading"].Split(',')[0]);
                    var Writing = Convert.ToBoolean(fc["Writing"].Split(',')[0]);
                    var Playing = Convert.ToBoolean(fc["Playing"].Split(',')[0]);
                    var strhob = "";
                    if (Reading == true)
                    {
                        strhob += "Reading" + ",";
                    }
                    if (Writing == true)
                    {
                        strhob += "Writing" + ",";
                    }
                    if (Playing == true)
                    {
                        strhob += "Playing" + ",";
                    }
                    obj.emp_hob = strhob;

                    GetCountry();
                    GetCity();
                    GetState();

                    dc.tblemployees.Add(obj);
                    dc.SaveChanges();
                    ModelState.Clear();
                    ViewBag.msg = "insert sucessfully";
                }
            }
            else
            {
                ViewBag.msg = "please select valid image ";
            }

            
            return View();
        }

        public ActionResult display()
        {
            var p = dc.tblemployees.ToList();
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            var p = dc.tblemployees.Find(id);
            dc.tblemployees.Remove(p);
            dc.SaveChanges();

            return RedirectToAction("display");
        }

        public List<checkmodel> checklist()
        {
            List<checkmodel> chklist = new List<checkmodel>(){
            new checkmodel{ id=1,name="Reading",ischecked=false},
            new checkmodel{ id=2,name="Writing",ischecked=false},
            new checkmodel{ id=3,name="Playing",ischecked=false}
            };
            return chklist;
        }

        public List<checkmodel> setchecklist( string strhob)
        {
            var arr = strhob.Split(',');
            var ss = checklist();
            foreach (var item in ss)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (item.name==arr[i])
                    {
                        item.ischecked = true;
                    }
                }
            }
            return ss;
        }
        [HttpGet]
        public ActionResult edit(int id)
        {
            var p = dc.tblemployees.Find(id);


            GetCountry(int.Parse(p.country_id.ToString()));
            GetState(int.Parse(p.state_id.ToString()));
            GetCity(int.Parse(p.city_id.ToString()));

           p.checklist= setchecklist(p.emp_hob);
                                   
            return View(p);
        }
        [HttpPost]
        public ActionResult edit(tblemployee obj)
        {
            var checklist = obj.checklist.Where(c => c.ischecked == true);
            var strhob = "";
            foreach (var item in checklist)
            {
                if (item.id==1)
                {
                    strhob += "Reading" + ",";
                }
                if (item.id==2)
                {
                    strhob += "Writing" + ",";
                }
                if (item.id==3)
                {
                    strhob += "Playing" + ",";
                }
            }
            obj.emp_hob = strhob;

            dc.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("display");
        }

    }
}