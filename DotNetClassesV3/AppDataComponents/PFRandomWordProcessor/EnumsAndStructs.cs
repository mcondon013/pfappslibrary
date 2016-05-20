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
namespace PFRandomWordProcessor
{
    public enum enWordType
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

    public enum enSentenceSyntaxCategory
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

    public struct stSentenceSyntaxDefinition
    {
        public string Syntax;
        public int Frequency;

        public stSentenceSyntaxDefinition(string pSyntax, int pFrequency)
        {
            Syntax = pSyntax;
            Frequency = pFrequency;
        }
    }

    public struct stRandomWordFileDefinition
    {
        public enWordType WordType;
        public string FilePath;

        public stRandomWordFileDefinition(enWordType pWordType, string pFilePath)
        {
            WordType = pWordType;
            FilePath = pFilePath;
        }

    }

}//end namespace
#pragma warning restore 1591
