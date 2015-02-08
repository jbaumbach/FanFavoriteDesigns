using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Ffd.Data
{
    public class AttributeTableDataSet : System.Data.DataSet
    {

        private string _filterColumnName;
        private string _dataColumnName;
        
        /// <summary>
        /// The column name of the code, typically the primary key (e.g. "template_attr_type_code").
        /// </summary>
        protected string FilterColumnName
        {
            get { return _filterColumnName; }
            set { _filterColumnName = value; }
        }

        /// <summary>
        /// The column name of the value to retrieve, typically the variant column (e.g. "template_attr_data").
        /// </summary>
        protected string DataColumnName
        {
            get { return _dataColumnName; }
            set { _dataColumnName = value; }
        }

        /// <summary>
        /// Searches the dataset for a row that matches the filter, then returns the value.  Note: if there
        /// are more than 1 matching rows, returns the data from the first one.
        /// </summary>
        /// <param name="uniqueRowValue">The value of the FilterColumnName to get - typically an integer but could be anything.</param>
        /// <returns>object (might be null)</returns>
        public object GetValueFromRowSet(object uniqueRowValue)
        {
            object result = null;
            string rowSetFilter = string.Format("{0} = {1}", _filterColumnName, uniqueRowValue);

            if (this.Tables.Count == 0)
            {
                throw new ApplicationException("There are no tables - has this DataSet been initialized?");
            }

            if ((_filterColumnName == string.Empty) || (_dataColumnName == string.Empty))
            {
                throw new ApplicationException("Either the filter column or data column names are empty.  Cannot continue.");
            }

            DataRow[] rows = this.Tables[0].Select(rowSetFilter);

            if (rows.Length > 0)
            {
                result = rows[0][_dataColumnName];         //.ToString();
            }

            //
            // For now, always throw descriptive error if we can't find a value.  WAAAAY better then
            // lame 'object is null' error prolly thrown by the calling function that tells you nothing.
            //
            if (result == null)
            {
                throw new ApplicationException(string.Format("Could not retrieve value from database for filter \"{0}\" and column name \"{1}\".", rowSetFilter, _dataColumnName));
            }

            return result;
        }

        public void GetQueryResults(string sql, params SqlParameter[] sqlParams)
        {
            DataManager.GetQueryResults(this, sql, sqlParams);
        }

        ///// <summary>
        ///// Fill this DataSet object with the results of the database call.
        ///// </summary>
        ///// <param name="sql">SQL command to execute</param>
        ///// <param name="sqlParams">Pameters to pass to sql command (if any)</param>
        //public void GetQueryResults(string sql, params SqlParameter[] sqlParams)
        //{
        //    //
        //    // Connect to the database
        //    //
        //    SqlConnection connection = new SqlConnection(ConnectionString());
        //    connection.Open();

        //    //
        //    // Specify the command to use to query the database
        //    //
        //    SqlCommand command = new SqlCommand(sql, connection);

        //    //
        //    // Add parameters if we have any
        //    //
        //    foreach (SqlParameter parameter in sqlParams)
        //    {
        //        command.Parameters.Add(parameter);
        //    }

        //    //
        //    // Connect to db and run the query
        //    //
        //    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

        //    dataAdapter.Fill(this);
        //}

        ///// <summary>
        ///// Gets the connection string for this computer to the database
        ///// </summary>
        ///// <returns>Connection string</returns>
        //public static string ConnectionString()
        //{
        //    return string.Format("Data Source=ergosql2;Initial Catalog=FFDMAIN;Integrated Security=true");
        //}

        #region Constructors
        public AttributeTableDataSet() : base() { }

        public AttributeTableDataSet(string sqlCommand) : base()
        {
            GetQueryResults(sqlCommand);
        }
        #endregion

    }

}
