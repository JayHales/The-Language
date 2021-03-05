using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace The_Language_V1
{
    static class Program
    {

        static string input = "var aaaa = 5; { var b = 10; }  {var       c = a + b} {var d = \"Hello\"}";

        static void Main(string[] args)
        {
            List<Token> tokens = Lexer.Lex(input);


        }
    }
}
