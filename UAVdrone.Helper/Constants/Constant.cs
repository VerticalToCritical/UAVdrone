using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAVdrone.Helper.Constants
{
    public class Constant
    {
        public enum CompassPoint
        {
            [Description("North")]
            N,
            [Description("South")]
            S,
            [Description("West")]
            W,
            [Description("East")]
            E
        }

        public enum DroneCommand
        {
            [Description("Rotate 90 Degree Left")]
            L,
            [Description("Rotate 90 Degree Right")]
            R,
            [Description("Move Forward")]
            M
        }
    }
}
