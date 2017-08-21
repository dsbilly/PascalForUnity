using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PascalForUnity.frontend.pascal;
using PascalForUnity.frontend;
using PascalForUnity.backend;
using PascalForUnity.intermediate;
using PascalForUnity.message;
using System.IO;
namespace PascalForUnity
{
    /**
 * <h1>Pascal</h1>
 *
 * <p>Compile or interpret a Pascal source program.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
public class Pascal
{
    private Parser parser;    // language-independent parser
    private Source source;    // language-independent scanner
    private ICode iCode;      // generated intermediate code
    private SymTab symTab;    // generated symbol table
    private Backend backend;  // backend

    /**
     * Compile or interpret a Pascal source program.
     * @param operation either "compile" or "execute".
     * @param filePath the source file path.
     * @param flags the command line flags.
     */
    public Pascal(String operation, String filePath, String flags)
    {
        try {
            bool intermediate = flags.IndexOf('i') > -1;
            bool xref         = flags.IndexOf('x') > -1;

            source = new Source(new StreamReader(filePath));
            source.AddMessageListener(new SourceMessageListener());

            parser = FrontendFactory.createParser("Pascal", "top-down", source);
            parser.AddMessageListener(new ParserMessageListener());

            backend = BackendFactory.createBackend(operation);
            backend.AddMessageListener(new BackendMessageListener());

            parser.Parse();
            source.close();
			
            iCode = parser.GetICode();
            symTab = parser.GetSymTab();

            backend.Process(iCode, symTab);
            Console.ReadLine();
        }
        catch (Exception ex) {
            Console.WriteLine("***** Internal translator error. *****");
            //ex.printStackTrace();
        }
    }

    private static readonly  String FLAGS = "[-ix]";
    private static readonly  String USAGE =
        "Usage: Pascal execute|compile " + FLAGS + " <source file path>";

    /**
     * The main method.
     * @param args command-line arguments: "compile" or "execute" followed by
     *             optional flags followed by the source file path.
     */
    static void Main(String[] args)
    {
        try {
            String operation = args[0];

            // Operation.
            if (!(   operation.ToLower().Equals("compile")
                  || operation.ToLower().Equals("execute"))) {
                throw new Exception();
            }

            int i = 0;
            String flags = "";

            // Flags.
            while ((++i < args.Length) && (args[i][0] == '-')) {
                flags += args[i].Substring(1);
            }

            // Source path.
            if (i < args.Length) {
                String path = args[i];
                new Pascal(operation, path, flags);
            }
            else {
                throw new Exception();
            }
        }
        catch (Exception ex) {
            //System.out.println(USAGE);
            Console.WriteLine(USAGE);
        }
    }

    private static readonly  String SOURCE_LINE_FORMAT = "{0} {1}";

    /**
     * Listener for source messages.
     */
    private class SourceMessageListener : MessageListener
    {
        /**
         * Called by the source whenever it produces a message.
         * @param message the message.
         */
        public void MessageReceived(Message message)
        {
            MessageType type = message.GetType();
            Object[] body = (Object []) message.GetBody();

            switch (type) {

                case MessageType.SOURCE_LINE: {
                    int lineNumber = (int) body[0];
                    String lineText = (String) body[1];

                    Console.WriteLine(String.Format(SOURCE_LINE_FORMAT,
                                                     lineNumber, lineText));
                    break;
                }
            }
        }
    }

    //private static readonly  String PARSER_SUMMARY_FORMAT =
    //    "\n%,20d source lines." +
    //    "\n%,20d syntax errors." +
    //    "\n%,20.2f seconds total parsing time.\n";
    private static readonly  String PARSER_SUMMARY_FORMAT =
        "\n%,{0} source lines." +
        "\n%,{1} syntax errors." +
        "\n%,{2} seconds total parsing time.\n";

    /**
     * Listener for parser messages.
     */
    private class ParserMessageListener : MessageListener
    {
        /**
         * Called by the parser whenever it produces a message.
         * @param message the message.
         */
        public void MessageReceived(Message message)
        {
            MessageType type = message.GetType();

            switch (type) {

                case MessageType.PARSER_SUMMARY: {
                    double[] body = (double[]) message.GetBody();
                    int statementCount = (int) body[0];
                    int syntaxErrors = (int) body[1];
                    float elapsedTime = (float) body[2];

                    Console.WriteLine(PARSER_SUMMARY_FORMAT,
                                      statementCount, syntaxErrors,
                                      elapsedTime);
                    break;
                }
            }
        }
    }

    //private static readonly  String INTERPRETER_SUMMARY_FORMAT =
    //    "\n%,20d statements executed." +
    //    "\n%,20d runtime errors." +
    //    "\n%,20.2f seconds total execution time.\n";

    private static readonly  String INTERPRETER_SUMMARY_FORMAT =
        "\n%,{0} statements executed." +
        "\n%,{1} runtime errors." +
        "\n%,{2} seconds total execution time.\n";

    private static readonly  String COMPILER_SUMMARY_FORMAT =
        "\n%,{0} instructions generated." +
        "\n%,{1} seconds total code generation time.\n";
    //private static readonly  String COMPILER_SUMMARY_FORMAT =
    //    "\n%,20d instructions generated." +
    //    "\n%,20.2f seconds total code generation time.\n";

    /**
     * Listener for back end messages.
     */
    private class BackendMessageListener : MessageListener
    {
        /**
         * Called by the back end whenever it produces a message.
         * @param message the message.
         */
        public void MessageReceived(Message message)
        {
            MessageType type = message.GetType();

            switch (type) {

                case MessageType.INTERPRETER_SUMMARY: {
                    double[] body = (double[]) message.GetBody();
                    int executionCount = (int) body[0];
                    int runtimeErrors = (int) body[1];
                    float elapsedTime = (float) body[2];

                    Console.WriteLine(INTERPRETER_SUMMARY_FORMAT,
                                      executionCount, runtimeErrors,
                                      elapsedTime);
                    break;
                }

                case MessageType.COMPILER_SUMMARY: {
                    double[] body = (double[]) message.GetBody();
                    int instructionCount = (int) body[0];
                    float elapsedTime = (float) body[1];

                    Console.WriteLine(COMPILER_SUMMARY_FORMAT,
                                      instructionCount, elapsedTime);
                    break;
                }
            }
        }
    }
}
}
