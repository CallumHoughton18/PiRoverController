using Microsoft.EntityFrameworkCore;
using PiRoverController.Common.Interfaces;
using PiRoverController.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PiRoverController.SettingAccessSQL
{
    public class SettingReaderWriterSQL : ISettingAccess
    {
        DbContextOptions<SettingsContext> options;
        public SettingReaderWriterSQL(string databasePath)
        {
            //inject db path through constructor
            var optionsBuilder = new DbContextOptionsBuilder<SettingsContext>();
            optionsBuilder.UseSqlite($"Filename={databasePath}");
            options = optionsBuilder.Options;
        }

        public IEnumerable<Setting> GetSettings()
        {
            using (var context = new SettingsContext(options))
            {
                return context.Settings.ToList();
            }
        }


        public Setting RetrieveSetting(string settingName)
        {
            using (var context = new SettingsContext(options))
            {
                return GetSettings().Where(x => x.SettingName == settingName).FirstOrDefault();
            }
        }

        public void SaveSetting(Setting setting)
        {
            using (var context = new SettingsContext(options))
            {
                context.Settings.Update(setting);
                context.SaveChanges();
            }
        }
    }
}
