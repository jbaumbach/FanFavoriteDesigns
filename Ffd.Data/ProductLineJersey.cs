using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ffd.Data
{
    public class ProductLineJersey : ProductLine
    {

        private string _leagueCode = string.Empty;
        private string _seasonCode = string.Empty;
        private string _teamCode = string.Empty;
        private string _playerPositionCode = string.Empty;
        private int _templateId;

        private TemplateGraphicJersey _templateGraphic = null;


        public string LeagueCode
        {
            get { return _leagueCode; }
            set { _leagueCode = value; }
        }

        public string SeasonCode
        {
            get { return _seasonCode; }
            set { _seasonCode = value; }
        }

        public string TeamCode
        {
            get { return _teamCode; }
            set { _teamCode = value; }
        }

        public string PlayerPositionCode
        {
            get { return _playerPositionCode; }
            set { _playerPositionCode = value; }
        }

        public override string Description()
        {
            return "Jersey";
        }

        public int TemplateId
        {
            get { return _templateId; }
            set { _templateId = value; }
        }
	
        public TemplateGraphicJersey TemplateGraphic
        {
            get { return _templateGraphic; }
            set { _templateGraphic = value; }
        }

        public ProductLineJersey()
        {
        }

        public ProductLineJersey(string leagueCode, string seasonCode, string teamCode) : this()
        {
            _leagueCode = leagueCode;
            _seasonCode = seasonCode;
            _teamCode = teamCode;
        }
    }
}
