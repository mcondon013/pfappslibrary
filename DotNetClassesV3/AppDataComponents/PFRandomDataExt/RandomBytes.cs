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
    /// Class for generating byte arrays containing random characters.
    /// </summary>
    public class RandomBytes
    {
        //private work variables
        //private StringBuilder _msg = new StringBuilder();
        private Random _random = new Random();
        private RandomString _rs = new RandomString();

        //private variables for properties

        //constructors
        /// <summary>
        /// Constructor for the class. Does nothing.
        /// </summary>
        public RandomBytes()
        {
            ;
        }

        //properties

        //methods
        
        /// <summary>
        /// Method for generating a byte array containing random values.
        /// </summary>
        /// <param name="numBytesToGenerate">Number of random bytes to produce.</param>
        /// <returns>Byte array.</returns>
        /// <remakrs>Each byte is 8 bits wide.</remakrs>
        public byte[] GenerateRandomBytes(int numBytesToGenerate)
        {
            Byte[] b = new Byte[numBytesToGenerate];
            _random.NextBytes(b);
            return b;
        }

        /// <summary>
        /// Method for generating a char array containing random values.
        /// </summary>
        /// <param name="numCharsToGenerate">Number of char values to produce.</param>
        /// <returns>Char array.</returns>
        /// <remarks>Each char represents a unicode character (16 bits wide). For purposes of random generation, random char will be a random byte expanded to 16 bit char.</remarks>
        public char[] GenerateRandomChars(int numCharsToGenerate)
        {
            string s = _rs.GetStringANX(numCharsToGenerate);
            char[] c = s.ToCharArray();
            return c;
        }

        /// <summary>
        /// Method for generating a char containing a random value.
        /// </summary>
        /// <returns>Char value.</returns>
        public char GenerateRandomChar()
        {
            return Convert.ToChar(_rs.GetStringANX(1));
        }

        /// <summary>
        /// Method for generating a byte containing a random value.
        /// </summary>
        /// <returns>Byte value.</returns>
        public byte GenerateRandomByte()
        {
            byte[] b = GenerateRandomBytes(1);
            return (byte)b[0];
        }

    }//end class
}//end namespace
