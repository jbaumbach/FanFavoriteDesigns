using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class Season
    {
        private int _seasonId;
        private string _seasonDesc;
        private string _leagueDescShort;
        private int _yearStarted;

        public int SeasonId
        {
            get { return _seasonId; }
            set { _seasonId = value; }
        }

        public string SeasonDesc
        {
            get { return _seasonDesc; }
            set { _seasonDesc = value; }
        }

        public override string ToString()
        {
            return _seasonDesc;
        }

        public string LeagueDescShort
        {
            get { return _leagueDescShort; }
            set { _leagueDescShort = value; }
        }

        public int YearStarted
        {
            get { return _yearStarted; }
            set { _yearStarted = value; }
        }
	
    }
}
