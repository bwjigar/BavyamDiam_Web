namespace Lib.Models
{
    public class NotificationGetRequest
    {
        public string TransId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string NotificationName { get; set; }
        public string IsActive { get; set; }
        public string PageNo { get; set; }
        public string PageSize { get; set; }
    }

    public class NotificationGetResponse
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public int iTransId { get; set; }
        public string sName { get; set; }
    }
}
