using DAL;
using System;
using BL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;
using System.Web.Security;
using vadim_final_326960382_91338._16.Binder;
using System.Data.Entity.Infrastructure;

namespace vadim_final_326960382_91338._16.Controllers
{
    /// <summary>
    /// rent controller class
    /// </summary>
    [Authorize]
    public class RentController : Controller
    {    

        /// <summary>
        /// order by id & branch name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="branch"></param>
        /// <returns>order view</returns>
        // GET: Rent
        [HttpGet]

        public ActionResult Order(int id, string branch)
        {
            using (TypeCarM tCar = new TypeCarM())
            {
                OrderRentModel order = new OrderRentModel();
                order.cars = tCar.GetById(id).Cars.Where(b => b.Branch == branch).ToList();
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string userName = ticket.Name;
                UserM user = new UserM();
                order.user = user.SearchFor(u => u.FullName == userName).FirstOrDefault();
                return View(order);
            }
        }

        /// <summary>
        /// order by id & branch name othet method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="branch"></param>
        /// <returns>order view</returns>
        [HttpGet]

        public ActionResult OrderByCar(int id, string branch)
        {
            using (CarM Car = new CarM())
            {
                OrderRentModel order = new OrderRentModel();
                order.cars = new List<DAL.Car>();
                order.cars.Add(Car.SearchFor(b => b.id == id).FirstOrDefault());
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string userName = ticket.Name;
                UserM user = new UserM();
                order.user = user.SearchFor(u => u.FullName == userName).FirstOrDefault();
                return View("~/Views/Rent/Order.cshtml", order);
            }
        }
        /// <summary>
        /// send order data to server 
        /// </summary>
        /// <param name="obj">order data</param>
        /// <returns>success page</returns>
        [HttpPost]
        public ActionResult Order([ModelBinder(typeof(OrderRentModelBinder))] OrderRentModel obj)
        {

            try
            {
                using (RentCar rent = new RentCar())
                {
                    rent.Create(obj.order);

                }
                using (CarM car = new CarM())
                {
                    Car updated = car.SearchFor(c => c.CarNumber == obj.order.NumCar).FirstOrDefault();
                    updated.FreeForRent = false;
                    car.Update(updated);
                    return RedirectToAction("Success", "Rent");
                }
            }
            catch (DbUpdateException e)
            {
                foreach (var eve in e.Entries)
                {
                    Console.WriteLine("Entity of type \"{0}\" has the following validation errors:",
                        eve.Entity);
                }
                throw;
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        /// <summary>
        /// success page
        /// </summary>
        /// <returns></returns>
        public ActionResult Success()
        {

            return View();
        }

        /// <summary>
        /// rents list 
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            using (RentCar rent = new RentCar())
            {

                return View(rent.GetList(User.Identity.Name).ToList());
            }
        }
    }

}