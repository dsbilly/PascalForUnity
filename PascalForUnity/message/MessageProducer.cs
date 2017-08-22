using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.Messages {
    /**
 * <h1>MessageProducer</h1>
 *
 * <p>All classes that produce messages must implement this interface.</p>
 *
 * <p>Copyright (c) 2009 by Ronald Mak</p>
 * <p>For instructional purposes only.  No warranties.</p>
 */
    public interface MessageProducer {
        /**
         * Add a listener to the listener list.
         * @param listener the listener to add.
         */
        void AddMessageListener ( MessageListener listener );

        /**
         * Remove a listener from the listener list.
         * @param listener the listener to remove.
         */
        void RemoveMessageListener ( MessageListener listener );

        /**
         * Notify listeners after setting the message.
         * @param message the message to set.
         */
        void SendMessage ( Message message );
    }
}
