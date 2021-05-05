using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Domain.Settings
{
    public class TemplatesSettings
    {
        public string ConfirmationFile { get; set; }
        public string ConfirmationFilePath
        {
            get
            {
                return Path.Combine(BasePath, ConfirmationFile);
            }
        }
        public string BasePath
        {
            get
            {
                var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); ;
                return Path.Combine(currentDirectory, "EmailTemplates");
            }
        }
    }
}
