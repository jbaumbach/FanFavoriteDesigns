using System;
using System.Collections.Generic;
using System.Text;
using Ffd.Common;

namespace Ffd.Data
{
    /// <summary>
    /// A helper class to define each value to write to the SQL.
    /// </summary>
    public class TableFieldValue
    {
        public enum SQuoteBehavior
        {
            Auto = 0,
            NoSQuote = 1,
            YesSQuote = 2
        }

        /// <summary>
        /// Determines how to handle primary keys
        /// </summary>
        public enum SpecialColumnBehavior
        {
            Nothing = 0,

            /// <summary>
            /// Returns the SCOPE_IDENTITY() variable so you can get it with a dataset.
            /// </summary>
            ReturnTheValue = 1,

            /// <summary>
            /// Saves the SCOPE_IDENTITY() value into a local variable in the SQL command so you can
            /// use it later.
            /// </summary>
            SetVariableInScope = 2,

            /// <summary>
            /// Does both things.
            /// </summary>
            BothReturnAndSet = 3,

            /// <summary>
            /// Use this column as part of the where clause when updating.
            /// </summary>
            AddToWhereFilterForUpdate = 4
        }

        private string _fieldName;
        private object _fieldValue;
        private SQuoteBehavior _SQuoteIt = SQuoteBehavior.Auto;
        private bool _isPrimaryKey = false;
        private SpecialColumnBehavior _scopeIdentity = SpecialColumnBehavior.Nothing;

        // private bool _nullable;

        /// <summary>
        /// The name of the field in the table (e.g. "player_lname").
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        /// <summary>
        /// The value to set.  (todo: make this an object and then autoselect squotes)
        /// </summary>
        public object FieldValue
        {
            get { return _fieldValue; }
            set { _fieldValue = value; }
        }

        /// <summary>
        /// Whether to put quotes around the field or not (if it's a string or date, then yes).
        /// </summary>
        public SQuoteBehavior SQuoteIt
        {
            get { return _SQuoteIt; }
            set { _SQuoteIt = value; }
        }

        public override string ToString()
        {
            return _fieldName;
        }

        /// <summary>
        /// For future use - to test db for existance for rows to autoset the insert/auto property of the SQLGen class.
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }

