using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.frontend {
   /**
 * <h1>Token</h1>
 *
 * <p>The framework class that represents a token returned by the scanner.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
public class Token
{
    protected TokenType type;  // language-specific token type
    protected String text;     // token text
    protected Object value;    // token value
    protected Source source;   // source
    protected int lineNum;     // line number of the token's source line
    protected int position;    // position of the first token character

    /**
     * Constructor.
     * @param source the source from where to fetch the token's characters.
     * @throws Exception if an error occurred.
     */
    public Token(Source source)     
    {
        this.source = source;
        this.lineNum = source.GetLineNum();
        this.position = source.GetPosition();

        Extract();
    }

    /**
     * Getter
     * @return the token type
     */
    public TokenType GetType()
    {
        return type;
    }

    /**
     * Getter.
     * @return the token text.
     */
    public String GetText()
    {
        return text;
    }

    /**
     * Getter.
     * @return the token value.
     */
    public Object GetValue()
    {
        return value;
    }

    /**
     * Getter.
     * @return the source line number.
     */
    public int GetLineNumber()
    {
        return lineNum;
    }

    /**
     * Getter.
     * @return the position.
     */
    public int GetPosition()
    {
        return position;
    }

    /**
     * Default method to extract only one-character tokens from the source.
     * Subclasses can override this method to construct language-specific
     * tokens.  After extracting the token, the current source line position
     * will be one beyond the last token character.
     * @throws Exception if an error occurred.
     */
    protected void Extract()
    {
        text = CurrentChar().ToString();
        value = null;

        NextChar();  // consume current character
    }

    /**
     * Call the source's currentChar() method.
     * @return the current character from the source.
     * @throws Exception if an error occurred.
     */
    protected char CurrentChar()
    {
        return source.CurrentChar();
    }

    /**
     * Call the source's nextChar() method.
     * @return the next character from the source after moving forward.
     * @throws Exception if an error occurred.
     */
    protected char NextChar()
    {
        return source.NextChar();
    }

    /**
     * Call the source's peekChar() method.
     * @return the next character from the source without moving forward.
     * @throws Exception if an error occurred.
     */
    protected char PeekChar()
    {
        return source.PeekChar();
    }
}
}
