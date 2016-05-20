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
using PFCollectionsObjects;
using PFTextObjects;
using PFRandomDataExt;

namespace PFRandomWordProcessor
{
    /// <summary>
    /// Class for the generation of random words.
    /// </summary>
    public class PFRandomWord
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private string _defaultWordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private stRandomWordFileDefinition[] _defaultRandomWordFileNames = new stRandomWordFileDefinition[Enum.GetNames(typeof(enWordType)).Length];
        PFList<string> _words = null;
        private int _minListInx = 0;
        private int _maxListInx = 0;
        private RandomNumber _rn = new RandomNumber();

        private string _fileXlatKey = @"vW]NlkNC?.|eE@x7";
        private string _fileXlatIV = @"SlQ36:zYNQD*=RZB";

        //private variables for properties
        private string _wordListFile = string.Empty;
        private enWordType _wordType = enWordType.NotSpecified;
        private StringBuilder _errorMessages = new StringBuilder();
        private int _numErrors = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFRandomWord()
        {
            InitInstance(enWordType.NotSpecified, string.Empty);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFRandomWord(enWordType wordType, string wordListFile)
        {
            InitInstance(wordType, wordListFile);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFRandomWord(string wordListFile)
        {
            InitInstance(enWordType.NotSpecified, wordListFile);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFRandomWord(enWordType wordType)
        {
            InitInstance(wordType, string.Empty);
        }

        private void InitInstance(enWordType wordType, string wordListFile)
        {
            LoadDefaultWordFilePaths();
            if (wordListFile != string.Empty)
                _wordListFile = wordListFile;
            else
                _wordListFile = _defaultRandomWordFileNames[(int)wordType].FilePath;
            _wordType = wordType;
            LoadWordList();
        }

        private void LoadDefaultWordFilePaths()
        {
            _defaultRandomWordFileNames[(int)enWordType.NotSpecified] = new stRandomWordFileDefinition(enWordType.NotSpecified, Path.Combine(_defaultWordListFolder, "Words.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Noun] = new stRandomWordFileDefinition(enWordType.Noun, Path.Combine(_defaultWordListFolder, "Nouns.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Verb] = new stRandomWordFileDefinition(enWordType.Verb, Path.Combine(_defaultWordListFolder, "Verbs.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Adjective] = new stRandomWordFileDefinition(enWordType.Adjective, Path.Combine(_defaultWordListFolder, "Adjectives.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Adverb] = new stRandomWordFileDefinition(enWordType.Adverb, Path.Combine(_defaultWordListFolder, "Adverbs.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Pronoun] = new stRandomWordFileDefinition(enWordType.Pronoun, Path.Combine(_defaultWordListFolder, "Pronouns.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Determiner] = new stRandomWordFileDefinition(enWordType.Determiner, Path.Combine(_defaultWordListFolder, "Determiners.dat"));
            _defaultRandomWordFileNames[(int)enWordType.DeterminerPronoun] = new stRandomWordFileDefinition(enWordType.DeterminerPronoun, Path.Combine(_defaultWordListFolder, "DeterminerPronouns.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Preposition] = new stRandomWordFileDefinition(enWordType.Preposition, Path.Combine(_defaultWordListFolder, "Prepositions.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Conjunction] = new stRandomWordFileDefinition(enWordType.Conjunction, Path.Combine(_defaultWordListFolder, "Conjunctions.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Interjection] = new stRandomWordFileDefinition(enWordType.Interjection, Path.Combine(_defaultWordListFolder, "Interjections.dat"));
            _defaultRandomWordFileNames[(int)enWordType.Interrogative] = new stRandomWordFileDefinition(enWordType.Interrogative, Path.Combine(_defaultWordListFolder, "Interrogatives.dat"));
            _defaultRandomWordFileNames[(int)enWordType.SubordinateConjunction] = new stRandomWordFileDefinition(enWordType.SubordinateConjunction, Path.Combine(_defaultWordListFolder, "SubordinateConjunctions.dat"));
            _defaultRandomWordFileNames[(int)enWordType.CityName] = new stRandomWordFileDefinition(enWordType.CityName, Path.Combine(_defaultWordListFolder, "CityNames.dat"));
            _defaultRandomWordFileNames[(int)enWordType.StateName] = new stRandomWordFileDefinition(enWordType.StateName, Path.Combine(_defaultWordListFolder, "StateNames.dat"));
            _defaultRandomWordFileNames[(int)enWordType.FirstName] = new stRandomWordFileDefinition(enWordType.FirstName, Path.Combine(_defaultWordListFolder, "FirstNames.dat"));
            _defaultRandomWordFileNames[(int)enWordType.FirstNameMale] = new stRandomWordFileDefinition(enWordType.FirstNameMale, Path.Combine(_defaultWordListFolder, "FirstNamesMale.dat"));
            _defaultRandomWordFileNames[(int)enWordType.FirstNameFemale] = new stRandomWordFileDefinition(enWordType.FirstNameFemale, Path.Combine(_defaultWordListFolder, "FirstNamesFemale.dat"));
            _defaultRandomWordFileNames[(int)enWordType.LastName] = new stRandomWordFileDefinition(enWordType.LastName, Path.Combine(_defaultWordListFolder, "LastNames.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BusinessName] = new stRandomWordFileDefinition(enWordType.BusinessName, Path.Combine(_defaultWordListFolder, "Biznames.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BizName3Con_1] = new stRandomWordFileDefinition(enWordType.BizName3Con_1, Path.Combine(_defaultWordListFolder, "BizName3Con_1.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BizName3Con_2] = new stRandomWordFileDefinition(enWordType.BizName3Con_2, Path.Combine(_defaultWordListFolder, "BizName3Con_2.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BizNameSyllable_1] = new stRandomWordFileDefinition(enWordType.BizNameSyllable_1, Path.Combine(_defaultWordListFolder, "BizNameSyllable_1.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BizNameSyllable_2] = new stRandomWordFileDefinition(enWordType.BizNameSyllable_2, Path.Combine(_defaultWordListFolder, "BizNameSyllable_2.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BizNameSuffix] = new stRandomWordFileDefinition(enWordType.BizNameSuffix, Path.Combine(_defaultWordListFolder, "BizNameSuffix.dat"));
            _defaultRandomWordFileNames[(int)enWordType.BizNamePrefix] = new stRandomWordFileDefinition(enWordType.BizNamePrefix, Path.Combine(_defaultWordListFolder, "BizNamePrefix.dat"));
        }

        private void LoadWordList()
        {

            try
            {
                _words = PFList<string>.LoadFromXmlFile(_wordListFile, _fileXlatKey, _fileXlatIV);
                _minListInx = 0;
                _maxListInx = _words.Count - 1;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find word list file: ");
                _msg.Append(_wordListFile);
                _msg.Append(Environment.NewLine);
                _msg.Append(PFTextProcessor.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
                 
        
        }

        //properties

        /// <summary>
        /// WordType property.
        /// </summary>
        public enWordType WordType
        {
            get
            {
                return _wordType;
            }
            set
            {
                _wordType = value;
            }
        }

        /// <summary>
        /// Path to XML file containing the word list to use for this instance.
        /// </summary>
        public string WordListFile
        {
            get
            {
                return _wordListFile;
            }
            set
            {
                _wordListFile = value;
                LoadWordList();
            }
        }

        /// <summary>
        /// ErrorMessages Property.
        /// </summary>
        public string ErrorMessages
        {
            get
            {
                return _errorMessages.ToString();
            }
        }

        /// <summary>
        /// NumErrors Property.
        /// </summary>
        public int NumErrors
        {
            get
            {
                return _numErrors;
            }
            set
            {
                _numErrors = value;
            }
        }

        //methods

        /// <summary>
        /// Routine to verify whether or not the default file paths exist on disk.
        /// </summary>
        /// <returns>Empty string if no errors found. If errors found, error messages with names of missing files are returned.</returns>
        public string VerifyDefaultWordFilePaths()
        {
            StringBuilder errMsg = new StringBuilder();
            int numFilesMissing = 0;

            errMsg.Length = 0;
            errMsg.Append("Number of default word files not found: ");
            errMsg.Append(numFilesMissing.ToString());
            errMsg.Append(Environment.NewLine);

            foreach (stRandomWordFileDefinition fd in _defaultRandomWordFileNames)
            {
                if (File.Exists(fd.FilePath) == false)
                {
                    numFilesMissing++;
                    errMsg.Append(fd.WordType.ToString());
                    errMsg.Append(": ");
                    errMsg.Append(fd.FilePath);
                    errMsg.Append(Environment.NewLine);
                }
            }

            if (numFilesMissing > 0)
            {
                _errorMessages.Append(errMsg.ToString());
                _numErrors++;
            }
            else
            {
                errMsg.Length = 0;
            }

            return errMsg.ToString();
        }

        /// <summary>
        /// Routine to generate a random word.
        /// </summary>
        /// <returns>String containing the generated word.</returns>
        public string GetWord()
        {
            return _words[_rn.GenerateRandomInt(_minListInx, _maxListInx)];
        }

    }//end class
}//end namespace
