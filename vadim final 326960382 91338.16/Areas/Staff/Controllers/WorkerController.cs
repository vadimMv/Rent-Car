using DAL;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using vadim_final_326960382_91338._16.Areas.Staff.Models;
using vadim_final_326960382_91338._16.Models;
using vadim_final_326960382_91338._16.Security;

namespace vadim_final_326960382_91338._16.Areas.Staff.Controllers
{
        /// <summary>
        /// Staff controller 
        /// </summary>
    public class WorkerController : Controller
    {
        /// <summary>
        /// Index action searching busy cars or single car by name 
        /// </summary>
        /// <param name="CarNum"></param>
        /// <returns>Cars list</returns>
        // GET: Staff/Worker
        [RolesAuth(Roles = "Staff")]
        public ActionResult Index(string CarNum)
        {
            using (SearchForReturn Car = new SearchForReturn())
            {
                var CID = 0;
                bool match = int.TryParse(CarNum, out CID);
                if (!match && CarNum != null)
                    ViewBag.validation = "Input no valid";
                return View(Car.GetItems(CarNum).ToList());
            }
        }
        /// <summary>
        /// Free-up cars action using by ajax request
        /// </summary>
        /// <param name="ID">car number</param>
        /// <param name="dist"> new kilometrage</param>
        /// <returns> json returned car data</returns>
        [RolesAuth(Roles = "Staff")]
        public string Free(int? ID, int? dist)
        {
            Car carFree = null;
            var days = 0.0;
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            using (RentCar rentM = new RentCar())
            {
                // search last active rent by car id
                CarsRent rent = rentM.SearchFor(c => c.NumCar == ID).LastOrDefault();
                if (rent != null)
                {
                    rent.RealFinishRent = System.DateTime.Now;
                    rentM.Update(rent);
                    days = (rent.FinishRent - rent.StartRent).TotalDays;

                    // search & free-up car
                    using (CarM tCar = new CarM())
                    {
                        carFree = tCar.SearchFor(c => c.CarNumber == ID).First();
                        carFree.FreeForRent = true;
                        if (dist != null)
                            carFree.CurrentDistance = (int)dist;
                        tCar.Update(carFree);
                    }

                    // succsess response 
                    var TheJson = TheSerializer.Serialize(new
                    {
                        ID = carFree.CarNumber,
                        CarModel = carFree.CarModel,
                        Days = days,

                    }
                );
                    return TheJson;

                }
                else
                {
                    // bad response
                    var TheJsonErr = TheSerializer.Serialize(new
                    {
                        text = "Invalid rent detected"
                    }
                );
                    return TheJsonErr;
                }
            }


        }

        /// <summary>
        /// Search dayli price (ajax using)
        /// </summary>
        /// <param name="model">car model</param>
        /// <returns>price value json file</returns>

        [RolesAuth(Roles = "Staff")]
        public string Price(string model)
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            CarType ct = null;
            using (TypeCarM tCar = new TypeCarM())
            {
                ct = tCar.SearchFor(t => t.Model == model).FirstOrDefault();
                var TheJson = TheSerializer.Serialize(new
                {
                    daily = ct.DailyCost,
                }
            );
                return TheJson;
            }


        }

        /// <summary>
        /// Editing rent data  (ajax using)
        /// </summary>
        /// <param name="curr">rent model</param>
        /// <returns> updated data</returns>
        [RolesAuth(Roles = "Admin")]
        public string Edit(CarsRent curr)
        {    
            // serialization instance 
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            try
            {
                // search rent in data base 
                using (RentCar rent = new RentCar())
                {
                    // update
                    CarsRent eqv = rent.GetById(curr.id);
                    if (eqv != null)
                    {
                        eqv.StartRent = curr.StartRent;
                        eqv.FinishRent = curr.FinishRent;
                        eqv.RealFinishRent = curr.RealFinishRent;
                        rent.Update(eqv);
                    }
                }

                //  response success
                var TheJson = TheSerializer.Serialize(new
                {
                    id = curr.id,
                    NumCar = curr.NumCar,
                    NumUser = curr.NumUser,
                    StartRent = curr.StartRent,
                    FinishRent = curr.FinishRent,
                    RealFinishRent = curr.RealFinishRent,

                }
              );
                return TheJson;
            }
            catch
            {
                // bad response
                var TheJsonErr = TheSerializer.Serialize(new
                {
                    text = "Invalid operaton..."
                }
            );
                return TheJsonErr;
            }
        }
    }
}