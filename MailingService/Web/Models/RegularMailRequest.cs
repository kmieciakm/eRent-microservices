using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Web.Models
{
    public class RegularMailRequest : MailRequestBase
    {
        public string BodyHtml { get; set; }
    }
}
