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
using PFMessageLogs;

namespace PFRandomDataProcessor
{
    /// <summary>
    /// NOTE: Class is only used for NamesAndLocations testing.
    /// See DataTableRandomizer for class used in application data randomizing of data table values.
    /// </summary>
    public class TEST_DataTableRandomizer
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        RandomNumber _randNumber = new RandomNumber();

        int _defaultBatchSizeForGeneratedRandomNumbers = 1000;
        int _defaultMaxBatchSizeForGeneratedRandomNumbers = 500000;

        //private variables for properties
        private DataTable _dt = null;
        private PFList<DataTableRandomizerColumnSpec> _randomizerColumnSpecs = null;
        RandomNamesAndLocationsDataRequest _randomizerNameSpecs = null;
        int _batchSizeForGeneratedRandomNames = 1000;

        private string _randomDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\Randomizer\Data\";
        private string _randomDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\Randomizer\Definitions\";
        private string _randomSamplesSubfolder = "Samples";
        private string _randomNamesAndLocationsSubfolder = "NamesAndLocations";
        private string _randomCustomDataSubfolder = "CustomValues";
        private string _randomNumbersSubfolder = "Numbers";
        private string _randomDatesSubfolder = "DatesAndTimes";
        private string _randomWordsSubfolder = "Words";
        private string _randomBooleansSubfolder = "Booleans";

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TEST_DataTableRandomizer()
        {
           ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be randomized.</param>
        /// <param name="randomizerRequests">List of specifications for which columns to randomize and values to use in the randomizing.</param>
        /// <param name="randomizerNameSpecs">Object containing various criteria for determining types of random names and locations to include.</param>
        public TEST_DataTableRandomizer(DataTable dt, PFList<DataTableRandomizerColumnSpec> randomizerRequests, RandomNamesAndLocationsDataRequest randomizerNameSpecs)
        {
            _dt = dt;
            _randomizerColumnSpecs = randomizerRequests;
            _randomizerNameSpecs = randomizerNameSpecs;
            DllMessageLogExt.WriteLine("TEST_DataTableRandomizer allocated ...");
        }


        //properties

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
        /// Contains various criteria for determining types of random names and locations to include.
        /// </summary>
        public RandomNamesAndLocationsDataRequest RandomizerNameSpecs
        {
            get
            {
                return _randomizerNameSpecs;
            }
            set
            {
                _randomizerNameSpecs = value;
            }
        }

        /// <summary>
        /// Maximum number of generated random names in list of generated names when the list the generated. 
        /// List can be generated multiple times during randomizer processing. Names on list are used once
        /// and when all names have been used, a new list is produced with the number of entries determined 
        /// by this property.
        /// </summary>
        public int BatchSizeForGeneratedRandomNames
        {
            get
            {
                return _batchSizeForGeneratedRandomNames;
            }
            set
            {
                _batchSizeForGeneratedRandomNames = value;
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
        public string RandomCustomDataSubfolder
        {
            get
            {
                return _randomCustomDataSubfolder;
            }
            set
            {
                _randomCustomDataSubfolder = value;
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
                return _randomDatesSubfolder;
            }
            set
            {
                _randomDatesSubfolder = value;
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
            CreateFolder(Path.Combine(_randomDataFolder, _randomCustomDataSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomNumbersSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomDatesSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomWordsSubfolder));
            CreateFolder(Path.Combine(_randomDataFolder, _randomBooleansSubfolder));

            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomSamplesSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomNamesAndLocationsSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomCustomDataSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomNumbersSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomDatesSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomWordsSubfolder));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, _randomBooleansSubfolder));

            //create, if necessary, the samples folders
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomNamesAndLocationsSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomCustomDataSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomNumbersSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomDatesSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomWordsSubfolder)));
            CreateFolder(Path.Combine(_randomDataFolder, Path.Combine(_randomSamplesSubfolder, _randomBooleansSubfolder)));

            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomNamesAndLocationsSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomCustomDataSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomNumbersSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomDatesSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomWordsSubfolder)));
            CreateFolder(Path.Combine(_randomDefinitionsFolder, Path.Combine(_randomSamplesSubfolder, _randomBooleansSubfolder)));

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
            RandomizeDataTableValues(this.DataTableToRandomize, this.RandomizerColumnSpecs, this.RandomizerNameSpecs);
        }

        /// <summary>
        /// Main routine for inserting random values into a data table.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be randomized.</param>
        /// <param name="colSpecs">List of specifications for which columns to randomize and values to use in the randomizing.</param>
        /// <param name="nameSpecs">Object containing various criteria for determining types of random names and locations to include.</param>
        public void RandomizeDataTableValues(DataTable dt, PFList<DataTableRandomizerColumnSpec> colSpecs, RandomNamesAndLocationsDataRequest nameSpecs)
        {
            PFList<PFList<RandomName>> randomNameLists = new PFList<PFList<RandomName>>();
            PFList<PFList<string>> randomDataValueLists = new PFList<PFList<string>>();
            PFKeyValueList<string, int> randomNamesListIndexes = new PFKeyValueList<string,int>();
            PFKeyValueList<string,int> randomDataValueListIndexes = new PFKeyValueList<string,int>();
            PFList<RandomName> currentRandomNames = null;
            PFList<string> currentRandomDataValues = null;

            PFList<RandomName> generatedRandomNamesList = null;
            RandomName currentGeneratedRandomName = null;
            bool generatedRandomNameRequested = false;
            int generatedRandomNameIndex = this.BatchSizeForGeneratedRandomNames;

            RandomDataProcessor rdp = null;

            if (dt == null || colSpecs == null || nameSpecs == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a non-null value for following parameter(s): ");
                if (dt == null)
                    _msg.Append("dt, ");
                if (colSpecs == null)
                    _msg.Append("colSpecs, ");
                if (nameSpecs == null)
                    _msg.Append("nameSpecs, ");
                char[] charsToTrim = {',',' '};
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
                throw new System.Exception(_msg.ToString().TrimEnd(charsToTrim) + ".");
            }

            if (this.BatchSizeForGeneratedRandomNames == _defaultBatchSizeForGeneratedRandomNumbers)
            {
                if (dt.Rows.Count > 0)
                    this.BatchSizeForGeneratedRandomNames = dt.Rows.Count;
            }
            if (this.BatchSizeForGeneratedRandomNames > _defaultMaxBatchSizeForGeneratedRandomNumbers)
            {
                this.BatchSizeForGeneratedRandomNames = _defaultMaxBatchSizeForGeneratedRandomNumbers;
            }


            //test
            int numDups = 0;
            string prevAddressLine1 = string.Empty;
            string prevGeneratedAddressLine1 = string.Empty;
            //end test

            this.DataTableToRandomize = dt;
            this.RandomizerColumnSpecs = colSpecs;
            this.RandomizerNameSpecs = nameSpecs;

            if (colSpecs == null || dt == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify both the data table to be randomized and the list of column randomizer specifications in order to run OLD_RandomizeDataTableValues method.");
                throw new System.Exception(_msg.ToString());
            }

            if (dt.Rows.Count < 1 || colSpecs.Count == 0)
            {
                //no changes nee3d to be made;
                return;
            }


            try
            {

                for (int inx = 0; inx < colSpecs.Count; inx++)
                {
                    DataTableRandomizerColumnSpec spec = colSpecs[inx];
                    spec.DataTableColumnIndex = GetDataTableColumnIndex(dt, spec);
                    if (spec.DataTableColumnIndex != -1)
                    {
                        if (spec.RandomDataType == enRandomDataType.RandomNamesAndLocations)
                        {
                            generatedRandomNameRequested = true;
                            spec.RandomDataFileName = string.Empty;
                            spec.RandomDataListIndex = -1;
                            spec.CurrentValueIndex = -1;
                            if (generatedRandomNamesList == null)
                            {
                                rdp = new RandomDataProcessor(nameSpecs.DatabaseFilePath, nameSpecs.DatabasePassword, nameSpecs.RandomDataXmlFilesFolder);
                                rdp.CountryRandomDataSpec = nameSpecs;
                                generatedRandomNamesList = rdp.GenerateRandomNameList(this.BatchSizeForGeneratedRandomNames);
                            }

                        }
                        else if (spec.RandomDataType == enRandomDataType.CustomRandomValues)
                        {
                            if (RandomListAlreadyStored(spec, inx, colSpecs) == false)
                            {
                                PFList<string> randomDataValueList;
                                randomDataValueList = PFList<string>.LoadFromXmlFile(spec.RandomDataFileName);
                                randomDataValueLists.Add(randomDataValueList);
                                spec.RandomDataListIndex = randomDataValueLists.Count - 1;
                                randomDataValueListIndexes.Add(new stKeyValuePair<string,int>(spec.RandomDataFileName, spec.RandomDataListIndex));
                            }
                            else
                            {
                                spec.RandomDataListIndex = GetRandomDataValueListIndex(spec, randomDataValueListIndexes); 
                            }
                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("Invalid or not specified random data file type detected: ");
                            _msg.Append(spec.RandomDataType.ToString());
                            throw new System.Exception(_msg.ToString());
                        }
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Invalid or missing column name detected: ");
                        _msg.Append(spec.DataTableColumnName);
                        _msg.Append(" Column name not found in DataTable.");
                        throw new System.Exception(_msg.ToString());
                    }
                }//end for

                generatedRandomNameIndex = -1;

                for (int rowInx = 0; rowInx < dt.Rows.Count; rowInx++)
                {
                    //test********************************************
                    if (currentRandomNames != null)
                    {
                        if (currentRandomNames.Count > 0)
                        {
                            if (rowInx > 0)
                                prevAddressLine1 = currentRandomNames[0].AddressLine1;
                            else
                                prevAddressLine1 = string.Empty;
                        }
                    }
                    if (generatedRandomNameRequested)
                    {
                        if (currentGeneratedRandomName != null)
                        {
                            if (rowInx > 0)
                                prevGeneratedAddressLine1 = currentGeneratedRandomName.AddressLine1;
                            else
                                prevGeneratedAddressLine1 = string.Empty;
                        }
                    }
                    //end test****************************************
                    
                    if (generatedRandomNameRequested)
                    {
                        generatedRandomNameIndex++;
                        if (generatedRandomNameIndex >= this.BatchSizeForGeneratedRandomNames)
                        {
                            generatedRandomNamesList.Clear();
                            generatedRandomNamesList = null;
                            generatedRandomNamesList = rdp.GenerateRandomNameList(this.BatchSizeForGeneratedRandomNames);
                            generatedRandomNameIndex = 0;
                        }
                        currentGeneratedRandomName = generatedRandomNamesList[generatedRandomNameIndex];
                    }
                    currentRandomNames = GetCurrentRandomNames(colSpecs, randomNameLists);
                    currentRandomDataValues = GetCurrentRandomDataValues(colSpecs, randomDataValueLists);
                    
                    //test***********************************************
                    if (currentRandomNames.Count > 0)
                    {
                        if (currentRandomNames[0].AddressLine1 == prevAddressLine1)
                        {
                            numDups++;
                        }
                    }
                    if (generatedRandomNameRequested)
                    {
                        if (currentGeneratedRandomName.AddressLine1 == prevGeneratedAddressLine1)
                        {
                            numDups++;
                        }
                    }
                    //end test******************************************

                    for (int specInx = 0; specInx < colSpecs.Count; specInx++)
                    {
                        DataTableRandomizerColumnSpec spec = colSpecs[specInx];
                        DataRow dr = dt.Rows[rowInx];
                        string val = string.Empty;

                        try
                        {
                            if (spec.RandomDataType == enRandomDataType.RandomNamesAndLocations)
                            {
                                val = currentGeneratedRandomName.GetPropertyValue(spec.RandomDataFieldName).ToString();
                            }
                            else
                            {
                                val = currentRandomDataValues[spec.CurrentValueIndex];
                            }
                            if (dt.Columns[spec.DataTableColumnIndex].DataType == Type.GetType("System.String"))
                            {
                                dr[spec.DataTableColumnIndex] = val;
                            }
                            else
                            {
                                dr[spec.DataTableColumnIndex] = Convert.ChangeType(val, dt.Columns[spec.DataTableColumnIndex].DataType);
                            }
                        }
                        catch (System.Exception ex)
                        {
                            _msg.Length = 0;
                            _msg.Append("Unable to randomize data for ");
                            _msg.Append(spec.DataTableColumnName);
                            if (spec.DataTableColumnIndex < dt.Columns.Count)
                            {
                                _msg.Append(" Randomized value is of type System.String. DataTable column type is ");
                                _msg.Append(dt.Columns[spec.DataTableColumnIndex].DataType.FullName);
                                if (spec.RandomDataType == enRandomDataType.RandomNamesAndLocations)
                                {
                                    _msg.Append(".");
                                    _msg.Append(" Random  field is ");
                                    _msg.Append(spec.RandomDataFieldName);
                                }
                                _msg.Append(".");
                            }
                            else
                            {
                                _msg.Append("Invalid column index. Column with index of ");
                                _msg.Append(spec.DataTableColumnIndex.ToString());
                                _msg.Append(" does not exit. Thre are ");
                                _msg.Append(dt.Columns.Count.ToString());
                                _msg.Append(" columns in the DataTable with indexes from 0 to ");
                                _msg.Append((dt.Columns.Count - 1).ToString());
                                _msg.Append(".");
                            }
                            _msg.Append(Environment.NewLine);
                            _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                            throw new System.Exception(_msg.ToString());
                        }
                 
        
                    }// end for loop on colSpecs
                }// end for loop on dt.rows

                dt.AcceptChanges(); //commit changes

            }//end try
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("Number of dup persons or businesses: ");
                _msg.Append(numDups.ToString("#,##0"));
                //Console.WriteLine(_msg.ToString());
                DllMessageLogExt.WriteLine(_msg.ToString());
            }
                 

        }//end method

        private int GetDataTableColumnIndex(DataTable dt, DataTableRandomizerColumnSpec spec)
        {
            int retval = -1;

            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                DataColumn dc = dt.Columns[colInx];
                if (dc.ColumnName == spec.DataTableColumnName)
                {
                    retval = colInx;
                    break;
                }
            }


            return retval;
        }

        private bool RandomListAlreadyStored(DataTableRandomizerColumnSpec spec, int currInx, PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            bool retval = false;

            for (int inx = 0; inx < currInx; inx++)
            {
                if (spec.RandomDataFileName == colSpecs[inx].RandomDataFileName
                    && spec.RandomDataType == colSpecs[inx].RandomDataType)
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }

        private int GetRandomNamesListIndex(DataTableRandomizerColumnSpec spec, PFKeyValueList<string, int> randomNamesListIndexes)
        {
            int listInx = -1;

            for (int inx = 0; inx < randomNamesListIndexes.Count; inx++)
            {
                if (spec.RandomDataFileName == randomNamesListIndexes[inx].Key)
                {
                    listInx = randomNamesListIndexes[inx].Value;
                    break;
                }
            }

            return listInx;
        }

        private int GetRandomDataValueListIndex(DataTableRandomizerColumnSpec spec, PFKeyValueList<string, int> randomDataValueListIndexes)
        {
            int listInx = -1;

            for (int inx = 0; inx < randomDataValueListIndexes.Count; inx++)
            {
                if (spec.RandomDataFileName == randomDataValueListIndexes[inx].Key)
                {
                    listInx = randomDataValueListIndexes[inx].Value;
                    break;
                }
            }

            return listInx;
        }

        private PFList<RandomName> GetCurrentRandomNames(PFList<DataTableRandomizerColumnSpec> colSpecs, PFList<PFList<RandomName>> randomNameLists)
        {
            PFList<RandomName> currentRandomNames = new PFList<RandomName>();

            currentRandomNames.Clear();

            for (int nameInx = 0; nameInx < randomNameLists.Count; nameInx++)
            {
                PFList<RandomName> randNames = randomNameLists[nameInx];
                int randNum = _randNumber.GenerateRandomInt(0, randNames.Count - 1);
                RandomName randName = randNames[randNum];
                currentRandomNames.Add(randName);
            }

            for (int specInx = 0; specInx < colSpecs.Count; specInx++)
            {
                DataTableRandomizerColumnSpec spec = colSpecs[specInx];
                //if (spec.RandomDataType == enRandomDataType.RandomNamesAndLocationsFile)
                //{
                //    spec.CurrentValueIndex = spec.RandomDataListIndex;
                //}
            }

            return currentRandomNames;
        }

        private PFList<string> GetCurrentRandomDataValues(PFList<DataTableRandomizerColumnSpec> colSpecs, PFList<PFList<string>> randomDataValueLists)
        {
            PFList<string> currentRandomDataValues = new PFList<string>();

            currentRandomDataValues.Clear();

            for (int valInx = 0; valInx < randomDataValueLists.Count; valInx++)
            {
                PFList<string> randDataValues = randomDataValueLists[valInx];
                int randNum = _randNumber.GenerateRandomInt(0, randDataValues.Count - 1);
                string randStringValue = randDataValues[randNum];
                currentRandomDataValues.Add(randStringValue);
            }

            for (int specInx = 0; specInx < colSpecs.Count; specInx++)
            {
                DataTableRandomizerColumnSpec spec = colSpecs[specInx];
                if (spec.RandomDataType == enRandomDataType.CustomRandomValues)
                {
                    spec.CurrentValueIndex = spec.RandomDataListIndex;
                }
            }

            return currentRandomDataValues;
        }

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
                throw new System.Exception(_msg.ToString());
            }

            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                DataColumn dc = dt.Columns[colInx];
                DataTableRandomizerColumnSpec colSpec = new DataTableRandomizerColumnSpec();
                colSpec.DataTableColumnName = dc.ColumnName;
                colSpec.DataTableColumnDataType = dc.DataType.FullName;
                //remainder of properties are left at their default values: to be filled in by application using the column specs.
                colSpecs.Add(colSpec);
            }

            return colSpecs;
        }


    }//end public class

}//end namespace
