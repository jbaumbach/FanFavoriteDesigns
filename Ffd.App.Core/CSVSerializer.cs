using Ffd.Common;
using Ffd.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Collections;

namespace Ffd.App.Core
{
    /// <summary>
    /// Class to take a generic list of objects, and the columns (object properties) you specify, and output them to a CSV file. 
    /// </summary>
    public class CSVSerializer
    {
        private List<object> _dataSource = null;
        private List<string> _columns = new List<string>();

        /// <summary>
        /// Set the passed list as the list to use for outputting the CSV.
        /// </summary>
        /// <param name="objects">A list of object to use.</param>
        public void SetList(IList objects)
        {
            _dataSource = new List<object>();
            foreach(Object item in objects)
            {
                _dataSource.Add(item);
            }
        }

        /// <summary>
        /// Add a column name to the csv file, specifying the corresponding property name.
        /// </summary>
        /// <param name="outputDesc">The output CSV column name.</param>
        /// <param name="dataSourceProperty">The property can be of arbitrary depth, like "Customer.ShippingAddress.ZipPostalCode"</param>
        public void AddCSVColumn(string outputDesc, string dataSourceProperty)
        {
            _columns.Add(string.Format("{0}|{1}", outputDesc, dataSourceProperty));
        }

        /// <summary>
        /// Creates the CSV file.
        /// </summary>
        /// <param name="outputFName">The file to write to.</param>
        /// <returns>Pretty much always true.  Raises error if something bad happens.</returns>
        public bool CreateCSV(string outputFName)
        {
            return CreateCSV(outputFName, null);
        }

        /// <summary>
        /// Creates the CSV file.
        /// </summary>
        /// <param name="outputFName">The file to write to.</param>
        /// <param name="log">(Optional) A callback delegate that can be called to receive update events.  Null if nothing.</param>
        /// <returns>Pretty much always true.  Raises error if something bad happens.</returns>
        public bool CreateCSV(string outputFName, ApplicationManager.LogDelegate log)
        {
            if (_dataSource == null)
            {
                throw new ApplicationException("Come on man, you never set the List!");
            }

            string quote = "\"";
            string twoQuotes = "\"\"";
            string separator = ",";

            StreamWriter sw = new StreamWriter(outputFName);
            string headerRow = string.Empty;
            int rowNum = 0;

            foreach (object row in _dataSource)
            {
                int colNum = 0;
                string dataRow = string.Empty;
                bool headerRowProcessed = !(headerRow == string.Empty);

                foreach (string column in _columns)
                {
                    string[] vals = column.Split('|');
                    string propertyVal = string.Empty;

                    if (vals[1].Trim() != string.Empty)
                    {
                        //
                        // Get the property value from the class.   If the value is in sub-object(s),
                        // loop through the property names and get each sub-object in turn.
                        //
                        string[] propList = vals[1].Split('.');
                        object temp = row;

                        try
                        {
                            for (int loop = 0; loop < propList.Length; loop++) //  each (string propName in propList)
                            {
                                System.Reflection.PropertyInfo pi = temp.GetType().GetProperty(propList[loop]);
                                temp = pi.GetValue(temp, null);
                            }

                            propertyVal = temp.ToString();
                        }
                        catch (Exception e)
                        {
                            // Prop not found or a default value?
                            propertyVal = vals[1];
                        }
                    }

                    Debug.WriteLine(string.Format("row: {0}, col {1}, name: {2} has value {3}",
                        rowNum,
                        colNum++,
                        vals[0],
                        propertyVal));

                    if (!headerRowProcessed)
                    {
                        headerRow = Functions.BuildStringFromElementsWithDelimiter(headerRow, string.Format("\"{0}\"", vals[0]), separator);
                    }

                    propertyVal = propertyVal.Replace(quote, twoQuotes);
                    dataRow = Functions.BuildStringFromElementsWithDelimiter(dataRow, string.Format("\"{0}\"", propertyVal), separator);
                }
                rowNum++;

                if (!headerRowProcessed)
                {
                    sw.WriteLine(headerRow);
                }

                sw.WriteLine(dataRow);

                if (log != null)
                {
                    log(rowNum.ToString());
                }
            }

            sw.Close();

            return true;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CSVSerializer()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objects">The list of objects to use.</param>
        public CSVSerializer(IList objects)
        {
            SetList(objects);
        }
    }
}
