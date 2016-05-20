//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomDataExt
{
    /// <summary>
    /// Class to generate random data elements.
    /// </summary>
    public class RandomValue
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        RandomNumber _rn = new RandomNumber();
        RandomString _rs = new RandomString();

        //private variables for properties

        //enumerations
#pragma warning disable 1591
        public enum enNationalIdCountry
        {
            NotSpecified,
            Canada,
            Mexico,
            UnitedStates
        }
        public enum enNationalIdNameType
        {
            NotSpecified,
            Business,
            Person
        }
#pragma warning restore 1591

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomValue()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Routine for generating the default national id of all zeros.
        /// </summary>
        /// <param name="country">Country for which the random id will be formatted.</param>
        /// <param name="nameType">Specify whether name is a person or business name.</param>
        /// <returns>Always returns string containing an all zeros national id.</returns>
        public string GetDefaultNationalId(enNationalIdCountry country, enNationalIdNameType nameType)
        {
            string nationalId = string.Empty;

            if (nameType == enNationalIdNameType.Person)
            {
                switch (country)
                {
                    case enNationalIdCountry.UnitedStates:
                        nationalId = "000-00-0000";
                        break;
                    case enNationalIdCountry.Canada:
                        nationalId = "000-000-000";
                        break;
                    case enNationalIdCountry.Mexico:
                        nationalId = "XXXX000000XXXXXX00";
                        break;
                    default:
                        nationalId = "000000000";
                        break;
                }
            }
            else if (nameType == enNationalIdNameType.Business)
            {
                switch (country)
                {
                    case enNationalIdCountry.UnitedStates:
                        nationalId = "00-0000000";
                        break;
                    case enNationalIdCountry.Canada:
                        nationalId = "000000000XX0000";
                        break;
                    case enNationalIdCountry.Mexico:
                        nationalId = "XXX000000000";
                        break;
                    default:
                        nationalId = "000000000";
                        break;
                }
            }
            else
            {
                nationalId = "*INVALID*";
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <param name="country">Country for which the random id will be formatted.</param>
        /// <param name="nameType">Specify whether name is a person or business name.</param>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalId(enNationalIdCountry country, enNationalIdNameType nameType)
        {
            string nationalId = string.Empty;



            if (nameType == enNationalIdNameType.Person)
            {
                switch (country)
                {
                    case enNationalIdCountry.UnitedStates:
                        nationalId = GetNationalPersonIdUS();
                        break;
                    case enNationalIdCountry.Canada:
                        nationalId = GetNationalPersonIdCAN();
                        break;
                    case enNationalIdCountry.Mexico:
                        nationalId = GetNationalPersonIdMEX();
                        break;
                    default:
                        nationalId = "000000000";
                        break;
                }
            }
            else if (nameType == enNationalIdNameType.Business)
            {
                switch (country)
                {
                    case enNationalIdCountry.UnitedStates:
                        nationalId = GetNationalBusinessIdUS();
                        break;
                    case enNationalIdCountry.Canada:
                        nationalId = GetNationalBusinessIdCAN();
                        break;
                    case enNationalIdCountry.Mexico:
                        nationalId = GetNationalBusinessIdMEX();
                        break;
                    default:
                        nationalId = "000000000";
                        break;
                }
            }
            else
            {
                nationalId = "*INVALID*";
            }
            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) SSN.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalPersonIdUS()
        {
            string nationalId = "000-00-0000";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = _rn.GenerateRandomInt(1, 999).ToString("000") + "-00-0000";
                    break;
                case 2:
                    nationalId = "000-" + _rn.GenerateRandomInt(1, 99).ToString("00") + "-0000";
                    break;
                case 3:
                    nationalId = "000-00-" + _rn.GenerateRandomInt(1, 9999).ToString("0000");
                    break;
                default:
                    nationalId = "000-00-0000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalPersonIdCAN()
        {
            string nationalId = "000-000-000";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = "0" + _rn.GenerateRandomInt(1, 99).ToString("00") + "-000-000";
                    break;
                case 2:
                    nationalId = "000-" + _rn.GenerateRandomInt(1, 999).ToString("000") + "-000";
                    break;
                case 3:
                    nationalId = "000-000-" + _rn.GenerateRandomInt(1, 999).ToString("000");
                    break;
                default:
                    nationalId = "000-000-000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalPersonIdMEX()
        {
            string nationalId = "XXXX000000XXXXXX00";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 4);

            switch (randomFormat)
            {
                case 1:
                    nationalId = _rs.GetStringUC(4) + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                case 2:
                    nationalId = "YYXX" + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                case 3:
                    nationalId = "ZZXX" + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                case 4:
                    nationalId = "XXXX" + "000000" + _rs.GetStringUC(6) + "00";
                    break;
                default:
                    nationalId = "XXXX000000XXXXXX00";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) SSN.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalBusinessIdUS()
        {
            string nationalId = "00-0000000";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = _rn.GenerateRandomInt(1, 99).ToString("00") + "-0000000";
                    break;
                case 2:
                    nationalId = "00-" + _rn.GenerateRandomInt(1, 99).ToString("00") + "00000";
                    break;
                case 3:
                    nationalId = "00-" + _rn.GenerateRandomInt(1, 9999).ToString("0000") + "000";
                    break;
                default:
                    nationalId = "00-0000000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalBusinessIdCAN()
        {
            string nationalId = "000000000XX0000";
            int randomFormat = 0;

            randomFormat = _rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = "000000000" + _rs.GetStringUC(2) + "0000";
                    break;
                case 2:
                    nationalId = _rn.GenerateRandomInt(1, 999999999).ToString("000000000") + "XX" + "0000";
                    break;
                case 3:
                    nationalId = "000000000" + "XX" + _rn.GenerateRandomInt(1, 9999).ToString("0000");
                    break;
                default:
                    nationalId = "000000000XX0000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalBusinessIdMEX()
        {
            string nationalId = "XXXX000000XXXXXX00";

            nationalId = _rs.GetStringUC(3) + "000000" + _rn.GenerateRandomInt(0, 999).ToString("000");

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Country code of 1 and area code of 000 will be specified in the resulting random telephone number.</remarks>
        public string GetTelephoneNumber()
        {
            return GetTelephoneNumber("1", "000");
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <param name="areaCode">Specify the area code for the telephone number.</param>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Country code of 1 will be specified in the resulting random telephone number.</remarks>
        public string GetTelephoneNumber(string areaCode)
        {
            return GetTelephoneNumber("1", areaCode);
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <param name="countryCode">Specify the country code for the telephone number.</param>
        /// <param name="areaCode">Specify the area code for the telephone number.</param>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Leave countryCode and/or areaCode blank to omit those portions of the telephone number from the random result.</remarks>
        public string GetTelephoneNumber(string countryCode, string areaCode)
        {
            string telNo = "1-000-000-0000";
            StringBuilder sb = new StringBuilder();
            int num = 0;

            if (countryCode.Length > 0)
            {
                sb.Append(countryCode);
                sb.Append("-");
            }
            if (areaCode.Length > 0)
            {
                if (areaCode == "33")   //mexico: guadalajara
                {
                    num = _rn.GenerateRandomInt(0, 1);
                    if (num == 0)
                    {
                        sb.Append("331");
                        sb.Append("-");
                    }
                    else
                    {
                        sb.Append("333");
                        sb.Append("-");
                    }
                }
                else
                {
                    sb.Append(areaCode);
                    sb.Append("-");
                }
            }

            if (countryCode == "52")  //mexico
            {
                if (areaCode == "55")  //mexico: federal district
                {
                    sb.Append("00-00-00-00");
                }
                else if (areaCode == "81")
                {
                    sb.Append("0000-0000");
                }
                else
                {
                    sb.Append("000-0000");  //all other mexico area codes
                }
            }
            else
            {
                //us and canada
                sb.Append("555-");
                sb.Append(_rn.GenerateRandomInt(100, 199).ToString("0000"));
            }

            telNo = sb.ToString();

            return telNo;
        }

        /// <summary>
        /// Routine to generate a random email address.
        /// </summary>
        /// <returns>String containing random email address.</returns>
        /// <remarks>All email addresses are for the example domain.</remarks>
        public string GetEmailAddress()
        {
            string topLevelDomain = "000";

            switch (_rn.GenerateRandomInt(1, 10))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    topLevelDomain = "com";
                    break;
                case 5:
                case 6:
                case 7:
                    topLevelDomain = "net";
                    break;
                case 8:
                case 9:
                    topLevelDomain = "org";
                    break;
                case 10:
                    topLevelDomain = "edu";
                    break;
                default:
                    topLevelDomain = "000";
                    break;
            }

            return GetEmailAddress(topLevelDomain);
        }

        /// <summary>
        /// Routine to generate a random email address.
        /// </summary>
        /// <param name="topLevelDomain">Top level domain to use for this address (e.g. com, net, org, edu).</param>
        /// <returns>String containing random email address.</returns>
        /// <remarks>All email addresses are for the example domain.</remarks>
        public string GetEmailAddress(string topLevelDomain)
        {
            StringBuilder sb = new StringBuilder();
            string emailAddress = string.Empty;


            sb.Append(_rs.GetRandomSyllablesLC(_rn.GenerateRandomInt(2, 4)));
            sb.Append(@"@example.");
            sb.Append(topLevelDomain.TrimStart('.'));

            emailAddress = sb.ToString();

            return emailAddress;
        }

        /// <summary>
        /// Routine to generate a random date.
        /// </summary>
        /// <param name="earliestDate">Earliest date to generate.</param>
        /// <param name="latestDate">Latest date to generate.</param>
        /// <returns>Random date that falls between earliestDate and lastestDate parameters.</returns>
        public DateTime GenerateRandomDate(DateTime earliestDate, DateTime latestDate)
        {
            DateTime randDate = DateTime.Now;
            TimeSpan earliestTs = earliestDate.Subtract(DateTime.MinValue);
            TimeSpan latestTs = latestDate.Subtract(DateTime.MinValue);

            double earliestDays = earliestTs.TotalDays;
            double latestDays = latestTs.TotalDays;

            double randDays = _rn.GenerateRandomNumber(earliestDays, latestDays);
            TimeSpan randTs = new TimeSpan((int)randDays, 0, 0, 0);
            randDate = DateTime.MinValue.Add(randTs);

            int randNum = _rn.GenerateRandomInt(0, 86399);  //seconds in day to add
            randTs = new TimeSpan(0, 0, randNum);
            randDate = randDate.Add(randTs);

            return randDate;
        }


    }//end class
}//end namespace
