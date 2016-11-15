using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;

[assembly: XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace LM_TestDev
{
    /// <summary>
    /// Lemon Ware Test Dev WebService
    /// <author>Mohamed CHOUAYA</author>
    /// </summary>
    [ScriptService]
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class LM_TestDev_WS : WebService
    {
        // On définit une variable logger static qui référence l'instance du logger nommé Program
        private static readonly ILog log = LogManager.GetLogger(typeof(WebService));


        private void ConfigureLog4net()
        {
            // On récupère le chemin du fichier de config
            // On remplace le BasicConfigurator par le XmlConfigurator
            // et on charge la configuration définie dans le fichier log4net.config
            XmlConfigurator.Configure(new FileInfo(Directory.GetCurrentDirectory() + @"\log4net.config"));
            //BasicConfigurator.Configure();
        }

        /// <summary>
        /// The Fibonacci service takes input an integer <paramref name="n"/>, 
        /// and return the Nth value in the Fibonacci sequence
        /// </summary>
        /// <param name="n">The rank of value to return from the Fibonacci sequence</param>
        /// <returns>
        /// if <paramref name="n"/> is between 1 and 100 the service returns the Nth value in the Fibonacci sequence
        /// othenwise the service must to return -1
        /// </returns>
        /// The Web Service is cached to prevent doing the same job if the input data is the same
        [WebMethod(CacheDuration = 3600, Description = "The Fibonacci service takes input an integer n and return the Nth value in the Fibonacci sequence")]
        public decimal Fibonacci(int n)
        {
            ConfigureLog4net();

            //Saving the starting time
            DateTime start = DateTime.Now;
            log.InfoFormat("Fibonacci({0}) webservice starting at {1:dd/MM/yyyy HH:mm:ss.fff}", n, start);

            //Loval variable for the obtained result
            decimal result;
            if (n < 1 || n > 100)
                result = -1;
            else
                result = FibonacciTailRecursive(n, 1, 0);

            //Saving the ending time
            DateTime end = DateTime.Now;
            //calculating the duration
            TimeSpan duration = end.Subtract(start);

            log.InfoFormat("Fibonacci({0}) webservice ended at {1:dd/MM/yyyy HH:mm:ss.fff} and lasted for {2:c}", n, end, duration);

            return result;
        }

        [WebMethod(Description = "The Fibonacci service takes input an integer n and return the Nth value in the Fibonacci sequence (JSON version)")]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void FibonacciJSON(int n)
        {
            decimal result;
            if (n < 1 || n > 100)
                result = -1;
            else
                result = FibonacciTailRecursive(n, 1, 0);
            Context.Response.ContentType = "application/json";
            Context.Response.Write(string.Format(@"{{ ""n"" : ""{0}"", ""result"" : ""{1}"" }}", n, result));

        }
        /// <summary>
        /// This function is the tail recursive version of the Fibonacci algorithm
        /// </summary>
        /// <param name="term">The current position</param>
        /// <param name="val">The current value</param>
        /// <param name="prev">The previous value</param>
        /// <returns>The value in the Fibonacci sequence corresponding in the position <paramref name="term"/></returns>
        private decimal FibonacciTailRecursive(decimal term, decimal val, decimal prev)
        {
            if (term == 0)
                return prev;
            if (term == 1)
                return val;
            return FibonacciTailRecursive(term - 1, val + prev, val);
        }

        /// <summary>
        /// The XmlToJson service converts an Xml string to it's Json format if it's well-formed.
        /// </summary>
        /// <param name="xml">The Xml string to be converted to Json</param>
        /// <returns>
        /// The json form of the <paramref name="xml"/> string,
        /// it will return "Bad Xml format" if the input string is not a well-formed Xml
        /// </returns>
        [WebMethod(CacheDuration = 3600, MessageName = "XmlToJson", Description = "The XmlToJson service converts an Xml string to it's Json format if it's well-formed.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string XmlToJson(string xml)
        {
            return XmlToJsonWithFormatting(xml, true);
        }
        [WebMethod(CacheDuration = 3600, MessageName = "XmlToJsonWithFormatting", Description = "The XmlToJson service converts an Xml string to it's Json format if it's well-formed.")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string XmlToJsonWithFormatting(string xml, bool isOutputIndented)
        {
            ConfigureLog4net();

            //Saving the starting time
            DateTime start = DateTime.Now;

            log.InfoFormat("XmlToJsonWithFormatting({0}) webservice starting at {1:dd/MM/yyyy HH:mm:ss.fff}", xml, start);
            // Instanciation of the XmlDocument where the xml string is will be loaded
            XmlDocument doc = new XmlDocument();
            try
            {
                // Loading the xml string in  the XmlDocument
                doc.LoadXml(xml);
            }
            catch (XmlException)
            {
                log.ErrorFormat("Bad Xml format - Input : {0}", xml);
                return "Bad Xml format";
            }
            try
            {
                // Serializing the XmlDocument to Json using Newtonsoft library
                // the seconde parameter makes the Json output well-formatted with indentation
                string result = JsonConvert.SerializeXmlNode(doc, isOutputIndented? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);

                //Saving the ending time
                DateTime end = DateTime.Now;
                //calculating the duration
                TimeSpan duration = end.Subtract(start);

                log.InfoFormat("Fibonacci({0}) webservice ended at {1:dd/MM/yyyy HH:mm:ss.fff} and lasted for {2:c}", result, end, duration);

                return result;
            }
            catch (Exception ex)
            {
                log.FatalFormat("Xml serialization exception - Input : {0}", xml);
                return "Xml serialization exception : " + ex.Message;
            }
        }

    }
}
