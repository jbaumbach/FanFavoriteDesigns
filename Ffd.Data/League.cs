using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    public class League
    {
        private int _leagueCode;
        private string _leagueDescShort;


        public int LeagueCode
        {
            get { return _leagueCode; }
            set { _leagueCode = value; }
        }
        public string LeagueDescShort
        {
            get { return _leagueDescShort; }
            set { _leagueDescShort = value; }
        }

        public override string ToString()
        {
            return _leagueDescShort;
        }
    }
}
