using Mid_SchoolManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mid_SchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
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
            return View();
        }
        //Upload File post
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            smsEntities data = new smsEntities();
            try
            {
                if (file.ContentLength > 0)
                {
                    string FileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), FileName);
                    file.SaveAs(path);
                    //uploadnote.filename = FileName;
                    //uploadnote.directory = path;
                    //uploadnote.sectionid = section.sectionid;


                    //data.SaveChanges();
                    //message = " Student Account " + student.studentname + " with ID = " + student.studentid + " has been created.";
                    //Status = true;

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

    }

}