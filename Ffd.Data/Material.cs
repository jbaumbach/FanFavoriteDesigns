using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ffd.Data
{
    public class Material
    {
        private int _materialId;
        private int _materialTypeCode;
        private string _rgbColorHex;
        private string _mfgPartNo;
        private string _mfgColorDesc;
        private bool _ffdOffered;
        private bool _ffdInStock;
        private int _brightness;

        public int MaterialId
        {
            get { return _materialId; }
            set { _materialId = value; }
        }
	
        public int MaterialTypeCode
        {
            get { return _materialTypeCode; }
            set { _materialTypeCode = value; }
        }

        public string RGBColorHex
        {
            get { return _rgbColorHex; }
            set { _rgbColorHex = value; }
        }

        public string MfgPartNo
        {
            get { return _mfgPartNo; }
            set { _mfgPartNo = value; }
        }

        public string MfgColorDesc
        {
            get { return _mfgColorDesc; }
            set { _mfgColorDesc = value; }
        }

        public bool FfdOffered
        {
            get { return _ffdOffered; }
            set { _ffdOffered = value; }
        }

        public bool FfdInStock
        {
            get { return _ffdInStock; }
            set { _ffdInStock = value; }
        }

        public int Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", _mfgPartNo, _mfgColorDesc);
        }
    }
}
