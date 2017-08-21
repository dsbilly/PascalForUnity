using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.frontend.pascal
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
            Token token;
            char currentChar = CurrentChar();

            // Construct the next token.  The current character determines the
            // token type.
            if (currentChar ==Source.EOF) {
                token = new EofToken(source);
            } else {
                token = new Token(source);
            }

            return token;
        }
    }
}
