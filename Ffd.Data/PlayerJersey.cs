using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    class PlayerJersey : Player
    {
        // private int _leagueCode;
        private int _templateId;

        //public int LeagueCode
        //{
        //    get { return _leagueCode; }
        //    set { _leagueCode = value; }
        //}

        public int TemplateId
        {
            get { return _templateId; }
            set { _templateId = value; }
        }

        public PlayerJersey(string jerseyName, int number, int templateId)
        {
            JerseyName = jerseyName;
            Number = number;
            _templateId = templateId;
        }
    }
}
