using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace ServiceX
{
    [DataContract]
    public class Contract
    {
        // [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        [DataMember]
        public string name;
        [DataMember]
        public string commercial_name;
        [DataMember]
        public string[] cities;
        [DataMember]
        public string country_code;


        public override string ToString()
        {
            string loc_cities = "";
            if (cities != null && cities.Length > 0)
                for (int i = 0; i < cities.Length; i++)
                    loc_cities += "\n\t\t" + cities[i] + " ";
            return "{\n" +
                "\tname:\t" + name +
                "\n\tcommercial_name:\t" + commercial_name +
                "\n\tcities:\t" + loc_cities +
                "\n\tcountry_code:\t" + country_code
                + "\n}";
        }

    }
}
