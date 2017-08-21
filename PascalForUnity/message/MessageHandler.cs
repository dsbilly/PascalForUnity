using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.message {
    /* <h1>MessageHandler</h1>
*
* <p>A helper class to which message producer classes delegate the task of
* maintaining and notifying listeners.</p>
*
* <p>Copyright (c) 2009 by Ronald Mak</p>
* <p>For instructional purposes only.  No warranties.</p>
*/
    public class MessageHandler {
        private Message message;                       // message
        private List<MessageListener> listeners;  // listener list

        /**
         * Constructor.
         */
        public MessageHandler ( ) {
            this.listeners = new List<MessageListener>();
        }

        /**
         * Add a listener to the listener list.
         * @param listener the listener to add.
         */
        public void AddListener ( MessageListener listener ) {
            listeners.Add(listener);
        }

        /**
         * Remove a listener from the listener list.
         * @param listener the listener to remove.
         */
        public void RemoveListener ( MessageListener listener ) {
            listeners.Remove(listener);
        }

        /**
         * Notify listeners after setting the message.
         * @param message the message to set.
         */
        public void SendMessage ( Message message ) {
            this.message = message;
            NotifyListeners();
        }

        /**
         * Notify each listener in the listener list by calling the listener's
         * messageReceived() method.
         */
        private void NotifyListeners ( ) {
            for (int i = 0; i < listeners.Count; i++) {
                listeners[i].MessageReceived(message);
            }
        }
    }
}
