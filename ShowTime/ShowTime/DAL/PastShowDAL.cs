using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShowTime.Models;


namespace ShowTime.DAL
{
    public class PastShowDAL : DbContext
    {
        public DbSet<PastShow> PastShows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PastShow>().ToTable("PastShow");
        }
    }
}