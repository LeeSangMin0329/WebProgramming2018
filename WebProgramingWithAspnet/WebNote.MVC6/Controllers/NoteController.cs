using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebNote.MVC6.DataContext;
using WebNote.MVC6.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebNote.MVC6.Controllers
{
    public class NoteController : Controller
    {
        // GET: /<controller>/

        /// <summary>
        /// Note list
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // not in login
                return RedirectToAction("Login", "Account");
            }
            using (var db = new WebNoteDbContext())
            {
                var list = db.Notes.ToList();
                return View(list);
            }
        }


        /// <summary>
        /// note contents open
        /// </summary>
        /// <param name="noteNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // not in login
                return RedirectToAction("Login", "Account");
            }

            using (var db = new WebNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.No.Equals(noteNo));
                return View(note);
            }
        }
        
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // not in login
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // not in login
                return RedirectToAction("Login", "Account");
            }
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                using (var db = new WebNoteDbContext())
                {
                    db.Notes.Add(model);

                    if(db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Can't save note.");
            }
            return View(model);
        }


        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // not in login
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // not in login
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        
    }
}
