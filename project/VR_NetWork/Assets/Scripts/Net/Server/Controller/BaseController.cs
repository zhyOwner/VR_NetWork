using Common;

namespace Servers{

    abstract class BaseController
    {
        protected RequestCode requestCode = RequestCode.None;

        public RequestCode RequestCode {
            get
            {
                return requestCode;
            }
        }
        public virtual string DefaultHandle(string data,Client client,Server server) { return null; }
    }
}