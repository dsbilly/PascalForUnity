using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.Frontend.Pascal
{
    class PascalToken : Token
    {
        /**
    * Constructor.
    * @param source the source from where to fetch the token's characters.
    * @throws Exception if an error occurred.
    */
        public PascalToken(Source source):base(source) {
            
        }
    }
}
