﻿using System.Collections.Generic;
using System.IO;

namespace HoGi.Commons.ToolsAndExtensions.Interfaces
{
    public interface IExcelReader
    {
        IList<T> GetExcelData<T>(Stream stream, bool hasHeader = true);
    }
}
