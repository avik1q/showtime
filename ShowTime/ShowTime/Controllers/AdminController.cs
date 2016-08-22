using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShowTime.Models;
using ShowTime.DAL;
using ShowTime.ViewModel;
using System.Data.Entity;

namespace ShowTime.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (TempData["Error"] != null)
                ViewData["Error"] = TempData["Error"];
            if (TempData["Ok"] != null)
                ViewData["Ok"] = TempData["Ok"];
            return View();
        }

        public ActionResult ViewOrders()
        {
            OrderDAL db = new OrderDAL();
            ShowDAL showDB = new ShowDAL();
            OrderVM list = new OrderVM();
            try
            {
                list.orderlist = db.Orders.ToList<Order>();
            }
            catch
            {
                TempData["Error"] = "תקלה בקריאה מבסיס הנתונים נא פנה למפתח האתר";
                return RedirectToAction("index", "Admin");
            }
            foreach (Order S in list.orderlist)
            {

                Show tmp = showDB.Shows.Single(x => (x.ID == S.Show));
                ViewData[tmp.ID.ToString()] = tmp.Name;

            }

            return View(list);
        }

        public ActionResult addpastshow()
        {
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            PastShowDAL psDB = new PastShowDAL();
            PastShowVM psVM = new PastShowVM();
            psVM.pastshow = new PastShow();
            psVM.pastshowlist = psDB.PastShows.ToList();
            return View(psVM);
        }
        public ActionResult updatePastShow(PastShow show)
        {
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            PastShowDAL psDB = new PastShowDAL();
            show.ID = int.Parse(Request.Form["item.ID"].ToString());
            show.Name = Request.Form["item.Name"].ToString();
            show.URL= Request.Form["item.URL"].ToString();
            var itemToUpdate = show;


            psDB.PastShows.Attach(itemToUpdate);//attach to DB
            psDB.Entry(itemToUpdate).State = EntityState.Modified;// modified mode

            psDB.SaveChanges(); //save DB

            return RedirectToAction("addpastshow");
        }
        public ActionResult newpastshow(PastShow show)
        {
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            PastShowDAL psDB = new PastShowDAL();
            show.Name = Request.Form["pastshow.Name"].ToString();
            show.URL = Request.Form["URL"].ToString();
            var itemToUpdate = show;

            psDB.PastShows.Add(itemToUpdate);//attach to DB
            psDB.SaveChanges(); //save DB

            return RedirectToAction("addpastshow");
        }
        public ActionResult deletepastshow(PastShow show)
        {
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            PastShowDAL psDB = new PastShowDAL();
            int index = int.Parse(Request.Form["ref"].ToString());

            var itemToUpdate = psDB.PastShows.Single(x => x.ID == index);

            psDB.PastShows.Remove(itemToUpdate);//attach to DB
            psDB.SaveChanges(); //save DB

            return RedirectToAction("addpastshow");
        }

        public ActionResult addshow()
        {
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            ShowDAL psDB = new ShowDAL();
            ShowVM showVM = new ShowVM();
            showVM.shows = new Show();
            showVM.showlist = psDB.Shows.ToList();
            return View(showVM);
        }
        public ActionResult updatepageshow()
        {
            int index = int.Parse(Request.QueryString["ref"].ToString());
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            ShowDAL psDB = new ShowDAL();
            Show show = psDB.Shows.Single(x => x.ID == index);
            return View(show);
        }
        public ActionResult updateshow(Show show)
        {
            try {
                string a1 = Request.Form["Day"].ToString() + "/" + Request.Form["Month"].ToString() + "/" + Request.Form["Year"].ToString();
                DateTime a = Convert.ToDateTime(a1);

                show.Date = a;
            }
            catch { }
            ShowDAL psDB = new ShowDAL();
            psDB.Shows.Attach(show);//attach to DB
            psDB.Entry(show).State = EntityState.Modified;// modified mode

            psDB.SaveChanges(); //save DB

            return RedirectToAction("addshow");
        }


        public ActionResult newshow(Show show)
        {
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            string a1 = Request.Form["Day"].ToString() + "/" + Request.Form["Month"].ToString() + "/" + Request.Form["Year"].ToString();
            DateTime a = Convert.ToDateTime(a1);
            ShowDAL psDB = new ShowDAL();
            show.Name = Request.Form["shows.Name"].ToString();
            show.Date = a;
            show.Loc = Request.Form["shows.Loc"].ToString();
            show.Price = int.Parse(Request.Form["shows.Price"].ToString());
            show.Commits = Request.Form["shows.Commits"].ToString();
            var itemToUpdate = show;

            psDB.Shows.Add(itemToUpdate);//attach to DB
            psDB.SaveChanges(); //save DB

            return RedirectToAction("addshow");
        }
        public ActionResult deleteshow()
        {
            int index = int.Parse(Request.QueryString["ref"].ToString());
            AdminDAL db = new AdminDAL();
            Admin loginAdmin = null;
            try
            {
                string admin = Session["Admin"].ToString();
                loginAdmin = db.Admins.Single(adms => adms.UserName == admin); // double chack in db
            }
            catch // if something is go bed
            {
                TempData["Error"] = " !אין אפשרות לצפות בדף זה ללא הרשאת מנהל";
                return RedirectToAction("home", "Home");
            }
            ShowDAL psDB = new ShowDAL();

            Show show = psDB.Shows.Single(x => x.ID == index);
            psDB.Shows.Remove(show);//attach to DB
            psDB.SaveChanges(); //save DB

            return RedirectToAction("addshow");
        }


        public ActionResult Login()
        {
            Admin adm = new Admin();
            return View(adm);
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("home", "Home"); // return to home page
        }
        public ActionResult HandleAdminLogin(Admin adm)
        {
            AdminDAL db = new AdminDAL();
            try
            {
                Admin loginAdmin = db.Admins.Single(adms => adms.UserName == adm.UserName && adms.Password == adm.Password);// search admin and password in admin DB
                if (loginAdmin != null)// if found
                {
                    Session["Admin"] = adm.UserName;
                    TempData["Error"]= null;
                    return RedirectToAction("index", "Admin");
                }
            }
            catch
            {
                List<Admin> aaa = db.Admins.ToList();
                TempData["alertMessage"] = "ש1ם המשתמש ו/או הסיסמה לא נכונים";
                return View("Login");
            }
            TempData["alertMessage"] = "תקלה נא לפנות למנהל המערכת";

            return View("Login");

        }

    }
}