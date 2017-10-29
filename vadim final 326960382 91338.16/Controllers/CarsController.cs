using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;
using PagedList;
using BL;
using System.Web.Script.Serialization;
using DAL;


namespace vadim_final_326960382_91338._16.Controllers
{   
    /// <summary>
    /// car controller 
    /// </summary>
    public class CarsController : Controller
    {
        const int RecordsPerPage = 5;
        /// <summary>
        /// car list 
        /// </summary>
        /// <param name="cars">serch model obj</param>
        /// <returns>list with finded cars</returns>
        public ActionResult List(SearchCarsModel cars)
        {

            using (TypeCarM car = new TypeCarM())
            {
                var results = car.GetItems(cars);
                var pageIndex = cars.Page ?? 1;
                cars.SearchResults = results.ToPagedList(pageIndex, RecordsPerPage);
                //     ViewBag.InPark = results.ToList();
                ViewBag.type = new SelectList(car.GetManufactures().Distinct());
            //    ViewBag.model = new SelectList(car.GetModelNames());
                return View(cars);
            }
        }
        /// <summary>
        /// cars details
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>details form</returns>
        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        /// <summary>
        /// adding new car &sending to server car data
        /// </summary>
        /// <param name="car">car object </param>
        /// <param name="upload">car image</param>
        /// <returns>index page</returns>
        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarModel, CarNumber, CurrentDistance, Branch, ProperForRent, FreeForRent")]Car car, HttpPostedFileBase upload)
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
         /// <summary>
         /// edit car form
         /// </summary>
         /// <param name="id">id</param>
         /// <returns>car form with data</returns>
        // GET: Cars/Edit/5
        public ActionResult Edit(int id)
        {
            using (CarM car = new CarM())
            {
                Car finded = car.GetById(id);
                return View(finded);
            }
        }

        // POST: Cars/Edit/5
        /// <summary>
        /// send edited form to server
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="car">car model</param>
        /// <returns>index page</returns>
        [HttpPost]
        public ActionResult Edit(int id, Car car)
        {
            try
            {
                using (CarM model = new CarM())
                {
                    if (ModelState.IsValid)
                    {
                        if (TryUpdateModel(car))
                            model.Update(car);
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }
        /// <summary>
        /// delete car 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>index page</returns>
        // GET: Cars/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (CarM car = new CarM())
                {
                    Car finded = car.GetById(id);
                    car.Delete(finded);
                }
                ViewBag.Message = "Car has deleted successfuly..";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Car has not deleted. Please try again.";
                return RedirectToAction("Index");
            }
        } 
        /// <summary>
        /// car data ajax request
        /// </summary>
        /// <param name="fc">form collection</param>
        /// <returns>json string</returns>
        [HttpPost]

        public string CarData(FormCollection fc)
        {
            string model = fc["model"];
            using (TypeCarM car = new TypeCarM())
            {
                var carData = car.GetByCarNum(model);
                // instantiate a serializer
                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
                var TheJson = TheSerializer.Serialize(new
                {
                    Model = carData.Model,
                    Manufacturer = carData.Manufacturer,
                    Cost = carData.DailyCost,
                    Delay = carData.DelayCost,
                    Geer = carData.Geer,
                    Year = carData.YearManufacture

                }
                                                                                                );
                return TheJson;
            }

        }
        /// <summary>
        /// rent data ajax request
        /// </summary>
        /// <param name="fc">form collection</param>
        /// <returns>json string</returns>
        [HttpPost]

        public string RentalCarData(FormCollection fc)
        {
            string id = fc["Id"];
            using (CarM car = new CarM())
            {
                var carData = car.GetByNumId(int.Parse(id));
                // instantiate a serializer
                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
                var TheJson = TheSerializer.Serialize(new
                {
                    Model = carData.CarModel,
                    free = carData.FreeForRent,
                    CurrentDistance = carData.CurrentDistance,
                    Branch = carData.Branch,
                    Image = carData.Image
                }
                                                                                                );
                return TheJson;
            }

        }
        /// <summary>
        /// car model data ajax request
        /// </summary>
        /// <param name="fc">form collection</param>
        /// <returns>json string</returns>
        [HttpPost]
        public string CarModel(FormCollection fc)
        {
            string m = fc["m"];
            using (TypeCarM car = new TypeCarM())
            {
                var carData = car.SearchFor(mod => mod.Manufacturer == m).Select(mod=>mod.Model).Distinct();
                // instantiate a serializer
                JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
                var json = TheSerializer.Serialize(carData);
                return json;
            }

        }
        /// <summary>
        /// search car in branch ajax request 
        /// </summary>
        /// <param name="fc">form collection</param>
        /// <returns>json string</returns>
        [HttpPost]
        public string Branches(FormCollection fc)
        {
            string model = fc["model"];
            using (CarM car = new CarM())
            {
                var carData = car.GetBranches(model);
                // instantiate a serializer
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(carData);

                return json;
            }

        }
    }
}
