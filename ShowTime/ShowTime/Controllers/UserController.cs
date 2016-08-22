using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShowTime.Models;
using ShowTime.DAL;
using ShowTime.ViewModel;

namespace ShowTime.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult cancelShow()
        {
            OrderDAL ordDB = new OrderDAL();
            int index = int.Parse(Request.QueryString["ref"]);
            Order ord = ordDB.Orders.Single(x => x.ID == index);
            ordDB.Orders.Remove(ord); // try to enter to DB the order
            ordDB.SaveChanges(); // save db of order
            TempData["Ok"] = " !ההזמנה בוטלה ";
            return RedirectToAction("mybuyshow");
            
        }

        public ActionResult mybuyshow()
        {
            OrderDAL db = new OrderDAL();
            ShowDAL showDB = new ShowDAL();
            OrderVM list = new OrderVM();
            try { 
            string user = Session["User"].ToString();
            list.orderlist=db.Orders.Where<Order>(x => (x.Name== user)).ToList<Order>();
            }catch
            {
                return RedirectToAction("home", "Home");
            }
            foreach(Order S in list.orderlist)
            {
                
                Show tmp=showDB.Shows.Single(x => (x.ID == S.Show));
                ViewData[tmp.ID.ToString()] = tmp.Name;
                
            }

            return View(list);
        }

        public ActionResult OrderTiket(Order ord)
        {
            if(@Session["User"]==null)
                return HttpNotFound();
            ModelState.Clear();
            TryValidateModel(ord); // valiate model order

            if (ModelState.IsValid)
            {
                OrderDAL ordDB = new OrderDAL();
                ordDB.Orders.Add(ord); // try to enter to DB the order
                ordDB.SaveChanges(); // save db of order
                TempData["Ok"] = " !הזמנתך נרשמה במערכת ";
                return RedirectToAction("mybuyshow");
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        public ActionResult buytiket()
        {
            int index = -1;
            try
            {
                index = int.Parse(Request.QueryString["ref"]);
            }catch
            {
                return HttpNotFound();
            }
            ViewData["showID"] = index;
            
            ShowDAL showDB = new ShowDAL();
            try { 
            Show TheShow= showDB.Shows.Single(shw => shw.ID == index);
                ViewData["ShowName"] = TheShow.Name;
                ViewData["ShowPrice"] = TheShow.Price;
                return View();
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            @Session["UserName"] = null;
            return RedirectToAction("home","Home"); // return to home page
        }

        public ActionResult signin()
        {
            User u1 = new User();
            return View(u1);
        }

        public ActionResult HandleUserLogin(User usr)
        {
            UserDAL db = new UserDAL();


                try
                {
                    User loginUser = db.Users.Single(users => users.UserName == usr.UserName && users.Password == usr.Password);// search user and password in admin DB
                if (loginUser != null)// if found
                    {
                    Session["UserName"] = loginUser.FirstName + " " + loginUser.LastName;
                        Session["User"] = usr.UserName;
                    return RedirectToAction("home", "Home");
                    }
                }
                catch
                {
                    TempData["alertMessage"] = "שם המשתמש ו/או הסיסמה לא נכונים";
                    return View("signin");
                }
            TempData["alertMessage"] = "תקלה נא לפנות למנהל המערכת";
            return View("signin");
        }

    }
}