//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InitConsoleApp
{
    public static class Tests
    {
        //private work variables
        private static StringBuilder _msg = new StringBuilder();
        private static StringBuilder _str = new StringBuilder();

        //private varialbles for properties


        //properties

        //methods
        public static void Test1()
        {
            int x = 10;
            int y = 2;
            int z = x / y;

            _msg.Length = 0;
            _msg.Append("z = ");
            _msg.Append(z.ToString("#,##0"));
            Console.WriteLine(_msg.ToString());
        }



    }//end class
}//end namespace
