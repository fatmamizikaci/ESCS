using ESCS.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ESCS.WebUI.Helper
{
    public static class JsonHelper
    {
        /// <summary>
        /// Sınav model verisini veri havuzuna ekler
        /// </summary>
        /// <param name="data"></param>
        public static void WriteJsonToFile(SinavModel data)
        {
            string sinavDosyaYolu = $"Data/Sinav/{data.Id}.txt";
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(sinavDosyaYolu, json);
        }

        /// <summary>
        /// Veri havuzundan sınav model verisini getirir
        /// </summary>
        /// <param name="sinavId"></param>
        /// <returns></returns>
        public static SinavModel ReadJsonFromFile(int sinavId)
        {
            string sinavDosyaYolu = $"Data/Sinav/{sinavId}.txt";
            string json = File.ReadAllText(sinavDosyaYolu);
            return JsonConvert.DeserializeObject<SinavModel>(json);
        }

        public static List<SinavModel> GetAllSinav()
        {
            int[] files = KayitliSinavlar();
            List<SinavModel> sinavlar = new List<SinavModel>();
            foreach (var id in files)
                sinavlar.Add(ReadJsonFromFile(id));
            return sinavlar;
        }

        public static int CreateSinavId()
        {
            int[] files = KayitliSinavlar();
            return files.Length == 0 ? 1 : files.Max() + 1;
        }

        private static int[] KayitliSinavlar()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"Data\Sinav");
            FileInfo[] files = directoryInfo.GetFiles("*.txt");
            return files.Select(s => Convert.ToInt32(s.Name.Replace(".txt", ""))).ToArray();
        }
    }
}
