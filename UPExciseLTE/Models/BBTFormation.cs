using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class BBTFormation
    {
        public int BrandID { get; set; }
        public int LiquorTypeID { get; set; }
        public int LicenseTypeID { get; set; }
        public string BBTName { get; set; }
        public decimal BBTCapacity { get; set; }
        public decimal BBTOpeningBalance { get; set; }
    }
}