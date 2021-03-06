﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBlogSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BlogSystemEntities : DbContext
    {
        public BlogSystemEntities()
            : base("name=BlogSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Collect> Collect { get; set; }
        public DbSet<Evaluation> Evaluation { get; set; }
        public DbSet<Focus> Focus { get; set; }
        public DbSet<GiveGood> GiveGood { get; set; }
        public DbSet<LeaveMessage> LeaveMessage { get; set; }
        public DbSet<Notice> Notice { get; set; }
        public DbSet<RecommandBlog> RecommandBlog { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserCategory> UserCategory { get; set; }
        public DbSet<UserMessage> UserMessage { get; set; }
    }
}
