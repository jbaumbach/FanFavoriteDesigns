using Chilkat;
using Ffd.Common;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Net;
using System.Web;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Ffd.App.Core
{
    /// <summary>
    /// Class to help with multithreading the marketing image uploading process for ebay items.
    /// </summary>
    public class ApplicationManagerWorker
    {
        // public volatile bool _uploadImagesShouldStop = false;
        private bool _isUploadImagesWorking = false;

        public Season _season = null;
        public Franchise _franchise = null;
        public ApplicationManager.LogDelegate _log = null;
        public bool _newPlayersOnly = false;
        public bool _ignoreDb = false;

        public bool _uploadImagesResult = false;

        public bool IsUploadImagesWorking
        {
            get
            {
                return _isUploadImagesWorking;
            }
        }

        public void DoUploadImagesWork()
        {
            _uploadImagesResult = ApplicationManager.UploadMarketingImages(_season, _franchise, _newPlayersOnly, _log, _ignoreDb);
        }
    }

    public static class ApplicationManager
    {
        /// <summary>
        /// The types of sticker output images we can create.
        /// </summary>
        public enum StickerImageType
        {
            Cutting = 0,
            Marketing = 1,
            Unknown = 99
        }

        //
        // Callback functions that you can pass in, if you're so inclined.
        //

        /// <summary>
        /// A callback function signature for logging stuff that happens in this procedure to your
        /// window.
        /// </summary>
        /// <param name="line">The string to log.</param>
        public delegate void LogDelegate(string line);

        /// <summary>
        /// Save the passed image to a file.
        /// </summary>
        /// <param name="image">The image to save.</param>
        /// <param name="fileName">The file name to save.</param>
        /// <param name="format">The image file format.</param>
        /// <param name="colorDepth">Color depth (e.g. "24L" - no quotes)</param>
        /// <returns>True if it worked.</returns>
        public static bool SaveImageToFile(Image image, string fileName, ImageFormat format, long colorDepth)
        {
            return SaveImageToFile(image, fileName, format, colorDepth, -1);
        }

        /// <summary>
        /// Save the passed image to a file.
        /// </summary>
        /// <param name="image">The image to save.</param>
        /// <param name="fileName">The file name to save.</param>
        /// <param name="format">The image file format.</param>
        /// <param name="newImageWidth">New width if you'd like to resize the image, -1 otherwise</param>
        /// <returns>True if it worked.</returns>
        public static bool SaveImageToFile(Image image, string fileName, ImageFormat format, long colorDepth, int newImageWidth)
        {
            ImageCodecInfo encoder = Functions.FindEncoder(format);
            EncoderParameters encoderParams = Functions.GetColorDepthEncoderParameters(colorDepth);

            Image scratchImage = null;
            Image targetCanvas = null;

            if (encoder == null)
            {
                throw new ApplicationException("Specified codec for image format not found on local system.");
            }

            if (image.Width == 0)
            {
                throw new ApplicationException("Image has no width - can't continue.");
            }

            if (newImageWidth > 0)
            {
                //
                // Resize image
                //
                int newImageHeight = (int)(image.Height * ((float)newImageWidth / (float)image.Width));

                targetCanvas = new Bitmap(newImageWidth, newImageHeight);
                Graphics grTarget = Graphics.FromImage(targetCanvas);
                //grTarget.DrawString(text, outputFont, Brushes.Black, 0, adjustments.FontVerticalPosition);    // <-- why is -70 needed?  If you put in a string format and 0,0 the text moves way down in the box.  Weird.
                //graphics.DrawImage(targetCanvas, actualBoundingBox);
                grTarget.DrawImage(image, new Rectangle(0, 0, newImageWidth, newImageHeight));

                grTarget.Dispose();

                scratchImage = targetCanvas;
            }
            else
            {
                scratchImage = image;
            }
            
            //
            // Give it a whirl, or throw an exception.  If this bombs out, check the filename.  Typically an error like
            // "A generic error occurred in GDI+" means that you don't have the target directory created.
            //
            //string path = new FileInfo(fileName).DirectoryName;
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            Functions.CreateDirectoryIfNeeded(fileName);

            scratchImage.Save(fileName, encoder, encoderParams);

            return true;
        }

        public static void ClearTemporaryBMPFiles()
        {
            string directoryToClear = Common.Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(),
                Config.WorkingDirectory());

            string[] fileList = Directory.GetFiles(directoryToClear, "*.bmp");

            foreach (string fileName in fileList)
            {
                FileInfo file = new FileInfo(fileName);
                file.Delete();
            }
        }

        public static TemplateGraphicJersey GetTemplateGraphicJerseyFromTemplate(Template template)
        {
            TemplateGraphicJersey result = DataManager.GetTemplateGraphicJerseyAttributes(template);

            result.CutfileFilename = BuildTemplateCutfileName(template);
            result.OutlineBmpFilename = BuildTemplateOutlineFilename(template);
            result.TemplateImage = Image.FromFile(result.OutlineBmpFilename);

            return result;
        }

        /// <summary>
        /// Gets a color from the passed hex string.
        /// </summary>
        /// <param name="colorHex">The RRGGBB hex string to use.</param>
        /// <returns>Color.Empty if it no workie.</returns>
        public static Color GetWindowsColorFromRGBString(string colorHex)
        {
            Color result = Color.Empty;

            byte[] rgbVals = { 255, 255, 255 };
            if (ConvertHexRGBValueToDecimalArray(colorHex, ref rgbVals))
            {
                result = Color.FromArgb(rgbVals[0], rgbVals[1], rgbVals[2]);
            }

            return result;
        }

        /// <summary>
        /// Return the recommended background color type for the passed foreground color.
        /// </summary>
        /// <param name="colorHex">The color to test.</param>
        /// <returns>The recommended type.</returns>
        public static ProductItemJersey.PreviewImageBackgroundColorType RecommendedBackgroundColor(string colorHex)
        {
            int colorBrightness = ColorBrightness(colorHex);
            if (colorBrightness >= 0 && colorBrightness < 80)
            {
                return ProductItemJersey.PreviewImageBackgroundColorType.Light;
            }
            else
            {
                return ProductItemJersey.PreviewImageBackgroundColorType.Dark;
            }
        }

        /// <summary>
        /// Create an image for the passed item of the selected image type and inserts it into the item.
        /// The TemplateGraphic property must contain the Image loaded already.
        /// </summary>
        /// <param name="item">Item to get all the required information from, and which receives the built image.</param>
        /// <param name="imageType">The image type to create.</param>
        /// <param name="drawBoundingBoxes">True to draw outline.</param>
        /// <returns>Image object containing the desired image type.</returns>
        public static Image GetImageFromProductItem(ProductItemJersey item, StickerImageType imageType, bool drawBoundingBoxes)
        {
            Image scratchImage;
            Graphics grImgWriter;
            Image result;

            // TemplateGraphicJersey templateGraphic = GetTemplateGraphicJerseyFromTemplate(item.Template); // DataManager.GetTemplateGraphicJersey(item.Template);
            TemplateGraphicJersey templateGraphic = GetTemplateGraphicJerseyFromTemplate(item.PlayerSeason.TemplateCurrent); // DataManager.GetTemplateGraphicJersey(item.Template);

            if (imageType == StickerImageType.Marketing)
            {
                Image sourceImage = templateGraphic.TemplateImage;

                //
                // Use memory stream to convert imported image into a format (TIFF) that the graphics engine
                // can work on
                //
                MemoryStream memoryImage = new MemoryStream();

                //
                // Saving in 24-bit color depth prevents an indexed-pixel format, which the graphics thingy doesn't like.
                //
                // Save our memory image
                //
                sourceImage.Save(memoryImage, Functions.FindEncoder(ImageFormat.Tiff), Functions.GetColorDepthEncoderParameters(24L));    //, System.Drawing.Imaging.Encoder.

                //
                // Finally, load something we can work on
                //
                scratchImage = Image.FromStream(memoryImage);
                grImgWriter = Graphics.FromImage(scratchImage);
            }
            else if (imageType == StickerImageType.Cutting)
            {
                scratchImage = new Bitmap(templateGraphic.TemplateImage.Width, templateGraphic.TemplateImage.Height);

                grImgWriter = Graphics.FromImage(scratchImage);
                grImgWriter.FillRectangle(Brushes.White, 0, 0, templateGraphic.TemplateImage.Width, templateGraphic.TemplateImage.Height);
            }
            else
            {
                throw new ApplicationException("Unknown sticker image type specified");
            }

            Rectangle actualNameBoundingBox = templateGraphic.NameBoundingBox;
            Rectangle actualNumberBoundingBox = templateGraphic.NumberBoundingBox;

            DrawStringOnTemplate(actualNameBoundingBox, templateGraphic.NameManualPositionAdjustments, templateGraphic.NameFont, item.PlayerSeason.JerseyName, scratchImage, grImgWriter);
            DrawStringOnTemplate(actualNumberBoundingBox, templateGraphic.NumberManualPositionAdjustments, templateGraphic.NameFont, item.PlayerSeason.JerseyNumber, scratchImage, grImgWriter);

            if (drawBoundingBoxes)
            {
                //
                // Debugging - draw the bounding rectangle we want to fill
                //
                Pen rectBorderPen = new Pen(Color.Black, 5);
                rectBorderPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                grImgWriter.DrawRectangle(rectBorderPen, actualNameBoundingBox);
                grImgWriter.DrawRectangle(rectBorderPen, actualNumberBoundingBox);
            }

            if (imageType == StickerImageType.Cutting)
            {
                //
                // We're done
                //
                result = scratchImage;
            }
            else if (imageType == StickerImageType.Marketing)
            {
                //
                // Create marketing image
                //

                //
                // Optional: add the copyright stuff
                //
                if (false)
                {
                    Rectangle copyrightBox = new Rectangle(200, 1415, 1150, 55);

                    string copyright = string.Format("Copyright (C) 2007-{0} Fan Favorite Designs", DateTime.Today.Year);
                    DrawStringOnTemplate(copyrightBox, new ManualPositionAdjustments(1, -10), "Tahoma", copyright, scratchImage, grImgWriter);
                }

                //
                // Resize image
                //
                int newImageWidth = 1800;   //350;
                int newImageHeight = (int)(scratchImage.Height * ((float)newImageWidth / (float)scratchImage.Width));
                Image targetCanvas = new Bitmap(newImageWidth, newImageHeight);
                grImgWriter = Graphics.FromImage(targetCanvas);
                grImgWriter.DrawImage(scratchImage, new Rectangle(0, 0, newImageWidth, newImageHeight));


                Bitmap inverted = new Bitmap(targetCanvas);

                bool lightPreviewBackground = false;
                if (item.PreviewImageBackgroundColor == ProductItemJersey.PreviewImageBackgroundColorType.Auto)
                {
                    int colorBrightness = ColorBrightness(item.Color);
                    lightPreviewBackground = colorBrightness >= 0 && colorBrightness < 80;
                }
                else
                {
                    lightPreviewBackground = item.PreviewImageBackgroundColor == ProductItemJersey.PreviewImageBackgroundColorType.Light;
                }

                Invert(inverted, item.Color, lightPreviewBackground);

                result = inverted;
            }
            else
            {
                throw new ApplicationException("Unknown sticker type specified - cannot continue.");
            }

            grImgWriter.Dispose();

            return result;
        }

        /// <summary>
        /// Draw the passed string onto the passed graphic.
        /// </summary>
        /// <param name="actualBoundingBox">Rectangle dimensions of the bounding box</param>
        /// <param name="adjustments">Adjustments to use</param>
        /// <param name="fontName">The name of the font to use</param>
        /// <param name="text">The string to write</param>
        /// <param name="canvas">Your current Image</param>
        /// <param name="graphics">Your current Graphics engine</param>
        private static void DrawStringOnTemplate(Rectangle actualBoundingBox, ManualPositionAdjustments adjustments, string fontName, string text, Image canvas, Graphics graphics)
        {
            Font outputFont = new Font(fontName, (int)((float)actualBoundingBox.Height * adjustments.FontHeight), FontStyle.Bold, GraphicsUnit.Pixel);

            StringFormat outputFormat = new StringFormat();
            outputFormat.Alignment = StringAlignment.Center;
            outputFormat.LineAlignment = StringAlignment.Center;        // <-- if not, font moves down inexplicably in rectangle
            outputFormat.FormatFlags = StringFormatFlags.FitBlackBox;   // doesn't do shit

            SizeF currentSize = graphics.MeasureString(text, outputFont);            // <-- gives you length of string!

            if (currentSize.ToSize().Width <= actualBoundingBox.Width)
            {
                graphics.DrawString(text, outputFont, Brushes.Black, actualBoundingBox, outputFormat);
            }
            else
            {
                //
                // Not gonna fit, need to adjust letter width
                //
                Image targetCanvas = new Bitmap(currentSize.ToSize().Width, actualBoundingBox.Height);
                Graphics grTarget = Graphics.FromImage(targetCanvas);

                //
                // Move about 8 px to the right; this makes the number seem to align better.  Damn if I know why.
                //
                // int centeringAdjustment = 15;
                int centeringAdjustment = 8;

                grTarget.DrawString(text, outputFont, Brushes.Black, centeringAdjustment, adjustments.FontVerticalPosition);    // <-- why is -70 needed?  If you put in a string format and 0,0 the text moves way down in the box.  Weird.
                graphics.DrawImage(targetCanvas, actualBoundingBox);

                grTarget.Dispose();     // 1/21/2008 JB: if something broken suddenly - check here
            }
        }

        /// <summary>
        /// Inverts all the pixels in a bitmap - fast.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Invert(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width * 3;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        p[0] = (byte)(255 - p[0]);
                        ++p;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }

        /// <summary>
        /// Converts the passed RGB value in hex format (e.g. "FFAA11") to an array of corresponding integers.
        /// </summary>
        /// <param name="hexColor">The hex value.</param>
        /// <param name="rgbVals">The array of size 3.</param>
        /// <returns>True if the hex color was valid.</returns>
        public static bool ConvertHexRGBValueToDecimalArray(string hexColor, ref byte[] rgbVals)
        {
            bool result = false;

            if (hexColor.Length == 6)
            {
                try
                {
                    byte colorValR = (byte)Convert.ToInt16(hexColor.Substring(0, 2), 16);
                    byte colorValG = (byte)Convert.ToInt16(hexColor.Substring(2, 2), 16);
                    byte colorValB = (byte)Convert.ToInt16(hexColor.Substring(4, 2), 16);

                    rgbVals[0] = colorValR;
                    rgbVals[1] = colorValG;
                    rgbVals[2] = colorValB;

                    result = true;
                }
                catch (Exception e)
                {
                    // Bummer
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the average of the three color components.
        /// </summary>
        /// <param name="rgbVals">An array of the 3 RGB values.</param>
        /// <returns>A byte with the average value.</returns>
        public static int ColorBrightness(string hexColor)
        {
            int result = -1;
            byte[] rgbVals = { 255, 255, 255 };
            if (ConvertHexRGBValueToDecimalArray(hexColor, ref rgbVals))
            {
                result = ColorBrightness(rgbVals);
            }
            return result;
        }

        /// <summary>
        /// Returns the average of the three color components.
        /// </summary>
        /// <param name="rgbVals">An array of the 3 RGB values.</param>
        /// <returns>A byte with the average value.</returns>
        public static byte ColorBrightness(byte[] rgbVals)
        {
            byte result = 0;
            result = (byte)((float)(rgbVals[0] + rgbVals[1] + rgbVals[2]) / 3);
            return result;
        }

        /// <summary>
        /// Inverts all the pixels in a bitmap - fast.  If a color is passed, and the color is so dark that the 
        /// resulting image would have not enough contrast, the bitmap is NOT inverted (stays white background).
        /// </summary>
        /// <param name="b"></param>
        /// <param name="RGBColorToUse">The hex color value to use to color the white part of the image, or string.Empty.  Default is white.</param>
        /// <returns>True if it worked.</returns>
        public static bool Invert(Bitmap b, string RGBColorToUse, bool useLightBG)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            System.IntPtr Scan0 = bmData.Scan0;
            
            //
            // Default color is white.  This is used if RGB value is invalid.
            //
            byte[] rgbVals = {255, 255, 255};
            ConvertHexRGBValueToDecimalArray(RGBColorToUse, ref rgbVals);

            //
            // Holds the levels of each color as a percentage, for multiplying below.
            //
            float[] rgbIntensity = { (float)rgbVals[0] / 255, (float)rgbVals[1] / 255, (float)rgbVals[2] / 255 };

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int nOffset = stride - b.Width * 3;
                // int nWidth = b.Width * 3;
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)   // nWidth
                    {
                        // p[0] = (byte)(255 - p[0]);
                        // ++p;

                        byte pixelR = p[2];
                        byte pixelG = p[1];
                        byte pixelB = p[0];

                        if (!useLightBG)
                        {
                            //
                            // This calculation does a few things: 
                            //
                            //  1. Invert the pixel:  "(255 - pixelR)"
                            //  2. Adjust the intensity based on the existing intensity:  " * rgbIntensity[0]".  This is
                            //     so the original, antialiased image doesn't look funky.
                            //
                            p[2] = (byte)((((float)(255 - pixelR) / 255) * rgbIntensity[0]) * 255);
                            p[1] = (byte)((((float)(255 - pixelG) / 255) * rgbIntensity[1]) * 255);
                            p[0] = (byte)((((float)(255 - pixelB) / 255) * rgbIntensity[2]) * 255);
                        }
                        else
                        {
                            // 
                            // This calculation keeps the white background the same, but adds in color into the black
                            // area.  Notice that it has to invert the color TWICE, since the color intensity is inverted by
                            // the color application.  Yeah, kind of a mind bender but it works.
                            //
                            p[2] = (byte)(255 - ((((float)(255 - pixelR) / 255) * (1 - rgbIntensity[0])) * 255));
                            p[1] = (byte)(255 - ((((float)(255 - pixelG) / 255) * (1 - rgbIntensity[1])) * 255));
                            p[0] = (byte)(255 - ((((float)(255 - pixelB) / 255) * (1 - rgbIntensity[2])) * 255));
                        }

                        p += 3;
                    }
                    p += nOffset;
                }
            }

            b.UnlockBits(bmData);

            return true;
        }


        public static string BuildPlayerMarketingImageFTPPath(PlayerSeason playerSeason, Franchise franchise)
        {
            // string franchiseDesc = season.LeagueDescShort;
            // string year = season.YearStarted.ToString();

            //string result = Functions.BuildUrlFromElements(Config.MarketingImagesFTPServer(),
            //    string.Format("{0}/{1}/{2}", leagueDesc, year, franchise.FranchiseDesc),
            //    BuildPlayerMarketingFilename(playerSeason));

            string result = string.Format("{0}/{1}/{2}", Config.MarketingImagesFTPBaseDir, playerSeason.LeagueDescriptionShort, franchise.FranchiseDesc);

            return result;
        }

        public static string BuildPlayerMarketingImageFTPPathRoot(PlayerSeason playerSeason)
        {
            string result = string.Format("{0}/{1}", Config.MarketingImagesFTPBaseDir, playerSeason.LeagueDescriptionShort);

            return result;
        }

        public static string BuildPlayerMarketingFilename(PlayerSeason playerSeason)
        {
            string fileName = string.Format("{0}_{1:000}_{2}_{3}.jpg",
                playerSeason.TemplateCurrent.TemplateDescShort,
                playerSeason.TemplateCurrent.TemplateId,
                playerSeason.JerseyName,
                playerSeason.JerseyNumber);

            return fileName;
        }

        public static string BuildPlayerMarketingTemporaryFullPath(PlayerSeason playerSeason)
        {
            string fileName = Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(),
                Config.TemporaryMarketingGIFFilesDirectory(),
                BuildPlayerMarketingFilename(playerSeason));

            return fileName;
        }

        public static string BuildPlayerMarketingTemporaryFullPath(Season season, Franchise franchise, PlayerSeason playerSeason)
        {
            //
            // Not using the year.
            //
            //string teamDir = string.Format("{0}\\{1}\\{2}", season.LeagueDescShort, 
            //    season.YearStarted.ToString(), 
            //    franchise.FranchiseDesc);

            string teamDir = string.Format("{0}\\{1}", season.LeagueDescShort, 
                franchise.FranchiseDesc);

            string fileName = Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(),
                Config.TemporaryMarketingGIFFilesDirectory(),
                Config.MarketingImagesLocalImageRootPath(),
                teamDir);

            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }

            fileName = Functions.BuildFilenameFromElements(fileName, BuildPlayerMarketingFilename(playerSeason));

            return fileName;
        }

        public static string BuildTemplateCutfileName(Template template)
        {
            string fileName = string.Format("{0}_{1:000}.cst", template.TemplateDescShort, template.TemplateId);

            return Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(),
                Config.TemplateCSTFilesDirectory(),
                fileName);
        }

        public static string BuildTemplateOutlineFilename(Template template)
        {
            string fileName = string.Format("{0}_{1:000}.bmp", template.TemplateDescShort, template.TemplateId);

            string localDir = Config.GraphicsRootDirectory();

            if (Directory.Exists(localDir))
            {
                return Functions.BuildFilenameFromElements(localDir,
                    Config.SourceFilesDirectory(),
                    fileName);
            }
            else
            {
                localDir = System.Web.HttpContext.Current.Request.MapPath(Functions.BuildUrlFromElements(Config.GraphicsWebRootDirectory(), 
                    Config.SourceFilesDirectory()));

                return Functions.BuildFilenameFromElements(localDir, fileName);
            }
        }

        public static string BuildPlayerImportableNameNumberFilename(PlayerSeason playerSeason, Template template)
        {
            string fileName = string.Format("{0}_{1:000}_{2}_{3}.bmp",
                template.TemplateDescShort,
                template.TemplateId,
                playerSeason.JerseyName,
                playerSeason.JerseyNumber);

            return Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(), 
                Config.WorkingDirectory(),
                fileName);
        }

        private static string BuildPlayerCutfileName(PlayerSeason playerSeason, Template template, string subDirectory)
        {
            string fileName = string.Format("{0}_{1:000}_{2}_{3}.cst",
                template.TemplateDescShort,
                template.TemplateId,
                playerSeason.JerseyName,
                playerSeason.JerseyNumber);

            return Functions.BuildFilenameFromElements(Config.GraphicsRootDirectory(),
                subDirectory,
                fileName);
        }

        public static string BuildPlayerSourceCutfileName(PlayerSeason playerSeason, Template template)
        {
            return BuildPlayerCutfileName(playerSeason, template, Config.ProductionReadyCSTFilesDirectory());
        }

        /// <summary>
        /// Build a link to the website homepage.
        /// </summary>
        /// <returns>A string url.</returns>
        public static string BuildWebsiteLink()
        {
            return BuildWebsiteLink(null, null);
        }

        /// <summary>
        /// Build a link to the website homepage.
        /// </summary>
        /// <param name="playerSeason">(Optional) Append preview parameters to the link for the passed playerseason.  The template 
        /// property must not be null.</param>
        /// <param name="lead">(Optional) A lead object to use to append Google Analytics tracking parameters.</param>
        /// <returns>A string url.</returns>
        public static string BuildWebsiteLink(PlayerSeason playerSeason, Campaign campaign)
        {
            return BuildWebsiteLink(playerSeason, campaign, Config.WebsiteHomepageName);
        }

        /// <summary>
        /// Build a link to the website homepage.
        /// </summary>
        /// <param name="playerSeason">(Optional) Append preview parameters to the link for the passed playerseason.  The template 
        /// property must not be null.</param>
        /// <param name="lead">(Optional) A lead object to use to append Google Analytics tracking parameters.</param>
        /// <returns>A string url.</returns>
        public static string BuildWebsiteLink(PlayerSeason playerSeason, Campaign campaign, string targetPage)
        {
            string baseUrl = Functions.BuildUrlFromElements(Config.WebsiteUrl, targetPage);
            string cmd = string.Empty;
            string track = string.Empty;

            if (playerSeason != null)
            {
                cmd = Functions.BuildQueryStringFromElements(
                    string.Format("{0}={1}", QsKeys.Name, playerSeason.JerseyName),
                    string.Format("{0}={1}", QsKeys.Number, playerSeason.JerseyNumber),
                    string.Format("{0}={1}", QsKeys.Template, playerSeason.TemplateCurrent.TemplateDescShort));

            }

            if (campaign != null)
            {
                track = campaign.BuildTrackingString();
            }

            return Functions.BuildStringFromElementsWithDelimiter(baseUrl,
                Functions.BuildStringFromElementsWithDelimiter(cmd, track, "&"),
                "?");
        }

        public static bool SourceCutFileExist(PlayerSeason playerSeason, Template template)
        {
            return File.Exists(BuildPlayerSourceCutfileName(playerSeason, template));
        }

        public static void WritePlayerMarketingGIFFile(PlayerSeason playerSeason)
        {
            WritePlayerMarketingGIFFile(null, null, playerSeason);
        }

        public static void WritePlayerMarketingGIFFile(Season season, Franchise franchise, PlayerSeason playerSeason)
        {
            WritePlayerMarketingGIFFile(season, franchise, playerSeason, false);
        }

        /// <summary>
        /// Create a marketing image and save it to a file specified in the config file.  It saves a JPG, not a GIF
        /// as the function title implies.
        /// </summary>
        /// <param name="season">(optional) A season object or null - used to build the filename.</param>
        /// <param name="franchise">(optional) A franchise object or null - used to build the filename.</param>
        /// <param name="playerSeason">The object specifying the desired parameters.</param>
        /// <param name="overWrite">True to overwrite an existing file.  False is much faster, the function doesn't do anything.</param>
        public static bool WritePlayerMarketingGIFFile(Season season, Franchise franchise, PlayerSeason playerSeason, bool overWrite)
        {
            string fileName = string.Empty;

            bool result = false;

            //
            // Save the image as JPG (was GIF - dumb of me to name the function as GIF) for posting to ebay or the website
            //
            if ((season != null) && (franchise != null))
            {
                fileName = BuildPlayerMarketingTemporaryFullPath(season, franchise, playerSeason);
            }
            else
            {
                fileName = BuildPlayerMarketingTemporaryFullPath(playerSeason);
            }

            //if (overWrite || !File.Exists(fileName))
            //{
                //ProductItemJersey item = new ProductItemJersey(playerSeason, playerSeason.TemplateCurrent);
                //Image marketingImage = ApplicationManager.GetImageFromProductItem(item, StickerImageType.Marketing, false);
                //result = ApplicationManager.SaveImageToFile(marketingImage, fileName, ImageFormat.Jpeg, 4L, 320);

            int dimension = 500;    // number of pixels on a side.  2014/02/17 JB: change to 500 from 320
            return WritePlayerMarketingGIFFile(playerSeason, dimension, fileName, overWrite);
            //}
            //else
            //{
            //    result = true;
            //}

        }

        /// <summary>
        /// Write the passed playerseason object to an image file of the passed width with the passed filename.
        /// </summary>
        /// <param name="playerSeason">The object specifying the desired parameters.  TemplateCurrent property must be set.</param>
        /// <param name="width">The width, in pixels.</param>
        /// <param name="fileName">The filename to write to.  If it exists, it's overwritten.</param>
        /// <param name="overWrite">True to overwrite an existing file.  False is much faster, the function doesn't do anything.</param>
        /// <returns></returns>
        public static bool WritePlayerMarketingGIFFile(PlayerSeason playerSeason, int width, string fileName, bool overWrite)
        {
            bool result;

            if (overWrite || !File.Exists(fileName))
            {
                ProductItemJersey item = new ProductItemJersey(playerSeason, playerSeason.TemplateCurrent);
                Image marketingImage = ApplicationManager.GetImageFromProductItem(item, StickerImageType.Marketing, false);
                result = ApplicationManager.SaveImageToFile(marketingImage, fileName, ImageFormat.Jpeg, 4L, width);
            }
            else
            {
                result = true;
            }

            return result;
        }
        
        public static bool CreateTurbolisterCustomsFile()
        {
            bool result = false;

            List<Template> templates = DataManager.GetTemplates();

            StreamReader reader = File.OpenText(Config.EbayCSVCustomsHeaderFilename());
            string header = reader.ReadToEnd();
            reader.Close();

            reader = File.OpenText(Config.EbayCSVCustomsRowTemplateFilename());
            string rowTemplate = reader.ReadToEnd();
            reader.Close();

            string outputDirectory = Functions.BuildFilenameFromElements(Config.EbayCSVOutputWorkingDir(),
                "custom-jerseys");

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            string outputFileName = Functions.BuildFilenameFromElements(outputDirectory,
                "readytoimport.csv");

            File.WriteAllText(outputFileName, header);

            StreamWriter writer = File.AppendText(outputFileName);

            foreach (Template template in templates)
            {
                string outputRow = rowTemplate;

                //
                // These two are the same.  One's shorter to fit into ebay's form fields.  The other one
                // should prolly be gone.
                //
                outputRow = outputRow.Replace("[template_desc_short]", template.TemplateDescShort);
                outputRow = outputRow.Replace("[desc_s]", template.TemplateDescShort);
                
                outputRow = outputRow.Replace("[copy_year]", DateTime.Now.Year.ToString());

                outputRow = outputRow.Replace("[image_file_url]", BuildGalleryImageString(template));
                outputRow = outputRow.Replace("[company_email_addr]", Config.EmailAddrSupport());
                outputRow = outputRow.Replace("[dimensions]", template.Dimensions);

                if (template.EBayCategoryCode == string.Empty)
                {
                    template.EBayCategoryCode = Config.GenericEbaySportsMemorabiliaCategory();
                }

                outputRow = outputRow.Replace("[ebay_category]", template.EBayCategoryCode);

                writer.WriteLine(outputRow);

                result = true;
            }

            writer.Close();

            return result;

        }

        /// <summary>
        /// Builds the full description line for a player on a team
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private static string BuildDescriptionLineFull(PlayerSeason player)
        {
            //
            // Max ebay item description length: 55 chars
            //
            const int maxDescriptionLength = 55;

            string itemDescription = string.Format("{0}{1} {2} {3} #{4} Jersey Sticker Decal Window",
                player.City,
                string.Format("{0}{1}", player.CityAbbreviation == string.Empty ? "" : " ", player.CityAbbreviation),
                player.TeamNickname,
                player.BuildFullName(),
                player.JerseyNumber);

            //
            // Don't wanna chop off a word in the middle
            //
            while (itemDescription.Length > maxDescriptionLength)
            {
                itemDescription = itemDescription.Substring(0, itemDescription.LastIndexOf(" "));
            }

            if (!itemDescription.Contains("Sticker"))
            {
                // Crap - name so long, it chopped off the word "sticker".  Not good.  Let's try something else.
                itemDescription = string.Format("{0} {1} {2} #{3} Jersey Sticker Decal Window",
                    player.CityAbbreviation == string.Empty ? player.City : player.CityAbbreviation,
                    player.TeamNickname,
                    player.BuildShortFullName(),
                    player.JerseyNumber);


                    while (itemDescription.Length > maxDescriptionLength)
                    {
                        itemDescription = itemDescription.Substring(0, itemDescription.LastIndexOf(" "));
                    }

                    if (!itemDescription.Contains("Sticker"))
                    {
                        // Man, we're getting desparate now.  One last try and we'll live with the results
                        itemDescription = string.Format("{0} {1} {2} #{3} Sticker",
                            player.CityAbbreviation == string.Empty ? player.City : player.CityAbbreviation,
                            player.TeamNickname,
                            player.LastName,
                            player.JerseyNumber);
                    }
            }

            return itemDescription;
        }

        private static string BuildDescriptionLineGeneric(PlayerSeason player)
        {
            //
            // Max ebay item description length: 55 chars
            //
            const int maxDescriptionLength = 55;

            string itemDescription = string.Format("{0} #{1} {2} Jersey Sticker Decal Window",
                player.LastName,
                player.JerseyNumber,
                player.TemplateCurrent.TemplateDescShort);

            //
            // Don't wanna chop off a word in the middle
            //
            while (itemDescription.Length > maxDescriptionLength)
            {
                itemDescription = itemDescription.Substring(0, itemDescription.LastIndexOf(" "));
            }

            return itemDescription;
        }

        public static string GetPlayerNameNumberKey(PlayerSeason player)
        {
            return string.Format("{0}{1}", player.JerseyName, player.JerseyNumber);
        }

        /// <summary>
        /// Returns a list of players with unique names and numbers.  This is on (o)2 algorithm but hopefully hashtable is optimized.
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static List<PlayerSeason> FilterToUniqueNamesAndNumbers(List<PlayerSeason> players)
        {
            List<PlayerSeason> results = new List<PlayerSeason>();
            Hashtable keylist = new Hashtable();

            foreach(PlayerSeason player in players)
            {
                string key = GetPlayerNameNumberKey(player);
                if (!keylist.Contains(key))
                {
                    results.Add(player);
                    keylist.Add(key, string.Empty);
                }
            }

            return results;
        }

        /// <summary>
        /// Get the header data from the exported CSV file without all that manual notepad rigamarole.
        /// </summary>
        /// <param name="fileName">The CSV file name.</param>
        /// <returns>The header from the file.</returns>
        public static string GetTurboListerHeaderFromExportedCSV(string fileName)
        {
            //
            // Read the first row
            //
            StreamReader reader = File.OpenText(fileName);
            string header = reader.ReadLine();
            reader.Close();

            return header.Trim();
        }

        /// <summary>
        /// Get the row data from the exported CSV data file.
        /// </summary>
        /// <param name="fileName">The CSV file name.</param>
        /// <param name="errMsg">If there's an error, like a missing value in the file, it'll be described here.</param>
        /// <returns>The row from the file.</returns>
        public static string GetTurboListerRowFromExportedCSV(string fileName, out string errMsg)
        {
            //
            // Read header row, then read the row we want.
            //
            StreamReader reader = File.OpenText(fileName);
            string rowTemplate = reader.ReadLine();
            rowTemplate = reader.ReadLine();
            reader.Close();
            errMsg = string.Empty;

            //
            // These fields are typically in the ebay output file, replace them with tokens.  This code
            // just makes the actual file builder code a bit more readable.
            //
            Dictionary<string, string> replacements = new Dictionary<string, string>();

            replacements.Add("http://www.ergocentricsoftware.com/images/ffd/NHL/2007/LA%20Kings/hockey_001_LaBARBERA_35.jpg|http://www.ergocentricsoftware.com/images/ffd/mkt/mkt-t001-005.jpg", "[image_file_url]");
            
            // 2013-02-05 JB: this seems to have just disappeared from the template?
            //replacements.Add("jbaumbach@fanfavoritedesigns.com", "[company_email_addr]");


            const string ebayMainProductCategoryId = "206";             // Select "NFL" for the category when saving the template (was: "25232")
            const string ebayStoreCategory = "16927499";                // Not sure what this is?!?!?!

            //replacements.Add("25232", "[ebay_category]");

            replacements.Add(ebayMainProductCategoryId, "[ebay_category]");       // Should match the NFL 
            replacements.Add(ebayStoreCategory, "[ebay_store_category]");

            //
            // Validate the row, all these fields must be there or we're wasting our time.
            // 
            foreach (KeyValuePair<string, string> replacement in replacements)
            {
                if (!rowTemplate.Contains(replacement.Key))
                {
                    errMsg = string.Format("Cannot find required value in output file: \"{0}\"", replacement);
                    return string.Empty;
                }
                else
                {
                    rowTemplate = rowTemplate.Replace(replacement.Key, replacement.Value);
                }
            }

            return rowTemplate.Trim();
        }

        /// <summary>
        /// Builds the ebay Turbolister file.  Modify this as necessary to build the individual listing files.
        /// </summary>
        /// <param name="season">The season object.</param>
        /// <param name="franchise">The franchise object or null.</param>
        /// <param name="previouslyUnexportedPlayersOnly">Whether to only include previously unexported players.</param>
        /// <param name="createGraphics">Whether to create the graphic files too for each player.</param>
        /// <returns>True if it worked.</returns>
        public static bool CreateTurbolisterFile(Season season, 
            Franchise franchise, 
            bool previouslyUnexportedPlayersOnly, 
            bool createGraphics,
            DataManager.GetPlayerSeasonType type,
            bool useGenericTemplate,
            string fileName,
            out string errMsg,
            LogDelegate log,
            LogDelegate uploadGraphicsLog)
        {

            bool result = false;
            bool havePassedFranchise = (franchise != null);
            int currentFranchiseCode = franchise == null ? -1 : franchise.FranchiseCode;

            // 2014-01-29 JB - Let's skip uploading for now, just create graphics.  When ready, implementing the uploading again to "a server"?
            bool doActualUpload = false;
                

            List<PlayerSeason> players = DataManager.GetPlayerSeasons(season, franchise, previouslyUnexportedPlayersOnly);
            List<PlayerSeason> playersToProcess = null;
            List<PlayerSeason> playersToUploadGraphics = new List<PlayerSeason>();
            Template currentTemplate = null;

            string header = GetTurboListerHeaderFromExportedCSV(fileName);
            string rowTemplate = GetTurboListerRowFromExportedCSV(fileName, out errMsg);

            //
            // If we don't have a valid row, nothing to do, let's boogie on outta here.
            //
            if (rowTemplate == string.Empty)
            {
                return false;
            }

            string outputDirectory = Functions.BuildFilenameFromElements(Config.EbayCSVOutputWorkingDir(),
                season.LeagueDescShort,
                season.YearStarted.ToString());

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            string outputFileName = Functions.BuildFilenameFromElements(outputDirectory,
            string.Format("{0}-readytoimport.csv", franchise == null ? "allteams" : franchise.FranchiseDesc));

            StreamWriter writer =  new StreamWriter(File.Create(outputFileName));
            writer.WriteLine(header);

            if (type == DataManager.GetPlayerSeasonType.GroupedByLastnameAndNumber)
            {
                playersToProcess = FilterToUniqueNamesAndNumbers(players);
            }
            else
            {
                playersToProcess = players;
            }

            //
            // If we want to create (and upload) graphics, we need to clear the db.  The db is used by this 
            // thread and the upload thread to communicate, and if the db isn't cleared then the upload thread
            // can't do anything.
            //
            if (createGraphics)
            {
                DataManager.SetPlayerSeasonsImageStatus(playersToProcess, PlayerSeason.WebImageStatusType.None);
            }

            //
            // We're gonna upload graphics in a parallel process, set up that action here.
            //
            ApplicationManagerWorker workerObject = new ApplicationManagerWorker();
            workerObject._season = season;
            workerObject._newPlayersOnly = previouslyUnexportedPlayersOnly;
            workerObject._ignoreDb = false;
            workerObject._log = uploadGraphicsLog;
            Thread workerThread = new Thread(workerObject.DoUploadImagesWork);
            uploadGraphicsLog("Graphics upload pending start...");

            foreach (PlayerSeason player in playersToProcess)
            {
                string outputRow = rowTemplate;

                string itemDescription;

                //
                // Get some extra data we might need
                //
                if ((franchise == null) || (currentFranchiseCode != player.FranchiseCode))
                {
                    currentFranchiseCode = player.FranchiseCode;
                    franchise = DataManager.GetFranchise(currentFranchiseCode);
                }

                //
                // Unfortunately the PlayerSeason objects returned have a "lite" version of the template object (not all fields 
                // are filled in).  Get the rest of the data here.
                //
                if ((currentTemplate == null) || (currentTemplate.TemplateId != player.TemplateCurrent.TemplateId))
                {
                    currentTemplate = DataManager.GetTemplate(player.TemplateCurrent.TemplateDescShort);
                }

                log(string.Format("Processing: {0} {1} {2}", player.City, player.TeamNickname, player.LastName));

                //
                // Depending on the type of export we want, fill in the various fields.
                //
                if (type == DataManager.GetPlayerSeasonType.TeamsAndPlayers)
                {
                    itemDescription = BuildDescriptionLineFull(player);
                    outputRow = outputRow.Replace("[player_full_name]", player.BuildFullName());
                    outputRow = outputRow.Replace("[franchise_city]", player.City);
                    outputRow = outputRow.Replace("[franchise_nickname]", player.TeamNickname);
                    outputRow = outputRow.Replace("[player_last_name]", player.LastName);
                    outputRow = outputRow.Replace("[league]", player.LeagueDescriptionShort);

                    if (player.EBayCategoryCode == string.Empty)
                    {
                        player.EBayCategoryCode = Config.GenericEbaySportsMemorabiliaCategory();
                    }

                    outputRow = outputRow.Replace("[ebay_category]", player.EBayCategoryCode);
                }
                else
                {
                    itemDescription = BuildDescriptionLineGeneric(player);
                    outputRow = outputRow.Replace("[player_last_name]", player.JerseyName);

                    if (currentTemplate.EBayCategoryCode == string.Empty)
                    {
                        currentTemplate.EBayCategoryCode = Config.GenericEbaySportsMemorabiliaCategory();
                    }

                    outputRow = outputRow.Replace("[ebay_category]", currentTemplate.EBayCategoryCode);
                }

                // Common to both output types
                outputRow = outputRow.Replace("[jersey_number]", player.JerseyNumber.ToString());
                outputRow = outputRow.Replace("[copy_year]", DateTime.Now.Year.ToString());
                outputRow = outputRow.Replace("[image_file_url]", BuildGalleryImageString(season, franchise, player));
                outputRow = outputRow.Replace("[company_email_addr]", Config.EmailAddrSupport());
                outputRow = outputRow.Replace("[dimensions]", currentTemplate.Dimensions);
                outputRow = outputRow.Replace("[manually build title in row template file]", itemDescription);
                outputRow = outputRow.Replace("[ebay_store_category]", currentTemplate.EbayFFDStoreCategoryCode.ToString());
                outputRow = outputRow.Replace("[related_items]", string.Format("Related items: {0}", BuildDescriptionLineFull(player)));

                writer.WriteLine(outputRow);


                //
                // Not sure if this logic is perfect, do some usability tests for a while.  It might be ok.
                //
                if (createGraphics && player.WebImageStatus != PlayerSeason.WebImageStatusType.Uploaded)
                {
                    WritePlayerMarketingGIFFile(season, franchise, player);
                    playersToUploadGraphics.Add(player);


                    if (doActualUpload)
                    {
                        //
                        // When we have at least 5 players to upload graphics for, start up the parallel upload process
                        //
                        if (playersToUploadGraphics.Count >= 2 && !workerThread.IsAlive)
                        {
                            uploadGraphicsLog("Preparing to start upload thread...");
                            DataManager.SetPlayerSeasonsImageStatus(playersToUploadGraphics, PlayerSeason.WebImageStatusType.ReadyToUpload);

                            //
                            // Reset the player counter, so the counting will start again from zero.
                            //
                            playersToUploadGraphics.Clear();

                            //
                            // Start the worker thread, and wait a bit for it to go live.  You can only use a thread once apparently,
                            // so create a new one if the current one has already been used.
                            //
                            if (workerThread.ThreadState != System.Threading.ThreadState.Unstarted)
                            {
                                workerThread = new Thread(workerObject.DoUploadImagesWork);
                            }

                            workerThread.Start();

                            while (!workerThread.IsAlive)
                            {
                                Thread.Sleep(50);
                            }
                        }
                        else if (!workerThread.IsAlive)
                        {
                            uploadGraphicsLog(string.Format("Player counter: {0}", playersToUploadGraphics.Count));
                        }
                    }

                }

                result = true;
            }

            writer.Close();

            //
            // Update all the players in the original list as exported.
            //
            log("Processed players, updating db...");
            result = DataManager.SetPlayerSeasonsAsExported(players);
            log("Processed players, updating db... Done");

            //
            // Prolly wanna change this to copy to dropbox or some such
            //
            if (createGraphics && doActualUpload)
            {
                //
                // Wait for the "upload graphics" thread to join, as necessary.  Use a loop here so that the logging delegate
                // doesn't get blocked (the "Join" does that) which freezes the whole works.
                //
                while (workerThread.IsAlive)
                {
                    log("Waiting for upload thread to complete...");
                    Application.DoEvents();
                    Thread.Sleep(50);
                    Debug.WriteLine("Main thread sleeping...");
                }

                //
                // Finish up the rest of the images, if any.
                //
                if (playersToUploadGraphics.Count > 0)
                {
                    DataManager.SetPlayerSeasonsImageStatus(playersToUploadGraphics, PlayerSeason.WebImageStatusType.ReadyToUpload);
                }

                log("Gonna upload remainder of images, if necessary");
                int retryCounter = 0;

                //
                // If we originally wanted to get whole league, reset the franchise here so the following call gets the 
                // whole league and not just the last franchise that we processed above.
                //
                if (!havePassedFranchise)
                {
                    franchise = null;
                }

                while (!ApplicationManager.UploadMarketingImages(season, franchise, previouslyUnexportedPlayersOnly, uploadGraphicsLog, false))
                {
                    if (++retryCounter > 5)
                    {
                        throw new ApplicationException("Not all images could be uploaded... figure out the deal and use the \"Upload Images\" button.");
                    }
                    else
                    {
                        log("Not all images uploaded successfully, let's try again.");
                    }
                }
            }

            log(string.Format("Done, processed {0} players.", players.Count));


            return result;
        }

        public static string BuildPrimaryMarketingRemoteImageUrl(Season season, Franchise franchise, PlayerSeason player)
        {
            string leagueDesc = season.LeagueDescShort.ToLower();
            // string year = season.YearStarted.ToString();

            //
            // 2008-11-12 JB: let's not do the year.  By doing the year we have to redo all the images every season.
            //
            //string result = Functions.BuildUrlFromElements(Config.MarketingImagesServerRootUrl(),
            //    string.Format("{0}/{1}/{2}", leagueDesc, year, franchise.FranchiseDesc),
            //    BuildPlayerMarketingFilename(player));

            string result = Functions.BuildUrlFromElements(Config.MarketingImagesServerRootUrl(),
                string.Format("{0}/{1}", leagueDesc, franchise.FranchiseDesc),
                BuildPlayerMarketingFilename(player));

            return result;
        }

        public static string BuildRandomMarketingImageUrl(Template template)
        {
            return BuildRandomMarketingImageUrl(template, null);
        }

        public static string BuildRandomMarketingImageUrl(Template template, PlayerSeason player)
        {
            //
            // Generate a random marketing image with index from 1 through 5
            //
            Random random = new Random();
            int randomMarketingImageIndex = random.Next(1, 6);

            System.Diagnostics.Debug.WriteLine(string.Format("Random: {0}", randomMarketingImageIndex));

            if (player != null)
            {
                player.MarketingImageIndex = randomMarketingImageIndex;
            }

            string result = Functions.BuildUrlFromElements(Config.MarketingImagesServerRootUrl(),
                string.Format("mkt/mkt-t{0:000}-{1:000}.jpg", template.TemplateId, randomMarketingImageIndex));

            return result;
        }

        public static string BuildGalleryImageString(Season season, Franchise franchise, PlayerSeason player)
        {
            string imageUrl1 = BuildPrimaryMarketingRemoteImageUrl(season, franchise, player);

            string imageUrl2 = BuildRandomMarketingImageUrl(player.TemplateCurrent, player);

            return Functions.BuildPipedStringFromElements(imageUrl1, imageUrl2);
        }

        public static string BuildGalleryImageString(Template template)
        {
            string imageUrl1 = Functions.BuildUrlFromElements(Config.MarketingImagesServerRootUrl(),
                string.Format("custom/{0}-t{1:000}-anyname.gif", template.TemplateDescShort, template.TemplateId));

            string imageUrl2 = BuildRandomMarketingImageUrl(template);

            return Functions.BuildPipedStringFromElements(imageUrl1, imageUrl2);
        }

        /// <summary>
        /// Parse each line and try to return the results.  The line should have a number first, then a name.
        /// </summary>
        /// <param name="line">The line to parse.</param>
        /// <param name="franchise">The franchise object to apply to.</param>
        /// <param name="season">The season object to apply to.</param>
        /// <returns>null if line cannot be parsed.</returns>
        private static PlayerSeason GetPlayerSeasonFromDataString(string line, Franchise franchise, Season season, bool singleNamesAsFirst)
        {
            PlayerSeason player = null; 

            string[] fields = line.Split('"', '\t');

            if (fields.Length > 1)
            {
                string playerNumber = fields[0].Trim();
                string playerName = string.Empty;
                int count = 1;
                while ((playerName == string.Empty) && (count < fields.Length))
                {
                    playerName = fields[count].Trim().Replace("*", "");
                    count++;
                }

                if (Functions.IsNumeric(playerNumber) && playerName != string.Empty) // && playerName.Contains(","))
                {
                    //
                    // OK - line looks valid
                    //
                    player = new PlayerSeason();
                    player.JerseyNumber = playerNumber;
                    player.ParseFullName(playerName, singleNamesAsFirst);
                    player.FranchiseCode = franchise.FranchiseCode;
                    player.SeasonId = season.SeasonId;

                }
            }

            return player;
        }

        public static List<PlayerSeason> GetPlayerSeasonListFromFile(string fileName, Franchise franchise, Season season, out int skipped)
        {
            StreamReader reader = new StreamReader(fileName);
            List<PlayerSeason> players = new List<PlayerSeason>();
            string line;
            skipped = 0;

            while ((line = reader.ReadLine()) != null)
            {
                PlayerSeason player = GetPlayerSeasonFromDataString(line, franchise, season, false);

                if (player != null)
                {
                    players.Add(player);
                }
                else
                {
                    skipped++;
                }
            }
            
            reader.Close();

            return players;
        }

        public static List<PlayerSeason> GetPlayerSeasonListFromStringLines(string linesToParse, Franchise franchise, Season season, bool singleNamesAsFirst, out int skipped)
        {
            string[] lines = linesToParse.Split('\n');

            List<PlayerSeason> players = new List<PlayerSeason>();
            skipped = 0;

            foreach (string line in lines)
            {
                PlayerSeason player = GetPlayerSeasonFromDataString(line, franchise, season, singleNamesAsFirst);

                if (player != null)
                {
                    players.Add(player);
                }
                else
                {
                    skipped++;
                }
            }

            return players;
        }

        /// <summary>
        /// Calculate the order total and return a list of detailed items making up the order, including
        /// quantity discount, shipping, tax, etc.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static void BuildOrderSummary(Order order)
        {
            //
            // Delete everything but the order items
            //
            List<OrderItem> items = new List<OrderItem>();
            int quantity = 0;
            decimal subtotal = 0;
            decimal shippingCost = 0;
            decimal tax = 0;
            decimal volumeDiscountEa = 0;
            decimal promoCodeDiscount = 0;

            foreach (OrderItem item in order.Items)
            {
                if (item.CurrentOrderItemTypeCode == OrderItem.OrderItemTypeCode.oitcProduct)
                {
                    quantity += item.Quantity;
                    subtotal += (item.Quantity * item.Price);

                    items.Add(item);
                }
            }

            order.ShippingMethodDescShort = "USPS First Class";

            //
            // Calculate shipping. This is flat rate costs based on total guesswork.  Need to really figure this out using the UPS website.
            //
            if (quantity < 10)
            {
                if (order.Customer.ShippingAddress.Domestic)
                {
                    shippingCost = 2.75M;
                }
                else
                {
                    shippingCost = 4.75M;
                }
            }
            else if (quantity < 20)
            {
                if (order.Customer.ShippingAddress.Domestic)
                {
                    shippingCost = 3.75M;
                }
                else
                {
                    shippingCost = 7.50M;
                    order.ShippingMethodDescShort = "UPS";
                }

                volumeDiscountEa = 0.25M;
            }
            else if (quantity < 50)
            {
                if (order.Customer.ShippingAddress.Domestic)
                {
                    shippingCost = 9.00M;
                    order.ShippingMethodDescShort = "UPS";
                }
                else
                {
                    shippingCost = 15.00M;
                    order.ShippingMethodDescShort = "UPS";
                }

                volumeDiscountEa = 0.75M;
            }
            else
            {
                if (order.Customer.ShippingAddress.Domestic)
                {
                    shippingCost = 15.00M + (decimal)(0.15f * (quantity - 50));
                    order.ShippingMethodDescShort = "UPS";
                }
                else
                {
                    shippingCost = 25.00M + (decimal)(0.20f * (quantity - 50));
                    order.ShippingMethodDescShort = "UPS";
                }

                volumeDiscountEa = 1.25M;
            }

            if (order.Customer.BillingAddress.Taxable)
            {
                tax = (decimal)((float)subtotal * 0.0725f);
            }

            if (order.PromoCode >= 0)
            {
                // Do something with the promo code
            }

            //
            // Add summary items to end of list
            //
            items.Add(new OrderItem(OrderItem.OrderItemTypeCode.oitcShipping, 1, shippingCost));

            items.Add(new OrderItem(OrderItem.OrderItemTypeCode.oitcSalesTax, 1, tax));

            if (volumeDiscountEa > 0)
            {
                items.Add(new OrderItem(OrderItem.OrderItemTypeCode.oitcVolumeDiscount, quantity, volumeDiscountEa * -1));
            }

            // todo: wholesale discount

            // todo: promo code discount

            //
            // Update the items collection with the new list
            //
            order.Items = items;

            //
            // Let's get our new total
            //
            order.Total = 0M;
            order.TotalProducts = quantity;

            foreach (OrderItem item in items)
            {
                order.Total += item.ExtendedPrice;
            }

        }

        public static List<OrderItem> GetOrderItemsOfType(Order order, bool productItems)
        {
            List<OrderItem> result = new List<OrderItem>();

            foreach (OrderItem item in order.Items)
            {
                if (productItems && item.CurrentOrderItemTypeCode == OrderItem.OrderItemTypeCode.oitcProduct)
                {
                    result.Add(item);
                }
                else if (!productItems && item.CurrentOrderItemTypeCode != OrderItem.OrderItemTypeCode.oitcProduct)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static bool CreateMarketingImagesSubdirs(Season season, Franchise franchise, bool previouslyUnexportedPlayersOnly)
        {
            throw new ApplicationException("Not implemented yet - need to fix.");

            bool result = true;

            //List<PlayerSeason> players = DataManager.GetPlayerSeasons(season, franchise, previouslyUnexportedPlayersOnly);

            //string localRootDirectory = Functions.BuildUrlFromElements(Config.GraphicsRootDirectory(),
            //    Config.TemporaryMarketingGIFFilesDirectory(),
            //    Config.MarketingImagesLocalImageRootPath());
            
            //string leagueDesc = season.LeagueDescShort;
            //string year = season.YearStarted.ToString();
            //string franchiseDesc = franchise.FranchiseDesc;

            //// string.Format("{0}/{1}/{2}", leagueDesc, year, franchise.FranchiseDesc));

            //response = request2.GetResponse();

            //foreach (PlayerSeason player in players)
            //{
            //    string localFileName = BuildPlayerMarketingTemporaryFullPath(player);

            //    string remoteFileName = BuildPlayerMarketingImageFTPUrl(season, franchise, player);
            //    // BuildGalleryImageString(season, franchise, player);

            //    if (!File.Exists(localFileName))
            //    {
            //        WritePlayerMarketingGIFFile(player);
            //    }

            //    //
            //    // Upload the file
            //    //
                
            //    // request.Method = WebRequestMethods.Ftp.UploadFile();

            //    response = request.GetResponse();

                
            //}

            return result;
        }

        // public static 

        public static List<Order> GetOrdersFromMonsterXMLFile(string fileName, out string statusMsg)
        {
            List<Order> result = new List<Order>();
            statusMsg = string.Empty;

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            // Create an XmlNamespaceManager to resolve the default namespace.
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "urn:networksolutions:apis");      // "urn:newbooks-schema");

            XmlNodeList orderNodes = doc.SelectNodes("/bk:ReadOrderResponse/bk:OrderList", nsmgr);

            if (orderNodes.Count > 0)
            {
                foreach (XmlNode orderNode in orderNodes)
                {
                    // 
                    // Do stuff with the data here
                    //
                    Customer customer = new Customer();

                    //
                    // There has GOT to be a better way to loop through the child nodes.  I can't seem to figure out
                    // how to set, like, the "starting point" or something.  Each time I try to get just one order at a 
                    // time the XPath query returns ALL orders.  It doesn't make sense.
                    //
                    XmlDocExtra orderDoc = new XmlDocExtra(orderNode.OuterXml, "/bk:OrderList", nsmgr);
                    // orderDoc.LoadXml(orderNode.OuterXml);

                    XmlNodeList custNode = orderDoc.SelectNodes("/bk:OrderList/bk:Customer", nsmgr);

                    Address shippingAddress = new Address();

                    shippingAddress.FirstName = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:FirstName", nsmgr)[0].InnerText;
                    shippingAddress.LastName = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:LastName", nsmgr)[0].InnerText;
                    shippingAddress.Address1 = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:Address1", nsmgr)[0].InnerText;
                    shippingAddress.Address2 = orderDoc.GetFirstValueFromExtraPath("/bk:Customer/bk:ShippingAddress/bk:Address2");
                    shippingAddress.City = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:City", nsmgr)[0].InnerText;
                    shippingAddress.StateProvAbbrev = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:StateProvince", nsmgr)[0].InnerText;
                    shippingAddress.ZipPostalCode = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:PostalCode", nsmgr)[0].InnerText;
                    shippingAddress.CountryCode = orderDoc.SelectNodes("/bk:OrderList/bk:Customer/bk:ShippingAddress/bk:Country", nsmgr)[0].InnerText;
                    shippingAddress.Phone = orderDoc.GetFirstValueFromExtraPath("/bk:Customer/bk:ShippingAddress/bk:Phone");

                    shippingAddress.Country = DataManager.GetCountryFromAbbrev(shippingAddress.CountryCode, true);

                    customer.ShippingAddress = shippingAddress;

                    //
                    // Hmmmm... use the shipping address to get their name I guess?
                    //
                    customer.FirstName = shippingAddress.FirstName;
                    customer.LastName = shippingAddress.LastName;

                    Order order = new Order(customer);
                    order.OrderId = int.Parse(orderNode.Attributes["OrderNumber"].Value);

                    XmlNodeList orderItemNodes = orderDoc.SelectNodes("/bk:OrderList/bk:Invoice/bk:LineItemList", nsmgr);

                    //
                    // Process all items in the order.
                    //
                    foreach (XmlNode orderItemNode in orderItemNodes)
                    {
                        XmlDocument orderItemDoc = new XmlDocument();
                        orderItemDoc.LoadXml(orderItemNode.OuterXml);

                        //
                        // Note: template_id is numeric, the "PartNumber" in the XML file is of the format: 
                        //      "t{0:3}", template_id  (e.g. "t002" == 2)
                        //
                        string partNo = orderItemDoc.SelectNodes("/bk:LineItemList/bk:PartNumber", nsmgr)[0].InnerText;
                        int templateId = int.Parse(partNo.Substring(2));

                        //
                        // Todo - implement caching or something, this will hit the DB on every iteration.
                        //
                        Template template = DataManager.GetTemplate(templateId);

                        PlayerSeason playerSeason = new PlayerSeason();
                        XmlNodeList jerseyInformation = orderItemDoc.SelectNodes("/bk:LineItemList/bk:QuestionList/bk:TextAnswerList", nsmgr);
                        playerSeason.JerseyName = jerseyInformation[0].InnerText.ToUpper();
                        playerSeason.JerseyNumber = jerseyInformation[1].InnerText;

                        playerSeason.TemplateCurrent = template;

                        OrderItem orderItem = new OrderItem(playerSeason);
                        int quantity = int.Parse(orderItemDoc.SelectNodes("/bk:LineItemList/bk:QtySold", nsmgr)[0].InnerText);
                        orderItem.Quantity = quantity;

                        string monsterItemDesc = orderItemDoc.SelectNodes("/bk:LineItemList/bk:Name", nsmgr)[0].InnerText;
                        orderItem.DescriptionExternal = monsterItemDesc;

                        XmlNodeList colorInformation = orderItemDoc.SelectNodes("/bk:LineItemList/bk:QuestionList/bk:BooleanAnswerList", nsmgr);
                        string colorDesc = string.Empty;
                        for (int loop = 0; loop < colorInformation.Count; loop++)
                        {
                            if (orderItemDoc.SelectNodes("/bk:LineItemList/bk:QuestionList/bk:BooleanAnswerList/bk:Value", nsmgr)[loop].InnerText == "true")
                            {
                                colorDesc = orderItemDoc.SelectNodes("/bk:LineItemList/bk:QuestionList/bk:BooleanAnswerList/bk:Answer", nsmgr)[loop].InnerText;

                                // The default value in the Monster Commerce store items adds " (default)" to the end, gotta zap it or 
                                // DB lookup no workie.
                                colorDesc = colorDesc.Replace(" (default)", "");

                                // orderItem.Color = colorDesc;
                                break;
                            }
                        }

                        if (colorDesc == string.Empty)
                        {
                            // throw new ApplicationException("Oops, can't find a color answer!");
                            MessageBox.Show(string.Format("Oops, the order for \"{0}\" is missing the color answer!", playerSeason.JerseyName),
                                "No Color", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            colorDesc = "Gloss White";
                        }

                        Material material = DataManager.GetMaterialOfColorDesc(colorDesc);
                        if (material.Brightness == -1)
                        {
                            material.Brightness = ColorBrightness(material.RGBColorHex);
                        }

                        orderItem.Material = material;

                        order.Items.Add(orderItem);
                    }

                    result.Add(order);
                }
            }
            else
            {
                // MessageBox.Show("Found and opened the file, but no items were found!", "No Parse!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                statusMsg = "Found and opened the file, but no items were found.";
            }

            return result;
        }

        public static List<OrderDisplayable> BuildGridListFromOrderList(List<Order> orders)
        {
            List<OrderDisplayable> result = new List<OrderDisplayable>();

            foreach (Order order in orders)
            {
                int itemNo = 0;
                foreach (OrderItem orderItem in order.Items)
                {
                    OrderDisplayable orderDisplayable = new OrderDisplayable(order, itemNo++);
                    result.Add(orderDisplayable);
                }
            }

            return result;
        }

        /// <summary>
        /// Writes a CSV file containing the orders in a format importable into paypal multishipping tool.
        /// </summary>
        /// <param name="orders">The order list.</param>
        /// <param name="statusMsg">A status message.</param>
        /// <returns>True if it worked.</returns>
        public static bool CreatePaypalMultishipCSVFile(List<Order> orders, out string statusMsg, out string otherAddrsTxtFName)
        {
            bool resultPaypal = true;
            bool resultTxt = true;

            otherAddrsTxtFName = string.Empty;
            string outputFName = string.Format("{0}-{1:00}-{2:00}-multiship.csv",
                DateTime.Today.Year,
                DateTime.Today.Month,
                DateTime.Today.Day);

            outputFName = Functions.BuildFilenameFromElements(Config.PaypalMultiShipImportDir, outputFName);

            string outputFNameTxt = Functions.BuildFilenameFromElements(Config.PaypalMultiShipImportDir, "other.txt");

            statusMsg = string.Empty;

            List<Order> multishipElgibleOrders = new List<Order>();
            List<Order> otherOrders = new List<Order>();

            foreach (Order order in orders)
            {
                if (order.Customer.ShippingAddress.CountryCode == "US")
                {
                    multishipElgibleOrders.Add(order);
                }
                else
                {
                    otherOrders.Add(order);
                }
            }

            if (multishipElgibleOrders.Count > 0)
            {
                CSVSerializer csvWriter = new CSVSerializer(multishipElgibleOrders);

                csvWriter.AddCSVColumn("First Name", "Customer.FirstName");
                csvWriter.AddCSVColumn("Last Name", "Customer.LastName");
                csvWriter.AddCSVColumn("Company", "");
                csvWriter.AddCSVColumn("Street Address 1", "Customer.ShippingAddress.Address1");
                csvWriter.AddCSVColumn("Street Address 2", "Customer.ShippingAddress.Address2");
                csvWriter.AddCSVColumn("City", "Customer.ShippingAddress.City");
                csvWriter.AddCSVColumn("State", "Customer.ShippingAddress.StateProvAbbrev");
                csvWriter.AddCSVColumn("Zip Code", "Customer.ShippingAddress.ZipPostalCode");
                csvWriter.AddCSVColumn("Country", "Customer.ShippingAddress.CountryCode");
                csvWriter.AddCSVColumn("Phone Number", "Customer.ShippingAddress.Phone");
                csvWriter.AddCSVColumn("Order ID", "OrderId");
                csvWriter.AddCSVColumn("Item Number", "");  // todo: implement indexers - orders[0].Items[0]
                csvWriter.AddCSVColumn("Title", "");
                csvWriter.AddCSVColumn("Quantity", "");
                csvWriter.AddCSVColumn("Payment Date", "");
                csvWriter.AddCSVColumn("Custom Message", "Thank you for your order!");
                csvWriter.AddCSVColumn("Shipping Carrier", "USPS");
                csvWriter.AddCSVColumn("Insurance Value", "");

                resultPaypal = csvWriter.CreateCSV(outputFName);
            }

            if (otherOrders.Count > 0)
            {
                string outputStr = "Other Addresses - load these up in the MS Word Document\r\n\r\n";

                foreach(Order order in otherOrders)
                {
                    outputStr += string.Format("{0}\r\n{1}{2}\r\n{3}, {4} {5}\r\n{6}\r\n\r\n",
                        order.Customer.BuildFullName(),
                        order.Customer.ShippingAddress.Address1,
                        order.Customer.ShippingAddress.Address2 == string.Empty ? "" : "\r\n" + order.Customer.ShippingAddress.Address2,
                        order.Customer.ShippingAddress.City,
                        order.Customer.ShippingAddress.StateProvAbbrev,
                        order.Customer.ShippingAddress.ZipPostalCode,
                        order.Customer.ShippingAddress.Country);
                }

                if (Functions.WriteStringAsFile(outputStr, outputFNameTxt))
                {
                    otherAddrsTxtFName = outputFNameTxt;
                }
            }

            return (resultPaypal && resultTxt);
        }

        /// <summary>
        /// Get an initialized Chilkat Mailman component.
        /// </summary>
        /// <returns>Null if it doesn't work.  Only can fail if component can't be unlocked.</returns>
        public static MailMan GetLeadMailman()
        {
            MailMan result = null;
            MailMan mailman = new MailMan();

            //  Any string argument automatically begins the 30-day trial.
            bool success;
            // success = mailman.UnlockComponent(Config.ChilkatUnlockCodeEmail);
            success = mailman.UnlockComponent(ChilkatRegCodes.ChilkatEmail);
            if (success == true)
            {
                //    resultMsg = "Chilkat Email: component unlock failed";
                //    return result;
                //}

                //  Set the POP3 server's hostname
                mailman.MailHost = Config.LeadMailboxServer;        // "mail.chilkatsoft.com";

                //  Set the POP3 login/password.
                mailman.PopUsername = Config.LeadMailboxLoginUID;       // "myLogin";
                mailman.PopPassword = Config.LeadMailboxLoginPW;        // "myPassword";

                result = mailman;
            }
            return result;
        }

        /// <summary>
        /// Get the lead emails using the passed connection.
        /// </summary>
        /// <param name="mailman">An initialized (unlocked, mail server params set) Chilkat component.</param>
        /// <param name="resultMsg">Result message.</param>
        /// <returns>The lead emails.</returns>
        public static EmailBundle GetLeadEmails(MailMan mailman, out string resultMsg)
        {
            //  The mailman object is used for receiving (POP3)
            //  and sending (SMTP) email.

            // Look here for more examples: http://www.example-code.com/csharp/pop3.asp

            EmailBundle result = null;
            resultMsg = string.Empty;

            //  Copy the all email from the user's POP3 mailbox
            //  into a bundle object.  The email remains on the server.
            result = mailman.CopyMail();

            if (result == null)
            {
                resultMsg = mailman.LastErrorText;
            }

            return result;
        }

        /// <summary>
        /// Parse through the body of the lead's email and set the various relevant properties.
        /// </summary>
        /// <param name="lead">The FundraisingLead.  It must have the original email set into the LeadInfoText property.</param>
        public static void SetFundraisingIdeasProperties(Lead lead)
        {
            string plainTextBody = lead.LeadInfoText;

            // Set lead quality properties.  Hmmmm... this might be a good time for regular expressions?
            if (Functions.HasAnyWords(plainTextBody, "baseball", "base-ball", "softball", "little league", "little-league"))
            {
                lead.TemplateCurrent = DataManager.GetTemplate("Baseball");
            }
            else if (Functions.HasAnyWords(plainTextBody, "football", "foot-ball", "pee-wee", "pee wee"))
            {
                lead.TemplateCurrent = DataManager.GetTemplate("Football");
            }
            else if (Functions.HasAnyWords(plainTextBody, "basketball", "basket-ball", "basket ball"))
            {
                lead.TemplateCurrent = DataManager.GetTemplate("Basketball");
            }
            else if (Functions.HasAnyWords(plainTextBody, "hockey"))
            {
                lead.TemplateCurrent = DataManager.GetTemplate("Hockey");
            }
            else if (Functions.HasAnyWords(plainTextBody, "soccer", "ayso"))
            {
                lead.TemplateCurrent = DataManager.GetTemplate("Soccer");
            }

            lead.IsAthletics = (lead.TemplateCurrent != null);

            if (Functions.HasAnyWords(plainTextBody, "school", "college", "university"))
            {
                lead.IsSchool = true;
            }

            string temp;

            if ((temp = Functions.GetValueFromText(plainTextBody, "Best Contact Method: (.*)")) != string.Empty)
            {
                lead.ParseContactMethodDescription(temp);
            }

            //
            // Try to get a phone if we don't already have one.
            //
            if (Functions.IsEmptyString(lead.PhoneDay))
            {
                lead.PhoneDay = Functions.GetValueFromText(plainTextBody, "Phone: (.*)");
            }

        }

        /// <summary>
        /// Remove observed strangeness from names that are retreived from various possibly unreliable sources.
        /// </summary>
        /// <remarks>
        /// Example fixes:
        ///     Input:      " Derek Tehrani <magicmoneymkt@yahoo.com>"
        ///     Output:     "Derek Tehrani"
        /// </remarks>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string FixName(string name)
        {
            // result = name.Trim();

            string result = Regex.Replace(name.Trim(), "<.*>", "").Trim();

            return result;

        }

        /// <summary>
        /// Parse out the special fields from a FundraisingIdeas email
        /// </summary>
        /// <param name="email">The Chilkat email object</param>
        /// <returns>The resulting FundraisingLead, or null if it's not a lead email.</returns>
        public static Lead GetLeadFromFundraisingIdeasEmail(Email email)
        {
            Lead lead = null;

            if (email.Subject.Contains("Your Request to the "))
            {
                lead = new Lead();

                lead.ParseFullName(FixName(email.FromName));

                if (Functions.IsEmptyString(lead.BuildFullName()))
                {
                    lead.FirstName = "Future Fundraiser";
                }

                lead.EmailAddress = email.FromAddress;
                lead.LeadDate = email.EmailDate;
                lead.LeadSourceCode = 1;    //  Lead.LeadSourceCodeType.lscFundraisingIdeasWebsite;

                lead.LeadInfoText = email.GetPlainTextBody();

                //
                // Parse the email body for keywords and such.  Good times.
                //
                SetFundraisingIdeasProperties(lead);
            }
            else
            {
                // Not a lead
            }

            return lead;
        }

        public static void OnFtpPercentDone(object source, Chilkat.FtpPercentDoneEventArgs args)
        {
            // progressBar1.Value = args.PercentDone;
        }

        /// <summary>
        /// Uploads the marketing images for players in a season.  Marketing images must already exist in the temp directory.
        /// </summary>
        /// <param name="season">The season to find players for.</param>
        /// <param name="franchise">(Optional) Narrow results to a specific franchise.</param>
        /// <param name="newPlayersOnly">Only previously unexported players.</param>
        /// <param name="log">Log delegate function to update with status.</param>
        /// <param name="ignoreDB">Do NOT check if image has been uploaded already (forces rewrite of image).</param>
        /// <returns>True if all qualified players images were uploaded successfully.</returns>
        public static bool UploadMarketingImages(Season season, Franchise franchise, bool newPlayersOnly, LogDelegate log, bool ignoreDB)
        {
            // bool result = false;
            // bool hadError = false;
            log("Getting players from db...");

            List<PlayerSeason> players = DataManager.GetPlayerSeasons(season, franchise, newPlayersOnly, !ignoreDB);
            List<PlayerSeason> successfulUpload = new List<PlayerSeason>();

            if (players != null && players.Count > 0)
            {
                int currentFranchiseCode = -1;

                log(string.Format("Got {0} players, starting to upload...", players.Count));

                Chilkat.Ftp2 ftp = new Chilkat.Ftp2();
                bool success;

                //  Any string unlocks the component for the 1st 30-days.
                success = ftp.UnlockComponent(ChilkatRegCodes.ChilkatFTP2);
                if (success != true)
                {
                    log(ftp.LastErrorText);
                    return false;
                }

                ftp.Hostname = Config.MarketingImagesFTPServer;
                ftp.Username = Config.MarketingImagesFTPUserId;
                ftp.Password = Config.MarketingImagesFTPPw;

                //
                // The default data transfer mode is "Active" as opposed to "Passive".  If you use "Active",  the 
                // upload below generates a Windows Firewall "Do you wanna allow?" message, but the client proceeds as if all
                // was ok (even though the transfer fails if you don't click "Yes" fast enough).  That's BAD, because
                // then the image is missing from the server and the database will say that all is well.
                //
                //  Good explanation: http://www.slacksite.com/other/ftp.html
                //
                ftp.Passive = true;

                //  Connect and login to the FTP server.
                success = ftp.Connect();
                if (success != true)
                {
                    log(ftp.LastErrorText);
                    return false;
                }

                foreach (PlayerSeason playerSeason in players)
                {
                    //
                    // Get some extra data we might need
                    //
                    if ((franchise == null) || (currentFranchiseCode != playerSeason.FranchiseCode))
                    {
                        currentFranchiseCode = playerSeason.FranchiseCode;
                        franchise = DataManager.GetFranchise(currentFranchiseCode);
                    }

                    string localFileName = BuildPlayerMarketingTemporaryFullPath(season, franchise, playerSeason);
                    if (!File.Exists(localFileName))
                    {
                        log(string.Format("Expected to find local file, but didn't: \"{0}\"", localFileName));
                        break;
                    }

                    string remoteFilePath = BuildPlayerMarketingImageFTPPath(playerSeason, franchise);
                    string remoteFileName = BuildPlayerMarketingFilename(playerSeason);

                    log(string.Format("Uploading: {0}", remoteFileName));

                    //
                    // Change to the remote directory where the file will be uploaded.
                    //
                    success = ftp.ChangeRemoteDir(remoteFilePath);

                    if (success != true)
                    {
                        //
                        // Prolly no remote directory exists?
                        //
                        if (ftp.LastErrorText.IndexOf("The system cannot find the file specified") > 0)
                        {
                            //
                            // Try to create the dir
                            //
                            success = ftp.CreateRemoteDir(remoteFilePath);

                            if (success)
                            {
                                success = ftp.ChangeRemoteDir(remoteFilePath);
                            }
                        }
                        else
                        {
                            //
                            // Some other error, let's skip this guy.
                            //
                        }
                    }

                    if (success == true)
                    {
                        //  Upload a file. 
                        success = ftp.PutFile(localFileName, remoteFileName);

                        //
                        // Double-check that the file is really there, sometimes "success" is wrong!  Typically
                        // it's incorrect for the first file in a session.
                        //
                        if (success == true && ftp.GetSizeByName(remoteFileName) > 0)
                        {
                            successfulUpload.Add(playerSeason);
                        }
                    }
                }

                ftp.Disconnect();

                if (successfulUpload.Count > 0)
                {
                    log(string.Format("Uploaded {0} images successfully, updating database...", successfulUpload.Count));
                    if (!DataManager.SetPlayerSeasonsImageStatus(successfulUpload, PlayerSeason.WebImageStatusType.Uploaded))
                    {
                        throw new ApplicationException("Yikes, unable to call 'DataManager.SetPlayerSeasonsImageStatus' successfully!");
                    }
                }

                log(string.Format("Done, uploaded {0} images successfully...", successfulUpload.Count));
            }
            else
            {
                log("Nothing to do...");
            }

            return (successfulUpload.Count == players.Count);
        }

        /// <summary>
        /// Todo: finish this.  It's supposed to create the spreadsheet from the database.
        /// </summary>
        /// <param name="numberOfTeams"></param>
        /// <param name="league"></param>
        /// <returns></returns>
        public static bool GenerateMarketingMessageBoardsWorksheetCSV(int numberOfTeams, League league)
        {
            if (league == null)
            {
                league = new League();
                league.LeagueCode = 7;      // NFL
            }

            Season season = DataManager.GetSeasonsForLeague(league, true)[0];

            List<Franchise> teams = DataManager.GetFranchisesForSeason(season);

            for (int i = 0; i < numberOfTeams; i++)
            {
            }

            return false;
        }





    // ********************************************************************************
    // 
    // End of class
    //
    }
}
