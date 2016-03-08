using Newtonsoft.Json;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Settings
    {
        static Settings _instance;
        static readonly string _filePath = ""; //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "settings.json");
        public static Stream SettingFileStream { get; set; }
        public static Settings Instance
        {
            get
            {
                return _instance ?? (_instance = Settings.Load());
            }
        }

        public string AuthUserID
        {
            get;
            set;
        }

        public string AthleteId
        {
            get;
            set;
        }

        public bool RegistrationComplete
        {
            get;
            set;
        }

        public string DeviceToken
        {
            get;
            set;
        }

        public ClientUser User
        {
            get;
            set;
        }

        public Task Save()
        {
            return Task.Factory.StartNew(() =>
            {
                //if (App.CurrentAthlete != null)
                    //DeviceToken = App.CurrentAthlete.DeviceToken;

                Debug.WriteLine(string.Format("Saving settings: {0}", _filePath));
                var json = JsonConvert.SerializeObject(this);
                using (var sw = new StreamWriter(SettingFileStream))
                {
                    sw.Write(json);
                }
            });
        }

        public static Settings Load()
        {
            Debug.WriteLine(string.Format("Loading settings: {0}", _filePath));
            return null;
            //var settings = Helpers.LoadFromFile<Settings>(_filePath) ?? new Settings();
            //return settings;
        }
    }
}
