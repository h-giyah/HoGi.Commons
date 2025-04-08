using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HoGi.Commons.ToolsAndExtensions.Models;
using Newtonsoft.Json;

namespace HoGi.Commons.ToolsAndExtensions.Tools
{
    public class BankProvider
    {
        public static IList<Bank> Banks { get; }
        
        static BankProvider()
        {
            var assembly = typeof(Bank).GetTypeInfo().Assembly;

            using var resource = assembly.GetManifestResourceStream("HoGi.ToolsAndExtensions.Resources.banks.json");

            using var jsonStreamReader = new StreamReader(resource ?? throw new InvalidOperationException());

            Banks = JsonConvert.DeserializeObject<IList<Bank>>(jsonStreamReader.ReadToEnd());

        }
    }
}
