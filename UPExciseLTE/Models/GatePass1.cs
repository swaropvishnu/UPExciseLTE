using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class GatePass1
    {
        public long GatePassId { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(7);
        public string To { get; set; } = "";
        public string From { get; set; } = "";
        public long BrandId { get; set; }
        public string ShopName { get; set; } = "";
        public long ShopId { get; set; }
        public string GatePassNo { get; set; } = "";
        public string LicenseeNo { get; set; } = "";
        public string VehicleNo { get; set; } = "";
        public string DriverName { get; set; } = "";
        public string LicenseeName { get; set; } = "";
        public string LicenseeAddress { get; set; } = "";
        public string AgencyNameAndAddress { get; set; } = "";
        public string Address { get; set; } = "";
        public string GrossWeight { get; set; } = "";
        public string TareWeight { get; set; } = "";
        public decimal MDistrictId1 { get; set; }
        public decimal MDistrictId2 { get; set; }
        public decimal MDistrictId3 { get; set; }
        public string GatePassResource { get; set; } = "";
        public long GatePassResourceId { get; set; }
        public int SP_Type { get; set; } = 1;
        public Message Message { get; set; }

    }
}