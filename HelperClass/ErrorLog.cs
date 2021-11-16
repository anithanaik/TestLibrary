using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LibraryAutomation

{
    /// <summary>
    /// Simple XML-based error log
    /// </summary>
    public class ErrorLog
    {
        //static NCU_LandingPage nc = new NCU_LandingPage();
        #region Members & Constants
        private const string XML_ERRORLOG = "errorlog";
        private const string XML_ERRORLOG_DOCTYPE = "errorlog";
        private const string XML_EXCEPTION = "exception";
        private const string XML_EXCEPTION_TIME = "time";
        private const string XML_EXCEPTION_DESCRIPTION = "description";
        private const string XML_EXCEPTION_FILE = "file";
        private const string XML_EXCEPTION_TRACE = "trace";
        private const string XML_EXCEPTION_METHOD = "method";

       private static string m_logFileName = TestContext.Parameters.Get("ErrorLogFilePath");
       // private static string m_logFileName = TestContext.Parameters.Get(@"C:\Rajesh\Automation\Library\FailureTCScreenShots").ToString().Trim();
        #endregion

        #region Methods
        /// <summary>
        /// Creates new log file specified by FileName
        /// or overwrites existing file.
        /// </summary>
        /// <param name="FileName">name of log file</param>
        private static void CreateLogFile(string LogFileName)
        {
            // create only if not extists
            if (!System.IO.File.Exists(m_logFileName))
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(LogFileName, System.Text.Encoding.UTF8);
                xmlWriter.WriteStartDocument(true);
                xmlWriter.WriteElementString(XML_ERRORLOG, "");
                xmlWriter.Close();
            }
        }

        /// <summary>
        /// Writes exception as XML 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="writer"></param>
        private static void WriteException(Exception e, XmlWriter xmlWriter, string fileName)
        {
            xmlWriter.WriteStartElement(XML_EXCEPTION);

            // write file name that raises exception
            xmlWriter.WriteElementString(XML_EXCEPTION_FILE, fileName);

            // write time of exception
            xmlWriter.WriteElementString(XML_EXCEPTION_TIME, System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            // write the description of exeption
            xmlWriter.WriteElementString(XML_EXCEPTION_DESCRIPTION, e.Message);

            // method where exception was thrown
            if (e.TargetSite != null)
                xmlWriter.WriteElementString(XML_EXCEPTION_METHOD, e.TargetSite.ToString());

            // call stack trace
            xmlWriter.WriteStartElement(XML_EXCEPTION_TRACE);
            xmlWriter.WriteString(e.StackTrace);
            xmlWriter.WriteEndElement();

            if (e.InnerException != null)
            {
                // recursively writes inner exceptions
                WriteException(e.InnerException, xmlWriter, fileName);
            }
        }

        /// <summary>
        /// Saves error message to log file.
        /// </summary>
        /// <param name="e" >Exception to write down into XML file</param>
        public static void SaveExeptionToLog(System.Exception e, string fileName, string locationPath)
        {
            m_logFileName = locationPath;
            //Create Error log file
            Console.WriteLine(m_logFileName);
            CreateLogFile(m_logFileName);

            string strOriginal = null;

            XmlTextReader xmlReader = new XmlTextReader(m_logFileName);
            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            //Moves the reader to the root element.
            // skip <?xml version="1.0" ...
            xmlReader.MoveToContent();
            strOriginal = xmlReader.ReadInnerXml();
            xmlReader.Close();
            // write new document
            XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(m_logFileName, System.Text.Encoding.UTF8);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(XML_ERRORLOG);
            // write original content
            xmlWriter.WriteRaw(strOriginal);
            //write down new exception
            WriteException(e, xmlWriter, fileName);
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }

        public static void SaveExeptionToLog_Old(System.Exception e, string fileName)
        {
            //Create Error log file
            CreateLogFile(m_logFileName);

            string strOriginal = null;

            XmlTextReader xmlReader = new XmlTextReader(m_logFileName);
            xmlReader.WhitespaceHandling = WhitespaceHandling.None;
            //Moves the reader to the root element.
            // skip <?xml version="1.0" ...
            xmlReader.MoveToContent();
            strOriginal = xmlReader.ReadInnerXml();
            xmlReader.Close();
            // write new document
            XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(m_logFileName, System.Text.Encoding.UTF8);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(XML_ERRORLOG);
            // write original content
            xmlWriter.WriteRaw(strOriginal);
            //write down new exception
            WriteException(e, xmlWriter, fileName);
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        }

        #endregion
    }
}
