﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12
{
    internal static class EmbeddedResources
    {
        private static TransactionSpecification _837specification;

        internal static TransactionSpecification Get837TransactionSpecification()
        {
            if (_837specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Claims.Ansi-837-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _837specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _837specification;
        }

        private static XslCompiledTransform _837Transform;

        internal static XslCompiledTransform Get837Transform()
        {
            if (_837Transform == null)
            {
                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Ansi-837-To-Claim.xslt"));
                _837Transform = new XslCompiledTransform();
                _837Transform.Load(xsltReader);
            }
            return _837Transform;
        }

        private static TransactionSpecification _835specification;

        internal static TransactionSpecification Get835TransactionSpecification()
        {
            if (_835specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Payments.Ansi-835-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _835specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _835specification;
        }

        private static XslCompiledTransform _835Transform;

        internal static XslCompiledTransform Get835Transform()
        {
            if (_835Transform == null)
            {                
                _835Transform = new XslCompiledTransform();

                WriteToFile("OopFactory.X12.Transformations.Ansi-Common.xslt", Environment.CurrentDirectory + "\\Ansi-Common.xslt");

                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Ansi-835-To-Payment.xslt"));
                
                _835Transform.Load(xsltReader);
                
            }
            return _835Transform;
        }

        private static void WriteToFile(string resourceName, string fileName)
        {
            StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName));
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);
            writer.Write(reader.ReadToEnd());
            writer.Close();
            fs.Close();
        }
    }
}