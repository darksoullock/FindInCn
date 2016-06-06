using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Helpers
{
    public class ConfigurationHelper
    {
        private static string mailPassword;

        public static string MailPassword => mailPassword ?? (mailPassword = System.IO.File.ReadAllText(@"D:\gmail"));
    }
}
