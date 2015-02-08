using System;
using System.Collections.Generic;
using System.Text;

namespace Ffd.Data
{
    /// <summary>
    /// The basic building block for most jersey and player related functions.
    /// </summary>
    public class PlayerSeason : PersonName
    {
        public enum WebImageStatusType
        {
            None = 0,
            ReadyToUpload = 1,
            Uploaded = 2
        }

        private string _jerseyNumber;
        private string _jerseyName;
        private string _teamNickname;
        private string _city;
        private string _cityAbbreviation;
        private string _leagueDescriptionShort;
        private string _seasonDesc;
        private Template _templateCurrent = null;
        private int _seasonId;
        private int _franchiseCode = -1;
        private int playerCode;
        private int _marketingImageIndex;
        private Season _season = null;
        private string _ebayCategoryCode = string.Empty;
        private WebImageStatusType _webImageStatus = WebImageStatusType.None;

        public string JerseyNumber
        {
            get { return _jerseyNumber; }
            set { _jerseyNumber = value; }
        }

        public string JerseyName
        {
            get { return _jerseyName; }
            set { _jerseyName = value; }
        }

        public string TeamNickname
        {
            get { return _teamNickname; }
            set { _teamNickname = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string CityAbbreviation
        {
            get { return _cityAbbreviation; }
            set { _cityAbbreviation = value; }
        }

        public string LeagueDescriptionShort
        {
            get { return _leagueDescriptionShort; }
            set { _leagueDescriptionShort = value; }
        }

        /// <summary>
        /// This property is deprecated - use "Season" property instead. 
        /// </summary>
        public string SeasonDesc
        {
            get { return _seasonDesc; }
            set { _seasonDesc = value; }
        }

        public Template TemplateCurrent
        {
            get { return _templateCurrent; }
            set { SetTemplate(value); }
        }

        /// <summary>
        /// This property is deprecated - use "Season" property instead.
        /// </summary>
        public int SeasonId
        {
            get { return _seasonId; }
            set { _seasonId = value; }
        }
        public int FranchiseCode
        {
            get { return _franchiseCode; }
            set { _franchiseCode = value; }
        }

        public int PlayerCode
        {
            get { return playerCode; }
            set { playerCode = value; }
        }

        public Season Season
        {
            get { return _season; }
            set { _season = value; }
        }

        public override string ToString()
        {
            if (_templateCurrent != null)
            {
                return string.Format("{0} #{1} {2}", _jerseyName, _jerseyNumber, _templateCurrent.TemplateDescShort);
            }
            else
            {
                return string.Format("{0} #{1}", _jerseyName, _jerseyNumber);
            }
        }

        public int MarketingImageIndex
        {
            get { return _marketingImageIndex; }
            set { _marketingImageIndex = value; }
        }

        public string EBayCategoryCode
        {
            get { return _ebayCategoryCode; }
            set { _ebayCategoryCode = value; }
        }

        public WebImageStatusType WebImageStatus
        {
            get { return _webImageStatus; }
            set { _webImageStatus = value; }
        }


        #region Constructor(s)

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlayerSeason()
        {
        }

        /// <summary>
        /// Construct a player object initialized with the jersey name and number
        /// </summary>
        /// <param name="jerseyName">Name on the jersey</param>
        /// <param name="jerseyNumber">Number on the jersey</param>
        public PlayerSeason(string jerseyName, string jerseyNumber)
        {
            _jerseyName = jerseyName;
            _jerseyNumber = jerseyNumber;
        }
        #endregion

        /// <summary>
        /// Special processing for setting the template.  Always make sure this runs when
        /// the template is being set (e.g. property or constructor)
        /// </summary>
        /// <param name="template"></param>
        private void SetTemplate(Template template)
        {
            _templateCurrent = template;

            if ((_franchiseCode == -1) && (_season == null))
            {
                //
                // If setting the template and we don't have other specific information,
                // set up some generic settings
                //
                _franchiseCode = 1;     // Generic franchise code in DB
                _season = new Season();
                _season.SeasonId = _templateCurrent.GenericSeasonId;    // template has a generic season
                _season.SeasonDesc = "Generic";
            }

        }

        /// <summary>
        /// Parses a full name into first, middle, and last and sets the class properties including jersey name.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <param name="singleNamesAsFirst">If a single name is found, treat as first name.</param>
        /// <returns>The validity of the full name string.</returns>
        public override bool ParseFullName(string fullName, bool singleNamesAsFirst)
        {
            bool result = base.ParseFullName(fullName, singleNamesAsFirst);

            if (result)
            {
                _jerseyName = LastName.ToUpper();

                if (_jerseyName == string.Empty)
                {
                    _jerseyName = FirstName.ToUpper();
                }
            }

            return result;
        }
    }
}
