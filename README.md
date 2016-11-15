# LW_TestDev

This project provides these Web services (accessible from this link http://lmtestdev.apphb.com/LM_TestDev_WS.asmx):

###[Fibonacci](http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/Fibonacci)
The Fibonacci service takes input an integer n and return the Nth value in the Fibonacci sequence

**_Parameters :_** 
- **n** (_decimal_) : the input number

**_Returns :_** 
- **result** (_decimal_) : the Nth value in the Fibonacci sequence 

**_Call sample :_** 
-  http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/Fibonacci?n=90
-  http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/Fibonacci?n=1000


###[FibonacciJSON](http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/FibonacciJSON)
The Fibonacci service takes input an integer n and return the Nth value in the Fibonacci sequence (JSON version)

**_Parameters :_** 
- **n** (_decimal_) : the input number 

**_Returns :_** 
- **result** (_decimal_) : the Nth value in the Fibonacci sequence 

**_Call sample :_** 
-  http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/FibonacciJSON?n=90
-  http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/FibonacciJSON?n=1000

###[XmlToJson](http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/XmlToJson) 
The XmlToJson service converts an Xml string to it's Json format if it's well-formed.

**_Parameters :_** 
- **xml** (_string_) : the xml to convert into JSON

**_Returns :_** 
- **json** (_string_) : the Nth value in the Fibonacci sequence 

**_Call sample :_** 
```
http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/XmlToJson?xml=<myxml><someElement/></myxml>
```

###[XmlToJsonWithFormatting](http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/XmlToJsonWithFormatting) 
The XmlToJson service converts an Xml string to it's Json format if it's well-formed.

**_Parameters :_** 
- **xml** (_string_) : the xml to convert into JSON

**_Returns :_** 
- **json** (_string_) : the Nth value in the Fibonacci sequence 

**_Call sample :_** 
```
http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/XmlToJsonWithFormatting?xml=<myxml><someElement/></myxml>&isOutputIndented=true
http://lmtestdev.apphb.com/LM_TestDev_WS.asmx/XmlToJsonWithFormatting?xml=<myxml><someElement/></myxml>&isOutputIndented=false
```
