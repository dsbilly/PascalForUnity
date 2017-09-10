using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.Frontend.Pascal.Tokens
{
    public class PascalWordToken : PascalToken
    {
        /**
    * Constructor.
    * @param source the source from where to fetch the token's characters.
    * @throws Exception if an error occurred.
    */
        public PascalWordToken(Source source)
            : base(source) {
        }

        /**
         * Extract a Pascal word token from the source.
         * @throws Exception if an error occurred.
         */
        protected void extract() {
            StringBuilder textBuffer = new StringBuilder();
            char currentChar = CurrentChar();

            // Get the word characters (letter or digit).  The scanner has
            // already determined that the first character is a letter.
            while (char.IsLetterOrDigit(currentChar)) {
                textBuffer.Append(currentChar);
                currentChar = NextChar();  // consume character
            }

            text = textBuffer.ToString();

            // Is it a reserved word or an identifier?
            //PascalTokenTypeEnum tokenTypeEnum = (PascalTokenType.RESERVED_WORDS.Contains(text.ToLower()))
            //       ?(PascalTokenTypeEnum)Enum.Parse(typeof(PascalTokenTypeEnum), text.ToUpper())   // reserved word
            //       : PascalTokenTypeEnum.IDENTIFIER;                                  // identifier

            type = new PascalTokenType(text);
        }
    }
}
