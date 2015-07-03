using System.Runtime.Serialization;
using System.Runtime.InteropServices;
namespace lfglnet
{
    [DataContract]
    public class reJSON
    {
        [DataMember]
        public int errCode { get; set; }
        [DataMember]
        public string errMessage { get; set; }
    }

    [DataContract]
    public class JSEQdata
    {
        public JSEQdata() { }
        [DataMember]
        public int Userid { get; set; }
       // [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20)]
        [DataMember]
        public string Checktime{ get; set; }
      //  public string Checktime { get; set; }
        [DataMember]
        public int Checktype { get; set; }
        [DataMember]
        public int Sensorid { get; set; }
        [DataMember]
        public int WorkType { get; set; }
        [DataMember]
        public int AttFlag { get; set; }
        [DataMember]
        public int OpenDoorFlag { get; set; }
    }
 
    [DataContract]
    public class Person
    {
        public Person() { }
        public Person(string firstname, string lastname)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
        }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public TData tdata { get; set; }
    }
    [DataContract]
    public class TData
    {
        public TData() { }
        public TData(string data1, string data2)
        {
            this.data1 = data1;
            this.data2 = data2;
        }
        [DataMember]
        public string data1 { get; set; }
        [DataMember]
        public string data2 { get; set; }
    }
}