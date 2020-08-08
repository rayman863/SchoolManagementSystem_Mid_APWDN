using Mid_SchoolManagementSystem.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;

namespace Mid_SchoolManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: admin
        smsEntities data = new smsEntities();
        [HttpGet]
        public ActionResult ListSection()
        {
            return View(data.section);
        }

        //Create Section
        [HttpGet]
        public ActionResult CreateSection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSection([Bind(Exclude = "id")] section section)
        {


            if (ModelState.IsValid)
            {
                data.section.Add(section);
                data.SaveChanges();
                return RedirectToAction("ListSection");
            }
            return View(section);

        }

        //Edit Section
        [HttpGet]
        public ActionResult EditSection(int id)
        {
            section s = data.section.Where(a => a.sectionid == id).FirstOrDefault();
            s.sectionid = id;
            return View(s);
        }

        [HttpPost]
        public ActionResult EditSection(section s, int id)
        {
            section sectionUpdate = data.section.Where(a => a.sectionid == id).FirstOrDefault();
            sectionUpdate.sectionid = id;
            sectionUpdate.sectionname = s.sectionname;
            sectionUpdate.classid = s.classid;
            data.SaveChanges();
            return RedirectToAction("ListSection");
        }

        //Delete Section
        [HttpGet]
        public ActionResult DeleteSection(int id)
        {
            section s = data.section.Where(a => a.sectionid == id).FirstOrDefault();
            return View(s);
        }

        [HttpPost, ActionName("DeleteSection")]
        public ActionResult ConfirmDelete(int id)
        {
            section s = data.section.Where(a => a.sectionid == id).FirstOrDefault();
            data.section.Remove(s);
            data.SaveChanges();

            return RedirectToAction("ListSection");
        }
        //Subject create,delete,update,list

        public ActionResult ListSubject()
        {
            return View(data.subject);
        }

        //Create Subject
        [HttpGet]
        public ActionResult CreateSubject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSubject([Bind(Exclude = "id")] subject subject)
        {


            if (ModelState.IsValid)
            {
                data.subject.Add(subject);
                data.SaveChanges();
                return RedirectToAction("ListSubject");
            }
            return View(subject);

        }

        //Edit Subject
        [HttpGet]
        public ActionResult EditSubject(int id)
        {
            subject s = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
            s.subjectid = id;
            return View(s);
        }

        [HttpPost]
        public ActionResult EditSubject(subject s, int id)
        {
            subject subjectUpdate = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
            subjectUpdate.subjectid = id;
            subjectUpdate.subjectname = s.subjectname;
            subjectUpdate.classid = s.classid;
            data.SaveChanges();
            return RedirectToAction("ListSubject");
        }

        //Delete Subject
        [HttpGet]
        public ActionResult DeleteSubject(int id)
        {
            subject s = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
            return View(s);
        }

        [HttpPost, ActionName("DeleteSubject")]
        public ActionResult ConfirmDeleteSubject(int id)
        {
            subject s = data.subject.Where(a => a.subjectid == id).FirstOrDefault();
            data.subject.Remove(s);
            data.SaveChanges();

            return RedirectToAction("ListSubject");
        }//subject done

        //Create Teacher
        
        
        [HttpGet]
        public ActionResult ListTeacher()
        {
            return View(data.teacher);
        }


        //Create Teacher
        [HttpGet]
        public ActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeacher([Bind(Exclude = "id")] teacher teacher)
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
                Debug.WriteLine(teacher.teacherid);
                Debug.WriteLine(teacher.teachername);
                Debug.WriteLine(teacher.teacheremail);
                Debug.WriteLine(teacher.teacherpassword);
                Debug.WriteLine(teacher.teacherconfirmpassword);
                Debug.WriteLine(teacher.teacherbloodgroup);
                Debug.WriteLine(teacher.teachersalary);
                Debug.WriteLine(teacher.teacherphone);

                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(teacher);
        }

        ////Edit Teacher
        //[HttpGet]
        //public ActionResult EditTeacher(int id)
        //{
        //    teacher t = data.teacher.Where(a => a.teacherid == id).FirstOrDefault();
        //    t.teacherid = id;
        //    return View(t);
        //}


        //create,list student
        [HttpGet]

        public ActionResult ListStudent()
        {
            return View(data.student);
        }
        //get student create view
        [HttpGet]
        public ActionResult CreateStudent()
        {
            return View();
        }
        //student info post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent([Bind(Exclude = "id")] student student)
        {
            bool Status = false;
            string message = "";

            student.studentid = generateStudentID();//"20-0005-04";
            if (ModelState.IsValid)
            {
                var nameExistsStudent = NameExistsStudent(student.studentname);
                if (nameExistsStudent)
                {
                    ModelState.AddModelError("NameExistSuper", "Super Admin name already exists");
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