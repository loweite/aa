using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBlogSystem.Models;
using WebBlogSystem.Security;
namespace WebBlogSystem.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        // GET: /Admin/
        MemberShip member = new MemberShip();
        BlogDB blogdb = new BlogDB();

        public ActionResult Index()
        {


            return View();
        }


        public ActionResult UserInfo(int id)
        {


            User user = member.GetUserByid(id);

            return View(user);
        }

        public string search()
        {
            return "true";
        }

        public string changeuserinfo()
        {
            string name = Request["username"].ToString();
            string tip = Request["tip"].ToString();
            string instruction = Request["instruction"].ToString();
            string email = Request["email"].ToString();
            string balance = Request["balance"].ToString();
            string password = Request["password"].ToString();
            //string changeinfo = Request["changeinfo"].ToString();
            string paypassword = Request["paypassword"].ToString();
            string phone = Request["phone"].ToString();
            User user = new User();
            user.name = name;
            user.instruction = instruction;
            user.tip = tip;
            user.password = password;
            user.paypassword = paypassword;
            user.phone = phone;
            user.balance = Int32.Parse(balance);
            user.email = email;

            return member.UpdateUser(user);
        }


    }
}
