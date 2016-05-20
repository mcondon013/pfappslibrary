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

namespace PFAppDataObjects
{

#pragma warning disable 1591
    public enum ReasonForFileSavePrompt
    {
        Unknown = 0,
        ApplicationExit = 1,
        FileOpen = 2,
        FileClose = 3,
        FileNew = 4,
        DefaultOptionsChange = 5,
        DataSourceChange = 6
    }

    public enum EditOperation
    {
        Unknown = 0,
        Cut = 1,
        Copy = 2,
        Paste = 3,
        SelectAll = 4,
        Delete = 5
    }

    public struct ControlValue
    {
        public string TabIndex;
        public string Description;
        public string Value;
    }

    public enum enFilterBoolean
    {
        None = 0,
        And = 1,
        Or = 2
    }

    public enum enFilterCondition
    {
        None,
        EqualTo,
        GreaterThan,
        LessThan,
        GreaterThanOrEqualTo,
        LessThanOrEqualTo,
        In,                              //array search (custom)  
        Contains,                        //c# string.contains
        StartsWith,
        EndsWith,
        IsNull,
        NotEqualTo,
        NotGreaterThan,
        NotLessThan,
        NotGreaterThanOrEqualTo,
        NotLessThanOrEqualTo,
        NotIn,
        DoesNotContain,
        DoesNotStartWith,
        DoesNotEndWith,
        IsNotNull
    }

    public enum enAggregateFunction
    {
        None,
        Count,
        Sum,
        Min,
        Max,
        Avg
    }


#pragma warning restore 1591

}//end namespace