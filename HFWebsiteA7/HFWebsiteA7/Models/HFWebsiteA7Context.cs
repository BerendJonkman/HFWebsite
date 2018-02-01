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
            // modelBuilder.Entity<AddConcert>().ToTable("Concerts");
            modelBuilder.Entity<RestaurantFoodType>().ToTable("RestaurantFoodType");
            modelBuilder.Entity<PassPartoutOrder>().ToTable("PassPartoutOrder");
        }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantFoodType> RestaurantFoodType { get; set; }
        public DbSet<DinnerSession> DinnerSessions { get; set; }
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<PassPartoutOrder> PassPartoutOrder { get; set; }
        public DbSet<PassPartoutType> PassPartoutTypes { get; set; }
       // public DbSet<AddConcert> AddConcerts { get; set; }
    }
}
