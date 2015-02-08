using Ffd.Common;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Ffd.Presentation.Manager
{
    public class FfdUserControl : UserControl
    {
        /// <summary>
        /// Sets the object's string value into the control's text property.  Handles nulls and stuff.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="value">The object to set.</param>
        protected void SetControlText(Control control, object value)
        {
            string valueToSet = value == null ? string.Empty : value.ToString();
            control.Text = valueToSet;
        }

        /// <summary>
        /// Gets the value of the control's text property.
        /// </summary>
        /// <param name="control"></param>
        protected string GetControlText(Control control)
        {
            return control.Text;
        }
    }

}
