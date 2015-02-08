using System;
using System.Collections.Generic;
using System.Text;
using Ffd.Common;

namespace Ffd.Data
{
    /// <summary>
    /// Describes a marketing campaign.
    /// </summary>
    public class Campaign
    {
        public enum TrackingSystemType
        {
            NotSet = 0,
            GoogleAnalytics = 1
        }

        private string _source = string.Empty;
        private string _medium = string.Empty;
        private string _term = string.Empty;
        private string _content = string.Empty;
        private string _campaignName = string.Empty;
        private TrackingSystemType _trackingSystem = TrackingSystemType.GoogleAnalytics;

        /// <summary>
        /// Campaign source (referrer: google, citysearch, newsletter4)
        /// </summary>
        /// <remarks>
        /// There are five separate dimensions to URL Tracking with Google Analytics. Each dimension in the 
        /// URL starts off with “utm_”, followed by the name of the dimension. This first one is called Source, and 
        /// Source is simply where someone originated from. This could say google, yahoo, msn, altavista, client-newsletter, 
        /// july-email-campaign, and so on.
        /// </remarks>
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// Campaign medium (marketing medium: cpc, banner, email) 
        /// </summary>
        /// <remarks>
        /// The medium dimension tells you by what means did someone access your website? For our example, someone 
        /// clicked on a sponsored ad, which Google Analytics classifies as “cpc”. However, this could also be “cpm”, 
        /// for any site-targeted campaigns that charge per thousand impressions, “banner” to denote a banner advertisement, 
        /// or “email” if it’s an email blast of some kind.
        /// </remarks>
        public string Medium
        {
            get { return _medium; }
            set { _medium = value; }
        }

        /// <summary>
        /// Campaign term (identify the paid keywords)
        /// </summary>
        /// <remarks>
        /// Basically, the term dimension represents the keyword that is being assigned this particular destination URL.
        /// e.g. analytics+blogs
        /// </remarks>
        public string Term
        {
            get { return _term; }
            set { _term = value; }
        }

        /// <summary>
        /// Campaign content (versioning, use to differentiate ads) 
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Campaign name (product, promo code, or slogan)
        /// </summary>
        /// <remarks>
        /// Basically, the content dimension represents the actual ad version that is being assigned this particular destination URL.
        /// e.g. Second+Ad+Copy
        /// </remarks>
        public string CampaignName
        {
            get { return _campaignName; }
            set { _campaignName = value; }
        }

        /// <summary>
        /// The type of tracking system used for this object.
        /// </summary>
        public TrackingSystemType TrackingSystem
        {
            get { return _trackingSystem; }
            set { _trackingSystem = value; }
        }

        public string BuildTrackingString()
        {
            string result = string.Empty;

            switch (_trackingSystem)
            {
                case TrackingSystemType.GoogleAnalytics:
                    //
                    // Google kicks ass.  Hopefully share prices goes up after this crazy week.  Anyway, to track an email
                    // or any campaign with your Google Analytics account, append a bunch of parameters as described here.
                    //
                    // http://www.google.com/support/googleanalytics/bin/answer.py?hl=en&answer=55578
                    //
                    //
                    // Example:
                    //      http://www.fanfavoritedesigns.com/index.aspx?utm_source=fundraisingweb&utm_medium=email&utm_term=t&utm_content=001&utm_campaign=nhttp://www.fanfavoritedesigns.com/index.aspx?utm_source=fundraisingweb&utm_medium=email&utm_term=t&utm_content=001&utm_campaign=n
                    //
                    if (Functions.IsEmptyString(_source))
                    {
                        throw new ApplicationException("Campaign: \"Source\" property is required.");
                    }
                    result = Functions.BuildStringFromElementsWithDelimiter(result, string.Format("{0}={1}", "utm_source", _source), "&");

                    if (Functions.IsEmptyString(_medium))
                    {
                        throw new ApplicationException("Campaign: \"Medium\" property is required.");
                    }
                    result = Functions.BuildStringFromElementsWithDelimiter(result, string.Format("{0}={1}", "utm_medium", _medium), "&");

                    if (!Functions.IsEmptyString(_term))
                    {
                        result = Functions.BuildStringFromElementsWithDelimiter(result, string.Format("{0}={1}", "utm_term", _term), "&");
                    }

                    if (!Functions.IsEmptyString(_content))
                    {
                        result = Functions.BuildStringFromElementsWithDelimiter(result, string.Format("{0}={1}", "utm_content", _content), "&");
                    }

                    if (!Functions.IsEmptyString(_campaignName))
                    {
                        result = Functions.BuildStringFromElementsWithDelimiter(result, string.Format("{0}={1}", "utm_campaign", _campaignName), "&");
                    }

                    break;
                default:
                    throw new ApplicationException("Campaign: specified tracking system not implemented!");
            }

            return result;
        }
    }
}
