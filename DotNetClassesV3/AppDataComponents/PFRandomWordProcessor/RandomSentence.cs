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
using PFRandomDataExt;

namespace PFRandomWordProcessor
{
    /// <summary>
    /// Class for generation of sentences containing random words in grammatical order.
    /// </summary>
    public class RandomSentence
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private stSentenceSyntaxDefinition[] _sentenceSyntaxDefs = {new stSentenceSyntaxDefinition("subject verb",30),
                                                                    new stSentenceSyntaxDefinition("subject verb object",70),
                                                                    new stSentenceSyntaxDefinition("interrogative subject verb",3),
                                                                    new stSentenceSyntaxDefinition("interrogative subject verb object",5),
                                                                    new stSentenceSyntaxDefinition("subject verb conjunction",2),
                                                                    new stSentenceSyntaxDefinition("subject verb object conjunction",3),
                                                                    new stSentenceSyntaxDefinition("subordinateclause subject verb",5),
                                                                    new stSentenceSyntaxDefinition("subordinateclause subject verb object",5),
                                                                    new stSentenceSyntaxDefinition("subject verb subordinateclause",5),
                                                                    new stSentenceSyntaxDefinition("subject verb object subordinateclause",5),
                                               };
        private stSentenceSyntaxDefinition[] _subordinateClauseDefs = {new stSentenceSyntaxDefinition("subject verb",30),
                                                                       new stSentenceSyntaxDefinition("subject verb object",70),
                                                                      };
        private stSentenceSyntaxDefinition[] _nounPhraseSyntaxDefs = {new stSentenceSyntaxDefinition("noun",40),
                                                  new stSentenceSyntaxDefinition("preposition noun",10),
                                                  new stSentenceSyntaxDefinition("preposition determiner noun",10),
                                                  new stSentenceSyntaxDefinition("preposition determiner adjective noun",10),
                                                  new stSentenceSyntaxDefinition("determiner noun",20),
                                                  new stSentenceSyntaxDefinition("determiner adjective noun",20),
                                                  new stSentenceSyntaxDefinition("preposition determinerpronoun noun",10),
                                                  new stSentenceSyntaxDefinition("preposition determinerpronoun adjective noun",10),
                                                  new stSentenceSyntaxDefinition("determinerpronoun noun",20),
                                                  new stSentenceSyntaxDefinition("determinerpronoun adjective noun",20),
                                                  new stSentenceSyntaxDefinition("adjective noun",20),
                                                 };
        private stSentenceSyntaxDefinition[] _subjectSyntaxDefs = {new stSentenceSyntaxDefinition("nounphrase",1000),
														   new stSentenceSyntaxDefinition("pronoun",40),
														   new stSentenceSyntaxDefinition("FirstName",40),
														   new stSentenceSyntaxDefinition("LastName",40),
														   new stSentenceSyntaxDefinition("FirstName LastName",20),
                                                           new stSentenceSyntaxDefinition("LastName BusinessName",10),
                                                           new stSentenceSyntaxDefinition("LastName BusinessName BizNameSuffix",10),
                                                           new stSentenceSyntaxDefinition("BusinessName",10),
                                                           new stSentenceSyntaxDefinition("BusinessName BizNameSuffix",10),
                                                           new stSentenceSyntaxDefinition("BizNameSyllable1 BizNameSuffix",10),
                                                           new stSentenceSyntaxDefinition("BizName3Syllable1+BizNameSyllable2 BizNameSuffix",10),
                                                           new stSentenceSyntaxDefinition("BizName3Con1 BizNameSuffix",3),
                                                           new stSentenceSyntaxDefinition("BizName3Con1+BizNameCon2 BizNameSuffix",3),
                                                           new stSentenceSyntaxDefinition("BizNamePrefix BusinessName BizNameSuffix",10),
                                                           new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1 BizNameSuffix",10),
                                                           new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix",10),
														   };
        private stSentenceSyntaxDefinition[] _verbPhraseSyntaxDefs = {new stSentenceSyntaxDefinition("verb",80),
                                                                      new stSentenceSyntaxDefinition("verb adverb",10),
                                                                      new stSentenceSyntaxDefinition("adverb verb",10),
                                                                      new stSentenceSyntaxDefinition("verbpast",40),
                                                                      new stSentenceSyntaxDefinition("verbpast adverb",10),
                                                                      new stSentenceSyntaxDefinition("adverb verbpast",10),                                                                     
                                                                      new stSentenceSyntaxDefinition("verb",40),
                                                                      new stSentenceSyntaxDefinition("verbfuture adverb",10),
                                                                      new stSentenceSyntaxDefinition("adverb verbfuture",10),                                                                     
                                                                      new stSentenceSyntaxDefinition("verbconditional",20),
                                                                      new stSentenceSyntaxDefinition("verbconditional adverb",10),
                                                                      new stSentenceSyntaxDefinition("adverb verbconditional",10),                                                                     
                                                                     };
        private stSentenceSyntaxDefinition[] _objectSyntaxDefs = {new stSentenceSyntaxDefinition("adjective",25),
														          new stSentenceSyntaxDefinition("nounphrase",1000),
														          new stSentenceSyntaxDefinition("pronoun",60),
														          new stSentenceSyntaxDefinition("FirstName",25),
														          new stSentenceSyntaxDefinition("LastName",25),
														          new stSentenceSyntaxDefinition("FullName",20),
                                                                  new stSentenceSyntaxDefinition("LastName BusinessName",10),
                                                                  new stSentenceSyntaxDefinition("LastName BusinessName BizNameSuffix",10),
                                                                  new stSentenceSyntaxDefinition("BusinessName",10),
                                                                  new stSentenceSyntaxDefinition("BusinessName BizNameSuffix",10),
                                                                  new stSentenceSyntaxDefinition("BizNameSyllable1 BizNameSuffix",10),
                                                                  new stSentenceSyntaxDefinition("BizName3Syllable1+BizNameSyllable2 BizNameSuffix",10),
                                                                  new stSentenceSyntaxDefinition("BizNamePrefix BusinessName BizNameSuffix",10),
                                                                  new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1 BizNameSuffix",10),
                                                                  new stSentenceSyntaxDefinition("BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix",10),
														         };
        private stSentenceSyntaxDefinition[] _sentenceTerminatorDefs = {new stSentenceSyntaxDefinition(". ", 20),
                                                                        new stSentenceSyntaxDefinition(": ", 2),
                                                                        new stSentenceSyntaxDefinition("; ", 2),
                                                                       };

