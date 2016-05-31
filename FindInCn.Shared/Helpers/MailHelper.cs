using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindInCn.Shared.Helpers
{
    public class MailHelper
    {
        public static void SendPasswd(string address, string key)
        {
#if DEBUG
            var thread = new Thread(new ParameterizedThreadStart(param => { System.Windows.Forms.Clipboard.SetText(key); }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
#endif
        }
    }
}
