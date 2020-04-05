using System;
using static System.Console;
using System.IO;
using System.Xml;
using static System.Environment;
using static System.IO.Path;
using System.IO.Compression;

namespace WorkingWithStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // WorkWithText();
            WorkWithXml();
            WorkWithCompression();
            WorkWithCompression(useBrotli: false);
        }
        static string[] callsigns = new string[]{
            "Husker","Starbuck","Apollo","Boomer",
            "Bulldog","Athena","Helo","Recetrack"
        };

        static void WorkWithText(){
            string textFile=Combine(CurrentDirectory,"streams.txt");
            StreamWriter text=File.CreateText(textFile);
            foreach(string item in callsigns){
                text.WriteLine(item);
            }
            text.Close();
            WriteLine("{0} contains {1:N0} bytes.",
              arg0: textFile,arg1: new FileInfo(textFile).Length);
            WriteLine(File.ReadAllText(textFile));
        }

        static void WorkWithXml(){
            FileStream xmlFileStream=null;
            XmlWriter xml=null;

            try{
                string xmlfile=Combine(CurrentDirectory,"streams.xml");
                xmlFileStream = File.Create(xmlfile);
                xml=XmlWriter.Create(xmlFileStream,new XmlWriterSettings{Indent=true});
                // FileStream xmlFileStream = File.Create(xmlfile);
                // XmlWriter xml=XmlWriter.Create(xmlFileStream,new XmlWriterSettings{Indent=true});
                xml.WriteStartDocument();
                xml.WriteStartElement("callsigns");
                foreach(string item in callsigns){
                    xml.WriteElementString("callsign",item);
                }
                xml.WriteEndElement();
                xml.Close();
                xmlFileStream.Close();
                WriteLine("{0} contains {1:N0} bytes.",
                arg0: xmlfile,arg1:new FileInfo(xmlfile).Length);
                WriteLine(File.ReadAllText(xmlfile));                
            }catch(Exception ex){
                WriteLine($"{ex.GetType()} says {ex.Message}");
            }finally{
                if(xml!=null){
                    xml.Dispose();
                    WriteLine("The XML writer's unmanaged resources have been disposed.");
                }
                if(xmlFileStream!=null){
                    xmlFileStream.Dispose();
                    WriteLine("The file stream's unmanaged resources have been disposed.");
                }
            }
        }

        static void WorkWithCompression(bool useBrotli=true){
            string fileExt=useBrotli ? "brotli":"gzip";
            string gzipFilePath=Combine(CurrentDirectory,$"streams.{fileExt}");
            FileStream gzipFile=File.Create(gzipFilePath);

            Stream compressor;
            if(useBrotli){
                compressor=new BrotliStream(gzipFile,CompressionMode.Compress);
            }else{
                compressor=new GZipStream(gzipFile,CompressionMode.Compress);
            }
            using (compressor){
                using (XmlWriter xmlGzip = XmlWriter.Create(compressor)){
                    xmlGzip.WriteStartDocument();
                    xmlGzip.WriteStartElement("callsigns");
                    foreach(string item in callsigns){
                        xmlGzip.WriteElementString("callsign",item);
                    }
                    //WriteEndElement() will auto added because using() auto dispose
                }
            }
            WriteLine("{0} contains {1:N0} bytes.",
              gzipFilePath,new FileInfo(gzipFilePath).Length);
            WriteLine($"The compressed contents:");
            WriteLine(File.ReadAllText(gzipFilePath));
            WriteLine("Reading the compressed XML file:");
            gzipFile=File.Open(gzipFilePath,FileMode.Open);

            Stream decompressor;
            if(useBrotli){
                decompressor=new BrotliStream(gzipFile,CompressionMode.Decompress);
            }else{
                decompressor=new GZipStream(gzipFile,CompressionMode.Decompress);
            }
            using(decompressor){
                using (XmlReader reader= XmlReader.Create(decompressor)){
                    while(reader.Read()){
                        if((reader.NodeType==XmlNodeType.Element) && (reader.Name=="callsign"))
                        {
                            reader.Read();
                            WriteLine($"{reader.Value}");
                        }
                    }
                }
            }
        }
    }
}
