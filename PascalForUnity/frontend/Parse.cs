﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PascalForUnity.message;
using PascalForUnity.intermediate;
namespace PascalForUnity.frontend {
    /**
 * <h1>Parser</h1>
 *
 * <p>A language-independent framework class.  This abstract parser class
 * will be implemented by language-specific subclasses.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
public abstract class Parser : MessageProducer
{
    protected static SymTab symTab = null;                  // generated symbol table
    protected static MessageHandler messageHandler = new MessageHandler();  // message handler delegate

    //static  symTab = null;
    //static messageHandler = new MessageHandler();
    

    protected Scanner scanner;  // scanner used with this parser
    protected ICode iCode;      // intermediate code generated by this parser

    /**
     * Constructor.
     * @param scanner the scanner to be used with this parser.
     */
    protected Parser(Scanner scanner)
    {
        this.scanner = scanner;
        this.iCode = null;
    }

    /**
     * Getter.
     * @return the scanner used by this parser.
     */
    public Scanner GetScanner()
    {
        return scanner;
    }

    /**
     * Getter.
     * @return the intermediate code generated by this parser.
     */
    public ICode GetICode()
    {
        return iCode;
    }

    /**
     * Getter.
     * @return the symbol table generated by this parser.
     */
    public SymTab GetSymTab()
    {
        return symTab;
    }

    /**
     * Getter.
     * @return the message handler.
     */
    public MessageHandler GetMessageHandler()
    {
        return messageHandler;
    }

    /**
     * Parse a source program and generate the intermediate code and the symbol
     * table.  To be implemented by a language-specific parser subclass.
     * @throws Exception if an error occurred.
     */
    public abstract void Parse();

    /**
     * Return the number of syntax errors found by the parser.
     * To be implemented by a language-specific parser subclass.
     * @return the error count.
     */
    public abstract int GetErrorCount();

    /**
     * Call the scanner's currentToken() method.
     * @return the current token.
     */
    public Token CurrentToken()
    {
        return scanner.CurrentToken();
    }

    /**
     * Call the scanner's nextToken() method.
     * @return the next token.
     * @throws Exception if an error occurred.
     */
    public Token NextToken()
    {
        return scanner.NextToken();
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