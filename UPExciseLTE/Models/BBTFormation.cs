using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class BBTFormation
    {
      
        public int BBTId { get; set; }
        public int BrandID { get; set; }
        public int LiquorTypeID { get; set; }
        public int LicenseTypeID { get; set; }
        public string BBTName { get; set; }
        public decimal BBTCapacity { get; set; }
        public decimal BBTBulkLiter { get; set; }
        public string LiquorType { get; set; }
        public string LicenseType { get; set; }
        public int SP_Type { get; set; }
        public string Status { get; set; }
        public string  MacAddress{ get; set; }
        public Message Message { get; set; }
    }
}