using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ffd.Presentation.Manager
{
    public class CursorManager : IDisposable
    {
        private Control _objectToManage;
        private Cursor _originalCursor;

        public Control ObjectToManage
        {
            get { return _objectToManage; }
            set { _objectToManage = value; }
        }

        /// <summary>
        /// Show a cursor while this object is in scope.  Goes away automatically when out of scope.
        /// </summary>
        /// <remarks>
        /// Example:
        ///     using (CursorManager cm = new CursorManager(this, Cursors.WaitCursor))
        ///     {
        ///         ... stuff that may take a while to do...
        ///     }
        /// </remarks>
        /// <param name="objectToManage">The object that the cursor needs to change when over (typically "this")</param>
        /// <param name="cursorToSet">The cursor to set from the built-in "Cursors" collection</param>
        public CursorManager(Control objectToManage, Cursor cursorToSet)
        {
            _objectToManage = objectToManage;
            _originalCursor = objectToManage.Cursor;

            _objectToManage.Cursor = cursorToSet;
        }

        public void Dispose()
        { 
            _objectToManage.Cursor = _originalCursor;
        }
    }
}
