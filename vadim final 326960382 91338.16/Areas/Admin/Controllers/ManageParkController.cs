using BL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;
using PagedList;
using DAL;
using vadim_final_326960382_91338._16.Security;

namespace vadim_final_326960382_91338._16.Areas.Admin.Controllers
{
    /// <summary>
    /// Cars conroller class
    /// </summary>
    /// 
    [RolesAuth(Roles = "Admin")]
    public class ManageParkController : Controller
    {
        

        const int RecordsPerPage = 5;
        /// <summary>
        /// search action 
        /// </summary>
        /// <param name="cars">search parametrs</param>
        /// <returns>view with result & paging </returns>
        public ActionResult InPark(SearchCarsModel cars)
        {
            using (TypeCarM car = new TypeCarM())
            {
                var results = car.GetItems(cars);
                var pageIndex = cars.Page ?? 1;
                cars.SearchResults = results.ToPagedList(pageIndex, RecordsPerPage);
                ViewBag.type = new SelectList(car.GetManufactures().Distinct());
                return View(cars);
            }
        }
        /// <summary>
        /// sendig car to branch form
        /// </summary>
        /// <param name="value">view with form</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Send(string value)
        {

            BranchM br = new BranchM();
            ViewBag.branches = new SelectList(br.GetNames());
            ViewBag.type = new SelectList(new List<string>() { value });
            return View();
        }
        /// <summary>
        /// sending car data to server
        /// </summary>
        /// <param name="car">car object</param>
        /// <param name="upload">car image</param>
        /// <returns>rederect to main page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send([Bind(Include = "CarModel, CarNumber, CurrentDistance, Branch, ProperForRent, FreeForRent")]Car car, HttpPostedFileBase upload)
        {
            try
            {
                using (CarM carM = new CarM())
                {
                    if (ModelState.IsValid)
                    {
                        if (upload != null && upload.ContentLength > 0)
                        {
                            carM.SaveWithImage(car, upload);
                        }
                        else
                            carM.Create(car);
                    }
                }

                return RedirectToAction("InPark");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// adding new car to park form
        /// </summary>
        /// <returns>car form</returns>
        public ActionResult NewType()
        {
            return View();
        }
        /// <summary>
        /// sending new car data to server 
        /// </summary>
        /// <param name="ct">car objet</param>
        /// <param name="f">form collection</param>
        /// <returns></returns>
        // POST: CarTypes/Create
        [HttpPost]
        public ActionResult NewType(CarType ct)
        {
            try
            {
                TempData["added"] = ct.Manufacturer;
                using (TypeCarM ctModel = new TypeCarM())
                {
                    ctModel.Create(ct);
                    return RedirectToAction("InPark");
                }
            }
            catch
            {
                ViewBag.error = "error was detected";
                return View();
            }
        }

    }
}