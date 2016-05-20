//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Reflection;
using PFSQLServerCE35Objects;
using PFCollectionsObjects;
using PFRandomDataExt;
using PFTextObjects;

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Contains processing routines for generating random data.
    /// </summary>
    public class RandomDataProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        PFSQLServerCE35 _db = null;
        private string _defaultRandomDataDatabase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\RandomData.sdf";
        private string _defaultRandomDataDatabasePassword = "+d@t@p*^t$1956=";

        private string _fileXlatKey = @"vW]NlkNC?.|eE@x7";
        private string _fileXlatIV = @"SlQ36:zYNQD*=RZB";

        private string _defaultRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";

        private int ZipCodeInx = 0;
        private int StateCodeInx = 1;
        private int CityNameInx = 2;
        //private int PopulationInx = 3;
        private int FrequencyInx = 4;
        private int AreaCodeInx = 5;
        //private int MetCodeInx = 6;
        //private int CmsaCodeInx = 7;
        private int USStateNameInx = 8;
        private int USRegionNameInx = 9;
        private int USSubRegionNameInx = 10;
        private int ProvinceNameInx = 6;
        private int CanRegionNameInx = 7;
        private int CanSubRegionNameInx = 8;
        private int MexStateNameInx = 6;
        private int MexNeighborhoodInx = 7;
        private int MexRegionNameInx = 8;
        private int MexSubRegionNameInx = 9;


        private RandomNumber _rnd = new RandomNumber();
        private RandomValue _rv = new RandomValue();

        private int _entryNumber = 0;

        //private variables for properties
        private string _connectionString = string.Empty;
        private string _randomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        RandomNamesAndLocationsDataRequest _reqlist = new RandomNamesAndLocationsDataRequest();    //creates an empty request object by default; if not overridden via the CountryRandomDataSpec property, no data is returned

        //query strings form data retrievals
        private string _locationQueryForUS = "select z.ZipCode, z.StateCode, z.CityName, z.Zip_Population, case when z.zip_population >= 2000 then(z.zip_population / 2000) else 1 end as freq, z.AreaCode, z.MetCode, z.CMSA_Code, r.StateName, r.RegionName, r.SubRegionName"
                                           + "  from tblUS_ZipCityStats z join RegionCodesUS r on r.StateCode = z.StateCode"
                                           + " where z.Zip_Population >= 1000 <AndStatements>"
                                           + " order by z.Zip_Population desc, z.ZipCode";

        private string _locationQueryForCanada = "select z.PostalCode, z.ProvinceCode, z.CityName, z.PostalCodePopulation, case when z.PostalCodePopulation >= 500 then(z.PostalCodePopulation / 500) else 1 end as freq, z.AreaCode, r.ProvinceName, r.RegionName, r.SubRegionName"
                                               + "  from tblCanada_PostalCodeCityStats z join RegionCodesCanada r on r.ProvinceCode = z.ProvinceCode"
                                               + " where z.PostalCodePopulation >= 100 <AndStatements>"
                                               + " order by z.PostalCodePopulation desc, z.PostalCode";

        private string _locationQueryForMexico = "select z.PostalCode, z.StateCode, z.CityName, z.PostalCodePopulation, case when z.PostalCodePopulation >= 1000 then(z.PostalCodePopulation / 1000) else 1 end as freq, z.AreaCode, r.StateName, z.Neighborhood, r.RegionName, r.SubRegionName"
                                               + "  from tblMexico_PostalCodeCityStats z join RegionCodesMexico r on r.StateCode = z.StateCode"
                                               + " where z.PostalCodePopulation >= 100 <AndStatements>"
                                               + " order by z.PostalCodePopulation desc, z.PostalCode";

        //List of random locations for this instance
        PFList<RandomLocation> _randomLocationListUS = new PFList<RandomLocation>();
        PFList<RandomLocation> _randomLocationListCAN = new PFList<RandomLocation>();
        PFList<RandomLocation> _randomLocationListMEX = new PFList<RandomLocation>();

        //XML file names containing random data
        private string _middleInitials = "             ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //private string _countryFrequencies = "country.dat";  //change July 2015: new processing uses user-defined frequencies instead of the frequencies derived from this country file.
        private string[] _lastNames = { "LastNames.dat", "LastNames.dat", "LastNamesEN.dat", "LastNamesFR.dat", "LastNamesSP.dat" };
        private string[] _firstNames = { "FirstNames.dat", "FirstNames.dat", "FirstNamesEN.dat", "FirstNamesFR.dat", "FirstNamesSP.dat" };
        private string[] _firstNamesMale = { "FirstNamesMale.dat", "FirstNamesMale.dat", "FirstNamesMaleEN.dat", "FirstNamesMaleFR.dat", "FirstNamesMaleSP.dat" };
        private string[] _firstNamesFemale = { "FirstNamesFemale.dat", "FirstNamesFemale.dat", "FirstNamesFemaleEN.dat", "FirstNamesFemaleFR.dat", "FirstNamesFemaleSP.dat" };
        private string[] _streetNames = { "StreetNames.dat", "StreetNames.dat", "StreetNamesEN.dat", "StreetNamesFR.dat", "StreetNamesSP.dat", "StreetNamesPR.dat" };
        private string _ageGroupsAll = @"AgeGroupsAll.dat";
        private string _addressLine2File = @"AddressLine2NamesUS.dat";
        private string _addressLine2BusinessFile = @"AddressLine2Business.dat";
        private string _businessDepartmentNamesFile = @"BusinessDepartmentNames.dat";
        private string[] _bizNameTemplates = { "BizNameTemplatesUS.dat", "BizNameTemplatesUS.dat", "BizNameTemplatesEN.dat", "BizNameTemplatesFR.dat", "BizNameTemplatesSP.dat", };
        private string[] _bizNameFiles = { "BizNames.dat", "BizNames.dat", "BizNames.dat", "BusinessName1FR.dat", "BusinessName1SP.dat" };
        private string[] _bizName1Files = { "BizNamePrefix.dat", "BizNamePrefix.dat", "BizNamePrefixEN.dat", "BusinessName1FR.dat", "BusinessName1SP.dat" };
        private string[] _bizName2Files = { "BizNameSuffix.dat", "BizNameSuffix.dat", "BizNameSuffixEN.dat", "BusinessName2FR.dat", "BusinessName2SP.dat" };
        private string[] _bizNamePrefixFiles = { "BizNamePrefix.dat", "BizNamePrefix.dat", "BizNamePrefixEN.dat", "BusinessName1FR.dat", "BusinessName1SP.dat" };
        private string[] _bizNameSuffixFiles = { "BizNameSuffix.dat", "BizNameSuffix.dat", "BizNameSuffixEN.dat", "BusinessName2FR.dat", "BusinessName2SP.dat" };
        private string _bizName3Con_1File = @"BizName3Con_1.dat";
        private string _bizName3Con_2File = @"BizName3Con_2.dat";
        private string _bizName3Syllable_1File = @"BizNameSyllable_1.dat";
        private string _bizName3Syllable_2File = @"BizNameSyllable_2.dat";


        PFList<PFList<string>> _lastNamesLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _firstNamesLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _firstNamesMaleLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _firstNamesFemaleLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _streetNamesLists = new PFList<PFList<string>>();
        PFList<string> _ageGroupAllList = new PFList<string>();
        PFList<string> _ageGroupList = new PFList<string>();
        PFList<string> _addressLine2List = new PFList<string>();
        PFList<string> _addressLine2BusinessList = new PFList<string>();
        PFList<string> _businessDepartmentNamesList = new PFList<string>();
        PFList<PFList<string>> _bizNameTemplatesLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _bizNameLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _bizName1Lists = new PFList<PFList<string>>();
        PFList<PFList<string>> _bizName2Lists = new PFList<PFList<string>>();
        PFList<PFList<string>> _bizNamePrefixLists = new PFList<PFList<string>>();
        PFList<PFList<string>> _bizNameSuffixLists = new PFList<PFList<string>>();
        PFList<string> _bizName3Con_1List = new PFList<string>();
        PFList<string> _bizName3Con_2List = new PFList<string>();
        PFList<string> _bizName3Syllable_1List = new PFList<string>();
        PFList<string> _bizName3Syllable_2List = new PFList<string>();
        PFList<string> _defaultBizNameList = new PFList<string>();

        
        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomDataProcessor()
        {
            InitInstance(_defaultRandomDataDatabase, _defaultRandomDataDatabasePassword, _defaultRandomDataXmlFilesFolder);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomDataProcessor(string dbFilePath)
        {
            InitInstance(dbFilePath, _defaultRandomDataDatabasePassword, _defaultRandomDataXmlFilesFolder);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomDataProcessor(string dbFilePath, string dbPassword)
        {
            InitInstance(dbFilePath, dbPassword, _defaultRandomDataXmlFilesFolder);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomDataProcessor(string dbFilePath, string dbPassword, string randomDataXmlFilesFolder)
        {
            InitInstance(dbFilePath, dbPassword, randomDataXmlFilesFolder);
        }

        private void InitInstance(string dbFilePath, string dbPassword, string randomDataXmlFilesFolder)
        {
            _db = new PFSQLServerCE35(dbFilePath, dbPassword);
            _connectionString = _db.ConnectionString;
            _randomDataXmlFilesFolder = randomDataXmlFilesFolder;
            LoadRandomDataFiles();
            LoadAddressLine2List();
        }

        private void LoadRandomDataFiles()
        {
            for (int i = 0; i < _lastNames.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _lastNames[i]), _fileXlatKey, _fileXlatIV);
                _lastNamesLists.Add(nameList);
            }
            for (int i = 0; i < _firstNames.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _firstNames[i]), _fileXlatKey, _fileXlatIV);
                _firstNamesLists.Add(nameList);
            }
            for (int i = 0; i < _firstNamesMale.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _firstNamesMale[i]), _fileXlatKey, _fileXlatIV);
                _firstNamesMaleLists.Add(nameList);
            }
            for (int i = 0; i < _firstNamesFemale.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _firstNamesFemale[i]), _fileXlatKey, _fileXlatIV);
                _firstNamesFemaleLists.Add(nameList);
            }
            for (int i = 0; i < _streetNames.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _streetNames[i]), _fileXlatKey, _fileXlatIV);
                _streetNamesLists.Add(nameList);
            }
            for (int i = 0; i < _bizNameTemplates.Length; i++)
            {
                PFList<string> templatesList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizNameTemplates[i]), _fileXlatKey, _fileXlatIV);
                _bizNameTemplatesLists.Add(templatesList);
            }
            for (int i = 0; i < _bizNameFiles.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizNameFiles[i]), _fileXlatKey, _fileXlatIV);
                _bizNameLists.Add(nameList);
            }
            for (int i = 0; i < _bizName1Files.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizName1Files[i]), _fileXlatKey, _fileXlatIV);
                _bizName1Lists.Add(nameList);
            }
            for (int i = 0; i < _bizName2Files.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizName2Files[i]), _fileXlatKey, _fileXlatIV);
                _bizName2Lists.Add(nameList);
            }
            for (int i = 0; i < _bizNamePrefixFiles.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizNamePrefixFiles[i]), _fileXlatKey, _fileXlatIV);
                _bizNamePrefixLists.Add(nameList);
            }
            for (int i = 0; i < _bizNameSuffixFiles.Length; i++)
            {
                PFList<string> nameList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizNameSuffixFiles[i]), _fileXlatKey, _fileXlatIV);
                _bizNameSuffixLists.Add(nameList);
            }
        }

        private void LoadAgeGroupList()
        {
            _ageGroupAllList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _ageGroupsAll), _fileXlatKey, _fileXlatIV);
            string[] parsedAgeGroup;
            char[] splitChar = {'-'};
            int minAge = 0;
            int maxAge = -1;
            int age = 0;

            for (int i = 0; i < _ageGroupAllList.Count; i++)
            {
                for (int x = 0; x < _reqlist.PersonAgeGroups.Count; x++)
                {
                    //determine if age is withing one of the age groups to include for this random data run
                    parsedAgeGroup = _reqlist.PersonAgeGroups[x].Split(splitChar);
                    minAge = Convert.ToInt32(parsedAgeGroup[0].Trim());
                    maxAge = Convert.ToInt32(parsedAgeGroup[1].Trim());
                    age = Convert.ToInt32(_ageGroupAllList[i].Trim());
                    if (age >= minAge && age <= maxAge)
                    {
                        _ageGroupList.Add(_ageGroupAllList[i]);
                    }
                }
            }
        }

        private void LoadAddressLine2List()
        {
            _addressLine2List = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _addressLine2File), _fileXlatKey, _fileXlatIV);
            _addressLine2BusinessList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _addressLine2BusinessFile), _fileXlatKey, _fileXlatIV);
            _businessDepartmentNamesList = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _businessDepartmentNamesFile), _fileXlatKey, _fileXlatIV);
            _bizName3Con_1List = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizName3Con_1File), _fileXlatKey, _fileXlatIV);
            _bizName3Con_2List = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizName3Con_2File), _fileXlatKey, _fileXlatIV);
            _bizName3Syllable_1List = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizName3Syllable_1File), _fileXlatKey, _fileXlatIV);
            _bizName3Syllable_2List = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _bizName3Syllable_2File), _fileXlatKey, _fileXlatIV);
            _defaultBizNameList.Add("INVALID BUSINESS NAME TEMPLATE");
            _defaultBizNameList.Add("INVALID BUSINESS NAME TEMPLATE");
            _defaultBizNameList.Add("INVALID BUSINESS NAME TEMPLATE");

        }

        //properties

        /// <summary>
        /// CountryRandomDataSpec property. Contains request specification for what subset of country random data to return.
        /// </summary>
        public RandomNamesAndLocationsDataRequest CountryRandomDataSpec
        {
            get
            {
                return _reqlist;
            }
            set
            {
                _reqlist = value;
                LoadAgeGroupList();
            }
        }

        /// <summary>
        /// Connection string property. Points to file containing the SQLCE 3.5 database.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// RandomDataXmlFilesFolder property. Identifies the folder containing the XML lookup files used in random name generation.
        /// </summary>
        public string RandomDataXmlFilesFolder
        {
            get
            {
                return _randomDataXmlFilesFolder;
            }
            set
            {
                _randomDataXmlFilesFolder = value;
            }
        }

        //methods

        /// <summary>
        /// Create a list of RandomName objects that are populated with the requested random data.
        /// </summary>
        /// <param name="numEntriesToGenerate">Max number of items to put in the list.</param>
        /// <returns>List of RandomName objects.</returns>
        public PFList<RandomName> GenerateRandomNameList(int numEntriesToGenerate)
        {
            PFList<RandomName> rnl = new PFList<RandomName>();
            enCountry country = enCountry.NotSpecified;
            GetLocationLists();
            PFList<string> countrySelector = GetCountrySelector();

            int countryRangeStart = 0;
            int countryRangeEnd = countrySelector.Count - 1;
            int countryInx = -1;
            int nameTypeInx = -1;

            _entryNumber = 0;
            for (int i = 0; i < numEntriesToGenerate; i++)
            {
                countryInx = _rnd.GenerateRandomInt(countryRangeStart, countryRangeEnd);
                nameTypeInx = _rnd.GenerateRandomInt(1, 100);
                country = (enCountry)Enum.Parse(typeof(enCountry), countrySelector[countryInx].Replace(" ",""));    //get rid of embedded space in "United States" string.
                if (nameTypeInx <= _reqlist.PersonNamesPercentFrequency && _reqlist.IncludePersonNames)
                    rnl.Add(GenerateRandomName(enNameType.Person, country));
                else if (_reqlist.IncludeBusinessNames)
                    rnl.Add(GenerateRandomName(enNameType.Business, country));
                else
                    rnl.Add(GenerateRandomName(enNameType.NotSpecified, country));
            }
            
            return rnl;
        }

        private void GetLocationLists()
        {
            if (_reqlist.IncludeUSData)
                GetLocationListUS();

            if (_reqlist.IncludeCanadaData )
                GetLocationListCanada();

            if (_reqlist.IncludeMexicoData)
                GetLocationListMexico();
        }

        private void GetLocationListUS()
        {
            DbDataReader rdr = null;
            string locationQuery = string.Empty;

            try
            {
                string selectionCriteria = GetLocationListSelectionCriteria(enCountry.UnitedStates);

                locationQuery = _locationQueryForUS.Replace("<AndStatements>", selectionCriteria);
                _db.OpenConnection();
                rdr = _db.RunQueryDataReader(locationQuery);
                while (rdr.Read())
                {
                    int frequency = rdr.GetInt32(FrequencyInx);
                    for (int i = 0; i < frequency; i++)
                    {
                        RandomLocation rloc = new RandomLocation();
                        rloc.Country = enCountry.UnitedStates;
                        rloc.ZipPostalCode = rdr.GetString(ZipCodeInx);
                        rloc.StateProvince = rdr.GetString(StateCodeInx);
                        rloc.City = rdr.GetString(CityNameInx);
                        rloc.Neighborhood = string.Empty;
                        rloc.StateProvinceName = rdr.GetString(USStateNameInx);
                        rloc.RegionName = rdr.GetString(USRegionNameInx);
                        rloc.SubRegionName = rdr.GetString(USSubRegionNameInx);
                        rloc.AreaCode = rdr.GetString(AreaCodeInx);
                        rloc.CountryCode = "US";
                        _randomLocationListUS.Add(rloc);
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to build list of random locations failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (rdr != null)
                {
                    if (rdr.IsClosed == false)
                    {
                        rdr.Close();
                    }
                }
                if (_db != null)
                {
                    if (_db.IsConnected)
                    {
                        _db.CloseConnection();
                    }
                }
            }
                 
        

        }//end method

        private void GetLocationListCanada()
        {
            DbDataReader rdr = null;
            string locationQuery = string.Empty;

            try
            {
                string selectionCriteria = GetLocationListSelectionCriteria(enCountry.Canada);

                locationQuery = _locationQueryForCanada.Replace("<AndStatements>", selectionCriteria);
                _db.OpenConnection();
                rdr = _db.RunQueryDataReader(locationQuery);
                while (rdr.Read())
                {
                    int frequency = rdr.GetInt32(FrequencyInx);
                    for (int i = 0; i < frequency; i++)
                    {
                        RandomLocation rloc = new RandomLocation();
                        rloc.Country = enCountry.Canada;
                        rloc.ZipPostalCode = rdr.GetString(ZipCodeInx);
                        rloc.StateProvince = rdr.GetString(StateCodeInx);
                        rloc.City = rdr.GetString(CityNameInx);
                        rloc.Neighborhood = string.Empty;
                        rloc.StateProvinceName = rdr.GetString(ProvinceNameInx);
                        rloc.RegionName = rdr.GetString(CanRegionNameInx);
                        rloc.SubRegionName = rdr.GetString(CanSubRegionNameInx);
                        rloc.AreaCode = rdr.GetString(AreaCodeInx);
                        rloc.CountryCode = "CA";
                        _randomLocationListCAN.Add(rloc);
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to build list of random locations failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (rdr != null)
                {
                    if (rdr.IsClosed == false)
                    {
                        rdr.Close();
                    }
                }
                if (_db != null)
                {
                    if (_db.IsConnected)
                    {
                        _db.CloseConnection();
                    }
                }
            }


        }// end get location list canada


        private void GetLocationListMexico()
        {
            DbDataReader rdr = null;
            string locationQuery = string.Empty;

            try
            {
                string selectionCriteria = GetLocationListSelectionCriteria(enCountry.Mexico);

                locationQuery = _locationQueryForMexico.Replace("<AndStatements>", selectionCriteria);
                _db.OpenConnection();
                rdr = _db.RunQueryDataReader(locationQuery);
                while (rdr.Read())
                {
                    int frequency = rdr.GetInt32(FrequencyInx);
                    for (int i = 0; i < frequency; i++)
                    {
                        RandomLocation rloc = new RandomLocation();
                        rloc.Country = enCountry.Mexico;
                        rloc.ZipPostalCode = rdr.GetString(ZipCodeInx);
                        rloc.StateProvince = rdr.GetString(StateCodeInx);
                        rloc.City = rdr.GetString(CityNameInx);
                        rloc.Neighborhood = rdr.GetString(MexNeighborhoodInx);
                        rloc.StateProvinceName = rdr.GetString(MexStateNameInx);
                        rloc.RegionName = rdr.GetString(MexRegionNameInx);
                        rloc.SubRegionName = rdr.GetString(MexSubRegionNameInx);
                        rloc.AreaCode = rdr.GetString(AreaCodeInx);
                        rloc.CountryCode = "MX";
                        _randomLocationListMEX.Add(rloc);
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to build list of random locations failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (rdr != null)
                {
                    if (rdr.IsClosed == false)
                    {
                        rdr.Close();
                    }
                }
                if (_db != null)
                {
                    if (_db.IsConnected)
                    {
                        _db.CloseConnection();
                    }
                }
            }

        }//end get locationlist mexico

        private string GetLocationListSelectionCriteria(enCountry country)
        {
            StringBuilder selectors = new StringBuilder();

            selectors.Length = 0;

            foreach (LocationSelectionCriteria criteria in _reqlist.LocationSelectionCriteriaLists)
            {

                if (criteria.Country == country)
                {

                    if (criteria.Regions.Count > 0)
                    {
                        selectors.Append(" and r.RegionName in (");
                        for (int i = 0; i < criteria.Regions.Count; i++)
                        {
                            if (i > 0)
                                selectors.Append(", ");
                            selectors.Append("'");
                            selectors.Append(criteria.Regions[i]);
                            selectors.Append("'");
                        }
                        selectors.Append(")");
                    }//end regions

                    if (criteria.SubRegions.Count > 0)
                    {
                        selectors.Append(" and r.SubRegionName in (");
                        for (int i = 0; i < criteria.SubRegions.Count; i++)
                        {
                            if (i > 0)
                                selectors.Append(", ");
                            selectors.Append("'");
                            selectors.Append(criteria.SubRegions[i]);
                            selectors.Append("'");
                        }
                        selectors.Append(")");
                    }//end subregions


                    if (criteria.StatesProvinces.Count > 0)
                    {
                        selectors.Append(" and r.");
                        switch (country)
                        {
                            case enCountry.UnitedStates:
                                selectors.Append("StateName");
                                break;
                            case enCountry.Canada:
                                selectors.Append("ProvinceName");
                                break;
                            case enCountry.Mexico:
                                selectors.Append("StateName");
                                break;
                            default:
                                selectors.Append("StateName");
                                break;
                        }
                        selectors.Append(" in (");
                        for (int i = 0; i < criteria.StatesProvinces.Count; i++)
                        {
                            if (i > 0)
                                selectors.Append(", ");
                            selectors.Append("'");
                            selectors.Append(criteria.StatesProvinces[i].Trim());
                            selectors.Append("'");
                        }
                        selectors.Append(")");

                    }//end StatesProvinces

                    char[] cityCriteriaParseChars = { ':', '~' };
                    string[] parsedCityCriteria;

                    if (criteria.MsaAreaCodes.Count > 0)
                    {
                        selectors.Append(" and z.");
                        switch (country)
                        {
                            case enCountry.UnitedStates:
                                selectors.Append("MetCode");
                                break;
                            case enCountry.Canada:
                                selectors.Append("AreaCode");
                                cityCriteriaParseChars = new char[] { ':', '-' };
                                break;
                            case enCountry.Mexico:
                                selectors.Append("AreaCode");
                                cityCriteriaParseChars = new char[] { ':', '-' };
                                break;
                            default:
                                selectors.Append("MetCode");
                                break;
                        }
                        selectors.Append(" in (");
                        for (int i = 0; i < criteria.MsaAreaCodes.Count; i++)
                        {
                            if (i > 0)
                                selectors.Append(", ");
                            selectors.Append("'");
                            parsedCityCriteria = criteria.MsaAreaCodes[i].Split(cityCriteriaParseChars);
                            selectors.Append(parsedCityCriteria[0].Trim());
                            selectors.Append("'");
                        }
                        selectors.Append(")");

                    }//end MsaAreaCodes

                    if (criteria.CmsaLargeCities.Count > 0)
                    {
                        selectors.Append(" and ");
                        switch (country)
                        {
                            case enCountry.UnitedStates:
                                selectors.Append("z.CMSA_Code");
                                break;
                            case enCountry.Canada:
                                selectors.Append("r.ProvinceName");
                                break;
                            case enCountry.Mexico:
                                selectors.Append("r.StateName");
                                break;
                            default:
                                selectors.Append("z.CMSA_Code");
                                break;
                        }
                        selectors.Append(" in (");
                        for (int i = 0; i < criteria.CmsaLargeCities.Count; i++)
                        {
                            if (i > 0)
                                selectors.Append(", ");
                            selectors.Append("'");
                            parsedCityCriteria = criteria.CmsaLargeCities[i].Split(cityCriteriaParseChars);
                            selectors.Append(parsedCityCriteria[0].Trim());
                            selectors.Append("'");
                        }
                        selectors.Append(")");

                        if (country == enCountry.Canada || country == enCountry.Mexico)
                        {
                            selectors.Append(" and z.CityName in (");
                            for (int i = 0; i < criteria.CmsaLargeCities.Count; i++)
                            {
                                if (i > 0)
                                    selectors.Append(", ");
                                selectors.Append("'");
                                parsedCityCriteria = criteria.CmsaLargeCities[i].Split(cityCriteriaParseChars);
                                selectors.Append(parsedCityCriteria[1].Trim());
                                selectors.Append("'");
                            }
                            selectors.Append(")");
                        }

                    }//end CmsaLargeCities

                }//end if criterialCountry == country


            }//end foreach
            
            
            return selectors.ToString();

        }//end method

        private PFList<string> GetCountrySelector()
        {
            //Following code replaced July 2015: user specified frequencies to be used instead
            //PFList<string> allCountries = new PFList<string>();
            //PFList<string> countrySelector = new PFList<string>();
            //string[] countryToInclude = { "", "", "" };
            //int inx = -1;

            //if (_reqlist.IncludeUSData && _randomLocationListUS.Count > 0)
            //{
            //    inx++;
            //    countryToInclude[inx] = "United States";
            //}
            //if (_reqlist.IncludeCanadaData && _randomLocationListCAN.Count > 0)
            //{
            //    inx++;
            //    countryToInclude[inx] = "Canada";
            //}
            //if (_reqlist.IncludeMexicoData && _randomLocationListMEX.Count > 0) 
            //{
            //    inx++;
            //    countryToInclude[inx] = "Mexico";
            //}

            //allCountries = PFList<string>.LoadFromXmlFile(Path.Combine(_randomDataXmlFilesFolder, _countryFrequencies), _fileXlatKey, _fileXlatIV);
            //var varSelector = from c in allCountries where countryToInclude.Contains(c) select c;

            //foreach (string s in varSelector)
            //{
            //    countrySelector.Add(s);
            //}

            //new code July 2015: user defined frequencies used
            PFList<string> countrySelector = new PFList<string>();

            if (_reqlist.IncludeUSData)
            {
                for (int n = 0; n < _reqlist.PercentFrequencyUnitedStates; n++)
                {
                    countrySelector.Add("United States");
                }
            }

            if (_reqlist.IncludeCanadaData)
            {
                for (int n = 0; n < _reqlist.PercentFrequencyCanada; n++)
                {
                    countrySelector.Add("Canada");
                }
            }

            if (_reqlist.IncludeMexicoData)
            {
                for (int n = 0; n < _reqlist.PercentFrequencyMexico; n++)
                {
                    countrySelector.Add("Mexico");
                }
            }

            if (countrySelector.Count == 0)
            {
                countrySelector.Add("UnitedStates");
                countrySelector.Add("UnitedStates");
                countrySelector.Add("UnitedStates");
                countrySelector.Add("UnitedStates");
                countrySelector.Add("UnitedStates");
                countrySelector.Add("UnitedStates");
                countrySelector.Add("UnitedStates");
                countrySelector.Add("Canada");
                countrySelector.Add("Mexico");
                countrySelector.Add("Mexico");
            }
            
            return countrySelector;
        }

        /// <summary>
        /// Creates a random person or business name for the specified country.
        /// </summary>
        /// <param name="nameType">Person or business name.</param>
        /// <param name="country">Country on which the random name will be based.</param>
        /// <returns>Object containing the random name values.</returns>
        public RandomName GenerateRandomName(enNameType nameType, enCountry country)
        {
            RandomName rn = new RandomName();
            enNameLanguage lang = enNameLanguage.NotSpecified;
            enStreetNameLanguage stLang = enStreetNameLanguage.NotSpecified;

            rn.NameRowNum = ++_entryNumber;
            rn.NameType = nameType;

            if (_reqlist.IncludeCityLocation)
            {
                RandomLocation rl = GenerateRandomLocation(country);
                rn.Country = country;
                rn.ZipPostalCode = rl.ZipPostalCode;
                rn.StateProvince = rl.StateProvince;
                rn.City = rl.City;
                rn.Neighborhood = rl.Neighborhood;
                rn.StateProvinceName = rl.StateProvinceName;
                rn.RegionName = rl.RegionName;
                rn.SubRegionName = rl.SubRegionName;
                rn.AreaCode = rl.AreaCode;
                switch (country)
                {
                    case enCountry.UnitedStates:
                        rn.CountryCode = "US";
                        break;
                    case enCountry.Canada:
                        rn.CountryCode = "CA";
                        break;
                    case enCountry.Mexico:
                        rn.CountryCode = "MX";
                        break;
                    default:
                        rn.CountryCode = string.Empty;
                        break;
                }

                switch (rl.Country)
                {
                    case enCountry.UnitedStates:
                        if (rl.StateProvince == "PR")
                        {
                            lang = enNameLanguage.Spanish;
                            stLang = enStreetNameLanguage.PuertoRico;
                        }
                        else
                        {
                            lang = enNameLanguage.US;
                            stLang = enStreetNameLanguage.US;
                        }
                        break;
                    case enCountry.Canada:
                        if (rl.StateProvince == "QC")
                        {
                            lang = enNameLanguage.French;
                            stLang = enStreetNameLanguage.French;
                        }
                        else
                        {
                            lang = enNameLanguage.English;
                            stLang = enStreetNameLanguage.English;
                        }
                        break;
                    case enCountry.Mexico:
                        lang = enNameLanguage.Spanish;
                        stLang = enStreetNameLanguage.Spanish;
                        break;
                    default:
                        lang = enNameLanguage.US;
                        stLang = enStreetNameLanguage.US;
                        break;
                }

                if (_reqlist.IncludeAddressLine1)
                {
                    RandomAddressLine1 ral = GenerateRandomAddressLine1(stLang, country);
                    rn.AddressLine1 = ral.AddressLine1;
                }

                //Include address line 2 if specified or if country is mexico and address line 1 specified.
                //For Mexican addresses, address line 2 always contains the neighborhood personName.
                if ((_reqlist.IncludeAddressLine2) || (country == enCountry.Mexico && _reqlist.IncludeAddressLine1))
                {
                    int ral2Inx = _rnd.GenerateRandomInt(1, 100);
                    if (ral2Inx <= _reqlist.AddressLine2PercentFrequency || (country == enCountry.Mexico && _reqlist.IncludeAddressLine1))
                    {
                        RandomAddressLine2 ral2 = GenerateRandomAddressLine2(country, rl, nameType, lang);
                        rn.AddressLine2 = ral2.AddressLine2;
                    }
                    else
                    {
                        rn.AddressLine2 = string.Empty;
                    }
                }

            }

            if (nameType == enNameType.Person)
            {
                if (_reqlist.IncludePersonNames)
                {
                    RandomPersonName rpn;
                    rpn = GenerateRandomPersonName(lang);
                    rn.LastName = rpn.LastName;
                    rn.FirstName = rpn.FirstName;
                    rn.MiddleInitial = rpn.MiddleInitial;
                    if (_reqlist.IncludeGenderForPersons)
                        rn.Gender = rpn.Gender;
                    if (_reqlist.IncludePersonBirthDate)
                    {
                        RandomPersonDemographics rpd = new RandomPersonDemographics();
                        rpd = GenerateRandomPersonDemographics(country);
                        rn.BirthDate = rpd.BirthDate;
                    }
                }
            }
            else
            {
                if (_reqlist.IncludeBusinessNames)
                {
                    RandomBusinessName rbn;
                    if (String.IsNullOrEmpty(rn.City) == false && String.IsNullOrEmpty(rn.StateProvince) == false)
                        rbn = GenerateRandomBusinessName(lang, rn.City, rn.StateProvinceName);
                    else
                        rbn = GenerateRandomBusinessName(lang, "CentroVille", "Espagne");
                    rn.LastName = rbn.BusinessName;
                }
            }

            if (_reqlist.IncludeNationalId)
            {
                rn.NationalId = GetNationalId(rn.Country, rn.NameType);
            }
            if (_reqlist.IncludeTelephoneNumber)
            {
                rn.TelephoneNumber = GetTelephoneNumber(rn.Country, rn.AreaCode);
            }
            if (_reqlist.IncludeEmailAddress)
            {
                rn.EmailAddress = _rv.GetEmailAddress();
            }

            return rn;
        }

        /// <summary>
        /// Creates an object containing random location data based on the specified country.
        /// </summary>
        /// <param name="country">Country on which random data will be based.</param>
        /// <returns>Object containing random location data</returns>
        public RandomLocation GenerateRandomLocation(enCountry country)
        {
            PFList<RandomLocation> randomLocationList = country == enCountry.UnitedStates ? _randomLocationListUS : country == enCountry.Canada ? _randomLocationListCAN : country == enCountry.Mexico ? _randomLocationListMEX : _randomLocationListUS;
            RandomLocation rl = new RandomLocation();

            if (randomLocationList.Count == 0)
                return rl;

            int rangeMin = 0;
            int rangeMax = randomLocationList.Count - 1;
            int inx = _rnd.GenerateRandomInt(rangeMin, rangeMax);

            rl.Country = randomLocationList[inx].Country;
            rl.ZipPostalCode = randomLocationList[inx].ZipPostalCode;
            rl.StateProvince = randomLocationList[inx].StateProvince;
            rl.City = randomLocationList[inx].City;
            if (country == enCountry.Mexico)
                rl.Neighborhood = randomLocationList[inx].Neighborhood;
            else
                rl.Neighborhood = string.Empty;
            rl.StateProvinceName = randomLocationList[inx].StateProvinceName;
            rl.RegionName = randomLocationList[inx].RegionName;
            rl.SubRegionName = randomLocationList[inx].SubRegionName;
            rl.AreaCode = randomLocationList[inx].AreaCode;
            rl.CountryCode = randomLocationList[inx].CountryCode;
            return rl;
        }

        /// <summary>
        /// Creates a random personal name (last, first, middle names) based on the specified language.
        /// </summary>
        /// <param name="lang">Language to use when creating the name.</param>
        /// <returns>Random personal name.</returns>
        public RandomPersonName GenerateRandomPersonName(enNameLanguage lang)
        {
            RandomPersonName rpn = new RandomPersonName();
            PFList<string> firstNames;
            int num = -1;

            rpn.Language = lang;

            num = _rnd.GenerateRandomInt(1, 100);
            if (num <= _reqlist.MalePersonNamePercentFrequency)
            {
                firstNames = _firstNamesMaleLists[(int)lang];
                rpn.Gender = enGender.Male;
            }
            else
            {
                firstNames = _firstNamesFemaleLists[(int)lang];
                rpn.Gender = enGender.Female;
            }
            PFList<string> lastNames = _lastNamesLists[(int)lang];


            rpn.FirstName = firstNames[_rnd.GenerateRandomInt(0, firstNames.Count - 1)];
            rpn.LastName = lastNames[_rnd.GenerateRandomInt(0, lastNames.Count - 1)];
            rpn.MiddleInitial = _middleInitials.Substring(_rnd.GenerateRandomInt(0, _middleInitials.Length - 1), 1);


            return rpn;
        }

        /// <summary>
        /// Creates a random birth date
        /// </summary>
        /// <param name="country">Valid country value. Must be supplied but it is not used in determining the birth date..</param>
        /// <returns>Object containing the random birth date.</returns>
        /// <remarks>Birth dates are based on an age group data range retrieved from U.S. data.</remarks>
        public RandomPersonDemographics GenerateRandomPersonDemographics(enCountry country)
        {
            RandomPersonDemographics rpe = new RandomPersonDemographics();
            int rangeMin = 0;
            int rangeMax = - 1;
            int inx = -1;
            int age = 0;
            int days = 0;
            DateTime birthDate = new DateTime();

            DateTime currDate = _reqlist.BaseDateTime;
            if (_ageGroupList.Count > 0)
            {
                //only allow for ages within ranges specified for this instance
                rangeMin = 0;
                rangeMax = _ageGroupList.Count - 1;   //use list that only contains ages specified for inclusion
                inx = _rnd.GenerateRandomInt(rangeMin, rangeMax);
                age = Convert.ToInt32(_ageGroupList[inx]);
                days = _rnd.GenerateRandomInt(0, 180);
                birthDate = currDate.AddYears(age * -1).AddDays(days * -1);
            }
            else if (_ageGroupAllList.Count > 0)
            {
                //allow for any possible age
                rangeMin = 0;
                rangeMax = _ageGroupAllList.Count - 1;    //use list that contains all ages
                inx = _rnd.GenerateRandomInt(rangeMin, rangeMax);
                age = Convert.ToInt32(_ageGroupAllList[inx]);
                days = _rnd.GenerateRandomInt(0, 180);
                birthDate = currDate.AddYears(age * -1).AddDays(days * -1);
            }
            else
            {
                birthDate = currDate.AddYears(100);
            }

            rpe.BirthDate = birthDate.ToString("yyyy/MM/dd");
            rpe.Country = country;

            return rpe;
        }

        /// <summary>
        /// Creates an address line 1 random value that can be used as part of a random location.
        /// </summary>
        /// <param name="stLang">Language to use when creating the address value.</param>
        /// <param name="country">Country on which the random value is based.</param>
        /// <returns>A random address line 1 value.</returns>
        public RandomAddressLine1 GenerateRandomAddressLine1(enStreetNameLanguage stLang, enCountry country)
        {
            RandomAddressLine1 ra = new RandomAddressLine1();
            PFList<string> streetNames = _streetNamesLists[(int)stLang];
            int num = 0;
            int aptNum = 0;

            ra.Country = country;

            num = _rnd.GenerateRandomInt(1, 5000);
            aptNum = _rnd.GenerateRandomInt(1, 1000);
            if(country == enCountry.Mexico)
            {
                if (_rnd.GenerateRandomInt(1, 100) > 20)
                    ra.AddressLine1 = streetNames[_rnd.GenerateRandomInt(0, streetNames.Count - 1)] + " " + num.ToString("###0");
                else
                    //attach an apartment number in 20 percent of the Mexico address lines
                    ra.AddressLine1 = streetNames[_rnd.GenerateRandomInt(0, streetNames.Count - 1)] + " " + num.ToString("###0") + " - " + aptNum.ToString();
            }
            else
                ra.AddressLine1 = num.ToString("###0") + " " + streetNames[_rnd.GenerateRandomInt(0, streetNames.Count - 1)];

            return ra;
        }

        /// <summary>
        /// Creates an address line 1 random value that can be used as part of a random location.
        /// </summary>
        /// <param name="country">Country on which the random value is based.</param>
        /// <param name="rl">Object containing a previously generated random location.</param>
        /// <param name="nameType">Person or Business. Type of name to which this address line will be attached varies depending on whether or not name is for a person or a business.</param>
        /// <param name="lang">Language to use when creating the address value.></param>
        /// <returns>Object containing text of a random Address Line 2 value.</returns>
        public RandomAddressLine2 GenerateRandomAddressLine2(enCountry country, RandomLocation rl, enNameType nameType, enNameLanguage lang)
        {
            RandomAddressLine2 ra = new RandomAddressLine2();
            int num = 0;

            ra.Country = country;

            if (country == enCountry.Mexico)
            {
                ra.AddressLine2 = rl.Neighborhood;
            }
            else
            {
                if (nameType == enNameType.Person)
                {
                    num = _rnd.GenerateRandomInt(1, 100);
                    ra.AddressLine2 = _addressLine2List[_rnd.GenerateRandomInt(0, _addressLine2List.Count - 1)] + " " + num.ToString("##0");
                }
                else
                {
                    ra.AddressLine2 = GenerateRandomBusinessAddressLine2(country,  rl, lang);
                }
            }

            return ra;
        }

        private string GenerateRandomBusinessAddressLine2(enCountry country, RandomLocation rl, enNameLanguage lang)
        {
            string addressLine2 = string.Empty;
            string tempLine = string.Empty;
            int num = -1;
            int num2 = -1;

            num = _rnd.GenerateRandomInt(0, _addressLine2BusinessList.Count - 1);
            tempLine = _addressLine2BusinessList[num];
            switch (tempLine)
            {
                case "MailStop":
                    num2 = _rnd.GenerateRandomInt(1, 500);
                    addressLine2 = tempLine + " " + num2.ToString("#0");
                    break;
                case "Building":
                    num2 = _rnd.GenerateRandomInt(1, 10);
                    addressLine2 = tempLine + " " + num2.ToString("#0");
                    break;
                case "Room":
                    num2 = _rnd.GenerateRandomInt(1, 100);
                    addressLine2 = tempLine + " " + num2.ToString("#0");
                    break;
                case "Attention: <PersonName>":
                    PFList<string> lastNames = _lastNamesLists[(int)lang];
                    PFList<string> firstNames = _firstNamesLists[(int)lang];
                    num = _rnd.GenerateRandomInt(0, lastNames.Count - 1);
                    num2 = _rnd.GenerateRandomInt(0, firstNames.Count - 1);
                    string personName = firstNames[num2] + " " + lastNames[num];
                    addressLine2 = "Attention: " + personName;
                    break;
                case "<DepartmentName>":
                    num = _rnd.GenerateRandomInt(0, _businessDepartmentNamesList.Count - 1);
                    string deptName = _businessDepartmentNamesList[(int)num];
                    addressLine2 = "Attention: " + deptName;
                    break;
                default:
                    num2 = _rnd.GenerateRandomInt(1, 500);
                    addressLine2 = tempLine + " " + num2.ToString("#0");
                    break;
            }

            return addressLine2;
        }
        
        /// <summary>
        /// Create a random business name.
        /// </summary>
        /// <param name="lang">Language to use when generating the name.</param>
        /// <returns>A random business name.</returns>
        public RandomBusinessName GenerateRandomBusinessName(enNameLanguage lang)
        {
            return GenerateRandomBusinessName(lang, "VilleCentre", "Provencal");
        }

        /// <summary>
        /// Create a random business name.
        /// </summary>
        /// <param name="lang">Language to use when generating the name.</param>
        /// <param name="cityName">City in which the business is located. (optional)</param>
        /// <param name="stateName">State or Province in which the business is located. (optional)</param>
        /// <returns>A random business name.</returns>
        /// <remarks>Some of the randomly generated business names contain the city or state name. If parameters 
        /// for the city and state names are left blank, default randomized city/state names will be created.</remarks>
        public RandomBusinessName GenerateRandomBusinessName(enNameLanguage lang, string cityName, string stateName)
        {
            RandomBusinessName rbn = new RandomBusinessName();
            int num = -1;
            string template = string.Empty;
            string[] templateParts;
            string templatePart = string.Empty;

            rbn.Language = lang;

            PFList<string> templatesList = _bizNameTemplatesLists[(int)lang];
            num = _rnd.GenerateRandomInt(0, templatesList.Count - 1);
            template = templatesList[num];
            templateParts = template.Split('|');
            StringBuilder businessName = new StringBuilder();
            PFList<string> templatePartList = null;
            bool appendSpaceAfterPart = false;
            string bizNameReturnValue = string.Empty;

            rbn.BusinessName = "XXXXXX YYYYYY ZZZZZZ";

            businessName.Length = 0;

            for (int i = 0; i < templateParts.Length; i++)
            {
                templatePart = templateParts[i];
                appendSpaceAfterPart = false;
                switch (templatePart)
                {
                    case "<BizName>":
                        templatePartList = _bizNameLists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizName1>":
                        templatePartList = _bizName1Lists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizName2>":
                        templatePartList = _bizName2Lists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizNamePrefix>":
                        templatePartList = _bizNamePrefixLists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizNameSuffix>":
                        templatePartList = _bizNameSuffixLists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizName3Con_1><BizName3Con_2>":
                        templatePartList = _bizName3Con_1List;
                        businessName.Append(templatePartList[_rnd.GenerateRandomInt(0, templatePartList.Count - 1)]);
                        templatePartList = _bizName3Con_2List;
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizName3Con_1>":
                        templatePartList = _bizName3Con_1List;
                        appendSpaceAfterPart = false;
                        break;
                    case "<BizName3Con_2>":
                        templatePartList = _bizName3Con_2List;
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizNameSyllable_1><BizNameSyllable_2>":
                        templatePartList = _bizName3Syllable_1List;
                        businessName.Append(templatePartList[_rnd.GenerateRandomInt(0, templatePartList.Count - 1)]);
                        templatePartList = _bizName3Syllable_2List;
                        appendSpaceAfterPart = true;
                        break;
                    case "<BizNameSyllable_1>":
                        templatePartList = _bizName3Syllable_1List;
                        appendSpaceAfterPart = false;
                        break;
                    case "<BizNameSyllable_2>":
                        templatePartList = _bizName3Syllable_2List;
                        appendSpaceAfterPart = true;
                        break;
                    case "<LastName>":
                        templatePartList = _lastNamesLists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    case "<FirstName>":
                        templatePartList = _firstNamesLists[(int)lang];
                        appendSpaceAfterPart = true;
                        break;
                    default:
                        templatePartList = _defaultBizNameList;
                        appendSpaceAfterPart = true;
                        break;
                }//end switch
                businessName.Append(templatePartList[_rnd.GenerateRandomInt(0, templatePartList.Count - 1)]);
                if(appendSpaceAfterPart)
                    businessName.Append(" ");
            }//end for loop

            bizNameReturnValue = businessName.ToString().TrimEnd().Replace("<CityName>", cityName).Replace("<StateName>", stateName);

            rbn.BusinessName = bizNameReturnValue;

            return rbn;
        }

        /// <summary>
        /// Routine to generate a random national ID.
        /// </summary>
        /// <param name="country">Country for which ID is formatted.</param>
        /// <param name="nameType">Specify person or business name for the national id.</param>
        /// <returns>String value containing a random national ID.</returns>
        public string GetNationalId(enCountry country, enNameType nameType)
        {
            RandomValue.enNationalIdCountry rvCountry = GetNationalIdCountry(country);
            RandomValue.enNationalIdNameType rvNameType = GetNationalIdNameType(nameType);

            return _rv.GetNationalId(rvCountry, rvNameType);
        }

        /// <summary>
        /// Routine to generate a random telephone number.
        /// </summary>
        /// <param name="country">Country for which the telephone number is formatted.</param>
        /// <param name="areaCode">Area code for the random number.</param>
        /// <returns>String value containing a random telephone number.</returns>
        public string GetTelephoneNumber(enCountry country, string areaCode)
        {
            string countryCode = country == enCountry.Mexico ? "52" : "1";

            return _rv.GetTelephoneNumber(countryCode, areaCode);
        }

        private RandomValue.enNationalIdCountry GetNationalIdCountry(enCountry country)
        {
            RandomValue.enNationalIdCountry rvCountry = RandomValue.enNationalIdCountry.NotSpecified;

            switch (country)
            {
                case enCountry.UnitedStates:
                    rvCountry = RandomValue.enNationalIdCountry.UnitedStates;
                    break;
                case enCountry.Canada:
                    rvCountry = RandomValue.enNationalIdCountry.Canada;
                    break;
                case enCountry.Mexico:
                    rvCountry = RandomValue.enNationalIdCountry.Mexico;
                    break;
                default:
                    rvCountry = RandomValue.enNationalIdCountry.NotSpecified;
                    break;
            }

            return rvCountry;
        }

        private RandomValue.enNationalIdNameType GetNationalIdNameType(enNameType nameType)
        {
            RandomValue.enNationalIdNameType rvNameType = RandomValue.enNationalIdNameType.NotSpecified;

            switch (nameType)
            {
                case enNameType.Person:
                    rvNameType = RandomValue.enNationalIdNameType.Person;
                    break;
                case enNameType.Business:
                    rvNameType = RandomValue.enNationalIdNameType.Business;
                    break;
                default:
                    rvNameType = RandomValue.enNationalIdNameType.NotSpecified;
                    break;
            }

            return rvNameType;
        }

        /// <summary>
        /// Creates a randomized person name (first name plus optional middle initial plus last name.
        /// </summary>
        /// <param name="language">Language to use for the name.</param>
        /// <param name="outputMiddleInitial">Set to false if you do not wish to return the middle initial (if it exists) for a name.</param>
        /// <returns>String containing full name.</returns>
        public string GetFullName(enNameLanguage language, bool outputMiddleInitial)
        {
            RandomPersonName rpn = new RandomPersonName();
            StringBuilder fullName = new StringBuilder();

            _reqlist.MalePersonNamePercentFrequency = 50;
            rpn = GenerateRandomPersonName(language);

            if (rpn != null)
            {
                if (rpn.FirstName.Length > 0)
                {
                    fullName.Append(rpn.FirstName);
                    fullName.Append(" ");
                }
                if (outputMiddleInitial)
                {
                    if (rpn.MiddleInitial.Trim().Length > 0)
                    {
                        fullName.Append(rpn.MiddleInitial);
                        fullName.Append(" ");
                    }
                }
                if (rpn.LastName.Length > 0)
                {
                    fullName.Append(rpn.LastName);
                 }
            }

            return fullName.ToString().TrimEnd();
        }

        /// <summary>
        /// Creates a randomized person last name.
        /// </summary>
        /// <param name="language">Language to use for the name.</param>
        /// <returns>String containing last name.</returns>
        public string GetLastName(enNameLanguage language)
        {
            RandomPersonName rpn = new RandomPersonName();
            string lastName = string.Empty;

            _reqlist.MalePersonNamePercentFrequency = 50;
            rpn = GenerateRandomPersonName(language);

            if (rpn != null)
            {
                lastName = rpn.LastName;
            }

            return lastName;
        }

        /// <summary>
        /// Generates random business name.
        /// </summary>
        /// <param name="language">Language to use for the business name.</param>
        /// <returns>String containing business name.</returns>
        /// <remarks>This routine is primarily used by the RandomDataElementDataTable class in PFRandomValueDataTables.</remarks>
        public string GetBusinessName(enNameLanguage language)
        {
            RandomBusinessName rbn = new RandomBusinessName();
            string businessName = string.Empty;

            rbn = GenerateRandomBusinessName(language, "CityCenter", "Stateside");

            businessName = rbn.BusinessName;

            return businessName;
        }

        /// <summary>
        /// Creates a randomized person first name.
        /// </summary>
        /// <param name="language">Language to use for the name.</param>
        /// <returns>String containing first name.</returns>
        public string GetFirstName(enNameLanguage language)
        {
            RandomPersonName rpn = new RandomPersonName();
            string firstName = string.Empty;

            _reqlist.MalePersonNamePercentFrequency = 50;
            rpn = GenerateRandomPersonName(language);

            if (rpn != null)
            {
                firstName = rpn.FirstName;
            }

            return firstName;
        }



        /// <summary>
        /// Converts list of RandomName objects into a DataTable object.
        /// </summary>
        /// <param name="rn">RandomName object.</param>
        /// <returns>DataTable object containing the data originally in the RandomName object.</returns>
        public DataTable ConvertRandomNameListToDataTable(PFList<RandomName> rn)
        {
            DataTable dt = new DataTable();
            dt.TableName = "RandomNamesList";
            string colName = string.Empty;

            if (rn.Count == 0)
                return dt;

            RandomName randName = rn[0];

            Type t = randName.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                if (prop.PropertyType.IsEnum)
                {
                    dt.Columns.Add(new DataColumn(prop.Name, Type.GetType("System.String")));
                }
                else
                {
                    dt.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
                }
                object val = prop.GetValue(randName, null);
            }

            foreach (RandomName nm in rn)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colName = dt.Columns[i].ColumnName;
                    t = nm.GetType();
                    props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                    for (int p = 0; p < props.Length; p++)
                    {
                        if (props[p].Name == colName)
                        {
                            if (props[p].PropertyType.IsEnum)
                            {
                                string enumString = props[p].GetValue(nm, null).ToString();
                                dr[colName] = enumString;
                            }
                            else
                            {
                                object val = props[p].GetValue(nm, null);
                                dr[colName] = val;
                            }
                        }
                    }
                }
                dt.Rows.Add(dr);
            }//end foreach

            return dt;
        }


    }//end class
}//end namespace
