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

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Class encapsulates all the information used to create a random person or business name.
    /// </summary>
    public class RandomName
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _dataSource = "RandomName";
        private int _nameRowNum = 0;
        private enNameType _nameType = enNameType.NotSpecified;
        private enCountry _country = enCountry.NotSpecified;
        private string _firstName = string.Empty;
        private string _middleInitial = string.Empty;
        private string _lastName = string.Empty;
        private enGender _gender = enGender.NotSpecified;
        private string _birthDate = DateTime.MaxValue.ToString("yyyy/MM/dd");

        private string _addressLine1 = string.Empty;
        private string _addressLine2 = string.Empty;

        private string _city = string.Empty;
        private string _neighborhood = string.Empty;
        private string _stateProvince = string.Empty;
        private string _zipPostalCode = string.Empty;
        private string _stateProvinceName = string.Empty;
        private string _regionName = string.Empty;
        private string _subRegionName = string.Empty;

        private string _areaCode = string.Empty;
        private string _telephoneNumber = string.Empty;
        private string _emailAddress = string.Empty;
        private string _nationalId = string.Empty;

        private string _countryCode = string.Empty;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomName()
        {
            ;
        }

        //properties

        /// <summary>
        /// Arbitrary value for identifying the source of the data.
        /// </summary>
        /// <remarks>Default value is RandomName.</remarks>
        public string DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }

        /// <summary>
        /// Number that identifies the row.
        /// </summary>
        public int NameRowNum
        {
            get
            {
                return _nameRowNum;
            }
            set
            {
                _nameRowNum = value;
            }
        }

        /// <summary>
        /// NameType Property.
        /// </summary>
        public enNameType NameType
        {
            get
            {
                return _nameType;
            }
            set
            {
                _nameType = value;
            }
        }

        /// <summary>
        /// Country Property.
        /// </summary>
        public enCountry Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }

        /// <summary>
        /// FirstName Property.
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        /// <summary>
        /// MiddleInitial Property.
        /// </summary>
        public string MiddleInitial
        {
            get
            {
                return _middleInitial;
            }
            set
            {
                _middleInitial = value;
            }
        }

        /// <summary>
        /// LastName Property.
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        /// <summary>
        /// Gender Property.
        /// </summary>
        public enGender Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// BirthDate Property.
        /// </summary>
        public string BirthDate
        {
            get
            {
                return _birthDate;
            }
            set
            {
                _birthDate = value;
            }
        }

        /// <summary>
        /// AddressLine1 Property.
        /// </summary>
        public string AddressLine1
        {
            get
            {
                return _addressLine1;
            }
            set
            {
                _addressLine1 = value;
            }
        }

        /// <summary>
        /// AddressLine2 Property.
        /// </summary>
        public string AddressLine2
        {
            get
            {
                return _addressLine2;
            }
            set
            {
                _addressLine2 = value;
            }
        }

        /// <summary>
        /// City Property.
        /// </summary>
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        /// <summary>
        /// Neighborhood property. (Only filled in for locations in Mexico.)
        /// </summary>
       
        public string Neighborhood
        {
            get
            {
                return _neighborhood;
            }
            set
            {
                _neighborhood = value;
            }
        }

        /// <summary>
        /// StateProvince Property.
        /// </summary>
        public string StateProvince
        {
            get
            {
                return _stateProvince;
            }
            set
            {
                _stateProvince = value;
            }
        }

        /// <summary>
        /// ZipPostalCode Property.
        /// </summary>
        public string ZipPostalCode
        {
            get
            {
                return _zipPostalCode;
            }
            set
            {
                _zipPostalCode = value;
            }
        }

        /// <summary>
        /// StateProvinceName property.
        /// </summary>
        public string StateProvinceName
        {
            get
            {
                return _stateProvinceName;
            }
            set
            {
                _stateProvinceName = value;
            }
        }

        /// <summary>
        /// RegionName Property.
        /// </summary>
        public string RegionName
        {
            get
            {
                return _regionName;
            }
            set
            {
                _regionName = value;
            }
        }

        /// <summary>
        /// SubRegionName Property.
        /// </summary>
        public string SubRegionName
        {
            get
            {
                return _subRegionName;
            }
            set
            {
                _subRegionName = value;
            }
        }

        /// <summary>
        /// AreaCode property.
        /// </summary>
        public string AreaCode
        {
            get
            {
                return _areaCode;
            }
            set
            {
                _areaCode = value;
            }
        }

        /// <summary>
        /// TelephoneNumber Property.
        /// </summary>
        public string TelephoneNumber
        {
            get
            {
                return _telephoneNumber;
            }
            set
            {
                _telephoneNumber = value;
            }
        }

        /// <summary>
        /// EmailAddress Property.
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
            }
        }

        /// <summary>
        /// NationalId Property.
        /// </summary>
        public string NationalId
        {
            get
            {
                return _nationalId;
            }
            set
            {
                _nationalId = value;
            }
        }

        /// <summary>
        /// CountryCode Property.
        /// </summary>
        public string CountryCode
        {
            get
            {
                return _countryCode;
            }
            set
            {
                _countryCode = value;
            }
        }


        //methods

        /// <summary>
        /// Method to obtain a property value via Reflection.
        /// </summary>
        /// <param name="propertyName">Name of property to be retrieved.</param>
        /// <returns>Object containing the value for the property.</returns>
        public object GetPropertyValue(string propertyName)
        {
            object retval = null;
            try
            {
                retval = this.GetType().GetProperty(propertyName).GetValue(this, null);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to get property ");
                _msg.Append(propertyName);
                _msg.Append(" for instance of ");
                _msg.Append(this.GetType().FullName);
                _msg.Append(".\r\n");
                _msg.Append(PFTextObjects.PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            return retval;
        }

        /// <summary>
        /// Method to set a property value via Reflection.
        /// </summary>
        /// <param name="propertyName">Name of property to be set.</param>
        /// <param name="propertyValue">Object containing the value to set the property to.</param>
        public void SetPropertyValue(string propertyName, object propertyValue)
        {
            try
            {
                Type typ = this.GetType();
                PropertyInfo prop = typ.GetProperty(propertyName);
                prop.SetValue(this, propertyValue, null);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to set property ");
                _msg.Append(propertyName);
                _msg.Append(" for instance of ");
                _msg.Append(this.GetType().FullName);
                _msg.Append(". Make sure property exists and datatype of value is correct.\r\n");
                _msg.Append(PFTextObjects.PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
        }

        
        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param personName="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomName));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param personName="filePath">Full path for the input file.</param>
        /// <returns>An instance of RandomName.</returns>
        public static RandomName LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomName));
            TextReader textReader = new StreamReader(filePath);
            RandomName objectInstance;
            objectInstance = (RandomName)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(RandomName));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param personName="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of RandomName.</returns>
        public static RandomName LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomName));
            StringReader strReader = new StringReader(xmlString);
            RandomName objectInstance;
            objectInstance = (RandomName)deserializer.Deserialize(strReader);
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
