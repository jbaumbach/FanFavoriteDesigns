using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Ffd.Common
{
    static public class Functions
    {
        /// <summary>
        /// Returns the value of "percentage" of "number", rounded down.
        /// </summary>
        /// <param name="percentage">The percentage to get.</param>
        /// <param name="number">The total number.</param>
        /// <returns>Integer</returns>
        public static int GetPercentOfNumber(int percentage, int number)
        {
            return (int)(((float)percentage / 100f) * number);
        }

        /// <summary>
        /// Concatenate two strings with a delimiter inbetwixt.  I just wanted to use the word inbetwixt.  Hopefully that's a word.
        /// </summary>
        /// <param name="element">The first string.</param>
        /// <param name="element2">The second string.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The built string.</returns>
        public static string BuildStringFromElementsWithDelimiter(string element, string element2, string delimiter)
        {
            return string.Format("{0}{1}{2}", element, ((element == string.Empty) || element.EndsWith(delimiter) || element2.StartsWith(delimiter)) ? "" : delimiter, element2);
        }

        /// <summary>
        /// Concatenate two strings with a delimiter inbetwixt.  I just wanted to use the word inbetwixt.  Hopefully that's a word.
        /// </summary>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="elements">Zero or more strings.</param>
        /// <returns>The built string.</returns>
        public static string BuildQueryStringFromElements(params string[] elements)
        {
            string result = string.Empty;

            foreach (string element in elements)
            {
                result = BuildStringFromElementsWithDelimiter(result, element, "&");
            }

            return result;

        }

        /// <summary>
        /// Builds a file name from the passed elements, handling the file delimiters.
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static string BuildFilenameFromElements(params string[] elements)
        {
            string result = string.Empty;

            foreach (string element in elements)
            {
                result = BuildStringFromElementsWithDelimiter(result, element, "\\"); // BuildFilenameFromElements(result, element);
            }

            return result;
        }

        //public static string BuildFilenameFromElements(string element, string element2)
        //{
        //    // return string.Format("{0}{1}{2}", element, ((element == string.Empty) || element.EndsWith("\\") || element2.StartsWith("\\")) ? "" : "\\", element2);
        //    return BuildStringFromElementsWithDelimiter(element, element2, "\\");
        //}

        public static string BuildUrlFromElements(params string[] elements)
        {
            string result = string.Empty;

            foreach (string element in elements)
            {
                result = BuildStringFromElementsWithDelimiter(result, element, "/");
            }

            return result;
        }

        /// <summary>
        /// Return string built from passed string elements separated with "|" character.
        /// </summary>
        /// <param name="elements">The strings to concatenate.</param>
        /// <returns>Resulting string.</returns>
        public static string BuildPipedStringFromElements(params string[] elements)
        {
            string result = string.Empty;

            foreach (string element in elements)
            {
                result = BuildStringFromElementsWithDelimiter(result, element, "|");
            }

            return result;
        }

        /// <summary>
        /// To use this method and retrieve the correct encoder, simply pass an ImageFormat for the format you 
        /// want to save the image as, and then pass the encoder to the Image.Save overload.
        /// </summary>
        /// <param name="format">The image format you want.</param>
        /// <returns>A codec suitable for saving images in the format you want.</returns>
        public static ImageCodecInfo FindEncoder(ImageFormat format)
        {

            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageEncoders())
            {
                if (codec.FormatID.Equals(format.Guid))
                {
                    return codec;
                }
            }

            return null;
        }

        /// <summary>
        /// Return an array of encoder parameters with a single parameter for color depth
        /// </summary>
        /// <param name="colorDepth">Long value of color depth.</param>
        /// <returns>EncoderParameters with color depth value in it.</returns>
        public static EncoderParameters GetColorDepthEncoderParameters(long colorDepth)
        {
            EncoderParameters result = new EncoderParameters(1);
            result.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, colorDepth);

            return result;
        }

        /// <summary>
        /// Determine if passed email addressed is valid or not.
        /// </summary>
        /// <param name="emailAddress">The email address to test.</param>
        /// <returns>True if email address is valid, false otherwise.</returns>
        public static bool IsValidEmailAddress(string emailAddress)
        {
            Regex reg = new Regex("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$");

            return reg.IsMatch(emailAddress.ToUpper());
        }

        /// <summary>
        /// Test passed value for a valid number or not.  Why C# doesn't have this built-in, I dunno.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            int temp;
            return int.TryParse(value, out temp);
        }

        /// <summary>
        /// Copy all the items from the passed ICollection into the passed combobox.
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="dataObjects"></param>
        public static void SetItemsIntoDropdownWithExtraDefaultValue(ComboBox comboBox, ICollection dataObjects)
        {
            if (dataObjects.Count > 1)
            {
                ArrayList comboItems = new ArrayList();

                comboItems.Add("[select]");
                comboItems.AddRange(dataObjects);

                comboBox.DataSource = comboItems;
            }
            else
            {
                comboBox.DataSource = dataObjects;
            }
        }

        /// <summary>
        /// Tests a string for emptiness or null.
        /// </summary>
        /// <param name="theString">String to test.</param>
        /// <returns>The results.</returns>
        public static bool IsEmptyString(string theString)
        {
            return ((theString == string.Empty) || (theString == null));
        }

        ///// <summary>
        ///// Convert an enumeration into an array of the enumeration items you can loop through.  (not tested yet)
        ///// </summary>
        ///// <typeparam name="TEnum">A enumeration, such as "TemplateTypeIds"</typeparam>
        ///// <returns>An array of the enumerated members.</returns>
        //public static IEnumerable<TEnum> GetEnumItems<TEnum>()
        //{

        //    var enumType = typeof(TEnum);

        //    if (enumType == typeof(Enum))

        //        throw new ArgumentException("typeof(TEnum) == System.Enum", "TEnum");

        //    if (!(enumType.IsEnum))

        //        throw new ArgumentException(String.Format("typeof({0}).IsEnum == false", enumType), "TEnum");

        //    return Enum.GetValues(enumType).OfType<TEnum>();

        //}

        /// <summary>
        /// Search the passed string for any of the passed words or phrases.
        /// </summary>
        /// <param name="sourceString">Source string.</param>
        /// <param name="wordsToFind">Words to find.</param>
        /// <returns>True if found.</returns>
        public static bool HasAnyWords(string sourceString, params string[] wordsToFind)
        {
            foreach (string word in wordsToFind)
            {
                if (sourceString.IndexOf(word) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a value from some text if the value cn be defined by a regular expression.
        /// </summary>
        /// <param name="sourceText">The source text to search (like an entire email body).</param>
        /// <param name="patternToMatch">The pattern to find (e.g. "Best Contact Method: (.*)"). You need to have at least one value in parens.</param>
        /// <returns>The first value found in the string.</returns>
        public static string GetValueFromText(string sourceText, string patternToMatch)
        {
            List<string> values = GetValueFromTextMulti(sourceText, patternToMatch);
            string result = string.Empty;
            if (values.Count > 0)
            {
                result = values[0];
            }
            return result;
        }

        /// <summary>
        /// Gets value(s) from some text if the value can be defined by a regular expression
        /// </summary>
        /// <param name="sourceText">The source text to search (like an entire email body).</param>
        /// <param name="patternToMatch">The pattern to find (e.g. "Best Contact Method: (.*)"). You need to have at least one value in parens.</param>
        /// <returns>The values found in the string, if any, and they're trimmed.</returns>
        public static List<string> GetValueFromTextMulti(string sourceText, string patternToMatch)
        {
            List<string> result = new List<string>();

            Regex reg = new Regex(patternToMatch);      // "Best Contact Method: (.*)");
            Match match = reg.Match(sourceText);        // plainTextBody);

            for (int loop = 1; loop < match.Groups.Count; loop++)
            {
                result.Add(match.Groups[loop].ToString().Trim());
            }

            return result;
        }

        /// <summary>
        /// Get the first non-empty (nulls ok) string from the passed list and return it.
        /// </summary>
        /// <param name="strings">A bunch of strings.</param>
        /// <returns>The found string, or string.Empty if we have none.</returns>
        public static string FirstNonemptyString(params string[] strings)
        {
            string result = string.Empty;
            foreach (string candidate in strings)
            {
                if (!IsEmptyString(candidate))
                {
                    result = candidate;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts bool value into 0 or 1.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>1 if true, 0 if false.</returns>
        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        /// <summary>
        /// Write the passed string to the indicated file.
        /// </summary>
        /// <param name="contents">The string to write.</param>
        /// <param name="fileName">The full file name.  The directory(s) are created if necessary.</param>
        /// <returns>True.  Always.  Or an error is raised.</returns>
        public static bool WriteStringAsFile(string contents, string fileName)
        {
            return WriteStringAsFile(contents, fileName, false);
        }

        /// <summary>
        /// Write the passed string to the indicated file.
        /// </summary>
        /// <param name="contents">The string to write.</param>
        /// <param name="fileName">The full file name.  The directory(s) are created if necessary.</param>
        /// <param name="append">Adds to an existing file if it, ummm..., exists.</param>
        /// <returns>True.  Always.  Or an error is raised.</returns>
        public static bool WriteStringAsFile(string contents, string fileName, bool append)
        {
            CreateDirectoryIfNeeded(fileName);
            StreamWriter writer = new StreamWriter(fileName, append);
            writer.Write(contents);
            writer.Close();

            return true;
        }

        /// <summary>
        /// Creates the directory for the target file if it doesn't exist already.
        /// </summary>
        /// <param name="fileName">The full path to the file name.</param>
        public static void CreateDirectoryIfNeeded(string fileName)
        {
            string path = new FileInfo(fileName).DirectoryName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Title-case the passed string.
        /// </summary>
        /// <param name="name">The string to title-case.</param>
        public static string TitleCase(string name)
        {
            System.Globalization.TextInfo txt = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            return txt.ToTitleCase(name.ToLower());
        }
    }
}
