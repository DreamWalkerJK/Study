using System.Runtime.Serialization;

namespace WCF_DemoDLL.Models
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
    }
}
