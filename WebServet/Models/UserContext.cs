using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServet.Models
{
    public class UserContext : DbContext
    {
        //public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public UserContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(@"server=120.78.200.228;uid=root;pwd=B,i123456;
                    port=3306;database=net;sslmode=Preferred;");
            }
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // 获取appsettings.json配置信息
        //    var config = new ConfigurationBuilder()
        //                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
        //                    .AddJsonFile("appsettings.json")
        //                    .Build();
        //    // 获取数据库连接字符串
        //    string conn = config.GetConnectionString("conn");
        //    Console.WriteLine(conn);
        //    //连接数据库
        //    optionsBuilder.UseSqlServer(conn);
        //}

        public DbSet<User> user { get; set; }

        public DbSet<post> posts { set; get; }

        public DbSet<likes> likes { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<likes>()
                .HasKey(c => new { c.user_id, c.post_id });
        }

        public DbSet<comment> comment { set; get; }

        public DbSet<relation> relation { set; get; }
    }
}
