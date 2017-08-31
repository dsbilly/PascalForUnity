using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.Frontend.Pascal
{
    /**
 * <h1>PascalScanner</h1>
 *
 * <p>The Pascal scanner.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
    public class PascalScanner : Scanner
    {
        /**
         * Constructor
         * @param source the source to be used with this scanner.
         */
        public PascalScanner(Source source)
            : base(source) {

        }

        /**
         * Extract and return the next Pascal token from the source.
         * @return the next token.
         * @throws Exception if an error occurred.
         */
        protected override Token ExtractToken() {
            skipWhiteSpace();

            Token token;
            char currentChar = CurrentChar();

            // Construct the next token.  The current character determines the
            // token type.
            if (currentChar == Source.EOF) {
                token = new EofToken(source);
            } else if (char.IsLetter(currentChar)) {
                token = new PascalWordToken(source);
            } else if (char.IsDigit(currentChar)) {
                token = new PascalNumberToken(source);
            } else if (currentChar == '\'') {
                token = new PascalStringToken(source);
            } else if (PascalTokenType.SPECIAL_SYMBOLS
                       .ContainsKey(char.ToString(currentChar))) {
                token = new PascalSpecialSymbolToken(source);
            } else {
                token = new PascalErrorToken(source, INVALID_CHARACTER,
                                             char.ToString(currentChar));
                NextChar();  // consume character
            }

            return token;
        }

        /**
    * Skip whitespace characters by consuming them.  A comment is whitespace.
    * @throws Exception if an error occurred.
    */
        private void skipWhiteSpace() {
            char currentChar = CurrentChar();

            while (char.IsWhiteSpace(currentChar) || (currentChar == '{')) {

                // Start of a comment?
                if (currentChar == '{') {
                    do {
                        currentChar = NextChar();  // consume comment characters
                    } while ((currentChar != '}') && (currentChar != Source.EOF));

                    // Found closing '}'?
                    if (currentChar == '}') {
                        currentChar = NextChar();  // consume the '}'
                    }
                }

                // Not a comment.
                else {
                    currentChar = NextChar();  // consume whitespace character
                }
            }
        }
    }
}