        private PFList<string> _sentenceSyntaxList = new PFList<string>();
        private PFList<string> _subordinateClauseList = new PFList<string>();
        private PFList<string> _nounPhraseSyntaxList = new PFList<string>();
        private PFList<string> _subjectSyntaxList = new PFList<string>();
        private PFList<string> _verbPhraseSyntaxList = new PFList<string>();
        private PFList<string> _objectSyntaxList = new PFList<string>();
        private PFList<string> _sentenceTerminatorList = new PFList<string>();

        private string _defaultWordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private string _wordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private stRandomWordFileDefinition[] _randomWordFileNames = new stRandomWordFileDefinition[Enum.GetNames(typeof(enWordType)).Length];
        private PFRandomWord[] _randomWordGenerators = new PFRandomWord[Enum.GetNames(typeof(enWordType)).Length];

        RandomNumber _rnd = new RandomNumber();

        string[] _thirdPersonSingularPronouns = { "it", "he", "she", "one", "no-one", "whoever" };

        string[] _auxiliaryVerbs = {"can", "could", "dare", "do", "may", "might", "must", "shall", "ought", "would" };

        string[] _letterIsVowel = {"a", "e", "i", "o", "u"};

        //private variables for properties

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomSentence()
        {
            InitInstance(_defaultWordListFolder);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomSentence(string wordListFolder)
        {
            InitInstance(wordListFolder);
        }

        private void InitInstance(string wordListFolder)
        {
            InitSyntaxLists();

            InitWordFileNames(wordListFolder);

            LoadRandomWordGenerators();

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

        private void InitWordFileNames(string wordListFolder)
        {
            _wordListFolder = wordListFolder;
            _randomWordFileNames[(int)enWordType.NotSpecified] = new stRandomWordFileDefinition(enWordType.NotSpecified, Path.Combine(_wordListFolder, "NotSpecified.dat"));
            _randomWordFileNames[(int)enWordType.Noun] = new stRandomWordFileDefinition(enWordType.Noun, Path.Combine(_wordListFolder, "Nouns.dat"));
            _randomWordFileNames[(int)enWordType.Verb] = new stRandomWordFileDefinition(enWordType.Verb, Path.Combine(_wordListFolder, "Verbs.dat"));
            _randomWordFileNames[(int)enWordType.Adjective] = new stRandomWordFileDefinition(enWordType.Adjective, Path.Combine(_wordListFolder, "Adjectives.dat"));
            _randomWordFileNames[(int)enWordType.Adverb] = new stRandomWordFileDefinition(enWordType.Adverb, Path.Combine(_wordListFolder, "Adverbs.dat"));
            _randomWordFileNames[(int)enWordType.Pronoun] = new stRandomWordFileDefinition(enWordType.Pronoun, Path.Combine(_wordListFolder, "Pronouns.dat"));
            _randomWordFileNames[(int)enWordType.Determiner] = new stRandomWordFileDefinition(enWordType.Determiner, Path.Combine(_wordListFolder, "Determiners.dat"));
            _randomWordFileNames[(int)enWordType.DeterminerPronoun] = new stRandomWordFileDefinition(enWordType.DeterminerPronoun, Path.Combine(_wordListFolder, "DeterminerPronouns.dat"));
            _randomWordFileNames[(int)enWordType.Preposition] = new stRandomWordFileDefinition(enWordType.Preposition, Path.Combine(_wordListFolder, "Prepositions.dat"));
            _randomWordFileNames[(int)enWordType.Conjunction] = new stRandomWordFileDefinition(enWordType.Conjunction, Path.Combine(_wordListFolder, "Conjunctions.dat"));
            _randomWordFileNames[(int)enWordType.Interjection] = new stRandomWordFileDefinition(enWordType.Interjection, Path.Combine(_wordListFolder, "Interjections.dat"));
            _randomWordFileNames[(int)enWordType.Interrogative] = new stRandomWordFileDefinition(enWordType.Interrogative, Path.Combine(_wordListFolder, "Interrogatives.dat"));
            _randomWordFileNames[(int)enWordType.SubordinateConjunction] = new stRandomWordFileDefinition(enWordType.SubordinateConjunction, Path.Combine(_wordListFolder, "SubordinateConjunctions.dat"));
            _randomWordFileNames[(int)enWordType.CityName] = new stRandomWordFileDefinition(enWordType.CityName, Path.Combine(_wordListFolder, "CityNames.dat"));
            _randomWordFileNames[(int)enWordType.StateName] = new stRandomWordFileDefinition(enWordType.StateName, Path.Combine(_wordListFolder, "StateNames.dat"));
            _randomWordFileNames[(int)enWordType.FirstName] = new stRandomWordFileDefinition(enWordType.FirstName, Path.Combine(_wordListFolder, "FirstNames.dat"));
            _randomWordFileNames[(int)enWordType.FirstNameMale] = new stRandomWordFileDefinition(enWordType.FirstNameMale, Path.Combine(_wordListFolder, "FirstNamesMale.dat"));
            _randomWordFileNames[(int)enWordType.FirstNameFemale] = new stRandomWordFileDefinition(enWordType.FirstNameFemale, Path.Combine(_wordListFolder, "FirstNamesFemale.dat"));
            _randomWordFileNames[(int)enWordType.LastName] = new stRandomWordFileDefinition(enWordType.LastName, Path.Combine(_wordListFolder, "LastNames.dat"));
            _randomWordFileNames[(int)enWordType.BusinessName] = new stRandomWordFileDefinition(enWordType.BusinessName, Path.Combine(_wordListFolder, "Biznames.dat"));
            _randomWordFileNames[(int)enWordType.BizName3Con_1] = new stRandomWordFileDefinition(enWordType.BizName3Con_1, Path.Combine(_wordListFolder, "BizName3Con_1.dat"));
            _randomWordFileNames[(int)enWordType.BizName3Con_2] = new stRandomWordFileDefinition(enWordType.BizName3Con_2, Path.Combine(_wordListFolder, "BizName3Con_2.dat"));
            _randomWordFileNames[(int)enWordType.BizNameSyllable_1] = new stRandomWordFileDefinition(enWordType.BizNameSyllable_1, Path.Combine(_wordListFolder, "BizNameSyllable_1.dat"));
            _randomWordFileNames[(int)enWordType.BizNameSyllable_2] = new stRandomWordFileDefinition(enWordType.BizNameSyllable_2, Path.Combine(_wordListFolder, "BizNameSyllable_2.dat"));
            _randomWordFileNames[(int)enWordType.BizNameSuffix] = new stRandomWordFileDefinition(enWordType.BizNameSuffix, Path.Combine(_wordListFolder, "BizNameSuffix.dat"));
            _randomWordFileNames[(int)enWordType.BizNamePrefix] = new stRandomWordFileDefinition(enWordType.BizNamePrefix, Path.Combine(_wordListFolder, "BizNamePrefix.dat"));
            _randomWordFileNames[(int)enWordType.SubjectPronoun] = new stRandomWordFileDefinition(enWordType.SubjectPronoun, Path.Combine(_wordListFolder, "SubjectPronouns.dat"));
            _randomWordFileNames[(int)enWordType.ObjectPronoun] = new stRandomWordFileDefinition(enWordType.ObjectPronoun, Path.Combine(_wordListFolder, "ObjectPronouns.dat"));
        }

        private void LoadRandomWordGenerators()
        {
            _randomWordGenerators[(int)enWordType.NotSpecified] = new PFRandomWord(enWordType.NotSpecified, _randomWordFileNames[(int)enWordType.NotSpecified].FilePath);
            _randomWordGenerators[(int)enWordType.Noun] = new PFRandomWord(enWordType.Noun, _randomWordFileNames[(int)enWordType.Noun].FilePath);
            _randomWordGenerators[(int)enWordType.Verb] = new PFRandomWord(enWordType.Verb, _randomWordFileNames[(int)enWordType.Verb].FilePath);

            _randomWordGenerators[(int)enWordType.Adjective] = new PFRandomWord(enWordType.Adjective, _randomWordFileNames[(int)enWordType.Adjective].FilePath);
            _randomWordGenerators[(int)enWordType.Adverb] = new PFRandomWord(enWordType.Adverb, _randomWordFileNames[(int)enWordType.Adverb].FilePath);
            _randomWordGenerators[(int)enWordType.Pronoun] = new PFRandomWord(enWordType.Pronoun, _randomWordFileNames[(int)enWordType.Pronoun].FilePath);
            _randomWordGenerators[(int)enWordType.Determiner] = new PFRandomWord(enWordType.Determiner, _randomWordFileNames[(int)enWordType.Determiner].FilePath);
            _randomWordGenerators[(int)enWordType.DeterminerPronoun] = new PFRandomWord(enWordType.DeterminerPronoun, _randomWordFileNames[(int)enWordType.DeterminerPronoun].FilePath);
            _randomWordGenerators[(int)enWordType.Preposition] = new PFRandomWord(enWordType.Preposition, _randomWordFileNames[(int)enWordType.Preposition].FilePath);
            _randomWordGenerators[(int)enWordType.Conjunction] = new PFRandomWord(enWordType.Conjunction, _randomWordFileNames[(int)enWordType.Conjunction].FilePath);
            _randomWordGenerators[(int)enWordType.Interjection] = new PFRandomWord(enWordType.Interjection, _randomWordFileNames[(int)enWordType.Interjection].FilePath);
            _randomWordGenerators[(int)enWordType.Interrogative] = new PFRandomWord(enWordType.Interrogative, _randomWordFileNames[(int)enWordType.Interrogative].FilePath);
            _randomWordGenerators[(int)enWordType.SubordinateConjunction] = new PFRandomWord(enWordType.SubordinateConjunction, _randomWordFileNames[(int)enWordType.SubordinateConjunction].FilePath);
            _randomWordGenerators[(int)enWordType.CityName] = new PFRandomWord(enWordType.CityName, _randomWordFileNames[(int)enWordType.CityName].FilePath);
            _randomWordGenerators[(int)enWordType.StateName] = new PFRandomWord(enWordType.StateName, _randomWordFileNames[(int)enWordType.StateName].FilePath);
            _randomWordGenerators[(int)enWordType.FirstName] = new PFRandomWord(enWordType.FirstName, _randomWordFileNames[(int)enWordType.FirstName].FilePath);
            _randomWordGenerators[(int)enWordType.FirstNameMale] = new PFRandomWord(enWordType.FirstNameMale, _randomWordFileNames[(int)enWordType.FirstNameMale].FilePath);
            _randomWordGenerators[(int)enWordType.FirstNameFemale] = new PFRandomWord(enWordType.FirstNameFemale, _randomWordFileNames[(int)enWordType.FirstNameFemale].FilePath);
            _randomWordGenerators[(int)enWordType.LastName] = new PFRandomWord(enWordType.LastName, _randomWordFileNames[(int)enWordType.LastName].FilePath);
            _randomWordGenerators[(int)enWordType.BusinessName] = new PFRandomWord(enWordType.BusinessName, _randomWordFileNames[(int)enWordType.BusinessName].FilePath);
            _randomWordGenerators[(int)enWordType.BizName3Con_1] = new PFRandomWord(enWordType.BizName3Con_1, _randomWordFileNames[(int)enWordType.BizName3Con_1].FilePath);
            _randomWordGenerators[(int)enWordType.BizName3Con_2] = new PFRandomWord(enWordType.BizName3Con_2, _randomWordFileNames[(int)enWordType.BizName3Con_2].FilePath);
            _randomWordGenerators[(int)enWordType.BizNameSyllable_1] = new PFRandomWord(enWordType.BizNameSyllable_1, _randomWordFileNames[(int)enWordType.BizNameSyllable_1].FilePath);
            _randomWordGenerators[(int)enWordType.BizNameSyllable_2] = new PFRandomWord(enWordType.BizNameSyllable_2, _randomWordFileNames[(int)enWordType.BizNameSyllable_2].FilePath);
            _randomWordGenerators[(int)enWordType.BizNameSuffix] = new PFRandomWord(enWordType.BizNameSuffix, _randomWordFileNames[(int)enWordType.BizNameSuffix].FilePath);
            _randomWordGenerators[(int)enWordType.BizNamePrefix] = new PFRandomWord(enWordType.BizNamePrefix, _randomWordFileNames[(int)enWordType.BizNamePrefix].FilePath);
            _randomWordGenerators[(int)enWordType.SubjectPronoun] = new PFRandomWord(enWordType.SubjectPronoun, _randomWordFileNames[(int)enWordType.SubjectPronoun].FilePath);
            _randomWordGenerators[(int)enWordType.ObjectPronoun] = new PFRandomWord(enWordType.ObjectPronoun, _randomWordFileNames[(int)enWordType.ObjectPronoun].FilePath);

        }


        //properties

        //methods

        /// <summary>
        /// Produces a sentence using random words arranged in a syntactical order.
        /// </summary>
        /// <returns>String containing random sentence.</returns>
        public string GenerateSentence()
        {
            StringBuilder sentence = new StringBuilder();

            sentence.Append(GenerateSentences((int)1));

            return sentence.ToString();
        }

        /// <summary>
        /// Routine to generate one or more sentences containing random words.
        /// </summary>
        /// <param name="numSentences">Number of sentences to generate.</param>
        /// <returns>String containing generated sentences.</returns>
        public string GenerateSentences(int numSentences)
        {
            StringBuilder sentence = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;
            string terminator = string.Empty;

            for (int s = 0; s < numSentences; s++)
            {
                rndMin = 0;
                rndMax = _sentenceSyntaxDefs.Length - 1;
                rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);

                string sentenceSyntax = _sentenceSyntaxDefs[rndNum].Syntax;
                string newSentenceFragment = BuildSentenceString(sentenceSyntax, ref terminator);
                newSentenceFragment = newSentenceFragment.First().ToString().ToUpper() + String.Join("", newSentenceFragment.Skip(1));
                while (newSentenceFragment.EndsWith("  "))
                {
                    newSentenceFragment = newSentenceFragment.Replace("  "," ");
                }
                sentence.Append(newSentenceFragment);
            }

            return sentence.ToString();
        }

        /// <summary>
        /// Produces a group of sentences consisting of random words arranged according the supported syntax rules.
        /// </summary>
        /// <returns>String containing random sentences.</returns>
        public string GenerateSampleSentences()
        {
            StringBuilder sentence = new StringBuilder();
            int sentenceNumber = 0;
            string prevTerminator = string.Empty;

            sentence.Length = 0;
            for (int s = 0; s < _sentenceSyntaxDefs.Length; s++)
            {
                if (s >= 0 && s <= 7)
                {
                    sentenceNumber++;
                    sentence.Append(sentenceNumber.ToString());
                    sentence.Append(": ");
                    sentence.Append(_sentenceSyntaxDefs[s].Syntax.ToString());
                    sentence.Append(Environment.NewLine);
                    sentence.Append("\t ");

                    //_testRunOnSentence = true;

                    string sentenceSyntax = _sentenceSyntaxDefs[s].Syntax;
                    string newSentenceFragment = BuildSentenceString(sentenceSyntax, ref prevTerminator);
                    newSentenceFragment = newSentenceFragment.First().ToString().ToUpper() + String.Join("", newSentenceFragment.Skip(1));
                    sentence.Append(newSentenceFragment);

                    //sentence.Append("\r\n\t\tprev terminator " + prevTerminator + " causes ");
                    //if (prevTerminator.Trim() != ":" && prevTerminator != ";")
                    //{
                    //    sentence.Append("ToUpper");
                    //}
                    //else
                    //{
                    //    sentence.Append("No change");
                    //}
                    sentence.Append(Environment.NewLine);
                }
            }

            return sentence.ToString();

        }

        //private bool _testRunOnSentence = true;

        private string BuildSentenceString(string sentenceSyntaxDef, ref string prevSentenceTerminator)
        {
            StringBuilder sentence = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;
            bool isDeclarativeSentence = false;
            bool isRunOnSentence = false;
            bool conjunctionSpecified = false;
            string terminator = " ";
            string prevTerminator = string.Empty;
            bool useThirdPersonSingular = false;

            sentence.Length = 0;

            isDeclarativeSentence = true;
            conjunctionSpecified = false;
            string[] sentenceSyntax = sentenceSyntaxDef.Split(' ');
            useThirdPersonSingular = true;
            for (int i = 0; i < sentenceSyntax.Length; i++)
            {

                switch (sentenceSyntax[i])
                {
                    case "subject":
                        sentence.Append(BuildSubjectString(ref useThirdPersonSingular));
                        break;
                    case "verb":
                        sentence.Append(BuildVerbPhrase(useThirdPersonSingular));
                        break;
                    case "object":
                        sentence.Append(BuildObjectString());
                        break;
                    case "conjunction":
                        if (sentence.ToString().EndsWith(" ") == false)
                            sentence.Append(" ");
                        sentence.Append(_randomWordGenerators[(int)enWordType.Conjunction].GetWord().Replace("_", " "));
                        sentence.Append(" ");
                        conjunctionSpecified = true;
                        break;
                    case "subordinateclause":
                        if (i > 0 && sentence.ToString().EndsWith(" ") == false)
                            sentence.Append(" ");
                        sentence.Append(BuildSubordinateClause(ref useThirdPersonSingular));
                        if (i == 0 && sentence.ToString().EndsWith(" ") == false)
                            sentence.Append(", ");
                        break;
                    case "interrogative":
                        sentence.Append(_randomWordGenerators[(int)enWordType.Interrogative].GetWord());
                        sentence.Append(" ");
                        isDeclarativeSentence = false;
                        break;
                    default:
                        sentence.Append("<<< sentence default >>>");
                        sentence.Append(" ");
                        break;
                }
            }


            if (conjunctionSpecified == false)
            {
                string temp = sentence.ToString().TrimEnd(' ');
                sentence.Length = 0;
                sentence.Append(temp);

                if (isDeclarativeSentence)
                {
                    rndMin = 0;
                    rndMax = _sentenceTerminatorList.Count - 1;
                    rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
                    terminator = _sentenceTerminatorList[rndNum];
                    //if (_testRunOnSentence)
                    //{
                    //    _testRunOnSentence = false;
                    //    terminator = ": ";
                    //}
                    if (terminator == "; " || terminator == ": ")
                        isRunOnSentence = true;
                    //terminator += " ";
                }
                else
                {
                    terminator = "? ";
                }
            }
            else
            {
                terminator = " ";
                rndMin = 0;
                rndMax = _sentenceSyntaxDefs.Length - 1;
                rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
                sentence.Append(BuildSentenceString(_sentenceSyntaxList[rndNum], ref terminator));
            }
            sentence.Append(terminator);

            if (isRunOnSentence)
            {
                rndMin = 0;
                rndMax = _sentenceSyntaxDefs.Length - 1;
                rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
                sentence.Append(BuildSentenceString(_sentenceSyntaxList[rndNum], ref terminator));
            }

            prevTerminator = terminator;
            
            return sentence.ToString();

        }
        
        private string BuildSubjectString(ref bool useThirdPersonSingular)
        {
            StringBuilder subject = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _subjectSyntaxList.Count - 1;
            rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
            //subject.Append("BuildSubjectString:");
            //subject.Append("\r\n\t");
            //subject.Append(_subjectSyntaxList[rndNum]);
            //subject.Append("\r\n\t");
            string subjectSyntax = _subjectSyntaxList[rndNum];

            useThirdPersonSingular = true;

            switch (subjectSyntax)
            {
                case "nounphrase":
                    subject.Append(BuildNounPhrase());
                    break;
                case "pronoun":
                    string pronoun = _randomWordGenerators[(int)enWordType.SubjectPronoun].GetWord();
                    subject.Append(pronoun);
                    if (PronounIsThirdPerson(pronoun) == false)
                        useThirdPersonSingular = false;
                    break;
                case "FirstName":
                    subject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    break;
                case "LastName":
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "FirstName LastName":
                    subject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "LastName BusinessName":
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "LastName BusinessName BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BusinessName":
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "BusinessName BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNameSyllable1 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Syllable1+BizNameSyllable2 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Con1 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizName3Con_1].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Con1+BizNameCon2 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizName3Con_1].GetWord());
                    subject.Append(_randomWordGenerators[(int)enWordType.BizName3Con_2].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BusinessName BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix":
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    subject.Append(" ");
                    subject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                default:
                        subject.Append("<<< subject default >>>");
                        subject.Append(" ");
                    break;
            }

            string temp = subject.ToString();
            temp = temp.Replace("<CityName>",_randomWordGenerators[(int)enWordType.CityName].GetWord());
            temp = temp.Replace("<StateName>",_randomWordGenerators[(int)enWordType.StateName].GetWord());
            subject.Length = 0;
            subject.Append(temp);
            
            subject.Append(" ");

            return subject.ToString();
        }

        //routine is not used as of October 2014.
        private bool PronounIsThirdPerson(string pronoun)
        {
            bool ret = false;

            for (int i = 0; i < _thirdPersonSingularPronouns.Length; i++)
            {
                if (_thirdPersonSingularPronouns[i].ToLower() == pronoun.ToLower())
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private string BuildNounPhrase()
        {
            StringBuilder nounPhrase = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _nounPhraseSyntaxList.Count - 1;
            rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
            string nounPhraseSyntax = _nounPhraseSyntaxList[rndNum];

            switch (nounPhraseSyntax)
            {
                case "noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "preposition noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Preposition].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "preposition determiner noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Preposition].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Determiner].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "preposition determiner adjective noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Preposition].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Determiner].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "determiner noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Determiner].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "determiner adjective noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Determiner].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "preposition determinerpronoun noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Preposition].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.DeterminerPronoun].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "preposition determinerpronoun adjective noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Preposition].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.DeterminerPronoun].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "determinerpronoun noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.DeterminerPronoun].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "determinerpronoun adjective noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.DeterminerPronoun].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                case "adjective noun":
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    nounPhrase.Append(" ");
                    nounPhrase.Append(_randomWordGenerators[(int)enWordType.Noun].GetWord());
                    break;
                default:
                        nounPhrase.Append("<<< nounphrase default >>>");
                        nounPhrase.Append(" ");
                    break;
            }
            
            //nounPhrase.Append(" ");

            return nounPhrase.ToString();
        }

        private string BuildVerbPhrase(bool useThirdPersonSingular)
        {
            StringBuilder verb = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _verbPhraseSyntaxList.Count - 1;
            rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
            string verbPhraseSyntax = _verbPhraseSyntaxList[rndNum];
            string randomVerb = _randomWordGenerators[(int)enWordType.Verb].GetWord();
            string modifiedVerb = randomVerb;
            if (WordIsAuxiliaryVerb(randomVerb) == true)
            {
                modifiedVerb = FixAuxiliaryVerb(randomVerb);
            }
            else if (useThirdPersonSingular && WordIsAuxiliaryVerb(randomVerb) == false)
            {
                modifiedVerb = FixThirdPersonVerb(randomVerb);
            }
            else
                modifiedVerb = randomVerb;

            switch (verbPhraseSyntax)
            {
                case "verb":
                    verb.Append(modifiedVerb);
                    break;
                case "verb adverb":
                    verb.Append(modifiedVerb);
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verb":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append(modifiedVerb);
                    break;
                case "verbpast":
                    verb.Append("did ");
                    verb.Append(randomVerb.Replace("will", "try"));
                    break;
                case "verbpast adverb":
                    verb.Append("did ");
                    verb.Append(randomVerb.Replace("will", "try"));
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verbpast":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append("did ");
                    verb.Append(randomVerb.Replace("will", "try"));
                    break;
                case "verbfuture":
                    verb.Append("will ");
                    verb.Append(randomVerb.Replace("will", "aim"));
                    break;
                case "verbfuture adverb":
                    verb.Append("will ");
                    verb.Append(randomVerb.Replace("will", "aim"));
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verbfuture":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append("will ");
                    verb.Append(randomVerb.Replace("will","aim"));
                    break;
                case "verbconditional":
                    verb.Append("should ");
                    verb.Append(randomVerb.Replace("should", "plan"));
                    break;
                case "verbconditional adverb":
                    verb.Append("should ");
                    verb.Append(randomVerb.Replace("should", "plan"));
                    verb.Append(" ");
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    break;
                case "adverb verbconditional":
                    verb.Append(_randomWordGenerators[(int)enWordType.Adverb].GetWord());
                    verb.Append(" ");
                    verb.Append("should ");
                    verb.Append(randomVerb.Replace("should", "plan"));
                    break;
                default:
                        verb.Append("<<< verb default >>>");
                        verb.Append(" ");
                    break;
            }

            verb.Append(" ");

            return verb.ToString();
        }

        private bool WordIsAuxiliaryVerb(string word)
        {
            bool ret = false;

            for (int i = 0; i < _auxiliaryVerbs.Length; i++)
            {
                if (_auxiliaryVerbs[i] == word)
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private string FixAuxiliaryVerb(string verbToFix)
        {
            string ret = verbToFix;

            switch (verbToFix)
            {
                case "ought":
                    ret = verbToFix + " to " + GetNonAuxiliaryVerb(verbToFix);
                    break;
                default:
                    ret = verbToFix + " " + GetNonAuxiliaryVerb(verbToFix);;
                    break;
            }


            return ret;
        }

        private string GetNonAuxiliaryVerb(string auxiliaryVerb)
        {
            string ret = string.Empty;
            string newVerb = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                newVerb = _randomWordGenerators[(int)enWordType.Verb].GetWord();
                if (WordIsAuxiliaryVerb(newVerb) == false)
                {
                    ret = newVerb;
                    break;
                }
            }

            if (ret == string.Empty)
            {
                ret = "translate into esperanto";
            }

            return ret;
        }
        
        private string FixThirdPersonVerb(string originalVerb)
        {
            string newVerb = originalVerb;

            switch (originalVerb)
            {
                case "be":
                    newVerb = "is";
                    break;
                case "have":
                    newVerb = "has";
                    break;
                case "do":
                    newVerb = "does";
                    break;
                case "can":
                case "could":
                case "would":
                case "should":
                case "shall":
                case "must":
                case "might":
                case "may":
                case "ought":
                    newVerb = originalVerb;
                    break;
                default:
                    if (originalVerb.EndsWith("s") || originalVerb.EndsWith("h"))
                        newVerb = originalVerb + "es";
                    else if (originalVerb.EndsWith("y"))
                    {
                        if (originalVerb.EndsWith("ay")
                            || originalVerb.EndsWith("ey")
                            || originalVerb.EndsWith("iy")
                            || originalVerb.EndsWith("oy")
                            || originalVerb.EndsWith("uy"))
                            newVerb = originalVerb + "s";
                        else
                        {
                            newVerb = originalVerb.TrimEnd('y') + "ies";  //fixed 3/2016 by MC
                        }
                    }
                    else
                        newVerb = originalVerb + "s";
                    break;
            }

            return newVerb;
        }

        private string BuildObjectString()
        {
            StringBuilder sObject = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            rndMax = _objectSyntaxList.Count - 1;
            rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
            string objectSyntax = _objectSyntaxList[rndNum];

            switch (objectSyntax)
            {
                case "adjective":
                    sObject.Append(_randomWordGenerators[(int)enWordType.Adjective].GetWord());
                    break;
                case "nounphrase":
                    sObject.Append(BuildNounPhrase());
                    break;
                case "pronoun":
                    sObject.Append(_randomWordGenerators[(int)enWordType.ObjectPronoun].GetWord());
                    break;
                case "FirstName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    break;
                case "LastName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "FullName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.FirstName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    break;
                case "LastName BusinessName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "LastName BusinessName BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.LastName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BusinessName":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    break;
                case "BusinessName BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNameSyllable1 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizName3Syllable1+BizNameSyllable2 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BusinessName BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BusinessName].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                case "BizNamePrefix BizNameSyllable1+BizNameSyllable2 BizNameSuffix":
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNamePrefix].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_1].GetWord());
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSyllable_2].GetWord());
                    sObject.Append(" ");
                    sObject.Append(_randomWordGenerators[(int)enWordType.BizNameSuffix].GetWord());
                    break;
                default:
                    sObject.Append("<<< object default >>>");
                    sObject.Append(" ");
                    break;
            }

            string temp = sObject.ToString();
            temp = temp.Replace("<CityName>", _randomWordGenerators[(int)enWordType.CityName].GetWord());
            temp = temp.Replace("<StateName>", _randomWordGenerators[(int)enWordType.StateName].GetWord());
            sObject.Length = 0;
            sObject.Append(temp);
            


            return sObject.ToString();
        }

        private string BuildSubordinateClause(ref bool useThirdPersonSingular)
        {
            StringBuilder subordinateClause = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            subordinateClause.Append(_randomWordGenerators[(int)enWordType.SubordinateConjunction].GetWord().Replace("_"," "));
            subordinateClause.Append(" ");

            rndMax = _subordinateClauseList.Count - 1;
            rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
            string subordinateClauseSyntax = _subordinateClauseList[rndNum];
            useThirdPersonSingular = true;

            string[] subordinateClauseElements = subordinateClauseSyntax.Split(' ');
            for (int i = 0; i < subordinateClauseElements.Length; i++)
            {
                switch (subordinateClauseElements[i])
                {
                    case "subject":
                        subordinateClause.Append(BuildSubjectString(ref useThirdPersonSingular));
                        break;
                    case "verb":
                        subordinateClause.Append(BuildVerbPhrase(useThirdPersonSingular));
                        break;
                    case "object":
                        subordinateClause.Append(BuildObjectString());
                        break;
                    default:
                        subordinateClause.Append("<<< subordinateClause default >>>");
                        subordinateClause.Append(" ");
                        break;
                }
            }

            return subordinateClause.ToString();
        }

    }//end class
}//end namespace
