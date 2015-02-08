using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ffd.Data
{
    public class TemplateGraphicJersey : TemplateGraphic
    {
        private Rectangle _nameBoundingBox;
        private Rectangle _numberBoundingBox;
        private string _nameFont;
        private ManualPositionAdjustments _nameManualPositionAdjustments;
        private ManualPositionAdjustments _numberManualPositionAdjustments;


        public Rectangle NameBoundingBox
        {
            get { return _nameBoundingBox; }
            set { _nameBoundingBox = value; }
        }
        public Rectangle NumberBoundingBox
        {
            get { return _numberBoundingBox; }
            set { _numberBoundingBox = value; }
        }

        public string NameFont
        {
            get { return _nameFont; }
            set { _nameFont = value; }
        }

        public ManualPositionAdjustments NameManualPositionAdjustments
        {
            get { return _nameManualPositionAdjustments; }
            set { _nameManualPositionAdjustments = value; }
        }

        public ManualPositionAdjustments NumberManualPositionAdjustments
        {
            get { return _numberManualPositionAdjustments; }
            set { _numberManualPositionAdjustments = value; }
        }
    }
}
