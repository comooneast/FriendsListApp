using FriendsListApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsListApp.Data
{
    public class FriendsListContext:DbContext
    {
        public FriendsListContext(DbContextOptions<FriendsListContext> options) : base(options)
        {
        }

        public DbSet<Relation> Relations { get; set; }
        public DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relation>().ToTable("Relation");
            modelBuilder.Entity<Friend>().ToTable("Friend");
        }
    }

    
}
