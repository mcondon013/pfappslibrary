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
using System.Data;
using System.Windows.Forms;
using PFDocumentObjects;
using PFDocumentGlobals;
using PFDataOutputProcessor;
using PFDataAccessObjects;
using PFAppUtils;
using AppGlobals;
using PFCollectionsObjects;

namespace PFDataOutputGrid
{
    /// <summary>
    /// Class containing routines to output the contents of the data grid to various formats.
    /// </summary>
    public class DataOutputGridExporter
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataOutputGridExporter()
        {
            ;
        }

        //properties


        //methods

        /// <summary>
        /// Converts the cells in a DataGridView grid into a data table.
        /// </summary>
        /// <param name="dgv">DataGridView object that will have its data copied to a data table.</param>
        /// <param name="IgnoreHiddenColumns">If true, hidden columns on the grid will be excluded from the data table.</param>
        /// <returns>DataTable containing data retrieved from DataGridView represented by dgv property.</returns>
        /// <remarks>This routine is useful for exporting only visible columns from the grid.</remarks>
        public DataTable GetGridContentAsDataTable(DataGridView dgv, bool IgnoreHiddenColumns = false)
        {
            DataTable dtSource = null;
            int numRowsCopied = 0;
            try
            {
                if (dgv.ColumnCount == 0) return null;
                dtSource = new DataTable();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (IgnoreHiddenColumns & !col.Visible) continue;
                    if (col.Name == string.Empty) continue;
                    if (col.ValueType == null) continue;
                    dtSource.Columns.Add(col.Name, col.ValueType);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                if (dtSource.Columns.Count == 0) return null;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                    numRowsCopied++;
                }
            }
            catch (OutOfMemoryException)
            {
                dtSource = null;
                MessageBox.Show("Out of memory in GetGridContentAsDataTable. Application will terminate now.", "Out of Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            catch (System.Exception ex)
            {
                dtSource = null;
                _msg.Length = 0;
                _msg.Append("Attempt to copy grid contents to a data table failed.\r\n");
                _msg.Append("Number of rows copied before failure: ");
                _msg.Append(numRowsCopied.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
                dtSource = null;
            }
            return dtSource;
        }


        /// <summary>
        /// Converts the cells in a DataGridView grid into a list of xml files containing data tables. List is used for output to conserve memory.
        /// </summary>
        /// <param name="dgv">DataGridView object that will have its data copied to a data table.</param>
        /// <param name="tableName">Table name to use when saving data table data to XML.</param>
        /// <param name="IgnoreHiddenColumns">If true, hidden columns on the grid will be excluded from the data table.</param>
        /// <param name="maxRowsForTempFile">Specifies the maximum number of rows to include in each temporary file. For example, 
        /// if datatable contains 100,000 rows and maxRowsForTempFile equals 50000 then two temp files, each containing 50,000 rows will be created.</param>
        /// <returns>List of XML file names containing XML data retrieved from DataGridView represented by dgv property.</returns>
        /// <remarks>This routine is useful for exporting only visible columns from the grid.</remarks>
        public PFList<string> GetGridContentAsDataTableList(DataGridView dgv, string tableName, bool IgnoreHiddenColumns = false, int maxRowsForTempFile = 50000)
        {
            PFList<string> dtList = new PFList<string>();
            DataTable dtSource = null;
            string dgvFilter = string.Empty;
            int totNumRowsCopied = 0;
            bool gridContainsDgvRowNumber = false;
            bool gridContainsNameRowNum = false;
            //int numRowsCopied = 0;
            try
            {
                if (dgv.ColumnCount == 0) return null;
                dtSource = new DataTable();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Name == "dgvRowNumber")
                        gridContainsDgvRowNumber = true;
                    if (col.Name == "NameRowNum")
                        gridContainsNameRowNum = true;
                    if (IgnoreHiddenColumns & !col.Visible) continue;       //ignore hidden columns
                    if (col.Name == string.Empty) continue;                 //cols without names are ignored
                    if (col.ValueType == null) continue;                    //ignore columns with null value type
                    dtSource.Columns.Add(col.Name, col.ValueType);
                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                if (dtSource.Columns.Count == 0) return null;

                System.Windows.Forms.BindingSource bsrc = (System.Windows.Forms.BindingSource)dgv.DataSource;
                //bsrc.Filter = @"1=1  AND (dgvRowNumber <=200000) AND (ProductKey <=350000)";
                //bsrc.Filter = @"(dgvRowNumber >=1) AND (dgvRowNumber <=200000)";
                Console.WriteLine(bsrc.Filter); 
                DataSet ds = (System.Data.DataSet)bsrc.DataSource;
                DataTable dt = ds.Tables[0];
                DataView dv = new DataView(dt);


                int maxTempFiles = (dt.Rows.Count / maxRowsForTempFile) + 1;


                for (int t = 0; t < maxTempFiles; t++)
                {
                    int minValue = (t * maxRowsForTempFile) + 1;
                    int maxValue = (minValue + maxRowsForTempFile) - 1;
                    string filter = string.Empty;
                    if(gridContainsDgvRowNumber)
                        filter = "(dgvRowNumber >= " + minValue.ToString() + ") AND (dgvRowNumber <= " + maxValue.ToString() + ")";
                    else if (gridContainsNameRowNum)
                        filter = "(NameRowNum >= " + minValue.ToString() + ") AND (NameRowNum <= " + maxValue.ToString() + ")";
                    else
                        filter = string.Empty;
                    if (bsrc.Filter != null)
                    {
                        dgvFilter = bsrc.Filter;
                    }
                    else
                    {
                        dgvFilter = string.Empty;
                    }

                    if (dgvFilter.Trim().Length == 0 && filter.Trim().Length > 0)
                        dv.RowFilter = filter;
                    else if (dgvFilter.Trim().Length > 0 && filter.Trim().Length > 0)
                        dv.RowFilter = filter + " AND " + dgvFilter;
                    else if (dgvFilter.Trim().Length > 0 && filter.Trim().Length == 0)
                        dv.RowFilter = dgvFilter;
                    else
                        dv.RowFilter = string.Empty;

                    for (int r = 0; r < dv.Count; r++)
                    {
                        DataRow drNewRow = dtSource.NewRow();
                        foreach (DataColumn col in dtSource.Columns)
                        {
                            drNewRow[col.ColumnName] = dv[r][col.ColumnName];
                        }
                        dtSource.Rows.Add(drNewRow);
                        drNewRow = null;
                    }

                    if (dv.Count > 0)
                    {
                        SaveDataTableToList(dtList, dtSource, tableName);
                    }
                    dtSource.Rows.Clear();
                    dtSource.Dispose();
                    dtSource = null;
                    dtSource = new DataTable();
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (IgnoreHiddenColumns & !col.Visible) continue;
                        if (col.Name == string.Empty) continue;
                        if (col.ValueType == null) continue;
                        dtSource.Columns.Add(col.Name, col.ValueType);
                        dtSource.Columns[col.Name].Caption = col.HeaderText;
                    }

                }
                
                dv.RowFilter = string.Empty;
                dv.Dispose();
                dv = null;
            
            }
            
            catch (OutOfMemoryException)
            {
                dtSource = null;
                MessageBox.Show("Out of memory in GetGridContentAsDataTableExt. Application will terminate now.", "Out of Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            catch (System.Exception ex)
            {
                dtSource = null;
                _msg.Length = 0;
                _msg.Append("Attempt to copy grid contents to a list of data tables failed.\r\n");
                _msg.Append("Number of rows copied before failure: ");
                _msg.Append(totNumRowsCopied.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
                dtList = null;
            }
            //AppMessages.DisplayAlertMessage("All done!");
            
            return dtList;
        }

        private void SaveDataTableToList(PFList<string> dtList, DataTable dtSource, string tableName)
        {
            string tempFolder = Path.GetTempPath();
            string tempFileName = Guid.NewGuid().ToString().ToUpper() + ".pfdgv";
            string fileName = System.IO.Path.GetFileNameWithoutExtension(tempFileName);
            string tempFullPath = Path.Combine(tempFolder, tempFileName);

            dtSource.TableName = tableName;
            dtSource.WriteXml(tempFullPath, XmlWriteMode.WriteSchema);

            dtList.Add(tempFullPath);
        }

        /// <summary>
        /// Routine to export the contents of a DataGridView object to an Excel file.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="excelFormat">Type of Excel file to generate: XLSX, XLS or CSV.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        public void ExportToExcelFile(DataTable dt, enExcelOutputFormat excelFormat, string documentFilePath, bool replaceExistingFile)
        {
            string sheetName = "GridOutput";
            if (String.IsNullOrEmpty(dt.TableName) == false)
                sheetName = dt.TableName;

            enExcelVersion outputVersion = enExcelVersion.NotSpecified;
            switch (excelFormat)
            {
                case enExcelOutputFormat.Excel2007:
                    outputVersion = enExcelVersion.Excel2007;
                    break;
                case enExcelOutputFormat.Excel2003:
                    outputVersion = enExcelVersion.Excel2003;
                    break;
                case enExcelOutputFormat.CSV:
                    outputVersion = enExcelVersion.CSV;
                    break;
                default:
                    outputVersion = enExcelVersion.CSV;
                    break;
            }
            ExcelDocumentFileOutputProcessor excelDoc = new ExcelDocumentFileOutputProcessor(outputVersion, documentFilePath, replaceExistingFile, sheetName);
            excelDoc.WriteDataToOutput(dt);
        }

        /// <summary>
        /// Routine to export the contents of a DataGridView object to an Excel file.
        /// </summary>
        /// <param name="dtList">List of temp file names pointing to XML files containing data table data to be output.</param>
        /// <param name="excelFormat">Type of Excel file to generate: XLSX, XLS or CSV.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        public void ExportToExcelFile(PFList<string> dtList, enExcelOutputFormat excelFormat, string documentFilePath, bool replaceExistingFile)
        {
            string sheetName = "GridOutput";
            if (String.IsNullOrEmpty(documentFilePath) == false)
                sheetName = Path.GetFileNameWithoutExtension(documentFilePath);

            enExcelVersion outputVersion = enExcelVersion.NotSpecified;
            switch (excelFormat)
            {
                case enExcelOutputFormat.Excel2007:
                    outputVersion = enExcelVersion.Excel2007;
                    break;
                case enExcelOutputFormat.Excel2003:
                    outputVersion = enExcelVersion.Excel2003;
                    break;
                case enExcelOutputFormat.CSV:
                    outputVersion = enExcelVersion.CSV;
                    break;
                default:
                    outputVersion = enExcelVersion.CSV;
                    break;
            }
            ExcelDocumentFileOutputProcessor excelDoc = new ExcelDocumentFileOutputProcessor(outputVersion, documentFilePath, replaceExistingFile, sheetName);
            excelDoc.WriteDataToOutput(dtList);
        }

        /// <summary>
        /// Routine to output data from grid to a text file that has a delimited line format.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="columnDelimiter">Character that will mark the end of a column of data.</param>
        /// <param name="lineTerminator">Character(s) that will make the end of a line of data.</param>
        /// <param name="includeColumnNamesInOutput">If true, column headers are output at the beginning of the text. If false, column headers are excluded from the output.</param>
        public void ExportToDelimitedTextFile(DataTable dt, string documentFilePath, bool replaceExistingFile, string columnDelimiter, string lineTerminator, bool includeColumnNamesInOutput)
        {
            DelimitedTextFileOutputProcessor textOut = new DelimitedTextFileOutputProcessor(documentFilePath, replaceExistingFile, columnDelimiter, includeColumnNamesInOutput, lineTerminator);
            textOut.WriteDataToOutput(dt);
        }

        /// <summary>
        /// Routine to output data from grid to a text file that has a delimited line format.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="columnDelimiter">Character that will mark the end of a column of data.</param>
        /// <param name="lineTerminator">Character(s) that will make the end of a line of data.</param>
        /// <param name="includeColumnNamesInOutput">If true, column headers are output at the beginning of the text. If false, column headers are excluded from the output.</param>
        public void ExportToDelimitedTextFile(PFList<string> dtList, string documentFilePath, bool replaceExistingFile, string columnDelimiter, string lineTerminator, bool includeColumnNamesInOutput)
        {
            DelimitedTextFileOutputProcessor textOut = new DelimitedTextFileOutputProcessor(documentFilePath, replaceExistingFile, columnDelimiter, includeColumnNamesInOutput, lineTerminator);
            textOut.WriteDataToOutput(dtList);
        }

        /// <summary>
        /// Routine to output data from grid to a text file that has a fixed length line format.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="columnNamesOnFirstLine">If true, column headers are output at the beginning of the text. If false, column headers are excluded from the output.</param>
        /// <param name="allowDataTruncation">Set to True if you with to allow data to be truncated if the data is longer than the target column's fixed width.</param>
        /// <param name="useLineTerminator">Set to true if you wish to have a line terminator placed at the end of each data row.</param>
        public void ExportToFixedLengthTextFile(DataTable dt, string documentFilePath, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator)
        {
            ExportToFixedLengthTextFile(dt, documentFilePath, replaceExistingFile, columnNamesOnFirstLine, allowDataTruncation, useLineTerminator, Environment.NewLine, (int) 255, (int) 1024);
        }

        /// <summary>
        /// Routine to output data from grid to a text file that has a fixed length line format.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="columnNamesOnFirstLine">If true, column headers are output at the beginning of the text. If false, column headers are excluded from the output.</param>
        /// <param name="allowDataTruncation">Set to True if you with to allow data to be truncated if the data is longer than the target column's fixed width.</param>
        /// <param name="useLineTerminator">Set to true if you wish to have a line terminator placed at the end of each data row.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF (Environment.NewLine). Set this property if userLineTermintor is True and you need to specify a non-default set of one or more characters for the line terminator.</param>
        /// <param name="columnWidthForStringData">Sets the width in characters that will be allowed for each string column being output.</param>
        /// <param name="_maximumAllowedColumnWidth">Sets the maximum width in characters that will be allowed for any column regardless of data type that is being output.</param>

        public void ExportToFixedLengthTextFile(DataTable dt, string documentFilePath, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator, string lineTerminatorChars, int columnWidthForStringData, int _maximumAllowedColumnWidth)
        {
            FixedLengthTextFileOutputProcessor textOut = new FixedLengthTextFileOutputProcessor(documentFilePath, replaceExistingFile, columnNamesOnFirstLine, allowDataTruncation, useLineTerminator, lineTerminatorChars, columnWidthForStringData, _maximumAllowedColumnWidth);
            textOut.WriteDataToOutput(dt);
        }

        /// <summary>
        /// Routine to output data from grid to a text file that has a fixed length line format.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="columnNamesOnFirstLine">If true, column headers are output at the beginning of the text. If false, column headers are excluded from the output.</param>
        /// <param name="allowDataTruncation">Set to True if you with to allow data to be truncated if the data is longer than the target column's fixed width.</param>
        /// <param name="useLineTerminator">Set to true if you wish to have a line terminator placed at the end of each data row.</param>
        public void ExportToFixedLengthTextFile(PFList<string> dtList, string documentFilePath, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator)
        {
            ExportToFixedLengthTextFile(dtList, documentFilePath, replaceExistingFile, columnNamesOnFirstLine, allowDataTruncation, useLineTerminator, Environment.NewLine, (int)255, (int)1024);
        }

        /// <summary>
        /// Routine to output data from grid to a text file that has a fixed length line format.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="columnNamesOnFirstLine">If true, column headers are output at the beginning of the text. If false, column headers are excluded from the output.</param>
        /// <param name="allowDataTruncation">Set to True if you with to allow data to be truncated if the data is longer than the target column's fixed width.</param>
        /// <param name="useLineTerminator">Set to true if you wish to have a line terminator placed at the end of each data row.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF (Environment.NewLine). Set this property if userLineTermintor is True and you need to specify a non-default set of one or more characters for the line terminator.</param>
        /// <param name="columnWidthForStringData">Sets the width in characters that will be allowed for each string column being output.</param>
        /// <param name="_maximumAllowedColumnWidth">Sets the maximum width in characters that will be allowed for any column regardless of data type that is being output.</param>

        public void ExportToFixedLengthTextFile(PFList<string> dtList, string documentFilePath, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator, string lineTerminatorChars, int columnWidthForStringData, int _maximumAllowedColumnWidth)
        {
            FixedLengthTextFileOutputProcessor textOut = new FixedLengthTextFileOutputProcessor(documentFilePath, replaceExistingFile, columnNamesOnFirstLine, allowDataTruncation, useLineTerminator, lineTerminatorChars, columnWidthForStringData, _maximumAllowedColumnWidth);
            textOut.WriteDataToOutput(dtList);
        }


        
        /// <summary>
        ///  Routine to export the contents of a DataGridView object to an Access database file.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="accessVersion">Specify either Access2007 (.ACCDB) or Access2003 (.MDB) version for the output.</param>
        /// <param name="username">Access database username. Usually this is Admin. Default is Admin.</param>
        /// <param name="password">Access database logon password. Usually this is an empty string. Default is the empty string.</param>
        public void ExportToAccessFile(DataTable dt, string documentFilePath, bool replaceExistingFile, enAccessVersion accessVersion, string username, string password)
        {
            AccessDatabaseFileOutputProcessor tabOut = new AccessDatabaseFileOutputProcessor(documentFilePath, accessVersion, username, password);
            tabOut.ReplaceExistingFile = replaceExistingFile;
            tabOut.TableName = System.IO.Path.GetFileNameWithoutExtension(documentFilePath);
            tabOut.WriteDataToOutput(dt);
        }

        /// <summary>
        ///  Routine to export the contents of a DataGridView object to an Access database file.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="accessVersion">Specify either Access2007 (.ACCDB) or Access2003 (.MDB) version for the output.</param>
        /// <param name="username">Access database username. Usually this is Admin. Default is Admin.</param>
        /// <param name="password">Access database logon password. Usually this is an empty string. Default is the empty string.</param>
        public void ExportToAccessFile(PFList<string> dtList, string documentFilePath, bool replaceExistingFile, enAccessVersion accessVersion, string username, string password)
        {
            AccessDatabaseFileOutputProcessor tabOut = new AccessDatabaseFileOutputProcessor(documentFilePath, accessVersion, username, password);
            tabOut.ReplaceExistingFile = replaceExistingFile;
            tabOut.TableName = System.IO.Path.GetFileNameWithoutExtension(documentFilePath);
            tabOut.WriteDataToOutput(dtList);
        }

        /// <summary>
        ///  Routine to export the contents of a DataGridView object to an Word database file.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        /// <param name="wordOutputFormat">Specify either Word2007 (.DOCX) or Word2003 (.DOC) version for the output.</param>
        public void ExportToWordFile(DataTable dt, string documentFilePath, bool replaceExistingFile, enWordOutputFormat wordOutputFormat)
        {
            enWordVersion outputFormat = enWordVersion.NotSpecified;
            switch (wordOutputFormat)
            {
                case enWordOutputFormat.Word2007:
                    outputFormat = enWordVersion.Word2007;
                    break;
                case enWordOutputFormat.Word2003:
                    outputFormat = enWordVersion.Word2003;
                    break;
                default:
                    outputFormat = enWordVersion.Word2007;
                    break;
            }

            WordFileOutputProcessor docOut = new WordFileOutputProcessor(outputFormat, documentFilePath, replaceExistingFile);
            docOut.WriteDataToOutput(dt);
        }


        /// <summary>
        ///  Routine to export the contents of a DataGridView object to an RTF file.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        public void ExportToRtfFile(DataTable dt, string documentFilePath, bool replaceExistingFile)
        {
            RTFFileOutputProcessor rtfDoc = new RTFFileOutputProcessor(documentFilePath, replaceExistingFile);
            rtfDoc.WriteDataToOutput(dt);
        }

        /// <summary>
        ///  Routine to export the contents of a DataGridView object to an PDF file.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        public void ExportToPdfFile(DataTable dt, string documentFilePath, bool replaceExistingFile)
        {
            PDFFileOutputProcessor pdfDoc = new PDFFileOutputProcessor(documentFilePath, replaceExistingFile);
            pdfDoc.WriteDataToOutput(dt);
        }

        /// <summary>
        ///  Routine to export the contents of a DataGridView object to an XML file.
        /// </summary>
        /// <param name="dt">DataTable containing the grid rows to be output.</param>
        /// <param name="documentFilePath">Full path for the output file.</param>
        /// <param name="replaceExistingFile">If true and documentFilePath already exists, the existing file will be deleted.</param>
        public void ExportToXmlFile(DataTable dt, string documentFilePath, bool replaceExistingFile)
        {
            XMLFileOutputProcessor xmlDoc = new XMLFileOutputProcessor(documentFilePath, replaceExistingFile, enXMLOutputType.DataPlusSchema);
            xmlDoc.WriteDataToOutput(dt);
        }

        /// <summary>
        /// Routine to export datagrid data to a database table.
        /// </summary>
        /// <param name="dt">DataTable object containing the DataGrid data values.</param>
        /// <param name="dbPlat">Output database type.</param>
        /// <param name="connectionString">Connection string to use when connecting to the database.</param>
        /// <param name="outputTableName">Name of the output table.</param>
        /// <param name="outputBatchSize">Batch size to use when writing data to the database table.</param>
        /// <param name="replaceExistingTable">Set to true if you wish to replace an existing table. If set to false and the table exists, then an error will be thrown.</param>
        /// <remarks>If table does not exist, it will be created. If table exists, it will be dropped and recreated. If table exists and replaceExistingTable=false, an error will be thrown.</remarks>
        public void ExportToDatabaseTable(DataTable dt, DatabasePlatform dbPlat, string connectionString, string outputTableName, int outputBatchSize, bool replaceExistingTable)
        {
            PFDatabase db = null;
            string dllInfo = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            DataTable saveDtSchema = null;
            string tableName = string.Empty;
            bool isOracleOdbc = false;
            bool isOracleOledb = false;

            try
            {


                //save column names that may be changed during export processing so that they can be restored in finally block below
                saveDtSchema = dt.Clone();

                //workaround for problems with inserts when using Oracle ODBC or OLEDB drivers
                if (dbPlat == DatabasePlatform.ODBC)
                {
                    PFOdbc odbcDb = new PFOdbc();
                    odbcDb.ConnectionString = connectionString;
                    DatabasePlatform odbcDbPlat = odbcDb.GetDatabasePlatform();
                    if (odbcDbPlat == DatabasePlatform.OracleNative || odbcDbPlat == DatabasePlatform.MSOracle)
                        isOracleOdbc = true;
                    odbcDb = null;
                }

                if (dbPlat == DatabasePlatform.OLEDB)
                {
                    PFOleDb oledbDb = new PFOleDb();
                    oledbDb.ConnectionString = connectionString;
                    DatabasePlatform oledbDbPlat = oledbDb.GetDatabasePlatform();
                    if (oledbDbPlat == DatabasePlatform.OracleNative || oledbDbPlat == DatabasePlatform.MSOracle)
                        isOracleOledb = true;
                    oledbDb = null;
                }
                //end workaround code

                dllInfo = AppConfig.GetStringValueFromConfigFile(dbPlat.ToString(), string.Empty);
                if (dllInfo.Length > 0)
                {
                    string[] parsedConfig = dllInfo.Split('|');
                    nmSpace = parsedConfig[0];
                    clsName = parsedConfig[1];
                    dllPath = parsedConfig[2];
                    db = new PFDatabase(dbPlat.ToString(), dllPath, nmSpace + "." + clsName);
                }
                else
                {
                    db = new PFDatabase(dbPlat.ToString());  //search for dlls in default location
                }

                db.ConnectionString = connectionString;
                db.OpenConnection();

                //workaround for oracle insert problems
                if (isOracleOdbc || isOracleOledb)
                {
                    //make table name and all column names upper case
                    tableName = outputTableName.ToUpper();
                    for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                    {
                        dt.Columns[colInx].ColumnName = dt.Columns[colInx].ColumnName.ToUpper();
                    }
                }
                else
                {
                    tableName = outputTableName;
                    //leave column names as is
                }
                //end workaround

                dt.TableName = tableName;

                if (db.TableExists(tableName))
                {
                    if (replaceExistingTable)
                    {
                        db.DropTable(tableName);
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Table ");
                        _msg.Append(tableName);
                        _msg.Append(" already exists and you specified False for Replace Existing Table parameter. Specify another table name or change Replace Existing Table to True.");
                        throw new System.Exception(_msg.ToString());
                    }
                    if (db.TableExists(tableName) == true)
                    {
                        _msg.Length = 0;
                        _msg.Append("Attemp to drop table ");
                        _msg.Append(tableName);
                        _msg.Append(" failed. Make sure you have permissions needed in the database to drop the table or change the name of the table.");
                        throw new System.Exception(_msg.ToString());
                    }
                }
                else
                {
                    ;  //table does not exist; new table will be created.
                }

                //create the table
                string createScript = string.Empty;
                string errorMessages = string.Empty;
                bool result = db.CreateTable(dt,out createScript,out errorMessages);
                if (result == false)
                {
                    _msg.Length = 0;
                    _msg.Append(errorMessages);
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Text of table create statement: ");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(createScript);
                    _msg.Append(Environment.NewLine);
                    throw new Exception(_msg.ToString());
                }

                int batchSize = outputBatchSize;
                if (batchSize == 1)
                    db.ImportDataFromDataTable(dt);
                else
                    db.ImportDataFromDataTable(dt, batchSize);
                }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                //resstore any column names that may have been changed during export processing
                for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                {
                    dt.Columns[colInx].ColumnName = saveDtSchema.Columns[colInx].ColumnName;
                }
            }
                 
        



        }


        /// <summary>
        /// Routine to export datagrid data to a database table. Data will be appended to an existing table.
        /// </summary>
        /// <param name="dt">DataTable object containing the DataGrid data values.</param>
        /// <param name="dbPlat">Output database type.</param>
        /// <param name="connectionString">Connection string to use when connecting to the database.</param>
        /// <param name="outputTableName">Name of the output table.</param>
        /// <param name="outputBatchSize">Batch size to use when writing data to the database table.</param>
        /// <param name="replaceExistingTable">Set to true if you wish to replace an existing table. If set to false and the table exists, then an error will be thrown.</param>
        /// <remarks>Table must already exist before using this routine.</remarks>
        public void AppendToDatabaseTable(DataTable dt, DatabasePlatform dbPlat, string connectionString, string outputTableName, int outputBatchSize, bool replaceExistingTable)
        {
            PFDatabase db = null;
            string dllInfo = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            dllInfo = AppConfig.GetStringValueFromConfigFile(dbPlat.ToString(), string.Empty);
            if (dllInfo.Length > 0)
            {
                string[] parsedConfig = dllInfo.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];
                db = new PFDatabase(dbPlat.ToString(), dllPath, nmSpace + "." + clsName);
            }
            else
            {
                db = new PFDatabase(dbPlat.ToString());  //search for dlls in default location
            }

            db.ConnectionString = connectionString;
            db.OpenConnection();

            string tableName = outputTableName;

            dt.TableName = tableName;

            if (db.TableExists(tableName) == false)
            {
                _msg.Length = 0;
                _msg.Append("Table ");
                _msg.Append(tableName);
                _msg.Append(" does not exist. Table must first exist before it can be appended to.");
                throw new System.Exception(_msg.ToString());
            }

            int batchSize = outputBatchSize;
            if (batchSize == 1)
                db.ImportDataFromDataTable(dt);
            else
                db.ImportDataFromDataTable(dt, batchSize);

        }


        /// <summary>
        /// Routine to export datagrid data to a database table.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <param name="dbPlat">Output database type.</param>
        /// <param name="connectionString">Connection string to use when connecting to the database.</param>
        /// <param name="outputTableName">Name of the output table.</param>
        /// <param name="outputBatchSize">Batch size to use when writing data to the database table.</param>
        /// <param name="replaceExistingTable">Set to true if you wish to replace an existing table. If set to false and the table exists, then an error will be thrown.</param>
        public void ExportToDatabaseTable(PFList<string> dtList, DatabasePlatform dbPlat, string connectionString, string outputTableName, int outputBatchSize, bool replaceExistingTable)
        {
            DataTable dt = null;

            for (int dtInx = 0; dtInx < dtList.Count; dtInx++)
            {
                dt = new DataTable();
                dt.ReadXml(dtList[dtInx]);
                dt.TableName = outputTableName;
                if (dtInx == 0)
                    ExportToDatabaseTable(dt, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                else
                    AppendToDatabaseTable(dt, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                dt.Dispose();
                dt = null;
            }

        }

        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataOutputGridExporter));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of DataOutputGridDef.</returns>
        public static DataOutputGridExporter LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DataOutputGridExporter));
            TextReader textReader = new StreamReader(filePath);
            DataOutputGridExporter objectInstance;
            objectInstance = (DataOutputGridExporter)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
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
            XmlSerializer ser = new XmlSerializer(typeof(DataOutputGridExporter));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of DataOutputGridDef.</returns>
        public static DataOutputGridExporter LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DataOutputGridExporter));
            StringReader strReader = new StringReader(xmlString);
            DataOutputGridExporter objectInstance;
            objectInstance = (DataOutputGridExporter)deserializer.Deserialize(strReader);
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
