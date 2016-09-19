using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.IO;

namespace MAP_REST.BusinessLogic
{
    public class CustomStreamContent : StreamContent
    {
        string filePath;

        public CustomStreamContent(string filePath)
            : this(File.OpenRead(filePath))
        {
            this.filePath = filePath;
        }

        private CustomStreamContent(Stream fileStream)
            : base(content: fileStream)
        {
        }

        protected override void Dispose(bool disposing)
        {
            //close the file stream
            base.Dispose(disposing);

            try
            {
                File.Delete(this.filePath);
            }
            catch (Exception ex)
            {
                //log this exception somewhere so that you know something bad happened
            }
        }
    }
}