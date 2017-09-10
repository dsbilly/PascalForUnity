using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PascalForUnity.Frontend.Pascal.Tokens
{
    class PascalErrorToken : PascalToken
    {
        /**
    * Constructor.
    * @param source the source from where to fetch subsequent characters.
    * @param errorCode the error code.
    * @param tokenText the text of the erroneous token.
    * @throws Exception if an error occurred.
    */
        public PascalErrorToken(Source source, PascalErrorCode errorCode,
                                String tokenText)
            : base(source) {


            this.text = tokenText;
            this.type = new PascalTokenType(PascalTokenTypeEnum.ERROR);
            this.value = errorCode;
        }

        /**
         * Do nothing.  Do not consume any source characters.
         * @throws Exception if an error occurred.
         */
        protected void Extract() {
        }
    }
}
