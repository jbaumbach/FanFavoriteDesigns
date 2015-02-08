using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Ffd.Common
{
    public static class Config
    {
        public static string GraphicsRootDirectory()
        {
            return "c:\\fanfavoritedesigns\\ffdgraphics";
        }

        public static string GraphicsWebRootDirectory()
        {
            return "/images";
        }

        public static string SourceFilesDirectory()
        {
            return "Source Files";
        }

        public static string TemporaryMarketingGIFFilesDirectory()
        {
            return "Temporary Marketing GIF Files";
        }

        public static string ProductionReadyCSTFilesDirectory()
        {
            return "Production Ready CST Files";
        }

        public static string WorkingDirectory()
        {
            return "Working Directory";
        }

        public static string TemplateCSTFilesDirectory()
        {
            return "Template CST Files";
        }

        public static string CutStudioFullPathToEXE()
        {
            // return "C:\\Program Files\\CutStudio\\CutStudio.exe";
            return "C:\\Program Files (x86)\\CutStudio\\CutStudio.exe";
        }

        public static string EbayCSVCustomsHeaderFilename()
        {
            return "C:\\fanfavoritedesigns\\ebay\\Template\\fileexchange-customjersey-header-2008-09-18.txt";
        }

        public static string EbayCSVCustomsRowTemplateFilename()
        {
            return "C:\\fanfavoritedesigns\\ebay\\Template\\fileexchange-customjersey-line-template-2008-09-18.txt";
        }

        /***************************************************************
         * Update these two properties when changing the ebay template
         * *************************************************************/
        /// <summary>
        /// The filename of the header template for the less descriptive version of the template.
        /// </summary>
        /// <returns></returns>
        //public static string EbayCSVHeaderFilename()
        //{
        //    return "C:\\ebay\\Template\\fileexchange-header-2008-09-13.txt";
        //}

        /// <summary>
        /// The filename of the row template for the less descriptive version of the template.
        /// </summary>
        /// <returns></returns>
        //public static string EbayCSVRowTemplateFilename()
        //{
        //    return "C:\\ebay\\Template\\fileexchange-line-template-2008-09-13.txt";
        //}

        /// <summary>
        /// The "generic" (no specific team/player) output CSV file from Turbolister that we want to process (has both header and row).
        /// </summary>
        public static string EbayCSVOutputGenericTemplateFilename
        {
            get
            {
                return "C:\\fanfavoritedesigns\\ebay\\Template\\ebay-sample-generic-export-v10.csv";
            }
        }


        /// <summary>
        /// The file name of the header template for the fully descriptive version of the template (team and player info).
        /// </summary>
        /// <returns></returns>
        //public static string EbayCSVHeaderFilenameFull()
        //{
        //    return "C:\\ebay\\Template\\fileexchange-header-full-2007-12-08.txt";
        //}

        /// <summary>
        /// The file name of the row template for the fully descriptive version of the template (team and player info).
        /// </summary>
        /// <returns></returns>
        //public static string EbayCSVRowTemplateFilenameFull()
        //{
        //    return "C:\\ebay\\Template\\fileexchange-line-template-full-2007-12-08.txt";
        //}

        /// <summary>
        /// The "full" (has specific players/teams) output CSV file from Turbolister that we will process.
        /// </summary>
        /// <remarks>
        /// 2014-01-30 JB: updated template, removed "custom" section
        /// </remarks>
        public static string EbayCSVOutputFullTemplateFilename
        {
            get
            {
                return "C:\\fanfavoritedesigns\\ebay\\Template\\ebay-sample-league-export-v15.csv";
            }
        }

        public static string EbayCSVOutputWorkingDir()
        {
            return "C:\\fanfavoritedesigns\\ebay\\Output for TurboLister\\";
        }



        public static string MarketingImagesServerImageRootPath()
        {
            return "images/ffd";
        }

        public static string MarketingImagesLocalImageRootPath()
        {
            return "images\\ffd";
        }

        public static string MarketingImagesServerRootUrl()
        {
            //return Functions.BuildUrlFromElements("http://www.ergocentricsoftware.com", 
            //    MarketingImagesServerImageRootPath());

            // 2014-01-29 JB: updated to point to temp server 
            return Functions.BuildUrlFromElements("http://www.otamata.com/ffd",
                MarketingImagesServerImageRootPath());
        }

        public static string EmailAddrSupport()
        {
            return "jbaumbach@fanfavoritedesigns.com";
        }

        public static string ImportFileDir()
        {
            // return "C:\\ebay\\data\\Player Import Files\\NHL\\2007";
            return "C:\\fanfavoritedesigns\\ebay\\data\\Player Import Files\\NBA\\2007";
        }

        public static string MarketingImagesFTPUserId
        {
            get
            {
                return "ergocentric";
            }
        }

        public static string MarketingImagesFTPPw
        {
            get
            {
                return "biersch";
            }
        }

        public static string MarketingImagesFTPServer
        {
            get
            {
                return "ftp://web6.ehost-services.com";
            }
        }

        public static string MarketingImagesFTPBaseDir
        {
            get
            {
                return "/ergocentric/images/ffd";
            }
        }

        public static string GenericEbaySportsMemorabiliaCategory()
        {
            return "37793";
        }

        public static string MonsterImportFileDir
        {
            get
            {
                return "C:\\fanfavoritedesigns\\ffdbiz\\monster-commerce\\orders-to-process";
            }
        }

        public static string MonsterArchiveDir
        {
            get
            {
                return "C:\\fanfavoritedesigns\\ffdbiz\\monster-commerce\\processed";
            }
        }

        public static string MonsterImportFName
        {
            get
            {
                return "orders.xml";
            }
        }

        public static string PaypalMultiShipImportDir
        {
            get
            {
                return "C:\\fanfavoritedesigns\\ffdbiz\\monster-commerce\\ready-to-import-into-paypal-multishipping";
            }
        }

        /// <summary>
        /// Server where mailbox for leads exists.
        /// </summary>
        public static string LeadMailboxServer
        {
            get
            {
                return "mail.fanfavoritedesigns.com";
            }
        }

        /// <summary>
        /// UID of leads mailbox.  DO NOT CHANGE THIS TO A NON-EMAIL ADDRESS, this is also used as the reply-to.
        /// </summary>
        public static string LeadMailboxLoginUID
        {
            get
            {
                return "leads@fanfavoritedesigns.com";
            }
        }

        /// <summary>
        /// PW of leads mailbox.
        /// </summary>
        public static string LeadMailboxLoginPW
        {
            get
            {
                return "Monster11";
            }
        }

        ///// <summary>
        ///// Chilkat email key
        ///// </summary>
        //public static string ChilkatUnlockCodeEmail
        //{
        //    get
        //    {
        //        return "30-day trial";
        //    }
        //}

        /// <summary>
        /// URL of homepage (just top level domain name, no specific page)
        /// </summary>
        public static string WebsiteUrl
        {
            get
            {
                return "http://www.fanfavoritedesigns.com";
            }
        }

        /// <summary>
        /// The name of the default homepage (e.g. "index.aspx")
        /// </summary>
        public static string WebsiteHomepageName
        {
            get
            {
                return "index.aspx";
            }
        }

        /// <summary>
        /// The name of the fundraising page.
        /// </summary>
        public static string WebsiteFundraisingPage
        {
            get
            {
                return "fundraisingopportunities.aspx";
            }
        }

        /// <summary>
        /// The path to the output directory for the lead CSV files.
        /// </summary>
        public static string LeadCSVOutputDirectory
        {
            get 
            {
                return "C:\\fanfavoritedesigns\\ffdbiz\\leads\\output-files";
            }
        }

        /// <summary>
        /// Quandry: allow direct access to this function (quick), or require a property for each (intellsense)?
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string GetAppSetting(string setting)
        {
            
            // return System.Configuration.ConfigurationSettings.AppSettings[setting];
            
            // This doesn't work either.  Builds this value into "Settings.Designer.cs"???? WTF???????????????????.
            // return Properties.Settings .Properties[setting]. .DefaultValue.ToString();

            // Nothing can read from app.config.  Nothing works.  why why why????
            //if (ConfigurationManager.AppSettings.Count == 0)
            //{
            //    throw new ApplicationException("Can't read from app.config file - no can do.");
            //}

            // Lets try this method I found on CodeProject.
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add("uggabugga", "crap");
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            return "";
            // return ConfigurationManager.AppSettings[setting];
        }

    }
}