        public SpecialColumnBehavior ScopeIdentity
        {
            get { return _scopeIdentity; }
            set { _scopeIdentity = value; }
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldName">The name of the field in the table (e.g. "player_lname").</param>
        /// <param name="fieldValue">The value to set.</param>
        public TableFieldValue(string fieldName, object fieldValue) : this(fieldName, fieldValue, SQuoteBehavior.Auto)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldName">The name of the field in the table (e.g. "player_lname").</param>
        public TableFieldValue(string fieldName, SpecialColumnBehavior scopeIdentity)
        {
            _fieldName = fieldName;
            _scopeIdentity = scopeIdentity;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldName">The name of the field in the table (e.g. "player_lname").</param>
        /// <param name="fieldValue">The value to set.</param>
        /// <param name="sQuoteIt">Whether to put quotes around the field or not (if it's a string or date, then yes; default is no).</param>
        public TableFieldValue(string fieldName, object fieldValue, SQuoteBehavior squoteIt)
        {
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _SQuoteIt = squoteIt;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldName">The name of the field in the table (e.g. "player_lname").</param>
        /// <param name="fieldValue">The value to set.</param>
        /// <param name="behavior">Determines column behavior - typically this overload is for adding params to the where clause of an update.</param>
        public TableFieldValue(string fieldName, object fieldValue, SpecialColumnBehavior behavior)
        {
            _fieldName = fieldName;
            _fieldValue = fieldValue;
            _scopeIdentity = behavior;
        }

    }

    /// <summary>
    /// Used to easily generate insert/update SQL.
    /// </summary>
    public class SQLGenerator
    {
        private string _table = string.Empty;
        private List<TableFieldValue> _values = new List<TableFieldValue>();
        private DataManager.ObjectWriteMode _writeMode = DataManager.ObjectWriteMode.Insert;
        //private string _newIdName;
        private string _identityReturnName = string.Empty;
        private string _identityScopeVarName = string.Empty;

        /// <summary>
        /// The table name.
        /// </summary>
        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        /// <summary>
        /// Write mode, like insert or update.
        /// </summary>
        public DataManager.ObjectWriteMode WriteMode
        {
            get { return _writeMode; }
            set { _writeMode = value; }
        }

        public string IdentityReturnName
        {
            get { return _identityReturnName; }
        }

        public string IdentityScopeVarName
        {
            get { return _identityScopeVarName; }
        }

        ///// <summary>
        ///// If set, append SQL to get a new identity value into this variable name using SCOPE_IDENTITY()
        ///// </summary>
        //public string NewIdName
        //{
        //    get { return _newIdName; }
        //    set { _newIdName = value; }
        //}

        /// <summary>
        /// Add a field to be generated.
        /// </summary>
        /// <param name="fieldValue">Object containing the field data.</param>
        public void AddFieldValue(TableFieldValue fieldValue)
        {
            _values.Add(fieldValue);
        }

        /// <summary>
        /// Determine if the passed field needs to be squoted or not.
        /// </summary>
        /// <param name="fieldValue">The field value to test.</param>
        /// <returns>True if it does, false otherwise.</returns>
        private static bool NeedToSQuote(TableFieldValue fieldValue)
        {
            return (fieldValue.SQuoteIt != TableFieldValue.SQuoteBehavior.NoSQuote &&
                            (fieldValue.FieldValue.GetType() == typeof(string) ||
                            fieldValue.FieldValue.GetType() == typeof(DateTime) ||
                            fieldValue.SQuoteIt == TableFieldValue.SQuoteBehavior.YesSQuote)
                            );
        }

        /// <summary>
        /// Generate the SQL.
        /// </summary>
        /// <param name="mode">The write mode to generate.</param>
        /// <returns>A SQL string.</returns>
        public string GenerateSQL(DataManager.ObjectWriteMode mode)
        {
            string sql = string.Empty;

            if (mode == DataManager.ObjectWriteMode.Insert)
            {
                string scopeIdentity = string.Empty;
                string fieldNames = string.Empty;
                string fieldValues = string.Empty;

                foreach (TableFieldValue fieldValue in _values)
                {
                    if (fieldValue.ScopeIdentity != TableFieldValue.SpecialColumnBehavior.Nothing)
                    {
                        if (fieldValue.ScopeIdentity == TableFieldValue.SpecialColumnBehavior.ReturnTheValue ||
                            fieldValue.ScopeIdentity == TableFieldValue.SpecialColumnBehavior.BothReturnAndSet)
                        {
                            _identityReturnName = string.Format("new_{0}", fieldValue.FieldName);
                            scopeIdentity += string.Format("select SCOPE_IDENTITY() as '{0}' \r\n\r\n", _identityReturnName);
                        }

                        if (fieldValue.ScopeIdentity == TableFieldValue.SpecialColumnBehavior.SetVariableInScope ||
                            fieldValue.ScopeIdentity == TableFieldValue.SpecialColumnBehavior.BothReturnAndSet)
                        {
                            _identityScopeVarName = string.Format("@new_{0}", fieldValue.FieldName);
                            scopeIdentity += string.Format("declare {0} BIGINT\r\nselect {0} = SCOPE_IDENTITY()\r\n\r\n", _identityScopeVarName);
                        }
                    }
                    else
                    {
                        fieldNames += string.Format("{0}{1}", fieldNames == string.Empty ? "" : ", ", fieldValue.FieldName);

                        string value;

                        if (fieldValue.FieldValue == null)
                        {
                            // set to default in case of min val?  As of now, no. || (fieldValue.FieldValue.GetType() == typeof(DateTime) && (DateTime)fieldValue.FieldValue == DateTime.MinValue)
                            value = "null";
                        }
                        //else if (fieldValue.SQuoteIt != TableFieldValue.SQuoteBehavior.NoSQuote &&
                        //    (fieldValue.FieldValue.GetType() == typeof(string) ||
                        //    fieldValue.FieldValue.GetType() == typeof(DateTime) ||
                        //    fieldValue.SQuoteIt == TableFieldValue.SQuoteBehavior.YesSQuote)
                        //    )
                        else if (NeedToSQuote(fieldValue))
                        {
                            value = DataManager.SQuote(fieldValue.FieldValue.ToString());
                        }
                        else
                        {
                            value = fieldValue.FieldValue.ToString();
                        }

                        fieldValues += string.Format("{0}{1}", fieldValues == string.Empty ? "" : ", ", value);
                    }
                }

                sql = string.Format("INSERT INTO {0} ({1}) \r\n VALUES ({2}) \r\n\r\n", _table, fieldNames, fieldValues);

                if (!Functions.IsEmptyString(scopeIdentity))
                {
                    sql += scopeIdentity;
                }
            }
            else if (mode == DataManager.ObjectWriteMode.Update)
            {
                string updateValues = string.Empty;
                string whereClause = string.Empty;

                foreach (TableFieldValue fieldValue in _values)
                {
                    if (fieldValue.ScopeIdentity == TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate)
                    {
                        whereClause += string.Format("{0}{1} = {2}",
                            whereClause == string.Empty ? "" : "\r\n  and ",
                            fieldValue.FieldName,
                            NeedToSQuote(fieldValue) ? DataManager.SQuote(fieldValue.FieldValue.ToString()) : fieldValue.FieldValue.ToString());
                    }
                    else
                    {
                        updateValues += string.Format("{0}{1} = {2}",
                            updateValues == string.Empty ? "" : ",\r\n    ",
                            fieldValue.FieldName,
                            NeedToSQuote(fieldValue) ? DataManager.SQuote(fieldValue.FieldValue.ToString()) : fieldValue.FieldValue.ToString());
                    }
                }

                sql = string.Format("UPDATE {0}\r\nSET {1}\r\nWHERE {2}\r\n\r\n", _table, updateValues, whereClause);

            }
            else
            {
                throw new ApplicationException("Sorry, only insert and update supported at this time.");
            }

            return sql;
        }

        /// <summary>
        /// Cleaner way to generate SQL rather than call GenerateSQL().
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return GenerateSQL(_writeMode);
        }

        /// <summary>
        /// Reset everything so you can reuse the class.
        /// </summary>
        /// <param name="nextTable">The next table you're going to use (or empty).</param>
        public void Reset(string nextTable)
        {
            _table = nextTable;
            _values.Clear();
            _identityReturnName = string.Empty;
            _identityScopeVarName = string.Empty;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="table">The table to work our magic on.</param>
        public SQLGenerator(string table)
        {
            _table = table;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="table">The table to work our magic on.</param>
        /// <param name="objectWriteMode">The write mode.</param>
        public SQLGenerator(string table, DataManager.ObjectWriteMode objectWriteMode)
            : this(table)
        {
            _writeMode = objectWriteMode;
        }

        /// <summary>
        /// Add try/catch and begin/commit transaction statements to the passed SQL.  
        /// </summary>
        /// <remarks>
        /// This makes
        /// multiple statements easy to roll into a single transaction that will either completely
        /// work or completely fail (no orphans anymore - there are a few already in the customer/address portion of the db).
        /// 
        /// From these guys:
        ///     http://www.4guysfromrolla.com/webtech/041906-1.shtml
        /// </remarks>
        /// <param name="sql">The sql to transactionize.</param>
        /// <returns>The resulting sql.</returns>
        public static string Transactionize(string sql)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("BEGIN TRY ");
            builder.AppendLine("   BEGIN TRANSACTION    -- Start the transaction   ");
            builder.AppendLine("");

            builder.AppendLine(sql);

            builder.AppendLine("");
            builder.AppendLine("   -- If we reach here, success!   ");
            builder.AppendLine("   COMMIT");
            builder.AppendLine("END TRY");
            builder.AppendLine("BEGIN CATCH");
            builder.AppendLine("  -- Whoops, there was an error");
            builder.AppendLine("  IF @@TRANCOUNT > 0   ");
            builder.AppendLine("     ROLLBACK  ");
            builder.AppendLine("");
            builder.AppendLine("  -- Raise an error with the details of the exception  ");
            builder.AppendLine("  DECLARE @ErrMsg nvarchar(4000), @ErrSeverity int ");
            builder.AppendLine("  SELECT @ErrMsg = ERROR_MESSAGE(),");
            builder.AppendLine("         @ErrSeverity = ERROR_SEVERITY()   ");
            builder.AppendLine("");
            builder.AppendLine("  RAISERROR(@ErrMsg, @ErrSeverity, 1)  ");
            builder.AppendLine("END CATCH  ");

            return builder.ToString();
        }
    }
}
