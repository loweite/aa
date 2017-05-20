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
    public class BlogController : Controller
    {
        BlogSystemEntities db = new BlogSystemEntities();
        MemberShip member = new MemberShip();
        BlogDB blogdb = new BlogDB();
        //
        // GET: /Blog/

        public ActionResult Index(int id)
        {
            List<int> bloginfo = blogdb.GetBlogCollectAndGoodNum(id);
            Blog blog = blogdb.GetBlogById(id);
            bool flag = member.IsCollectBlog(HttpContext.User.Identity.Name, id);
            ViewBag.flag = flag ? "true" : "false";
            bool isgivegood = member.IsCollectBlog(HttpContext.User.Identity.Name, id);
            ViewBag.isgivegood = isgivegood ? "true" : "false";
            List<Evaluation> elist = blogdb.GetBlogEvaluation(id);
            List<string> usernamelist = new List<string>();
            foreach (Evaluation e in elist)
            {
                usernamelist.Add(member.getUserNameById(e.userid));
            }
            string username = member.getUserNameById(blog.userid);
          //  ViewBag.blog = blog;
            ViewBag.elist = elist;
            ViewBag.bloginfo = bloginfo;
            ViewBag.username = username;
            ViewBag.usernamelist = usernamelist;
            return View(blog);
           /* MemberShip member = new MemberShip();
            BlogDB blogdb = new BlogDB();
            List<Evaluation> elist = blogdb.GetBlogEvaluation(2011);
            Blog blog = blogdb.GetBlogById(2011);
            ViewBag.username = member.getUserNameById(blog.userid);
            List<string> usernamelist = new List<string>();
            foreach (Evaluation a in elist)
            {
                usernamelist.Add(member.getUserNameById(a.userid));
            }
            ViewBag.usernamelist = usernamelist;
            ViewBag.blog = blog;
            ViewBag.elist = elist;
            return View();*/
        }
        public ActionResult SearchBlog(int id, string name)
        {
            List<Blog> bloglist = blogdb.SearchBlog(name);
            List<string> namelist = new List<string>();
            foreach (var a in bloglist)
            {
                namelist.Add(member.getUserNameById(a.userid));
            }
            ViewBag.bloglist = bloglist;
            ViewBag.namelist = namelist;
            ViewBag.search = name;
            return View();
        }
        public ActionResult EditBlog(int id)
        {
            Blog blog = blogdb.GetBlogById(id);
            return View(blog);
            
        }
        public ActionResult WriteBlog()
        {
            ViewBag.username = HttpContext.User.Identity.Name;
            return View();
        }
        public string evaluaeblog()
        {
            string evaluation = Request["evaluation"].ToString();
            string username = Request["username"].ToString();
            string blogid = Request["blogid"].ToString();
            return blogdb.EvalBlog(username, Int32.Parse(blogid), evaluation);
        }
        public string deleteevaluation()
        {
            string eid = Request["eid"].ToString();
            return blogdb.DeleteEvaluation(Int32.Parse(eid));
            //return eid;
        }

        public string blogcreate()
        {
            //username: $("#hname").val(), title: $("#title").val(), content: $("#content").val(), category: $("#category").val() 
            string username = Request["username"].ToString();
            string title = Request["title"].ToString();
            //  String q = Server.HtmlEncode(Request["val1"].ToString());
            String content = Server.HtmlEncode(Request["content"].ToString());
            string category = Request["category"].ToString();
            string authority = Request["authority"].ToString();

            BlogDB blogdb = new BlogDB();
            if (blogdb.CreateBlog(username, title, content, category, authority))
            {
                return "true";
            }
            else
            {
                return "false";
            }


        }
        public string updateblog()
        {
            string blogid = Request["blogid"].ToString();
            string title = Request["title"].ToString();
            //  String q = Server.HtmlEncode(Request["val1"].ToString());
            String content = Server.HtmlEncode(Request["content"].ToString());
            string category = Request["category"].ToString();
            string authority = Request["authority"].ToString(); BlogDB blogdb = new BlogDB();
            return blogdb.ChangeBlog(title, content, category, authority, Int32.Parse(blogid));
       
        }
        public string collectblog()
        {
            string blogid = Request["blogid"].ToString();
            if (HttpContext.User.Identity.Name == "")
                return "未登录";
            else
                return member.CollectBlog(HttpContext.User.Identity.Name, Int32.Parse(blogid));
            
        }
        public string discollectblog()
        {
            string blogid = Request["blogid"].ToString();
            return member.DiscollectBlog(HttpContext.User.Identity.Name, Int32.Parse(blogid));
        }
        public string givegood()
        {
            string blogid = Request["blogid"].ToString();
            if (HttpContext.User.Identity.Name == "")
                return "请登陆";
            else
                return blogdb.GiveGood(HttpContext.User.Identity.Name, Int32.Parse(blogid));
        }
        public string cancelgivegood()
        {
            string blogid = Request["blogid"].ToString();
            return blogdb.DeleteGood(HttpContext.User.Identity.Name, Int32.Parse(blogid));
            
        }

        public string giviemoney()
        {
            string username = Request["username"].ToString();
            string blogid = Request["blogid"].ToString();
            string money = Request["money"].ToString();
            try
            {
                if (HttpContext.User.Identity.Name == "")
                {
                    return "请先登录";
                }
                return member.GiveMoney(HttpContext.User.Identity.Name, username, Int32.Parse(money), Int32.Parse(blogid));
            }
            catch (Exception e)
            {
                return "请输入有效的数字";
            }
            
        }
        public string deleteblog()
        {
            string blogid = Request["blogid"].ToString();
            return blogdb.DeleteBlog(Int32.Parse(blogid));
        }

    }
}
