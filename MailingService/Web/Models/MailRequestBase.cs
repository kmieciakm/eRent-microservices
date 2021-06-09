using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Web.Models
{
    public abstract class MailRequestBase
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
    }
}
