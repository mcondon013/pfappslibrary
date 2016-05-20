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
using System.Drawing;
using System.Windows.Forms;
using AppGlobals;
using pfDataExtractorCPObjects;

namespace pfDataExtractorCP
{
    /// <summary>
    /// Basic prototype for a ProFast application or library class.
    /// </summary>
    public class SettingsInitializer
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //folder options defaults
        private string _defaultExtractorDefinitionsSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Definitions\";

        private string _defaultSourceAccessDatabaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Sources\Access\";
        private string _defaultSourceExcelDataFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Sources\Excel\";
        private string _defaultSourceDelimitedTextFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Sources\TextFiles\Delimited\";
        private string _defaultSourceFixedLengthTextFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Sources\TextFiles\FixedLength\";
        private string _defaultSourceXmlFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Sources\XML\";

        private string _defaultDestinationAccessDatabaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\Access\";
        private string _defaultDestinationExcelDataFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\Excel\";
        private string _defaultDestinationDelimitedTextFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\TextFiles\Delimited\";
        private string _defaultDestinationFixedLengthTextFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\TextFiles\FixedLength\";
        private string _defaultDestinationXmlFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\XML\";

        //user options defaults

        private int _maxNumberOfRows = 1000;
        private string _defaultDataSourceType = string.Empty;
        private string _defaultDataDestinationType = string.Empty;
        private Font _defaultApplicationFont = System.Drawing.SystemFonts.DefaultFont;
        private int _batchSizeForDataImportsAndExports = 1000;
        private int _batchSizeForRandomDataGeneration = 5000;

        //database options defaults

