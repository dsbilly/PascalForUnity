using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.Frontend.Pascal.Tokens
{
    class PascalStringToken:PascalToken
    {
         /**
     * Constructor.
     * @param source the source from where to fetch the token's characters.
     * @throws Exception if an error occurred.
     */
    public PascalStringToken(Source source):base(source)
        
    {
      
    }

    /**
     * Extract a Pascal string token from the source.
     * @throws Exception if an error occurred.
     */
    protected void Extract()
    {
        StringBuilder textBuffer = new StringBuilder();
        StringBuilder valueBuffer = new StringBuilder();

        char currentChar = NextChar();  // consume initial quote
        textBuffer.Append('\'');

        // Get string characters.
        do {
            // Replace any whitespace character with a blank.
            if (Char.IsWhiteSpace(currentChar)) {
                currentChar = ' ';
            }

            if ((currentChar != '\'') && (currentChar !=Source.EOF)) {
                textBuffer.Append(currentChar);
                valueBuffer.Append(currentChar);
                currentChar = NextChar();  // consume character
            }

            // Quote?  Each pair of adjacent quotes represents a single-quote.
            if (currentChar == '\'') {
                while ((currentChar == '\'') && (PeekChar() == '\'')) {
                    textBuffer.Append("''");
                    valueBuffer.Append(currentChar); // append single-quote
                    currentChar = NextChar();        // consume pair of quotes
                    currentChar = NextChar();
                }
            }
        } while ((currentChar != '\'') && (currentChar != Source.EOF));

        if (currentChar == '\'') {
            NextChar();  // consume final quote
            textBuffer.Append('\'');

            type =new PascalTokenType(PascalTokenTypeEnum.STRING);
            value = valueBuffer.ToString();
        }
        else {
            type =new PascalTokenType(PascalTokenTypeEnum.ERROR); ;
            value =Source.UNEXPECTED_EOF;
        }

        text = textBuffer.ToString();
    }
    }
}
