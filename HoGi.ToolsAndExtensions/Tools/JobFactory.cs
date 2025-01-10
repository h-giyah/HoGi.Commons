using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HoGi.ToolsAndExtensions.Models;
using Newtonsoft.Json;

namespace HoGi.ToolsAndExtensions.Tools
{
    public class JobFactory
    {
        private static IList<SejamJob> jobs;

        static JobFactory()
        {
            var assembly = typeof(JobFactory).GetTypeInfo().Assembly;
            using var resource = assembly.GetManifestResourceStream($"GreenPhoenix.ToolsAndExtensions.Resources.jobs.json");
            using var jsonStreamReader = new StreamReader(resource ?? throw new InvalidOperationException());
            jobs = JsonConvert.DeserializeObject<IList<SejamJob>>(jsonStreamReader.ReadToEnd());
        }

        public static SejamJob GetBySejamCode(int sejamCode)
        {
            return jobs.FirstOrDefault(j => j.SejamCode == sejamCode);
        } 
        public static SejamJob GetByIBshopCode(int id)
        {
            return jobs.FirstOrDefault(j => j.Id == id);
        }
    }
}
