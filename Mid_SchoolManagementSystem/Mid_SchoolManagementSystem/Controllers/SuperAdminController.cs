using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mid_SchoolManagementSystem.Models.DataAccess;


namespace Mid_SchoolManagementSystem.Controllers
{
    public class SuperAdminController : Controller
    {
        smsEntities data = new smsEntities();

        public object superadmin { get; private set; }

        //SuperAdmin List GET
        [HttpGet]
        public ActionResult ListSuperAdmin()
        {
            return View(data.superadmin.ToList());
        }


        //SuperAdmin Create GET
        [HttpGet]
        public ActionResult CreateSuperAdmin()
        {
            return View();
        }

        //SuperAdmin Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSuperAdmin([Bind(Exclude = "id")] superadmin superadmin)
        {
            bool Status = false;
            string message = "";

            superadmin.superadminid = generateSuperID();//"20-0005-01";

            if (ModelState.IsValid)
            {
                var nameExistsSuper = NameExistsSuper(superadmin.superadminname);
                if (nameExistsSuper)
                {
                    ModelState.AddModelError("NameExistSuper", "Super Admin name already exists");
                    return View(superadmin);
                }

                superadmin.superadminpassword = Crypto.Hash(superadmin.superadminpassword);
                superadmin.superadminconfirmpassword = Crypto.Hash(superadmin.superadminconfirmpassword);

                using (smsEntities data = new smsEntities())
                {
                    data.superadmin.Add(superadmin);
                    data.SaveChanges();
                    message = " Super Admin Account " + superadmin.superadminname + " with ID = " + superadmin.superadminid + " has been created.";
                    Status = true;
                }
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(superadmin);
        }

        //Edit Super Admin GET
        [HttpGet]
        public ActionResult EditSuperAdmin(int id)
        {
            superadmin s = data.superadmin.Where(x => x.id == id).FirstOrDefault();

            s.id = id;
            superadmin[] superadmin = data.superadmin.ToArray();
            ViewData["superadmin"] = superadmin;
            return View(s);
        }


        //Edit Super Admin POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSuperAdmin(superadmin s, int id)
        {

            superadmin superadmintoupdate = data.superadmin.Where(x => x.id == id).FirstOrDefault();
            Debug.WriteLine(superadmintoupdate);
            Debug.WriteLine(id);
            Debug.WriteLine(s.superadminid);
            Debug.WriteLine(s.superadminname);
            Debug.WriteLine(s.superadminpassword);
            Debug.WriteLine(s.superadminconfirmpassword);
            //superadmintoupdate.id = s.id;
            superadmintoupdate.superadminid = s.superadminid;
            superadmintoupdate.superadminname = s.superadminname;
            superadmintoupdate.superadminpassword = Crypto.Hash(s.superadminpassword);
            superadmintoupdate.superadminconfirmpassword = Crypto.Hash(s.superadminconfirmpassword);

            try
            {
                data.Entry(superadmintoupdate).State = EntityState.Modified;
                data.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            return RedirectToAction("ListSuperAdmin");

        }



        //Create Admin GET
        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return View();
        }

        //Create Admin Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin([Bind(Exclude = "id")] admin admin)
        {
            bool Status = false;
            string message = "";

            admin.adminid = generateAdminID();//"20-0005-01";

            if (ModelState.IsValid)
            {
                var nameExistAdmin = NameExistsAdmin(admin.adminname);
                if (nameExistAdmin)
                {
                    ModelState.AddModelError("NameExistAdmin", "Admin name already exists");
                    return View(admin);
                }

                admin.adminpassword = Crypto.Hash(admin.adminpassword);
                admin.adminconfirmpassword = Crypto.Hash(admin.adminconfirmpassword);

                using (smsEntities data = new smsEntities())
                {
                    data.admin.Add(admin);
                    data.SaveChanges();
                    message = "Admin Account " + admin.adminname + " with ID = " + admin.adminid + " has been created.";
                    Status = true;
                }
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(admin);
        }


        [NonAction]
        public bool NameExistsSuper(string superadminname)
        {
            using (smsEntities data = new smsEntities())
            {
                var name = data.superadmin.Where(a => a.superadminname == superadminname).FirstOrDefault();
                return name != null;
            }
        }

        [NonAction]
        public bool NameExistsAdmin(string adminname)
        {
            using (smsEntities data = new smsEntities())
            {
                var name = data.admin.Where(a => a.adminname == adminname).FirstOrDefault();
                return name != null;
            }
        }

        [NonAction]
        public string generateSuperID()
        {
            using (smsEntities data = new smsEntities())
            {
                var oldID = (from superadmin in data.superadmin
                             orderby
                             superadmin.id descending
                             select superadmin.superadminid).Take(1).FirstOrDefault();//.ToString();
                //Debug.WriteLine(oldID);
                string toBreak = oldID.ToString();
                string[] idList = toBreak.Split('-');//20-0000-01
                //foreach (string id in idList)
                //{ Debug.WriteLine(id); }
                string id1 = idList[0];
                //Debug.WriteLine(id1);
                string id2 = idList[1];
                //Debug.WriteLine(id2);
                string id3 = idList[2];
                //Debug.WriteLine(id3);
                int idInc = Convert.ToInt32(id2);
                idInc = idInc + 1;
                id2 = idInc.ToString("D" + 4);
                string newID = id1 + "-" + id2 + "-" + id3;
                return newID;
            }
        }

        [NonAction]
        public string generateAdminID()
        {
            using (smsEntities data = new smsEntities())
            {
                var oldID = (from admin in data.admin
                             orderby
                             admin.id descending
                             select admin.adminid).Take(1).FirstOrDefault();//.ToString();
                //Debug.WriteLine(oldID);
                string toBreak = oldID.ToString();
                string[] idList = toBreak.Split('-');//20-0000-01
                //foreach (string id in idList)
                //{ Debug.WriteLine(id); }
                string id1 = idList[0];
                //Debug.WriteLine(id1);
                string id2 = idList[1];
                //Debug.WriteLine(id2);
                string id3 = idList[2];
                //Debug.WriteLine(id3);
                int idInc = Convert.ToInt32(id2);
                idInc = idInc + 1;
                id2 = idInc.ToString("D" + 4);
                string newID = id1 + "-" + id2 + "-" + id3;
                return newID;
            }
        }


    }
}