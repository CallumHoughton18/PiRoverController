using Microsoft.EntityFrameworkCore;
using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PiRoverController.SettingAccessSQL
{
    sealed class SettingsContext : DbContext
    {
        public SettingsContext(DbContextOptions<SettingsContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Setting>().HasData(new Setting { Id = 1, SettingName = "Web Server IP", SettingValue = "http://192.168.0.22:8080/", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 2, SettingName = "Initialize GPIOs", SettingValue = "/RoverControls/initGPIO", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 3, SettingName = "Go Forwards Endpoint", SettingValue = "/RoverControls/forward", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 4, SettingName = "Go Backwards Endpoint", SettingValue = "/RoverControls/backward", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 5, SettingName = "Go Left Endpoint", SettingValue = "/RoverControls/left", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 6, SettingName = "Go Right Endpoint", SettingValue = "/RoverControls/right", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 7, SettingName = "Stop Left&Right Endpoint", SettingValue = "/RoverControls/stopLeftAndRight", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 8, SettingName = "Stop Forwards&Backwards Endpoint", SettingValue = "/RoverControls/stopForwardAndBackward", SettingType = Common.Enums.SettingsType.Wifi },
                                                   new Setting { Id = 9, SettingName = "Exit Endpoint", SettingValue = "/RoverControls/stopAll", SettingType = Common.Enums.SettingsType.Wifi });
        }

        public DbSet<Setting> Settings { get; set; }
    }
}
