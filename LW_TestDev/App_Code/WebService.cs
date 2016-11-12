using Newtonsoft.Json;
using System;
using System.Web.Services;
using System.Xml;

/// <summary>
/// Description résumée de WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Supprimez les marques de commentaire dans la ligne suivante si vous utilisez des composants conçus 
        //InitializeComponent(); 
    }

    /// <summary>
    /// The Fibonacci service takes input an integer <paramref name="n"/>, and return the Nth value in the Fibonacci sequence
    /// </summary>
    /// <param name="n">The rank of value to return from the Fibonacci sequence</param>
    /// <returns>
    /// if <paramref name="n"/> is between 1 and 100 the service  the Nth value in the Fibonacci sequence
    /// othenwise the service must to return -1
    /// </returns>
    [WebMethod]
    public decimal Fibonacci(int n)
    {
        if (n < 1 || n > 100)
            return -1;
        return FibonacciTailRecursive(n, 1, 0);
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
    [WebMethod]
    public string XmlToJson(string xml)
    {
        // Instanciation of the XmlDocument where the xml string is will be loaded
        XmlDocument doc = new XmlDocument();
        try
        {
            // Loading the xml string in  the XmlDocument
            doc.LoadXml(xml);
        }
        catch (XmlException)
        {
            return "Bad Xml format";
        }
        try
        {
            // Serializing the XmlDocument to Json using Newtonsoft library
            // the seconde parameter makes the Json output well-formatted with indentation
            return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);
        }
        catch (Exception ex)
        {
            return "Xml serialization exception : " + ex.Message;
        }
    }

}
