using System;

namespace HoGi.Commons.ToolsAndExtensions.Attributes
{
    public class EndPointUriAttribute : Attribute
    {
        public string Uri { get; }

        public EndPointUriAttribute(string Uri)
        {
            this.Uri = Uri;
        }


    }
}
