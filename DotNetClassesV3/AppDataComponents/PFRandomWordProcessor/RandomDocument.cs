//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFRandomDataExt;

namespace PFRandomWordProcessor
{
    /// <summary>
    /// Class for producing documents containing sentences build from random words.
    /// </summary>
    public class RandomDocument
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private string _defaultWordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private string _wordListFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        RandomSentence _randomSentence = null;
        RandomNumber _rnd = new RandomNumber();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomDocument()
        {
            InitInstance(_defaultWordListFolder);
        }

             /// <summary>
        /// Constructor.
        /// </summary>
        public RandomDocument(string wordListFolder)
        {
            InitInstance(wordListFolder);
        }

        private void InitInstance(string wordListFolder)
        {
            _wordListFolder = wordListFolder;
            _randomSentence = new RandomSentence(wordListFolder);
        }


        //properties

        //methods

        /// <summary>
        /// Routine to generate sentences containing random words organized into chapters and books.
        /// </summary>
        /// <param name="bookTitle">Title text to be inserted at the beginning of the book's text.</param>
        /// <param name="numChapters">Total number of chapters to produce for the book.</param>
        /// <param name="minNumParagraphsPerChapter">Minimum number of paragraphs to generate for a chapter.</param>
        /// <param name="maxNumParagraphsPerChapter">Maximum number of paragraphs to generate for a chapter.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <returns>String containing random text in chapter and book format.</returns>
        public string GenerateBook(string bookTitle,
                           int numChapters,
                           int minNumParagraphsPerChapter,
                           int maxNumParagraphsPerChapter,
                           int minNumSentencesPerParagraph,
                           int maxNumSentencesPerParagraph)
        {
            return GenerateBook(bookTitle, string.Empty, numChapters, null, minNumParagraphsPerChapter, maxNumParagraphsPerChapter, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
        }

        /// <summary>
        /// Routine to generate sentences containing random words organized into chapters and books.
        /// </summary>
        /// <param name="bookTitle">Title text to be inserted at the beginning of the book's text.</param>
        /// <param name="numChapters">Total number of chapters to produce for the book.</param>
        /// <param name="pChapterTitles">Array of strings containing titles for chapters. Omit or set to null to allow generation of default chapter titles.</param>
        /// <param name="minNumParagraphsPerChapter">Minimum number of paragraphs to generate for a chapter.</param>
        /// <param name="maxNumParagraphsPerChapter">Maximum number of paragraphs to generate for a chapter.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <returns>String containing random text in chapter and book format.</returns>
        public string GenerateBook(string bookTitle,
                                   int numChapters,
                                   string[] pChapterTitles,
                                   int minNumParagraphsPerChapter,
                                   int maxNumParagraphsPerChapter,
                                   int minNumSentencesPerParagraph,
                                   int maxNumSentencesPerParagraph)
        {
            return GenerateBook(bookTitle, string.Empty, numChapters, pChapterTitles, minNumParagraphsPerChapter, maxNumParagraphsPerChapter, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
        }

                /// <summary>
        /// Routine to generate sentences containing random words organized into chapters and books.
        /// </summary>
        /// <param name="bookTitle">Title text to be inserted at the beginning of the book's text.</param>
        /// <param name="appendTheEndToBook">Phrase to be appended to the book text.</param>
        /// <param name="numChapters">Total number of chapters to produce for the book.</param>
        /// <param name="minNumParagraphsPerChapter">Minimum number of paragraphs to generate for a chapter.</param>
        /// <param name="maxNumParagraphsPerChapter">Maximum number of paragraphs to generate for a chapter.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <returns>String containing random text in chapter and book format.</returns>
        public string GenerateBook(string bookTitle,
                                   string appendTheEndToBook,
                                   int numChapters,
                                   int minNumParagraphsPerChapter,
                                   int maxNumParagraphsPerChapter,
                                   int minNumSentencesPerParagraph,
                                   int maxNumSentencesPerParagraph)
        {
            return GenerateBook(bookTitle, appendTheEndToBook, numChapters, null, minNumParagraphsPerChapter, maxNumParagraphsPerChapter, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
        }
        
        /// <summary>
        /// Routine to generate sentences containing random words organized into chapters and books.
        /// </summary>
        /// <param name="bookTitle">Title text to be inserted at the beginning of the book's text.</param>
        /// <param name="appendTheEndToBook">Phrase to be appended to the book text.</param>
        /// <param name="numChapters">Total number of chapters to produce for the book.</param>
        /// <param name="pChapterTitles">Array of strings containing titles for chapters. Omit or set to null to allow generation of default chapter titles.</param>
        /// <param name="minNumParagraphsPerChapter">Minimum number of paragraphs to generate for a chapter.</param>
        /// <param name="maxNumParagraphsPerChapter">Maximum number of paragraphs to generate for a chapter.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <returns>String containing random text in chapter and book format.</returns>
        public string GenerateBook(string bookTitle,
                                   string appendTheEndToBook,
                                   int numChapters, 
                                   string[] pChapterTitles, 
                                   int minNumParagraphsPerChapter, 
                                   int maxNumParagraphsPerChapter, 
                                   int minNumSentencesPerParagraph, 
                                   int maxNumSentencesPerParagraph)
        {
            StringBuilder book = new StringBuilder();
            string[] chapterTitles = null;
            int rndMin = 0;
            int rndMax = 0;
            //int rndNum = 0;

            if (String.IsNullOrEmpty(bookTitle) == false)
            {
                book.Append(bookTitle);
            }
            else
            {
                book.Append("Book");
            }
            book.Append(Environment.NewLine);
            book.Append(Environment.NewLine);

            if (pChapterTitles == null)
            {
                chapterTitles = InitChapterTitles(numChapters);
            }
            else
            {
                chapterTitles = pChapterTitles;
            }

            int maxChapterTitleInx = chapterTitles.Length - 1;
            string chapterTitle = string.Empty;
            for (int c = 0; c < numChapters; c++)
            {
                if (c > maxChapterTitleInx)
                {
                    chapterTitle = "Chapter " + (c + 1).ToString();
                }
                else
                {
                    chapterTitle = chapterTitles[c];
                }
                rndMin = minNumParagraphsPerChapter;
                rndMax = maxNumParagraphsPerChapter;
                int numParagraphs = _rnd.GenerateRandomInt(rndMin, rndMax);
                book.Append(GenerateChapter(chapterTitle, numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph));

            }//end chapter loop


            if (String.IsNullOrEmpty(appendTheEndToBook)==false)
            {
                book.Append(Environment.NewLine);
                book.Append(Environment.NewLine);
                book.Append(appendTheEndToBook);
            }

            return book.ToString();
        }

        private string[] InitChapterTitles(int numChapters)
        {
            string[] chapterTitles = new string[numChapters];

            for (int c = 0; c < numChapters; c++)
            {
                chapterTitles[c] = "Chapter " + (c+1).ToString();
            }

            return chapterTitles;
        }

        /// <summary>
        /// Routine to generate one or more paragraphs organized as a book chapter and containing random words in random sentences.
        /// </summary>
        /// <param name="numParagraphs">Number of paragraphs to produce.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <returns>String containing random text organized into paragraphs and random sentences.</returns>
        public string GenerateChapter(int numParagraphs, int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph)
        {
            return GenerateChapter(string.Empty, numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph);
        }

        /// <summary>
        /// Routine to generate one or more paragraphs organized as a book chapter and containing random words in random sentences.
        /// </summary>
        /// <param name="chapterTitle">Title text to be placed at beginning of a chapter.</param>
        /// <param name="numParagraphs">Number of paragraphs to produce.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <returns>String containing random text organized into paragraphs and random sentences.</returns>
        public string GenerateChapter(string chapterTitle, int numParagraphs, int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph)
        {
            StringBuilder chapter = new StringBuilder();

            if (String.IsNullOrEmpty(chapterTitle) == false)
            {
                chapter.Append(chapterTitle);
                chapter.Append(Environment.NewLine);
                chapter.Append(Environment.NewLine);
            }

            chapter.Append(GenerateDocument(numParagraphs, minNumSentencesPerParagraph, maxNumSentencesPerParagraph, string.Empty));

            return chapter.ToString();
        }

        /// <summary>
        /// Routine to generate one or more paragraphs containing random words in random sentences.
        /// </summary>
        /// <param name="numParagraphs">Number of paragraphs to produce.</param>
        /// <param name="minNumSentencesPerParagraph">Minimum number of sentences in a paragraph.</param>
        /// <param name="maxNumSentencesPerParagraph">Maximum number of sentences in a paragraph.</param>
        /// <param name="documentSubject">Text to be displayed in subject line of the document. Omit this parameter to exclude subject line from the document.</param>
        /// <returns>String containing the random paragraphs.</returns>
        /// <remarks>Use the minNumSentencesPerParagraph and maxNumSentencesPerParagraph to produce a document in which paragraphs have varying numbers of sentences as opposed to having same number of sentences in each paragraph.</remarks>
        public string GenerateDocument(int numParagraphs, int minNumSentencesPerParagraph, int maxNumSentencesPerParagraph, string documentSubject)
        {
            StringBuilder document = new StringBuilder();
            int rndMin = 0;
            int rndMax = 0;
            int rndNum = 0;

            if (String.IsNullOrEmpty(documentSubject) == false)
            {
                document.Append("SUBJECT: ");
                document.Append(documentSubject);
                document.Append(Environment.NewLine);
                document.Append(Environment.NewLine);
            }

            rndMin = minNumSentencesPerParagraph;
            rndMax = maxNumSentencesPerParagraph;

            for (int p = 0; p < numParagraphs; p++)
            {
                rndNum = _rnd.GenerateRandomInt(rndMin, rndMax);
                document.Append(GenerateParagraph(rndNum));
                document.Append(Environment.NewLine);
            }


            return document.ToString();
        }

        /// <summary>
        /// Generates one or more sentences containing random words organized as sentences in a paragraph.
        /// </summary>
        /// <param name="numSentences">Number of sentences to generate.</param>
        ///<returns>String value.</returns>
        public string GenerateParagraph(int numSentences)
        {
            StringBuilder paragraph = new StringBuilder();

            paragraph.Append(_randomSentence.GenerateSentences(numSentences));
            paragraph.Append(Environment.NewLine);

            return paragraph.ToString();
        }

    }//end class
}//end namespace
