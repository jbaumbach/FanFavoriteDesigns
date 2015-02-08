using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using Ffd.Common;

namespace Ffd.Data
{
    public static class DataManager
    {
        public enum ObjectWriteMode
        {
            Insert,
            Update,
            Delete
        }

        public enum GetPlayerSeasonType
        {
            TeamsAndPlayers,
            GroupedByLastnameAndNumber
        }

        public enum AddressPresence
        {
            NotSet,
            Required,
            MustBeBlank
        }

        /// <summary>
        /// A callback function signature for logging stuff that happens in this procedure to your
        /// window.
        /// </summary>
        /// <param name="line">The string to log.</param>
        public delegate void LogDelegate(string line);


        public static string GetStringFromDataRow(DataRow row, string columnName)
        {
            return GetStringFromDataRow(row, columnName, true);
        }

        public static string GetStringFromDataRow(DataRow row, string columnName, bool withTrim)
        {
            string result = string.Empty;

            try
            {
                if (row[columnName] != null)
                {
                    result = row[columnName].ToString();

                    if (withTrim)
                    {
                        result = result.Trim();
                    }
                }
            }
            catch
            {
            }

            return result;
        }

        public static int GetIntFromDataRow(DataRow row, string columnName, int defaultReturnValue)
        {
            int result = defaultReturnValue;

            try
            {
                if (row[columnName] != null)
                {
                    result = int.Parse(row[columnName].ToString());
                }
            }
            catch
            {
            }

            return result;
        }

        public static bool GetBoolFromDataRow(DataRow row, string columnName, bool defaultReturnValue)
        {
            bool result = defaultReturnValue;

            // int temp = GetIntFromDataRow(row, columnName, defaultReturnValue == true ? 1 : 0);

            // result = temp == 1 ? true : false;

            if (row[columnName] != null)
            {
                result = (bool)row[columnName];
            }

            return result;
        }

        public static decimal GetDecimalFromDataRow(DataRow row, string columnName, decimal defaultReturnValue)
        {
            decimal result = defaultReturnValue;

            try
            {
                if (row[columnName] != null)
                {
                    result = decimal.Parse(row[columnName].ToString());
                }
            }
            catch
            {
            }

            return result;
        }

        public static float GetFloatFromDataRow(DataRow row, string columnName, float defaultReturnValue)
        {
            float result = defaultReturnValue;

            try
            {
                if (row[columnName] != null)
                {
                    result = float.Parse(row[columnName].ToString());
                }
            }
            catch
            {
            }

            return result;
        }

