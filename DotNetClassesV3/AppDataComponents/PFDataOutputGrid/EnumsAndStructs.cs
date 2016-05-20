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
namespace PFDataOutputGrid
{
    public enum enFilterType
    {
        NotSpecified,
        CheckBoxColumnFilter,
        ComboBoxColumnFilter,
        TextBoxColumnFilter,
        DateColumnFilter,
        DateRangeColumnFilter,
        MonthYearColumnFilter,
        NumRangeColumnFilter
    }

    public class GridColumnFilter
    {
        string _colName = string.Empty;
        enFilterType _colFilterType = enFilterType.NotSpecified;

        public GridColumnFilter(string colName, enFilterType colFilterType)
        {
            _colName = colName;
            _colFilterType = colFilterType;
        }
        
            /// <summary>
        /// Name of column in the grid.
        /// </summary>
        public string ColName
        {
            get
            {
                return _colName;
            }
            set
            {
                _colName = value;
            }
        }

        /// <summary>
        /// Type of filter to apply for the column.
        /// </summary>
        public enFilterType ColFilterType
        {
            get
            {
                return _colFilterType;
            }
            set
            {
                _colFilterType = value;
            }
        }



    }

}//end namespace
#pragma warning restore 1591
