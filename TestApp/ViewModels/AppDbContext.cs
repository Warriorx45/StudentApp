using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class AppDbContext:DbContext
    {
        public DbSet<DataModel> dataModels { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string p = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(p, "Database.db");
            optionsBuilder.UseSqlite($"FileName={path}");

        }
    }
}