        /// <summary>
        /// Get a valid datetime from the data row.
        /// </summary>
        /// <param name="row">The data row.</param>
        /// <param name="columnName">The column name.</param>
        /// <returns>A valid datetime, or DateTime.MinVal.</returns>
        public static DateTime GetDateTimeFromDataRow(DataRow row, string columnName)
        {
            DateTime leadDate;
            bool haveGoodDate = DateTime.TryParse(GetStringFromDataRow(row, "time"), out leadDate);

            if (haveGoodDate)
            {
                return leadDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Return a SQL statement friendly string.  NOTE: you should call SQuote() instead so you can get a "null" returned.
        /// </summary>
        /// <param name="value">The string to escape.</param>
        /// <returns>The escaped string.  Will return string.Empty if value is null.</returns>
        public static string EscapeSQLString(string value)
        {
            if (value != null)
            {
                return value.Replace("'", "''");
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the passed value with single quotes around it.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <returns>Returns the value, or the word "null" (no quotes) if the passed value is null.</returns>
        public static string SQuote(string value)
        {
            if (value == null)
            {
                return "null";
            }
            else
            {
                return string.Format("'{0}'", EscapeSQLString(value));
            }
        }

        public static TemplateGraphicJersey GetTemplateGraphicJerseyAttributes(Template template)
        {
            TemplateGraphicJersey result = new TemplateGraphicJersey();

            TemplateAttrDataSet ds = new TemplateAttrDataSet(string.Format("select * from [dbo].[TEMPLATE_ATTRIBUTES] where template_id = {0}", template.TemplateId));

            //
            // Actual pixels (easier to calculate)
            //
            result.NameBoundingBox = ds.GetGraphicRect(TemplateAttrDataSet.TemplateAttrTypeCode.tatcNameBoundingBoxRect);
            result.NumberBoundingBox = ds.GetGraphicRect(TemplateAttrDataSet.TemplateAttrTypeCode.tatcNumberBoundingBoxRect);

            result.NameManualPositionAdjustments = ds.GetManualPositionAdjustments(TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNameFontHeight,
                TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNameFontVerticalPosition);

            result.NumberManualPositionAdjustments = ds.GetManualPositionAdjustments(TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNumberFontHeight,
                TemplateAttrDataSet.TemplateAttrTypeCode.tatcManualPosAdjsNumberFontVertPosition);

            result.NameFont = ds.GetValueFromRowSet((int)TemplateAttrDataSet.TemplateAttrTypeCode.tatcNameFontName).ToString();// "Comic Sans MS";

            return result;
        }

        private static string GetPlayerSeasonQueryBaseSQL()
        {
            string sql = "";

            sql += "select PSA.player_number, \r\n";
            sql += "    PSA.franchise_code, \r\n";
            sql += "    PSA.psa_web_image_status, \r\n";
            sql += "    P.player_fname,  \r\n";
            sql += "    P.player_lname,  \r\n";
            sql += "    P.player_jersey_name, \r\n";
            sql += "    P.player_code, \r\n";
            sql += "    N.nickname_desc, \r\n";
            sql += "    C.city_name, \r\n";
            sql += "    C.city_abbrev, \r\n";
            sql += "    L.league_desc_short, \r\n";
            sql += "    T.template_id, \r\n";
            sql += "    T.template_desc_short, \r\n";
            sql += "    S.season_desc, \r\n";
            sql += "    S.season_start_year, \r\n";
            sql += "    S.season_id, \r\n";
            sql += "    FSA.ebay_category_code \r\n";
            sql += "from PLAYER_SEASON_ASSOC PSA, \r\n";
            sql += "    PLAYER P, \r\n";
            sql += "    NICKNAME N, \r\n";
            sql += "    CITY C, \r\n";
            sql += "    LEAGUE L, \r\n";
            sql += "    SEASON S, \r\n";
            sql += "    TEMPLATE T, \r\n";
            sql += "    FRANCHISE_SEASON_ASSOC FSA \r\n";
            sql += "where PSA.player_code = P.player_code \r\n";
            sql += "and	PSA.season_id = FSA.season_id \r\n";
            sql += "and PSA.franchise_code = FSA.franchise_code \r\n";
            sql += "and FSA.city_code = C.city_code \r\n";
            sql += "and FSA.nickname_code = N.nickname_code \r\n";
            sql += "and FSA.season_id = S.season_id \r\n";
            sql += "and S.league_code = L.league_code \r\n";
            sql += "and	L.template_id = T.template_id \r\n";
            //
            // Exclude players that cannot be marketed
            //
            sql += "and P.player_nomarket_date is NULL  \r\n";

            return sql;
        }

        private static PlayerSeason GetPlayerSeasonFromDataRow(DataRow playerData)
        {
            PlayerSeason playerSeason = new PlayerSeason();

            playerSeason.JerseyNumber = GetStringFromDataRow(playerData, "player_number");
            playerSeason.FirstName = GetStringFromDataRow(playerData, "player_fname");
            playerSeason.LastName = GetStringFromDataRow(playerData, "player_lname");
            playerSeason.JerseyName = GetStringFromDataRow(playerData, "player_jersey_name");
            playerSeason.TeamNickname = GetStringFromDataRow(playerData, "nickname_desc");
            playerSeason.City = GetStringFromDataRow(playerData, "city_name");
            playerSeason.CityAbbreviation = GetStringFromDataRow(playerData, "city_abbrev");
            playerSeason.LeagueDescriptionShort = GetStringFromDataRow(playerData, "league_desc_short");
            playerSeason.WebImageStatus = (PlayerSeason.WebImageStatusType)GetIntFromDataRow(playerData, "psa_web_image_status", 0);

            // Note: these fields are deprecated and should be removed from this class.  Use the "Season" 
            // property instead (below).
            playerSeason.SeasonDesc = GetStringFromDataRow(playerData, "season_desc");
            playerSeason.SeasonId = GetIntFromDataRow(playerData, "season_id", -1);

            // 11/15/2007: This is the property to populate and use from now on.
            playerSeason.Season = new Season();
            playerSeason.Season.SeasonDesc = GetStringFromDataRow(playerData, "season_desc");
            playerSeason.Season.SeasonId = GetIntFromDataRow(playerData, "season_id", -1);
            playerSeason.Season.YearStarted = GetIntFromDataRow(playerData, "season_start_year", -1);

            playerSeason.FranchiseCode = GetIntFromDataRow(playerData, "franchise_code", -1);
            playerSeason.PlayerCode = GetIntFromDataRow(playerData, "player_code", -1);

            playerSeason.TemplateCurrent = GetTemplate(GetIntFromDataRow(playerData, "template_id", -1));  //templateCurrent;

            playerSeason.EBayCategoryCode = GetStringFromDataRow(playerData, "ebay_category_code");

            return playerSeason;
        }

        /// <summary>
        /// Seach database for player with lastname and number matching passed string.
        /// </summary>
        /// <param name="lastName">Player last name</param>
        /// <param name="number">Player jersey number</param>
        /// <returns>May return more than one PlayerSeason if the search matches more than one player.</returns>
        public static List<PlayerSeason> GetPlayerSeasons(string lastName, string number)
        {
            List<PlayerSeason> result = new List<PlayerSeason>();

            string sql = GetPlayerSeasonQueryBaseSQL();

            sql += string.Format("and P.player_lname = '{0}' \r\n", lastName.Replace("'", "''"));
            sql += string.Format("and PSA.player_number = '{0}' \r\n", number);

            DataSet playerDataList = new DataSet();

            GetQueryResults(playerDataList, sql);

            // If the query returns nothing, make sure template_id is not null in the db.  To set it:
            //      update LEAGUE set template_id = 2 where league_code = 7

            foreach (DataRow playerData in playerDataList.Tables[0].Rows)
            {
                result.Add(GetPlayerSeasonFromDataRow(playerData));
            }

            return result;
        }

        /// <summary>
        /// Gets players for a season and optionally a franchise.
        /// </summary>
        /// <param name="season">The season to get</param>
        /// <param name="franchise">(Optional) The franchise to get.  If null, all franchises in the season are searched.</param>
        /// <param name="previouslyUnexportedPlayersOnly">True to only get players that have not been previously exported.</param>
        /// <returns></returns>
        public static List<PlayerSeason> GetPlayerSeasons(Season season, Franchise franchise, bool previouslyUnexportedPlayersOnly)
        {
            return GetPlayerSeasons(season, franchise, previouslyUnexportedPlayersOnly, false);
        }

        /// <summary>
        /// Gets players for a season and optionally a franchise.
        /// </summary>
        /// <param name="season">The season to get</param>
        /// <param name="franchise">(Optional) The franchise to get.  If null, all franchises in the season are searched.</param>
        /// <param name="previouslyUnexportedPlayersOnly">True to only get players that have not been previously exported.</param>
        /// <param name="imagesReadyToUploadOnly">True to only get players that need images uploaded.</param>
        /// <returns></returns>
        public static List<PlayerSeason> GetPlayerSeasons(Season season, 
            Franchise franchise, 
            bool previouslyUnexportedPlayersOnly,
            bool imagesReadyToUploadOnly)
        {
            if (season == null)
            {
                throw new ApplicationException("Season must be non-null.");
            }

            List<PlayerSeason> result = new List<PlayerSeason>();

            string sql = GetPlayerSeasonQueryBaseSQL();

            sql += string.Format("and S.season_id = {0} \r\n", season.SeasonId);

            if (previouslyUnexportedPlayersOnly)
            {
                sql += "and PSA.psa_sales_export_date is null \r\n";
            }

            if (imagesReadyToUploadOnly)
            {
                sql += string.Format("and PSA.psa_web_image_status = {0} \r\n", (int)PlayerSeason.WebImageStatusType.ReadyToUpload);
            }

            if (franchise != null)
            {
                sql += string.Format("and FSA.franchise_code = {0} \r\n", franchise.FranchiseCode);
            }

            sql += "order by S.season_desc, \r\n";
            sql += "	C.city_name, \r\n";
            sql += "	N.nickname_desc, \r\n";
            sql += "	P.player_lname \r\n";

            DataSet playerDataList = new DataSet();

            GetQueryResults(playerDataList, sql);

            // If the query returns nothing: make sure template_id is not null in the db.  To set it:
            //      update LEAGUE set template_id = 2 where league_code = 7

            foreach (DataRow playerData in playerDataList.Tables[0].Rows)
            {
                result.Add(GetPlayerSeasonFromDataRow(playerData));
            }

            return result;
        }

        /// <summary>
        /// Set the image status for all the players in the passed list.
        /// </summary>
        /// <param name="players">The players to process.</param>
        /// <param name="status">The status to set.</param>
        /// <returns></returns>
        public static bool SetPlayerSeasonsImageStatus(List<PlayerSeason> players, PlayerSeason.WebImageStatusType status)
        {
            bool result;
            string sql = "";

            foreach (PlayerSeason player in players)
            {
                SQLGenerator gen = new SQLGenerator("PLAYER_SEASON_ASSOC", ObjectWriteMode.Update);
                gen.AddFieldValue(new TableFieldValue("psa_web_image_status", (int)status));
                AddWhereClauseForPlayerSeasonToSQLGen(gen, player);
                sql += gen;
            }

            sql = SQLGenerator.Transactionize(sql);
            result = (ExecuteNonQuery(sql) == players.Count);

            //
            // Also - reset the indicator in each object.  Not sure if this is best place for this, but no place seems perfect.
            //
            if (result)
            {
                foreach (PlayerSeason player in players)
                {
                    player.WebImageStatus = status;
                }
            }

            return result;
        }

        /// <summary>
        /// Marks all the players in the passed list as exported in the database.
        /// </summary>
        /// <param name="players">The player list.</param>
        /// <returns></returns>
        public static bool SetPlayerSeasonsAsExported(List<PlayerSeason> players)
        {
            DateTime exportDate = DateTime.Now;
            bool result;

            string sql = "";

            // sql += "begin transaction \r\n";

            foreach (PlayerSeason player in players)
            {
                SQLGenerator gen = new SQLGenerator("PLAYER_SEASON_ASSOC", ObjectWriteMode.Update);

                gen.AddFieldValue(new TableFieldValue("psa_sales_export_date", exportDate));
                gen.AddFieldValue(new TableFieldValue("psa_mkt_image_index", player.MarketingImageIndex));
                // gen.AddFieldValue(new TableFieldValue("psa_web_image_status", (int)player.WebImageStatus));

                AddWhereClauseForPlayerSeasonToSQLGen(gen, player);

                sql += gen;
            }

            // sql += "commit transaction \r\n";

            sql = SQLGenerator.Transactionize(sql);

            // throw new ApplicationException("Better check this sql first - restoring a database will NOT be fun.");

            result = (ExecuteNonQuery(sql) == players.Count);

            return result;
        }

        private static void AddWhereClauseForPlayerSeasonToSQLGen(SQLGenerator gen, PlayerSeason player)
        {
            gen.AddFieldValue(new TableFieldValue("player_code", player.PlayerCode, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
            gen.AddFieldValue(new TableFieldValue("season_id", player.SeasonId, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
            gen.AddFieldValue(new TableFieldValue("franchise_code", player.FranchiseCode, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
            gen.AddFieldValue(new TableFieldValue("player_number", player.JerseyNumber, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
        }

        /// <summary>
        /// Inserts the passed playerlist into the database.
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static bool WritePlayerSeasonListToDB(List<PlayerSeason> players)
        {
            int skipped = 0;

            foreach (PlayerSeason player in players)
            {
                string sql = "";

                // sql += "begin transaction \r\n";

                sql += "declare @franchise_code int;	 \r\n";
                sql += "declare @season_id int;		 \r\n";
                sql += "declare @player_fname varchar(50); \r\n";
                sql += "declare @player_lname varchar(50); \r\n";
                sql += "declare @player_jersey_name varchar(50); \r\n";
                sql += "declare @player_no int; \r\n";

                sql += string.Format("set @franchise_code = {0}; \r\n", player.FranchiseCode.ToString());
                sql += string.Format("set @season_id = {0};		 \r\n", player.SeasonId.ToString());
                sql += string.Format("set @player_fname = '{0}'; \r\n", player.FirstName.Replace("'", "''"));
                sql += string.Format("set @player_lname = '{0}'; \r\n", player.LastName.Replace("'", "''"));
                sql += string.Format("set @player_jersey_name = '{0}'; \r\n", player.JerseyName.Replace("'", "''"));
                sql += string.Format("set @player_no = {0}; \r\n", player.JerseyNumber);

                sql += "INSERT INTO PLAYER ([player_fname] ,[player_mi] ,[player_lname] ,[player_jersey_name] ,[marketing_code])  \r\n";
                sql += "	VALUES (@player_fname, null, @player_lname, @player_jersey_name, null) \r\n";

                sql += "INSERT INTO [PLAYER_SEASON_ASSOC] ([player_code] ,[season_id] ,[franchise_code] ,[player_number] ,[product_id]) \r\n";
                sql += "	 VALUES (SCOPE_IDENTITY(), @season_id, @franchise_code, @player_no, null) \r\n";

                sql = SQLGenerator.Transactionize(sql);

                // sql += "commit transaction \r\n";

                if (ExecuteNonQuery(sql) == 0)
                {
                    skipped++;
                }
            }

            return skipped == 0;
        }

        /// <summary>
        /// Get all the seasons for the passed league.
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        public static List<Season> GetSeasonsForLeague(League league)
        {
            return GetSeasonsForLeague(league, false);
        }

        /// <summary>
        /// Get all the seasons for the passed league.
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        public static List<Season> GetSeasonsForLeague(League league, bool latestSeasonOnly)
        {
            List<Season> result = new List<Season>();

            string sql = "";

            //sql += "select * from SEASON \r\n";
            //sql += string.Format("where league_code = {0} \r\n", league.LeagueCode);

            if (latestSeasonOnly == false)
            {
                sql += "select S.*, \r\n";
            }
            else
            {
                sql += "select top 1 S.*, \r\n";
            }

	        sql += "L.league_desc_short \r\n";
            sql += "from SEASON S,  \r\n";
            sql += "	LEAGUE L \r\n";
            sql += "where S.league_Code = L.league_code \r\n";
            sql += string.Format("and S.league_code = {0} \r\n", league.LeagueCode);

            if (latestSeasonOnly)
            {
                sql += "order by S.season_start_year DESC \r\n";
            }

            DataSet seasonDataList = new DataSet();

            GetQueryResults(seasonDataList, sql);

            foreach (DataRow seasonData in seasonDataList.Tables[0].Rows)
            {
                Season season = new Season();

                season.SeasonId = GetIntFromDataRow(seasonData, "season_id", -1);
                season.SeasonDesc = GetStringFromDataRow(seasonData, "season_desc");
                season.YearStarted = GetIntFromDataRow(seasonData, "season_start_year", -1);
                season.LeagueDescShort = GetStringFromDataRow(seasonData, "league_desc_short");

                result.Add(season);
            }

            return result;
        }

        /// <summary>
        /// Get the league objects.
        /// </summary>
        /// <param name="nonGenericOnly"></param>
        /// <returns></returns>
        public static List<League> GetLeagues(bool nonGenericOnly)
        {
            List<League> result = new List<League>();

            string sql = "";

            sql += "select * from LEAGUE \r\n";

            if (nonGenericOnly)
            {
                sql += "where league_generic_ind = 0 \r\n";
            }

            DataSet leagueDataList = new DataSet();

            GetQueryResults(leagueDataList, sql);

            foreach (DataRow leagueData in leagueDataList.Tables[0].Rows)
            {
                League league = new League();

                league.LeagueCode = GetIntFromDataRow(leagueData, "league_code", -1);
                league.LeagueDescShort = GetStringFromDataRow(leagueData, "league_desc_short");

                result.Add(league);
            }

            return result;
        }

        /// <summary>
        /// Return a single template matching the supplied description
        /// </summary>
        /// <param name="description">String description of template to get - the short description.</param>
        /// <returns>Template, or null if none found.  If more than one found, returns the first.</returns>
        public static Template GetTemplate(string description)
        {
            List<Template> templates = GetTemplates(description);
            Template result = null;

            if (templates.Count > 0)
            {
                result = templates[0];
            }

            return result;
        }

        /// <summary>
        /// Return a single template matching the supplied description.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>The Template, or null if template id is invalid.</returns>
        public static Template GetTemplate(int templateId)
        {
            Template result = null; // new Template();

            string sql = GetTemplateBaseSQL();

            sql += string.Format("and T.template_id = {0} \r\n", templateId);

            DataSet templateDataList = new DataSet();

            GetQueryResults(templateDataList, sql);

            if (templateDataList.Tables[0].Rows.Count == 1)
            {
                result = GetTemplateFromDataRow(templateDataList.Tables[0].Rows[0]);
            }

            return result;
        }

        /// <summary>
        /// Get current active templates from the database
        /// </summary>
        /// <returns>A genertic List&lt;Template&gt;.</returns>
        public static List<Template> GetTemplates()
        {
            return GetTemplates(string.Empty);
        }

        private static string GetTemplateBaseSQL()
        {
            string sql = "";

            sql += "select T.*, S.season_id,  \r\n";
            sql += "( \r\n";
	        sql += "select template_attr_data as msrp from TEMPLATE_ATTRIBUTES where template_attr_type_code = 9 and template_id = T.template_id \r\n";
            sql += ") as msrp, \r\n";
            sql += "(  \r\n";
            sql += "select template_attr_data as ebay_cat from TEMPLATE_ATTRIBUTES where template_attr_type_code = 17 and template_id = T.template_id  \r\n";
            sql += ") as ebay_cat,  \r\n";
            sql += "(  \r\n";
            sql += "select template_attr_data from TEMPLATE_ATTRIBUTES where template_attr_type_code = 19 and template_id = T.template_id  \r\n";
            sql += ") as ebay_store_cat,  \r\n";
            sql += "(  \r\n";
            sql += "select template_attr_data as ebay_cat from TEMPLATE_ATTRIBUTES where template_attr_type_code = 10 and template_id = T.template_id  \r\n";
            sql += ") as dimensions,  \r\n";
            sql += "(  \r\n";
            sql += "select template_attr_data as ebay_cat from TEMPLATE_ATTRIBUTES where template_attr_type_code = 18 and template_id = T.template_id  \r\n";
            sql += ") as namenoboxheight  \r\n";
            sql += "from TEMPLATE T, SEASON S, LEAGUE L \r\n";
            sql += "where  T.template_id = L.template_id \r\n";
            sql += "and L.league_code = S.league_code \r\n";
            sql += "and	S.season_start_year = -1 \r\n";
            sql += "and	T.template_active_ind = 1 \r\n";

            return sql;
        }

        /// <summary>
        /// Get a template object from data contained in the passed data row.
        /// </summary>
        /// <param name="templateData"></param>
        /// <returns></returns>
        private static Template GetTemplateFromDataRow(DataRow templateData)
        {
            Template template = new Template();

            template.TemplateId = GetIntFromDataRow(templateData, "template_id", -1);
            template.TemplateDescShort = GetStringFromDataRow(templateData, "template_desc_short");
            template.GenericSeasonId = GetIntFromDataRow(templateData, "season_id", -1);
            template.MSRP = GetDecimalFromDataRow(templateData, "msrp", -1);
            template.EBayCategoryCode = GetStringFromDataRow(templateData, "ebay_cat");
            template.Dimensions = GetStringFromDataRow(templateData, "dimensions");
            template.NameNoUnitHeight = GetFloatFromDataRow(templateData, "namenoboxheight", -1f);
            template.EbayFFDStoreCategoryCode = GetIntFromDataRow(templateData, "ebay_store_cat", -1);

            return template;
        }

        /// <summary>
        /// Get current active templates from the database
        /// </summary>
        /// <returns>A genertic List&lt;Template&gt;.</returns>
        /// <param name="description">A string description of the template to retreive - the short description.</param>
        public static List<Template> GetTemplates(string description)
        {
            List<Template> result = new List<Template>();

            string sql = GetTemplateBaseSQL();

            if (description != string.Empty)
            {
                sql += string.Format("and T.template_desc_short = '{0}' \r\n", description);
            }

            DataSet templateDataList = new DataSet();

            GetQueryResults(templateDataList, sql);

            foreach (DataRow templateData in templateDataList.Tables[0].Rows)
            {
                result.Add(GetTemplateFromDataRow(templateData));
            }

            return result;
        }


        public static Franchise GetFranchise(int franchiseCode)
        {
            Franchise result = null;

            string sql = "";

            sql += "select F.* \r\n";
            sql += "from FRANCHISE F \r\n";
            sql += string.Format("where F.franchise_code = {0} \r\n", franchiseCode);

            DataSet franchiseDataList = new DataSet();

            GetQueryResults(franchiseDataList, sql);

            foreach (DataRow franchiseData in franchiseDataList.Tables[0].Rows)
            {
                result = new Franchise();

                result.FranchiseCode = GetIntFromDataRow(franchiseData, "franchise_code", -1);
                result.FranchiseDesc = GetStringFromDataRow(franchiseData, "franchise_nickname_at_entry");
            }

            return result;
        }

        public static List<Franchise> GetFranchisesForSeason(Season season)
        {
            List<Franchise> result = new List<Franchise>();

            string sql = "";

            sql += "select F.*, \r\n";
            sql += "	FSA.* \r\n";
            sql += "from FRANCHISE F, \r\n";
            // sql += "	SEASON S, \r\n";
            sql += "	FRANCHISE_SEASON_ASSOC FSA \r\n";
            sql += "where F.franchise_code = FSA.franchise_code \r\n";
            // sql += "	and FSA.season_id = S.season_id \r\n";
            // sql += string.Format("	and S.league_code = {0} \r\n", leagueCode);

            sql += string.Format("	and FSA.season_id = {0} \r\n", season.SeasonId);

            DataSet franchiseDataList = new DataSet();

            GetQueryResults(franchiseDataList, sql);

            foreach (DataRow franchiseData in franchiseDataList.Tables[0].Rows)
            {
                Franchise franchise = new Franchise();

                franchise.FranchiseCode = GetIntFromDataRow(franchiseData, "franchise_code", -1);
                franchise.FranchiseDesc = GetStringFromDataRow(franchiseData, "franchise_nickname_at_entry");

                result.Add(franchise);
            }

            return result;
        }

        /// <summary>
        /// Builds a SQLGenerator object for the passed customer.  Does NOT generate the sql or set the mode.
        /// </summary>
        /// <param name="customer">The customer object.</param>
        /// <param name="mode">The mode to use.</param>
        /// <returns>The created SQLGenerator object.</returns>
        private static SQLGenerator BuildWriteCustomerSQLGenerator(Customer customer, ObjectWriteMode mode)
        {
            SQLGenerator sqlGen = new SQLGenerator("CUSTOMER");
            sqlGen.WriteMode = mode;
            if (mode == ObjectWriteMode.Insert)
            {
                sqlGen.AddFieldValue(new TableFieldValue("customer_id", TableFieldValue.SpecialColumnBehavior.BothReturnAndSet));
            }
            else
            {
                sqlGen.AddFieldValue(new TableFieldValue("customer_id", customer.CustomerId, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
            }
            sqlGen.AddFieldValue(new TableFieldValue("customer_email_addr", customer.EmailAddress));
            sqlGen.AddFieldValue(new TableFieldValue("customer_password", customer.Password));
            sqlGen.AddFieldValue(new TableFieldValue("customer_fname", customer.FirstName));
            sqlGen.AddFieldValue(new TableFieldValue("customer_mi", customer.MiddleInitial));
            sqlGen.AddFieldValue(new TableFieldValue("customer_lname", customer.LastName));
            sqlGen.AddFieldValue(new TableFieldValue("cust_company_name", customer.CompanyName));
            sqlGen.AddFieldValue(new TableFieldValue("customer_phone_day", customer.PhoneDay));
            sqlGen.AddFieldValue(new TableFieldValue("customer_create_by", customer.CreateBy));

            return sqlGen;
        }

        /// <summary>
        /// Inserts a customer (or subclass) object.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool InsertCustomer(Customer customer)
        {
            string sql = "";

            bool result = false;


            // ******* TODO: test the SQlGenerator.Transactionize() procedure here...


            // sql += "begin transaction \r\n\r\n";

            //SQLGenerator sqlGen = new SQLGenerator("CUSTOMER");
            //sqlGen.AddFieldValue(new TableFieldValue("customer_id", TableFieldValue.SpecialColumnBehavior.BothReturnAndSet));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_email_addr", customer.EmailAddress));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_password", customer.Password));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_fname", customer.FirstName));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_mi", customer.MiddleInitial));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_lname", customer.LastName));
            //sqlGen.AddFieldValue(new TableFieldValue("cust_company_name", customer.CompanyName));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_phone_day", customer.PhoneDay));
            //sqlGen.AddFieldValue(new TableFieldValue("customer_create_by", "webserver"));

            SQLGenerator sqlGen = BuildWriteCustomerSQLGenerator(customer, ObjectWriteMode.Insert);
            sql += sqlGen.ToString();

            string newCustomerIDScopeVar = sqlGen.IdentityScopeVarName;
            string newCustomerReturnVarName = sqlGen.IdentityReturnName;

            sql += BuildAddressSQL(customer.BillingAddress, ObjectWriteMode.Insert, customer, Address.AddrTypeCode.atcBilling, newCustomerIDScopeVar);
            sql += BuildAddressSQL(customer.ShippingAddress, ObjectWriteMode.Insert, customer, Address.AddrTypeCode.atcShipping, newCustomerIDScopeVar);

            if (typeof(Lead) == customer.GetType())
            {
                sql += BuildWriteLeadSQL((Lead)customer, ObjectWriteMode.Insert, newCustomerIDScopeVar);
            }

            // sql += "commit transaction \r\n";


            //
            // Remove all this after testing pretty thoroughly.  Man, you do NOT want to figure out how to restore a database.
            //
            //if (!result)
            //{
            //    throw new ApplicationException("A lot of this code is untested - remove this line and try again!");
            //}

            sql = SQLGenerator.Transactionize(sql);

            DataSet newCustomerIdDataset = new DataSet();

            GetQueryResults(newCustomerIdDataset, sql);

            DataRow newCustomerIdRow = newCustomerIdDataset.Tables[0].Rows[0];
            int newCustomerId = GetIntFromDataRow(newCustomerIdRow, newCustomerReturnVarName, -1);
            customer.CustomerId = newCustomerId;

            result = (newCustomerId > 0);

            return result;
        }

        /// <summary>
        /// Get a Lead object selected by the passed in SQL additional where clause.
        /// </summary>
        /// <param name="addtlSQL"></param>
        /// <returns></returns>
        private static Lead GetLeadGeneric(string sql)
        {
            Lead result = null;

            // string sql = GetLeadSQL();
            // sql += addtlSQL;

            DataSet customerDataset = new DataSet();

            GetQueryResults(customerDataset, sql);

            if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count == 1))
            {
                result = new Lead();

                DataRow customerRow = customerDataset.Tables[0].Rows[0];

                PopulateCustomerFromDataRow(result, customerRow);
                PopulateLeadFromDataRow(result, customerRow);

            }

            return result;
        }

        /// <summary>
        /// Gets a lead by passed name and status code.
        /// </summary>
        /// <param name="fname">Lead first name.</param>
        /// <param name="lname">Lead last name.</param>
        /// <param name="code">Status code to check.</param>
        /// <returns>A lead or null.</returns>
        public static Lead GetLeadByNameAndStatus(string fname, string lname, Lead.LeadStatusCode code)
        {
            string sql = GetLeadSQL();
            sql += string.Format("and customer_fname = {0} and customer_lname = {1} and LSL.lsl_status_code = {2}\r\n",
                SQuote(fname),
                SQuote(lname),
                (int)code);

            return GetLeadGeneric(sql);
        }

        /// <summary>
        /// Get a lead by company name and zip.
        /// </summary>
        /// <param name="companyName">Company name.</param>
        /// <param name="zip">Zip code.</param>
        /// <returns>The first lead found, if any.  Otherwise null.</returns>
        public static Lead GetLeadByCompanyNameAndZip(string companyName, string zip)
        {
            Lead lead = null;

            string sql = "";
            sql += "select * from CUSTOMER_ADDRESS CA, CUSTOMER_ADDR_ASSOC CAA \r\n";
            sql += "where CA.customer_addr_id = CAA.customer_addr_id \r\n";
            sql += string.Format("and addr_company_name = {0} \r\n", SQuote(companyName));
            sql += string.Format("and addr_zip_postal = {0} \r\n", SQuote(zip));

            DataSet customerDataset = new DataSet();

            GetQueryResults(customerDataset, sql);

            if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count > 0))
            {
                int customerId = GetIntFromDataRow(customerDataset.Tables[0].Rows[0], "customer_id", -1);
                lead = GetLeadByCustomerId(customerId);
            }

            return lead;
        }

        /// <summary>
        /// Get the lead associated with the email address.
        /// </summary>
        /// <param name="emailAddress">Email address to search for.</param>
        /// <returns>The lead, if it exists.  Returns null otherwise.</returns>
        public static Lead GetLead(string emailAddress)
        {
            //Lead result = null;

            string sql = GetLeadSQL();
            sql += string.Format("and customer_email_addr = {0}  \r\n", SQuote(emailAddress));

            return GetLeadGeneric(sql);
        }

        /// <summary>
        /// Get a lead by customer id.
        /// </summary>
        /// <param name="customerId">The customer id.</param>
        /// <returns>A lead if we have one, or null.</returns>
        public static Lead GetLeadByCustomerId(int customerId)
        {
            string sql = GetLeadSQL();
            sql += string.Format("and C.customer_id = {0} \r\n", SQuote(customerId.ToString()));

            return GetLeadGeneric(sql);
        }

        /// <summary>
        /// Build a SQL string to get leads from the db.
        /// </summary>
        /// <returns>A SQL string.</returns>
        private static string GetLeadSQL()
        {
            return GetLeadSQL(false);
        }

        /// <summary>
        /// Build a SQL string to get leads from the db.
        /// </summary>
        /// <returns>A SQL string.</returns>
        private static string GetLeadSQL(bool quantityOnly)
        {
            string sql = "";

            if (quantityOnly)
            {
                sql += "select count(*) as 'count' \r\n";
            }
            else
            {
                sql += "select C.*, L.*, LSL.lsl_status_date, LSL.lsl_status_code, LSL.lsl_status_comment \r\n";
            }

            sql += "from CUSTOMER C, LEAD L, LEAD_STATUS_LOG LSL, \r\n";
            sql += "( \r\n";
            sql += "	select customer_id, max(lsl_status_date) as max_date \r\n";
            sql += "	from LEAD_STATUS_LOG L \r\n";
            sql += "	group by customer_id \r\n";
            sql += ") as maxLSL \r\n";
            sql += "where C.customer_id = L.customer_id \r\n";
            sql += "and LSL.customer_id = L.customer_id \r\n";
            sql += "and maxLSL.customer_id = LSL.customer_id \r\n";
            sql += "and maxLSL.max_date = LSL.lsl_status_date \r\n";

            return sql;
        }

        /// <summary>
        /// Gets all fundraising leads with the status code of "lscInserted"
        /// </summary>
        /// <returns></returns>
        public static List<Lead> GetNewFundraisingLeads()
        {
            //List<Lead> result = new List<Lead>();

            //string sql = GetLeadSQL();

            //sql += "and LSL.lsl_status_code = 1 \r\n";
            //sql += "and L.lead_source_code = 1 \r\n";

            //DataSet customerDataset = new DataSet();

            //GetQueryResults(customerDataset, sql);

            //if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count > 0))
            //{
            //    foreach (DataRow customerRow in customerDataset.Tables[0].Rows)
            //    {
            //        Lead lead = new Lead();
            //        PopulateCustomerFromDataRow(lead, customerRow);
            //        PopulateLeadFromDataRow(lead, customerRow);

            //        result.Add(lead);
            //    }
            //}

            //return result;

            return GetLeads("1", "1", "", "", null);
        }

        /// <summary>
        /// Get all leads with the given source code, status code, and minimum enrollment.
        /// </summary>
        /// <param name="leadSourceCode">Source code.</param>
        /// <param name="leadStatusCode">Status code.</param>
        /// <returns>List of result leads.</returns>
        public static List<Lead> GetLeads(string leadSourceCode, string leadStatusCode)
        {
            return GetLeads(leadSourceCode, leadStatusCode, "", "", null);
        }

        /// <summary>
        /// Get all leads with the given source code, status code, and minimum enrollment.
        /// </summary>
        /// <param name="leadSourceCode">Source code.</param>
        /// <param name="leadStatusCode">Status code.</param>
        /// <param name="minEnrollment">(Optional) Minimum enrollment the lead should have (should be a school)</param>
        /// <param name="skipEachValue">(Optional) Number of leads to skip in result set.  Helps to randomize.</param>
        /// <param name="log">(Optional) Delegate called periodically with the loop value.</param>
        /// <returns>List of result leads.</returns>
        public static List<Lead> GetLeads(string leadSourceCode, string leadStatusCode, string minEnrollment, string skipEachValue, LogDelegate log)
        {
            return GetLeads(leadSourceCode, leadStatusCode, minEnrollment, skipEachValue, log, AddressPresence.NotSet, false);
        }

        /// <summary>
        /// Get all leads with the given source code, status code, and minimum enrollment.
        /// </summary>
        /// <param name="leadSourceCode">Source code.</param>
        /// <param name="leadStatusCode">Status code.</param>
        /// <param name="minEnrollment">(Optional) Minimum enrollment the lead should have (should be a school)</param>
        /// <param name="skipEachValue">(Optional) Number of leads to skip in result set.  Helps to randomize.</param>
        /// <param name="log">(Optional) Delegate called periodically with the loop value.</param>
        /// <param name="shippingAddressRequirement">(Optional) Filter result set on address presense.</param>
        /// <param name="mustBeAthletics">Set to true to filter for only true values.  If false, nothing is filtered.</param>
        /// <returns>List of result leads.</returns>
        public static List<Lead> GetLeads(string leadSourceCode, 
            string leadStatusCode, 
            string minEnrollment, 
            string skipEachValue, 
            LogDelegate log, 
            AddressPresence shippingAddressRequirement,
            bool mustBeAthletics)
        {
            List<Lead> result = new List<Lead>();

            string sql = GetLeadSQL();

            sql += string.Format("and LSL.lsl_status_code = {0} \r\n", leadStatusCode);
            sql += string.Format("and L.lead_source_code = {0} \r\n", leadSourceCode);

            if (!Functions.IsEmptyString(minEnrollment) && Functions.IsNumeric(minEnrollment))
            {
                sql += string.Format("and L.school_tot_enrollment >= {0} \r\n", minEnrollment);
            }

            if (mustBeAthletics)
            {
                sql += "and L.is_athletics_ind = 1 \r\n";
            }

            DataSet customerDataset = new DataSet();

            GetQueryResults(customerDataset, sql);

            if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count > 0))
            {
                int rowCount = 0;

                //
                // The skip value of 1 means include every one, 2 means take every 2nd one, 3 means
                // take every third, etc.
                //
                int skip = Functions.IsNumeric(skipEachValue) ? int.Parse(skipEachValue) : 1;
                if (skip <= 0)
                {
                    skip = 1;
                }

                foreach (DataRow customerRow in customerDataset.Tables[0].Rows)
                {
                    bool includeLead = ((rowCount++ % skip) == 0);

                    if (includeLead)
                    {
                        Lead lead = new Lead();
                        PopulateCustomerFromDataRow(lead, customerRow);
                        PopulateLeadFromDataRow(lead, customerRow);

                        includeLead = false;

                        if (shippingAddressRequirement == AddressPresence.MustBeBlank)
                        {
                            if (lead.ShippingAddress == null)
                            {
                                includeLead = true;
                            }
                            else
                            {
                                includeLead = Functions.IsEmptyString(lead.ShippingAddress.Address1);
                            }
                        }
                        else if (shippingAddressRequirement == AddressPresence.Required)
                        {
                            includeLead = (lead.ShippingAddress != null && !Functions.IsEmptyString(lead.ShippingAddress.Address1));
                        }
                        else
                        {
                            includeLead = true;
                        }

                        if (includeLead)
                        {
                            result.Add(lead);
                        }

                        if (log != null)
                        {
                            log(rowCount.ToString());
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Get the lead quantity for the passed filters.
        /// </summary>
        /// <param name="leadSourceCode">Lead source code to get.</param>
        /// <param name="leadStatusCode">Lead status code to get.</param>
        /// <param name="minEnrollment">Minimum enrollment (applies to leads of type school only)</param>
        /// <returns>The quantity, or -1 if an error.</returns>
        public static int GetLeadQty(string leadSourceCode, string leadStatusCode, string minEnrollment, bool mustBeAthletics)
        {
            string sql = GetLeadSQL(true);
            int result = -1;

            sql += string.Format("and LSL.lsl_status_code = {0} \r\n", leadStatusCode);
            sql += string.Format("and L.lead_source_code = {0} \r\n", leadSourceCode);

            if (!Functions.IsEmptyString(minEnrollment) && Functions.IsNumeric(minEnrollment))
            {
                sql += string.Format("and L.school_tot_enrollment >= {0} \r\n", minEnrollment);
            }

            if (mustBeAthletics)
            {
                sql += "and L.is_athletics_ind = 1 \r\n";
            }

            DataSet customerDataset = new DataSet();

            GetQueryResults(customerDataset, sql);

            if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count > 0))
            {
                result = GetIntFromDataRow(customerDataset.Tables[0].Rows[0], "count", -1);
            }

            return result;

        }

        /// <summary>
        /// Return a customer matching the passed email address.
        /// </summary>
        /// <param name="emailAddress">The email address</param>
        /// <returns>The customer or null.</returns>
        public static Customer GetCustomer(string emailAddress)
        {
            return GetCustomer(emailAddress, string.Empty);
        }

        /// <summary>
        /// Return a customer matching the passed email address and password.
        /// </summary>
        /// <param name="emailAddress">The email address</param>
        /// <param name="password">The user's password</param>
        /// <returns>The customer or null.</returns>
        public static Customer GetCustomer(string emailAddress, string password)
        {
            Customer result = null;
            string sql = "";

            sql += "select * from CUSTOMER \r\n";
            sql += string.Format("where customer_email_addr = '{0}'  \r\n", EscapeSQLString(emailAddress));

            if (password != string.Empty)
            {
                sql += string.Format("and	customer_password = '{0}' \r\n", EscapeSQLString(password));
            }

            DataSet customerDataset = new DataSet();

            GetQueryResults(customerDataset, sql);

            if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count > 0))
            {
                DataRow customerRow = customerDataset.Tables[0].Rows[0];

                result = new Customer();

                PopulateCustomerFromDataRow(result, customerRow);
            }

            return result;
        }

        /// <summary>
        /// Populate a customer object from a datarow containing all the columns that we need.
        /// </summary>
        /// <param name="customer">The customer object to populate.</param>
        /// <param name="customerRow">The datarow with our goodies.</param>
        private static void PopulateCustomerFromDataRow(Customer customer, DataRow customerRow)
        {
            customer.CustomerId = GetIntFromDataRow(customerRow, "customer_id", -1);
            customer.EmailAddress = GetStringFromDataRow(customerRow, "customer_email_addr");
            customer.Password = GetStringFromDataRow(customerRow, "customer_password");
            customer.FirstName = GetStringFromDataRow(customerRow, "customer_fname");
            customer.MiddleInitial = GetStringFromDataRow(customerRow, "customer_mi");
            customer.LastName = GetStringFromDataRow(customerRow, "customer_lname");
            customer.CompanyName = GetStringFromDataRow(customerRow, "cust_company_name");
            customer.PhoneDay = GetStringFromDataRow(customerRow, "customer_phone_day");
            customer.CreateBy = GetStringFromDataRow(customerRow, "customer_create_by");

            //
            // Load addresses if we got 'em
            //
            LoadCustomerAddresses(customer);
        }

        /// <summary>
        /// Populate the address fields of the passed customer object.
        /// </summary>
        /// <param name="customer">The customer to populate.</param>
        private static void LoadCustomerAddresses(Customer customer)
        {
            string sql = string.Empty;

            sql += "select * from CUSTOMER_ADDRESS CA, CUSTOMER_ADDR_ASSOC CAA, STATE_PROV_CNTY SPC, COUNTRY CTY \r\n";
            sql += "where CA.customer_addr_id = CAA.customer_addr_id \r\n";
            sql += "and SPC.state_prov_code = CA.state_prov_code \r\n";
            sql += "and CTY.country_code = CA.country_code \r\n";
            sql += string.Format("and CAA.customer_id = {0} \r\n", customer.CustomerId);

            DataSet customerDataset = new DataSet();

            GetQueryResults(customerDataset, sql);

            if ((customerDataset.Tables.Count > 0) && (customerDataset.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow row in customerDataset.Tables[0].Rows)
                {
                    Address address = new Address();
                    address.AddressId = GetIntFromDataRow(row, "customer_addr_id", -1);
                    address.Address1 = GetStringFromDataRow(row, "addr_line1");
                    address.Address2 = GetStringFromDataRow(row, "addr_line2");
                    address.Address3 = GetStringFromDataRow(row, "addr_line3");
                    address.City = GetStringFromDataRow(row, "addr_city");
                    address.CompanyName = GetStringFromDataRow(row, "addr_company_name");
                    address.FirstName = GetStringFromDataRow(row, "addr_person_fname");
                    address.LastName = GetStringFromDataRow(row, "addr_person_lname");
                    address.StateProvCode = GetIntFromDataRow(row, "state_prov_code", -1);
                    address.StateProvAbbrev = GetStringFromDataRow(row, "spc_national_abbrev");
                    address.ZipPostalCode = GetStringFromDataRow(row, "addr_zip_postal");
                    address.CountryCode = GetStringFromDataRow(row, "country_code");
                    address.Phone = GetStringFromDataRow(row, "addr_phone");
                    address.Country = GetStringFromDataRow(row, "country_desc");

                    int type = GetIntFromDataRow(row, "attr_type_code", -1);

                    if (type == (int)Address.AddrTypeCode.atcBilling)
                    {
                        customer.BillingAddress = address;
                    }
                    else if (type == (int)Address.AddrTypeCode.atcShipping)
                    {
                        customer.ShippingAddress = address;
                    }
                }
            }
        }

        /// <summary>
        /// Populate lead object with datarow fields.
        /// </summary>
        /// <param name="lead">The lead object.</param>
        /// <param name="leadRow">The datarow with the data.</param>
        private static void PopulateLeadFromDataRow(Lead lead, DataRow leadRow)
        {
            lead.LeadSourceCode = GetIntFromDataRow(leadRow, "lead_source_code", -1);
            lead.LeadInfoText = GetStringFromDataRow(leadRow, "lead_info_txt");
            lead.LeadStatusCodeCurrent = (Lead.LeadStatusCode)GetIntFromDataRow(leadRow, "lsl_status_code", (int)Lead.LeadStatusCode.lscNotSet);
            lead.BestContactMethod = (Lead.BestContactMethodType)GetIntFromDataRow(leadRow, "best_contact_method", 0);
            lead.IsSchool = GetBoolFromDataRow(leadRow, "is_school_ind", false);
            lead.IsAthletics = GetBoolFromDataRow(leadRow, "is_athletics_ind", false);
            lead.SchoolTotalEnrollment = GetIntFromDataRow(leadRow, "school_tot_enrollment", -1);

            int templateId = GetIntFromDataRow(leadRow, "template_id", -1);
            if (templateId >= 0)
            {
                lead.TemplateCurrent = GetTemplate(templateId);
            }
        }

        /// <summary>
        /// Build the SQL to write to the lead table.
        /// </summary>
        /// <param name="lead">The lead object.</param>
        /// <param name="mode">The write mode.</param>
        /// <returns>The SQL.</returns>
        public static string BuildWriteLeadSQL(Lead lead, ObjectWriteMode mode, string newCustomerIDScopeVar)
        {
            string sql = "";

            SQLGenerator gen = new SQLGenerator("LEAD");
            gen.WriteMode = mode;

            if (mode == ObjectWriteMode.Insert)
            {
                //
                // If we're inserting, this is the variable name for SCOPE_IDENTITY().
                //
                gen.AddFieldValue(new TableFieldValue("customer_id", newCustomerIDScopeVar, TableFieldValue.SQuoteBehavior.NoSQuote));
            }
            else
            {
                gen.AddFieldValue(new TableFieldValue("customer_id", lead.CustomerId, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
            }
            gen.AddFieldValue(new TableFieldValue("lead_source_code", lead.LeadSourceCode));
            gen.AddFieldValue(new TableFieldValue("lead_info_txt", lead.LeadInfoText));
            gen.AddFieldValue(new TableFieldValue("best_contact_method", (int)lead.BestContactMethod));
            gen.AddFieldValue(new TableFieldValue("is_school_ind", Functions.BoolToInt(lead.IsSchool)));
            gen.AddFieldValue(new TableFieldValue("is_athletics_ind", Functions.BoolToInt(lead.IsAthletics)));
            gen.AddFieldValue(new TableFieldValue("template_id", lead.TemplateCurrent == null ? "null" : lead.TemplateCurrent.TemplateId.ToString(), TableFieldValue.SQuoteBehavior.NoSQuote));
            gen.AddFieldValue(new TableFieldValue("lead_orig_contact_us_date", lead.LeadDate));
            gen.AddFieldValue(new TableFieldValue("school_tot_enrollment", lead.SchoolTotalEnrollment));
            sql += gen;

            sql += GetLeadStatusLogSQL(lead, Lead.LeadStatusCode.lscInserted, null, newCustomerIDScopeVar);

            return sql;
        }

        ///// <summary>
        ///// Inserts or updates a lead.  This procedure is bad - it should be transactional.  Let's hope 
        ///// nothing goes wrong.
        ///// </summary>
        ///// <param name="lead">The lead object.</param>
        ///// <param name="mode">The mode.</param>
        ///// <returns>True if the insertion worked.</returns>
        //public static bool WriteLead(Lead lead, ObjectWriteMode mode)
        //{
        //    bool result = WriteCustomer(lead, mode);

        //    //if (result)
        //    //{
        //    //    if (mode == ObjectWriteMode.Insert)
        //    //    {
        //    //        string sql = "";

        //    //        sql += "begin transaction \r\n";

        //    //        sql += BuildWriteLeadSQL(lead, mode);

        //    //        sql += "commit transaction \r\n";

        //    //        result = (ExecuteNonQuery(sql) >= 1);
        //    //    }
        //    //    else
        //    //    {
        //    //        throw new ApplicationException("Only insert supported at this time.");
        //    //    }
        //    //}

        //    return result;
        //}

        /// <summary>
        /// Append to the lead's status log.
        /// </summary>
        /// <param name="lead"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool AddLeadStatusLog(Lead lead, Lead.LeadStatusCode code, string comment)
        {
            string sql = GetLeadStatusLogSQL(lead, code, comment);
            return (ExecuteNonQuery(sql) == 1);
        }

        /// <summary>
        /// Get SQL for writing to the status log.  Useful for inserting into a transactional statement.
        /// </summary>
        /// <param name="lead"></param>
        /// <param name="code"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private static string GetLeadStatusLogSQL(Lead lead, Lead.LeadStatusCode code, string comment)
        {
            return GetLeadStatusLogSQL(lead, code, comment, string.Empty);
        }
        /// <summary>
        /// Get SQL for writing to the status log.  Useful for inserting into a transactional statement.
        /// </summary>
        /// <param name="lead"></param>
        /// <param name="code"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        private static string GetLeadStatusLogSQL(Lead lead, Lead.LeadStatusCode code, string comment, string newCustomerIDScopeVar)
        {
            //string sql = "";

            SQLGenerator sql = new SQLGenerator("LEAD_STATUS_LOG");
            if (lead.CustomerId == -1)
            {
                if (newCustomerIDScopeVar == string.Empty)
                {
                    throw new ApplicationException("GetLeadStatusLogSQL(): attempt to write to status log without valid customer id scope variable name.");
                }

                //
                // Assume we're inserting, and use the value that should already exist in the SQL for SCOPE_IDENTITY()
                //
                sql.AddFieldValue(new TableFieldValue("customer_id", newCustomerIDScopeVar, TableFieldValue.SQuoteBehavior.NoSQuote));
                
            }
            else
            {
                sql.AddFieldValue(new TableFieldValue("customer_id", lead.CustomerId));
            }
            sql.AddFieldValue(new TableFieldValue("lsl_status_date", "GetDate()", TableFieldValue.SQuoteBehavior.NoSQuote));
            sql.AddFieldValue(new TableFieldValue("lsl_status_code", (int)code));
            sql.AddFieldValue(new TableFieldValue("lsl_status_comment", comment));

            //sql += "insert LEAD_STATUS_LOG (customer_id, lsl_status_date, lsl_status_code, lsl_status_comment) \r\n";
            //sql += string.Format("values ({0},  \r\n", lead.CustomerId);
            //sql += string.Format("          GetDate(), \r\n");
            //sql += string.Format("          {0}, \r\n", (int)code);
            //sql += string.Format("          {0}) \r\n", SQuote(comment));

            return sql.ToString();
        }

        /// <summary>
        /// Update an existing lead's status code by writing to the status log.
        /// </summary>
        /// <param name="lead">The lead to updated.</param>
        /// <param name="code">The status code to write.</param>
        /// <returns>True if it worked.</returns>
        public static bool WriteLeadStatusLog(Lead lead, Lead.LeadStatusCode code)
        {
            return WriteLeadStatusLog(lead, code, string.Empty);
        }

        /// <summary>
        /// Update an existing lead's status code by writing to the status log.
        /// </summary>
        /// <param name="lead">The lead to updated.</param>
        /// <param name="code">The status code to write.</param>
        /// <param name="comment">(Optional) A comment.</param>
        /// <returns>True if it worked.</returns>
        public static bool WriteLeadStatusLog(Lead lead, Lead.LeadStatusCode code, string comment)
        {
            string sql = GetLeadStatusLogSQL(lead, code, comment);
            return (ExecuteNonQuery(sql) == 1);
        }

        /// <summary>
        /// Write a customer object.  NOTE: UPDATE IS A WORK IN PROGRESS!!
        /// </summary>
        /// <param name="customer">The customer object.</param>
        /// <param name="mode">The mode (insert, update, delete)</param>
        /// <returns>True if it worked.</returns>
        public static bool WriteCustomer(Customer customer, ObjectWriteMode mode)
        {
            bool result = false;

            if (mode == ObjectWriteMode.Insert)
            {
                // Todo: move insert customer code here, use SQLGenerator class
                return InsertCustomer(customer);
            }
            else if (mode == ObjectWriteMode.Update)
            {
                string sql = string.Empty;

                if (customer.CustomerId != -1)
                {
                    sql += BuildWriteCustomerSQLGenerator(customer, mode).ToString();

                    //
                    // Updating an address is the only thing supported right now
                    //
                    if (customer.ShippingAddress != null && customer.ShippingAddress.AddressId != -1)
                    {
                        sql += BuildAddressSQL(customer.ShippingAddress, ObjectWriteMode.Update, customer, Address.AddrTypeCode.atcShipping, "");

                        //
                        // Not really needed to transactionize here, but let's try out the new code.
                        //
                        sql = SQLGenerator.Transactionize(sql);

                        result = (ExecuteNonQuery(sql) > 0);
                    }
                }
            }
            else
            {
                throw new ApplicationException("Only insert supported at this time.");
            }

            return result;
        }

        /// <summary>
        /// Get a state code for USA.
        /// </summary>
        /// <param name="abbrev">The abbreviated code (e.g. CA)</param>
        /// <returns>The state code, or -1 if not found</returns>
        public static int GetStateProvCode(string abbrev)
        {
            return GetStateProvCode(abbrev, "USA");
        }

        /// <summary>
        /// Get a state code for the passed country.
        /// </summary>
        /// <param name="abbrev">The abbreviated code (e.g. CA)</param>
        /// <param name="countryCode">The country code (e.g. USA or CAN)</param>
        /// <returns>The state code, or -1 if not found</returns>
        public static int GetStateProvCode(string abbrev, string countryCode)
        {
            int result = -1;
            string sql = "";

            sql += "select * from STATE_PROV_CNTY  \r\n";
            sql += string.Format("where spc_national_abbrev = '{0}' \r\n", abbrev);
            sql += string.Format("and country_code = '{0}'\r\n", countryCode);

            DataSet res = new DataSet();

            GetQueryResults(res, sql);

            if (res.Tables[0].Rows.Count == 1)
            {
                //foreach (DataRow row in res.Tables[0].Rows)
                //{
                result = GetIntFromDataRow(res.Tables[0].Rows[0], "state_prov_code", -1);
            }
            else
            {
                // Hmmmmm... no exist.  What to do?
            }

            return result;
        }

        /// <summary>
        /// Builds the address changing SQL for the CUSTOMER_ADDRESS table.
        /// </summary>
        /// <param name="address">An address object.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="customer">A customer object to grab fields from as necessary.</param>
        /// <param name="typeCode">Type of address.</param>
        /// <param name="newCustomerIDScopeVar">(Optional) The scope identity variable, typically created by SQLGenerator class.</param>
        /// <returns>The handy dandy SQL.</returns>
        private static string BuildAddressSQL(Address address, ObjectWriteMode mode, Customer customer, Address.AddrTypeCode typeCode, string newCustomerIDScopeVar)
        {
            string sql = string.Empty;

            if (address != null)
            {
                SQLGenerator sqlGen = new SQLGenerator("CUSTOMER_ADDRESS");

                sqlGen.AddFieldValue(new TableFieldValue("customer_addr_id", address.AddressId, TableFieldValue.SpecialColumnBehavior.AddToWhereFilterForUpdate));
                sqlGen.AddFieldValue(new TableFieldValue("addr_person_fname", Functions.FirstNonemptyString(address.FirstName, customer.FirstName)));
                sqlGen.AddFieldValue(new TableFieldValue("addr_person_lname", Functions.FirstNonemptyString(address.LastName, customer.LastName)));
                sqlGen.AddFieldValue(new TableFieldValue("addr_company_name", Functions.FirstNonemptyString(address.CompanyName, customer.CompanyName)));
                sqlGen.AddFieldValue(new TableFieldValue("addr_line1", address.Address1));
                sqlGen.AddFieldValue(new TableFieldValue("addr_line2", address.Address2));
                sqlGen.AddFieldValue(new TableFieldValue("addr_city", address.City));
                sqlGen.AddFieldValue(new TableFieldValue("state_prov_code", address.StateProvCode));
                sqlGen.AddFieldValue(new TableFieldValue("addr_zip_postal", address.ZipPostalCode));
                sqlGen.AddFieldValue(new TableFieldValue("country_code", address.CountryCode));
                sqlGen.AddFieldValue(new TableFieldValue("addr_phone", address.Phone));

                sql += sqlGen.GenerateSQL(mode);

                if (mode == ObjectWriteMode.Insert)
                {
                    sqlGen.Reset("CUSTOMER_ADDR_ASSOC");

                    //
                    // Since we're inserting, and customer will be new, we have to know the customer id.  
                    //
                    sqlGen.AddFieldValue(new TableFieldValue("customer_id", newCustomerIDScopeVar, TableFieldValue.SQuoteBehavior.NoSQuote));
                    sqlGen.AddFieldValue(new TableFieldValue("attr_type_code", ((int)typeCode).ToString(), TableFieldValue.SQuoteBehavior.NoSQuote));

                    // 
                    // Customer address will also be new and have an identity, so get it here.
                    //
                    sqlGen.AddFieldValue(new TableFieldValue("customer_addr_id", "SCOPE_IDENTITY()", TableFieldValue.SQuoteBehavior.NoSQuote));

                    sql += sqlGen.GenerateSQL(ObjectWriteMode.Insert);
                }
            }

            return sql;
        }

        /// <summary>
        /// Get the states/provinces/counties for a given country.
        /// </summary>
        /// <param name="countryCode">Country code to get data for</param>
        /// <returns>Hashtable of strings - state code is key, description is value.</returns>
        public static List<DictionaryEntry> GetStates(string countryCode)
        {
            List<DictionaryEntry> result = new List<DictionaryEntry>();

            string sql = "";

            sql += "select * from STATE_PROV_CNTY  \r\n";
            sql += string.Format("where country_code = '{0}' \r\n", countryCode);
            sql += "order by state_prov_desc  \r\n";

            DataSet stateDataList = new DataSet();

            GetQueryResults(stateDataList, sql);

            foreach (DataRow stateData in stateDataList.Tables[0].Rows)
            {
                string stateCode = GetStringFromDataRow(stateData, "state_prov_code");
                string stateDesc = GetStringFromDataRow(stateData, "state_prov_desc");

                //
                // Use the description as the key; to preserve the order, and we know it'll be unique.
                //
                result.Add(new DictionaryEntry(stateCode, stateDesc));
            }

            return result;

        }

        /// <summary>
        /// Get the material from the database that has the color specified.
        /// </summary>
        /// <param name="colorDescription">The color to get.</param>
        /// <returns>A Material object if one and only one is found.</returns>
        public static Material GetMaterialOfColorDesc(string colorDescription)
        {
            Material result = null;

            List<Material> res = GetMaterials(0, colorDescription);
            if (res != null && res.Count == 1)
            {
                result = res[0];
            }

            return result;
        }

        /// <summary>
        /// Get all the sticker materials from the database.
        /// </summary>
        /// <returns>A list of materials.</returns>
        public static List<Material> GetMaterials()
        {
            return GetMaterials(0);
        }

        /// <summary>
        /// Get all the sticker materials from the database.
        /// </summary>
        /// <param name="materialTypeCode">The type to get</param>
        /// <returns>A list of materials.</returns>
        public static List<Material> GetMaterials(int materialTypeCode)
        {
            return GetMaterials(materialTypeCode, string.Empty);
        }

        /// <summary>
        /// Get all the sticker materials from the database.
        /// </summary>
        /// <param name="materialTypeCode">The type to get</param>
        /// <param name="colorDescription">The color description to use.</param>
        /// <returns>A list of materials.</returns>
        public static List<Material> GetMaterials(int materialTypeCode, string colorDescription)
        {
            List<Material> result = new List<Material>();

            string sql = "";

            sql += "select * from MATERIAL  \r\n";
            sql += string.Format("where material_type_code = {0} \r\n", materialTypeCode);
            sql += string.Format("and mat_ffd_offered = 1 \r\n");

            if (colorDescription != string.Empty)
            {
                sql += string.Format("and mat_mfg_color_desc = '{0}' \r\n", colorDescription);
            }

            sql += "order by mat_color_rgb_hex DESC \r\n";

            DataSet materialsDataSet = new DataSet();

            GetQueryResults(materialsDataSet, sql);

            foreach (DataRow materialsDataRow in materialsDataSet.Tables[0].Rows)
            {
                Material material = new Material();
                material.MaterialId = GetIntFromDataRow(materialsDataRow, "material_id", -1);
                material.MaterialTypeCode = GetIntFromDataRow(materialsDataRow, "material_type_code", -1);
                material.RGBColorHex = GetStringFromDataRow(materialsDataRow, "mat_color_rgb_hex");
                material.MfgPartNo = GetStringFromDataRow(materialsDataRow, "mat_mfg_part_no");
                material.MfgColorDesc = GetStringFromDataRow(materialsDataRow, "mat_mfg_color_desc");
                material.FfdOffered = GetBoolFromDataRow(materialsDataRow, "mat_ffd_offered", false);
                material.FfdInStock = GetBoolFromDataRow(materialsDataRow, "mat_ffd_in_stock", false);
                material.Brightness = GetIntFromDataRow(materialsDataRow, "mat_color_brightness", -1);

                result.Add(material);
            }

            return result;
        }

        /// <summary>
        /// Gets an existing lead from the database corresponding to the lead data in the passed row.
        /// </summary>
        /// <param name="row">A data row containing the lead data, typically recieved from a mailing list error file.</param>
        /// <returns>The lead, if found, otherwise null.</returns>
        public static Lead GetLeadFromMailingListErrorCSVDataRow(DataRow row)
        {
            Lead result = null;

            string leadIdTemp = GetStringFromDataRow(row, "lead_id");
            int leadId = -1;
            if (int.TryParse(leadIdTemp, out leadId))
            {
                Lead temp = GetLeadByCustomerId(leadId);

                if (temp != null)   // && temp.CompanyName == GetStringFromDataRow(row, "company"))
                {
                    //
                    // Bingo!
                    //
                    result = temp;
                }
            }

            return result;
        }
        /// <summary>
        /// Gets a Lead object from a datarow of a CSV file (converted from XLS by MS Excel) from Fundraising Ideas website.
        /// </summary>
        /// <param name="row">The datarow containing the lead data.</param>
        /// <returns>A Lead object or null if no workie.</returns>
        public static Lead GetLeadFromFundraisingIdeasCSVDataRow(DataRow row)
        {
            Lead lead = new Lead();

            lead.FirstName = Functions.TitleCase(GetStringFromDataRow(row, "namefirst"));
            lead.LastName = Functions.TitleCase(GetStringFromDataRow(row, "namelast"));
            lead.BillingAddress = new Address();
            lead.BillingAddress.Address1 = Functions.TitleCase(GetStringFromDataRow(row, "address"));
            lead.BillingAddress.City = Functions.TitleCase(GetStringFromDataRow(row, "city"));

            string countryCode = GetCountryCodeFromDesc(GetStringFromDataRow(row, "country"));
            lead.BillingAddress.CountryCode = countryCode;
            lead.BillingAddress.StateProvCode = GetStateProvCode(GetStringFromDataRow(row, "state"), countryCode);

            lead.BillingAddress.ZipPostalCode = GetStringFromDataRow(row, "zip");
            lead.PhoneDay = GetStringFromDataRow(row, "phone");
            lead.EmailAddress = GetStringFromDataRow(row, "email");
            lead.LeadDate = GetDateTimeFromDataRow(row, "time");
            lead.LeadSourceCode = 1;

            lead.LeadInfoText = string.Format("group: {0}\r\ninterests: {1}\r\ncomments: {2}",
                GetStringFromDataRow(row, "group"),
                GetStringFromDataRow(row, "interests"),
                GetStringFromDataRow(row, "comments"));

            lead.ParseContactMethodDescription(GetStringFromDataRow(row, "contact"));

            return lead;
        }

        public static bool UpdateLeadsStatusCode(string sourceCode, string origStatusCode, string newStatusCode)
        {
            List<Lead> leadsToUpdate = GetLeads(sourceCode, origStatusCode);

            foreach (Lead lead in leadsToUpdate)
            {
                // WriteLeadStatusLog(lead, Lead.LeadStatusCode
            }

            return false;
        }

        /// <summary>
        /// Gets a country code from the passed description.
        /// </summary>
        /// <param name="desc">A description, like "United States"</param>
        /// <returns>The country code, or "---" if it cannot be determined uniquely.</returns>
        public static string GetCountryCodeFromDesc(string desc)
        {
            string result = "---";      
            string sql = "";

            sql += "select * from COUNTRY  \r\n";
            sql += string.Format("where country_desc like '%{0}%' \r\n", desc);

            DataSet res = new DataSet();

            GetQueryResults(res, sql);

            if (res.Tables[0].Rows.Count == 1)
            {
            //foreach (DataRow row in res.Tables[0].Rows)
            //{
                result = GetStringFromDataRow(res.Tables[0].Rows[0], "country_code");
            }

            return result;
        }

        /// <summary>
        /// Get the country name from the passed abbreviation
        /// </summary>
        /// <param name="abbrev">The country abbreviation (e.g. "USA" or "CAN")</param>
        /// <param name="matchIfPrefix">Only match first few letters, like "US" will match "USA" and "CA" will match "CAN"</param>
        /// <returns></returns>
        public static string GetCountryFromAbbrev(string abbrev, bool matchIfPrefix)
        {
            string result = string.Empty;
            string sql = "";

            sql += "select * from COUNTRY  \r\n";
            sql += string.Format("where country_code {0} '{1}{2}' \r\n", 
                matchIfPrefix ? "like" : "=", 
                abbrev,
                matchIfPrefix ? "%" : "");

            DataSet res = GetQueryResults(sql);

            if (res.Tables.Count > 0 && res.Tables[0].Rows.Count == 1)
            {
                //foreach (DataRow row in res.Tables[0].Rows)
                //{
                result = GetStringFromDataRow(res.Tables[0].Rows[0], "country_desc");
            }

            return result;
        }

        public static List<Code> GetLeadSourceCodes()
        {
            return GetCodeTable("LEAD_SOURCE_CODE", "lead_source_code", "lead_source_desc");
        }

        public static List<Code> GetLeadStatusCodes()
        {
            return GetCodeTable("LEAD_STATUS_CODE", "lead_status_code", "lead_status_desc");
        }

        /// <summary>
        /// Return the values in a code database table.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="codeColumnName">The column name containing the code.</param>
        /// <param name="descColumnName">The column name containing the description.</param>
        /// <returns>A dictionary with the code value in the first string and the description in the second string.</returns>
        private static List<Code> GetCodeTable(string tableName, string codeColumnName, string descColumnName)
        {
            List<Code> result = new List<Code>();

            string sql = string.Format("select * from {0} order by {1}", tableName, codeColumnName);

            DataSet res = new DataSet();

            GetQueryResults(res, sql);

            if (res.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in res.Tables[0].Rows)
                {
                    result.Add(new Code(GetStringFromDataRow(row, codeColumnName), 
                        GetStringFromDataRow(row, descColumnName)));
                }
            }

            return result;
        }

        /// <summary>
        /// Get a DataSet object with the results of the database call.
        /// </summary>
        /// <param name="sql">SQL command to execute</param>
        /// <param name="sqlParams">Pameters to pass to sql command (if any)</param>
        /// <returns>A non-null dataset (may be empty)</returns>
        public static DataSet GetQueryResults(string sql, params SqlParameter[] sqlParams)
        {
            DataSet result = new DataSet();
            GetQueryResults(result, sql, sqlParams);
            return result;
        }

        /// <summary>
        /// Fill this DataSet object with the results of the database call.
        /// </summary>
        /// <param name="ds">The dataset to fill</param>
        /// <param name="sql">SQL command to execute</param>
        /// <param name="sqlParams">Pameters to pass to sql command (if any)</param>
        public static void GetQueryResults(DataSet ds, string sql, params SqlParameter[] sqlParams)
        {
            //
            // Connect to the database
            //
            SqlConnection connection = new SqlConnection(ConnectionString());

            try
            {
                connection.Open();

                //
                // Specify the command to use to query the database
                //
                SqlCommand command = new SqlCommand(sql, connection);

                //
                // Add parameters if we have any
                //
                foreach (SqlParameter parameter in sqlParams)
                {
                    command.Parameters.Add(parameter);
                }

                //
                // Connect to db and run the query
                //
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);

                dataAdapter.Fill(ds);
            }
            //catch(Exception e)
            //{
            //}
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Execute a command that doesn't return stuff, with parameters if you got 'em.  Returns # of rows affected.
        /// </summary>
        /// <param name="sql">The sql to execute.</param>
        /// <param name="sqlParams">Optional - parameters.</param>
        /// <returns>Number of rows affected.</returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] sqlParams)
        {
            int result = -1;

            //
            // Connect to the database
            //
            SqlConnection connection = new SqlConnection(ConnectionString());

            try
            {
                connection.Open();

                //
                // Specify the command to use to query the database
                //
                SqlCommand command = new SqlCommand(sql, connection);

                //
                // Add parameters if we have any
                //
                foreach (SqlParameter parameter in sqlParams)
                {
                    command.Parameters.Add(parameter);
                }

                result = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }

            return result;
        }


        /// <summary>
        /// Gets the connection string for this computer to the database
        /// </summary>
        /// <returns>Connection string</returns>
        /// <remarks>
        /// See article on MSDN for connection stuff: "Accessing SQL Server with Explicit Credentials" (google it)
        /// ASPNET user is an explicit user created in SQL Manager, then associated with this DB and with role of "public".
        /// You have to manually grant permissions to public on all SQL server tables - I wrote a script for that, see the script.
        /// ASPNET is not a good name, since it has nothing to do with ASP.  The fat client uses the same user.  Oh well.
        /// </remarks>
        public static string ConnectionString()
        {
            return string.Format("Data Source=fanfavmain\\sqlexpress;Initial Catalog=FFDMAIN;user id=ASPNET;password=[redacted!]");
        }
    }
}
