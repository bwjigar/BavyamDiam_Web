
namespace Lib.Models
{
    public class Information
    {
        public int InformationID { get; set; }
        public string InformationName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IsProfileChanged { get; set; }
        public byte[] Image { get; set; }
        public string FileExtenstion { get; set; }
        public string OptType { get; set; }
        public string FileName { get; set; }
        public bool IsBeforeLogin { get; set; }
        public string NaturalHeight { get; set; }
        public string NaturalWidth { get; set; }
    }

    public class EventActionRequest
    {
        public int InformationID { get; set; }
        public string Action { get; set; }
    }

    public class CustInformation
    {
        public int CustomerID { get; set; }
        public string UserName { get; set; }
        public int InformationID { get; set; }
        public string InformationName { get; set; }
        public string Action { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FileName { get; set; }
    }
}
