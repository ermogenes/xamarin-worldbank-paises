using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldBankPaises.API
{
        [XmlRoot(ElementName = "indicator", Namespace = "http://www.worldbank.org")]
        public class Indicator
        {
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "country", Namespace = "http://www.worldbank.org")]
        public class Country
        {
            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "data", Namespace = "http://www.worldbank.org")]
        public class Data
        {
            [XmlElement(ElementName = "indicator", Namespace = "http://www.worldbank.org")]
            public Indicator Indicator { get; set; }
            [XmlElement(ElementName = "country", Namespace = "http://www.worldbank.org")]
            public Country Country { get; set; }
            [XmlElement(ElementName = "date", Namespace = "http://www.worldbank.org")]
            public string Date { get; set; }
            [XmlElement(ElementName = "value", Namespace = "http://www.worldbank.org")]
            public string Value { get; set; }
            [XmlElement(ElementName = "decimal", Namespace = "http://www.worldbank.org")]
            public string Decimal { get; set; }
        }

        [XmlRoot(ElementName = "data")]
        public class TaxRevenueXmlRoot
        {
            [XmlElement(ElementName = "data", Namespace = "http://www.worldbank.org")]
            public Data Data { get; set; }
        }

    }

