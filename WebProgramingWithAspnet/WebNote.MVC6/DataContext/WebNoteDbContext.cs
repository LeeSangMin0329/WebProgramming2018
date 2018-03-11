using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNote.MVC6.Models;

namespace WebNote.MVC6.DataContext
{
    public class WebNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }  // db table
        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=localhost;Database=WebNoteAspnet;User Id=sa;Password=testadmin;");
        }
    }
}
