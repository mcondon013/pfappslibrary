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

namespace pfDataExtractorCPObjects
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PFRandomOrdersDefinition
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variable for peoperties
        private string _outputDatabaseLocation = string.Empty;
        private string _outputDatabasePlatform = string.Empty;
        private string _outputDatabaseConnection = string.Empty;
        private string _accessOutputUsername = string.Empty;    //Used for Access only: if Access database requires logon, otherwise blank
        private string _accessOutputPassword = string.Empty;    //Used for Access only: if Access database has a password, otherwise blank
        private bool _replaceExistingTables = false;
        private bool _replaceExistingAccessFile = false;        //Used for Access only   
        private int _databaseOutputBatchSize = 1;               //Used for relational database output only
        private bool _generateSalesOrders = true;
        private bool _generatePurchaseOrders = true;
        private bool _generateInternetSalesTable = true;
        private bool _generateResellerSalesTable = true;

        private string _tableSchema = string.Empty;
        private string _salesOrderHeadersTableName = "TestSalesOrderHeader";
        private string _salesOrderDetailsTableName = "TestSalesOrderDetail";
        private string _purchaseOrderHeadersTableName = "TestPurchaseOrderHeader";
        private string _purchaseOrderDetailsTableName = "TestPurchaseOrderDetail";
        private string _dwInternetSalesTableName = "TestFactInternetSales";
        private string _dwResellerSalesTableName = "TestFactResellerSales";

        private string _earliestTransactionDate = "1/1/2000";
        private string _latestTransactionDate = "12/31/2014";
        private bool _includeWeekendDays = true;
        private string _minNumSalesOrdersPerDate = "2";
        private string _maxNumSalesOrdersPerDate = "5";
        private string _minNumPurchaseOrdersPerDate = "0";
        private string _maxNumPurchaseOrdersPerDate = "2";
        private string _minTimePerDate = "02:00:00";
        private string _maxTimePerDate = "19:59:59";

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFRandomOrdersDefinition()
        {

        }

        //properties

        /// <summary>
        /// OutputDatabaseLocation Property.
        /// </summary>
        public string OutputDatabaseLocation
        {
            get
            {
                return _outputDatabaseLocation;
            }
            set
            {
                _outputDatabaseLocation = value;
            }
        }

        /// <summary>
        /// OutputDatabasePlatform Property.
        /// </summary>
        public string OutputDatabasePlatform
        {
            get
            {
                return _outputDatabasePlatform;
            }
            set
            {
                _outputDatabasePlatform = value;
            }
        }

        /// <summary>
        /// OutputDatabaseConnection Property.
        /// </summary>
        public string OutputDatabaseConnection
        {
            get
            {
                return _outputDatabaseConnection;
            }
            set
            {
                _outputDatabaseConnection = value;
            }
        }

        /// <summary>
        /// AccessOutputUsername Property.
        /// </summary>
        /// <remarks>Used for Access only: if Access database requires logon, otherwise blank.</remarks>
        public string AccessOutputUsername
        {
            get
            {
                return _accessOutputUsername;
            }
            set
            {
                _accessOutputUsername = value;
            }
        }

        /// <summary>
        /// AccessOutputPassword Property.
        /// </summary>
        /// <remarks>Used for Access only: if Access database has a password, otherwise blank.</remarks>
        public string AccessOutputPassword
        {
            get
            {
                return _accessOutputPassword;
            }
            set
            {
                _accessOutputPassword = value;
            }
        }


        /// <summary>
        /// ReplaceExistingTables Property.
        /// </summary>
        public bool ReplaceExistingTables
        {
            get
            {
                return _replaceExistingTables;
            }
            set
            {
                _replaceExistingTables = value;
            }
        }

        /// <summary>
        /// ReplaceExistingAccessFile Property.
        /// </summary>
        /// <remarks>Used for Access output only.</remarks>
        public bool ReplaceExistingAccessFile
        {
            get
            {
                return _replaceExistingAccessFile;
            }
            set
            {
                _replaceExistingAccessFile = value;
            }
        }

        /// <summary>
        /// DatabaseOutputBatchSize Property.
        /// </summary>
        /// <remarks>Only used for relational database destinations.</remarks>
        public int DatabaseOutputBatchSize
        {
            get
            {
                return _databaseOutputBatchSize;
            }
            set
            {
                _databaseOutputBatchSize = value;
            }
        }

        /// <summary>
        /// GenerateSalesOrders Property.
        /// </summary>
        public bool GenerateSalesOrders
        {
            get
            {
                return _generateSalesOrders;
            }
            set
            {
                _generateSalesOrders = value;
            }
        }

        /// <summary>
        /// GeneratePurchaseOrders Property.
        /// </summary>
        public bool GeneratePurchaseOrders
        {
            get
            {
                return _generatePurchaseOrders;
            }
            set
            {
                _generatePurchaseOrders = value;
            }
        }

        /// <summary>
        /// GenerateInternetSalesTable Property.
        /// </summary>
        public bool GenerateInternetSalesTable
        {
            get
            {
                return _generateInternetSalesTable;
            }
            set
            {
                _generateInternetSalesTable = value;
            }
        }

        /// <summary>
        /// GenerateResellerSalesTable Property.
        /// </summary>
        public bool GenerateResellerSalesTable
        {
            get
            {
                return _generateResellerSalesTable;
            }
            set
            {
                _generateResellerSalesTable = value;
            }
        }

        /// <summary>
        /// TableSchema Property.
        /// </summary>
        /// <remarks>Optional schema name to use for output test table names.</remarks>
        public string TableSchema
        {
            get
            {
                return _tableSchema;
            }
            set
            {
                _tableSchema = value;
            }
        }

        /// <summary>
        /// SalesOrderHeadersTableName Property.
        /// </summary>
        public string SalesOrderHeadersTableName
        {
            get
            {
                return _salesOrderHeadersTableName;
            }
            set
            {
                _salesOrderHeadersTableName = value;
            }
        }

        /// <summary>
        /// SalesOrderDetailsTableName Property.
        /// </summary>
        public string SalesOrderDetailsTableName
        {
            get
            {
                return _salesOrderDetailsTableName;
            }
            set
            {
                _salesOrderDetailsTableName = value;
            }
        }

        /// <summary>
        /// PurchaseOrderHeadersTableName Property.
        /// </summary>
        public string PurchaseOrderHeadersTableName
        {
            get
            {
                return _purchaseOrderHeadersTableName;
            }
            set
            {
                _purchaseOrderHeadersTableName = value;
            }
        }

        /// <summary>
        /// PurchaseOrderDetailsTableName Property.
        /// </summary>
        public string PurchaseOrderDetailsTableName
        {
            get
            {
                return _purchaseOrderDetailsTableName;
            }
            set
            {
                _purchaseOrderDetailsTableName = value;
            }
        }

        /// <summary>
        /// DwInternetSalesTableName Property.
        /// </summary>
        public string DwInternetSalesTableName
        {
            get
            {
                return _dwInternetSalesTableName;
            }
            set
            {
                _dwInternetSalesTableName = value;
            }
        }

        /// <summary>
        /// DwResellerSalesTableName Property.
        /// </summary>
        public string DwResellerSalesTableName
        {
            get
            {
                return _dwResellerSalesTableName;
            }
            set
            {
                _dwResellerSalesTableName = value;
            }
        }

        /// <summary>
        /// EarliestTransactionDate Property.
        /// </summary>
        public string EarliestTransactionDate
        {
            get
            {
                return _earliestTransactionDate;
            }
            set
            {
                _earliestTransactionDate = value;
            }
        }

        /// <summary>
        /// LatestTransactionDate Property.
        /// </summary>
        public string LatestTransactionDate
        {
            get
            {
                return _latestTransactionDate;
            }
            set
            {
                _latestTransactionDate = value;
            }
        }

        /// <summary>
        /// IncludeWeekendDays Property.
        /// </summary>
        public bool IncludeWeekendDays
        {
            get
            {
                return _includeWeekendDays;
            }
            set
            {
                _includeWeekendDays = value;
            }
        }

        /// <summary>
        /// MinNumSalesOrdersPerDate Property.
        /// </summary>
        public string MinNumSalesOrdersPerDate
        {
            get
            {
                return _minNumSalesOrdersPerDate;
            }
            set
            {
                _minNumSalesOrdersPerDate = value;
            }
        }

        /// <summary>
        /// MaxNumSalesOrdersPerDate Property.
        /// </summary>
        public string MaxNumSalesOrdersPerDate
        {
            get
            {
                return _maxNumSalesOrdersPerDate;
            }
            set
            {
                _maxNumSalesOrdersPerDate = value;
            }
        }

        /// <summary>
        /// MinNumPurchaseOrdersPerDate Property.
        /// </summary>
        public string MinNumPurchaseOrdersPerDate
        {
            get
            {
                return _minNumPurchaseOrdersPerDate;
            }
            set
            {
                _minNumPurchaseOrdersPerDate = value;
            }
        }

        /// <summary>
        /// MaxNumPurchaseOrdersPerDate Property.
        /// </summary>
        public string MaxNumPurchaseOrdersPerDate
        {
            get
            {
                return _maxNumPurchaseOrdersPerDate;
            }
            set
            {
                _maxNumPurchaseOrdersPerDate = value;
            }
        }

        /// <summary>
        /// MinTimePerDate Property.
        /// </summary>
        public string MinTimePerDate
        {
            get
            {
                return _minTimePerDate;
            }
            set
            {
                _minTimePerDate = value;
            }
        }

        /// <summary>
        /// MaxTimePerDate Property.
        /// </summary>
        public string MaxTimePerDate
        {
            get
            {
                return _maxTimePerDate;
            }
            set
            {
                _maxTimePerDate = value;
            }
        }


        //methods

        //class helpers

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFRandomOrdersDefinition));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFRandomOrdersDefinition.</returns>
        public static PFRandomOrdersDefinition LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFRandomOrdersDefinition));
            TextReader textReader = new StreamReader(filePath);
            PFRandomOrdersDefinition columnDefinitions;
            columnDefinitions = (PFRandomOrdersDefinition)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from an XML formatted string.
        /// </summary>
        /// <param name="xmlString">String containing formatted XML version of the object.</param>
        /// <returns>An instance of PFRandomOrdersDefinition.</returns>
        public static PFRandomOrdersDefinition LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFRandomOrdersDefinition));
            StringReader strReader = new StringReader(xmlString);
            PFRandomOrdersDefinition fieldDefinitions = (PFRandomOrdersDefinition)deserializer.Deserialize(strReader);
            strReader.Close();
            return fieldDefinitions;
        }

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
            XmlSerializer ser = new XmlSerializer(typeof(PFRandomOrdersDefinition));
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

        /// <summary>
        /// Makes a deep copy of the current instance to a new instance.
        /// </summary>
        /// <returns>New instance of PFExtractorOutputOptions object.</returns>
        public PFRandomOrdersDefinition Copy()
        {
            string xmlString = this.ToXmlString();
            PFRandomOrdersDefinition newInstance = PFRandomOrdersDefinition.LoadFromXmlString(xmlString);
            return newInstance;
        }


    }//end class
}//end namespace
