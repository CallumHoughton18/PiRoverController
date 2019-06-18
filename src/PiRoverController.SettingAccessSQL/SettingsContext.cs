using Microsoft.EntityFrameworkCore;
using PiRoverController.Common;
using PiRoverController.Common.Helper_Classes;
using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PiRoverController.SettingAccessSQL
{
    sealed class SettingsContext : DbContext
    {
        public SettingsContext(DbContextOptions<SettingsContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().HasData(DataHelper.GenerateDefaultSettingsData().ToArray());
        }

        public DbSet<Setting> Settings { get; set; }
    }
}
