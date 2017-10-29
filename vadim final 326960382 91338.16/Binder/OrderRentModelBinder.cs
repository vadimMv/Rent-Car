using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using vadim_final_326960382_91338._16.Models;
using BL;

namespace vadim_final_326960382_91338._16.Binder
{
    /// <summary>
    /// bindig models 
    /// </summary>
    public class OrderRentModelBinder : IModelBinder
    {
        /// <summary>
        /// Implementention IModelBinder interface
        /// combine 2 models 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns>OrderRentModel</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpContextBase objContext = controllerContext.HttpContext;
            //getting fields from request
            string userId = objContext.Request.Form["user"];
            string carId = objContext.Request.Form["car"];
            string start = objContext.Request.Form["start"];
            string finish = objContext.Request.Form["finish"];
            // creating models accordingly request
            UserM um = new UserM();
            CarM cm = new CarM();
            var UID = int.Parse(userId);
            var CID = int.Parse(carId);
            Car auto = cm.SearchFor(c => c.CarNumber == CID).FirstOrDefault();
            User user=um.SearchFor(u => u.IdNumber == UID).FirstOrDefault();

            CarsRent rent = new CarsRent();
            rent.NumCar = int.Parse(carId);
            rent.NumUser = int.Parse(userId);
            rent.StartRent= DateTime.Parse(start);
            rent.FinishRent = DateTime.Parse(finish);
            rent.RealFinishRent = DateTime.Parse(finish);
            //rent.User = user;
            //rent.Car = auto;
            //returned model
            OrderRentModel rentModel = new OrderRentModel() {
                cars = new List<Car>()
            };

            rentModel.cars.Add(auto);
            rentModel.user = user;
            rentModel.order = rent;
            return rentModel;


        }
    }
}