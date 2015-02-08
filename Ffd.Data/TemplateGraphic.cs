using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ffd.Data
{
    /// <summary>
    /// Defines a source graphic template.
    /// </summary>
    public class TemplateGraphic
    {
        private Image _templateImage;
        private string _cutfileFilename;
        private string _outlineBmpFilename;

        public Image TemplateImage
        {
            get { return _templateImage; }
            set { _templateImage = value; }
        }

        public string CutfileFilename
        {
            get { return _cutfileFilename; }
            set { _cutfileFilename = value; }
        }

        public string OutlineBmpFilename
        {
            get { return _outlineBmpFilename; }
            set { _outlineBmpFilename = value; }
        }

    }
}
