//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text.RegularExpressions;
using AppGlobals;
using PFRandomDataExt;
using PFRandomDataProcessor;

namespace PFRandomValueDataTables
{
    /// <summary>
    /// Contains routines to generate random string values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomStringDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private delegate string GetRandomStringDelegate(int size);
        private delegate string GetRandomSyllableStringDelegate(int size);
        private delegate string GetRandomRepeatCharacterDelegate(int size);

        private GetRandomStringDelegate[] getRandomString = new GetRandomStringDelegate[9];
        private GetRandomSyllableStringDelegate[] getRandomSyllableString = new GetRandomSyllableStringDelegate[3];
        private GetRandomRepeatCharacterDelegate[] getRandomRepeatedCharacter = new GetRandomRepeatCharacterDelegate[9];

        private RandomString _rstr = new RandomString();
        private RandomSingleCharacterRepeated _rcr = new RandomSingleCharacterRepeated();


        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomStringDataTable()
        {
            InitInstance();
        }

        private void InitInstance()
        {

            getRandomString[(int)enRandomStringType.enAN] = new GetRandomStringDelegate(_rstr.GetStringAN);
            getRandomString[(int)enRandomStringType.enANUC] = new GetRandomStringDelegate(_rstr.GetStringANUC);
            getRandomString[(int)enRandomStringType.enANLC] = new GetRandomStringDelegate(_rstr.GetStringANLC);
            getRandomString[(int)enRandomStringType.enANX] = new GetRandomStringDelegate(_rstr.GetStringANX);
            getRandomString[(int)enRandomStringType.enAL] = new GetRandomStringDelegate(_rstr.GetStringAL);
            getRandomString[(int)enRandomStringType.enLC] = new GetRandomStringDelegate(_rstr.GetStringLC);
            getRandomString[(int)enRandomStringType.enUC] = new GetRandomStringDelegate(_rstr.GetStringUC);
            getRandomString[(int)enRandomStringType.enDEC] = new GetRandomStringDelegate(_rstr.GetStringDEC);
            getRandomString[(int)enRandomStringType.enHEX] = new GetRandomStringDelegate(_rstr.GetStringHEX);

            getRandomSyllableString[(int)enRandomSyllableStringType.enUCLC] = new GetRandomSyllableStringDelegate(_rstr.GetRandomSyllablesUCLC);
            getRandomSyllableString[(int)enRandomSyllableStringType.enLC] = new GetRandomSyllableStringDelegate(_rstr.GetRandomSyllablesLC);
            getRandomSyllableString[(int)enRandomSyllableStringType.enUC] = new GetRandomSyllableStringDelegate(_rstr.GetRandomSyllablesUC);

            getRandomRepeatedCharacter[(int)enRandomStringType.enAN] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringAN);
            getRandomRepeatedCharacter[(int)enRandomStringType.enANUC] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringANUC);
            getRandomRepeatedCharacter[(int)enRandomStringType.enANLC] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringANLC);
            getRandomRepeatedCharacter[(int)enRandomStringType.enANX] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringANX);
            getRandomRepeatedCharacter[(int)enRandomStringType.enAL] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringAL);
            getRandomRepeatedCharacter[(int)enRandomStringType.enLC] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringLC);
            getRandomRepeatedCharacter[(int)enRandomStringType.enUC] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringUC);
            getRandomRepeatedCharacter[(int)enRandomStringType.enDEC] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringDEC);
            getRandomRepeatedCharacter[(int)enRandomStringType.enHEX] = new GetRandomRepeatCharacterDelegate(_rcr.GetStringHEX);

        }

        //properties

        //methods

        /// <summary>
        /// Creates an ADO.NET DataTable object containing list of random strings.
        /// </summary>
        /// <param name="numRows">Number of rows containing random strings to generate.</param>
        /// <param name="dataRequest">RandomStringDataRequest object containing definition for the type of strings to generate.</param>
        /// <returns>ADO.NET DataTable object.</returns>
        public DataTable CreateRandomDataTable(int numRows, RandomStringDataRequest dataRequest)
        {
            DataTable dt = null;
            enRandomStringType randStringType = enRandomStringType.enUnknown;
            enRandomSyllableStringType randSyllableStringType = enRandomSyllableStringType.enUnknown;

            if (dataRequest.OutputRandomStrings)
            {
                randStringType = GetRandomStringType(dataRequest);
                if (randStringType != enRandomStringType.enUnknown)
                {
                    dt = CreateRandomStringsDataTable(randStringType, numRows, dataRequest.MinNumStrings, dataRequest.MaxNumStrings, dataRequest.StringMinimumLength, dataRequest.StringMaximumLength);
                }
            }
            else if (dataRequest.OutputRandomSyllableStrings)
            {
                randSyllableStringType = GetRandomSyllableStringType(dataRequest);
                if (randSyllableStringType != enRandomSyllableStringType.enUnknown)
                {
                    dt = CreateRandomSyllableStringsDataTable(randSyllableStringType, numRows, dataRequest.MinNumSyllableStrings, dataRequest.MaxNumSyllableStrings, dataRequest.SyllableStringMinimumLength, dataRequest.SyllableStringMaximumLength);
                }
            }
            else if (dataRequest.OutputRepeatingStrings)
            {
                if (dataRequest.OutputRepeatRandomCharacter)
                {
                    randStringType = GetRandomRepeatingStringType(dataRequest);
                    if (randStringType != enRandomStringType.enUnknown)
                    {
                        dt = CreateRandomRepeatingCharacterDataTable(randStringType, numRows, dataRequest.MinRepeatOutputLength, dataRequest.MaxRepeatOutputLength, dataRequest.MinNumRepeats, dataRequest.MaxNumRepeats);
                    }
                }
                else
                {
                    dt = CreateRepeatingTextDataTable(numRows, dataRequest.TextToRepeat, dataRequest.MinNumRepeats, dataRequest.MaxNumRepeats);
                }
            }
            else
            {
                dt = new DataTable();
            }


            return dt;
        }

        private enRandomStringType GetRandomRepeatingStringType(RandomStringDataRequest dataRequest)
        {
            enRandomStringType randStringType = enRandomStringType.enUnknown;

            if (dataRequest.OutputRepeatAN)
                randStringType = enRandomStringType.enAN;
            else if (dataRequest.OutputRepeatANUC)
                randStringType = enRandomStringType.enANUC;
            else if (dataRequest.OutputRepeatANLC)
                randStringType = enRandomStringType.enANLC;
            else if (dataRequest.OutputRepeatANX)
                randStringType = enRandomStringType.enANX;
            else if (dataRequest.OutputRepeatAL)
                randStringType = enRandomStringType.enAL;
            else if (dataRequest.OutputRepeatLC)
                randStringType = enRandomStringType.enLC;
            else if (dataRequest.OutputRepeatUC)
                randStringType = enRandomStringType.enUC;
            else if (dataRequest.OutputRepeatDEC)
                randStringType = enRandomStringType.enDEC;
            else if (dataRequest.OutputRepeatHEX)
                randStringType = enRandomStringType.enHEX;
            else
                randStringType = enRandomStringType.enUnknown;

            return randStringType;
        }

        private enRandomSyllableStringType GetRandomSyllableStringType(RandomStringDataRequest dataRequest)
        {
            enRandomSyllableStringType randSyllableStringType = enRandomSyllableStringType.enUnknown;

            if (dataRequest.OutputSyllableUCLC)
                randSyllableStringType = enRandomSyllableStringType.enUCLC;
            else if (dataRequest.OutputSyllableLC)
                randSyllableStringType = enRandomSyllableStringType.enLC;
            else if (dataRequest.OutputSyllableUC)
                randSyllableStringType = enRandomSyllableStringType.enUC;
            else
                randSyllableStringType = enRandomSyllableStringType.enUnknown;


            return randSyllableStringType;
        }

        private enRandomStringType GetRandomStringType(RandomStringDataRequest dataRequest)
        {
            enRandomStringType randStringType = enRandomStringType.enUnknown;

            if (dataRequest.OutputAN)
                randStringType = enRandomStringType.enAN;
            else if (dataRequest.OutputANUC)
                randStringType = enRandomStringType.enANUC;
            else if (dataRequest.OutputANLC)
                randStringType = enRandomStringType.enANLC;
            else if (dataRequest.OutputANX)
                randStringType = enRandomStringType.enANX;
            else if (dataRequest.OutputAL)
                randStringType = enRandomStringType.enAL;
            else if (dataRequest.OutputLC)
                randStringType = enRandomStringType.enLC;
            else if (dataRequest.OutputUC)
                randStringType = enRandomStringType.enUC;
            else if (dataRequest.OutputDEC)
                randStringType = enRandomStringType.enDEC;
            else if (dataRequest.OutputHEX)
                randStringType = enRandomStringType.enHEX;
            else
                randStringType = enRandomStringType.enUnknown;

            return randStringType;
        }


        /// <summary>
        /// Routine to create a set of random strings.
        /// </summary>
        /// <param name="randStringType">Enumeration describing the format of the strings to be generated. (e.g. AlphaNumeric, Alpha Only etc.)</param>
        /// <param name="numRows">Number of DataTable rows to generate containing random strings.</param>
        /// <param name="minNumStrings">Minimum number of strings to generate for each output row.</param>
        /// <param name="maxNumStrings">Maximum number of strings to generate for each output row.</param>
        /// <param name="stringMinimumLength">Minimum length of strings to generate for each output row.</param>
        /// <param name="stringMaximumLength">Maximum length of strings to generate for each output row.</param>
        /// <returns>ADO.NET DataTable containing the generated strings.</returns>
        public DataTable CreateRandomStringsDataTable(enRandomStringType randStringType,
                                                      int numRows,
                                                      string minNumStrings,
                                                      string maxNumStrings,
                                                      string stringMinimumLength,
                                                      string stringMaximumLength)
        {
            return CreateRandomStringsDataTable(randStringType,
                                                 numRows,
                                                 minNumStrings,
                                                 maxNumStrings,
                                                 stringMinimumLength,
                                                 stringMaximumLength,
                                                 string.Empty,
                                                 string.Empty);
        }

        /// <summary>
        /// Routine to create a set of random strings.
        /// </summary>
        /// <param name="randStringType">Enumeration describing the format of the strings to be generated. (e.g. AlphaNumeric, Alpha Only etc.)</param>
        /// <param name="numRows">Number of DataTable rows to generate containing random strings.</param>
        /// <param name="minNumStrings">Minimum number of strings to generate for each output row.</param>
        /// <param name="maxNumStrings">Maximum number of strings to generate for each output row.</param>
        /// <param name="stringMinimumLength">Minimum length of strings to generate for each output row.</param>
        /// <param name="stringMaximumLength">Maximum length of strings to generate for each output row.</param>
        /// <param name="regexPattern">Regex pattern to use when generating a string. Assign empty string to this property to bypass regex processing.</param>
        /// <param name="regexReplacement">Replacement place holders for the regex generation. Assign empty string to this property to bypass regex processing.</param>
        /// <returns>ADO.NET DataTable containing the generated random strings.</returns>
        /// <remarks>Example of how regex is used by this routine: Regex.Replace(str, @"(\w{4})(\w{4})(\w{4})", @"$1-$2-$3"); </remarks>
        /// <remarks>Leave regexPattern and regexReplacement blank if you do not wish to use regex.</remarks>
        public DataTable CreateRandomStringsDataTable(enRandomStringType randStringType, 
                                                      int numRows, 
                                                      string minNumStrings, 
                                                      string maxNumStrings, 
                                                      string stringMinimumLength,
                                                      string stringMaximumLength,
                                                      string regexPattern,
                                                      string regexReplacement)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            StringBuilder randString = new StringBuilder();

            try
            {
                int minimumNumberOfStrings = Convert.ToInt32(minNumStrings);
                int maximumNumberOfStrings = Convert.ToInt32(maxNumStrings);
                int minimumLength = Convert.ToInt32(stringMinimumLength);
                int maximumLength = Convert.ToInt32(stringMaximumLength);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    int numStrings = rn.GenerateRandomInt(minimumNumberOfStrings, maximumNumberOfStrings);
                    randString.Length = 0;
                    for (int s = 0; s < numStrings; s++)
                    {
                        int size = rn.GenerateRandomInt(minimumLength, maximumLength);
                        string str = getRandomString[(int)randStringType](size);
                        //str = Regex.Replace(str, @"(\w{4})(\w{4})(\w{4})", @"$1-$2-$3");
                        if (regexPattern.Trim().Length > 0)
                            if (regexReplacement.Trim().Length > 0)
                                str = Regex.Replace(str, regexPattern, regexReplacement);
                        randString.Append(str);
                        randString.Append(" ");
                    }
                    dr[0] = randString.ToString();
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomStringsDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Creates strings containing random syllables.
        /// </summary>
        /// <param name="randSyllableStringType">enRandomSyllableStringType enumeration used to specify format of the syllable strings.</param>
        /// <param name="numRows">Number of DataTable rows to generate containing random syllable strings.</param>
        /// <param name="minNumStrings">Minimum number of random syllable strings to generate for each output row.</param>
        /// <param name="maxNumStrings">Maximum number of random syllable strings to generate for each output row.</param>
        /// <param name="stringMinimumLength">Minimum length of random syllable strings to generate for each output row.</param>
        /// <param name="stringMaximumLength">Maximum length of random syllable strings to generate for each output row.</param>
        /// <returns>ADO.NET DataTable containing the generated random syllables.</returns>
        public DataTable CreateRandomSyllableStringsDataTable(enRandomSyllableStringType randSyllableStringType, int numRows, string minNumStrings, string maxNumStrings, string stringMinimumLength, string stringMaximumLength)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            StringBuilder randString = new StringBuilder();

            try
            {
                int minimumNumberOfStrings = Convert.ToInt32(minNumStrings);
                int maximumNumberOfStrings = Convert.ToInt32(maxNumStrings);
                int minimumLength = Convert.ToInt32(stringMinimumLength);
                int maximumLength = Convert.ToInt32(stringMaximumLength);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    int numStrings = rn.GenerateRandomInt(minimumNumberOfStrings, maximumNumberOfStrings);
                    randString.Length = 0;
                    for (int s = 0; s < numStrings; s++)
                    {
                        int size = rn.GenerateRandomInt(minimumLength, maximumLength);
                        string str = getRandomSyllableString[(int)randSyllableStringType](size);
                        randString.Append(str);
                        randString.Append(" ");
                    }
                    dr[0] = randString.ToString();
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomSyllableStringsDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Generates random strings containing repeating characters.
        /// </summary>
        /// <param name="randStringType">enRandomStringType enumeration used to define output format.</param>
        /// <param name="numRows">Number of DataTable rows to generate containing random strings.</param>
        /// <param name="minRepeatOutputLength">Minimum number of times character is repeated.</param>
        /// <param name="maxRepeatOutputLength">Maximum number of times character is repeated.</param>
        /// <param name="minNumRepeats">Minimum number of strings containing repeating characters to generate for each output row.</param>
        /// <param name="maxNumRepeats">Minimum number of strings containing repeating characters to generate for each output row.</param>
        /// <returns>ADO.NET DataTable containing the generated strings with repeating characgters.</returns>
        public DataTable CreateRandomRepeatingCharacterDataTable(enRandomStringType randStringType, int numRows, string minRepeatOutputLength, string maxRepeatOutputLength, string minNumRepeats, string maxNumRepeats)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            StringBuilder randString = new StringBuilder();

            try
            {
                int minimumRepeatOutputLength = Convert.ToInt32(minRepeatOutputLength);
                int maximumRepeatOutputLength = Convert.ToInt32(maxRepeatOutputLength);
                int minimumNumberOfRepeats = Convert.ToInt32(minNumRepeats);
                int maximumNumberOfRepeats = Convert.ToInt32(maxNumRepeats);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    int numStrings = rn.GenerateRandomInt(minimumNumberOfRepeats, maximumNumberOfRepeats);
                    randString.Length = 0;
                    for (int s = 0; s < numStrings; s++)
                    {
                        int size = rn.GenerateRandomInt(minimumRepeatOutputLength, maximumRepeatOutputLength);
                        string str = getRandomRepeatedCharacter[(int)randStringType](size);
                        randString.Append(str);
                        randString.Append(" ");
                    }
                    dr[0] = randString.ToString();
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomRepeatingCharacterDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Generates string containing the specified text repeated.
        /// </summary>
        /// <param name="numRows">Number of DataTable rows to generate containing strings with repeated text.</param>
        /// <param name="textToRepeat">String containing the text to repeat.</param>
        /// <param name="minNumRepeats">Minium number of repeats per output row.</param>
        /// <param name="maxNumRepeats">Maxium number of repeats per output row.</param>
        /// <returns>ADO.NET DataTable containing the generated strings with the repeated text.</returns>
        /// <remarks>Output example: "WARNING WARNING WARNING" </remarks>
        public DataTable CreateRepeatingTextDataTable(int numRows, string textToRepeat, string minNumRepeats, string maxNumRepeats)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            StringBuilder randString = new StringBuilder();

            try
            {
                int minimumNumberOfRepeats = Convert.ToInt32(minNumRepeats);
                int maximumNumberOfRepeats = Convert.ToInt32(maxNumRepeats);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    int numStrings = rn.GenerateRandomInt(minimumNumberOfRepeats, maximumNumberOfRepeats);
                    randString.Length = 0;
                    for (int s = 0; s < numStrings; s++)
                    {
                        randString.Append(textToRepeat);
                        randString.Append(" ");
                    }
                    dr[0] = randString.ToString();
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRepeatingTextDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }



    }//end class
}//end namespace
