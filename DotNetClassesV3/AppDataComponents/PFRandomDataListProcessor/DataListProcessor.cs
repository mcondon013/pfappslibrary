using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.Xml;
using System.IO;
using System.Data;
using PFProcessObjects;
using PFMessageLogs;
using PFRandomDataProcessor;
using PFCollectionsObjects;
using PFDataOutputProcessor;
using PFDataAccessObjects;
using PFTextObjects;
using PFDataOutputGrid;
using PFTimers;

namespace PFRandomDataListProcessor
{
    /// <summary>
    /// Class contains methods to produce a set of random names and locations.
    /// </summary>
    public class DataListProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;

        private string _helpFilePath = string.Empty;

        private string _defaultRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private string _defaultRandomDataUserXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations\";
        private string _defaultCustomRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Custom\";
        private string _defaultGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\";
        private string _defaultGridNameListExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations\";
        private string _defaultGridCustomListExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Custom";

        private bool _showInstalledDatabaseProvidersOnly = true;
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private string _gridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\";

        //properties
        /// <summary>
        /// SaveErrorMessagesToAppLog property.
        /// </summary>
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

        /// <summary>
        /// Switch used by database selection controls to determine whether or not to list only installed database providers or to list all supported providers.
        /// </summary>
        public bool ShowInstalledDatabaseProvidersOnly
        {
            get
            {
                return _showInstalledDatabaseProvidersOnly;
            }
            set
            {
                _showInstalledDatabaseProvidersOnly = value;
            }
        }

        /// <summary>
        /// DefaultOutputDatabaseType Property.
        /// </summary>
        public string DefaultOutputDatabaseType
        {
            get
            {
                return _defaultOutputDatabaseType;
            }
            set
            {
                _defaultOutputDatabaseType = value;
            }
        }

        /// <summary>
        /// DefaultOutputDatabaseConnectionString Property.
        /// </summary>
        public string DefaultOutputDatabaseConnectionString
        {
            get
            {
                return _defaultOutputDatabaseConnectionString;
            }
            set
            {
                _defaultOutputDatabaseConnectionString = value;
            }
        }

        /// <summary>
        /// GridExportFolder property.
        /// </summary>
        public string GridExportFolder
        {
            get
            {
                return _gridExportFolder;
            }
            set
            {
                _gridExportFolder = value;
            }
        }

        //constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataListProcessor()
        {
            _defaultGridExportFolder = _gridExportFolder;
        }

        //application routines

