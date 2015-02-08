<%@ WebHandler Language="C#" Class="Ffd.Presentation.Website.getimage" %>

using Ffd.App.Core;
using Ffd.Common;
using Ffd.Data;

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Web;

namespace Ffd.Presentation.Website
{

    /// <summary>
    /// Displays an image.
    /// 
    /// Usage:
    ///     Parameter 1: Name to put on the jersey
    ///     [Required]  Name:   "name"
    ///                 Value:  string
    /// 
    ///     Parameter 2: Number to put on the jersey
    ///     [Required]  Name:   "number"
    ///                 Value:  string
    /// 
    ///     Parameter 3: The template name to use
    ///     [Required]  Name: "template"
    ///                 Value: string
    /// 
    ///     Parameter 4: A debug value
    ///     [Optional]  Name: "debug"
    ///                 Value: "debug"
    /// 
    /// Returns:
    ///     If debug is blank: An image of the requested object in JPEG format
    ///     If debug is valid: debug information in TXT format
    /// 
    /// </summary>
    public class getimage : IHttpHandler
    {
        private bool IsValidRequest(string name, string number, string templateDescription)
        {
            return ((!Functions.IsEmptyString(name) || !Functions.IsEmptyString(number)) && !Functions.IsEmptyString(templateDescription));
        }
        
        /// <summary>
        /// This is where the magic happens.
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            string name = context.Request.QueryString[QsKeys.Name];
            string number = context.Request.QueryString[QsKeys.Number];
            string templateDescription = context.Request.QueryString[QsKeys.Template];
            string debug = context.Request.QueryString[QsKeys.Debug];
            string referrer = context.Request.ServerVariables["HTTP_REFERER"];
            bool referrerValid = ((!Functions.IsEmptyString(referrer)) && 
                ((referrer.IndexOf("fanfavoritedesigns.com") > 0) ||
                ((referrer.IndexOf("ergocentricsoftware.com") > 0))));

            if (debug == "debug")
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("DEBUG RESPONSE\r\n");
                context.Response.Write("==============\r\n");
                context.Response.Write("\r\n");
                context.Response.Write(string.Format("varsion:  {0}\r\n", "1.0.1"));
                context.Response.Write(string.Format("name:     {0}\r\n", name));
                context.Response.Write(string.Format("number:   {0}\r\n", number));
                context.Response.Write(string.Format("template: {0}\r\n", templateDescription));
                context.Response.Write(string.Format("debug:    {0}\r\n", debug));
                context.Response.Write("\r\n");
                context.Response.Write(string.Format("referrer:   {0}\r\n", referrer));
                context.Response.Write(string.Format("ref. valid: {0}\r\n", referrerValid));
                context.Response.Write(string.Format("params val: {0}\r\n", IsValidRequest(name, number, templateDescription)));
            }
            else
            {
                if (referrerValid && IsValidRequest(name, number, templateDescription))
                {
                    context.Response.ContentType = "image/jpeg";

                    //
                    // Do some crazy stuff here
                    //
                    PlayerSeason playerSeason = new PlayerSeason(name.Substring(0, 13), number.Substring(0, 2));
                    Template template = DataManager.GetTemplate(templateDescription.Substring(0, 20));
                    ProductItemJersey piUserJersey = new ProductItemJersey(playerSeason, template);
                    Image userJerseyImage = ApplicationManager.GetImageFromProductItem(piUserJersey, ApplicationManager.StickerImageType.Marketing, false);
                    userJerseyImage.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    context.Response.ContentType = "image/gif";
                    context.Response.WriteFile("images-source/oops-bad-referer.gif");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}