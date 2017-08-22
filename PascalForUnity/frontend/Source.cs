using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PascalForUnity.Messages;
using System.IO;
namespace PascalForUnity.Frontend {
    /**
 * <h1>Source</h1>
 *
 * <p>The framework class that represents the source program.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
public class Source : MessageProducer
{
    public static readonly  char EOL = '\n';      // end-of-line character
    public static readonly  char EOF = (char)0;  // end-of-file character

    private StreamReader  reader;            // reader for the source program
    private String line;                      // source line
    private int lineNum;                      // current source line number
    private int currentPos;                   // current source line position

    private MessageHandler messageHandler;    // delegate to handle messages

    /**
     * Constructor.
     * @param reader the reader for the source program
     * @throws IOException if an I/O error occurred
     */
    public Source(StreamReader reader)
        
    {
        this.lineNum = 0;
        this.currentPos = -2;  // set to -2 to read the first source line
        this.reader = reader;
        this.messageHandler = new MessageHandler();
    }

    /**
     * Getter.
     * @return the current source line number.
     */
    public int GetLineNum()
    {
        return lineNum;
    }

    /**
     * Getter.
     * @return the position of the next source character in the
     * current source line.
     */
    public int GetPosition()
    {
        return currentPos;
    }

    /**
     * Return the source character at the current position.
     * @return the source character at the current position.
     * @throws Exception if an error occurred.
     */
    public char CurrentChar()
    {
        // First time?
        if (currentPos == -2) {
            ReadLine();
            return NextChar();
        }

        // At end of file?
        else if (line == null) {
            return EOF;
        }

        // At end of line?
        else if ((currentPos == -1) || (currentPos == line.Length)) {
            return EOL;
        }

        // Need to read the next line?
        else if (currentPos > line.Length) {
            ReadLine();
            return NextChar();
        }

        // Return the character at the current position.
        else {
            return line[currentPos];
        }
    }

    /**
     * Consume the current source character and return the next character.
     * @return the next source character.
     * @throws Exception if an error occurred.
     */
    public char NextChar()
    {
        ++currentPos;
        return CurrentChar();
    }

    /**
     * Return the source character following the current character without
     * consuming the current character.
     * @return the following character.
     * @throws Exception if an error occurred.
     */
    public char PeekChar()
    {
        CurrentChar();
        if (line == null) {
            return EOF;
        }

        int nextPos = currentPos + 1;
        return nextPos < line.Length ? line[nextPos] : EOL;
    }

    /**
     * Read the next source line.
     * @throws IOException if an I/O error occurred.
     */
    private void ReadLine()
    {
        line = reader.ReadLine();// readLine();  // null when at the end of the source
        currentPos = -1;

        if (line != null) {
            ++lineNum;
        }

        // Send a source line message containing the line number
        // and the line text to all the listeners.
        if (line != null) {
            SendMessage(new Message(MessageType.SOURCE_LINE,
                                    new Object[] {lineNum, line}));
        }
    }

    /**
     * Close the source.
     * @throws Exception if an error occurred.
     */
    public void close()
    {
        if (reader != null) {
            try {
                reader.Close();
            }
            catch (IOException ex) {
                //ex.printStackTrace();
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }

    /**
     * Add a parser message listener.
     * @param listener the message listener to add.
     */
    public void AddMessageListener(MessageListener listener)
    {
        messageHandler.AddListener(listener);
    }

    /**
     * Remove a parser message listener.
     * @param listener the message listener to remove.
     */
    public void RemoveMessageListener(MessageListener listener)
    {
        messageHandler.RemoveListener(listener);
    }

    /**
     * Notify listeners after setting the message.
     * @param message the message to set.
     */
    public void SendMessage(Message message)
    {
        messageHandler.SendMessage(message);
    }
}
}
