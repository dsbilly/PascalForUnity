﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PascalForUnity.Frontend;
using PascalForUnity.Messages;
namespace PascalForUnity.Frontend.Pascal
{
    /**
  * <h1>PascalParserTD</h1>
  *
  * <p>The top-down Pascal parser.</p>
  *
  * <p>Copyright (c) 2009 by Ronald Mak</p>
  * <p>For instructional purposes only.  No warranties.</p>
  */
    public class PascalParserTD : Parser
    {
        /**
         * Constructor.
         * @param scanner the scanner to be used with this parser.
         */
        public PascalParserTD(Scanner scanner)
            : base(scanner) {

        }

        /**
         * Parse a Pascal source program and generate the symbol table
         * and the intermediate code.
         */
        public override void Parse() {
            Token token;
            //long startTime = System.currentTimeMillis();
            DateTime startTime = System.DateTime.Now;// .currentTimeMillis();

            try {
                // Loop over each token until the end of file.
                while (!((token = NextToken()) is EofToken)) {
                    TokenType tokenType = token.GetType();

                    if (tokenType != TokenType.ERROR) {

                        // Format each token.
                        SendMessage(new Message(TOKEN,
                                                new Object[] {token.GetLineNumber(),
                                                          token.GetPosition(),
                                                          tokenType,
                                                          token.GetText(),
                                                          token.GetValue()}));
                    } else {
                        errorHandler.flag(token, (PascalErrorCode)token.getValue(),
                                          this);
                    }

                }

                // Send the parser summary message.
                //float elapsedTime = (System.currentTimeMillis() - startTime)/1000f;
                double elapsedTime = (System.DateTime.Now - startTime).TotalSeconds;
                SendMessage(new Message(MessageType.PARSER_SUMMARY,
                                        new double[] {token.GetLineNumber(),
                                              GetErrorCount(),
                                              elapsedTime}));
            } catch (System.Exception e) {
                Console.WriteLine(e.ToString());
            }
        }

        /**
         * Return the number of syntax errors found by the parser.
         * @return the error count.
         */
        public override int GetErrorCount() {
            return 0;
        }
    }
}
