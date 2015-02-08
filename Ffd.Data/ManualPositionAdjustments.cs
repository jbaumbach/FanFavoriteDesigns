using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    /// <summary>
    /// The text output functions don't behave as my limited understanding would suggest.  This class holds offsets 
    /// for a given string ouput rectangle.
    /// </summary>
    public class ManualPositionAdjustments
    {
        private float _fontHeight;
        private int _fontVerticalPosition;

        /// <summary>
        /// Helps calculate font height to fit in bounding box
        /// </summary>
        public float FontHeight
        {
            get { return _fontHeight; }
            set { _fontHeight = value; }
        }

        /// <summary>
        /// Vertical offset to move text up or down in bounding box
        /// </summary>
        public int FontVerticalPosition
        {
            get { return _fontVerticalPosition; }
            set { _fontVerticalPosition = value; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fontHeight">new FontHeight value</param>
        /// <param name="fontVerticalPosition">new FontVerticalPosition value</param>
        public ManualPositionAdjustments(float fontHeight, int fontVerticalPosition)
        {
            _fontHeight = fontHeight;
            _fontVerticalPosition = fontVerticalPosition;
        }
    }
}
