using HPL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPL.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PlayerDetail obj)

        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                try
                {
                    HPLEntities db = new HPLEntities();

                    if (db.PlayerDetails.Any(pd => pd.EmployeeId == obj.EmployeeId))
                    {
                        return Redirect("/AlreadyRegistered.html");
                    }
                    string _FileName = Path.GetFileNameWithoutExtension(obj.UploadedImage.FileName);
                    string _Extension = Path.GetExtension(obj.UploadedImage.FileName);
                    _FileName = _FileName + "_" + DateTime.Now.ToString("mmddyyyy") + _Extension;
                    obj.ImageUrl = "~/UploadedImages/" + _FileName;
                    _FileName = Path.Combine(Server.MapPath("~/UploadedImages"), _FileName);
                    obj.UploadedImage.SaveAs(_FileName);
                                        
                    db.PlayerDetails.Add(obj);
                    db.SaveChanges();
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            //return View(obj);

            return Redirect("/RegistrationComplete.html");
        }
    }
}