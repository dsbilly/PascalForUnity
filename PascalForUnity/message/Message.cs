using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.message {
    /**
 * <h1>Message</h1>
 *
 * <p>Message format.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
    public class Message {
        private MessageType type;
        private Object body;

        /**
         * Constructor.
         * @param type the message type.
         * @param body the message body.
         */
        public Message ( MessageType type,Object body ) {
            this.type = type;
            this.body = body;
        }

        /**
         * Getter.
         * @return the message type.
         */
        public MessageType GetType ( ) {
            return type;
        }

        /**
         * Getter.
         * @return the message body.
         */
        public Object GetBody ( ) {
            return body;
        }
    }
}
