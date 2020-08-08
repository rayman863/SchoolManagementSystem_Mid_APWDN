using Mid_SchoolManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Data.Entity.Validation;
using Mid_SchoolManagementSystem.Models.ViewModel;

namespace Mid_SchoolManagementSystem.Controllers
{
    public class AdminController : Controller
    {

        //Super landing page or INDEX
        [HttpGet]
        public ActionResult AdminIndex()
        {
            if ((string)Session["user"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }

        // GET sectionlist
        smsEntities data = new smsEntities();
        [HttpGet]
        public ActionResult ListSection()
        {
            if ((string)Session["user"] != null)
            {
                return View(data.section);
            }
            return RedirectToAction("Login", "User");
        }

        //Create Section get
        [HttpGet]
        public ActionResult CreateSection()
        {
            if ((string)Session["user"] != null)
            {
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        //create section post
        [HttpPost]
        public ActionResult CreateSection([Bind(Exclude = "id")] section section)
        {
            if ((string)Session["user"] != null)
            {
                if (ModelState.IsValid)
                {
                    data.section.Add(section);
                    data.SaveChanges();
                    return RedirectToAction("ListSection");
                }
                return View(section);
            }
            return RedirectToAction("Login", "User");
        }

        //Edit Section get
        [HttpGet]
        public ActionResult EditSection(int id)
        {
            if ((string)Session["user"] != null)
            {
                section s = data.section.Where(a => a.sectionid == id).FirstOrDefault();
                s.sectionid = id;
                return View(s);
            }
            return RedirectToAction("Login", "User");
        }
        //edit section post
        [HttpPost]
        public ActionResult EditSection(section s, int id)
        {
            if ((string)Session["user"] != null)
            {
                section sectionUpdate = data.section.Where(a => a.sectionid == id).FirstOrDefault();
                sectionUpdate.sectionid = id;
                sectionUpdate.sectionname = s.sectionname;
                sectionUpdate.classid = s.classid;
                data.SaveChanges();
                return RedirectToAction("ListSection");
            }
            return RedirectToAction("Login", "User");
        }

        //Delete Section get
        [HttpGet]
        public ActionResult DeleteSection(int id)
        {
            if ((string)Session["user"] != null)
            {
                section s = data.section.Where(a => a.sectionid == id).FirstOrDefault();
                return View(s);
            }
            return RedirectToAction("Login", "User");
        }
        //delete section post
        [HttpPost, ActionName("DeleteSection")]
        public ActionResult ConfirmDelete(int id)
        {
            if ((string)Session["user"] != null)
            {
                section s = data.section.Where(a => a.sectionid == id).FirstOrDefault();
                data.section.Remove(s);
                data.SaveChanges();

                return RedirectToAction("ListSection");
            }
            return RedirectToAction("Login", "User");
        }
        //List Subject GET
        [HttpGet]
        public ActionResult ListSubject()
        {
            if ((string)Session["user"] != null)
            {
                var subject_class = (from t1 in data.subject
                                     join t2 in data.@class
                                     on t1.classid equals t2.classid
                                     select new SubjectClassView { subject = t1, @class = t2 }
                                 ).ToList();

                return View(subject_class);
            }
            return RedirectToAction("Login", "User");
        }

        //Create Subject GET
        [HttpGet]
        public ActionResult CreateSubject()
        {
            if ((string)Session["user"] != null)
            {
                var classlist = new SelectList(data.@class.ToList(), "classid", "classname");
                ViewData["ClassList"] = classlist;

                return View();
            }
            return RedirectToAction("Login", "User");
        }


        //Create Subject POST
        [HttpPost]
        public ActionResult CreateSubject([Bind(Exclude = "id")] subject subject)
        {
            if ((string)Session["user"] != null)
            {
                if (ModelState.IsValid)
                {
                    data.subject.Add(subject);
                    data.SaveChanges();
                    return RedirectToAction("ListSubject");
                }
                return View(subject);
            }
            return RedirectToAction("Login", "User");
        }

        //Edit Subject GET
        [HttpGet]
        public ActionResult EditSubject(int id)
        {
            if ((string)Session["user"] != null)
            {
                subject s = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
                s.subjectid = id;
                return View(s);
            }
            return RedirectToAction("Login", "User");
        }


        //Edit Subject POST
        [HttpPost]
        public ActionResult EditSubject(subject s, int id)
        {
            if ((string)Session["user"] != null)
            {
                subject subjectUpdate = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
                subjectUpdate.subjectid = id;
                subjectUpdate.subjectname = s.subjectname;
                subjectUpdate.classid = s.classid;
                data.SaveChanges();
                return RedirectToAction("ListSubject");
            }
            return RedirectToAction("Login", "User");
        }

        //Delete Subject GET
        [HttpGet]
        public ActionResult DeleteSubject(int id)
        {
            if ((string)Session["user"] != null)
            {
                subject s = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
                return View(s);
            }
            return RedirectToAction("Login", "User");
        }


        //Delete Subject POST
        [HttpPost, ActionName("DeleteSubject")]
        public ActionResult ConfirmDeleteSubject(int id)
        {
            if ((string)Session["user"] != null)
            {
                subject s = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
                data.subject.Remove(s);
                data.SaveChanges();

                return RedirectToAction("ListSubject");
            }
            return RedirectToAction("Login", "User");
        }


        // List Teacher
        [HttpGet]
        public ActionResult ListTeacher()
        {
            if ((string)Session["user"] != null)
            {
                return View(data.teacher);
            }
            return RedirectToAction("Login", "User");
        }


        //Create Teacher
        [HttpGet]
        public ActionResult CreateTeacher()
        {
            if ((string)Session["user"] != null)
            {
                var fromDatabaseEF = new SelectList(data.@class.ToList(), "classid", "classname");
                var sec = new SelectList(data.section.ToList(), "sectionid", "sectionname");
                var sub = new SelectList(data.subject.ToList(), "subjectid", "subjectname");
                ViewData["classlist"] = fromDatabaseEF;
                ViewData["sectionlist"] = sec;
                ViewData["subjectlist"] = sub;

                return View();
            }
            return RedirectToAction("Login", "User");
        }
        // Create Teacher Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher([Bind(Exclude = "id")] teacher teacher)
        {
            if ((string)Session["user"] != null)
            {
                bool Status = false;
                string message = "";

                teacher.teacherid = generateTeacherID();//"20-0005-01";

                if (ModelState.IsValid)
                {
                    var nameExistTeacher = NameExistsTeacher(teacher.teachername);
                    if (nameExistTeacher)
                    {
                        ModelState.AddModelError("NameExistTeacher", "Teacher name already exists");
                        return View(teacher);
                    }

                    teacher.teacherpassword = Crypto.Hash(teacher.teacherpassword);
                    teacher.teacherconfirmpassword = Crypto.Hash(teacher.teacherconfirmpassword);


                    data.teacher.Add(teacher);
                    data.SaveChanges();
                    message = "Teacher Account " + teacher.teachername + " with ID = " + teacher.teacherid + " has been created.";
                    Status = true;

                }
                else
                {
                    message = "Invalid Request";
                }

                ViewBag.Message = message;
                ViewBag.Status = Status;
                return View(teacher);
            }
            return RedirectToAction("Login", "User");
        }
        //Edit Teacher GET
        [HttpGet]
        public ActionResult EditTeacher(int id)
        {
            if ((string)Session["user"] != null)
            {
                teacher t = data.teacher.Where(x => x.id == id).FirstOrDefault();

                t.id = id;
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
            return RedirectToAction("Login", "User");
        }


        //Edit Teacher POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTeacher(teacher t, int id)
        {
            if ((string)Session["user"] != null)
            {
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
                teachertoupdate.teacherpassword = Crypto.Hash(t.teacherpassword);
                teachertoupdate.teacherconfirmpassword = Crypto.Hash(t.teacherconfirmpassword);

                try
                {
                    data.Entry(teachertoupdate).State = EntityState.Modified;
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

                return RedirectToAction("ListTeacher");
            }
            return RedirectToAction("Login", "User");
        }


        //Delete Teacher
        [HttpGet]
        public ActionResult DeleteTeacher(int id)
        {
            if ((string)Session["user"] != null)
            {
                teacher t = data.teacher.Where(a => a.id == id).FirstOrDefault();
                return View(t);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost, ActionName("DeleteTeacher")]
        public ActionResult ConfirmDeleteTeacher(int id)
        {
            if ((string)Session["user"] != null)
            {
                teacher t = data.teacher.Where(a => a.id == id).FirstOrDefault();
                data.teacher.Remove(t);
                data.SaveChanges();

                return RedirectToAction("ListTeacher");
            }
            return RedirectToAction("Login", "User");
        }



        //Student List GET
        [HttpGet]

        public ActionResult ListStudent()
        {
            if ((string)Session["user"] != null)
            {
                return View(data.student);
            }
            return RedirectToAction("Login", "User");
        }
        //GET student create 
        [HttpGet]
        public ActionResult CreateStudent()
        {
            if ((string)Session["user"] != null)
            {
                var fromDatabaseEF = new SelectList(data.@class.ToArray(), "classid", "classname");
                var sec = new SelectList(data.section.ToList(), "sectionid", "sectionname");
                ViewData["classlist"] = fromDatabaseEF;
                ViewData["sectionlist"] = sec;
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        //student Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent([Bind(Exclude = "id")] student student)
        {
            if ((string)Session["user"] != null)
            {
                bool Status = false;
                string message = "";

                student.studentid = generateStudentID();//"20-0005-04";
                if (ModelState.IsValid)
                {
                    var nameExistsStudent = NameExistsStudent(student.studentname);
                    if (nameExistsStudent)
                    {
                        ModelState.AddModelError("NameExistStudent", "Student name already exists");
                        return View(student);
                    }
                    student.studentpassword = Crypto.Hash(student.studentpassword);
                    student.studentconfirmpassword = Crypto.Hash(student.studentconfirmpassword);

                    using (smsEntities data = new smsEntities())
                    {
                        data.student.Add(student);
                        data.SaveChanges();
                        message = " Student Account " + student.studentname + " with ID = " + student.studentid + " has been created.";
                        Status = true;
                    }
                }
                else
                {
                    message = "Invalid Request";
                }
                ViewBag.Message = message;
                ViewBag.Status = Status;
                return View(student);
            }
            return RedirectToAction("Login", "User");
        }

        //Edit Student GET
        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            if ((string)Session["user"] != null)
            {
                student s = data.student.Where(x => x.id == id).FirstOrDefault();
                Debug.WriteLine(s);
                s.id = id;
                student[] student = data.student.ToArray();
                ViewData["student"] = student;
                var fromDatabaseEF = new SelectList(data.@class.ToList(), "classid", "classname");
                var sec = new SelectList(data.section.ToList(), "sectionid", "sectionname");
                ViewData["classlist"] = fromDatabaseEF;
                ViewData["sectionlist"] = sec;
                return View(s);
            }
            return RedirectToAction("Login", "User");
        }


        //Edit Student POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(student s, int id)
        {
            if ((string)Session["user"] != null)
            {
                student studenttoupdate = data.student.Where(x => x.id == id).FirstOrDefault();
                // Debug.WriteLine(studenttoupdate);
                //  Debug.WriteLine(id);
                // Debug.WriteLine(s.studentid);
                // Debug.WriteLine(s.studentname);
                // Debug.WriteLine(s.studentpassword);
                // Debug.WriteLine(s.studentconfirmpassword);

                studenttoupdate.studentid = s.studentid;
                studenttoupdate.studentname = s.studentname;
                studenttoupdate.studentpassword = Crypto.Hash(s.studentpassword);
                studenttoupdate.studentconfirmpassword = Crypto.Hash(s.studentconfirmpassword);
                studenttoupdate.studentdob = s.studentdob;
                studenttoupdate.studentphone = s.studentphone;
                studenttoupdate.studentaddress = s.studentaddress;
                studenttoupdate.studentemail = s.studentemail;
                studenttoupdate.studentbloodgroup = s.studentbloodgroup;
                studenttoupdate.studentfees = s.studentfees;
                studenttoupdate.classid = s.classid;
                studenttoupdate.sectionid = s.sectionid;

                try
                {
                    data.Entry(studenttoupdate).State = EntityState.Modified;
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

                return RedirectToAction("ListStudent");
            }
            return RedirectToAction("Login", "User");
        }

        //Delete Student Get
        [HttpGet]
        public ActionResult DeleteStudent(int id)
        {
            if ((string)Session["user"] != null)
            {
                student t = data.student.Where(a => a.id == id).FirstOrDefault();
                return View(t);
            }
            return RedirectToAction("Login", "User");
        }

        //Delete Student POST
        [HttpPost, ActionName("DeleteStudent")]
        public ActionResult ConfirmDeleteStudent(int id)
        {
            if ((string)Session["user"] != null)
            {
                student t = data.student.Where(a => a.id == id).FirstOrDefault();
                data.student.Remove(t);
                data.SaveChanges();

                return RedirectToAction("ListStudent");
            }
            return RedirectToAction("Login", "User");
        }

        //nonaction teacher start
        [NonAction]
        public bool NameExistsTeacher(string tname)
        {

            var name = data.teacher.Where(a => a.teachername == tname).FirstOrDefault();
            return name != null;

        }
        
        [NonAction]
        public string generateTeacherID()
        {

            var oldID = (from teacher in data.teacher
                         orderby
                         teacher.id descending
                         select teacher.teacherid).Take(1).FirstOrDefault();//.ToString();
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

        }//nonaction teacher END

        //nonaction student start

        [NonAction]
        public bool NameExistsStudent(string studentname)
        {
            using (smsEntities data = new smsEntities())
            {
                var name = data.student.Where(s => s.studentname == studentname).FirstOrDefault();
                return name != null;
            }
        }
        [NonAction]
        public string generateStudentID()
        {
            using (smsEntities data = new smsEntities())
            {
                var oldstID = (from student in data.student
                             orderby
                             student.id descending
                             select student.studentid).Take(1).FirstOrDefault();//.ToString();
                //Debug.WriteLine(oldstID);
                string toBreak = oldstID.ToString();
                string[] idList = toBreak.Split('-');//20-0000-04
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
        }//non action student
    }
}