using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcCrudJson.Models;

namespace MvcCrudJson.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            PersonDBHandler PersonHandler = new PersonDBHandler();
            return View(PersonHandler.GetPersonDetails());
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler();
            return View(PersonHandler.GetPersonDetails().Find(personmodel => personmodel.ID == id));
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(PersonModel iList)
        {
            try
            {
                PersonDBHandler PersonHandler = new PersonDBHandler();
                if (PersonHandler.InsertPerson(iList))
                {
                    ModelState.Clear();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler();
            return View(PersonHandler.GetPersonDetails().Find(personmodel => personmodel.ID == id));
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PersonModel iList)
        {
            try
            {
                PersonDBHandler PersonHandler = new PersonDBHandler();
                PersonHandler.UpdatePerson(iList);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            PersonDBHandler PersonHandler = new PersonDBHandler();
            return View(PersonHandler.GetPersonDetails().Find(personmodel => personmodel.ID == id));
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PersonDBHandler PersonHandler = new PersonDBHandler();
                if (PersonHandler.DeletePerson(id))
                {
                    ModelState.Clear();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
