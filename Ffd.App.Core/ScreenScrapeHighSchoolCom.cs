using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Ffd.Common;
using Ffd.Data;

namespace Ffd.App.Core
{
    public class ScreenScrapeHighSchoolCom : ScreenScrape
    {
        private static void ProcessHSCSchoolUrls(List<string> schoolUrls, LogDelegate log)
        {

            const string schoolPageSearchToken = "School Contact Info:</strong></font></legend>";
            // Note: must read all the lines until the end token into a string removing cr/lfs.
            const string schoolPageItemPattern = "<font size=\"1\">(.*) Address:.*<br>(.*)<br><a href=\"/.*\\.html\">(.*)</a>, <a href=.*\\.html\">(.*)</a>(.*)</font>";
            const string schoolPageEndToken = "<p><font size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\"><strong>Website:</strong><br>";




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

                string schoolDataSource = string.Empty;
                string enrollment = string.Empty;

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
                            log("Found start of links...");

                            try
                            {
                                while ((sInString = readStream.ReadLine()) != null)
                                {
                                    sInString = sInString.Trim();
                                    if (sInString != schoolPageEndToken)
                                    {
                                        schoolDataSource += sInString.Trim();
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

                        if (enrollment == string.Empty)
                        {
                            enrollment = Functions.GetValueFromText(sInString, "Total Enrollment - ([-0-9]+)<br>");
                        }

                        if (enrollment != string.Empty && schoolDataSource != string.Empty)
                        {
                            break;
                        }

                    }
                }

                readStream.Close();
                response.Close();

                //
                // We have a big-ass string of urls, one for each city, let's extract the urls and put them in
                // another big-ass list of url strings.
                //
                Regex linkFinder = new Regex(schoolPageItemPattern);
                MatchCollection urlMatches = linkFinder.Matches(schoolDataSource);

                log(string.Format("Found {0} data fields in the school data source.", urlMatches.Count));

                if (urlMatches.Count > 0)
                {
                    Match schoolData = urlMatches[0];

                    if (schoolData.Groups.Count >= 6)
                    {
                        Lead lead = new Lead();

                        lead.CompanyName = schoolData.Groups[1].ToString();
                        lead.FirstName = "";
                        lead.LastName = "";
                        lead.ShippingAddress = new Address();
                        lead.ShippingAddress.CompanyName = lead.CompanyName.ToString().Trim();
                        lead.ShippingAddress.Address1 = schoolData.Groups[2].ToString().Trim();
                        lead.ShippingAddress.City = schoolData.Groups[3].ToString().Trim();
                        lead.ShippingAddress.StateProvCode = DataManager.GetStateProvCode(schoolData.Groups[4].ToString(), "USA");
                        lead.ShippingAddress.ZipPostalCode = schoolData.Groups[5].ToString().Trim();
                        lead.ShippingAddress.CountryCode = "USA";
                        lead.IsSchool = true;
                        lead.LeadSourceCode = 2;
                        lead.LeadDate = DateTime.Now;       // This column doesn't like min val, SQLGenerator too hard to update to write null.  Just use now.

                        if (enrollment != string.Empty)
                        {
                            lead.SchoolTotalEnrollment = int.Parse(enrollment);
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
                    string err = string.Format("*** Couldn't grab the school data from the input string!\r\n{0}", schoolDataSource);
                    throw new ApplicationException(err);

                    log(err);

                }

                //
                // Sleep for a bit so we don't overload their poor server.
                //
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Get all the highschools on an HSC city page
        /// </summary>
        /// <param name="cityUrls"></param>
        /// <param name="log"></param>
        private static void ProcessHSCCityUrls(List<string> cityUrls, LogDelegate log)
        {
            const string baseUrl = "http://high-schools.com";







            //
            // Debugging - move the citylist declaration down to inside the foreach loop and remove the remainder of lines
            //
            //List<string> schoolList = new List<string>();

            //schoolList.Add("http://high-schools.com/schools/99020/abbeville-christian-academy.html");
            //ProcessHSCSchoolUrls(schoolList, log);
            //return;







            const string cityFileSearchToken = "public or private high school to view the specific high school";

            // Cool thing: the question mark makes it match the... ummm... smallest thing possible I think.  If you don't include 
            // it, the regex will match the whole string and all the urls will be included in the one match it returns.  This is bad.
            const string cityFileItemsPattern = "<a href=\"(.*?)\">";
            const string cityFileEndToken = "</table>";


            foreach (string cityUrl in cityUrls)
            {
                string longLineOfUrls = string.Empty;
                List<string> schoolList = new List<string>();


                log(string.Format("Attempting to read HTML page \"{0}\" from remote server", cityUrl));
                WebRequest request = WebRequest.Create(cityUrl);

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

                log("Page read, checking for start of school links...");

                if (!readStream.EndOfStream)
                {
                    // Seek to PACIFIC division in NHL.COM's standing webpage.  If their page changes, gotta
                    // redo this code.
                    while ((sInString = readStream.ReadLine()) != null)
                    {
                        sInString = sInString.Trim();
                        Regex reg = new Regex(cityFileSearchToken);

                        if (reg.IsMatch(sInString))
                        {
                            log("Found start of links...");

                            try
                            {
                                while ((sInString = readStream.ReadLine()) != null)
                                {
                                    sInString = sInString.Trim();
                                    if (sInString != cityFileEndToken)
                                    {
                                        longLineOfUrls += sInString.Trim();
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

                            break;
                        }
                    }
                }

                readStream.Close();
                response.Close();

                //
                // We have a big-ass string of urls, one for each city, let's extract the urls and put them in
                // another big-ass list of url strings.
                //
                Regex linkFinder = new Regex(cityFileItemsPattern);
                MatchCollection urlMatches = linkFinder.Matches(longLineOfUrls);

                log(string.Format("Found {0} schools in the city page.", urlMatches.Count));

                foreach (Match schoolMatch in urlMatches)
                {
                    schoolList.Add(Functions.BuildUrlFromElements(baseUrl, schoolMatch.Groups[1].ToString()));
                }

                ProcessHSCSchoolUrls(schoolList, log);
            }

        }

        /// <summary>
        /// Process the state urls.
        /// </summary>
        /// <param name="stateUrls"></param>
        /// <param name="log"></param>
        private static void ProcessHSCStateUrls(List<string> stateUrls, LogDelegate log)
        {
            const string baseUrl = "http://high-schools.com";





            //
            // Debugging - move the citylist declaration down to inside the foreach loop and remove the remainder of lines
            //
            //List<string> cityList = new List<string>();

            //cityList.Add("http://high-schools.com/alabama/abbeville.html");
            //ProcessHSCCityUrls(cityList, log);
            //return;






            const string stateFileSearchToken = "Statistics for .* High Schools</font></a>";
            const string stateFileItemsPattern = "<a href=\"(.*?)\">";
            const string stateFileItemsEndToken = "<td>&nbsp;</td>";


            foreach (string stateUrl in stateUrls)
            {
                List<string> cityList = new List<string>();
                string longLineOfUrls = string.Empty;

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
                        Regex reg = new Regex(stateFileSearchToken);

                        if (reg.IsMatch(sInString))
                        {
                            log("Found start of links...");

                            try
                            {
                                while ((sInString = readStream.ReadLine()) != null)
                                {
                                    sInString = sInString.Trim();
                                    if (sInString != stateFileItemsEndToken)
                                    {
                                        longLineOfUrls += sInString.Trim();
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

                            break;
                        }
                    }
                }

                readStream.Close();
                response.Close();

                //
                // We have a big-ass string of urls, one for each city, let's extract the urls and put them in
                // another big-ass list of url strings.
                //
                Regex linkFinder = new Regex(stateFileItemsPattern);
                MatchCollection urlMatches = linkFinder.Matches(longLineOfUrls);

                log(string.Format("Found {0} cities in the state file.", urlMatches.Count));

                foreach (Match cityMatch in urlMatches)
                {
                    cityList.Add(Functions.BuildUrlFromElements(baseUrl, cityMatch.Groups[1].ToString()));
                }

                ProcessHSCCityUrls(cityList, log);
            }

        }

        /// <summary>
        /// Scrape the High-school.com website and grab all the highschool addresses.
        /// </summary>
        /// <param name="log"></param>
        public static void ScreenScrapeHSCWebsite(LogDelegate log)
        {
            log("Starting screenscrape...");

            const string baseUrl = "http://high-schools.com";

            const string baseFileSearchToken = "<a href=\"/alabama.html\">Alabama</a><br>";
            const string baseFileItemsPattern = "<a href=\"(.*)\">";
            const string baseFileItemsEndToken = "</font>";

            List<string> stateUrls = new List<string>();



            //
            // Debugging - remove this later
            //
            //stateUrls.Add("http://high-schools.com/alabama.html");
            //ProcessHSCStateUrls(stateUrls, log);
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
                            string statePageUlr = Functions.BuildUrlFromElements(baseUrl, statePage);
                            log(statePageUlr);

                            stateUrls.Add(statePageUlr);

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
            ProcessHSCStateUrls(stateUrls, log);

            log("All done.");
        }

    }
}
