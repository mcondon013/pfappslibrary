using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using System.Data;
using PFProcessObjects;
using PFMessageLogs;
using PFDataAccessObjects;
using PFTextFiles;
using PFCollectionsObjects;
using PFAppDataObjects;
using PFAppDataForms;

namespace TestprogAppDataForms
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog = null;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";

        private string _helpFilePath = string.Empty;


        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }

        /// <summary>
        /// Path to application help file.
        /// </summary>
        public string HelpFilePath
        {
            get
            {
                return _helpFilePath;
            }
            set
            {
                _helpFilePath = value;
            }
        }


        //application routines

        public void ShowFixedLenInputColSpecForm(string tableName)
        {
            PFFixedLenColDefsInputForm frm = new PFFixedLenColDefsInputForm();
            DialogResult res = DialogResult.None;
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            DataTable dt = null;
            PFColumnDefinitionsExt colDefsExt = null;
            string configValue = string.Empty;
            string configKey = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowFixedLenInputColSpecForm started ...\r\n");
                WriteToMessageLog(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_SQLServerCE35", string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find config.sys entry for DefaultConnection_SQLServerCE35");
                    throw new System.Exception(_msg.ToString());
                }
                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                connStr = configValue;

                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                configKey = dbPlatformDesc + "_" + tableName;
                configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
                if (configValue == string.Empty) 
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(configKey);
                    _msg.Append(" in config.sys.");
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                //db.SQLQuery = "select  CustomerKey ,GeographyKey ,CustomerAlternateKey ,Title ,FirstName ,MiddleName ,LastName ,NameStyle ,BirthDate ,MaritalStatus ,Suffix ,Gender ,EmailAddress ,YearlyIncome ,TotalChildren ,NumberChildrenAtHome ,EnglishEducation ,SpanishEducation ,FrenchEducation ,EnglishOccupation ,SpanishOccupation ,FrenchOccupation ,HouseOwnerFlag ,NumberCarsOwned ,AddressLine1 ,AddressLine2 ,Phone ,DateFirstPurchase ,CommuteDistance from DimCustomer where 1=0";
                db.SQLQuery = configValue;
                db.CommandType = CommandType.Text;

                dt = db.GetQueryDataSchema();

                //colDefsExt = PFColumnDefinitionsExt.GetColumnDefinitionsExt(dt);

                colDefsExt = new PFColumnDefinitionsExt(1);

                frm.ColDefs = colDefsExt;

                frm.MessageLogUI = _messageLog;
                res = frm.ShowDialog();

                colDefsExt = frm.ColDefs;

                _msg.Length = 0;
                _msg.Append("Form closed with DialogResult = ");
                _msg.Append(res.ToString());
                WriteToMessageLog(_msg.ToString());

                if (res == DialogResult.OK)
                {
                    _msg.Length = 0;
                    _msg.Append(Environment.NewLine);
                    _msg.Append("COLUMN DEFINITIONS:\r\n\r\n");
                    _msg.Append(colDefsExt.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    WriteToMessageLog(_msg.ToString());
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.IsConnected)
                    db.CloseConnection();
                db = null;

                if (frm != null)
                {
                    if(FormIsOpen(frm.Name))
                        frm.Close();
                }
                frm = null;

                _msg.Length = 0;
                _msg.Append("\r\n... ShowFixedLenInputColSpecForm finished.");
                WriteToMessageLog(_msg.ToString());

                
            }
        }



        public void ShowFilterBuilderForm(string tableName)
        {
            PFFilterBuilderForm frm = new PFFilterBuilderForm();
            DialogResult res = DialogResult.None;
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            DataTable dt = null;
            PFList<PFFilterDef> filterDefs = null;
            PFColumnDefinitionsExt colDefsExt = null;
            string configValue = string.Empty;
            string configKey = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowFilterBuilderForm started ...\r\n");
                 WriteToMessageLog(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_SQLServerCE35", string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find config.sys entry for DefaultConnection_SQLServerCE35");
                    throw new System.Exception(_msg.ToString());
                }
                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                connStr = configValue;

                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                configKey = dbPlatformDesc + "_" + tableName;
                configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(configKey);
                    _msg.Append(" in config.sys.");
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                //db.SQLQuery = "select  CustomerKey ,GeographyKey ,CustomerAlternateKey ,Title ,FirstName ,MiddleName ,LastName ,NameStyle ,BirthDate ,MaritalStatus ,Suffix ,Gender ,EmailAddress ,YearlyIncome ,TotalChildren ,NumberChildrenAtHome ,EnglishEducation ,SpanishEducation ,FrenchEducation ,EnglishOccupation ,SpanishOccupation ,FrenchOccupation ,HouseOwnerFlag ,NumberCarsOwned ,AddressLine1 ,AddressLine2 ,Phone ,DateFirstPurchase ,CommuteDistance from DimCustomer where 1=0";
                db.SQLQuery = configValue;
                db.CommandType = CommandType.Text;

                dt = db.GetQueryDataSchema();

                colDefsExt = PFColumnDefinitionsExt.GetColumnDefinitionsExt(dt);
                filterDefs = new PFList<PFFilterDef>();

                frm.QueryDataTable = dt;
                frm.FilterDefs = filterDefs;

                frm.MessageLogUI = _messageLog;
                res = frm.ShowDialog();

                filterDefs = frm.FilterDefs;

                _msg.Length = 0;
                _msg.Append("Form closed with DialogResult = ");
                _msg.Append(res.ToString());
                WriteToMessageLog(_msg.ToString());

                if (res == DialogResult.OK)
                {
                    _msg.Length = 0;
                    _msg.Append(Environment.NewLine);
                    _msg.Append("FILTER DEFINITIONS:\r\n\r\n");
                    _msg.Append(filterDefs.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    WriteToMessageLog(_msg.ToString());
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.IsConnected)
                    db.CloseConnection();
                db = null;

                if (frm != null)
                {
                    if (FormIsOpen(frm.Name))
                        frm.Close();
                }
                frm = null;

                _msg.Length = 0;
                _msg.Append("\r\n... ShowFilterBuilderForm finished.");
                 WriteToMessageLog(_msg.ToString());

            }
        }
                 
        
        
        
        private bool FormIsOpen(string name)
        {
            bool retval = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == name)
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }


        public void TestRowFilter()
        {
            string tableName = "DimCustomer";
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            DataTable dt = null;
            string configValue = string.Empty;
            string configKey = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("TestRowFilter started ...\r\n");
                 WriteToMessageLog(_msg.ToString());

                // 
                configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_SQLServerCE35", string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find config.sys entry for DefaultConnection_SQLServerCE35");
                    throw new System.Exception(_msg.ToString());
                }
                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                connStr = configValue;

                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                configKey = dbPlatformDesc + "_" + tableName;
                configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(configKey);
                    _msg.Append(" in config.sys.");
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                //db.SQLQuery = "select  CustomerKey ,GeographyKey ,CustomerAlternateKey ,Title ,FirstName ,MiddleName ,LastName ,NameStyle ,BirthDate ,MaritalStatus ,Suffix ,Gender ,EmailAddress ,YearlyIncome ,TotalChildren ,NumberChildrenAtHome ,EnglishEducation ,SpanishEducation ,FrenchEducation ,EnglishOccupation ,SpanishOccupation ,FrenchOccupation ,HouseOwnerFlag ,NumberCarsOwned ,AddressLine1 ,AddressLine2 ,Phone ,DateFirstPurchase ,CommuteDistance from DimCustomer where 1=0";
                db.SQLQuery = configValue;
                db.CommandType = CommandType.Text;

                //dt = db.GetQueryDataSchema();
                dt = db.RunQueryDataTable();

                DataView dv = new DataView(dt);
                dv.RowFilter = "CustomerKey NOT < 11200 and CustomerKey NOT > 11600";

                _msg.Length = 0;
                _msg.Append("Num rows before view: ");
                _msg.Append(dt.Rows.Count.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Num rows after view:  ");
                _msg.Append(dv.Count.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());

                DataTable dt2 = dt.AsEnumerable().Skip(0).Take(50).CopyToDataTable();
                _msg.Length = 0;
                _msg.Append("Num rows after AsEnumerable.Take:  ");
                _msg.Append(dt2.Rows.Count.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                 WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.IsConnected)
                    db.CloseConnection();
                db = null;

                _msg.Length = 0;
                _msg.Append("\r\n... TestRowFilter finished.");
                 WriteToMessageLog(_msg.ToString());

            }
        }



        public void ShowFilterForm(string tableName, string filterFile)
        {
            PFFilterForm frm = new PFFilterForm();
            DialogResult res = DialogResult.None;
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            DataTable dt = null;
            PFFilter filter = new PFFilter();
            DataView dv = null;
            string configValue = string.Empty;
            string configKey = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowFilterForm started ...\r\n");
                WriteToMessageLog(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_SQLServerCE35", string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find config.sys entry for DefaultConnection_SQLServerCE35");
                    throw new System.Exception(_msg.ToString());
                }
                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                connStr = configValue;

                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                if(String.IsNullOrEmpty(filterFile))
                {
                    configKey = dbPlatformDesc + "_" + tableName;
                    configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
                    if (configValue == string.Empty)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find ");
                        _msg.Append(configKey);
                        _msg.Append(" in config.sys.");
                        AppMessages.DisplayErrorMessage(_msg.ToString());
                        return;
                    }
                    //db.SQLQuery = "select  CustomerKey ,GeographyKey ,CustomerAlternateKey ,Title ,FirstName ,MiddleName ,LastName ,NameStyle ,BirthDate ,MaritalStatus ,Suffix ,Gender ,EmailAddress ,YearlyIncome ,TotalChildren ,NumberChildrenAtHome ,EnglishEducation ,SpanishEducation ,FrenchEducation ,EnglishOccupation ,SpanishOccupation ,FrenchOccupation ,HouseOwnerFlag ,NumberCarsOwned ,AddressLine1 ,AddressLine2 ,Phone ,DateFirstPurchase ,CommuteDistance from DimCustomer where 1=0";
                    db.SQLQuery = configValue;
                    filter = new PFFilter();
                }
                else
                {
                    if (File.Exists(filterFile))
                    {
                        filter = PFFilter.LoadFromXmlFile(filterFile);
                        db.SQLQuery = filter.QueryText;
                    }
                    else
                    {
                        configKey = dbPlatformDesc + "_" + tableName;
                        configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
                        if (configValue == string.Empty)
                        {
                            _msg.Length = 0;
                            _msg.Append("Unable to find ");
                            _msg.Append(configKey);
                            _msg.Append(" in config.sys.");
                            AppMessages.DisplayErrorMessage(_msg.ToString());
                            return;
                        }
                        //db.SQLQuery = "select  CustomerKey ,GeographyKey ,CustomerAlternateKey ,Title ,FirstName ,MiddleName ,LastName ,NameStyle ,BirthDate ,MaritalStatus ,Suffix ,Gender ,EmailAddress ,YearlyIncome ,TotalChildren ,NumberChildrenAtHome ,EnglishEducation ,SpanishEducation ,FrenchEducation ,EnglishOccupation ,SpanishOccupation ,FrenchOccupation ,HouseOwnerFlag ,NumberCarsOwned ,AddressLine1 ,AddressLine2 ,Phone ,DateFirstPurchase ,CommuteDistance from DimCustomer where 1=0";
                        db.SQLQuery = configValue;
                        filter = new PFFilter();
                    }
                }
                db.CommandType = CommandType.Text;
                    
                dt = db.GetQueryDataSchema();

                filter.QueryText = db.SQLQuery;

                frm.QueryText = db.SQLQuery;
                frm.QueryDataTable = dt;
                frm.Filter = filter;

                frm.MessageLogUI = _messageLog;
                res = frm.ShowDialog();

                filter = frm.Filter;
                dv = frm.QueryDataView;

                _msg.Length = 0;
                _msg.Append("Form closed with DialogResult = ");
                _msg.Append(res.ToString());
                WriteToMessageLog(_msg.ToString());

                if (res == DialogResult.Abort)
                {
                    _msg.Length = 0;
                    _msg.Append("Form open failed due to error.");
                    WriteToMessageLog(_msg.ToString());
                }

            if (res == DialogResult.OK)
                {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("FILTER OBJECT:\r\n\r\n");
                _msg.Append(filter.ToXmlString());
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());
                if(String.IsNullOrEmpty(filterFile)== false)
                    filter.SaveToXmlFile(filterFile);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                 WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.IsConnected)
                    db.CloseConnection();
                db = null;

                if (frm != null)
                {
                    if (FormIsOpen(frm.Name))
                        frm.Close();
                }
                frm = null;

                _msg.Length = 0;
                _msg.Append("\r\n... ShowFilterForm finished.");
                 WriteToMessageLog(_msg.ToString());

            }
        }


        public void ShowFixedLenOutputColSpecForm(string tableName)
        {
            PFFixedLenColDefsOutputForm frm = new PFFixedLenColDefsOutputForm();
            DialogResult res = DialogResult.None;
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            DataTable dt = null;
            PFColumnDefinitionsExt colDefsExt = null;
            string configValue = string.Empty;
            string configKey = string.Empty;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowFixedLenOutputColSpecForm started ...\r\n");
                WriteToMessageLog(_msg.ToString());

                configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_SQLServerCE35", string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find config.sys entry for DefaultConnection_SQLServerCE35");
                    throw new System.Exception(_msg.ToString());
                }
                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                connStr = configValue;

                dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                configKey = dbPlatformDesc + "_" + tableName;
                configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
                if (configValue == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find ");
                    _msg.Append(configKey);
                    _msg.Append(" in config.sys.");
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                //db.SQLQuery = "select  CustomerKey ,GeographyKey ,CustomerAlternateKey ,Title ,FirstName ,MiddleName ,LastName ,NameStyle ,BirthDate ,MaritalStatus ,Suffix ,Gender ,EmailAddress ,YearlyIncome ,TotalChildren ,NumberChildrenAtHome ,EnglishEducation ,SpanishEducation ,FrenchEducation ,EnglishOccupation ,SpanishOccupation ,FrenchOccupation ,HouseOwnerFlag ,NumberCarsOwned ,AddressLine1 ,AddressLine2 ,Phone ,DateFirstPurchase ,CommuteDistance from DimCustomer where 1=0";
                db.SQLQuery = configValue;
                db.CommandType = CommandType.Text;

                dt = db.GetQueryDataSchema();

                colDefsExt = PFColumnDefinitionsExt.GetColumnDefinitionsExt(dt);


                frm.ColDefs = colDefsExt;

                frm.MessageLogUI = _messageLog;
                res = frm.ShowDialog();

                colDefsExt = frm.ColDefs;

                _msg.Length = 0;
                _msg.Append("Form closed with DialogResult = ");
                _msg.Append(res.ToString());
                WriteToMessageLog(_msg.ToString());

                if (res == DialogResult.OK)
                {
                    _msg.Length = 0;
                    _msg.Append(Environment.NewLine);
                    _msg.Append("COLUMN DEFINITIONS:\r\n\r\n");
                    _msg.Append(colDefsExt.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    WriteToMessageLog(_msg.ToString());
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (db.IsConnected)
                    db.CloseConnection();
                db = null;

                if (frm != null)
                {
                    if (FormIsOpen(frm.Name))
                        frm.Close();
                }
                frm = null;

                _msg.Length = 0;
                _msg.Append("\r\n... ShowFixedLenOutputColSpecForm finished.");
                WriteToMessageLog(_msg.ToString());


            }
        }


        public void ShowFxlVisualParseForm()
        {
            PFFixedLenColDefsVisualParseForm fxlForm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ShowFxlVisualParseForm started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                fxlForm = new PFFixedLenColDefsVisualParseForm();
                
                
                //System.Threading.Thread formThread = new System.Threading.Thread(FxlVisualParseFormThread);
                //formThread.SetApartmentState(System.Threading.ApartmentState.STA);
                //formThread.Start();

                fxlForm.TextLine1 = "LNTYPFIELD1         FIELDB            FIELDC                    FIELD_D                     COLHDR_E            FINALCOL       NUMBERS   ";
                fxlForm.TextLine2 = "LINE2xxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyzzzzzzzzzzzzzzzzzzzzzzzzzzddddddddddddddddddddddddddddeeeeeeeeeeeeeeeeeeeefffffffffffffff0123456789";
                fxlForm.ExpectedLineWidth = fxlForm.TextLine1.Length;
                fxlForm.ColumnNamesOnFirstLine = true;

                fxlForm.ShowDialog();
                //fxlForm.Focus();
                //fxlForm.Close();

                PFFixedLenColDefsVisualParseForm.ColNameAndLength[] colNamesAndLengths = fxlForm.GetFieldNamesAndLengths();
                
                fxlForm.Close();

                DisplayColNamesAndLengths(colNamesAndLengths);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... ShowFxlVisualParseForm finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private void DisplayColNamesAndLengths(PFFixedLenColDefsVisualParseForm.ColNameAndLength[] colNamesAndLengths)
        {
            int maxInx = colNamesAndLengths.Length - 1;
            string values = string.Empty;

            for (int i = 0; i < maxInx; i++)
            {
                if (i > 0)
                {
                    values += Environment.NewLine + colNamesAndLengths[i].colName + ", " + colNamesAndLengths[i].colLength;
                }
                else
                {
                    values += colNamesAndLengths[i].colName + ", " + colNamesAndLengths[i].colLength;
                }
            }

            _msg.Length = 0;
            _msg.Append("Field names and lengths are: ");
            _msg.Append(Environment.NewLine);
            _msg.Append(values);
            AppMessages.DisplayInfoMessage(_msg.ToString());
        }

        //private void FxlVisualParseFormThread()
        //{
        //    Application.Run(fxlForm);
        //    fxlForm.Focus();
        //}
        

        /// <summary>
        /// Routine to add context menu item for pfFolderSize to Windows Explorer.
        /// </summary>
        public void ShowAppConfigManager()
        {
            PFProcess proc = new PFProcess();
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string currAppExePath = AppInfo.CurrentEntryAssembly;
            string appConfigManagerApp = Path.Combine(currAppFolder, _appConfigManagerExe);
            string mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool appConfigManagerFound = false;

            try
            {
                proc.Arguments = "\"" + currAppExePath + "\" \"" + _helpFilePath + "\" " + "\"Change an Application Setting\"";

                if (File.Exists(appConfigManagerApp))
                {
                    appConfigManagerFound = true;
                    proc.WorkingDirectory = currAppFolder;
                    proc.ExecutableToRun = appConfigManagerApp;
                }
                else
                {
                    string configValue = AppConfig.GetStringValueFromConfigFile("appConfigManagerPath", string.Empty);
                    if (configValue.Length > 0)
                    {
                        if (File.Exists(configValue))
                        {
                            appConfigManagerFound = true;
                            proc.WorkingDirectory = Path.GetDirectoryName(configValue);
                            proc.ExecutableToRun = configValue;
                        }
                        else
                        {
                            appConfigManagerFound = false;
                        }
                    }
                    else
                    {
                        appConfigManagerFound = false;

                    }
                }
                if (appConfigManagerFound == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find Application Configuration Manager Application in current app folder: ");
                    _msg.Append(currAppFolder);
                    throw new System.Exception(_msg.ToString());
                }
                proc.CreateNoWindow = true;
                proc.UseShellExecute = true;
                proc.WindowStyle = PFProcessWindowStyle.Normal;
                proc.RedirectStandardOutput = false;
                proc.RedirectStandardError = false;
                proc.RedirectStandardInput = false;
                proc.CheckIfProcessWaitingForInput = false;
                proc.MaxProcessRunSeconds = (int)0;

                proc.Run();

                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                proc = null;
            }

        }


        private void WriteToMessageLog(string msg)
        {
            if (_messageLog != null)
                _messageLog.WriteLine(msg);
        }


    }//end class
}//end namespace
