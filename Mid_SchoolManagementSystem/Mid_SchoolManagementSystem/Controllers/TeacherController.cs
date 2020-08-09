using Mid_SchoolManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_SchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        smsEntities data = new smsEntities();

        //Teacher landing page or INDEX
        [HttpGet]
        public ActionResult TeacherIndex()
        {
            if ((string)Session["user"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }


        //UploadFile get       
        [HttpGet]
        public ActionResult UploadFile()
        {
            if ((string)Session["user"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }



        //Upload File post
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, uploadnote uploadnote, @class classid, section section, teacher teacher)
        {
            if ((string)Session["user"] != null)
            {
                smsEntities data = new smsEntities();
                try
                {
                    if (file.ContentLength > 0)
                    {

                        string FileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), FileName);
                        file.SaveAs(path);

                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    return View();
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }
            }
            return RedirectToAction("Login", "User");
        }


        ////UploadFile get       
        //[HttpGet]

        //public ActionResult UploadFile()
        //{
        //    using (smsEntities data = new smsEntities())
        //    {

        //        string id = (string)Session["user"];

        //        teacher teacher = data.teacher.Where(x => x.teacherid == id).FirstOrDefault();

        //        var sec = new SelectList(data.section.ToList(), "sectionid", "sectionname");
        //        ViewData["sectionlist"] = sec;

        //        var sub = new SelectList(data.subject.ToList(), "subjectid", "subjectname");
        //        ViewData["sublistlist"] = sub;
        //    }
        //    return View();
        //}
        ////Upload File post
        //[HttpPost]
        //public ActionResult UploadFile(HttpPostedFileBase file, uploadnote uploadnote)
        //{
        //    smsEntities data = new smsEntities();
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string FileName = Path.GetFileName(file.FileName);
        //            string path = Path.Combine(Server.MapPath("~/UploadedFiles"), FileName);
        //            file.SaveAs(path);


        //            data.uploadnote.Add(uploadnote);
        //            data.SaveChanges();

        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";
        //        return View();
        //    }
        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return View();
        //    }
        //}



        //Edit Teacher GET
        [HttpGet]
        public ActionResult EditTeacherProfile()
        {
            string teacherid = (string)Session["user"];

            teacher t = data.teacher.Where(x => x.teacherid == teacherid).FirstOrDefault();

            t.teacherid = teacherid;
            teacher[] teacher = data.teacher.ToArray();
            ViewData["teacher"] = teacher;

            var fromDatabaseEF = new SelectList(data.@class.ToList(), "classid", "classname");
            var sec = new SelectList(data.section.ToList(), "sectionid", "sectionname");
            var sub = new SelectList(data.subject.ToList(), "subjectid", "subjectname");
            ViewData["classlist"] = fromDatabaseEF;
            ViewData["sectionlist"] = sec;
            ViewData["subjectlist"] = sub;

            return View(t);
        }


        //Edit Teacher POST
        [HttpPost]
        public ActionResult EditTeacherProfile(teacher t, int id)
        {
            teacher teacher = new teacher();


            teacher teachertoupdate = data.teacher.Where(x => x.id == id).FirstOrDefault();
            //Debug.WriteLine(superadmintoupdate);
            //Debug.WriteLine(id);
            //Debug.WriteLine(s.superadminid);
            //Debug.WriteLine(s.superadminname);
            //Debug.WriteLine(s.superadminpassword);
            //Debug.WriteLine(s.superadminconfirmpassword);
            //superadmintoupdate.id = s.id;
            teachertoupdate.teacherid = t.teacherid;
            teachertoupdate.teachername = t.teachername;
            teachertoupdate.teacherphone = t.teacherphone;
            teachertoupdate.teacherbloodgroup = t.teacherbloodgroup;
            teachertoupdate.teacheremail = t.teacheremail;
            teachertoupdate.teacherpassword = Crypto.Hash(t.teacherpassword);
            teachertoupdate.teacherconfirmpassword = Crypto.Hash(t.teacherconfirmpassword);
            teachertoupdate.classid = t.classid;
            teachertoupdate.sectionid = t.sectionid;
            teachertoupdate.subjectid = t.subjectid;



            try
            {
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

            return RedirectToAction("TeacherIndex");

        }
        //CourseNotice GET       
        [HttpGet]

        public ActionResult CreateCourseNotice()
        {
            return View();
        }

    }

}


