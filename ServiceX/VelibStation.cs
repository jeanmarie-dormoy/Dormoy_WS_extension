using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ServiceX
{

    [DataContract]
    public class VelibStation
    {

        //[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 1)]
        [DataMember]
        public int number;
        [DataMember]
        public string contract_name;
        [DataMember]
        public string name;
        [DataMember]
        public string address;

        [DataMember]
        public Position position;
        [DataMember]
        public bool banking;
        [DataMember]
        public bool bonus;
        [DataMember]
        public int bike_stands;

        [DataMember]
        public int available_bike_stands;
        [DataMember]
        public int available_bikes;

        [DataMember]
        public string status;
        [DataMember]
        public long last_update;


        public override string ToString()
        {
            string res;
            res = "-------------------------------------\n"
                + number + "\n"
                + contract_name + "\n"
                + name + "\n"
                + address + "\n"
                + position + "\n"
                + banking + "\n"
                + bonus + "\n"
                + bike_stands + "\n"
                + available_bike_stands + "\n"
                + available_bikes + "\n"
                + status + "\n"
                + last_update + "\n"
                + "-------------------------------------\n";
            return res;
        }
    }
}
