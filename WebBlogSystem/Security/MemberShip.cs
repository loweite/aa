using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebBlogSystem.Models;
namespace WebBlogSystem.Security
{
    public class MemberShip
    {
        BlogSystemEntities db = new BlogSystemEntities();
        Time time = new Time();
        public string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public  string ChangePassword(string username, string oldPassword, string newPassword)
        {
            var q = from c in db.User where c.name == username&&c.password==oldPassword select c;
            if (q.Count() == 0)
            {
                return "false";
            }
            else
            {
                foreach (var a in q)
                {
                    a.password = newPassword;
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

                return "true";
            }
        }

        public bool ChangePayPassword(string username, string oldPassword, string newPassword)
        {
            var q = from c in db.User where c.name == username && c.paypassword == oldPassword select c;
            if (q.Count() == 0)
            {
                return false;
            }
            else
            {
                foreach (var a in q)
                {
                    a.paypassword = newPassword;
                }
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

                return true;
            }
        }
        public  bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public  String CreateUser(string username, string password,string paypass ,string email, string phone ,string tip,string instruction,string img)
        {

            try {
                    var q=from c in db.User where c.name==username select new{c.userid};
                    if (q.Count() > 0)
                    {
                        return "此用户名已使用";
                    }else if(IsEmaiUse(email))
                    {
                        return "邮箱已使用";

                    }else if(IsPhoneUse(phone))
                    {
                        return "电话已使用";
                        
                    }
                    else {

                            var newuser = new User
                            {
                                name = username,
                                password = password,
                                paypassword = paypass,
                                email = email,
                                phone = phone,
                                tip = tip,
                                instruction = instruction,
                                image=img,
                                 balance=1000
                            };
                            db.User.Add(newuser);
                            db.SaveChanges();
                            return "注册成功";
                    }
            }
            catch (Exception e)
            {
                return "error";
            }
           
        }
        public  bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public  bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public  bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public  MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public  MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public  MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public  int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public  string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public  MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public  MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public  string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public  int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public  int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public  int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public  int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public  MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public  string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public  bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public  bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public  string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public  bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public  string UpdateUser(User user)
        {
            var q = from c in db.User where c.name == user.name select c;
            foreach (var a in q)
            {
                a.instruction = user.instruction;
                a.password = user.password;
                a.paypassword = user.paypassword;
                a.phone = user.phone;
                a.tip = user.tip;
                a.balance = user.balance;
                a.email = user.email;
                a.image = user.image;
            }
            db.SaveChanges();
            return "true";
        }

        public  bool ValidateUser(string username, string password)
        {
            var q = (from c in db.User where c.name == username && c.password == password select c.name);
            if (q.Count() > 0)
                return true;
            else
            {
                return false;
            }
           // return true;
        }
        public bool ValidateAdmin(string username, string password)
        {

            var q = (from c in db.Admin where c.name == username && c.password== password select c.name);
            if (q.Count() > 0)
                return true;
            else
            {
                return false;
            }
        }
        public bool IsPhoneUse(string phone)
        {
            var q = from c in db.User where c.phone == phone select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }
        public bool IsEmaiUse(string email)
        {
            var q = from c in db.User where c.email == email select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }
        public int getUserIdByName(string username)
        {
            var q = (from c in db.User where c.name == username  select new {c.userid});
          foreach(var a in q)
          {
          return a.userid;
          }
            return 0;
        }

        public string getUserNameById(int  userid)
        {
            var q = (from c in db.User where c.userid == userid select new { c.name });
            foreach (var a in q)
            {
                return a.name.Trim();
            }
            return "";
        }
        public User GetUserByid(int userid)
        {
            User user = new User();
            var q = from c in db.User where c.userid == userid select c;
            foreach (var a in q)
            {
                user.userid = a.userid;
                user.tip = a.tip.Trim();
                user.phone = a.phone.Trim();
                user.name = a.name.Trim();
                user.password = a.password.Trim();
                user.paypassword = a.paypassword.Trim();
                user.instruction = a.instruction.Trim();
                user.balance = a.balance;
                user.email = a.email.Trim();
                user.image = a.image;
            }
            return user;
        }
        public string GetUserImage(string username)
        { 
            int userid=getUserIdByName(username);
            var q = from c in db.User where c.userid == userid select new{ c.image };
            foreach (var a in q)
            {
                return a.image;
            }
            return "";
        }
        public string AddFocus(string username1, string username2,String anothername)
        {

            var focus = new Focus
            {
                  anothername=anothername,
                  userid1=getUserIdByName(username1),
                  userid2 = getUserIdByName(username2),
            };
            db.Focus.Add(focus);
            db.SaveChanges();
            return "关注成功";

       }
        public bool IsFocus(string username1, string username2)
        {

            int userid1 = getUserIdByName(username1);
            int userid2 = getUserIdByName(username2);
            var q = from c in db.Focus where c.userid1 == userid1 && c.userid2 == userid2 select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }
        public string DeleteFocus(string username1, string username2)
        {
            int userid1=getUserIdByName(username1);
            int userid2=getUserIdByName(username2);
            //Focus focus = db.Focus.FirstOrDefault(c => c.userid1 == userid1 && c.userid2 == userid2);
            var q = from c in db.Focus where c.userid1 == userid1 && c.userid2 == userid2 select c;
            foreach (var a in q)
            {
                db.Focus.Remove(a);
            }
            db.SaveChanges();
            return "取消关注成功";
        }
        public List<Focus> GetUserFocus(string username1)
        {
            List<Focus> userfocuselist = new List<Focus>();
            int userid1 = getUserIdByName(username1);
            var q = from c in db.Focus where c.userid1 == userid1 select c;
            Focus focus = null;
            foreach (var a in q)
            {
                focus = new Focus();
                focus.userid1 = userid1;
                focus.userid2 = a.userid2;
                focus.anothername = a.anothername;
                userfocuselist.Add(focus);
            }
            return userfocuselist;
        }

        public int GetUserFocusNum(string username)
        {
            int userid = getUserIdByName(username);
            var q = from c in db.Focus where c.userid1 == userid select c;
            return q.Count();
        }
        public int GetfollowerNum(string username)
        {
            int userid = getUserIdByName(username);
            var q = from c in db.Focus where c.userid2 == userid select c;
            return q.Count();
        }
        public List<string> GetUserFocused(string username2)
        {
            List<string> usernamelist = new List<string>();
            int userid2 = getUserIdByName(username2);
            var q = from c in db.Focus where c.userid2 == userid2 select new { c.userid1 };
            foreach (var a in q)
            {
                string username = getUserNameById(a.userid1);
                usernamelist.Add(username);
            }
            return usernamelist;
        }
        public string ChangeFocusName(string username1, string username2, string anothername)
        {

            int userid1 = getUserIdByName(username1);
            int userid2 = getUserIdByName(username2);
            var q = from c in db.Focus where c.userid1 == userid1 && c.userid2 == userid2 select c;

            foreach (var a in q)
            {
                a.anothername = anothername;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return "true";
        }
        public string CollectBlog(string username, int blogid)
        {
            int userid = getUserIdByName(username);
            var collect = new Collect
            {
                blogid = blogid,
                userid = userid
            };
            db.Collect.Add(collect);
            db.SaveChanges();
            return "收藏成功";
        }
        public bool IsCollectBlog(string username, int blogid)
        {
            int userid = getUserIdByName(username);
            var q = from c in db.Collect where c.blogid == blogid && c.userid == userid select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
            
        }
        public string DiscollectBlog(string username, int blogid)
        {
            int userid = getUserIdByName(username);
            var q = from c in db.Collect where c.userid == userid && c.blogid == blogid select c;
            foreach (var a in q)
            {
                db.Collect.Remove(a);
            }
            db.SaveChanges();
            return "已取消收藏";
        }
        public List<Blog> GetUserCollect(string username)
        {
            int userid = getUserIdByName(username);
            List<Blog> bloglist = new List<Blog>();
            Blog blog=null;
            var q = from c in db.Collect join b in db.Blog on c.userid equals b.userid where c.userid == userid select b;
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
        public string AddLeaveMessage(string  username1,string username2,string content)
        { 
            int userid1= getUserIdByName(username1);
            int userid2 = getUserIdByName(username2);
            var leavemessage = new LeaveMessage
            {
                content = content,
                time = time.getCurrentDateTime(),
                userid1 = userid1,
                userid2 = userid2,
                isreaded = 0
            };
            db.LeaveMessage.Add(leavemessage);
            db.SaveChanges();
            return "留言成功";
        }
        public string ReadLeaveMeassage(int  messageid)
        {
            var q = from c in db.LeaveMessage where c.Id==messageid select c;
            foreach (var a in q)
            {
                a.isreaded = 1;
            }
            db.SaveChanges();
            return "true";
        }
        public string DeleteLeaveMessage(int messageid)
        {
            var q =from c in db.LeaveMessage where c.Id==messageid select c;
            foreach(var a in q)
            {
                db.LeaveMessage.Remove(a);
            }
            db.SaveChanges();
            return "true";
        }
        public int GetLeaveMessageNum(string username2)
        {
            int userid2 = getUserIdByName(username2);
            var q = from c in db.LeaveMessage where c.userid2 == userid2 select c;
            return q.Count();
        }
        public List<LeaveMessage> GetLeaveMessage(string username)
        {
            int userid2=getUserIdByName(username);
            List<LeaveMessage> leavemessagelist = new List<LeaveMessage>();
            var q = from c in db.LeaveMessage where c.userid2 == userid2 select c;
            LeaveMessage l = null;
            foreach (var a in q)
            {
                l = new LeaveMessage();
                l.Id = a.Id;
                l.content = a.content;
                l.isreaded = a.isreaded;
                l.time = a.time;
                l.userid1 = a.userid1;
                l.userid2 = a.userid2;
                leavemessagelist.Add(l);
            }
            return leavemessagelist;
        }
        public string AddChat(string username1, string username2, string content)
        { 
            int userid1= getUserIdByName(username1);
            int userid2 = getUserIdByName(username2);
            var chatmessage=new Chat
            {
                 content=content,
                  userid1=userid1,
                  userid2=userid2,
                   time=time.getCurrentDateTime()
            };
            db.Chat.Add(chatmessage);
            db.SaveChanges();
            return "评论成功";
        }
        public int GetNotReadChat(string username)
        {
            int userid2 = getUserIdByName(username);
            var q = from c in db.Chat where c.userid2 == userid2 select c;
            return q.Count();
        }
        public List<string> GetChatUser(string username)
        { 
             int userid=getUserIdByName(username);
            List<string> namelist=new List<string>();
            var q = (from c in db.Chat where c.userid1 == userid  select c.userid2 ).Distinct();
            foreach (var a in q)
            {
                namelist.Add(getUserNameById(a));
            }
            var p = (from c in db.Chat where c.userid2 == userid select c.userid1).Distinct();
            foreach (var a in p)
            {
                if (!namelist.Contains(getUserNameById(a)))
                {
                    namelist.Add(getUserNameById(a));
                }
            }
            return namelist;
            
        }
        public List<Chat> GetChatWith(string name)
        {
            List<Chat> chatlist = new List<Chat>();
            string username = HttpContext.Current.User.Identity.Name;
            int userid1 = getUserIdByName(name);
            int userid2 = getUserIdByName(username);
            var q = from c in db.Chat where (c.userid1 == userid1 && c.userid2 == userid2) ||( c.userid1 == userid2 && c.userid2 == userid1 )select c;
            Chat chat = null;
            foreach (var a in q)
            {
                chat = new Chat();
                chat.Id = a.Id;
                chat.isread = a.isread;
                chat.time = a.time;
                chat.userid1 = a.userid1;
                chat.userid2 = a.userid2;
                chat.content = a.content;
                chatlist.Add(chat);
            }
            return chatlist;
        }
        public List<Chat> GetAllUserChat(string username)
        {
            List<Chat> chatlist = new List<Chat>();
            int userid = getUserIdByName(username);
            Chat chat = null;
            var q = from c in db.Chat where c.userid2 == userid || c.userid1 == userid select c;
            foreach (var a in q)
            {
                chat = new Chat();
                chat.userid1 = a.userid1;
                chat.userid2 = a.userid2;
                chat.time = a.time;
                chat.Id = a.Id;
                chat.content = a.content;
                chat.isread = a.isread;
                chatlist.Add(chat);
            }
            return chatlist;
        }

        public List<Chat> GetUserSendChat(string username)
        {
            List<Chat> chatlist = new List<Chat>();
            int userid = getUserIdByName(username);
            Chat chat = null;
            var q = from c in db.Chat where c.userid1 == userid select c;
            foreach (var a in q)
            {
                chat = new Chat();
                chat.userid1 = a.userid1;
                chat.userid2 = a.userid2;
                chat.time = a.time;
                chat.Id = a.Id;
                chat.content = a.content;
                chat.isread = a.isread;
                chatlist.Add(chat);
            }
            return chatlist;
        }
        public List<Chat> GetUserGetChat(string username)
        {
            List<Chat> chatlist = new List<Chat>();
            int userid = getUserIdByName(username);
            Chat chat = null;
            var q = from c in db.Chat where c.userid2 == userid select c;
            foreach (var a in q)
            {
                chat = new Chat();
                chat.userid1 = a.userid1;
                chat.userid2 = a.userid2;
                chat.time = a.time;
                chat.Id = a.Id;
                chat.content = a.content;
                chat.isread = a.isread;
                chatlist.Add(chat);
            }
            return chatlist;
        }
        public string ReadChat(string username)
        { 
            int userid2=getUserIdByName(username);
            var q = from c in db.Chat where c.userid2 == userid2 && c.time.CompareTo(DateTime.Now)==-1 select c;
            foreach (var a in q)
            {
                a.isread = 1;
            }
            db.SaveChanges();
            return "true";
        }
        public string DeleteChatWith(string username)
        {
            int userid1 = getUserIdByName(username);
            int userid2 = getUserIdByName(HttpContext.Current.User.Identity.Name);
            var q = from c in db.Chat where (c.userid1 == userid1 && c.userid2 == userid2) || (c.userid1 == userid2 && c.userid2 == userid1) select c;
            foreach (var a in q)
            {
                db.Chat.Remove(a);
            }
            db.SaveChanges();
            return "已清空私信";
        }
        public string DeleteChat(int chatid)
        {
            var q = from c in db.Chat where c.Id == chatid select c;
            foreach (var a in q)
            {
                db.Chat.Remove(a);
            }
            db.SaveChanges();
            return "true";
        }
        public string GetCurrentUsername()
        {
            string username = HttpContext.Current.User.Identity.Name;
            return username;
        }
        public int GetUserBlogNum(string username)
        {
            int userid = getUserIdByName(username);
            var q = from c in db.Blog where userid == userid select c;
            return q.Count();
        }
        public void SendMessage(string username,string message)
        {
            int userid = getUserIdByName(username);
            var userMessage = new UserMessage
            {
                userid = userid,
                message = message,
                time=DateTime.Now
            };
            db.UserMessage.Add(userMessage);
            db.SaveChanges();
        }
        public void DeleteGetMessage(string username)
        { 
            int userid=getUserIdByName(username);
            var q = from c in db.UserMessage where c.userid == userid select c;
            foreach (var a in q)
            {
                var x = db.UserMessage.First(c => c.time == a.time);
                db.UserMessage.Remove(x);
            }
            db.SaveChanges();
        }
        public List<UserMessage> GetUserMessage(string username)
        {
            List<UserMessage> messagelist = new List<UserMessage>();
            int userid = getUserIdByName(username);
            var q = from c in db.UserMessage where c.userid == userid select c;
            UserMessage usermessage = null;
            foreach (var a in q)
            {
                usermessage = new UserMessage();
                usermessage.Id = a.Id;
                usermessage.message = a.message;
                usermessage.userid = a.userid;
                usermessage.time = a.time;
                messagelist.Add(usermessage);
            }
            return messagelist;
        }
        public string GiveMoney(string username1, string username2, int money,int blogid)
        {
            int userid1 = getUserIdByName(username1);
            int userid2 = getUserIdByName(username2);
            var q = from c in db.User where c.userid == userid1 select c;
            foreach (var a in q)
            {
                a.balance -= money;
            }
            var p = from c in db.User where c.userid == userid2 select c;
            foreach (var a in p)
            {
                a.balance += money;
            }
            var m = from c in db.Blog where c.blogid == blogid select c;
            foreach (var a in m)
            {
                a.costnum+=1;
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            return "打赏成功";
        }
        public string AddNotice(string username, string content)
        { 
            int userid=getUserIdByName(username);
            var notice = new Notice
            {
                content = content,
                time = DateTime.Now,
                userid = userid
            };
            db.Notice.Add(notice);
            db.SaveChanges();
            return "添加成功";
        }
        public string DeleteNotice(int noticeid)
        {
            var q = from c in db.Notice where c.Id == noticeid select c;
            foreach (var a in q)
            {
                db.Notice.Remove(a);
            }
            db.SaveChanges();
            return "true";
        }
        public List<Notice> GetNoticeByName(string username)
        {
            int userid = getUserIdByName(username);
            List<Notice> list = new List<Notice>();
            var q = from c in db.Notice where c.userid == userid orderby c.time descending select c;
            Notice notice = null;
            foreach (var a in q)
            {
                notice = new Notice();
                notice.Id = a.Id;
                notice.time = a.time;
                notice.userid = a.userid;
                notice.content = a.content;
                list.Add(notice);
            }
            return list;
        }
        public List<Blog> GetUserCollecedtBlog(string username)
        {

            List<Blog> bloglist = new List<Blog>();
            int userid=getUserIdByName(username);
            var q= from c in db.Collect join b in db.Blog on c.blogid equals b.blogid where c.userid==userid select b;
            Blog blog = null;
            foreach (var a in q)
            {
                blog = new Blog();
                blog.blogid = a.blogid;
                blog.authority = a.authority;
                blog.category = a.category;
                blog.content = a.content;
                blog.costnum = a.costnum;
                blog.title = a.title;
                blog.updatetime = a.updatetime;
                bloglist.Add(blog);
            }
            return bloglist;

        }
        public int GetUserCollectBlogNum(string username)
        {
            int userid = getUserIdByName(username);
            var q = from c in db.Collect join b in db.Blog on c.blogid equals b.blogid where c.userid == userid select b;
            return q.Count();
        }


        public bool ValidateRecommend(int adminid, int blogid)
        {
            var q = from c in db.RecommandBlog where c.adminid == adminid && c.blogid == blogid select c;
            if (q.Count() > 0)
            {
                return true;
            }
            else
                return false;
        }


        public string AddRecommend(int adminid, int blogid)
        {
            RecommandBlog rb = new RecommandBlog();
            rb.adminid = adminid;
            rb.blogid = blogid;
            rb.time = System.DateTime.Now;
            db.RecommandBlog.Add(rb);
            db.SaveChanges();
            return "true";
        }


        public string DeleteUser(int userid)
        {
            var qu = from c in db.User where c.userid == userid select c;
            foreach (var a in qu)
            {
                db.User.Remove(a);
            }
            db.SaveChanges();

            var qc1 = from c in db.Chat where c.userid1 == userid select c;
            if (qc1.Count() > 0)
            {
                foreach (var a in qc1)
                {
                    db.Chat.Remove(a);
                }
                db.SaveChanges();
            }

            var qc2 = from c in db.Chat where c.userid2 == userid select c;
            if (qc2.Count() > 0)
            {
                foreach (var a in qc2)
                {
                    db.Chat.Remove(a);
                }
                db.SaveChanges();
            }

            var qc3 = from c in db.Collect where c.userid == userid select c;
            if (qc3.Count() > 0)
            {
                foreach (var a in qc3)
                {
                    db.Collect.Remove(a);
                }
                db.SaveChanges();
            }

            var qe = from c in db.Evaluation where c.userid == userid select c;
            if (qe.Count() > 0)
            {
                foreach (var a in qe)
                {
                    db.Evaluation.Remove(a);
                }
                db.SaveChanges();
            }

            var qf1 = from c in db.Focus where c.userid1 == userid select c;
            if (qf1.Count() > 0)
            {
                foreach (var a in qf1)
                {
                    db.Focus.Remove(a);
                }
                db.SaveChanges();
            }

            var qf2 = from c in db.Focus where c.userid2 == userid select c;
            if (qf2.Count() > 0)
            {
                foreach (var a in qf2)
                {
                    db.Focus.Remove(a);
                }
                db.SaveChanges();
            }

            var qg = from c in db.GiveGood where c.userid == userid select c;
            if (qg.Count() > 0)
            {
                foreach (var a in qg)
                {
                    db.GiveGood.Remove(a);
                }
                db.SaveChanges();
            }

            var ql1 = from c in db.LeaveMessage where c.userid1 == userid select c;
            if (ql1.Count() > 0)
            {
                foreach (var a in ql1)
                {
                    db.LeaveMessage.Remove(a);
                }
                db.SaveChanges();
            }
            var ql2 = from c in db.LeaveMessage where c.userid2 == userid select c;
            if (ql1.Count() > 0)
            {
                foreach (var a in ql2)
                {
                    db.LeaveMessage.Remove(a);
                }
                db.SaveChanges();
            }

            var qn = from c in db.Notice where c.userid == userid select c;
            if (qn.Count() > 0)
            {
                foreach (var a in qn)
                {
                    db.Notice.Remove(a);
                }
                db.SaveChanges();
            }

            var qb = from c in db.Blog where c.userid == userid select c;
            if (qb.Count() > 0)
            {
                foreach (var a in qb)
                {
                    db.Blog.Remove(a);
                }
                db.SaveChanges();
            }

            var quc = from c in db.UserCategory where c.userid == userid select c;
            if (quc.Count() > 0)
            {
                foreach (var a in quc)
                {
                    db.UserCategory.Remove(a);
                }
                db.SaveChanges();
            }

            var qum = from c in db.UserMessage where c.userid == userid select c;
            if (qum.Count() > 0)
            {
                foreach (var a in qum)
                {
                    db.UserMessage.Remove(a);
                }
                db.SaveChanges();
            }
            return "ture";
        }



        public List<User> getUserByUserName(string username)
        {
            MemberShip member = new MemberShip();
            List<User> userlist = new List<User>();
            User user = null;
            var q = from c in db.User where c.name.Contains(username) select c;
            foreach (var a in q)
            {
                user = new User();
                user.userid = a.userid;
                user.tip = a.tip;
                user.phone = a.phone;
                user.paypassword = a.paypassword;
                user.password = a.password;
                user.name = a.name;
                user.instruction = a.instruction;
                user.image = a.image;
                userlist.Add(user);
            }

            return userlist;
        }


        public List<User> GetAllUsers()
        {
            var q = from c in db.User select c;
            List<User> userlist = new List<User>();
            User user = null;
            foreach (var a in q)
            {
                user = new User();
                user.userid = a.userid;
                user.tip = a.tip;
                user.phone = a.phone;
                user.paypassword = a.paypassword;
                user.password = a.password;
                user.name = a.name;
                user.instruction = a.instruction;
                user.image = a.image;
                userlist.Add(user);
            }
            return userlist;
        }


        public bool ValidateUser1(string username)
        {
            var q = (from c in db.User where c.name == username select c.name);
            if (q.Count() > 0)
                return true;
            else
                return false;
        }


        public string DeleteBlog(int blogid)
        {
            var qb = from c in db.Blog where c.blogid == blogid select c;
            Blog blog = new Blog();
            foreach (var a in qb)
            {
                blog = a;
                db.Blog.Remove(blog);

            }
            db.SaveChanges();
            var qc = from c in db.Collect where c.blogid == blogid select c;
            if (qc.Count() > 0)
            {
                foreach (var a in qc)
                {
                    db.Collect.Remove(a);

                }
                db.SaveChanges();
            }

            var qe = from c in db.Evaluation where c.blogid == blogid select c;
            if (qe.Count() > 0)
            {
                foreach (var a in qe)
                {
                    db.Evaluation.Remove(a);

                }
                db.SaveChanges();
            }

            var qg = from c in db.GiveGood where c.blogid == blogid select c;
            if (qg.Count() > 0)
            {
                foreach (var a in qg)
                {
                    db.GiveGood.Remove(a);

                } db.SaveChanges();
            }

            var qr = from c in db.RecommandBlog where c.blogid == blogid select c;
            if (qr.Count() > 0)
            {
                foreach (var a in qr)
                {
                    db.RecommandBlog.Remove(a);

                } db.SaveChanges();
            }

            return "ture";
        }

        public List<Blog> getBlogByUsername(string username)
        {
            MemberShip member = new MemberShip();
            List<Blog> bloglist = new List<Blog>();
            Blog blog = null;
            int userid = member.getUserIdByName(username);
            var q = from c in db.Blog where c.userid == userid select c;
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

        public int getAdminIdByName(string username)
        {
            var q = (from c in db.Admin where c.name == username select new { c.adminid });
            foreach (var a in q)
            {
                return a.adminid;
            }
            return 0;
        }



    }
}