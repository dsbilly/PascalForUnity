using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PascalForUnity.Frontend;
namespace PascalForUnity.Frontend.Pascal {
    /**
 * <h1>PascalTokenType</h1>
 *
 * <p>Pascal token types.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
    public class PascalTokenType : TokenType {
        public enum Types {// Reserved words.
            AND, ARRAY, BEGIN, CASE, CONST, DIV, DO, DOWNTO, ELSE, END,
            FILE, FOR, FUNCTION, GOTO, IF, IN, LABEL, MOD, NIL, NOT,
            OF, OR, PACKED, PROCEDURE, PROGRAM, RECORD, REPEAT, SET,
            THEN, TO, TYPE, UNTIL, VAR, WHILE, WITH,

            // Special symbols.
            PLUS, MINUS, STAR, SLASH, COLON_EQUALS,
            DOT, COMMA, SEMICOLON, COLON, QUOTE,
            EQUALS, NOT_EQUALS, LESS_THAN, LESS_EQUALS,
            GREATER_EQUALS, GREATER_THAN, LEFT_PAREN, RIGHT_PAREN,
            LEFT_BRACKET, RIGHT_BRACKET, LEFT_BRACE, RIGHT_BRACE,
            UP_ARROW, DOT_DOT,

            IDENTIFIER, INTEGER, REAL, STRING,
            ERROR, END_OF_FILE
        }


        private static readonly int FIRST_RESERVED_INDEX =(int)Types.AND;
        private static readonly int LAST_RESERVED_INDEX  = (int)Types.WITH;

        private static readonly int FIRST_SPECIAL_INDEX = (int)Types.PLUS;
        private static readonly int LAST_SPECIAL_INDEX  = (int)Types.DOT_DOT;

        private String text;  // token text

        /**
         * Constructor.
         */
        PascalTokenType ( ) {
            this.text = this.ToString().ToLower();
        }

        /**
         * Constructor.
         * @param text the token text.
         */
        PascalTokenType ( String text ) {
            this.text = text;
        }

        /**
         * Getter.
         * @return the token text.
         */
        public String GetText ( ) {
            return text;
        }

        // Set of lower-cased Pascal reserved word text strings.
        public static HashSet<String> RESERVED_WORDS {
            get {
                if (_RESERVED_WORDS == null) {
                    _RESERVED_WORDS = new HashSet<string>();
                    //foreach (int  myCode in Enum.GetValues(typeof(Types))) {
                    //    string strName =Enum.GetName(typeof(Types),myCode);//获取名称
                    //    string strVaule = myCode.ToString();//获取值
                    //    _RESERVED_WORDS.Add(strName.ToLower());
                    //}

                    //PascalTokenType.Types values[] = PascalTokenType.values();
                    for (int i = FIRST_RESERVED_INDEX; i <= LAST_RESERVED_INDEX; ++i) {
                        string strName =Enum.GetName(typeof(Types),i);//获取名称
                        _RESERVED_WORDS.Add(strName.ToLower());
                    }
                }
                return _RESERVED_WORDS;
            }
        }


        static  HashSet<String> _RESERVED_WORDS =null;

        // Hash table of Pascal special symbols.  Each special symbol's text
        // is the key to its Pascal token type.
        public static Dictionary<String,PascalTokenType.Types> SPECIAL_SYMBOLS {
            get {
                if (_SPECIAL_SYMBOLS == null) {
                    _SPECIAL_SYMBOLS = new Dictionary<string,PascalTokenType.Types>();


                    //PascalTokenType.Types values[] = PascalTokenType.values();
                    for (int i = FIRST_RESERVED_INDEX; i <= LAST_RESERVED_INDEX; ++i) {
                        Types type = (Types)Enum.ToObject(typeof(Types),i);
                        string strName =Enum.GetName(typeof(Types),type);//获取名称
                        _SPECIAL_SYMBOLS.Add(strName.ToLower(),type);
                    }
                }
                return _SPECIAL_SYMBOLS;
            }
        }


        static  Dictionary<String, PascalTokenType.Types>  _SPECIAL_SYMBOLS =null;

        public static Dictionary<String, PascalTokenType> SPECIAL_SYMBOLS =
        new Dictionary<String,PascalTokenType>();
        //static {
        //    PascalTokenType values[] = PascalTokenType.values();
        //    for (int i = FIRST_SPECIAL_INDEX; i <= LAST_SPECIAL_INDEX; ++i) {
        //        SPECIAL_SYMBOLS.put(values[i].getText(), values[i]);
        //    }
        //}
    }
}
