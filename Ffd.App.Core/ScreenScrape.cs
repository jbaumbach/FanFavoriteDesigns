using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ffd.Common;
using Ffd.Data;

namespace Ffd.App.Core
{
    public class ScreenScrape
    {
        //
        // Callback functions that you can pass in, if you're so inclined.
        //

        /// <summary>
        /// A callback function signature for logging stuff that happens in this procedure to your
        /// window.
        /// </summary>
        /// <param name="line">The string to log.</param>
        public delegate void LogDelegate(string line);

        private bool _userCancel;

        public bool UserCancel
        {
            get { return _userCancel; }
            set { _userCancel = value; }
        }

    }
}
