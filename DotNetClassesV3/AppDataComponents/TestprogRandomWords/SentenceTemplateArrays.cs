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
using AppGlobals;
using PFMessageLogs;
using PFCollectionsObjects;
using PFRandomDataExt;
using System.Globalization;

namespace TestprogRandomWords
{
    /// <summary>
    /// Contains sets of definitions used to build random sentences that have correct structure but randomized wordsc.
    /// </summary>
    public class SentenceTemplateArrays
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;

        private enum enWordType
        {
            NotSpecified = 0,
            Noun = 1,
            Verb = 2,
            Adjective = 3,
            Adverb = 4,
            Pronoun = 5,
            Determiner = 6,
            DeterminerPronoun = 7,
            Preposition = 8,
            Conjunction = 9,
            Interjection = 10,
            Interrogative = 11,
            SubordinateConjunction = 12,
            CityName = 13,
            StateName = 14,
            FirstName = 15,
            FirstNameMale = 16,
            FirstNameFemale = 17,
            LastName = 18,
            BusinessName = 19,
            BizName3Con_1 = 20,
            BizName3Con_2 = 21,
            BizNameSyllable_1 = 22,
            BizNameSyllable_2 = 23,
            BizNameSuffix = 24,
            BizNamePrefix = 25,
            SubjectPronoun = 26,
            ObjectPronoun = 27
        }

        private enum enSentenceSyntaxCategory
        {
            Sentence = 0,
            SubordinateClause = 1,
            NounPhrase = 2,
            Subject = 3,
            VerbPhrase = 4,
            Object = 5,
            SentenceTerminator = 6,
            Unknown = 99,
        }

        private struct stSentenceSyntaxDefinition
        {
            public string Syntax;
            public int Frequency;

            public stSentenceSyntaxDefinition(string pSyntax, int pFrequency)
            {
                Syntax = pSyntax;
                Frequency = pFrequency;
            }
        }

        private struct stRandomWordFileDefinition
        {
            public enWordType WordType;
            public string FilePath;

            public stRandomWordFileDefinition(enWordType pWordType, string pFilePath)
            {
                WordType = pWordType;
                FilePath = pFilePath;
            }

        }


        private stSentenceSyntaxDefinition[] _sentenceSyntaxDefs = {new stSentenceSyntaxDefinition("subject verb",20),
                                                                    new stSentenceSyntaxDefinition("subject verb object",30),
                                                                    new stSentenceSyntaxDefinition("interrogative subject verb",1),
                                                                    new stSentenceSyntaxDefinition("interrogative subject verb object",2),
                                                                    new stSentenceSyntaxDefinition("subject verb conjunction",1),
                                                                    new stSentenceSyntaxDefinition("subject verb object conjunction",1),
                                                                    new stSentenceSyntaxDefinition("subordinateclause subject verb",2),
                                                                    new stSentenceSyntaxDefinition("subordinateclause subject verb object",3),
                                                                    new stSentenceSyntaxDefinition("subject verb subordinateclause",3),
                                                                    new stSentenceSyntaxDefinition("subject verb object subordinateclause",3),
                                               };
        private stSentenceSyntaxDefinition[] _subordinateClauseDefs = {new stSentenceSyntaxDefinition("subject verb",3),
                                                                       new stSentenceSyntaxDefinition("subject verb object",7),
                                                                      };
        private stSentenceSyntaxDefinition[] _nounPhraseSyntaxDefs = {new stSentenceSyntaxDefinition("noun",4),
                                                  new stSentenceSyntaxDefinition("preposition noun",1),
                                                  new stSentenceSyntaxDefinition("preposition determiner noun",1),
                                                  new stSentenceSyntaxDefinition("preposition determiner adjective noun",1),
                                                  new stSentenceSyntaxDefinition("determiner noun",2),
                                                  new stSentenceSyntaxDefinition("determiner adjective noun",2),
                                                  new stSentenceSyntaxDefinition("preposition determinerpronoun noun",1),
                                                  new stSentenceSyntaxDefinition("preposition determinerpronoun adjective noun",1),
                                                  new stSentenceSyntaxDefinition("determinerpronoun noun",2),
                                                  new stSentenceSyntaxDefinition("determinerpronoun adjective noun",2),
                                                  new stSentenceSyntaxDefinition("adjective noun",2),
                                                 };
        private stSentenceSyntaxDefinition[] _subjectSyntaxDefs = {new stSentenceSyntaxDefinition("nounphrase",100),
														   new stSentenceSyntaxDefinition("pronoun",4),
														   new stSentenceSyntaxDefinition("FirstName",4),
														   new stSentenceSyntaxDefinition("LastName",4),
														   new stSentenceSyntaxDefinition("FirstName LastName",2),
                                                           new stSentenceSyntaxDefinition("LastName BusinessName",1),
                                                           new stSentenceSyntaxDefinition("LastName BusinessName BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BusinessName",1),
                                                           new stSentenceSyntaxDefinition("BusinessName BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizNameSyllable1 BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizName3Syllable1+BizNameSyllable2 BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizName3Con1 BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizName3Con1+BizNameCon2 BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizNamePrefix BusinessName BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1 BizNameSuffix",1),
                                                           new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix",1),
														   };
        private stSentenceSyntaxDefinition[] _verbPhraseSyntaxDefs = {new stSentenceSyntaxDefinition("verb",8),
                                                                      new stSentenceSyntaxDefinition("verb adverb",1),
                                                                      new stSentenceSyntaxDefinition("adverb verb",1),
                                                                      new stSentenceSyntaxDefinition("verbpast",4),
                                                                      new stSentenceSyntaxDefinition("verbpast adverb",1),
                                                                      new stSentenceSyntaxDefinition("adverb verbpast",1),                                                                     
                                                                      new stSentenceSyntaxDefinition("verb",4),
                                                                      new stSentenceSyntaxDefinition("verbfuture adverb",1),
                                                                      new stSentenceSyntaxDefinition("adverb verbfuture",1),                                                                     
                                                                      new stSentenceSyntaxDefinition("verbconditional",2),
                                                                      new stSentenceSyntaxDefinition("verbconditional adverb",1),
                                                                      new stSentenceSyntaxDefinition("adverb verbconditional",1),                                                                     
                                                                     };
        private stSentenceSyntaxDefinition[] _objectSyntaxDefs = {new stSentenceSyntaxDefinition("adjective",3),
														          new stSentenceSyntaxDefinition("nounphrase",100),
														          new stSentenceSyntaxDefinition("pronoun",6),
														          new stSentenceSyntaxDefinition("FirstName",3),
														          new stSentenceSyntaxDefinition("LastName",3),
														          new stSentenceSyntaxDefinition("FullName",2),
                                                                  new stSentenceSyntaxDefinition("LastName BusinessName",1),
                                                                  new stSentenceSyntaxDefinition("LastName BusinessName BizNameSuffix",1),
                                                                  new stSentenceSyntaxDefinition("BusinessName",1),
                                                                  new stSentenceSyntaxDefinition("BusinessName BizNameSuffix",1),
                                                                  new stSentenceSyntaxDefinition("BizNameSyllable1 BizNameSuffix",1),
                                                                  new stSentenceSyntaxDefinition("BizName3Syllable1+BizNameSyllable2 BizNameSuffix",1),
                                                                  new stSentenceSyntaxDefinition("BizNamePrefix BusinessName BizNameSuffix",1),
                                                                  new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1 BizNameSuffix",1),
                                                                  new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix",1),
														         };
        private stSentenceSyntaxDefinition[] _sentenceTerminatorDefs = {new stSentenceSyntaxDefinition(". ", 10),
                                                                        new stSentenceSyntaxDefinition(": ", 1),
                                                                        new stSentenceSyntaxDefinition("; ", 1),
                                                                       };

