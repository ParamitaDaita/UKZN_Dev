using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WroseModel.Models;
using WroseModel.Repository;
using WroseModel.Helper;
using WroseModel.View_Model;

namespace WroseModel.Controllers
{
    public class UserRegistrationController : Controller
    {
        private IRegistration Iregistration;
        private EncryptDecryptPwd encryptDecryptPwd;
        private EmailHelper emailHelper;
        public UserRegistrationController()
        {
            this.Iregistration = new RegistrationRepository(new WROSEDBEntities());
            this.encryptDecryptPwd = new EncryptDecryptPwd();
            this.emailHelper = new EmailHelper();
        }
        public UserRegistrationController(IRegistration registrationRepository)
        {
            Iregistration = registrationRepository;
        }
        // GET: UserRegistration
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRegistration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegistration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRegistration/Create
        [HttpPost]
        public ActionResult Index(FormCollection collection, RegistrationVM objRegistration)
        {
            try
            {
                string useremail = string.Empty;
                int isSaved = 0;
                if (objRegistration.User_Email != null)
                {
                    useremail = this.Iregistration.GetUserByEmail(objRegistration.User_Email);
                }
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(useremail))
                    {
                        ViewBag.DuplicateMessage = "" + useremail + " already exists, please use a different email address.";
                    }
                    else if (ModelState.IsValid)
                    {
                        string encryptedPwd = this.encryptDecryptPwd.EncodePasswordToBase64(objRegistration.User_Password);
                        objRegistration.User_Password = encryptedPwd;
                        objRegistration.Active = 1;
                        objRegistration.User_Role_ID = 1;
                        objRegistration.Created_Date = DateTime.Now;
                        isSaved=this.Iregistration.NewRegistration(objRegistration);
                        if (isSaved == 1)
                        {
                            this.emailHelper.SendMail(objRegistration.User_Email, objRegistration.First_Name);
                            ViewBag.SuccessMessage = "You are successfully registered, please check your email " + objRegistration.User_Email;
                            ModelState.Clear();
                        }
                        else
                        {
                            ViewBag.SuccessMessage = "Registration failed";
                        }
                     
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return View("~/Error/NotFound");
            }
        }
        //[HttpPost] 
        //public JsonResult GetUserByEmail(string email)
        //{
        //    var user_Registration = new User_Registration();
        //    try
        //    {
        //        user_Registration = this.Iregistration.GetUserByEmail(email);
        //        if (ModelState.IsValid)
        //        {
        //            if (user_Registration != null)
        //            {
        //                ViewBag.DuplicateMessage= "Email address " + user_Registration.User_Email + " already exists, please use a different email address.";
        //                return Json(user_Registration.User_Email, JsonRequestBehavior.AllowGet);
        //               // message = "Email address " + user_Registration.User_Email + " already exists, please use a different email address.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Json(user_Registration, JsonRequestBehavior.AllowGet);
        //}
        // GET: UserRegistration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegistration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRegistration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegistration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