        /// <summary>
        /// Retrieves a DataTable containing a list of random names.
        /// </summary>
        /// <param name="rdr">Object containing the constraints to be used in generating random names.</param>
        /// <param name="numEntriesToGenerate">Number of name entries to generate</param>
        /// <param name="outputToXmlFile">Set to true to output to an XML file.</param>
        /// <param name="xmlOutputFolder">Path to XML output folder.</param>
        /// <param name="listName">Name given to the list of names. This name will serveras the DataTable.TableName value.</param>
        /// <param name="outputToGrid">Set to true to output the names to a DataViewGrid.</param>
        /// <returns>DataTable populated with rows containing the generated names.</returns>
        public DataTable GetRandomNamesList(RandomNamesAndLocationsDataRequest rdr, int numEntriesToGenerate, bool outputToXmlFile, string xmlOutputFolder, string listName, bool outputToGrid)
        {
            RandomDataProcessor rdp = new RandomDataProcessor(rdr.DatabaseFilePath, rdr.DatabasePassword, rdr.RandomDataXmlFilesFolder);
            string xmlFileName = string.Empty;
            Stopwatch sw = new Stopwatch();
            DataTable dtRData = null;

            try
            {
                sw.Start();

                //rdr.SaveToXmlFile(@"c:\temp\CountryRequest.xml");

                rdp.CountryRandomDataSpec = rdr;

                PFList<RandomName> rn = rdp.GenerateRandomNameList(numEntriesToGenerate);

                //rn.SaveToXmlFile(@"c:\temp\RandomNamesPfList.xml");

                dtRData = rdp.ConvertRandomNameListToDataTable(rn);
                dtRData.TableName = listName;
                //for (int c = 0; c < dtRData.Columns.Count; c++)
                //{ 
                //    //workaround: RowNum column name causes Oracle ODBC and OLEDB drivers to fail
                //    if (dtRData.Columns[c].ColumnName == "RowNum")
                //        dtRData.Columns[c].ColumnName="NameRowNum";
                //}

                if (outputToXmlFile)
                {
                    string outputFile = string.Empty;
                    if (xmlOutputFolder.Trim() == string.Empty)
                        outputFile = Path.Combine(_defaultRandomDataUserXmlFilesFolder, listName + ".xml");
                    else
                        outputFile = Path.Combine(xmlOutputFolder, listName + ".xml");
                    XMLFileOutputProcessor xout = new XMLFileOutputProcessor(outputFile, true);
                    xout.XMLOutputType = enXMLOutputType.DataPlusSchema;
                    xout.WriteDataToOutput(dtRData);
                    //save data definition to an xml file
                    if (xmlOutputFolder.Trim() == string.Empty)
                        outputFile = Path.Combine(_defaultRandomDataUserXmlFilesFolder, listName + ".nlistdef");
                    else
                        outputFile = Path.Combine(xmlOutputFolder, listName + ".nlistdef");
                    rdr.SaveToXmlFile(outputFile);
                }

                sw.Stop();

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Random name list generation finished.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time for name list generation: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());

                if (outputToGrid)
                {
                    PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
                    grid.ShowInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
                    grid.DefaultOutputDatabaseType = this.DefaultOutputDatabaseType;
                    grid.DefaultOutputDatabaseConnectionString = this.DefaultOutputDatabaseConnectionString;
                    if (_gridExportFolder != _defaultGridExportFolder)
                        grid.DefaultGridExportFolder = _gridExportFolder;
                    else
                        grid.DefaultGridExportFolder = Path.Combine(_defaultGridExportFolder, "NamesAndLocations");
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter("NameRowNum", PFDataOutputGrid.enFilterType.NumRangeColumnFilter));
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter("Country", PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter("NameType", PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter("City", PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter("StateProvince", PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter("StateProvinceName", PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.WriteDataToGrid(dtRData);
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
                if (sw.StopwatchIsRunning)
                {
                    sw.Stop();
                }

            }

            return dtRData;
        }

        /// <summary>
        /// Produces DataTable containing custom random data.
        /// </summary>
        /// <param name="dataRequest">Object containg the definition for the custom data request.</param>
        /// <param name="maxTotalFrequency">Maximum total frequencies for random values.</param>
        /// <param name="outputToXmlFile">Set to true to store random values in an XML file.</param>
        /// <param name="outputToGrid">Set to true to output random values to a DataGridView object.</param>
        /// <returns>DataTable containing random values.</returns>
        /// <remakrs>Custom random data is generated by running custom queries against a database.</remakrs>
        public DataTable GetCustomRandomDataFile(RandomCustomValuesDataRequest dataRequest, int maxTotalFrequency, bool outputToXmlFile, bool outputToGrid)
        {
            bool generateResult = true;
            CustomDataListGenerator gen = new CustomDataListGenerator(dataRequest.DbPlatform);
            Stopwatch sw = new Stopwatch();
            string sqlUsedToGenerateDataValues = string.Empty;
            DataTable customListDataTableValues = null;

            sw.Start();


            try
            {
                if (dataRequest.DbTableName.Length == 0
                    || dataRequest.DbFieldName.Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must connect to the database and define a table name and field to use.");
                    throw new System.Exception(_msg.ToString());
                }

                if (dataRequest.SelectionCriteria.Trim().Length > 0
                    && dataRequest.SelectionCondition.Trim().Length > 0
                    && dataRequest.SelectionField.Trim().Length > 0)
                {
                    dataRequest.SelectionCriteria = VerifySelectionCriteriaFormat(dataRequest);
                }
                else
                {
                    dataRequest.SelectionCriteria = string.Empty;
                    dataRequest.SelectionCondition = string.Empty;
                    dataRequest.SelectionField = string.Empty;
                }

                //XML & related file locations are set in the gen.GenerateCustomRandomDataList routine
                gen.MaxTotalFrequencyCount = maxTotalFrequency;
                generateResult = gen.GenerateCustomRandomDataList(dataRequest, outputToXmlFile, out sqlUsedToGenerateDataValues, out customListDataTableValues);

                if (generateResult == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Attempt to generate custom random data list ");
                    _msg.Append(dataRequest.ListName);
                    _msg.Append(" failed.");
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("SQL statement for data retrieval:");
                _msg.Append(Environment.NewLine);
                _msg.Append(sqlUsedToGenerateDataValues);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());

                sw.Stop();

                if (outputToGrid)
                {
                    string filterColumnName = customListDataTableValues.Columns[0].ColumnName;
                    PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
                    grid.ShowInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
                    grid.DefaultOutputDatabaseType = this.DefaultOutputDatabaseType;
                    grid.DefaultOutputDatabaseConnectionString = this.DefaultOutputDatabaseConnectionString;
                    if (_gridExportFolder != _defaultGridExportFolder)
                        grid.DefaultGridExportFolder = _gridExportFolder;
                    else
                        grid.DefaultGridExportFolder = Path.Combine(_defaultGridExportFolder, "Custom");
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter(filterColumnName, PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.WriteDataToGrid(customListDataTableValues);
                    grid = null;
                    
                    //string customDataFileName = Path.Combine(dataRequest.ListFolder, dataRequest.ListName + ".xml");
                    //DataSet customData = new DataSet();
                    //customData.ReadXml(customDataFileName);
                    //customData.Tables[0].TableName = dataRequest.ListName;
                    //customData.Tables[0].Columns[0].ColumnName = "CustomDataValue";
                    DataTable customListTable = CreateCustomListTableForGridOutput(customListDataTableValues);
                    customListTable.TableName = dataRequest.ListName;
                    customListTable.Columns[0].ColumnName = "CustomDataValue";

                    grid = new PFDataOutputGrid.DataOutputGridProcessor();
                    grid.ShowInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
                    grid.DefaultOutputDatabaseType = this.DefaultOutputDatabaseType;
                    grid.DefaultOutputDatabaseConnectionString = this.DefaultOutputDatabaseConnectionString;
                    filterColumnName = customListTable.Columns[0].ColumnName;
                    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter(filterColumnName, PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
                    grid.WriteDataToGrid(customListTable);
                    grid = null;

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
                if (sw.StopwatchIsRunning)
                {
                    sw.Stop();
                }

                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Random custom data list generation finished.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time for data generation: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());


            }

            return customListDataTableValues;

        }

        private DataTable CreateCustomListTableForGridOutput(DataTable customListDataTableValues)
        {
            DataTable listTable = new DataTable();
            int frequency = 0;
            int valueInx = 0;
            //int frequencyInx = 1;
            int adjustedFrequencyInx = 2;
            //int adjustmentNumberInx = 3;

            DataColumn dc = new DataColumn("RandomValue", Type.GetType("System.String"));
            listTable.Columns.Add(dc);

            for (int i = 0; i < customListDataTableValues.Rows.Count; i++)
            {
                frequency = Convert.ToInt32(customListDataTableValues.Rows[i][adjustedFrequencyInx].ToString());
                for (int k = 0; k < frequency; k++)
                {
                    DataRow dr = listTable.NewRow();
                    dr[0] = customListDataTableValues.Rows[i][valueInx].ToString();
                    listTable.Rows.Add(dr);
                }
            }



            return listTable;
        }

        private string VerifySelectionCriteriaFormat(RandomCustomValuesDataRequest dataRequest)
        {
            string verifiedSelectionCriteria = dataRequest.SelectionCriteria;
            List<string> compareToValues = new List<string>();

            string[] nonCriteriaWords = { "not", "in", "like" };
            if (dataRequest.SelectionFieldType == "System.String")
            {
                char[] seps = { ' ', '(', ')', ',' };
                string[] words = dataRequest.SelectionCriteria.Split(seps);
                foreach (string word in words)
                {
                    bool wordIsCriteria = true;
                    foreach (string excludeWord in nonCriteriaWords)
                    {
                        if (word.ToLower() == excludeWord.ToLower())
                        {
                            wordIsCriteria = false;
                        }
                    }
                    if (word.Trim().Length == 0)
                    {
                        wordIsCriteria = false;
                    }
                    if (wordIsCriteria && word.StartsWith("'") == false)
                    {
                        compareToValues.Add(word);
                    }
                }
                foreach (string word in compareToValues)
                {
                    verifiedSelectionCriteria = verifiedSelectionCriteria.Replace(word, "'" + word + "'");
                }
            }



            return verifiedSelectionCriteria;
        }

        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }


    }//end class
}//end namespace
