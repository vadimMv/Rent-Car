using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vadim_final_326960382_91338._16.Models;
using DAL;
using System.Data.Entity.Validation;
using System.Web.Security;
using System.Web.Script.Serialization;
using BL;
using System.IO;

namespace vadim_final_326960382_91338._16.Controllers
{
    /// <summary>
    /// user controller 
    /// </summary>
    public class UserController : Controller
    {

        /// <summary>
        /// enter to app page
        /// </summary>
        /// <returns>login page</returns>
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// send login form to server check authefication
        /// </summary>
        /// <param name="user"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            // Lets first check if the Model is valid or not
            //if (ModelState.IsValid)
            //{
            using (UserM userM = new UserM())
            {


                // Now if our password was enctypted or hashed we would have done the
                // same operation on the user entered password here, But for now
                // since the password is in plain text lets just authenticate directly

                bool userValid = userM.Any(user);

                // User found in the database
                if (userValid)
                {

                    FormsAuthentication.SetAuthCookie(user.FullName, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(user);
        }
        /// <summary>
        /// log out redirect to index
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// get details 
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            using (UserM user = new UserM())
            {
                return View(user.GetById(id));
            }
        }
        /// <summary>
        /// registration page
        /// </summary>
        /// <returns>registration page</returns>
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// send registration form to server 
        /// </summary>
        /// <param name="user">user model object</param>
        /// <param name="upload">image</param>
        /// <returns></returns>
        // POST: User/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdNumber, FullName, BirthDate, Sex, Password, Email, Role")]User user, HttpPostedFileBase upload)
        {
            try
            {
                using (UserM UserModel = new UserM())
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        UserModel.SaveWithImage(user, upload);

                    }
                    else
                    {
                        UserModel.Create(user);
                    }
                    FormsAuthentication.SetAuthCookie(user.FullName, false);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// get editing populated  form 
        /// </summary>
        /// <param name="id">id </param>
        /// <returns>editing form</returns>
        // GET: User/Edit/5
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
        /// send edited form to server 
        /// </summary>
        /// <param name="user">user object</param>
        /// <param name="upload">image</param>
        /// <param name="id"> id</param>
        /// <returns>redirection to index </returns>
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IdNumber, FullName, BirthDate, Sex, Password, Email, Role")] UserEditModel user, HttpPostedFileBase upload ,int id)
        {
            try
            {
                using (UserM model = new UserM())
                {
                    User orginUser = model.GetById(id);
                    if (ModelState.IsValid)
                    {
                      //  if (TryUpdateModel(orginUser))
                            Util<UserEditModel, User>.Copy(user, orginUser);
                        if (upload != null && upload.ContentLength > 0)
                        {
                            model.UpdateWithImage(orginUser, upload);
                        }
                        else
                        {
                            model.Update(orginUser);
                        }
                        return RedirectToAction("Index", "Home");
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

        
        /// <summary>
        /// remove user 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>redirect to index</returns>
        // GET: User/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                using (UserM user = new UserM())
                {
                    User finded = user.GetById(id);
                    user.Delete(finded);
                }
                ViewBag.Message = "User was deleted .";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
        }
        /// <summary>
        /// user data getting using by ajax 
        /// </summary>
        /// <param name="fc">form collection</param>
        /// <returns>json string</returns>
        [HttpPost]
       
        public string UserData(FormCollection fc)
        {
            // instantiate a serializer
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            if (!User.Identity.IsAuthenticated)
            {
                return TheSerializer.Serialize(new { noAuth= "NoAuthrezited"}) ;
            }
            string userName = fc["FullName"];
            using (UserM user = new UserM())
            {
                var userData = user.SearchFor(u => u.FullName == userName).FirstOrDefault();
                if (userData.Image == null) {
                    string path = Server.MapPath("~/Images/Logos/noPhoto.jpg");
                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                     userData.Image = new byte[(int)fileStream.Length];
                    fileStream.Read(userData.Image, 0, userData.Image.Length);
                }
                var TheJson = TheSerializer.Serialize(new
                {
                    Email = userData.Email,
                    IdNumber = userData.IdNumber,
                    Image = userData.Image,
                    id = userData.id,
                }
                                                                                              );
                return TheJson;
            }

        }
    }
}
