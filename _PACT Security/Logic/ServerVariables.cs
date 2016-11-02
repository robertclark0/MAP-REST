using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PACT.BusinessLogic
{
    public class ServerVariables
    {
        /// <summary>
        /// Get Server Variable value
        /// </summary>
        /// <param name="key">{string} actual server variable description (i.e. HTTP_UID)</param>
        /// <returns>server variable value {string}</returns>
        public static string GetServerVariable(string key)
        {
            var nameValueCollection = HttpContext.Current.Request.ServerVariables;
            var dictionary = nameValueCollection.AllKeys.ToDictionary(k => k, k => nameValueCollection[k]);
            string serverVariable;

            dictionary.TryGetValue(key.ToUpper(), out serverVariable);

            return serverVariable;
        }
    }
}