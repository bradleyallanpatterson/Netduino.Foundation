using System;
using Microsoft.SPOT;

namespace Netduino.Foundation.Displays.TextDisplayMenu.InputTypes
{
    public class TimeDetailed : TimeBase
    {
        public TimeDetailed() : base(TimeMode.HH_MM_SS)
        {
        }
    }
}
