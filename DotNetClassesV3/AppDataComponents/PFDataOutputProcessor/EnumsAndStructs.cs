//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#pragma warning disable 1591
namespace PFDataOutputProcessor
{

    public enum enOutputType
    {
        NotSpecified,
        DelimitedTextFile,
        FixedLengthTextFile,
        DatabaseTable,
        DesktopDatabaseFile,
        AccessDatabaseFile,
        ExcelDocumentFile,
        WordDocumentFile,
        XMLFile,
        RTFFile,
        PDFFile
    }

    public enum enExcelVersion
    {
        NotSpecified,
        Excel2003,
        Excel2007,
        CSV
    }

    public enum enWordVersion
    {
        NotSpecified,
        Word2003,
        Word2007
    }

    public enum enAccessVersion
    {
        NotSpecified = 0,
        Access2003 = 1,
        Access2007 = 2
    }

    public enum enXMLOutputType
    {
        NotSpecified,
        DataOnly,
        DataPlusSchema,
        SchemaOnly
    }

    public enum enDesktopDbVersion
    {
        NotSpecified,
        SQLCE_Version35,
        SQLCE_Version40,
        SQLAnywhere,
        SQLAnywhere_UltraLite
    }


}//end namespace
#pragma warning restore 1591
