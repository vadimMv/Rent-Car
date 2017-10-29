using DAL;
using System.Linq;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;
using vadim_final_326960382_91338._16.Security;

namespace vadim_final_326960382_91338._16.Areas.Admin.Controllers
{
    /// <summary>
    /// Rent controller 
    /// </summary>
    /// 
    [RolesAuth(Roles = "Admin")]
    public class ManageRentController : Controller
    {
        // GET: Admin/ManageRent
        /// <summary>
        /// all rents 
        /// </summary>
        /// <returns>view with rents list</returns>
        public ActionResult Index()
        {
            using (RentCar tCar = new RentCar())
            {
                return View(tCar.GetAll().ToList());
            }
        }

        /// <summary>
        /// getting all rents fom data base
        /// </summary>
        /// <returns>view with rents list</returns>
        // GET: User
        [HttpPost]
        public ActionResult Index(string IdOrNum)
        {
            using (RentCar rents = new RentCar())
            {
                int uid;
                bool isNumeric = int.TryParse(IdOrNum, out uid);
                if (isNumeric == true)
                    return View(rents.GetByIdOrNum(uid).ToList());
   
                return View(rents.GetAll().ToList());
            }

        }
        /// <summary>
        /// deleting rents
        /// </summary>
        /// <param name="id">rent id</param>
        /// <returns>redirect to index action</returns>
        public ActionResult Delete(int id)
        {
            try
            {
                using (RentCar c = new RentCar())
                {
                    CarsRent finded = c.GetById(id);
                    c.Delete(finded);
                }
                TempData["RentDel"] = "Rent information was removed from system...";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["RentDel"] = "Remove unfortunately has stopped...";
                return RedirectToAction("Index");
            }
        }
    }
}