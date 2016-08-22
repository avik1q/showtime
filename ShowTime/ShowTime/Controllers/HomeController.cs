using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShowTime.Models;
using ShowTime.DAL;
using ShowTime.BindModel;
using ShowTime.ViewModel;

namespace ShowTime.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult about()
        {
            return View();
        }
        public ActionResult pastshow()
        {
            PastShowDAL psDb = new PastShowDAL();
            PastShowVM psVM = new PastShowVM();
            psVM.pastshowlist = psDb.PastShows.ToList();
            return View(psVM);
        }
        public ActionResult SearchItems()
        {
            ShowVM showVM = new ShowVM();
            return View(showVM);
        }
        public ActionResult hendleSearchItems(string ref1)
        {
            try
            {
                string nameshow = Request.QueryString["ref1"];
                ShowDAL showDB = new ShowDAL();
                List<Show> listshow = showDB.Shows.Where<Show>(x => (x.Name.Contains(nameshow))).ToList<Show>();
                ShowVM showVM1 = new ShowVM();
                showVM1.showlist = listshow;
                if (listshow.Count==0)
                    ViewData["Error"] = "אין הופעה בשם זה";
                return View("SearchItems",showVM1);
            }
            catch
            {
                ViewData["Error"] = "שגיאה קריטית נא לחפש הופעה בשם חוקי";
                ShowVM showVM = new ShowVM();
            return View("SearchItems",showVM);
            }

        }
        public ActionResult register()
        {
            User usr = new User();
            return View(usr);
        }
        public ActionResult home()
        {
            PastShowDAL PshowDB = new PastShowDAL();
            ShowDAL showDB = new ShowDAL();
            
            ShowsOverView SOV = new ShowsOverView();
            SOV.showlist = showDB.Shows.ToList<Show>();
            SOV.pastshowlist = PshowDB.PastShows.ToList<PastShow>();
            return View(SOV);
        }
        public ActionResult RegisterToSite(User usr)
        {
            ModelState.Clear();
            TryValidateModel(usr); // valiate model order
            UserDAL userDB = new UserDAL();
            User user = null;
            bool userexist = true;
            try
            {
                user = userDB.Users.Single(userr => userr.UserName == usr.UserName); // find the item from inventory by barcode 
            }
            catch
            {
                userexist = false;
            }

            if (userexist) // if someone try to order more item then the store have
            {
                TempData["Error"] = "המשתמש קיים נסה שם משתמש אחר";

                return RedirectToAction("register", "Home"); // go to register page
            }
            try
            {
                userexist = true;
                AdminDAL admDB = new AdminDAL(); 
                Admin adm = admDB.Admins.Single(userr => userr.UserName == usr.UserName); // find the item from inventory by barcode 
            }
            catch
            {
                userexist = false;
            }

            if (userexist) // if someone try to order more item then the store have
            {
                TempData["Error"] = "המשתמש קיים נסה שם משתמש אחר";

                return RedirectToAction("register", "Home"); // go to register page
            }
            if (ModelState.IsValid) // if model is valid
            {

                userDB.Users.Add(usr); // try to enter to DB the user
                userDB.SaveChanges(); // save db of user
                Session["UserName"] = usr.FirstName + " " + usr.LastName;
                Session["User"] = usr.UserName;
                return RedirectToAction("home"); // return to home page
            }
            else // if something bad happend 
            {
                TempData["Error"] = " הפעולה לא הצליחה ";

                return RedirectToAction("register", "Home");
            }
        }
    }
}