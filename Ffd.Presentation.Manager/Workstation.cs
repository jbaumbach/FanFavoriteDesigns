using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Ffd.Presentation.Manager
{
    public class Workstation
    {
        /// <summary>
        /// Gets the process that Roland Cutstudio is running under, if it is running.
        /// </summary>
        /// <returns>The Cutstudio process.</returns>
        public static Process GetCutStudioProcess()
        {
            Process result = null;

            Process[] procList = Process.GetProcesses();

            foreach (Process process in procList)
            {
                string temp = process.MainWindowTitle;

                if (temp.Contains("CutStudio"))
                {
                    result = process;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// True if Roland Cutstudio is running.
        /// </summary>
        /// <returns>True if it's running.</returns>
        public static bool IsCutStudioRunning()
        {
            return (GetCutStudioProcess() != null);
        }

        /// <summary>
        /// Switches the workstation's current focus to Cutstudio.
        /// </summary>
        public static void SwitchFocusToCutStudio()
        {
            SwitchFocusToCutStudio(string.Empty);
        }

        /// <summary>
        /// Switches the workstation's current focus to Cutstudio.
        /// </summary>
        /// <param name="keysToSend">After the switch, sends some keys.  The function waits until the application
        /// has processed the keystrokes before returning.</param>
        /// <remarks>
        /// Here are some common things you could send to a program in the string parameter.
        ///     ^o = control-o
        ///     %f = alt-f
        ///     +a = shift-a
        ///     %fo = alt-f then o
        ///     +(EC) = shift e and shift c at same time        
        /// </remarks>
        public static void SwitchFocusToCutStudio(string keysToSend)
        {
            Process cutStudio = GetCutStudioProcess();
            string windowTitle = cutStudio.MainWindowTitle;

            Common.AppActivate.VbFunctions.SwitchToWindow(windowTitle);

            System.Threading.Thread.Sleep(800);

            if (keysToSend != string.Empty)
            {
                SendKeys.SendWait(keysToSend); 
            }
        }

        /// <summary>
        /// Runs a program.  Waits for the process to finish loading, or 5 seconds, whichever comes first.
        /// </summary>
        /// <param name="fileName">The full path to the program to run.</param>
        /// <param name="arguments">Any parameters if you got 'em.</param>
        /// <returns>The process that was started.</returns>
        public static Process StartProgram(string fileName, string arguments)
        {
            Process result = Process.Start(fileName, arguments);

            //
            // Wait for the process to finish loading, or 5 seconds, whichever comes first.
            //
            result.WaitForInputIdle(15000);

            return result;
        }

        /// <summary>
        /// The full path to the currently running EXE.
        /// </summary>
        public static string CurrentRunningEXE
        {
            get
            {
                return System.Windows.Forms.Application.ExecutablePath;
            }
        }

        /// <summary>
        /// Gets the creation time of the current running EXE.
        /// </summary>
        public static DateTime CurrentRunningEXEFileDate
        {
            get
            {
                return File.GetLastWriteTime(CurrentRunningEXE);
            }
        }

        /// <summary>
        /// Open up a file in the system's registered application (e.g. Notepad for files ending in .txt)
        /// </summary>
        /// <param name="otherAddrsFName">The file to open.</param>
        internal static void OpenFile(string otherAddrsFName)
        {
            Process.Start(otherAddrsFName);
        }
    }
}