        private PFList<string> _sentenceSyntaxList = new PFList<string>();
        private PFList<string> _subordinateClauseList = new PFList<string>();
        private PFList<string> _nounPhraseSyntaxList = new PFList<string>();
        private PFList<string> _subjectSyntaxList = new PFList<string>();
        private PFList<string> _verbPhraseSyntaxList = new PFList<string>();
        private PFList<string> _objectSyntaxList = new PFList<string>();
        private PFList<string> _sentenceTerminatorList = new PFList<string>();

        //private string _defaultWordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        //private string _wordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        //private stRandomWordFileDefinition[] _randomWordFileNames = new stRandomWordFileDefinition[Enum.GetNames(typeof(enWordType)).Length];
        //private PFRandomWord[] _randomWordGenerators = new PFRandomWord[Enum.GetNames(typeof(enWordType)).Length];

        RandomNumber _rnd = new RandomNumber();

        private string[] _thirdPersonSingularPronouns = { "it", "he", "she", "one", "no-one", "whoever" };

        private string[] _auxiliaryVerbs = { "can", "could", "dare", "do", "may", "might", "must", "shall", "ought", "would" };

        private string[] _letterIsVowel = { "a", "e", "i", "o", "u" };

        private string _declareClassBegin = "public class SentenceTemplateArrays\r\n" + "{\r\n";
        private string _declareArrayBegin = "    public string[] <array_name> = \r\n";
        private string _declareArraryBegin2 = "                        {\r\n";
        private string _declareArrayIndent = "                         ";
        private string _declareArrayNode = "\"<node_value>\", ";
        private string _declareArrayEnd = "                        };\r\n";
        private string _declareClassEnd = "}//End Class\r\n";


        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SentenceTemplateArrays()
        {
            InitSyntaxLists();
        }


