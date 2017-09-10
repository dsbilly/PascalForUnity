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
 * <p>Pascal token PascalTokenTypeEnum.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
    public enum PascalTokenTypeEnum
    {
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

    public class PascalTokenType : TokenType {
        //public enum PascalTokenTypeEnum {// Reserved words.
        //    AND, ARRAY, BEGIN, CASE, CONST, DIV, DO, DOWNTO, ELSE, END,
        //    FILE, FOR, FUNCTION, GOTO, IF, IN, LABEL, MOD, NIL, NOT,
        //    OF, OR, PACKED, PROCEDURE, PROGRAM, RECORD, REPEAT, SET,
        //    THEN, TO, TYPE, UNTIL, VAR, WHILE, WITH,

        //    // Special symbols.
        //    PLUS, MINUS, STAR, SLASH, COLON_EQUALS,
        //    DOT, COMMA, SEMICOLON, COLON, QUOTE,
        //    EQUALS, NOT_EQUALS, LESS_THAN, LESS_EQUALS,
        //    GREATER_EQUALS, GREATER_THAN, LEFT_PAREN, RIGHT_PAREN,
        //    LEFT_BRACKET, RIGHT_BRACKET, LEFT_BRACE, RIGHT_BRACE,
        //    UP_ARROW, DOT_DOT,

        //    IDENTIFIER, INTEGER, REAL, STRING,
        //    ERROR, END_OF_FILE
        //}


        private static readonly int FIRST_RESERVED_INDEX =(int)PascalTokenTypeEnum.AND;
        private static readonly int LAST_RESERVED_INDEX  = (int)PascalTokenTypeEnum.WITH;

        private static readonly int FIRST_SPECIAL_INDEX = (int)PascalTokenTypeEnum.PLUS;
        private static readonly int LAST_SPECIAL_INDEX  = (int)PascalTokenTypeEnum.DOT_DOT;

        private String text;  // token text
        PascalTokenTypeEnum type;
        /**
         * Constructor.
         */
        public PascalTokenType ( ) {
            this.text = this.ToString().ToLower();
        }

        /**
         * Constructor.
         * @param text the token text.
         */
        public PascalTokenType ( String text ) {
            this.text = text;
            this.type = (PascalTokenType.RESERVED_WORDS.Contains(text.ToLower()))
                  ? (PascalTokenTypeEnum)Enum.Parse(typeof(PascalTokenTypeEnum), text.ToUpper())   // reserved word
                  : PascalTokenTypeEnum.IDENTIFIER;   
        }

        public PascalTokenType(PascalTokenTypeEnum type) {
            this.text = type.ToString();
            this.type = type;
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
                    //foreach (int  myCode in Enum.GetValues(typeof(PascalTokenTypeEnum))) {
                    //    string strName =Enum.GetName(typeof(PascalTokenTypeEnum),myCode);//获取名称
                    //    string strVaule = myCode.ToString();//获取值
                    //    _RESERVED_WORDS.Add(strName.ToLower());
                    //}

                    //PascalTokenType.PascalTokenTypeEnum values[] = PascalTokenType.values();
                    for (int i = FIRST_RESERVED_INDEX; i <= LAST_RESERVED_INDEX; ++i) {
                        string strName =Enum.GetName(typeof(PascalTokenTypeEnum),i);//获取名称
                        _RESERVED_WORDS.Add(strName.ToLower());
                    }
                }
                return _RESERVED_WORDS;
            }
        }


        static  HashSet<String> _RESERVED_WORDS =null;

        // Hash table of Pascal special symbols.  Each special symbol's text
        // is the key to its Pascal token type.
        public static Dictionary<String,PascalTokenTypeEnum> SPECIAL_SYMBOLS {
            get {
                if (_SPECIAL_SYMBOLS == null) {
                    _SPECIAL_SYMBOLS = new Dictionary<string,PascalTokenTypeEnum>();


                    //PascalTokenType.PascalTokenTypeEnum values[] = PascalTokenType.values();
                    for (int i = FIRST_RESERVED_INDEX; i <= LAST_RESERVED_INDEX; ++i) {
                        PascalTokenTypeEnum type = (PascalTokenTypeEnum)Enum.ToObject(typeof(PascalTokenTypeEnum),i);
                        string strName =Enum.GetName(typeof(PascalTokenTypeEnum),type);//获取名称
                        _SPECIAL_SYMBOLS.Add(strName.ToLower(),type);
                    }
                }
                return _SPECIAL_SYMBOLS;
            }
        }


        static  Dictionary<String, PascalTokenTypeEnum>  _SPECIAL_SYMBOLS =null;

        //public static Dictionary<String, PascalTokenType> SPECIAL_SYMBOLS =
        //new Dictionary<String,PascalTokenType>();
        //static {
        //    PascalTokenType values[] = PascalTokenType.values();
        //    for (int i = FIRST_SPECIAL_INDEX; i <= LAST_SPECIAL_INDEX; ++i) {
        //        SPECIAL_SYMBOLS.put(values[i].getText(), values[i]);
        //    }
        //}

        public static bool operator ==(PascalTokenType lhs, PascalTokenType rhs) {
            return lhs.type == rhs.type;
        }

        //比较运算符必须成对重载.
        public static bool operator !=(PascalTokenType lhs, PascalTokenType rhs) {
            return !(lhs.type == rhs.type);
        }
    }
}
