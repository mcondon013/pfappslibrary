using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFProcessObjects;
using PFMessageLogs;
using PFRandomWordProcessor;
using PFTimers;
using PFCollectionsObjects;
using PFRandomDataExt;

namespace TestprogRandomWords
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";

        private string _helpFilePath = string.Empty;

        private MainForm _frm = null;

        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

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

        /// <summary>
        /// Path to application help file.
        /// </summary>
        public string HelpFilePath
        {
            get
            {
                return _helpFilePath;
            }
            set
            {
                _helpFilePath = value;
            }
        }

        /// <summary>
        /// Path to application main input form.
        /// </summary>
        public MainForm MainInputForm
        {
            get
            {
                return _frm;
            }
            set
            {
                _frm = value;
            }
        }


        //application routines

        /// <summary>
        /// Routine to add context menu item for pfFolderSize to Windows Explorer.
        /// </summary>
        public void ShowAppConfigManager()
        {
            PFProcess proc = new PFProcess();
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string currAppExePath = AppInfo.CurrentEntryAssembly;
            string appConfigManagerApp = Path.Combine(currAppFolder, _appConfigManagerExe);
            string mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool appConfigManagerFound = false;

            try
            {
                proc.Arguments = "\"" + currAppExePath + "\" \"" + _helpFilePath + "\" " + "\"Change an Application Setting\"";

                if (File.Exists(appConfigManagerApp))
                {
                    appConfigManagerFound = true;
                    proc.WorkingDirectory = currAppFolder;
                    proc.ExecutableToRun = appConfigManagerApp;
                }
                else
                {
                    string configValue = AppConfig.GetStringValueFromConfigFile("appConfigManagerPath", string.Empty);
                    if (configValue.Length > 0)
                    {
                        if (File.Exists(configValue))
                        {
                            appConfigManagerFound = true;
                            proc.WorkingDirectory = Path.GetDirectoryName(configValue);
                            proc.ExecutableToRun = configValue;
                        }
                        else
                        {
                            appConfigManagerFound = false;
                        }
                    }
                    else
                    {
                        appConfigManagerFound = false;

                    }
                }
                if (appConfigManagerFound == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find Application Configuration Manager Application in current app folder: ");
                    _msg.Append(currAppFolder);
                    throw new System.Exception(_msg.ToString());
                }
                proc.CreateNoWindow = true;
                proc.UseShellExecute = true;
                proc.WindowStyle = PFProcessWindowStyle.Normal;
                proc.RedirectStandardOutput = false;
                proc.RedirectStandardError = false;
                proc.RedirectStandardInput = false;
                proc.CheckIfProcessWaitingForInput = false;
                proc.MaxProcessRunSeconds = (int)0;

                proc.Run();

                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                proc = null;
            }

        }



        public void GenerateRandomWords()
        {
            string randomDataFilePath = string.Empty;
            PFList<string> wordList = new PFList<string>();
            int numWordsOutput = 0;
            int maxNumWordsToOutput = 0;
            string word = string.Empty;
            RandomNumber rn = new RandomNumber();
            int minRNum = 0;
            int maxRNum = -1;
            int numWordsOnLine = 0;
            int maxWordsPerLine = 10;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateRandomWords started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                if (_frm.txtRandomDataXmlFilesFolder.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify the folder containing the random data XML files.");
                    throw new System.Exception(_msg.ToString());
                }

                if (_frm.cboRandomDataXmlFile.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify the file containing the random data.");
                    throw new System.Exception(_msg.ToString());
                }

                randomDataFilePath = Path.Combine(_frm.txtRandomDataXmlFilesFolder.Text, _frm.cboRandomDataXmlFile.Text);
                if (File.Exists(randomDataFilePath) == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find file: ");
                    _msg.Append(randomDataFilePath);
                    throw new System.Exception(_msg.ToString());
                }

                wordList = PFList<string>.LoadFromXmlFile(randomDataFilePath);

                _msg.Length = 0;
                _msg.Append("Num words in list ");
                _msg.Append(_frm.cboRandomDataXmlFile.Text);
                _msg.Append(": ");
                _msg.Append(wordList.Count.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                maxNumWordsToOutput = Convert.ToInt32(_frm.txtNumWordsToOutput.Text);
                numWordsOutput = 0;
                minRNum = 0;
                maxRNum = wordList.Count - 1;
                numWordsOnLine = 0;
                _msg.Length = 0;

                while (numWordsOutput < maxNumWordsToOutput)
                {
                    word = wordList[rn.GenerateRandomInt(minRNum, maxRNum)];
                    numWordsOutput++;
                    numWordsOnLine++;
                    _msg.Append(word);
                    _msg.Append(" ");
                    if (numWordsOnLine >= maxWordsPerLine)
                    {
                        Program._messageLog.WriteLine(_msg.ToString());
                        _msg.Length = 0;
                        numWordsOnLine = 0;
                    }

                }
                if (_msg.Length > 0)
                {
                    Program._messageLog.WriteLine(_msg.ToString());
                    _msg.Length = 0;
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateRandomWords finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void GenerateSampleRandomSentences()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateSampleRandomSentences started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());


                //test
                PFRandomWord randWord = new PFRandomWord(enWordType.Noun);
                string errmsg = randWord.VerifyDefaultWordFilePaths();
                _msg.Length = 0;
                if (errmsg == string.Empty)
                {
                    _msg.Append("No errors found in default random word file paths.");
                }
                else
                {
                    _msg.Append("Errors found in default random word file paths.");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(errmsg);
                }
                Program._messageLog.WriteLine(_msg.ToString());

                //end test
                
                RandomSentence randSentence = new RandomSentence();
                string sampleSentences = randSentence.GenerateSampleSentences();
                _msg.Length = 0;
                _msg.Append("Random sentences (Sample): \r\n");
                _msg.Append(sampleSentences);
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateSampleRandomSentences finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void GenerateRandomSentences()
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateRandomSentences started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                RandomSentence randSentence = new RandomSentence();
                //string sentences = randSentence.GenerateSentence();
                //_msg.Length = 0;
                //_msg.Append("Random sentence: \r\n");
                //_msg.Append(sentences);
                //_msg.Append(Environment.NewLine);
                //Program._messageLog.WriteLine(_msg.ToString());
                //_msg.Append(Environment.NewLine);

                string sentences = randSentence.GenerateSentences(Convert.ToInt32(_frm.txtNumSentencesToOutput.Text));
                _msg.Length = 0;
                _msg.Append("Random sentences: \r\n");
                _msg.Append(sentences);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Append(Environment.NewLine);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateRandomSentences finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void GenerateRandomParagraphs()
        {
            RandomDocument randDoc = new RandomDocument();
            int numParagraphsToGenerate = 3;
            int numSentencesPerParagraph = 10;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateRandomParagraphs started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                numParagraphsToGenerate = Convert.ToInt32(_frm.txtNumParagraphsToOutput.Text);
                numSentencesPerParagraph = Convert.ToInt32(_frm.txtNumSentencesToOutput.Text);

                for (int p = 0; p < numParagraphsToGenerate; p++)
                {
                    string paragraph = randDoc.GenerateParagraph(numSentencesPerParagraph);
                    _msg.Length = 0;
                    _msg.Append(paragraph);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateRandomParagraphs finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void GenerateRandomDocument()
        {
            RandomDocument randDoc = new RandomDocument();
            int numParagraphsToGenerate = 3;
            int numSentencesPerParagraph = 10;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateRandomDocument started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                numParagraphsToGenerate = Convert.ToInt32(_frm.txtNumParagraphsToOutput.Text);
                numSentencesPerParagraph = Convert.ToInt32(_frm.txtNumSentencesToOutput.Text);

                if(numSentencesPerParagraph < 5)
                    numSentencesPerParagraph = 5;

                string document = randDoc.GenerateDocument(numParagraphsToGenerate, numSentencesPerParagraph - 2, numSentencesPerParagraph + 3, "Test Program Routine");

                _msg.Length = 0;
                _msg.Append(document);
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateRandomDocument finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void GenerateRandomChapter()
        {
            RandomDocument randDoc = new RandomDocument();
            int numParagraphsToGenerate = 3;
            int numSentencesPerParagraph = 10;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateRandomChapter started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                numParagraphsToGenerate = Convert.ToInt32(_frm.txtNumParagraphsToOutput.Text);
                numSentencesPerParagraph = Convert.ToInt32(_frm.txtNumSentencesToOutput.Text);

                if (numSentencesPerParagraph < 5)
                    numSentencesPerParagraph = 5;

                
                string chapter = randDoc.GenerateChapter("Test Program Chapter" ,numParagraphsToGenerate, numSentencesPerParagraph - 2, numSentencesPerParagraph + 3);



                _msg.Length = 0;
                _msg.Append(chapter);
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateRandomChapter finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }



        public void GenerateRandomBook()
        {
            RandomDocument randDoc = new RandomDocument();
            int numChaptersToGenerate = 3;
            int numParagraphsPerChapter = 10;
            int numSentencesPerParagraph = 5;
            string[] chapterTitles = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateRandomBook started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                numChaptersToGenerate = Convert.ToInt32(_frm.txtNumChaptersToOutput.Text);
                numParagraphsPerChapter = Convert.ToInt32(_frm.txtNumParagraphsToOutput.Text);
                numSentencesPerParagraph = Convert.ToInt32(_frm.txtNumSentencesToOutput.Text);

                if (numParagraphsPerChapter < 5)
                    numParagraphsPerChapter = 5;
                if (numSentencesPerParagraph < 5)
                    numSentencesPerParagraph = 5;

                chapterTitles = new string[2];
                chapterTitles[0] = "First Chapter Is This";
                chapterTitles[1] = "The Second Chapter Makes an Appearance";

                string book = randDoc.GenerateBook("Test Program Book", "*** THE END ***", numChaptersToGenerate, chapterTitles,  numParagraphsPerChapter - 3, numParagraphsPerChapter + 3, numSentencesPerParagraph - 2, numSentencesPerParagraph + 3);

                _msg.Length = 0;
                _msg.Append(book);
                Program._messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateRandomBook finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void GenerateSentenceTemplateArrays()
        {
            SentenceTemplateArrays sta = new SentenceTemplateArrays();

            try
            {
                _msg.Length = 0;
                _msg.Append("GenerateSentenceTemplateArrays started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sta.GenerateArrayCode(Program._messageLog);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GenerateSentenceTemplateArrays finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }
                 
                
        

    }//end class
}//end namespace
