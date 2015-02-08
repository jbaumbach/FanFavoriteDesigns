using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    /// <summary>
    /// Holds a value from a code table, suitable for storing in a list box and such.
    /// </summary>
    public class Code
    {
        private string _codeValue;
        private string _codeDescription;

        /// <summary>
        /// The value of this code.
        /// </summary>
        public string CodeValue
        {
            get { return _codeValue; }
            set { _codeValue = value; }
        }

        /// <summary>
        /// The description of this code.
        /// </summary>
        public string CodeDescription
        {
            get { return _codeDescription; }
            set { _codeDescription = value; }
        }

        /// <summary>
        /// String representation.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _codeDescription;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="codeValue">The value of this code.</param>
        /// <param name="codeDescription">The description of this code.</param>
        public Code(string codeValue, string codeDescription)
        {
            _codeValue = codeValue;
            _codeDescription = codeDescription;
        }
    }
}
