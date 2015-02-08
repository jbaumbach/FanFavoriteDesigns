using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Common.AppActivate
{
    public class VbFunctions
    {
        public static void SwitchToWindow(string windowTitle)
        {
            Microsoft.VisualBasic.Interaction.AppActivate(windowTitle);
        }

       
    }
}
