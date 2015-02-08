using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ffd.Common;
using Ffd.Data;
using System.Xml;

namespace Ffd.App.Core
{
    public class ScreenScrapeStateUniversityCom : ScreenScrape
    {



        private static void ProcessSUCSchoolUrls(List<string> schoolUrls, LogDelegate log)
        {

            // Address data - all on one line, no need for end token
            const string schoolPageSearchToken = "<address id=\"static_map_address\">";
            const string schoolPageItemPattern = "target=\"_blank\">(.*)</a>.*<span>(.*)</span>.*<span>(.*?)</span>";
            const string schoolPageAltAddrPattern = "class=\"name\">(.*)</span><br /><span>(.*)</span>.*<span>(.*?)</span>";
            const string schoolPageNoAddrPattern = "target=\"_blank\">(.*)</a></span><br /><span>(.*?)</span>";
            const string schoolPageNoAddrNoLinkPattern = "class=\"name\">(.*)</span><br /><span>(.*)</span>";

            const string schoolPageSearchEndToken = "</address>";
            const string twoPartAddressDelimiter = "||";

            // const string schoolPageEndToken = "<p><font size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><strong>Website:</strong><br>";

            // Get all the lines between the tokens
            const string schoolEnrollmentStartToken = "<th>Enrollment";
            const string schoolEnrollmentPattern = "<td>(.*)</td>";
            const string schoolEnrollmentEndToken = "</tr>";

            // There's a bunch of things between these lines we want to get.  So they all have to be put into one long string.
            const string schoolAthleticsStartToken = "<th>Sports / Athletic Conference Memberships</th>";
            // There's one of these
            const string schoolAthleticsMembershipPattern = "<td><acronym title=\"(.*)\">(.*)</acronym></td>";

            // There are one or more of these
            const string schoolAthleticsConferencePattern = "<th><acronym title=.*?>.*?</acronym>(.*?)</th><td><a href=.*?rel=\"nofollow\">(.*?)</a></td>";
            const string schoolAthleticsEndToken = "</tbody>";


            foreach (string schoolUrl in schoolUrls)
            {


                log(string.Format("Attempting to read HTML page \"{0}\" from remote server", schoolUrl));
                WebRequest request = WebRequest.Create(schoolUrl);

                WebResponse response;
                Stream resStream;
                Encoding encode;
                StreamReader readStream;
                try
                {
                    response = (request.GetResponse());
                    resStream = response.GetResponseStream();
                    encode = System.Text.Encoding.GetEncoding("utf-8");
                    readStream = new StreamReader(resStream, encode);
                }
                catch (Exception e)
                {
                    log(string.Format("*** Can't read page: {0}", e.Message));
                    continue;
                }

                string sInString;

                string schoolAddress = string.Empty;
                string schoolEnrollment = string.Empty;
                string schoolAthletics = string.Empty;

                log("Page read, checking for start of school data...");

                if (!readStream.EndOfStream)
                {
                    // Seek to PACIFIC division in NHL.COM's standing webpage.  If their page changes, gotta
                    // redo this code.
                    while ((sInString = readStream.ReadLine()) != null)
                    {
                        sInString = sInString.Trim();
                        Regex reg = new Regex(schoolPageSearchToken);

                        if (reg.IsMatch(sInString))
                        {
                            log("Found address...");
                            schoolAddress = sInString;

                            while (!schoolAddress.Contains(schoolPageSearchEndToken))
                            {
                                sInString = readStream.ReadLine();
                                if (sInString != null)
                                {
                                    schoolAddress += twoPartAddressDelimiter + sInString;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        if (sInString == schoolEnrollmentStartToken)
                        {
                            log("Found enrollment...");

                            try
                            {
                                while ((sInString = readStream.ReadLine()) != null)
                                {
                                    sInString = sInString.Trim();
                                    if (sInString != schoolEnrollmentEndToken)
                                    {
                                        schoolEnrollment += sInString.Trim();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                // Oh crap, something bad happened.
                                log(e.Message);
                            }

                            // break;
                        }

                        if (sInString == schoolAthleticsStartToken)
                        {
                            log("Found athletics...");

                            try
                            {
                                while ((sInString = readStream.ReadLine()) != null)
                                {
                                    sInString = sInString.Trim();
                                    if (sInString != schoolAthleticsEndToken)
                                    {
                                        schoolAthletics += sInString.Trim();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                // Oh crap, something bad happened.
                                log(e.Message);
                            }

                            // break;
                        }

                        if (schoolAddress != string.Empty && schoolEnrollment != string.Empty && schoolAthletics != string.Empty)
                        {
                            break;
                        }

                    }
                }

                readStream.Close();
                response.Close();

                //
                // Get address info.  There's gonna have to be more parsing.
                //
                Regex linkFinder = new Regex(schoolPageItemPattern);
                MatchCollection urlMatches = linkFinder.Matches(schoolAddress);

                log(string.Format("Found {0} data fields in the school data source.", urlMatches.Count));
                bool haveAmbiguousAddress = false;

                if (urlMatches.Count == 0)
                {
                    //
                    // We don't have an address.  Let's try an alternate pattern
                    //
                    linkFinder = new Regex(schoolPageAltAddrPattern);
                    urlMatches = linkFinder.Matches(schoolAddress);
                }

                if (urlMatches.Count == 0)
                {
                    //
                    // Still no address.  Let's see if it's one of those "Ambiguous address" pages
                    //
                    linkFinder = new Regex(schoolPageNoAddrPattern);
                    urlMatches = linkFinder.Matches(schoolAddress);
                    haveAmbiguousAddress = true;
                }

                if (urlMatches.Count == 0)
                {
                    //
                    // Try the ambiguous address but no url pattern.
                    //
                    linkFinder = new Regex(schoolPageNoAddrNoLinkPattern);
                    urlMatches = linkFinder.Matches(schoolAddress);
                    haveAmbiguousAddress = true;        // Just for readability, this val was already set above.
                }

                if (urlMatches.Count > 0)
                {
                    Match schoolData = urlMatches[0];

                    if (schoolData.Groups.Count >= 3)
                    {
                        Lead lead = new Lead();

                        lead.CompanyName = schoolData.Groups[1].ToString();
                        lead.FirstName = "";
                        lead.LastName = "";
                        lead.ShippingAddress = new Address();
                        lead.ShippingAddress.CompanyName = lead.CompanyName.ToString().Trim();

                        int cityStZipGroup = 3;

                        if (!haveAmbiguousAddress)
                        {
                            string[] addrs = schoolData.Groups[2].ToString().Trim().Split(new string[] { twoPartAddressDelimiter }, StringSplitOptions.RemoveEmptyEntries);

                            lead.ShippingAddress.Address1 = addrs[0];

                            if (addrs.Length > 1)
                            {
                                lead.ShippingAddress.Address2 = addrs[1];
                            }
                        }
                        else
                        {
                            lead.ShippingAddress.Address1 = "";
                            cityStZipGroup = 2;
                        }

                        Regex subAddr = new Regex("(.*), (.*) (.*)");
                        Match addressBreakdown = subAddr.Match(schoolData.Groups[cityStZipGroup].ToString().Trim());

                        if (addressBreakdown.Groups.Count >= 2)
                        {
                            lead.ShippingAddress.City = addressBreakdown.Groups[1].ToString();
                            lead.ShippingAddress.StateProvCode = DataManager.GetStateProvCode(addressBreakdown.Groups[2].ToString(), "USA");

                            if (lead.ShippingAddress.StateProvCode < 0)
                            {
                                //
                                // International lead - don't write
                                //
                                log(string.Format("** Not writing international lead: {0}, {1}", lead.ShippingAddress.City, addressBreakdown.Groups[2].ToString()));
                                break;
                            }

                            lead.ShippingAddress.ZipPostalCode = addressBreakdown.Groups[3].ToString().Trim();
                            lead.ShippingAddress.CountryCode = "USA";
                        }
                        else
                        {
                            log(string.Format("*** can't get address components from string: {0}", schoolData.Groups[3].ToString().Trim()));
                            break;
                        }

                        lead.IsSchool = true;
                        lead.LeadSourceCode = 3;
                        lead.LeadDate = DateTime.Now;       // This column doesn't like min val, SQLGenerator too hard to update to write null.  Just use now.

                        if (schoolEnrollment != string.Empty)
                        {
                            string enrollmentVal = Functions.GetValueFromText(schoolEnrollment, schoolEnrollmentPattern);
                            enrollmentVal = enrollmentVal.Replace(",", "");
                            if (Functions.IsNumeric(enrollmentVal))
                            {
                                lead.SchoolTotalEnrollment = int.Parse(enrollmentVal);
                            }
                            else
                            {
                                if (enrollmentVal != "N/A")
                                {
                                    log(string.Format("*** Non-numeric enrollment found: {0}", enrollmentVal));
                                }
                            }
                        }

                        if (schoolAthletics != string.Empty)
                        {
                            Regex athleticsMembershipFinder = new Regex(schoolAthleticsMembershipPattern);
                            MatchCollection athleticsMembershipMatches = athleticsMembershipFinder.Matches(schoolAthletics);

                            if (athleticsMembershipMatches.Count > 0 && athleticsMembershipMatches[0].Groups.Count >= 3)
                            {
                                string athAssocFullName = athleticsMembershipMatches[0].Groups[1].ToString();
                                // e.g. "NCAA"
                                string athAssocAbbrev = athleticsMembershipMatches[0].Groups[2].ToString();

                                Regex athleticsConferenceFinder = new Regex(schoolAthleticsConferencePattern);
                                MatchCollection athleticsConferenceMatches = athleticsConferenceFinder.Matches(schoolAthletics);

                                // Do stuff here. XML doc to store in comments?

                                XmlDocument doc = new XmlDocument();
                                XmlElement root = doc.CreateElement("lead");

                                XmlElement athleticAssociation = doc.CreateElement("athleticAssociation");
                                XmlAttribute athleticAssociationFullName = doc.CreateAttribute("athleticAssociationFullName");
                                athleticAssociationFullName.Value = athAssocFullName;
                                athleticAssociation.SetAttributeNode(athleticAssociationFullName);

                                XmlAttribute athleticAssociationAbbreviation = doc.CreateAttribute("athleticAssociationAbbreviation");
                                athleticAssociationAbbreviation.Value = athAssocAbbrev;
                                athleticAssociation.SetAttributeNode(athleticAssociationAbbreviation);

                                root.AppendChild(athleticAssociation);

                                foreach (Match sportMatch in athleticsConferenceMatches)
                                {
                                    XmlElement sport = doc.CreateElement("sport");

                                    XmlAttribute name = doc.CreateAttribute("name");
                                    name.Value = sportMatch.Groups[1].ToString().Trim();
                                    sport.SetAttributeNode(name);

                                    XmlAttribute conference = doc.CreateAttribute("conference");
                                    conference.Value = sportMatch.Groups[2].ToString().Trim();
                                    sport.SetAttributeNode(conference);

                                    root.AppendChild(sport);
                                }

                                doc.AppendChild(root);

                                lead.LeadInfoText = doc.InnerXml;
                                lead.IsAthletics = true;
                            }
                            else
                            {
                                log(string.Format("*** Can't get athletics listings from input string: {0}", schoolAthletics));
                            }
                        }

                        Regex reg = new Regex("[-0-9]+");
                        if (reg.IsMatch(lead.ShippingAddress.ZipPostalCode))
                        {
                            Lead existingLead = DataManager.GetLeadByCompanyNameAndZip(lead.CompanyName, lead.ShippingAddress.ZipPostalCode);

                            if (existingLead == null)
                            {
                                DataManager.WriteCustomer(lead, DataManager.ObjectWriteMode.Insert);
                                log(string.Format("Successful write of lead \"{0}\" to database!", lead.CompanyName));
                            }
                            else
                            {
                                log(string.Format("Found duplicate lead: \"{0}\" in \"{1}\"", lead.CompanyName, lead.ShippingAddress.ZipPostalCode));
                            }
                        }
                        else
                        {
                            log(string.Format("*** Unexpected data in regex - gotta recheck \"{0}\"", schoolUrl));
                        }
                    }
                }
                else
                {
                    string err = string.Format("*** Couldn't grab the school data from the input string!\r\n{0}", schoolAddress);
                    throw new ApplicationException(err);

                    log(err);

                }

                //
                // Sleep for a bit so we don't overload their poor server.
                //
                Thread.Sleep(250);
            }
        }



        /// <summary>
        /// Process the state urls.
        /// </summary>
        /// <param name="stateUrls"></param>
        /// <param name="log"></param>
        private static void ProcessSUCStateUrls(List<string> stateUrls, LogDelegate log)
        {
            const string baseUrl = "http://www.stateuniversity.com/universities/";





            //
            // Debugging - move the universityList declaration down to inside the foreach loop and remove the remainder of lines
            //
            //List<string> universityList = new List<string>();

            //universityList.Add("http://high-schools.com/alabama/abbeville.html");
            //ProcessHSCCityUrls(universityList, log);
            //return;






            const string stateFileSearchToken = "<h4>Universities (by state)</h4>";
            const string stateFileItemsPattern = "<a href=\"(.*?)\">";
            const string stateFileItemsEndToken = "</tbody>";


            foreach (string stateUrl in stateUrls)
            {
                List<string> universityList = new List<string>();

                log(string.Format("Attempting to read HTML page \"{0}\" from remote server", stateUrl));
                WebRequest request = WebRequest.Create(stateUrl);

                WebResponse response = (request.GetResponse());
                Stream resStream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(resStream, encode);

                string sInString;

                log("Page read, checking for start of city links...");

                if (!readStream.EndOfStream)
                {
                    // Seek to PACIFIC division in NHL.COM's standing webpage.  If their page changes, gotta
                    // redo this code.
                    while ((sInString = readStream.ReadLine()) != null)
                    {
                        sInString = sInString.Trim();
                        if (sInString == stateFileSearchToken)
                        {
                            log("Found start of links...");
                            while (sInString != null && sInString != stateFileItemsEndToken)
                            {
                                string statePage = Functions.GetValueFromText(sInString, stateFileItemsPattern);

                                if (!Functions.IsEmptyString(statePage))
                                {
                                    string statePageUlr = statePage;    //  Functions.BuildUrlFromElements(baseUrl, statePage);
                                    log(statePageUlr);

                                    universityList.Add(statePageUlr);
                                }

                                try
                                {
                                    sInString = readStream.ReadLine();

                                    if (sInString != null)
                                    {
                                        sInString = sInString.Trim();
                                    }
                                }
                                catch (Exception e)
                                {
                                    // Oh crap, something bad happened.
                                    log(e.Message);
                                }
                            }
                        }
                    }
                }

                readStream.Close();
                response.Close();

                ProcessSUCSchoolUrls(universityList, log);
            }

        }









        /// <summary>
        /// Scrape the StateUniversity.com website and grab all the university addresses.
        /// </summary>
        /// <param name="log"></param>
        public static void ScreenScrapeSUCWebsite(LogDelegate log)
        {
            log("Starting screenscrape...");

            const string baseUrl = "http://www.stateuniversity.com/universities/";

            const string baseFileSearchToken = "<td><a href=\"http://www.stateuniversity.com/universities/AK\">Alaska (AK)</a></td>";
            const string baseFileItemsPattern = "<a href=\"(.*)\">";
            const string baseFileItemsEndToken = "</tbody>";

            List<string> stateUrls = new List<string>();



            //
            // Debugging - remove this later
            //
            //stateUrls.Add("http://www.stateuniversity.com/universities/AK/University_of_Alaska_Anchorage.html");
            //ProcessSUCSchoolUrls(stateUrls, log);
            //return;



            log("Attempting to read HTML page from remote server");
            WebRequest request = WebRequest.Create(baseUrl);

            WebResponse response = (request.GetResponse());
            Stream resStream = response.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(resStream, encode);

            string sInString;

            log("Page read, checking for start of states links...");

            if (!readStream.EndOfStream)
            {
                // Seek to PACIFIC division in NHL.COM's standing webpage.  If their page changes, gotta
                // redo this code.
                while ((sInString = readStream.ReadLine()) != null)
                {
                    sInString = sInString.Trim();
                    if (sInString == baseFileSearchToken)
                    {
                        log("Found start of links...");
                        while (sInString != null && sInString != baseFileItemsEndToken)
                        {
                            string statePage = Functions.GetValueFromText(sInString, baseFileItemsPattern);

                            if (!Functions.IsEmptyString(statePage))
                            {
                                string statePageUlr = statePage;        //  Functions.BuildUrlFromElements(baseUrl, statePage);
                                log(statePageUlr);

                                stateUrls.Add(statePageUlr);
                            }

                            try
                            {
                                sInString = readStream.ReadLine();

                                if (sInString != null)
                                {
                                    sInString = sInString.Trim();
                                }
                            }
                            catch (Exception e)
                            {
                                // Oh crap, something bad happened.
                                log(e.Message);
                            }
                        }
                    }
                }
            }

            readStream.Close();
            response.Close();

            //
            // Let's delve a bit deeper into the site, shall we?
            //
            ProcessSUCStateUrls(stateUrls, log);

            log("All done.");
        }

    }
}
