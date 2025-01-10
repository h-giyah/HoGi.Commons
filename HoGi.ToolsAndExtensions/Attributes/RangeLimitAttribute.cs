using System;

namespace HoGi.ToolsAndExtensions.Attributes
{
    public class RangeLimitAttribute : Attribute
    {
        public int Minimum { get; }
        public int Maximum { get; }

        public RangeLimitAttribute(int Minimum,int Maximum)
        {
            this.Minimum = Minimum;
            this.Maximum = Maximum;
        }
    }
}
