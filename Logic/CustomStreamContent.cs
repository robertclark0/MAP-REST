using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.IO;
using Logger;

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
            base.Dispose(disposing);

            try
            {
                File.Delete(this.filePath);
            }
            catch (Exception ex)
            {
                Logger.BusinessLogic.Log.ServerLog(null, "stream-error", ex.ToString(), null);
            }
        }
    }
}