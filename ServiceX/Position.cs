using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace ServiceX
{
    [DataContract]
    public class Position
    {
        [DataMember]
        public double lat;
        [DataMember]
        public double lng;


        public override string ToString()
        {
            string res;
            res = "{\n\t" + lat + "\n\t"
                + lng + "\n}";
            return res;
        }
    }
}
