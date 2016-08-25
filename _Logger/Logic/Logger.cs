using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logger.DataAccess;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Logger.BusinessLogic
{
    public static class Log
    {
        public static void ServerLog(string serverSessionID, string recordType, string recordValue, string clientSessionID)
        {
            ServerLogtoSQL(serverSessionID, clientSessionID, recordType, recordValue);
        }
        public static void ServerLog(Guid serverSessionID, string recordType, string recordValue, string clientSessionID)
        {
            ServerLogtoSQL(serverSessionID.ToString(), clientSessionID, recordType, recordValue);
        }
        public static void ServerLog(string serverSessionID, string recordType, string recordValue, dynamic postObject = null)
        {
            string clientSessionID = Convert.ToString(postObject.log.clientSessionID);
            ServerLogtoSQL(serverSessionID, clientSessionID, recordType, recordValue);
            ClientLogtoSQL(postObject);
        }
        public static void ServerLog(Guid serverSessionID, string recordType, string recordValue, dynamic postObject = null)
        {
            string clientSessionID = Convert.ToString(postObject.log.clientSessionID);
            ServerLogtoSQL(serverSessionID.ToString(), clientSessionID, recordType, recordValue);
            ClientLogtoSQL(postObject);
        }


        public static void ServerLogtoSQL(string serverSessionID, string clientSessionID, string recordType, string recordValue)
        {
            var logger = new LoggerDataContext();
            logger.insertServerLog(serverSessionID, clientSessionID, recordType, recordValue);
        }
        public static void ClientLogtoSQL(dynamic postObject)
        {
            if (postObject != null)
            {
                var clientLog = postObject.log.clientLog;
                if (clientLog.HasValues == true)
                {
                    var logger = new LoggerDataContext();

                    foreach (dynamic entry in clientLog)
                    {
                        var clientSessionID = Convert.ToString(postObject.log.clientSessionID);
                        var clientTime = Convert.ToString(entry.clientTime);
                        var user = postObject.log.user.ToString(Formatting.None) as string;
                        var recordType = entry.recordType.ToString(Formatting.None) as string;
                        var recordValue = entry.recordValue.ToString(Formatting.None) as string;

                        logger.insertClientLog(clientSessionID, clientTime, user, recordType, recordValue);
                    }
                }
            }            
        }



        public static Guid GenerateServerSessionID()
        {
            return Guid.NewGuid();
        }

        public static string SerializeObject(Object toSerialize)
        {
            return new JavaScriptSerializer().Serialize(toSerialize);
        }
    }
}