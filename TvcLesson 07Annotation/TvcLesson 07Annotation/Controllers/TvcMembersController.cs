using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TvcLesson_07Annotation.Models;

namespace TvcLesson_07Annotation.Controllers
{
    public class TvcMembersController : Controller
    {
        private static List<TvcMember> tvcMembers = new List<TvcMember>();
        // GET: TvcMembersController
        public ActionResult Index()
        {
            return View(tvcMembers);
        }

        // GET: TvcMembersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TvcMembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TvcMembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TvcMember tvcMember)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    return View(tvcMember);
                }
                tvcMember.Id = tvcMembers.Id;
                tvcMembers.Add(tvcMember);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TvcMembersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TvcMembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TvcMembersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TvcMembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
