using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ShowTime.Models;

namespace ShowTime.DAL
{
    public class ShowDAL : DbContext
    {
        public DbSet<Show> Shows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Show>().ToTable("Show");
        }
    }
}