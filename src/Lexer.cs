using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace The_Language_V1
{
    static class Lexer
    {
        public static Dictionary<TokenType, Regex> tokenRegex = new Dictionary<TokenType, Regex>
        {
            { TokenType.StringLiteral, CaretOptionalWS("\"\\w+\"") },
            { TokenType.Equals, CaretOptionalWS("\\=") },
            { TokenType.Semicolon, CaretOptionalWS("\\;") },

            { TokenType.OpenCurlyBracket, CaretOptionalWS("\\{") },
            { TokenType.CloseCurlyBracket, CaretOptionalWS("\\}")},
            { TokenType.OpenBracket, CaretOptionalWS("\\(") },
            { TokenType.CloseBracket, CaretOptionalWS("\\)")},

            
            { TokenType.Number, CaretOptionalWS("\\d+") },

            { TokenType.Addition, CaretOptionalWS("\\+") },
            { TokenType.Subtraction, CaretOptionalWS("\\-") },
            { TokenType.Multiplication, CaretOptionalWS("\\*") },
            { TokenType.Division, CaretOptionalWS("\\/") },
            { TokenType.VariableInitializer, CaretOptionalWS("var ") },
            { TokenType.VariableName, CaretOptionalWS("\\w+") },
        };

       

        public static List<Token> Lex(string input)
        {

            List<Token> tokens = new List<Token>();

            input = input.Replace("\n", "");
            while (input.IndexOf("  ") != -1)
                input = input.Replace("  ", " ");

            while (input.Length != 0)
            {
                foreach (KeyValuePair<TokenType, Regex> kv in tokenRegex)
                {
                    if (kv.Value.Match(input).Success)
                    {
                        tokens.Add(new Token { tokenType = kv.Key, value = kv.Value.Match(input).Value.Trim() });

                        input = input[kv.Value.Match(input).Value.Length..];

                        break;
                    }
                }
                Console.WriteLine(input);
            }

            foreach (Token t in tokens)
            {
                Console.WriteLine(t.value + " " + t.tokenType);
            }

            return tokens;
        }

        /// <summary>
        /// Returns a regex that looks like: / ?s ?/g
        /// </summary>
        /// <param name="s">The interior string</param>
        /// <returns></returns>
        private static Regex CaretOptionalWS(string s) => new Regex("^ ?" + s + " ?");
    }

    public enum TokenType
    {
        
        OpenCurlyBracket,
        CloseCurlyBracket,

        OpenBracket,
        CloseBracket,

        /// <summary>
        /// = "var a = 5;"
        /// </summary>
        Equals,

        /// <summary>
        /// ; "var a = 5;"
        /// </summary>
        Semicolon,

        /// <summary>
        /// [0-9] "var a = 5;"
        /// </summary>
        Number,

        /// <summary>
        /// + "a + b"
        /// </summary>
        Addition,
        Subtraction,
        Multiplication,
        Division,

        /// <summary>
        /// "hello" "var d = "hello"";
        /// </summary>
        StringLiteral,

        /// <summary>
        /// var  "var a = 5;"
        /// </summary>
        VariableInitializer,

        /// <summary>
        /// [a-z] "var a = 5;"
        /// </summary>
        VariableName,



    }

    struct Token
    {
        public TokenType tokenType;
        public string value;
    }
}
