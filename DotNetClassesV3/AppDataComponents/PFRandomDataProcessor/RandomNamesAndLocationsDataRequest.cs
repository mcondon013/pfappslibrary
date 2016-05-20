//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
using PFCollectionsObjects;

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Class containing the selection criteria and data files used by random name generator.
    /// </summary>
    public class RandomNamesAndLocationsDataRequest
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _databaseFilePath = string.Empty;
        private string _databasePassword = string.Empty;
        private string _randomDataXmlFilesFolder = string.Empty;

        //private string _listFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations";
        private string _listName = string.Empty;
        private bool _outputToXmlFile = true;
        private bool _outputToGrid = true;
        private int _numRandomNameItems = 1000;

        private bool _includeUSData = false;
        private bool _includeCanadaData = false;
        private bool _includeMexicoData = false;
        private bool _includePersonNames = false;
        private bool _includeBusinessNames = false;
        private bool _includeAddressLine1 = false;
        private bool _includeAddressLine2 = false;
        private bool _includeCityLocation = false;
        private bool _includeAreaCode = false;
        private bool _includeNationalId = false;
        private bool _includeTelephoneNumber = false;
        private bool _includeEmailAddress = false;
        private bool _includeGenderForPersons = false;
        private bool _includePersonBirthDate = false;
        private DateTime _baseDateTime = DateTime.Now;

        private int _personNamesPercentFrequency = 100;
        private int _businessNamesPercentFrequency = 0;
        private int _malePersonNamePercentFrequency = 50;
        private int _femalePersonNamePercentFrequency = 50;
        private int _addressLine2PercentFrequency = 50;

        private int _percentFrequencyUnitedStates = 75;
        private int _percentFrequencyCanada = 10;
        private int _percentFrequencyMexico = 15;

        private PFList<string> _personAgeGroups = new PFList<string>();

        private PFList<LocationSelectionCriteria> _locationSelectionCriteriaLists = new PFList<LocationSelectionCriteria>();


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomNamesAndLocationsDataRequest()
        {
            ;
        }


        //properties

        /// <summary>
        /// Path to SQLCE database file containing data used in random name generation.
        /// </summary>
        public string DatabaseFilePath
        {
            get
            {
                return _databaseFilePath;
            }
            set
            {
                _databaseFilePath = value;
            }
        }

        /// <summary>
        /// DatabasePassword Property.
        /// </summary>
        public string DatabasePassword
        {
            get
            {
                return _databasePassword;
            }
            set
            {
                _databasePassword = value;
            }
        }

        /// <summary>
        /// Identifies folder containing XML files used in random data generation.
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

        ///// <summary>
        ///// ListFolder Property.
        ///// </summary>
        //public string ListFolder
        //{
        //    get
        //    {
        //        return _listFolder;
        //    }
        //    set
        //    {
        //        _listFolder = value;
        //    }
        //}

        /// <summary>
        /// ListName Property.
        /// </summary>
        public string ListName
        {
            get
            {
                return _listName;
            }
            set
            {
                _listName = value;
            }
        }

        /// <summary>
        /// OutputToXmlFile Property.
        /// </summary>
        public bool OutputToXmlFile
        {
            get
            {
                return _outputToXmlFile;
            }
            set
            {
                _outputToXmlFile = value;
            }
        }

        /// <summary>
        /// OutputToGrid Property.
        /// </summary>
        public bool OutputToGrid
        {
            get
            {
                return _outputToGrid;
            }
            set
            {
                _outputToGrid = value;
            }
        }

        /// <summary>
        /// NumRandomNameItems Property.
        /// </summary>
        public int NumRandomNameItems
        {
            get
            {
                return _numRandomNameItems;
            }
            set
            {
                _numRandomNameItems = value;
            }
        }

        /// <summary>
        /// IncludeUSData Property.
        /// </summary>
        public bool IncludeUSData
        {
            get
            {
                return _includeUSData;
            }
            set
            {
                _includeUSData = value;
            }
        }

        /// <summary>
        /// IncludeCanadaData Property.
        /// </summary>
        public bool IncludeCanadaData
        {
            get
            {
                return _includeCanadaData;
            }
            set
            {
                _includeCanadaData = value;
            }
        }

        /// <summary>
        /// IncludeMexicoData Property.
        /// </summary>
        public bool IncludeMexicoData
        {
            get
            {
                return _includeMexicoData;
            }
            set
            {
                _includeMexicoData = value;
            }
        }
        /// <summary>
        /// IncludePersonNames Property.
        /// </summary>
        public bool IncludePersonNames
        {
            get
            {
                return _includePersonNames;
            }
            set
            {
                _includePersonNames = value;
            }
        }

        /// <summary>
        /// IncludeBusinessNames Property.
        /// </summary>
        public bool IncludeBusinessNames
        {
            get
            {
                return _includeBusinessNames;
            }
            set
            {
                _includeBusinessNames = value;
            }
        }

        /// <summary>
        /// IncludeAddressLine1 Property.
        /// </summary>
        public bool IncludeAddressLine1
        {
            get
            {
                return _includeAddressLine1;
            }
            set
            {
                _includeAddressLine1 = value;
            }
        }

        /// <summary>
        /// IncludeAddressLine2 Property.
        /// </summary>
        public bool IncludeAddressLine2
        {
            get
            {
                return _includeAddressLine2;
            }
            set
            {
                _includeAddressLine2 = value;
            }
        }

        /// <summary>
        /// IncludeCityLocation Property.
        /// </summary>
        public bool IncludeCityLocation
        {
            get
            {
                return _includeCityLocation;
            }
            set
            {
                _includeCityLocation = value;
            }
        }

        /// <summary>
        /// IncludeAreaCode Property.
        /// </summary>
        public bool IncludeAreaCode
        {
            get
            {
                return _includeAreaCode;
            }
            set
            {
                _includeAreaCode = value;
            }
        }

        /// <summary>
        /// IncludeNationalId Property.
        /// </summary>
        public bool IncludeNationalId
        {
            get
            {
                return _includeNationalId;
            }
            set
            {
                _includeNationalId = value;
            }
        }

        /// <summary>
        /// IncludeTelephoneNumber Property.
        /// </summary>
        public bool IncludeTelephoneNumber
        {
            get
            {
                return _includeTelephoneNumber;
            }
            set
            {
                _includeTelephoneNumber = value;
            }
        }

        /// <summary>
        /// IncludeEmailAddress Property.
        /// </summary>
        public bool IncludeEmailAddress
        {
            get
            {
                return _includeEmailAddress;
            }
            set
            {
                _includeEmailAddress = value;
            }
        }

        /// <summary>
        /// IncludeGenderForPersons Property.
        /// </summary>
        public bool IncludeGenderForPersons
        {
            get
            {
                return _includeGenderForPersons;
            }
            set
            {
                _includeGenderForPersons = value;
            }
        }

        /// <summary>
        /// IncludePersonBirthDate Property.
        /// </summary>
        public bool IncludePersonBirthDate
        {
            get
            {
                return _includePersonBirthDate;
            }
            set
            {
                _includePersonBirthDate = value;
            }
        }

        /// <summary>
        /// BaseDateTime Property. Date/time used for calculating birthdates based on this date and the age group.
        /// </summary>
        public DateTime BaseDateTime
        {
            get
            {
                return _baseDateTime;
            }
            set
            {
                _baseDateTime = value;
            }
        }

        /// <summary>
        /// PersonNamesPercentFrequency Property.
        /// </summary>
        public int PersonNamesPercentFrequency
        {
            get
            {
                return _personNamesPercentFrequency;
            }
            set
            {
                _personNamesPercentFrequency = value;
            }
        }

        /// <summary>
        /// BusinessNamesPercentFrequency Property.
        /// </summary>
        public int BusinessNamesPercentFrequency
        {
            get
            {
                return _businessNamesPercentFrequency;
            }
            set
            {
                _businessNamesPercentFrequency = value;
            }
        }

        /// <summary>
        /// MalePersonNamePercentFrequency Property.
        /// </summary>
        public int MalePersonNamePercentFrequency
        {
            get
            {
                return _malePersonNamePercentFrequency;
            }
            set
            {
                _malePersonNamePercentFrequency = value;
            }
        }

        /// <summary>
        /// FemalePersonNamePercentFrequency Property.
        /// </summary>
        public int FemalePersonNamePercentFrequency
        {
            get
            {
                return _femalePersonNamePercentFrequency;
            }
            set
            {
                _femalePersonNamePercentFrequency = value;
            }
        }

        /// <summary>
        /// Specifies percentage of names that will have an address line 2 defined for them.
        /// </summary>
        public int AddressLine2PercentFrequency
        {
            get
            {
                return _addressLine2PercentFrequency;
            }
            set
            {
                _addressLine2PercentFrequency = value;
            }
        }


        /// <summary>
        /// PercentFrequencyUnitedStates Property.
        /// </summary>
        public int PercentFrequencyUnitedStates
        {
            get
            {
                return _percentFrequencyUnitedStates;
            }
            set
            {
                _percentFrequencyUnitedStates = value;
            }
        }

        /// <summary>
        /// PercentFrequencyCanada Property.
        /// </summary>
        public int PercentFrequencyCanada
        {
            get
            {
                return _percentFrequencyCanada;
            }
            set
            {
                _percentFrequencyCanada = value;
            }
        }

        /// <summary>
        /// PercentFrequencyMexico Property.
        /// </summary>
        public int PercentFrequencyMexico
        {
            get
            {
                return _percentFrequencyMexico;
            }
            set
            {
                _percentFrequencyMexico = value;
            }
        }

        /// <summary>
        /// PersonAgeGroups Property.
        /// </summary>
        public PFList<string> PersonAgeGroups
        {
            get
            {
                return _personAgeGroups;
            }
            set
            {
                _personAgeGroups = value;
            }
        }

        /// <summary>
        /// LocationSelectionCriteriaLists property. Contains lists of values that determine which locations to use when creating a randomized location.
        /// </summary>
        public PFList<LocationSelectionCriteria> LocationSelectionCriteriaLists
        {
            get
            {
                return _locationSelectionCriteriaLists;
            }
            set
            {
                _locationSelectionCriteriaLists = value;
            }
        }





        //methods


        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param personName="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomNamesAndLocationsDataRequest));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param personName="filePath">Full path for the input file.</param>
        /// <returns>An instance of RandomNamesAndLocationsDataRequest.</returns>
        public static RandomNamesAndLocationsDataRequest LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomNamesAndLocationsDataRequest));
            TextReader textReader = new StreamReader(filePath);
            RandomNamesAndLocationsDataRequest objectInstance;
            objectInstance = (RandomNamesAndLocationsDataRequest)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Routine overrides default ToString method and outputs personName, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs personName and value for all properties.
        /// </summary>
        /// <returns>String value.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = prop.GetValue(this, null);

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs personName and value for all fields.
        /// </summary>
        /// <returns>String value.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomNamesAndLocationsDataRequest));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param personName="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of RandomNamesAndLocationsDataRequest.</returns>
        public static RandomNamesAndLocationsDataRequest LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomNamesAndLocationsDataRequest));
            StringReader strReader = new StringReader(xmlString);
            RandomNamesAndLocationsDataRequest objectInstance;
            objectInstance = (RandomNamesAndLocationsDataRequest)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
