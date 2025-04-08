using System.Linq;
using HoGi.Commons.ToolsAndExtensions.Enums;
using HoGi.Commons.ToolsAndExtensions.Models.Logs;
using Microsoft.Extensions.Logging;

namespace HoGi.Commons.ToolsAndExtensions.Extensions
{
    public static class LogExtensions
    {
        public static void LogInfo(this ILogger<object> logger
            , string entityName, string entityId, OperationType type
            , string message, string changes=null, string extraData=null)
        {
            new Information(logger)
            {
                EntityName = entityName,
                EntityId = entityId,
                ServiceName = logger.GetType().GetGenericArguments().FirstOrDefault()?.Name,
                Type = type,
                Changes = changes,
                ExtraData = extraData,
                Message = message
            }.Log();
        }
    }
}
