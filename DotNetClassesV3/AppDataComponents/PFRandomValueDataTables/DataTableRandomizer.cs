//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using PFCollectionsObjects;
using PFRandomDataExt;
using PFRandomDataProcessor;
using PFEncryptionObjects;
using AppGlobals;
using PFMessageLogs;
using PFTimers;
using PFSystemObjects;

namespace PFRandomValueDataTables
{
    /// <summary>
    /// Contains routines to randomize the data in a DataTable.
    /// </summary>
    public class DataTableRandomizer
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private MessageLog _messageLog;

        private RandomNumber _randNumber = new RandomNumber();

        private int _defaultBatchSizeForGeneratedRandomValues = 50000;

        private string xlatkey = @"Ov$S=9hT?ONeU],`";
        private string xlativ = @",x033lcI]*O*Y/O0";

        //string _currSequentialNumber = string.Empty;
        //string _currSequentialDate = string.Empty;
        
        //private variables for properties
        private DataTable _dt = null;
        private PFList<DataTableRandomizerColumnSpec> _randomizerColumnSpecs = null;

        private int _batchSizeForGeneratedValues = 50000;
        private string _randomDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\Randomizer\Data\";
        private string _randomDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\Randomizer\Definitions\";
        private string _randomOriginalDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\Randomizer\OriginalDefinitions\";
        private string _randomSamplesSubfolder = "Samples";
        private string _randomNamesAndLocationsSubfolder = "NamesAndLocations";
        private string _randomCustomValuesSubfolder = "CustomValues";
        private string _randomNumbersSubfolder = "Numbers";
        private string _randomDatesAndTimesSubfolder = "DatesAndTimes";
        private string _randomWordsSubfolder = "Words";
        private string _randomBooleansSubfolder = "Booleans";
        private string _randomStringsSubfolder = "Strings";
        private string _randomBytesSubfolder = "Bytes";
        private string _randomDataElementsSubfolder = "DataElements";

        //key in following lists will be set to the index of the entry in the _dataTableRandomizerTypeProcessorSpecs list
        private PFKeyValueList<int, RandomNamesAndLocationsDataRequest> _randomNamesAndLocationsRequests = new PFKeyValueList<int, RandomNamesAndLocationsDataRequest>();
        private PFKeyValueList<int, RandomNumberDataRequest> _randomNumberRequests = new PFKeyValueList<int, RandomNumberDataRequest>();
        private PFKeyValueList<int, RandomWordsDataRequest> _randomWordsRequests = new PFKeyValueList<int, RandomWordsDataRequest>();
        private PFKeyValueList<int, RandomDateTimeDataRequest> _randomDateTimeRequests = new PFKeyValueList<int, RandomDateTimeDataRequest>();
        private PFKeyValueList<int, RandomBooleanDataRequest> _randomBooleanRequests = new PFKeyValueList<int, RandomBooleanDataRequest>();
        private PFKeyValueList<int, RandomCustomValuesDataRequest> _randomCustomValuesRequests = new PFKeyValueList<int, RandomCustomValuesDataRequest>();
        private PFKeyValueList<int, RandomStringDataRequest> _randomStringRequests = new PFKeyValueList<int, RandomStringDataRequest>();
        private PFKeyValueList<int, RandomBytesDataRequest> _randomBytesRequests = new PFKeyValueList<int, RandomBytesDataRequest>();
        private PFKeyValueList<int, RandomDataElementDataRequest> _randomDataElementRequests = new PFKeyValueList<int, RandomDataElementDataRequest>();

#pragma warning disable 1591
        public System.Type[] _supportedSystemTypes = new System.Type[] {
                                                                        System.Type.GetType("System.String"),
                                                                        System.Type.GetType("System.DateTime"),
                                                                        System.Type.GetType("System.Int32"),
                                                                        System.Type.GetType("System.Int64"),
                                                                        System.Type.GetType("System.Int16"),
                                                                        System.Type.GetType("System.SByte"),
                                                                        System.Type.GetType("System.Double"),
                                                                        System.Type.GetType("System.Single"),
                                                                        System.Type.GetType("System.Decimal"),
                                                                        System.Type.GetType("System.UInt32"),
                                                                        System.Type.GetType("System.UInt64"),
                                                                        System.Type.GetType("System.UInt16"),
                                                                        System.Type.GetType("System.Byte"),
                                                                        System.Type.GetType("System.Char"),
                                                                        System.Type.GetType("System.Boolean"),
                                                                        System.Type.GetType("System.Byte[]"),
                                                                        System.Type.GetType("System.Char[]"),
                                                                        System.Type.GetType("System.Guid")
                                                                       };

#pragma warning restore 1591
        private class DataTableRandomizerProcessorSpec
        {
            private enRandomDataType _randomDataType = enRandomDataType.NotSpecified;
            private int _randomNamesAndLocationsNumber = -1;                //Used to distinguish between various RandomNamesAndLocationsTypes
            private string _randomDataSource = string.Empty;                //path to file containing definition or custom data values
            private int _randomDataListIndex = -1;                          //index of the random value table in the _randomValueDataTables list
            private int _currentValueIndex = -1;                            //row from which to retrieve random value
            private string _currentSequentialNumber = string.Empty;         //Used to keep track of current number for this column when sequential numbering processing is being done for this column
            private string _currentSequentialDate = string.Empty;           //Used to keep track of current date for this column when sequential date processing is being done for this column

            /// <summary>
            /// RandomDataType Property.
            /// </summary>
            public enRandomDataType RandomDataType
            {
                get
                {
                    return _randomDataType;
                }
                set
                {
                    _randomDataType = value;
                }
            }

            /// <summary>
            /// RandomNamesAndLocationsNumber property is used to distiguish between the different RandomNamesAndLocations options (RandomNamesAndLocations, RandomNamesAndLocations_2 through RandomNamesAndLocations_5.
            /// </summary>
            public int RandomNamesAndLocationsNumber
            {
                get
                {
                    return _randomNamesAndLocationsNumber;
                }
                set
                {
                    _randomNamesAndLocationsNumber = value;
                }
            }

            /// <summary>
            /// RandomDataSource Property.
            /// </summary>
            public string RandomDataSource
            {
                get
                {
                    return _randomDataSource;
                }
                set
                {
                    _randomDataSource = value;
                }
            }

            /// <summary>
            /// RandomDataListIndex Property.
            /// </summary>
            public int RandomDataListIndex
            {
                get
                {
                    return _randomDataListIndex;
                }
                set
                {
                    _randomDataListIndex = value;
                }
            }

            /// <summary>
            /// CurrentValueIndex property.
            /// </summary>
            public int CurrentValueIndex
            {
                get
                {
                    return _currentValueIndex;
                }
                set
                {
                    _currentValueIndex = value;
                }
            }

            /// <summary>
            /// CurrentSequentialNumber Property.
            /// </summary>
            public string CurrentSequentialNumber
            {
                get
                {
                    return _currentSequentialNumber;
                }
                set
                {
                    _currentSequentialNumber = value;
                }
            }

            /// <summary>
            /// CurrentSequentialDate Property.
            /// </summary>
            public string CurrentSequentialDate
            {
                get
                {
                    return _currentSequentialDate;
                }
                set
                {
                    _currentSequentialDate = value;
                }
            }


        }//end private class

        private PFList<DataTableRandomizerProcessorSpec> _dataTableRandomizerTypeProcessorSpecs = new PFList<DataTableRandomizerProcessorSpec>();

