using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple {
    internal class LoggerXML : ILogger {
        ILogger originalLogger;

        public LoggerXML(ILogger logger) {
            originalLogger = logger;
        }

        public void LogInfo(string message, params object[] args)
        {
            originalLogger.LogInfo(message, args);
            WriteToFile("INFO", message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            originalLogger.LogWarning(message, args);
            WriteToFile("WARN", message, args);
        }

        private void WriteToFile(string type, string message, params object[] args)
        {
            using (StreamWriter logfile = File.AppendText("log.xml"))
            {
                logfile.WriteLine("<log><type>" + type + "</type><message>" + message + "</message></log>", args);
            }
        }
    }
}