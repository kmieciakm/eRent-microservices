using System;
using System.Collections.Generic;
using System.IO;
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
                var currentDirectory = Directory.GetCurrentDirectory();
                return Path.Combine(currentDirectory, "EmailTemplates");
            }
        }
    }
}