        private void InitSyntaxLists()
        {

            for (int i = 0; i < _sentenceSyntaxDefs.Length; i++)
            {
                for (int f = 0; f < _sentenceSyntaxDefs[i].Frequency; f++)
                {
                    _sentenceSyntaxList.Add(_sentenceSyntaxDefs[i].Syntax);
                }

            }

            for (int i = 0; i < _subordinateClauseDefs.Length; i++)
            {
                for (int f = 0; f < _subordinateClauseDefs[i].Frequency; f++)
                {
                    _subordinateClauseList.Add(_subordinateClauseDefs[i].Syntax);
                }

            }

            for (int i = 0; i < _nounPhraseSyntaxDefs.Length; i++)
            {
                for (int f = 0; f < _nounPhraseSyntaxDefs[i].Frequency; f++)
                {
                    _nounPhraseSyntaxList.Add(_nounPhraseSyntaxDefs[i].Syntax);
                }

            }

            for (int i = 0; i < _subjectSyntaxDefs.Length; i++)
            {
                for (int f = 0; f < _subjectSyntaxDefs[i].Frequency; f++)
                {
                    _subjectSyntaxList.Add(_subjectSyntaxDefs[i].Syntax);
                }

            }

            for (int i = 0; i < _verbPhraseSyntaxDefs.Length; i++)
            {
                for (int f = 0; f < _verbPhraseSyntaxDefs[i].Frequency; f++)
                {
                    _verbPhraseSyntaxList.Add(_verbPhraseSyntaxDefs[i].Syntax);
                }

            }

            for (int i = 0; i < _objectSyntaxDefs.Length; i++)
            {
                for (int f = 0; f < _objectSyntaxDefs[i].Frequency; f++)
                {
                    _objectSyntaxList.Add(_objectSyntaxDefs[i].Syntax);
                }

            }

            for (int i = 0; i < _sentenceTerminatorDefs.Length; i++)
            {
                for (int f = 0; f < _sentenceTerminatorDefs[i].Frequency; f++)
                {
                    _sentenceTerminatorList.Add(_sentenceTerminatorDefs[i].Syntax);
                }

            }

        }


        //properties

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }

        //methods

        public void GenerateArrayCode(MessageLog pMessageLog)
        {
            _messageLog = pMessageLog;

            try
            {
                _messageLog.Clear();

                WriteMessageToLog(_declareClassBegin);

                GenerateSyntaxArrayCode("SentenceSyntaxDefs", _sentenceSyntaxList, 5);
                GenerateSyntaxArrayCode("SubordinateClauseDefs", _subordinateClauseList, 5);
                GenerateSyntaxArrayCode("NounPhraseSyntaxDefs", _nounPhraseSyntaxList, 5);
                GenerateSyntaxArrayCode("SubjectSyntaxDefs", _subjectSyntaxList, 5);
                GenerateSyntaxArrayCode("VerbPhraseSyntaxDefs", _verbPhraseSyntaxList, 5);
                GenerateSyntaxArrayCode("ObjectSyntaxDefs", _objectSyntaxList, 5);
                GenerateSyntaxArrayCode("SentenceTerminatorDefs", _sentenceTerminatorList, 5);
                GenerateHelperArrays();

                WriteMessageToLog(_declareClassEnd);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
                 
        }

        private void GenerateSyntaxArrayCode(string arrayName, PFList<string> syntaxDefsList, int modulusNum)
        {
            _str.Length = 0;
            _str.Append(_declareArrayBegin.Replace("<array_name>", arrayName));
            _str.Append(_declareArraryBegin2);
            int maxListInx = syntaxDefsList.Count - 1;
            for (int listInx = 0; listInx <= maxListInx; listInx++)
            {
                if ((listInx % modulusNum) == 0)
                {
                    if (listInx > 0)
                        _str.Append(Environment.NewLine);
                    _str.Append(_declareArrayIndent);
                }
                string template = RemoveDiacritics(syntaxDefsList[listInx]);
                template = template.TrimEnd();
                if (listInx < maxListInx)
                {
                    _str.Append(_declareArrayNode.Replace("<node_value>", template));
                }
                else
                {
                    _str.Append(_declareArrayNode.Replace("<node_value>", template).Replace(",", ""));
                }
            }
            _str.Append(Environment.NewLine);
            _str.Append(_declareArrayEnd);
            _str.Append(Environment.NewLine);
            _str.Append(Environment.NewLine);
            WriteMessageToLog(_str.ToString());
            //File.AppendAllText(outputFile, _str.ToString());
        }

        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private string RemoveAccents(string text)
        {
            //string accentedStr;
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(text);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            return asciiStr;
        }

        private void GenerateHelperArrays()
        {
            string x1 = @"    public string[] ThirdPersonSingularPronouns = { ""it"", ""he"", ""she"", ""one"", ""no-one"", ""whoever"" };";

            string x2 = @"    public string[] AuxiliaryVerbs = { ""can"", ""could"", ""dare"", ""do"", ""may"", ""might"", ""must"", ""shall"", ""ought"", ""would"" };";

            string x3 = @"    public string[] LetterIsVowel = { ""a"", ""e"", ""i"", ""o"", ""u"" };";

            _str.Length = 0;
            _str.Append(x1);
            _str.Append(Environment.NewLine);
            _str.Append(Environment.NewLine);
            _str.Append(x2);
            _str.Append(Environment.NewLine);
            _str.Append(Environment.NewLine);
            _str.Append(x3);
            _str.Append(Environment.NewLine);
            _str.Append(Environment.NewLine);

            WriteMessageToLog(_str.ToString());
        }

        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }


    }//end class
}//end namespace
