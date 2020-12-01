using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DevIncubator.Models;

namespace DevIncubator.DbContext
{
    public class MyDbContext : System.Data.Entity.DbContext
    {
        public MyDbContext() : base("mydb")
        { }
            public DbSet<UserData> UserDatas { get; set; }

            public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>()
                .HasRequired(c => c.Chart)
                .WithMany(s => s.Points)
                .HasForeignKey(d => d.ChartId);
                
        }
    }
}