        private PFKeyValueList<int, DataTable> _randomValueDataTables = new PFKeyValueList<int, DataTable>();


        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataTableRandomizer()
        {
            InitDataTableRandomizer();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be randomized.</param>
        /// <param name="randomizerRequests">List of specifications for which columns to randomize and values to use in the randomizing.</param>
        public DataTableRandomizer(DataTable dt, PFList<DataTableRandomizerColumnSpec> randomizerRequests)
        {
            _dt = dt;
            _randomizerColumnSpecs = randomizerRequests;
            InitDataTableRandomizer();
        }

        private void InitDataTableRandomizer()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomDataFolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomDefinitionsFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomDefinitionsFolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomOriginalDefinitionsFolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomOriginalDefinitionsFolder = configValue;
            }


            configValue = AppConfig.GetStringValueFromConfigFile("RandomNumbersSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomNumbersSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomDatesAndTimesSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomDatesAndTimesSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomBooleansSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomBooleansSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomStringsSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomStringsSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomWordsSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomWordsSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomNamesAndLocationsSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomNamesAndLocationsSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomCustomValuesSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomCustomValuesSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomBytesSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomBytesSubfolder = configValue;
            }
            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataElementsSubfolder", string.Empty);
            if (configValue != string.Empty)
            {
                _randomDataElementsSubfolder = configValue;
            }

        }


        //properties

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
        /// DataTable object containing data to be randomized.
        /// </summary>
        public DataTable DataTableToRandomize
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        }

        /// <summary>
        /// List of specifications for which columns to randomize and values to use in the randomizing.
        /// </summary>
        public PFList<DataTableRandomizerColumnSpec> RandomizerColumnSpecs
        {
            get
            {
                return _randomizerColumnSpecs;
            }
            set
            {
                _randomizerColumnSpecs = value;
            }
        }

        /// <summary>
        /// Maximum number of generated random values in list of generated values when the list the generated. 
        /// List can be generated multiple times during randomizer processing. Values on list are used once
        /// and when all values have been used, a new list is produced with the number of entries determined 
        /// by this property.
        /// </summary>
        /// <remark>This value is ignored for CustomRandomValues. Custom random values list is reused as often as needed. 
        /// It is loaded from a file and is not regenerated.</remark>
        public int BatchSizeForGeneratedValues
        {
            get
            {
                return _batchSizeForGeneratedValues;
            }
            set
            {
                _batchSizeForGeneratedValues = value;
            }
        }

        /// <summary>
        /// RandomDataFolder contains files with random data..
        /// </summary>
        public string RandomDataFolder
        {
            get
            {
                return _randomDataFolder;
            }
            set
            {
                _randomDataFolder = value;
            }
        }

        /// <summary>
        /// RandomDefinitionsFolder contains files with definitions used to generate random data.
        /// </summary>
        public string RandomDefinitionsFolder
        {
            get
            {
                return _randomDefinitionsFolder;
            }
            set
            {
                _randomDefinitionsFolder = value;
            }
        }

        /// <summary>
        /// RandomOriginalDefinitionsFolder contains definition files that shipped with application.
        /// </summary>
        /// <remarks>Files in this folder can be used as templates for building new definitions.</remarks>
        public string RandomOriginalDefinitionsFolder
        {
            get
            {
                return _randomOriginalDefinitionsFolder;
            }
            set
            {
                _randomOriginalDefinitionsFolder = value;
            }
        }

        /// <summary>
        /// RandomStandardSubfolder Property. Folder contains built-in random values and definitions files.
        /// </summary>
        public string RandomStandardSubfolder
        {
            get
            {
                return _randomSamplesSubfolder;
            }
            set
            {
                _randomSamplesSubfolder = value;
            }
        }

        /// <summary>
        /// RandomNamesAndLocationsSubfolder contains files with random name/location values and definitions..
        /// </summary>
        public string RandomNamesAndLocationsSubfolder
        {
            get
            {
                return _randomNamesAndLocationsSubfolder;
            }
            set
            {
                _randomNamesAndLocationsSubfolder = value;
            }
        }

        /// <summary>
        /// RandomCustomValuesSubfolder contains random data values or definitions created by user.
        /// </summary>
        public string RandomCustomValuesSubfolder
        {
            get
            {
                return _randomCustomValuesSubfolder;
            }
            set
            {
                _randomCustomValuesSubfolder = value;
            }
        }

        /// <summary>
        /// RandomNumbersSubfolder contains files with random numeric values or the definitions for generating random numeric values.
        /// </summary>
        public string RandomNumbersSubfolder
        {
            get
            {
                return _randomNumbersSubfolder;
            }
            set
            {
                _randomNumbersSubfolder = value;
            }
        }

        /// <summary>
        /// RandomDatesSubfolder contains files with random date values or the definitions for generating random date values.
        /// </summary>
        public string RandomDatesSubfolder
        {
            get
            {
                return _randomDatesAndTimesSubfolder;
            }
            set
            {
                _randomDatesAndTimesSubfolder = value;
            }
        }

        /// <summary>
        /// RandomWordsSubfolder contains files with random date and time values or the definitions for generating random date and time values.
        /// </summary>
        public string RandomWordsSubfolder
        {
            get
            {
                return _randomWordsSubfolder;
            }
            set
            {
                _randomWordsSubfolder = value;
            }
        }

        /// <summary>
        /// RandomBooleansSubfolder contains files with random true/false boolean values or the definitions for generating random true/false boolean values.
        /// </summary>
        public string RandomBooleansSubfolder
        {
            get
            {
                return _randomBooleansSubfolder;
            }
            set
            {
                _randomBooleansSubfolder = value;
            }
        }

        /// <summary>
        /// RandomStringsSubfolder Property.
        /// </summary>
        public string RandomStringsSubfolder
        {
            get
            {
                return _randomStringsSubfolder;
            }
            set
            {
                _randomStringsSubfolder = value;
            }
        }

        /// <summary>
        /// RandomBytesSubfolder Property.
        /// </summary>
        public string RandomBytesSubfolder
        {
            get
            {
                return _randomBytesSubfolder;
            }
            set
            {
                _randomBytesSubfolder = value;
            }
        }

        /// <summary>
        /// RandomDataElementsSubfolder Property.
        /// </summary>
        public string RandomDataElementsSubfolder
        {
            get
            {
                return _randomDataElementsSubfolder;
            }
            set
            {
                _randomDataElementsSubfolder = value;
            }
        }
        //methods

        /// <summary>
        /// Creates, if necessary, the data and definitions folders used by the randomizer.
        /// </summary>
        public void InitRandomizerFolders()
        {
            CreateFolder(_randomDataFolder);
            CreateFolder(_randomDefinitionsFolder);

            CreateFolder(Path.Combine(_randomDataFolder, _randomSamplesSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomNamesAndLocationsSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomCustomValuesSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomNumbersSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomDatesAndTimesSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomWordsSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomBooleansSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomStringsSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomBytesSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomDataElementsSubfolder));

            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomSamplesSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomNamesAndLocationsSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomCustomValuesSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomNumbersSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomDatesAndTimesSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomWordsSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomBooleansSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomStringsSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomBytesSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomDataElementsSubfolder));

            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomSamplesSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomNamesAndLocationsSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomCustomValuesSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomNumbersSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomDatesAndTimesSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomWordsSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomBooleansSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomStringsSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomBytesSubfolder));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, _randomDataElementsSubfolder));

            //create, if necessary, the samples folders
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomNamesAndLocationsSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomCustomValuesSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomNumbersSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomDatesAndTimesSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomWordsSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomBooleansSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomStringsSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomBytesSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomDataElementsSubfolder)));

            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomNamesAndLocationsSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomCustomValuesSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomNumbersSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomDatesAndTimesSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomWordsSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomBooleansSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomStringsSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomBytesSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomDataElementsSubfolder)));

            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomNamesAndLocationsSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomCustomValuesSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomNumbersSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomDatesAndTimesSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomWordsSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomBooleansSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomStringsSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomBytesSubfolder)));
            CreateFolder(Path.Combine(_randomOriginalDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomDataElementsSubfolder)));

        }

        private void CreateFolder(string dirpath)
        {
            if (Directory.Exists(dirpath) == false)
                Directory.CreateDirectory(dirpath);
        }

        /// <summary>
        /// Main routine for inserting random values into a data table.
        /// </summary>
        /// <remarks>You must set the DataTableToRandomize and RandomizerColumnSpecs properties before running this method.</remarks>
        public void RandomizeDataTableValues()
        {
            RandomizeDataTableValues(this.DataTableToRandomize, this.RandomizerColumnSpecs, _defaultBatchSizeForGeneratedRandomValues);
        }

        /// <summary>
        /// Main routine for inserting random values into a data table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be randomized.</param>
        /// <param name="colSpecs">List of specifications for which columns to randomize and values to use in the randomizing.</param>
        /// <param name="batchSize">Maximum number of random data values to generate each time a random value list is generated.</param>
        public void RandomizeDataTableValues(DataTable dt, PFList<DataTableRandomizerColumnSpec> colSpecs, int batchSize)
        {
            //RandomDataProcessor rdp = null;
            Stopwatch swQuery = new Stopwatch();

            try
            {
                swQuery.Start();

                if (dt == null || colSpecs == null)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a non-null value for following parameter(s): ");
                    if (dt == null)
                        _msg.Append("dt, ");
                    if (colSpecs == null)
                        _msg.Append("colSpecs, ");
                    char[] charsToTrim = { ',', ' ' };
                    WriteMessageToLog(_msg.ToString().TrimEnd(charsToTrim) + ".");
                    throw new System.Exception(_msg.ToString().TrimEnd(charsToTrim) + ".");
                }

                if (dt.Rows.Count < 1 || colSpecs.Count == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify non-empty collections for following parameter(s): ");
                    if (dt.Rows.Count < 1)
                        _msg.Append("dt, ");
                    if (colSpecs.Count == 0)
                        _msg.Append("colSpecs, ");
                    char[] charsToTrim = { ',', ' ' };
                    WriteMessageToLog(_msg.ToString().TrimEnd(charsToTrim) + ".");
                    throw new System.Exception(_msg.ToString().TrimEnd(charsToTrim) + ".");
                }

                this.DataTableToRandomize = dt;
                this.RandomizerColumnSpecs = colSpecs;
                this.BatchSizeForGeneratedValues = batchSize;

                if (colSpecs == null || dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both the data table to be randomized and the list of column randomizer specifications in order to run OLD_RandomizeDataTableValues method.");
                    throw new System.Exception(_msg.ToString());
                }

                if (dt.Rows.Count < 1 || colSpecs.Count == 0)
                {
                    //no data to randomize;
                    return;
                }


                //Get list of unique randomizer types and their sources to use for this run
                //Update colSpecs to point to unique type/source entry
                _dataTableRandomizerTypeProcessorSpecs = GetDataTableRandomizerTypeProcessorSpecs(this.RandomizerColumnSpecs);
                if (_dataTableRandomizerTypeProcessorSpecs.Count > 0)
                {
                    //Load definition files for the randomizer types specified in the RandomizerColumnSpecs
                    LoadRandomizerDefinitions(_dataTableRandomizerTypeProcessorSpecs);
                    //Load initial lists for each type/source pair
                    LoadInitialRandomDataLists(_dataTableRandomizerTypeProcessorSpecs);
                    //Sync colSpecs random field name with columnIndex in random values table
                    GetRandomFieldNameColumnIndexes(this.RandomizerColumnSpecs, _dataTableRandomizerTypeProcessorSpecs);
                }
                //Process each row in the data table
                ProcessDataTableRows(dt, colSpecs, batchSize);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                swQuery.Stop();

                _msg.Length = 0;
                _msg.Append("Randomize Data Table Values Elapsed Time: ");
                _msg.Append(swQuery.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());
            }
                 
        
        }


        private PFList<DataTableRandomizerProcessorSpec> GetDataTableRandomizerTypeProcessorSpecs(PFList<DataTableRandomizerColumnSpec> RandomizerColumnSpecs)
        {
            PFList<DataTableRandomizerProcessorSpec> dataTableRandomizerProcessorSpecs = new PFList<DataTableRandomizerProcessorSpec>();

            foreach (DataTableRandomizerColumnSpec colSpec in RandomizerColumnSpecs)
            {
                if (colSpec.RandomDataType != enRandomDataType.NotSpecified)
                {
                    if (ColSpecAlreadyProcessed(colSpec, dataTableRandomizerProcessorSpecs) == false)
                    {
                        DataTableRandomizerProcessorSpec processorSpec = new DataTableRandomizerProcessorSpec();
                        processorSpec.RandomDataType = colSpec.RandomDataType;
                        processorSpec.RandomDataSource = colSpec.RandomDataSource;
                        processorSpec.RandomNamesAndLocationsNumber = colSpec.RandomNamesAndLocationsNumber;
                        dataTableRandomizerProcessorSpecs.Add(processorSpec);
                        colSpec.RandomDataTypeProcessorIndex = dataTableRandomizerProcessorSpecs.Count - 1;
                    }
                }
            }


            return dataTableRandomizerProcessorSpecs;
        }

        private void LoadRandomizerDefinitions(PFList<DataTableRandomizerProcessorSpec>  dataTableRandomizerProcessorSpecs)
        {
            string folderPath = string.Empty;
            string filePath = string.Empty;

            for (int i = 0; i < dataTableRandomizerProcessorSpecs.Count; i++)
            { 
                DataTableRandomizerProcessorSpec processorSpec = dataTableRandomizerProcessorSpecs[i];

                switch (processorSpec.RandomDataType)
                {
                    case enRandomDataType.RandomNamesAndLocations:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomNamesAndLocationsSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomNamesAndLocationsDataRequest drNamesAndLocations = RandomNamesAndLocationsDataRequest.LoadFromXmlFile(filePath);
                        PFStringEncryptor enc = new PFStringEncryptor(pfEncryptionAlgorithm.AES);
                        enc.Key = xlatkey;
                        enc.IV = xlativ;
                        string decryptedString = enc.Decrypt(drNamesAndLocations.DatabasePassword);
                        drNamesAndLocations.DatabasePassword = decryptedString;
                        _randomNamesAndLocationsRequests.Add(new stKeyValuePair<int,RandomNamesAndLocationsDataRequest>(i, drNamesAndLocations));
                        break;
                    case enRandomDataType.RandomNumbers:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomNumbersSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomNumberDataRequest drNumber = RandomNumberDataRequest.LoadFromXmlFile(filePath);
                        _randomNumberRequests.Add(new stKeyValuePair<int, RandomNumberDataRequest>(i, drNumber));
                        break;
                    case enRandomDataType.RandomWords:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomWordsSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomWordsDataRequest drWords = RandomWordsDataRequest.LoadFromXmlFile(filePath);
                        _randomWordsRequests.Add(new stKeyValuePair<int, RandomWordsDataRequest>(i, drWords));
                        break;
                    case enRandomDataType.RandomDatesAndTimes:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomDatesSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomDateTimeDataRequest drDates = RandomDateTimeDataRequest.LoadFromXmlFile(filePath);
                        _randomDateTimeRequests.Add(new stKeyValuePair<int, RandomDateTimeDataRequest>(i, drDates));
                        break;
                    case enRandomDataType.RandomBooleans:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomBooleansSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomBooleanDataRequest drBoolean = RandomBooleanDataRequest.LoadFromXmlFile(filePath);
                        _randomBooleanRequests.Add(new stKeyValuePair<int, RandomBooleanDataRequest>(i, drBoolean));
                        break;
                    case enRandomDataType.CustomRandomValues:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomCustomValuesSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomCustomValuesDataRequest drCustomValues = RandomCustomValuesDataRequest.LoadFromXmlFile(filePath);
                        _randomCustomValuesRequests.Add(new stKeyValuePair<int, RandomCustomValuesDataRequest>(i, drCustomValues));
                        break;
                    case enRandomDataType.RandomStrings:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomStringsSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomStringDataRequest drString = RandomStringDataRequest.LoadFromXmlFile(filePath);
                        _randomStringRequests.Add(new stKeyValuePair<int, RandomStringDataRequest>(i, drString));
                        break;
                    case enRandomDataType.RandomBytes:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomBytesSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomBytesDataRequest drBytes = RandomBytesDataRequest.LoadFromXmlFile(filePath);
                        _randomBytesRequests.Add(new stKeyValuePair<int, RandomBytesDataRequest>(i, drBytes));
                        break;
                    case enRandomDataType.RandomDataElements:
                        folderPath = Path.Combine(this.RandomDefinitionsFolder, this.RandomDataElementsSubfolder);
                        filePath = Path.Combine(folderPath, processorSpec.RandomDataSource);
                        RandomDataElementDataRequest drDataElement = RandomDataElementDataRequest.LoadFromXmlFile(filePath);
                        _randomDataElementRequests.Add(new stKeyValuePair<int, RandomDataElementDataRequest>(i, drDataElement));
                        break;
                    default:
                        break;
                }//end for loop
            }//end switch

        }//end method

        private bool ColSpecAlreadyProcessed(DataTableRandomizerColumnSpec colSpec, PFList<DataTableRandomizerProcessorSpec> dataTableRandomizerProcessorSpecs)
        {
            bool alreadyProcessed = false;

            if (dataTableRandomizerProcessorSpecs.Count > 0)
            {
                for (int i = 0; i < dataTableRandomizerProcessorSpecs.Count; i++)
                {
                    DataTableRandomizerProcessorSpec processorSpec = dataTableRandomizerProcessorSpecs[i];
                    if (processorSpec.RandomDataType == colSpec.RandomDataType
                        && processorSpec.RandomNamesAndLocationsNumber == colSpec.RandomNamesAndLocationsNumber
                        && processorSpec.RandomDataSource == colSpec.RandomDataSource)
                    {
                        alreadyProcessed = true;
                        colSpec.RandomDataTypeProcessorIndex = i;
                        break;
                    }
                }
            }

            return alreadyProcessed;
        }

        private void LoadInitialRandomDataLists(PFList<DataTableRandomizerProcessorSpec> _dataTableRandomizerTypeProcessorSpecs)
        {
            Stopwatch swGetRandom = new Stopwatch();

            swGetRandom.Start();

            for (int i = 0; i < _dataTableRandomizerTypeProcessorSpecs.Count; i++)
            {
                DataTableRandomizerProcessorSpec dtrSpec = _dataTableRandomizerTypeProcessorSpecs[i];

                switch (dtrSpec.RandomDataType)
                {
                    case enRandomDataType.RandomNamesAndLocations:
                        LoadRandomNamesAndLocationsList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomNumbers:
                        LoadRandomNumbersList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomWords:
                        LoadRandomWordsList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomDatesAndTimes:
                        LoadRandomDatesAndTimesList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomBooleans:
                        LoadRandomBooleansList(i, dtrSpec);
                        break;
                    case enRandomDataType.CustomRandomValues:
                        LoadCustomRandomValuesList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomStrings:
                        LoadRandomStringsList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomBytes:
                        LoadRandomBytesList(i, dtrSpec);
                        break;
                    case enRandomDataType.RandomDataElements:
                        LoadRandomDataElementsList(i, dtrSpec);
                        break;
                    default:
                        break;
                }

            }

            swGetRandom.Stop();

            _msg.Length = 0;
            _msg.Append("LoadInitialRandomDataLists Elapsed Time: ");
            _msg.Append(swGetRandom.FormattedElapsedTime);
            WriteMessageToLog(_msg.ToString());
        }

        private void LoadRandomNamesAndLocationsList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomNamesAndLocationsDataRequest> kvpDr = default(stKeyValuePair<int, RandomNamesAndLocationsDataRequest>);
            kvpDr = _randomNamesAndLocationsRequests.Find(i.ToString());
            RandomNamesAndLocationsDataRequest dr = kvpDr.Value;
            RandomNamesAndLocationsDataTable rndt = new RandomNamesAndLocationsDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            stKeyValuePair<int, DataTable> kvpDtDefault = default(stKeyValuePair<int, DataTable>);
            if (kvpDt.Key != kvpDtDefault.Key || kvpDt.Value != kvpDtDefault.Value)
            {
                //_randomValueDataTables.Remove(kvpDt.Key.ToString());
                _randomValueDataTables[i].Value.Clear();
            }
            DataTable dt = rndt.CreateRandomNamesAndLocationsDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }

            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }
        }

        private void LoadRandomNumbersList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomNumberDataRequest> kvpDr = default(stKeyValuePair<int, RandomNumberDataRequest>);
            kvpDr = _randomNumberRequests.Find(i.ToString());
            RandomNumberDataRequest dr = kvpDr.Value;
            RandomNumberDataTable rndt = new RandomNumberDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                //_randomValueDataTables.Remove(kvpDt.Key.ToString());
                //kvpDt.Value.Clear();
                _randomValueDataTables[i].Value.Clear();
            }
            if (dr.OutputSequentialNumbers)
            {
                if (dtrSpec.CurrentSequentialNumber != string.Empty)
                {
                    //calculate an updated start number for the number sequence
                    double currSeqNum = Convert.ToDouble(dtrSpec.CurrentSequentialNumber);
                    double seqIncrement = Convert.ToDouble(dr.IncrementForSequentialValue);
                    double newStartNum = currSeqNum + seqIncrement;
                    dr.StartSequentialValue = newStartNum.ToString();
                }
            }
            DataTable dt = rndt.CreateRandomNumberDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }
        }

        private void LoadRandomWordsList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomWordsDataRequest> kvpDr = default(stKeyValuePair<int, RandomWordsDataRequest>);
            kvpDr = _randomWordsRequests.Find(i.ToString());
            RandomWordsDataRequest dr = kvpDr.Value;
            RandomWordsDataTable rndt = new RandomWordsDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                _randomValueDataTables[i].Value.Clear();
            }
            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }

        }

        private void LoadRandomDatesAndTimesList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomDateTimeDataRequest> kvpDr = default(stKeyValuePair<int, RandomDateTimeDataRequest>);
            kvpDr = _randomDateTimeRequests.Find(i.ToString());
            RandomDateTimeDataRequest dr = kvpDr.Value;
            RandomDateTimeDataTable rndt = new RandomDateTimeDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                //_randomValueDataTables.Remove(kvpDt.Key.ToString());
                _randomValueDataTables[i].Value.Clear();
            }

            if (dr.OutputSequentialDates)
            {
                if (dtrSpec.CurrentSequentialDate != string.Empty)
                {
                    //calculate an updated start number for the number sequence
                    DateTime currSeqDate = Convert.ToDateTime(dtrSpec.CurrentSequentialDate);
                    //double seqIncrement = Convert.ToDouble(dr.IncrementSize);
                    enRandomIncrementType incrementType = dr.YearsIncrement ? enRandomIncrementType.enYears : dr.MonthsIncrement ? enRandomIncrementType.enMinutes : enRandomIncrementType.enDays;
                    int sizeOfIncrement = AppTextGlobals.ConvertStringToInt(dr.IncrementSize, 1);
                    DateTime currSeqDateAtMidnight = new DateTime(currSeqDate.Year, currSeqDate.Month, currSeqDate.Day, 0, 0, 0);
                    DateTime newStartDate = rndt.IncrementDateTime(incrementType, sizeOfIncrement, currSeqDateAtMidnight, Convert.ToDateTime(dr.StartSequentialDate));
                    dr.StartSequentialDate = newStartDate.ToString();
                }
            }

            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }

        }

        private void LoadRandomBooleansList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomBooleanDataRequest> kvpDr = default(stKeyValuePair<int, RandomBooleanDataRequest>);
            kvpDr = _randomBooleanRequests.Find(i.ToString());
            RandomBooleanDataRequest dr = kvpDr.Value;
            RandomBooleanDataTable rndt = new RandomBooleanDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                _randomValueDataTables[i].Value.Clear();
            }
            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
            dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }
        }

        private void LoadCustomRandomValuesList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomCustomValuesDataRequest> kvpDr = default(stKeyValuePair<int, RandomCustomValuesDataRequest>);
            kvpDr = _randomCustomValuesRequests.Find(i.ToString());
            RandomCustomValuesDataRequest dr = kvpDr.Value;
            RandomCustomValuesDataTable rndt = new RandomCustomValuesDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                _randomValueDataTables.Remove(kvpDt.Key.ToString());
            }
            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }
        }

        private void LoadRandomStringsList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomStringDataRequest> kvpDr = default(stKeyValuePair<int, RandomStringDataRequest>);
            kvpDr = _randomStringRequests.Find(i.ToString());
            RandomStringDataRequest dr = kvpDr.Value;
            RandomStringDataTable rndt = new RandomStringDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                _randomValueDataTables[i].Value.Clear();
            }
            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }

        }

        private void LoadRandomBytesList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomBytesDataRequest> kvpDr = default(stKeyValuePair<int, RandomBytesDataRequest>);
            kvpDr = _randomBytesRequests.Find(i.ToString());
            RandomBytesDataRequest dr = kvpDr.Value;
            RandomBytesDataTable rndt = new RandomBytesDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                _randomValueDataTables[i].Value.Clear();
            }
            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }

        }

        private void LoadRandomDataElementsList(int i, DataTableRandomizerProcessorSpec dtrSpec)
        {
            stKeyValuePair<int, RandomDataElementDataRequest> kvpDr = default(stKeyValuePair<int, RandomDataElementDataRequest>);
            kvpDr = _randomDataElementRequests.Find(i.ToString());
            RandomDataElementDataRequest dr = kvpDr.Value;
            RandomDataElementDataTable rndt = new RandomDataElementDataTable();
            stKeyValuePair<int, DataTable> kvpDt = _randomValueDataTables.Find(i.ToString());
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                _randomValueDataTables[i].Value.Clear();
            }
            DataTable dt = rndt.CreateRandomDataTable(this.BatchSizeForGeneratedValues, dr);
            if (object.Equals(kvpDt, default(stKeyValuePair<int, DataTable>)) == false)
            {
                kvpDt.Value = dt.Copy();
                _randomValueDataTables[i] = kvpDt;
            }
            else
            {
                _randomValueDataTables.Add(new stKeyValuePair<int, DataTable>(i, dt));
                dtrSpec.RandomDataListIndex = _randomValueDataTables.Count - 1;
            }
            if (dt.Rows.Count > 0)
            {
                dtrSpec.CurrentValueIndex = 0;
            }
            else
            {
                dtrSpec.CurrentValueIndex = -1;
            }

        }
        
        
        private void GetRandomFieldNameColumnIndexes(PFList<DataTableRandomizerColumnSpec> colSpecs, PFList<DataTableRandomizerProcessorSpec> dataTableRandomizerProcessorSpecs)
        {
            for (int i = 0; i < colSpecs.Count; i++)
            {
                if (colSpecs[i].RandomDataType != enRandomDataType.NotSpecified)
                {
                    int randomizerProcessorInx = colSpecs[i].RandomDataTypeProcessorIndex;
                    stKeyValuePair<int, DataTable> kvp = _randomValueDataTables.Find(randomizerProcessorInx.ToString());
                    DataTable dt = kvp.Value;
                    if (colSpecs[i].RandomDataType == enRandomDataType.RandomNamesAndLocations)
                    {
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (dt.Columns[c].ColumnName == colSpecs[i].RandomDataFieldName.Replace(" ",string.Empty))       //no embedded spaces in a column name
                            {
                                colSpecs[i].RandomDataFieldColumnIndex = c;
                                break;
                            }
                        }
                    }
                    else
                    {
                        colSpecs[i].RandomDataFieldColumnIndex = 0;
                    }
                }//end if
            }//end for
        }

        /// <summary>
        /// Processes each row in the DataTable and randomizes the column values as specified by the column specifications.
        /// </summary>
        /// <param name="dt">DataTable containing data to be randomized.</param>
        /// <param name="colSpecs">Set of specifications that define whether or not a column is to be randomized. If it is to be randomized, the specification specifies where to obtain the appropriate random values.</param>
        /// <param name="batchSize">Maximum number of random data values to generate each time a random value list is generated.</param>
        public void ProcessDataTableRows(DataTable dt, PFList<DataTableRandomizerColumnSpec> colSpecs, int batchSize)
        {
            //_dataTableRandomizerTypeProcessorSpecs
            //_randomValueDataTables 
            DataRow dr = null;
            DataColumn dc = null;
            DataTableRandomizerProcessorSpec dtrSpec = null;
            DataTable randomValueDataTable = null;
            string val = string.Empty;
            double dblVal = 0.0;
            DateTime dateTimeValue = DateTime.MinValue;
            byte[] byteArrayVal = {(byte)0};
            char[] charArrayVal = { '?' };
            stKeyValuePair<int, DataTable> kvp = default(stKeyValuePair<int, DataTable>);

            /*
             *  Loop thru rows in data table
             *      Loop thru columns in data table row
             *          if colSpec for the column calls for randomization
             *              Get randomizer for the column
             *              Get current randomRow for the randomizer for that column
            *               if randomizing Name and Location
             *                  get column index for column containing randomized field
             *                  get randomized value
             *              else **randomizer table on contains a RandomValue column
             *                  get randomized value from col 1
             *              end if randomizing Name and Location
             *          set data table column to randomized value
             *          end if colSpec calls for randomization
             *      end Loop thru columns in data table
             *      Reset current row index for all active randomizer specs
             *  end loop thru rows in data table
            */

            bool[] dataTableColumnDataTypeIsSupported = new bool[dt.Columns.Count];
            int[] columnErrors = new int[dt.Columns.Count];
            string[] columnErrorMessages = new string[dt.Columns.Count];
            int totalErrors = 0;
            int saveTotalErrors = 0;
            int numRowsWithErrors = 0;
            int maxRowsWithErrors = 20;

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                columnErrors[c] = 0;
                columnErrorMessages[c] = string.Empty;
                dt.Columns[c].AllowDBNull = true;  //force allowdbnulls to true to dbnull.value can be applied in case of errors
                dt.Columns[c].ReadOnly = false;
                dataTableColumnDataTypeIsSupported[c] = DataTypeIsSupportedByRandomizer(dt.Columns[c].DataType);
                if (colSpecs[c].RandomDataType != enRandomDataType.NotSpecified && colSpecs[c].RandomDataTypeProcessorIndex != -1 && dataTableColumnDataTypeIsSupported[c] == true)
                {

                    if (colSpecs[c].RandomDataType == enRandomDataType.RandomNumbers)
                    {
                        stKeyValuePair<int, RandomNumberDataRequest> kvpDr = default(stKeyValuePair<int, RandomNumberDataRequest>);
                        kvpDr = _randomNumberRequests.Find(colSpecs[c].RandomDataTypeProcessorIndex.ToString());
                        RandomNumberDataRequest numDr = kvpDr.Value;
                        if (numDr.OutputOffsetFromCurrentNumber)
                           colSpecs[c].IsOffsetRandomValue = true;
                        else
                            colSpecs[c].IsOffsetRandomValue = false;
                        if (numDr.OutputSequentialNumbers)
                            colSpecs[c].IsSequentialNumber = true;
                        else
                            colSpecs[c].IsSequentialNumber = false;
                    }
                    else if (colSpecs[c].RandomDataType == enRandomDataType.RandomDatesAndTimes)
                    {
                        stKeyValuePair<int, RandomDateTimeDataRequest> kvpDr = default(stKeyValuePair<int, RandomDateTimeDataRequest>);
                        kvpDr = _randomDateTimeRequests.Find(colSpecs[c].RandomDataTypeProcessorIndex.ToString());
                        RandomDateTimeDataRequest dtDr = kvpDr.Value;
                        if (dtDr.OffsetFromDataTableDate)
                            colSpecs[c].IsOffsetRandomValue = true;
                        else
                            colSpecs[c].IsOffsetRandomValue = false;
                        if (dtDr.OutputSequentialDates)
                            colSpecs[c].IsSequentialDate = true;
                        else
                            colSpecs[c].IsSequentialDate = false;
                        if (dtDr.ConvertGeneratedValueToInteger)
                        {
                            colSpecs[c].ConvertRandomDateTimeToInteger = true;
                            if (dtDr.ConvertDateTo32BitInteger)
                            {
                                colSpecs[c].ConvertRandomDateToInt32 = true;
                            }
                            else
                            {
                                colSpecs[c].ConvertRandomDateToInt32 = false;
                            }
                            if (dtDr.ConvertTimeTo32BitInteger)
                            {
                                colSpecs[c].ConvertRandomTimeToInt32 = true;
                            }
                            else
                            {
                                colSpecs[c].ConvertRandomTimeToInt32 = false;
                            }
                            if (dtDr.ConvertDateTimeTo64BitInteger)
                            {
                                colSpecs[c].ConvertRandomDateTimeToInt64 = true;
                            }
                            else
                            {
                                colSpecs[c].ConvertRandomDateTimeToInt64 = false;
                            }
                        }
                        else
                        {
                            colSpecs[c].ConvertRandomDateTimeToInteger = false;
                            colSpecs[c].ConvertRandomDateToInt32 = false;
                            colSpecs[c].ConvertRandomTimeToInt32 = false;
                            colSpecs[c].ConvertRandomDateTimeToInt64 = false;
                        }
                    }
                    else if (colSpecs[c].RandomDataType == enRandomDataType.RandomBytes)
                    {
                        stKeyValuePair<int, RandomBytesDataRequest> kvpDr = default(stKeyValuePair<int, RandomBytesDataRequest>);
                        kvpDr = _randomBytesRequests.Find(colSpecs[c].RandomDataTypeProcessorIndex.ToString());
                        RandomBytesDataRequest byteDr = kvpDr.Value;
                        if (byteDr.OutputByte && byteDr.OutputArrayOfValues)
                            colSpecs[c].IsByteArray = true;
                        else if (byteDr.OutputChar && byteDr.OutputArrayOfValues)
                            colSpecs[c].IsCharArray = true;
                        else
                        {
                            colSpecs[c].IsByteArray = false;
                            colSpecs[c].IsCharArray = false;
                        }

                    }
                    else
                    {
                        ;
                    }
                }//end if
            }//end for

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                dr = dt.Rows[r];

                saveTotalErrors = totalErrors;

                for (int c = 0; c < dt.Columns.Count; c++)
                {

                    dc = dt.Columns[c];
                    if (colSpecs[c].RandomDataType != enRandomDataType.NotSpecified && colSpecs[c].RandomDataTypeProcessorIndex != -1 && dataTableColumnDataTypeIsSupported[c] == true)
                    {
                        try
                        {
                            val = string.Empty;
                            //Get the NamesAndLocations randomizer processor for the column
                            dtrSpec = _dataTableRandomizerTypeProcessorSpecs[colSpecs[c].RandomDataTypeProcessorIndex];
                            kvp = _randomValueDataTables.Find(colSpecs[c].RandomDataTypeProcessorIndex.ToString());
                            randomValueDataTable = kvp.Value;
                            if (randomValueDataTable != null)
                            {
                                //get name of column for this pass
                                if (colSpecs[c].RandomDataType == enRandomDataType.RandomNamesAndLocations)
                                {
                                    //randomizer pro9duces multiple random column values (FirstName, LastName, BirthDate, etc.)
                                    val = randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][colSpecs[c].RandomDataFieldColumnIndex].ToString();

                                }//end if NamesAndLocations
                                else if (colSpecs[c].RandomDataType == enRandomDataType.RandomNumbers && colSpecs[c].IsOffsetRandomValue)
                                {
                                    if (dtrSpec.CurrentValueIndex != -1)
                                    {
                                        //get the offset
                                        double offset = (double)randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][0];
                                        double baseValue = (double)randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][1];
                                        double offsetRatio = 0.0;
                                        double colValue = 0.0;
                                        object oColValue = dt.Rows[r][c];
                                        double adjustedOffset = 0.0;
                                        if (PFSystemTypeInfo.DataTypeIsNumeric(oColValue.GetType()))
                                        {
                                            colValue = Convert.ToDouble(oColValue);
                                            offsetRatio = colValue / baseValue;
                                            adjustedOffset = offset * offsetRatio;
                                            dblVal = colValue + adjustedOffset;
                                            val = dblVal.ToString();
                                        }
                                        else
                                        {
                                            dblVal = 0.0;
                                            val = dblVal.ToString();
                                        }
                                    }
                                    else
                                    {
                                        dblVal = 0.0;
                                        val = dblVal.ToString();
                                    }
                                }
                                else if (colSpecs[c].RandomDataType == enRandomDataType.RandomDatesAndTimes && colSpecs[c].IsOffsetRandomValue)
                                {
                                    if (dtrSpec.CurrentValueIndex != -1)
                                    {
                                        int offset = (int)randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][0];
                                        object oColValue = dt.Rows[r][c];
                                        DateTime colValue = DateTime.MinValue;
                                        if (PFSystemTypeInfo.DataTypeIsDateTime(oColValue.GetType()) && colSpecs[c].ConvertRandomDateTimeToInteger==false)
                                        {
                                            colValue = Convert.ToDateTime(oColValue);
                                            dateTimeValue = colValue.AddDays((double)offset);
                                            val = dateTimeValue.ToString("G");
                                        }
                                        else if (PFSystemTypeInfo.DataTypeIs32BitInteger(oColValue.GetType()))
                                        {
                                            if (colSpecs[c].ConvertRandomDateToInt32)
                                            {
                                                colValue = ConvertInt32ToDate(oColValue);
                                                dateTimeValue = colValue.AddDays((double)offset);
                                                val = dateTimeValue.ToString("yyyyMMdd");
                                            }
                                            else if (colSpecs[c].ConvertRandomTimeToInt32)
                                            {
                                                colValue = ConvertInt32ToTime(oColValue);
                                                dateTimeValue = colValue.AddDays((double)offset);
                                                val = dateTimeValue.ToString("HHmmss");
                                            }
                                            else
                                            {
                                                totalErrors++;
                                                columnErrors[c]++;
                                                columnErrorMessages[c] = "Datatype of query table 32-bit integer column does not match type of randomizer value.";
                                                dateTimeValue = DateTime.MinValue;
                                                val = null;
                                            }
                                        }
                                        else if (PFSystemTypeInfo.DataTypeIs64BitInteger(oColValue.GetType()))
                                        {
                                            if (colSpecs[c].ConvertRandomDateTimeToInt64)
                                            {
                                                colValue = ConvertInt64ToDateTime(oColValue);
                                                dateTimeValue = colValue.AddDays((double)offset);
                                                val = dateTimeValue.ToString("yyyyMMddHHmmss");
                                            }
                                            else
                                            {
                                                totalErrors++;
                                                columnErrors[c]++;
                                                columnErrorMessages[c] = "Datatype of query table 64-bit integer column does not match type randomizer value.";
                                                dateTimeValue = DateTime.MinValue;
                                                val = null;
                                            }
                                        }
                                        else
                                        {
                                            totalErrors++;
                                            columnErrors[c]++;
                                            columnErrorMessages[c] = "No date/time offset randomizer value available.";
                                            dateTimeValue = DateTime.MinValue;
                                            //val = dateTimeValue.ToString("G");
                                            val = null;
                                        }
                                    }
                                    else
                                    {
                                        totalErrors++;
                                        columnErrors[c]++;
                                        columnErrorMessages[c] = "Unable to process date/time offset randomizer request.";
                                        dateTimeValue = DateTime.MinValue;
                                        val = dateTimeValue.ToString("G");
                                    }
                                }
                                else if (colSpecs[c].RandomDataType == enRandomDataType.CustomRandomValues)
                                {
                                    if (randomValueDataTable.Rows.Count > 0)
                                    {
                                        int minRow = 0;
                                        int maxRow = (int)randomValueDataTable.Rows.Count - 1;
                                        int randRow = _randNumber.GenerateRandomInt(minRow, maxRow);
                                        val = randomValueDataTable.Rows[randRow][0].ToString();
                                    }
                                    else
                                    {
                                        val = string.Empty;
                                    }
                                }
                                else if (colSpecs[c].RandomDataType == enRandomDataType.RandomBytes)
                                {
                                    if (randomValueDataTable.Rows.Count > 0)
                                    {
                                        if (colSpecs[c].IsByteArray)
                                        {
                                            byteArrayVal = (byte[])randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][0];
                                            charArrayVal = new char[] { '?' };
                                            val = System.Text.Encoding.ASCII.GetString(byteArrayVal); ;
                                        }
                                        else if (colSpecs[c].IsCharArray)
                                        {
                                            byteArrayVal = new byte[] { (byte)0 };
                                            charArrayVal = (char[])randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][0];
                                            val = new String(charArrayVal);
                                        }
                                        else
                                        {
                                            byteArrayVal = new byte[] { (byte)0 };
                                            charArrayVal = new char[] { '?' };
                                            val = randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][0].ToString();
                                        }
                                    }
                                    else
                                    {
                                        byteArrayVal = new byte[] {(byte)0};
                                        charArrayVal = new char[] {'?'};
                                        val = string.Empty;
                                    }
                                }
                                else
                                {
                                    //one of the randomizers that have only one value per row
                                    if (dtrSpec.CurrentValueIndex != -1)
                                    {
                                        val = randomValueDataTable.Rows[dtrSpec.CurrentValueIndex][0].ToString();
                                        if (colSpecs[c].IsSequentialNumber)
                                            dtrSpec.CurrentSequentialNumber = val;
                                        if (colSpecs[c].IsSequentialDate)
                                            dtrSpec.CurrentSequentialDate = val;
                                    }
                                    else
                                    {
                                        val = string.Empty;
                                    }
                                }//end else

                                //assign the random value to the query data table column
                                if (dt.Columns[c].DataType == Type.GetType("System.String"))
                                {
                                    if (val.Length > dt.Columns[c].MaxLength)
                                        val = val.Substring(0, dt.Columns[c].MaxLength);
                                    dt.Rows[r][c] = val;
                                }
                                else
                                {
                                    if (val != null)
                                    {
                                        if (colSpecs[c].RandomDataType == enRandomDataType.RandomNumbers && colSpecs[c].IsOffsetRandomValue)
                                        {
                                            dt.Rows[r][c] = Convert.ChangeType(dblVal, dt.Columns[c].DataType);
                                        }
                                        else if (colSpecs[c].RandomDataType == enRandomDataType.RandomDatesAndTimes && colSpecs[c].IsOffsetRandomValue && colSpecs[c].ConvertRandomDateTimeToInteger == false)
                                        {
                                            if (dateTimeValue != DateTime.MinValue)
                                            {
                                                dt.Rows[r][c] = dateTimeValue;
                                            }
                                            else
                                            {
                                                dt.Rows[r][c] = DBNull.Value;
                                            }
                                        }
                                        else if (colSpecs[c].RandomDataType == enRandomDataType.RandomBytes && colSpecs[c].IsByteArray)
                                        {
                                            if (dt.Columns[c].DataType == Type.GetType("System.Char[]"))
                                            {
                                                char[] chars = new char[byteArrayVal.Length / sizeof(char)];
                                                System.Buffer.BlockCopy(byteArrayVal, 0, chars, 0, byteArrayVal.Length);
                                                dt.Rows[r][c] = chars;
                                            }
                                            else
                                            {
                                                dt.Rows[r][c] = byteArrayVal;
                                            }
                                        }
                                        else if (colSpecs[c].RandomDataType == enRandomDataType.RandomBytes && colSpecs[c].IsCharArray)
                                        {
                                            if (dt.Columns[c].DataType == Type.GetType("System.Byte[]"))
                                            {
                                                string str = new String(charArrayVal);
                                                byte[] bytes = new byte[str.Length * sizeof(char)];
                                                System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
                                                dt.Rows[r][c] = bytes;
                                            }
                                            else
                                            {
                                                dt.Rows[r][c] = charArrayVal;
                                            }
                                        }
                                        else if (colSpecs[c].RandomDataType == enRandomDataType.RandomDataElements && dt.Columns[c].DataType == Type.GetType("System.Guid"))
                                        {
                                            dt.Rows[r][c] = new Guid(val);
                                        }
                                        else
                                        {
                                            dt.Rows[r][c] = Convert.ChangeType(val, dt.Columns[c].DataType);
                                        }
                                    }
                                    else
                                        dt.Rows[r][c] = DBNull.Value;
                                }
                            }
                            else
                            {
                                _msg.Length = 0;
                                _msg.Append("Null DataTable occurred in ProcessDataTableRows:\r\n");
                                _msg.Append("Row No: ");
                                _msg.Append(r.ToString());
                                _msg.Append(" Col No: ");
                                _msg.Append(c.ToString());
                                WriteMessageToLog(_msg.ToString());
                                throw new System.Exception(_msg.ToString());
                            }
                        }
                        catch (System.Exception ex)
                        {
                            //_msg.Length = 0;
                            //_msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                            //WriteMessageToLog(_msg.ToString());
                            //throw new System.Exception(_msg.ToString());

                            //if(dt.Columns[c].AllowDBNull)
                            //{
                            //    dt.Rows[r][c] = DBNull.Value;
                            //}
                            //else
                            //{
                            //    dt.Rows[r][c] = dt.Columns[c].DefaultValue;
                            //}

                            dt.Rows[r][c] = DBNull.Value;

                            totalErrors++;
                            columnErrors[c]++;
                            columnErrorMessages[c] = AppMessages.FormatErrorMessage(ex);
                        
                        }
                        finally
                        {
                            ;
                        }

                    }//end if != Not Specified
                }//end for dt.Columns

                //move row index pointers to next random value rows
                RefreshCurrentValueIndexes();

                if (totalErrors != saveTotalErrors)
                {
                    numRowsWithErrors++;
                    if (numRowsWithErrors > maxRowsWithErrors)
                    {
                        break;
                    }
                }
            }//end for dt.Rows

            if (totalErrors > 0)
            {
                _msg.Length = 0;
                if (numRowsWithErrors > maxRowsWithErrors)
                    _msg.Append("Maximum number of rows with errors exceeded during randomizing of data.");
                else
                    _msg.Append("Errors occurred during randomizing of data.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Number of rows with errors: " + numRowsWithErrors.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                for (int ce = 0; ce < columnErrors.Length; ce++)
                {
                    if (columnErrors[ce] > 0)
                    {
                        _msg.Append("Column " + ce.ToString() + " error count: " + columnErrors[ce].ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        if (dt != null)
                        {
                            if (dt.Columns.Count >= ce)
                            {
                                _msg.Append("Column Name: " + dt.Columns[ce].ColumnName);
                                _msg.Append("  Data Type for column: ");
                                _msg.Append(dt.Columns[ce].DataType.FullName);
                                _msg.Append(Environment.NewLine);
                                _msg.Append("Randomizer value data type: ");
                                _msg.Append(colSpecs[ce].RandomDataType.ToString());
                                _msg.Append(Environment.NewLine);
                                if (colSpecs[ce].RandomDataType == enRandomDataType.RandomDatesAndTimes)
                                {
                                    if (colSpecs[ce].ConvertRandomDateTimeToInteger)
                                        _msg.Append("Random Data Type: Integer");
                                    else
                                        _msg.Append("Random Data Type: DateTime");
                                    _msg.Append(Environment.NewLine);
                                }
                            }
                        }
                        _msg.Append("Last Error Message:");
                        _msg.Append(Environment.NewLine);
                        _msg.Append(columnErrorMessages[ce]);
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                    }
                }
                //if (numRowsWithErrors > maxRowsWithErrors)
                //    dt.Rows.Clear();
                WriteMessageToLog(_msg.ToString());
                //AppMessages.DisplayErrorMessage(_msg.ToString());
                throw new System.Exception(_msg.ToString());
            }

        }//end method


        private bool DataTypeIsSupportedByRandomizer(System.Type pType)
        {
            bool result = false;

            for (int t = 0; t < _supportedSystemTypes.Length; t++)
            {
                if (_supportedSystemTypes[t] == pType)
                {
                    result = true;
                    break;
                }
            }


            return result;
        }


        private void RefreshCurrentValueIndexes()
        {
            //Stopwatch swRefresh = new Stopwatch();

            //swRefresh.Start();

            for (int i = 0; i < _dataTableRandomizerTypeProcessorSpecs.Count; i++)
            {
                DataTableRandomizerProcessorSpec dtrSpec = _dataTableRandomizerTypeProcessorSpecs[i];
                ////test
                //_msg.Length = 0;
                //_msg.Append("LOOP: _dataTableRandomizerTypeProcessorSpecs [");
                //_msg.Append(i.ToString());
                //_msg.Append("]: dtrSpec.RandomDataListIndex = ");
                //_msg.Append(dtrSpec.RandomDataListIndex.ToString());
                //WriteMessageToLog(_msg.ToString());
                ////endtest
                if (dtrSpec.RandomDataListIndex == -1)
                {
                    //this particular specification does not have a data table. Do return for testing purposes.
                    return;
                }
                stKeyValuePair<int, DataTable> kvp = _randomValueDataTables[dtrSpec.RandomDataListIndex];
                DataTable dt = kvp.Value;

                if (dtrSpec.CurrentValueIndex == -1 || dt == null)
                {
                    //this particular specification is not being used. do return for testing purposes
                    return;
                }
                dtrSpec.CurrentValueIndex++;

                switch (dtrSpec.RandomDataType)
                {
                    case enRandomDataType.RandomNamesAndLocations:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            //past the last row in the current data table rows
                            //must load another list
                            LoadRandomNamesAndLocationsList(i, dtrSpec); 
                        }
                        break;
                    case enRandomDataType.RandomNumbers:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomNumbersList(i, dtrSpec);
                        }
                        break;
                    case enRandomDataType.RandomWords:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomWordsList(i, dtrSpec);
                        }
                        break;
                    case enRandomDataType.RandomDatesAndTimes:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomDatesAndTimesList(i, dtrSpec);
                        }
                        break;
                    case enRandomDataType.RandomBooleans:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomBooleansList(i, dtrSpec);
                        }
                        break;
                    // No need to refresh custom value list: it is never regenerated
                    //case enRandomDataType.CustomRandomValues:
                    //    if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                    //    {
                    //        LoadCustomRandomValuesList(i, dtrSpec);
                    //    }
                    //    break;
                    case enRandomDataType.RandomStrings:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomStringsList(i, dtrSpec);
                        }
                        break;
                    case enRandomDataType.RandomBytes:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomBytesList(i, dtrSpec);
                        }
                        break;
                    case enRandomDataType.RandomDataElements:
                        if (dtrSpec.CurrentValueIndex > (dt.Rows.Count - 1))
                        {
                            LoadRandomDataElementsList(i, dtrSpec);
                        }
                        break;
                    default:
                        break;
                }//end switch

                ////test
                //_msg.Length = 0;
                //_msg.Append("END : _dataTableRandomizerTypeProcessorSpecs [");
                //_msg.Append(i.ToString());
                //_msg.Append("]: dtrSpec.RandomDataListIndex = ");
                //_msg.Append(dtrSpec.RandomDataListIndex.ToString());
                //_msg.Append(Environment.NewLine);
                //WriteMessageToLog(_msg.ToString());
                ////endtest
            }//end for loop

            //swRefresh.Stop();

            //_msg.Length = 0;
            //_msg.Append("RefreshCurrentValueIndexes Elapsed Time: ");
            //_msg.Append(swRefresh.FormattedElapsedTime);
            //WriteMessageToLog(_msg.ToString());

        }//end method


        /// <summary>
        /// Routine to use column definitions in a DataTable to initialize a DataTableRandomizerColumnSpec list.
        /// </summary>
        /// <param name="dt">DataTable object containing columns to include in the column specification list.</param>
        /// <returns>List of DataTableRandomizerColumnSpec objects.</returns>
        public PFList<DataTableRandomizerColumnSpec> GetInitColSpecListFromDataTable(DataTable dt)
        {
            PFList<DataTableRandomizerColumnSpec> colSpecs = new PFList<DataTableRandomizerColumnSpec>();

            if (dt == null)
            {
                _msg.Length = 0;
                _msg.Append("DataTable is null. You must specify an instance of a DataTable in order to get a list of column specs.");
                WriteMessageToLog(_msg.ToString());
                throw new System.Exception(_msg.ToString());
            }

            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                DataColumn dc = dt.Columns[colInx];
                DataTableRandomizerColumnSpec colSpec = new DataTableRandomizerColumnSpec();
                colSpec.DataTableColumnName = dc.ColumnName;
                colSpec.DataTableColumnDataType = dc.DataType.FullName;
                colSpec.DataTableColumnIndex = colInx;
                //remainder of properties are left at their default values: to be filled in by application using the column specs.
                colSpecs.Add(colSpec);
            }

            return colSpecs;
        }

        private DateTime ConvertInt32ToDate(object oColValue)
        {
            DateTime dateOut = DateTime.MinValue;


            int nDateKey = (int)oColValue;
            string sDateKey = nDateKey.ToString("0000/00/00");

            DateTime.TryParse(sDateKey, out dateOut);

            return dateOut;
        }

        private DateTime ConvertInt32ToTime(object oColValue)
        {
            DateTime dateOut = DateTime.MinValue;

            int nTimeKey = (int)oColValue;
            string sTimeKey = "0" + nTimeKey.ToString();
            if (sTimeKey.Length > 6)
                sTimeKey = sTimeKey.Substring(1, 6);
            if (sTimeKey.Length == 6)
            {
                sTimeKey = DateTime.Now.ToString("MM/dd/yyyy ") + sTimeKey.Substring(0, 2) + ":" + sTimeKey.Substring(2, 2) + ":" + sTimeKey.Substring(4, 2);
            }
            else
            {
                sTimeKey = "01/01/0001 00:00:00";
            }

            DateTime.TryParse(sTimeKey, out dateOut);

            return dateOut;
        }

        private DateTime ConvertInt64ToDateTime(object oColValue)
        {
            DateTime dateOut = DateTime.MinValue;

            long dateKey64 = (long)oColValue;
            string sDateKey64 = dateKey64.ToString("0000/00/00 00:00:00");

            DateTime.TryParse(sDateKey64, out dateOut);

            return dateOut;
        }



        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }


    }//end public class

}//end namespace