        private string _defaultQueryDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\QueryDefs\";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\DataExports\";


        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SettingsInitializer()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Initializes folder settings with defaults.
        /// </summary>
        public void InitFolderOptionsSettings()
        {
            string configValue = string.Empty;



            try
            {
                CreateDefaultFolders();

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultExtractorDefinitionsSaveFolder"] = _defaultExtractorDefinitionsSaveFolder;
                
                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultQueryDefinitionsFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultQueryDefinitionsFolder"] = _defaultQueryDefinitionsFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDataGridExportFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["defaultDataGridExportFolder"] = _defaultDataGridExportFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceAccessDatabaseFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceAccessDatabaseFolder"] = _defaultSourceAccessDatabaseFolder;
                
                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceExcelDataFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceExcelDataFileFolder"] = _defaultSourceExcelDataFileFolder;
                
                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceDelimitedTextFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceDelimitedTextFileFolder"] = _defaultSourceDelimitedTextFileFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceFixedLengthTextFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceFixedLengthTextFileFolder"] = _defaultSourceFixedLengthTextFileFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceXmlFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceXmlFileFolder"] = _defaultSourceXmlFileFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationAccessDatabaseFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationAccessDatabaseFolder"] = _defaultDestinationAccessDatabaseFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationExcelDataFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationExcelDataFileFolder"] = _defaultDestinationExcelDataFileFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationDelimitedTextFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationDelimitedTextFileFolder"] = _defaultDestinationDelimitedTextFileFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationFixedLengthTextFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationFixedLengthTextFileFolder"] = _defaultDestinationFixedLengthTextFileFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationXmlFileFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationXmlFileFolder"] = _defaultDestinationXmlFileFolder;

                
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
            }
            finally
            {
                ;
            }
                 
        

        }

        private void CreateDefaultFolders()
        {
            if (Directory.Exists(_defaultExtractorDefinitionsSaveFolder) == false)
                Directory.CreateDirectory(_defaultExtractorDefinitionsSaveFolder);

            if (Directory.Exists(_defaultQueryDefinitionsFolder) == false)
                Directory.CreateDirectory(_defaultQueryDefinitionsFolder);
            if (Directory.Exists(_defaultDataGridExportFolder) == false)
                Directory.CreateDirectory(_defaultDataGridExportFolder);

            if (Directory.Exists(_defaultSourceAccessDatabaseFolder) == false)
                Directory.CreateDirectory(_defaultSourceAccessDatabaseFolder);
            if (Directory.Exists(_defaultSourceExcelDataFileFolder) == false)
                Directory.CreateDirectory(_defaultSourceExcelDataFileFolder);
            if (Directory.Exists(_defaultSourceDelimitedTextFileFolder) == false)
                Directory.CreateDirectory(_defaultSourceDelimitedTextFileFolder);
            if (Directory.Exists(_defaultSourceFixedLengthTextFileFolder) == false)
                Directory.CreateDirectory(_defaultSourceFixedLengthTextFileFolder);
            if (Directory.Exists(_defaultSourceXmlFileFolder) == false)
                Directory.CreateDirectory(_defaultSourceXmlFileFolder);

            if (Directory.Exists(_defaultDestinationAccessDatabaseFolder) == false)
                Directory.CreateDirectory(_defaultDestinationAccessDatabaseFolder);
            if (Directory.Exists(_defaultDestinationExcelDataFileFolder) == false)
                Directory.CreateDirectory(_defaultDestinationExcelDataFileFolder);
            if (Directory.Exists(_defaultDestinationDelimitedTextFileFolder) == false)
                Directory.CreateDirectory(_defaultDestinationDelimitedTextFileFolder);
            if (Directory.Exists(_defaultDestinationFixedLengthTextFileFolder) == false)
                Directory.CreateDirectory(_defaultDestinationFixedLengthTextFileFolder);
            if (Directory.Exists(_defaultDestinationXmlFileFolder) == false)
                Directory.CreateDirectory(_defaultDestinationXmlFileFolder);

        }

        /// <summary>
        /// Initializes user options settings with defaults.
        /// </summary>
        public void InitUserOptionsSettings()
        {
            string configValue = string.Empty;

            try
            {
                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDataSourceType.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDataSourceType"] = ExtractorDataTypeList.ExtractorDataLocations[0];

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDataDestinationType.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDataDestinationType"] = ExtractorDataTypeList.ExtractorDataLocations[0];

                if (pfDataExtractorCP.Properties.Settings.Default.MaxNumberOfRows == 0)
                    pfDataExtractorCP.Properties.Settings.Default["MaxNumberOfRows"] = _maxNumberOfRows;

                if (pfDataExtractorCP.Properties.Settings.Default.BatchSizeForDataImportsAndExports == 0)
                    pfDataExtractorCP.Properties.Settings.Default["BatchSizeForDataImportsAndExports"] = _batchSizeForDataImportsAndExports;
                
                if (pfDataExtractorCP.Properties.Settings.Default.BatchSizeForRandomDataGeneration == 0)
                    pfDataExtractorCP.Properties.Settings.Default["BatchSizeForRandomDataGeneration"] = _batchSizeForRandomDataGeneration;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
            }
            finally
            {
                ;
            }
        
        }

        /// <summary>
        /// Initializes database settings with defaults.
        /// </summary>
        public void InitDatabaseSettings()
        {
            string configValue = string.Empty;

            try
            {
                CreateDefaultDatabaseFolders();

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultQueryDefinitionsFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["DefaultQueryDefinitionsFolder"] = _defaultQueryDefinitionsFolder;

                configValue = pfDataExtractorCP.Properties.Settings.Default.DefaultDataGridExportFolder.Trim();
                if (configValue == string.Empty)
                    pfDataExtractorCP.Properties.Settings.Default["defaultDataGridExportFolder"] = _defaultDataGridExportFolder;


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
            }
            finally
            {
                ;
            }
                        
        }

        private void CreateDefaultDatabaseFolders()
        {
            if (Directory.Exists(_defaultQueryDefinitionsFolder) == false)
                Directory.CreateDirectory(_defaultQueryDefinitionsFolder);

            if (Directory.Exists(_defaultDataGridExportFolder) == false)
                Directory.CreateDirectory(_defaultDataGridExportFolder);
        }

    }//end class
}//end namespace
