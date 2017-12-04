using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HFWebsiteA7.Models
{
    public class HFWebsiteA7Context : DbContext
    {
          
        public HFWebsiteA7Context() : base("name=hfDB")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<DinnerSession>().ToTable("DinnerSessions");
            modelBuilder.Entity<Concert>().ToTable("Concerts");
        }

        public DbSet<HFWebsiteA7.Models.Band> Bands { get; set; }
        public DbSet<HFWebsiteA7.Models.Day> Days { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<HFWebsiteA7.Models.Restaurant> Restaurants { get; set; }
        public System.Data.Entity.DbSet<HFWebsiteA7.Models.RestaurantFoodType> RestaurantFoodType { get; set; }
        public System.Data.Entity.DbSet<HFWebsiteA7.Models.DinnerSession> DinnerSessions { get; set; }
        public System.Data.Entity.DbSet<HFWebsiteA7.Models.Concert> Concerts { get; set; }
        public DbSet<HFWebsiteA7.Models.FoodType> FoodTypes { get; set; }
        public System.Data.Entity.DbSet<HFWebsiteA7.Models.Event> Events { get; set; }
    }
}
