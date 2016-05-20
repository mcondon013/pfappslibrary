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
using System.Globalization;
using AppGlobals;
using PFRandomDataExt;
using PFRandomWordProcessor;
using PFRandomDataProcessor;

namespace PFRandomValueDataTables
{
    /// <summary>
    /// Contains routines to generate random word values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomWordsDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomWordsDataTable()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Creates an ADO.NET DataTable object containing list of random words.
        /// </summary>
        /// <param name="numRows">Number of rows containing random words to generate.</param>
        /// <param name="dataRequest">RandomWordsDataRequest object containing definition for the type of words to generate.</param>
        /// <returns>ADO.NET DataTable object.</returns>
        public DataTable CreateRandomDataTable(int numRows, RandomWordsDataRequest dataRequest)
        {
            DataTable dt = null;
            enRandomWordOutputFormat randWordOutputFormat = enRandomWordOutputFormat.enUnknown;

            if (dataRequest.OutputRandomWords)
            {
                randWordOutputFormat = GetRandomWordOutputFormat(dataRequest);
                dt = CreateRandomWordsDataTable(numRows, dataRequest.MinNumWords, dataRequest.MaxNumWords, randWordOutputFormat);
            }
            else if (dataRequest.OutputRandomSentences)
            {
                dt = CreateRandomSentencesDataTable(numRows, dataRequest.MinNumSentences, dataRequest.MaxNumSentences);
            }
            else if (dataRequest.OutputRandomDocument)
            {
                dt = CreateRandomDocumentDataTable(numRows, dataRequest.MinNumParagraphs, dataRequest.MaxNumParagraphs, dataRequest.MinNumSentencesPerParagraph, dataRequest.MaxNumSentencesPerParagraph, dataRequest.IncludeDocumentTitle);
            }
            else
            {
                dt = new DataTable();  //return an empty data table
            }



            return dt;
        }

        /// <summary>
        /// Retrieves the word formatting (upper and lower case patterns).
        /// </summary>
        /// <param name="dataRequest">RandomWordsDataRequest object containing the definition for the word list to generate.</param>
        /// <returns>enRandomWordOutputFormat enumerated value.</returns>
        public enRandomWordOutputFormat GetRandomWordOutputFormat(RandomWordsDataRequest dataRequest)
        {
            enRandomWordOutputFormat randWordOutputFormat = enRandomWordOutputFormat.enUnknown;

            if (dataRequest.OutputWordUCLC)
                randWordOutputFormat = enRandomWordOutputFormat.enUCLC;
            else if (dataRequest.OutputWordLC)
                randWordOutputFormat = enRandomWordOutputFormat.enLC;
            else if (dataRequest.OutputWordUC)
                randWordOutputFormat = enRandomWordOutputFormat.enUC;
            else
                randWordOutputFormat = enRandomWordOutputFormat.enUnknown;


            return randWordOutputFormat;
        }


        /// <summary>
        /// Creates DataTable containing list of random words.
        /// </summary>
        /// <param name="numRows">Number of rows containg random words to generate.</param>
        /// <param name="minNumWords">Minimum number of random words per row.</param>
        /// <param name="maxNumWords">Maximum number of random words per row.</param>
        /// <param name="randWordOutputFormat">Format for the random words (upper and lower case formats).</param>
        /// <returns>ADO.NET DataTable.</returns>
        public DataTable CreateRandomWordsDataTable(int numRows, string minNumWords, string maxNumWords, enRandomWordOutputFormat randWordOutputFormat)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            PFRandomWord randWord = new PFRandomWord();
            StringBuilder words = new StringBuilder();

