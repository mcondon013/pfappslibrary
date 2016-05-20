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
    /// Contains definition for creating a random string value.
    /// </summary>
    public class RandomStringDataRequest
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _name = string.Empty;
        private bool _outputRandomStrings = true;
        private bool _outputAN = true;
        private bool _outputANUC = false;
        private bool _outputANLC = false;
        private bool _outputANX = false;
        private bool _outputAL = false;
        private bool _outputLC = false;
        private bool _outputUC = false;
        private bool _outputDEC = false;
        private bool _outputHEX = false;
        private string _minNumStrings = string.Empty;
        private string _maxNumStrings = string.Empty;
        private string _stringMinimumLength = string.Empty;
        private string _stringMaximumLength = string.Empty;
        private string _regexPattern = string.Empty;
        private string _regexReplacement = string.Empty;
        private bool _outputRandomSyllableStrings = false;
        private bool _outputSyllableUCLC = false;
        private bool _outputSyllableLC = true;
        private bool _outputSyllableUC = false;
        private string _minNumSyllableStrings = string.Empty;
        private string _maxNumSyllableStrings = string.Empty;
        private string _syllableStringMinimumLength = string.Empty;
        private string _syllableStringMaximumLength = string.Empty;
        private bool _outputRepeatingStrings = false;
        private bool _outputRepeatRandomCharacter = false;
        private bool _outputRepeatAN = true;
        private bool _outputRepeatANUC = false;
        private bool _outputRepeatANLC = false;
        private bool _outputRepeatANX = false;
        private bool _outputRepeatAL = false;
        private bool _outputRepeatLC = false;
        private bool _outputRepeatUC = false;
        private bool _outputRepeatDEC = false;
        private bool _outputRepeatHEX = false;
        private string _minRepeatOutputLength = string.Empty;
        private string _maxRepeatOutputLength = string.Empty;
        private bool _outputRepeatText = false;
        private string _textToRepeat = string.Empty;
        private string _minNumRepeats = string.Empty;
        private string _maxNumRepeats = string.Empty;
        private int _numRandomDataItems = 1000;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomStringDataRequest()
        {
            ;
        }

        //properties

        /// <summary>
        /// Name Property.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// OutputRandomStrings Property.
        /// </summary>
        public bool OutputRandomStrings
        {
            get
            {
                return _outputRandomStrings;
            }
            set
            {
                _outputRandomStrings = value;
            }
        }

        /// <summary>
        /// OutputAN Property.
        /// </summary>
        public bool OutputAN
        {
            get
            {
                return _outputAN;
            }
            set
            {
                _outputAN = value;
            }
        }

        /// <summary>
        /// OutputANUC Property.
        /// </summary>
        public bool OutputANUC
        {
            get
            {
                return _outputANUC;
            }
            set
            {
                _outputANUC = value;
            }
        }

        /// <summary>
        /// OutputANLC Property.
        /// </summary>
        public bool OutputANLC
        {
            get
            {
                return _outputANLC;
            }
            set
            {
                _outputANLC = value;
            }
        }

        /// <summary>
        /// OutputANX Property.
        /// </summary>
        public bool OutputANX
        {
            get
            {
                return _outputANX;
            }
            set
            {
                _outputANX = value;
            }
        }

        /// <summary>
        /// OutputAL Property.
        /// </summary>
        public bool OutputAL
        {
            get
            {
                return _outputAL;
            }
            set
            {
                _outputAL = value;
            }
        }

        /// <summary>
        /// OutputLC Property.
        /// </summary>
        public bool OutputLC
        {
            get
            {
                return _outputLC;
            }
            set
            {
                _outputLC = value;
            }
        }

        /// <summary>
        /// OutputUC Property.
        /// </summary>
        public bool OutputUC
        {
            get
            {
                return _outputUC;
            }
            set
            {
                _outputUC = value;
            }
        }

        /// <summary>
        /// OutputDEC Property.
        /// </summary>
        public bool OutputDEC
        {
            get
            {
                return _outputDEC;
            }
            set
            {
                _outputDEC = value;
            }
        }

        /// <summary>
        /// OutputHEX Property.
        /// </summary>
        public bool OutputHEX
        {
            get
            {
                return _outputHEX;
            }
            set
            {
                _outputHEX = value;
            }
        }

        /// <summary>
        /// MinNumStrings Property.
        /// </summary>
        public string MinNumStrings
        {
            get
            {
                return _minNumStrings;
            }
            set
            {
                _minNumStrings = value;
            }
        }

        /// <summary>
        /// MaxNumStrings Property.
        /// </summary>
        public string MaxNumStrings
        {
            get
            {
                return _maxNumStrings;
            }
            set
            {
                _maxNumStrings = value;
            }
        }

        /// <summary>
        /// StringMinimumLength Property.
        /// </summary>
        public string StringMinimumLength
        {
            get
            {
                return _stringMinimumLength;
            }
            set
            {
                _stringMinimumLength = value;
            }
        }

        /// <summary>
        /// StringMaximumLength Property.
        /// </summary>
        public string StringMaximumLength
        {
            get
            {
                return _stringMaximumLength;
            }
            set
            {
                _stringMaximumLength = value;
            }
        }

        /// <summary>
        /// RegexPattern Property.
        /// </summary>
        public string RegexPattern
        {
            get
            {
                return _regexPattern;
            }
            set
            {
                _regexPattern = value;
            }
        }

        /// <summary>
        /// RegexReplacement Property.
        /// </summary>
        public string RegexReplacement
        {
            get
            {
                return _regexReplacement;
            }
            set
            {
                _regexReplacement = value;
            }
        }

        /// <summary>
        /// OutputRandomSyllableStrings Property.
        /// </summary>
        public bool OutputRandomSyllableStrings
        {
            get
            {
                return _outputRandomSyllableStrings;
            }
            set
            {
                _outputRandomSyllableStrings = value;
            }
        }

        /// <summary>
        /// OutputSyllableUCLC Property.
        /// </summary>
        public bool OutputSyllableUCLC
        {
            get
            {
                return _outputSyllableUCLC;
            }
            set
            {
                _outputSyllableUCLC = value;
            }
        }

        /// <summary>
        /// OutputSyllableLC Property.
        /// </summary>
        public bool OutputSyllableLC
        {
            get
            {
                return _outputSyllableLC;
            }
            set
            {
                _outputSyllableLC = value;
            }
        }

        /// <summary>
        /// OutputSyllableUC Property.
        /// </summary>
        public bool OutputSyllableUC
        {
            get
            {
                return _outputSyllableUC;
            }
            set
            {
                _outputSyllableUC = value;
            }
        }

        /// <summary>
        /// MinNumSyllableStrings Property.
        /// </summary>
        public string MinNumSyllableStrings
        {
            get
            {
                return _minNumSyllableStrings;
            }
            set
            {
                _minNumSyllableStrings = value;
            }
        }

        /// <summary>
        /// MaxNumSyllableStrings Property.
        /// </summary>
        public string MaxNumSyllableStrings
        {
            get
            {
                return _maxNumSyllableStrings;
            }
            set
            {
                _maxNumSyllableStrings = value;
            }
        }

        /// <summary>
        /// SyllableStringMinimumLength Property.
        /// </summary>
        public string SyllableStringMinimumLength
        {
            get
            {
                return _syllableStringMinimumLength;
            }
            set
            {
                _syllableStringMinimumLength = value;
            }
        }

        /// <summary>
        /// SyllableStringMaximumLength Property.
        /// </summary>
        public string SyllableStringMaximumLength
        {
            get
            {
                return _syllableStringMaximumLength;
            }
            set
            {
                _syllableStringMaximumLength = value;
            }
        }

        /// <summary>
        /// OutputRepeatingStrings Property.
        /// </summary>
        public bool OutputRepeatingStrings
        {
            get
            {
                return _outputRepeatingStrings;
            }
            set
            {
                _outputRepeatingStrings = value;
            }
        }

        /// <summary>
        /// OutputRepeatRandomCharacter Property.
        /// </summary>
        public bool OutputRepeatRandomCharacter
        {
            get
            {
                return _outputRepeatRandomCharacter;
            }
            set
            {
                _outputRepeatRandomCharacter = value;
            }
        }

        /// <summary>
        /// OutputRepeatAN Property.
        /// </summary>
        public bool OutputRepeatAN
        {
            get
            {
                return _outputRepeatAN;
            }
            set
            {
                _outputRepeatAN = value;
            }
        }

        /// <summary>
        /// OutputRepeatANUC Property.
        /// </summary>
        public bool OutputRepeatANUC
        {
            get
            {
                return _outputRepeatANUC;
            }
            set
            {
                _outputRepeatANUC = value;
            }
        }

        /// <summary>
        /// OutputRepeatANLC Property.
        /// </summary>
        public bool OutputRepeatANLC
        {
            get
            {
                return _outputRepeatANLC;
            }
            set
            {
                _outputRepeatANLC = value;
            }
        }

        /// <summary>
        /// OutputRepeatANX Property.
        /// </summary>
        public bool OutputRepeatANX
        {
            get
            {
                return _outputRepeatANX;
            }
            set
            {
                _outputRepeatANX = value;
            }
        }

        /// <summary>
        /// OutputRepeatAL Property.
        /// </summary>
        public bool OutputRepeatAL
        {
            get
            {
                return _outputRepeatAL;
            }
            set
            {
                _outputRepeatAL = value;
            }
        }

        /// <summary>
        /// OutputRepeatLC Property.
        /// </summary>
        public bool OutputRepeatLC
        {
            get
            {
                return _outputRepeatLC;
            }
            set
            {
                _outputRepeatLC = value;
            }
        }

        /// <summary>
        /// OutputRepeatUC Property.
        /// </summary>
        public bool OutputRepeatUC
        {
            get
            {
                return _outputRepeatUC;
            }
            set
            {
                _outputRepeatUC = value;
            }
        }

        /// <summary>
        /// OutputRepeatDEC Property.
        /// </summary>
        public bool OutputRepeatDEC
        {
            get
            {
                return _outputRepeatDEC;
            }
            set
            {
                _outputRepeatDEC = value;
            }
        }

        /// <summary>
        /// OutputRepeatHEX Property.
        /// </summary>
        public bool OutputRepeatHEX
        {
            get
            {
                return _outputRepeatHEX;
            }
            set
            {
                _outputRepeatHEX = value;
            }
        }

        /// <summary>
        /// MinRepeatOutputLength Property.
        /// </summary>
        public string MinRepeatOutputLength
        {
            get
            {
                return _minRepeatOutputLength;
            }
            set
            {
                _minRepeatOutputLength = value;
            }
        }

        /// <summary>
        /// MaxRepeatOutputLength Property.
        /// </summary>
        public string MaxRepeatOutputLength
        {
            get
            {
                return _maxRepeatOutputLength;
            }
            set
            {
                _maxRepeatOutputLength = value;
            }
        }


        /// <summary>
        /// OutputRepeatText Property.
        /// </summary>
        public bool OutputRepeatText
        {
            get
            {
                return _outputRepeatText;
            }
            set
            {
                _outputRepeatText = value;
            }
        }

        /// <summary>
        /// TextToRepeat Property.
        /// </summary>
        public string TextToRepeat
        {
            get
            {
                return _textToRepeat;
            }
            set
            {
                _textToRepeat = value;
            }
        }

        /// <summary>
        /// MinNumRepeats Property.
        /// </summary>
        public string MinNumRepeats
        {
            get
            {
                return _minNumRepeats;
            }
            set
            {
                _minNumRepeats = value;
            }
        }

        /// <summary>
        /// MaxNumRepeats Property.
        /// </summary>
        public string MaxNumRepeats
        {
            get
            {
                return _maxNumRepeats;
            }
            set
            {
                _maxNumRepeats = value;
            }
        }

        /// <summary>
        /// NumRandomPreviewItems Property.
        /// </summary>
        public int NumRandomDataItems
        {
            get
            {
                return _numRandomDataItems;
            }
            set
            {
                _numRandomDataItems = value;
            }
        }


        //methods

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomStringDataRequest));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of RandomStringDataRequest.</returns>
        public static RandomStringDataRequest LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomStringDataRequest));
            TextReader textReader = new StreamReader(filePath);
            RandomStringDataRequest columnDefinitions;
            columnDefinitions = (RandomStringDataRequest)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
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
        /// Routine outputs name and value for all properties.
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
                object val = null;
                try
                {
                    val = prop.GetValue(this, null);
                }
                catch
                {
                    val = "Unable to retrieve value.";
                }

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
        /// Routine outputs name and value for all fields.
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
            XmlSerializer ser = new XmlSerializer(typeof(RandomStringDataRequest));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
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
