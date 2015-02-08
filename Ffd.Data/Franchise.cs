using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class Franchise
    {
        private string _franchiseDesc;
        private int _franchiseCode;

        public int FranchiseCode
        {
            get { return _franchiseCode; }
            set { _franchiseCode = value; }
        }

        public string FranchiseDesc
        {
            get { return _franchiseDesc; }
            set { _franchiseDesc = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", _franchiseDesc, _franchiseCode);
        }

        public Franchise()
        {
        }

        public Franchise(int franchiseCode, string franchiseDesc) : this()
        {
            _franchiseCode = franchiseCode;
            _franchiseDesc = franchiseDesc;
        }
    }
}
