using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Restaurant.Models;

namespace Restaurant
{
    public class Settings
    {
        static Settings _instance;
        static readonly string FilePath = ""; //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "settings.json");
        public static Stream SettingFileStream { get; set; }
        public static Settings Instance => _instance ?? (_instance = Load());

        public string AuthUserId
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

                Debug.WriteLine("Saving settings: {0}", FilePath);
                var json = JsonConvert.SerializeObject(this);
                using (var sw = new StreamWriter(SettingFileStream))
                {
                    sw.Write(json);
                }
            });
        }

        public static Settings Load()
        {
            Debug.WriteLine("Loading settings: {0}", FilePath);
            return null;
            //var settings = Helpers.LoadFromFile<Settings>(_filePath) ?? new Settings();
            //return settings;
        }
    }
}