            try
            {
                int minimumNumberOfWords = Convert.ToInt32(minNumWords);
                int maximumNumberOfWords = Convert.ToInt32(maxNumWords);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    int numWords = rn.GenerateRandomInt(minimumNumberOfWords, maximumNumberOfWords);
                    words.Length = 0;
                    for (int s = 0; s < numWords; s++)
                    {
                        string str = randWord.GetWord();
                        switch (randWordOutputFormat)
                        {
                            case enRandomWordOutputFormat.enUCLC:
                                if (str.Length > 1)
                                    str = str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
                                else
                                    str = str.Substring(0, 1).ToUpper();
                                break;
                            case enRandomWordOutputFormat.enLC:
                                str = str.ToLower();
                                break;
                            case enRandomWordOutputFormat.enUC:
                                str = str.ToUpper();
                                break;
                            default:
                                break;
                        }
                        words.Append(str);
                        words.Append(" ");
                    }
                    dr[0] = words.ToString();
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
        /// Routine to create random sentences.
        /// </summary>
        /// <param name="numRows">Number of output data rows containing random sentences.</param>
        /// <param name="minNumSentences">Minimum number of random sentences per data row.</param>
        /// <param name="maxNumSentences">Maximum number of random sentences per data row.</param>
        /// <returns>ADO.NET DataTable containing the random sentences.</returns>
        public DataTable CreateRandomSentencesDataTable(int numRows, string minNumSentences, string maxNumSentences)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            RandomSentence randSentence = new RandomSentence();

            try
            {
                int minimumNumberOfSentences = Convert.ToInt32(minNumSentences);
                int maximumNumberOfSentences = Convert.ToInt32(maxNumSentences);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    int numSentences = rn.GenerateRandomInt(minimumNumberOfSentences, maximumNumberOfSentences);
                    string str = randSentence.GenerateSentences(numSentences);
                    dr[0] = str;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomSentencesDataTable routine.\r\n");
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
        /// Routine to create random documents.
        /// </summary>
        /// <param name="numRows">Number of data rows to generate containing random sentences.</param>
        /// <param name="minNumParagraphs">Minimum number of paragraphs in a data row.</param>
        /// <param name="maxNumParagraphs">Maximum number of paragraphs in a data row.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences per paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences per paragraph.</param>
        /// <param name="includeDocumentTitle">If true, a line containing a random phrase that serves as document title is included at the beginning of each random document. If false, no title is generated.</param>
        /// <returns>ADO.NET DataTable containing the random documents.</returns>
        public DataTable CreateRandomDocumentDataTable(int numRows, string minNumParagraphs, string maxNumParagraphs,  string minNumSentencesPerParagraph, string maxNumSentencesPerParagraph, bool includeDocumentTitle)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            PFRandomWord randWord = new PFRandomWord();
            RandomDocument randDocument = new RandomDocument();
            StringBuilder title = new StringBuilder();

            try
            {
                int minimumNumberOfParagraphs = Convert.ToInt32(minNumParagraphs);
                int maximumNumberOfParagraphs = Convert.ToInt32(maxNumParagraphs);
                int minimumNumberOfSentencesPerParagraph = Convert.ToInt32(minNumSentencesPerParagraph);
                int maximumNumberOfSentencesPerParagraph = Convert.ToInt32(maxNumSentencesPerParagraph);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();

                    title.Length = 0;
                    if (includeDocumentTitle)
                    {
                        int numWordsInTitle = rn.GenerateRandomInt((int)3, (int)7);
                        for (int t = 0; t < numWordsInTitle; t++)
                        {
                            if(t>0)
                                title.Append(" ");
                            string temp = randWord.GetWord();
                            temp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(temp.ToLower());
                            title.Append(temp);
                        }
                    }

                    int numParagraphs = rn.GenerateRandomInt(minimumNumberOfParagraphs, maximumNumberOfParagraphs);
                    string str = randDocument.GenerateDocument(numParagraphs, minimumNumberOfSentencesPerParagraph, maximumNumberOfSentencesPerParagraph, title.ToString());
                    
                    
                    dr[0] = str;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomDocumentDataTable routine.\r\n");
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
