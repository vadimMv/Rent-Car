using BL;
using DAL;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;
using vadim_final_326960382_91338._16.Security;

namespace vadim_final_326960382_91338._16.Areas.Admin.Controllers
{
    /// <summary>
    /// controller people administration
    /// </summary>
    /// 
    [RolesAuth(Roles = "Admin")]
    public class ManageHumanController : Controller
    {
        /// <summary>
        /// getting all peoples fom data base
        /// </summary>
        /// <returns>view with people list</returns>
        // GET: User
        public ActionResult Index()
        {

            using (UserM user = new UserM())
            {
                return View(user.GetAll().ToList());
            }
        }

        /// <summary>
        /// getting all peoples fom data base
        /// </summary>
        /// <returns>view with people list</returns>
        // GET: User
        [HttpPost]
        public ActionResult Index(string IdOrName)
        {
            using (UserM user = new UserM())
            {
                int uid;
                bool isNumeric = int.TryParse(IdOrName, out uid);
                if (isNumeric == true)
                    return View(user.GetByIdNum(uid).ToList());
                else if (isNumeric == false && IdOrName!=null && IdOrName!="")
                    return View(user.GetbyName(IdOrName).ToList());
                return View(user.GetAll().ToList());
            }

        }
        /// <summary>
        /// removing user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user list view</returns>
        public ActionResult Delete(int id)
        {
            try
            {
                using (UserM user = new UserM())
                {
                    User finded = user.GetById(id);
                    user.Delete(finded);
                }
                TempData["Message"] = "User was removed from system...";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Message"] = "Remove unfortunately has stopped...";
                return RedirectToAction("Index");
            }
        }


        // GET: User/Edit/5
        /// <summary>
        /// get user form
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user form view</returns>
        public ActionResult Edit(int id)
        {
            using (UserM user = new UserM())
            {
                User finded = user.GetById(id);
                UserEditModel edit = new UserEditModel();

                Util<User, UserEditModel>.Copy(finded, edit);
                ModelState.Clear();
                return View(edit);
            }

        }

        /// <summary>
        /// sending user data to server
        /// </summary>
        /// <param name="user">user model</param>
        /// <param name="upload">image</param>
        /// <param name="id">id</param>
        /// <returns>rederect to index view by succsess</returns>
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdNumber, FullName, BirthDate, Sex, Password, Email, Role")] UserEditModel user, HttpPostedFileBase upload, int id)
        {
            try
            {
                using (UserM model = new UserM())
                {
                    User orginUser = model.GetById(id);
                    if (ModelState.IsValid)
                    {
                        Util<UserEditModel, User>.Copy(user, orginUser);
                        if (upload != null && upload.ContentLength > 0)
                        {
                            model.UpdateWithImage(orginUser, upload);
                        }
                        else
                        {
                            model.Update(orginUser);
                        }
                        return RedirectToAction("Index");
                    }

                }
                return View();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception e)
            {
                return View(e.ToString());
            }
        }
    }
}