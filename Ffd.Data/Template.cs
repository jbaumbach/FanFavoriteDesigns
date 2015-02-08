using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class Template
    {
        private int _templateId;
        private string _templateDesc;
        private string _templateDescShort;
        private bool _active;
        private DateTime _createDate;
        private int _predecessorId;
        private bool _backwardCompatible;
        private int _genericSeasonId = -1;
        private decimal _msrp;
        private string _ebayCategoryCode;
        private string _dimensions;
        private float _nameNoUnitHeight;
        private int _ebayFFDStoreCategoryCode;

        public int TemplateId
        {
            get { return _templateId; }
            set { _templateId = value; }
        }

        public string TemplateDesc
        {
            get { return _templateDesc; }
            set { _templateDesc = value; }
        }

        /// <summary>
        /// The sport name - e.g. "Football", "Baseball"
        /// </summary>
        public string TemplateDescShort
        {
            get { return _templateDescShort; }
            set { _templateDescShort = value; }
        }

        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        public int PredecessorId
        {
            get { return _predecessorId; }
            set { _predecessorId = value; }
        }

        public bool BackwardCompatible
        {
            get { return _backwardCompatible; }
            set { _backwardCompatible = value; }
        }

        public int GenericSeasonId
        {
            get { return _genericSeasonId; }
            set { _genericSeasonId = value; }
        }

        public decimal MSRP
        {
            get { return _msrp; }
            set { _msrp = value; }
        }

        public string EBayCategoryCode
        {
            get { return _ebayCategoryCode; }
            set { _ebayCategoryCode = value; }
        }

        public string Dimensions
        {
            get { return _dimensions; }
            set { _dimensions = value; }
        }

        public float NameNoUnitHeight
        {
            get { return _nameNoUnitHeight; }
            set { _nameNoUnitHeight = value; }
        }

        /// <summary>
        /// The category code of the FFD store on ebay.  e.g. "Hockey" store category = 16927499.  You can 
        /// get this by looking at the link to the ebay store category in a browser, then looking at the store id.
        /// </summary>
        public int EbayFFDStoreCategoryCode
        {
            get { return _ebayFFDStoreCategoryCode; }
            set { _ebayFFDStoreCategoryCode = value; }
        }
	
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Template()
        {
            _templateId = -1;
            _templateDescShort = "[not set]";
        }

        public Template(int templateId) : this()
        {
            _templateId = templateId;
        }

        public Template(int templateId, string templateDescShort) : this(templateId)
        {
            _templateDescShort = templateDescShort;
        }

        #endregion

        public override string ToString()
        {
            return _templateDescShort;
        }
    }
}
