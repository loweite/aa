using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using  WebBlogSystem.Models;
using WebBlogSystem.Security;
namespace WebBlogSystem.Models
{
    public class BlogDB
    {
        BlogSystemEntities db = new BlogSystemEntities();
        Time time = new Time();
        public bool CreateBlog(string username, string title, string content, string category, string authority)
        {
            MemberShip member = new MemberShip();
            try
            {
                var blog = new Blog
                {
                    title = title,
                    content = content,
                    category = category,
                    userid = member.getUserIdByName(username),
                    updatetime = time.getCurrentDateTime(),
                    authority=authority,
                     costnum=0
                };
                db.Blog.Add(blog);
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
            return false;
            }
        }
        public string DeleteBlog(int blogid)
        {
            Blog blog = db.Blog.First(c => c.blogid == blogid);
            db.Blog.Remove(blog);
            db.SaveChanges();
            return "删除成功";
        }
        public string ChangeBlog(string title, string content, string category, string authority, int blogid)
        {
            var q = from c in db.Blog where c.blogid == blogid select c;
            foreach (var a in q)
            {
                a.title = title;
                a.content = content;
                a.authority = authority;
                a.category = category;
                a.updatetime = time.getCurrentDateTime();
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return "true";
        }
        public string SetBlogAuthority(int blogid, string authority)
        {
            var q = from c in db.Blog where c.blogid == blogid select c;
            foreach (var a in q)
            {
                a.authority = authority;
            }
            db.SaveChanges();
            return "true";
        }
        public string ChangeBlogCategory(string category, int blogid)
        {
            var q = from c in db.Blog where c.blogid == blogid select c;
            foreach (var a in q)
            {
                a.category = category;
            }
            db.SaveChanges();
            return "true";
        }
        public string CreateCategory(string username, string categoryname)
        {

            MemberShip member = new MemberShip();
            int userid=member.getUserIdByName(username);
            var usercategory = new UserCategory
            {
                categoryname = categoryname,
                userid = userid
            };
            db.UserCategory.Add(usercategory);
            db.SaveChanges();
            return "true";
        }
        public string DeleteCategory(string username, string categoryname)
        {
            MemberShip member = new MemberShip();
            int userid=member.getUserIdByName(username);
            UserCategory usercategory = db.UserCategory.First(c => c.categoryname == categoryname && c.userid == userid);
            db.UserCategory.Remove(usercategory);
            db.SaveChanges();
            return "true";
        }
        public bool IsCategoryExist(string categoryname,string username)
        {
            MemberShip member = new MemberShip();
            int userid=member.getUserIdByName(username);
            var q = from c in db.UserCategory where c.categoryname == categoryname && c.userid == userid select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }
        public List<Blog> getBlogByUsername(string username)
        {
            MemberShip member = new MemberShip();
            List<Blog>  bloglist=new List<Blog>();
            Blog blog = null;
            int userid=member.getUserIdByName(username);
            var q = from c in db.Blog where c.userid == userid select c;
            foreach (var a in q)
            {
                blog = new Blog();
                blog.blogid = a.blogid;
                blog.category = a.category;
                blog.content = a.content;
                blog.title = a.title;
                blog.updatetime = a.updatetime;
                blog.userid = a.userid;
                blog.costnum = a.costnum;
                bloglist.Add(blog);
            }
            return bloglist;
        }
        public Blog GetBlogById(int blogid)
        {
            var q = from c in db.Blog where c.blogid == blogid select c;
            Blog blog = new Blog();
            foreach (var a in q)
            { 
                blog.blogid=a.blogid;
                blog.authority=a.authority;
                blog.category=a.category;
                blog.content=a.content;
                blog.title=a.title;
                blog.updatetime=a.updatetime;
                blog.userid=a.userid;
                blog.costnum = a.costnum;
            }
            return blog;
                  

        }
        public string GiveGood(string username, int blogid)
        {
            MemberShip member = new MemberShip();
            int userid = member.getUserIdByName(username);
            var givegood = new GiveGood
            {
                blogid = blogid,
                time = time.getCurrentDateTime(),
                userid = userid
            };
            db.GiveGood.Add(givegood);
            db.SaveChanges();

            return "点赞成功";
        }
        public bool IsGiveGood(string username, int blogid)
        {
            MemberShip member = new MemberShip();
            int userid = member.getUserIdByName(username);
            var q = from c in db.GiveGood where c.blogid == blogid && c.userid == userid select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
               
        }
        public string DeleteGood(string username, int blogid)
        {
            MemberShip member = new MemberShip();
            int userid = member.getUserIdByName(username);
            GiveGood givegood = db.GiveGood.First(c=>c.userid==userid&&c.blogid==blogid);
            db.GiveGood.Remove(givegood);
            db.SaveChanges();
            return "已取消点赞";
        }
        public int GetGoodNum(int blogid)
        {
            var q = from c in db.Focus where blogid == blogid select c;
            return q.Count();
        }
        public List<Blog> SearchBlog(string value)
        {
            List<Blog> bloglist = new List<Blog>();
            var q = from c in db.Blog where c.title.Contains(value) || c.content.Contains(value) select c;
            Blog blog = null;
            foreach (var a in q)
            {
                blog = new Blog();
                blog.authority = a.authority;
                blog.blogid = a.blogid;
                blog.category = a.category;
                blog.content = a.content;
                blog.title = a.title;
                blog.updatetime = a.updatetime;
                blog.userid = a.userid;
                bloglist.Add(blog);
            }
            return bloglist;
        }
        public string EvalBlog(string username, int blogid, string content)
        {
            MemberShip member = new MemberShip();
            int userid = member.getUserIdByName(username);
            var evaluation = new Evaluation
            {
                blogid = blogid,
                isreaded = 0,
                userid = userid,
                time = time.getCurrentDateTime(),
                content=content
            };
            db.Evaluation.Add(evaluation);
            db.SaveChanges();
            return "评论成功";
        }
        public string DeleteEvaluation(int evaluationid)
        {
            Evaluation e = db.Evaluation.First(c=>c.Id==evaluationid);
            db.Evaluation.Remove(e);
            db.SaveChanges();
            return "删除成功";
        }
        public string ReadEvaluation(int evaluationid)
        {
            var q = from c in db.Evaluation where c.Id == evaluationid select c;
            foreach (var a in q)
            {
                a.isreaded = 1;
            }
            db.SaveChanges();
            return "true";
        }
        public int GetEvaluationuNum(int blogid)
        {
            var q = from c in db.Evaluation where blogid == blogid select c;
            return q.Count();
        }
        public List<Evaluation> GetBlogEvaluation(int blogid)
        {
            List<Evaluation> evalist = new List<Evaluation>();
            var q = from c in db.Evaluation where c.blogid == blogid orderby c.time descending select c;
            Evaluation e = null;
            foreach (var a in q)
            {
                e = new Evaluation();
                e.blogid = a.blogid;
                e.Id = a.Id;
                e.isreaded = a.isreaded;
                e.time = a.time;
                e.userid = a.userid;
                e.content = a.content;
                e.Id = a.Id;
                evalist.Add(e);
            }
            return evalist;
        }
        public int GetCollectNum(int blogid)
        {
            var q = from c in db.Collect where c.blogid == blogid select c;
            return q.Count();
        }
        public List<int> GetBlogCollectAndGoodNum(int blogid)
        {
            List<int> bloginfo = new List<int>();
            var q = from c in db.Collect where c.blogid == blogid select c;
            var p = from c in db.GiveGood where c.blogid == blogid select c;
            bloginfo.Add(q.Count());
            bloginfo.Add(p.Count());
            return bloginfo;
            
        }
        public List<Blog> GetRecommandBlog()
        {
            List<Blog> bloglist = new List<Blog>();
            var q = (from c in db.RecommandBlog join b in db.Blog on c.blogid equals b.blogid select b);
            Blog blog = null;
            foreach (var a in q)
            {
                blog = new Blog();
                blog.authority = a.authority;
                blog.blogid = a.blogid;
                blog.category = a.category;
                blog.content = a.content;
                blog.costnum = a.costnum;
                blog.title = a.title;
                blog.updatetime = a.updatetime;
                blog.userid = a.userid;
                bloglist.Add(blog);
            }
            return bloglist;
        }
        public List<Blog> getAllBlog()
        {
            List<Blog> bloglist = new List<Blog>();
            Blog blog = null;
            var q = from c in db.Blog select c;
            foreach (var a in q)
            {
                blog = new Blog();
                blog.userid = a.userid;
                blog.blogid = a.blogid;
                blog.category = a.category;
                blog.content = a.content;
                blog.title = a.title;
                blog.updatetime = a.updatetime;
                blog.userid = a.userid;
                blog.costnum = a.costnum;
                bloglist.Add(blog);
            }
            return bloglist;
        }
    }
}