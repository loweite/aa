using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBlogSystem.Models;
using WebBlogSystem.Security;
using System.Web.Security;
namespace WebBlogSystem.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public static int adminid;
        BlogSystemEntities db = new BlogSystemEntities();
        MemberShip member = new MemberShip();
        BlogDB blogdb = new BlogDB();
        public ActionResult Index()
        {
            List<Blog> bloglist = blogdb.GetRecommandBlog();
            ViewBag.bloglist = bloglist;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string ReturnUrl = "")
        {
            List<Blog> bloglist = blogdb.GetRecommandBlog();
            ViewBag.bloglist = bloglist;
           
            string name = Request.Form["username"].ToString();
            string pass = Request.Form["password"].ToString();
            string role = Request.Form["group1"].ToString();
            if (role == "a")
            {
                if (member.ValidateUser(name, pass))
                {
                    FormsAuthentication.SetAuthCookie(name, true);  //将name 给cookie
                    return RedirectToAction("UserIndex", "Home");
                }
            }
            else
            { 
                if(member.ValidateAdmin(name,pass))
                {
                    adminid = member.getAdminIdByName(name);
                    return RedirectToAction("Index", "Admin"); 
                }
            }
           // ViewBag.username = HttpContext.User.Identity.Name;
            return View();
        }
        public ActionResult UserIndex()
        {
            List<Blog> bloglist = blogdb.GetRecommandBlog();
            ViewBag.bloglist = bloglist;
            ViewBag.username = HttpContext.User.Identity.Name;
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult UserInfo(string name)
        {
            bool flag = member.IsFocus(HttpContext.User.Identity.Name, name);
            ViewBag.flag = flag ? "true" : "false";
            string username = name;
            string imag = member.GetUserImage(name);
            ViewBag.imag = imag;
            List<Blog> bloglist = blogdb.getBlogByUsername(username);
            int  focunum = member.GetUserFocusNum(username);
            int flollwnum = member.GetfollowerNum(username);
            int leavemessagenum = member.GetLeaveMessageNum(username);
            int usercollectnum = member.GetUserCollectBlogNum(username);
            int notreadchatnum = member.GetNotReadChat(username);
            List<Notice> noticelist = member.GetNoticeByName(name);
            Notice notice = new Notice();
            notice.time = DateTime.Now;
            notice.content = "无公告";
            if (noticelist.Count > 0)
                ViewBag.notice = noticelist.ElementAt(0);
            else
            {
                ViewBag.notice = notice;
            }
            ViewBag.focunum = focunum;
            ViewBag.flollwnum = flollwnum;
            ViewBag.bloglist = bloglist;
            ViewBag.usercollectnum = usercollectnum;
            ViewBag.notreadchatnum = notreadchatnum;
            ViewBag.leavemessagenum = leavemessagenum;
            ViewBag.username = username;
            return View();
        }
        public ActionResult GetUserFocus(int id,string name)
        {
            string imag = member.GetUserImage(name);
            ViewBag.imag = imag;
            bool flag = member.IsFocus(HttpContext.User.Identity.Name, name);
            ViewBag.flag = flag?"true":"false";
            string username = "a";
            List<Focus> focuslist = member.GetUserFocus(name);
            List<string> namelist = new List<string>();
            foreach (var a in focuslist)
            {
                namelist.Add(member.getUserNameById(a.userid2));
            }
            List<Notice> noticelist = member.GetNoticeByName(name);
            Notice notice = new Notice();
            notice.time = DateTime.Now;
            notice.content = "无公告";
            if (noticelist.Count > 0)
                ViewBag.notice = noticelist.ElementAt(0);
            else
            {
                ViewBag.notice = notice;
            }
            ViewBag.namelist = namelist;
            ViewBag.focuslist = focuslist;
            ViewBag.username = name;
            return View();
        }
        public ActionResult GetUserFollower(int id, string name)
        {
            string imag = member.GetUserImage(name);
            ViewBag.imag = imag;
            bool flag = member.IsFocus(HttpContext.User.Identity.Name, name);
            ViewBag.flag = flag ? "true" : "false";
            List<string> namelist = member.GetUserFocused(name);
            List<Notice> noticelist = member.GetNoticeByName(name);
            Notice notice = new Notice();
            notice.time = DateTime.Now;
            notice.content = "无公告";
            if (noticelist.Count > 0)
                ViewBag.notice = noticelist.ElementAt(0);
            else
            {
                ViewBag.notice = notice;
            }
            ViewBag.namelist = namelist;
            ViewBag.username = name;
            return View();
        }
        public ActionResult GetUserLeaveMessage(int id, string name)
        {
            string imag = member.GetUserImage(name);
            ViewBag.imag = imag;
            bool flag = member.IsFocus(HttpContext.User.Identity.Name, name);
            ViewBag.flag = flag ? "true" : "false";
            List<LeaveMessage> llist = member.GetLeaveMessage(name);
            List<string> namelist =new List<string>();
            List<Notice> noticelist = member.GetNoticeByName(name);
            Notice notice = new Notice();
            notice.time = DateTime.Now;
            notice.content = "无公告";
            if (noticelist.Count > 0)
                ViewBag.notice = noticelist.ElementAt(0);
            else
            {
                ViewBag.notice = notice;
            }
            foreach (var a in llist)
            {
                namelist.Add(member.getUserNameById(a.userid1));
            }
            ViewBag.namelist = namelist;
            ViewBag.username = name;
            ViewBag.llist = llist;
            return View();
        }


        public ActionResult GetUserCollect(int id,string name)
        {
            string imag = member.GetUserImage(name);
            ViewBag.imag = imag;
            bool flag = member.IsFocus(HttpContext.User.Identity.Name, name);
            ViewBag.flag = flag ? "true" : "false";
            List<Blog> bloglist = member.GetUserCollecedtBlog(name);
            List<string> namelist = new List<string>();
            foreach (var a in bloglist)
            {
                namelist.Add(member.getUserNameById(a.userid));
            }
            Notice notice = new Notice();
            notice.time = DateTime.Now;
            notice.content = "无公告";
            List<Notice> noticelist = member.GetNoticeByName(name);
            if (noticelist.Count > 0)
                ViewBag.notice = noticelist.ElementAt(0);
            else
            {
                ViewBag.notice = notice; 
            }
            ViewBag.namelist = namelist;
            ViewBag.bloglist = bloglist;
            ViewBag.username = name;
            return View();
        }
        public ActionResult UserChat(string name)
        {
            List<string> namelist = member.GetChatUser(HttpContext.User.Identity.Name);
            List<Chat> chatlist = null; 
            if (name == null||name==HttpContext.User.Identity.Name)
            {
                if (namelist.Count == 0)
                {
                    ViewBag.targetuser = ""; chatlist = member.GetChatWith("");
                }
                else
                { ViewBag.targetuser = namelist.ElementAt(0); chatlist = member.GetChatWith(namelist.ElementAt(0)); }
            }else if (!namelist.Contains(name))
            {
              chatlist=  member.GetChatWith(name);
                ViewBag.targetuser = name;
                namelist.Clear();
                namelist.Add(name);
                foreach (var a in member.GetChatUser(HttpContext.User.Identity.Name))
                {
                    namelist.Add(a);
                }
            }
            ViewBag.chatlist = chatlist;
            ViewBag.namelist = namelist;

            return View();
        }
        public string loginpost()
        {
            string name = Request["username"].ToString();
            string pass = Request["password"].ToString();
            if (member.ValidateUser(name, pass))
            {
                FormsAuthentication.SetAuthCookie(name, true);  //将name 给cookie
                //return "true";
                return HttpContext.User.Identity.Name;
            }
            else
            {
                return "false";
            }
           // return name;
        }
        public string logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return "已退出登录";
        } 
        public string registerpost()
        {
            string name = Request["username"].ToString();
            string image = Request["image"].ToString();
            string pass = Request["password"].ToString();
            string paypass ="a";
            string email = Request["email"].ToString();
            string phone = Request["phone"].ToString();
            string tip = Request["tip"].ToString();
            string instruction = Request["instruction"].ToString();
            MemberShip member = new MemberShip();
            string mesage = member.CreateUser(name, pass, paypass, email, phone, tip, instruction,image);
            return mesage;
            return "register";
        }
        public string deletefocus()
        {
            string username1 = HttpContext.User.Identity.Name.ToString().Trim();
            string username2 = Request["username"].ToString().Trim();
            return member.DeleteFocus(username1, username2);
        }
        public string focususer()
        {
            string username2 = Request["username"].ToString().Trim();
            if (HttpContext.User.Identity.Name == "")
            {
                return "请先登录";
            }
            else
            {
                return member.AddFocus(HttpContext.User.Identity.Name, username2, username2);
            }
        }
        public string disfocus()
        {
            string username2 = Request["username"].ToString().Trim();
            if (HttpContext.User.Identity.Name == "")
            {
                return "请先登录";
            }
            else
            {
                return member.DeleteFocus(HttpContext.User.Identity.Name, username2);
            }
        }
        public string addleavemessage()
        {
            string username1 = HttpContext.User.Identity.Name;
            string username2 = Request["username"].ToString().Trim();
            string content = Request["content"].ToString().Trim();
            return member.AddLeaveMessage(username1, username2, content);
        }
        public string addchat()
        {
            string username2 = Request["username"].ToString().Trim();
            string content = Request["content"].ToString().Trim();
            return member.AddChat(HttpContext.User.Identity.Name, username2, content);
        }
        public string  selectuserchat()
        {
            string username = Request["username"].ToString().Trim();
            List<Chat> chatlist = member.GetChatWith(username);
            string s="";
            foreach(var a in chatlist)
            {
                s += "<div class='user_letter' >     " + member.getUserNameById(a.userid1) + "(" + a.time + "):<br/>" + a.content + "</div>";
            }
            s += "<div id='appenddiv'></div>";
            return s;
        }
        public string deletechat()
        {
            string username = Request["username"].ToString().Trim();
            return member.DeleteChatWith(username);
        }
        public string writenotice()
        {
            string username = HttpContext.User.Identity.Name;
            string content = Request["content"].ToString().Trim();
            if (username == "")
            {
                return "请先登录";
            }
            else {
                return member.AddNotice(username, content);
            }
        }

     
        public string Recommend()
        {
            int blogid = Convert.ToInt32(Request["blogid"].ToString());
            if (member.ValidateRecommend(adminid, blogid))
            {
                return "";
            }
            else
                return member.AddRecommend(adminid, blogid);
        }


        public string DeleteUser()
        {
            int userid = Convert.ToInt32(Request["userid"].ToString());

            return member.DeleteUser(userid);
        }

        public String Delete()
        {
            string username = Request["username"].ToString();

            User user = new User();

            List<User> userlist = member.getUserByUserName(username);
            String str = "";
            if (username == "")
            {
                userlist = member.GetAllUsers();
            }
            else
            {
                if (member.ValidateUser1(username) == false)
                {
                    return "";
                }
            }
            foreach (var q in userlist)
            {
                str += "<div class='user' id='" + q.userid + "'>";
                str += "<img src='" + q.image + "' class='pic' />";
                str += "<p class='name'>" + q.name + "</p>";
                str += "<button class='delete' name='delete' id='" + q.userid + "' >删除</button>";
                str += "</div>";

            }
            return str;
        }


        public String Search()
        {
            string username = Request["username"].ToString();


            BlogDB blogdb = new BlogDB();
            List<Blog> bloglist = blogdb.getBlogByUsername(username);
            String str = "";
            if (username == "")
            {
                bloglist = blogdb.getAllBlog();
            }
            else
            {
                if (member.ValidateUser1(username) == false)
                {
                    return "";
                }
            }
            foreach (var q in bloglist)
            {
                str += "<div class='blog' id='" + q.blogid + "'>";
                str += "<img  src=‘u=1194969155,893228591&fm=111&gp=0.jpg’ class=‘pic’/>";
                str += "<p class='name'  >" + member.getUserNameById(q.userid) + "</p>";
                str += "<p class='name'  >" + q.title + "</p>";
                str += "<button class='delete' name='bb' id='" + q.blogid + "'>删除</button>";
                str += "<input type='button' value='推荐' id='" + q.blogid + "'/></div>";
            }
            // str = " @foreach (var q in @BlogSystem.Controllers.AdminController.bloglist1){ <table id='@q.blogid' style='width:100%'><tr><td>title:</td><td>@q.title</td></tr><tr> <td>category:</td><td>@q.category</td></tr><tr> <td>updatetime:</td><td>@q.updatetime</td></tr> </table>}"; 
            return str;
        }

        public string DeleteBlog()
        {
            int blogid = Convert.ToInt32(Request["blogid"].ToString());

            return member.DeleteBlog(blogid);
        }


        public int Changeuserinfo()
        {
            string username = Request["username"].ToString();
            int number = member.getUserIdByName(username);
            if (number == 0)
            {
                return 0;
            }
            return number;
        }


        
    }
}
