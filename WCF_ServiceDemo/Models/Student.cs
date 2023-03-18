using System.Runtime.Serialization;

namespace WCF_ServiceDemo.Models
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string SId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}