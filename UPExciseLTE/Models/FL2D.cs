using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    //public class FL2D
    //{

    //}
    public class FormFL33
    {
        public int FL33ID { get; set; } = -1;
        public int BrandId { get; set; } = -1;
        public string Brand { get; set; } = "";
        public short SPType { get; set; } = 1;
        public string EncFL33IDId { get; set; } = "";
        public int BoxSize { get; set; } = 0;
        public decimal QuantityInBottleML { get; set; } = 0;
        public string FromConsignorName { get; set; } = "";
        public string FromConsignorAddress { get; set; } = "";
        public string FromLicenceNo { get; set; } = "";
        public string ToLicenceNo { get; set; } = "";
        public string ToConsigeeName { get; set; } = "";
        public string ToConsigeeAddress { get; set; } = "";
        public int TotalCase { get; set; } = 0;
        public int TotalBottle { get; set; } = 0;
        public decimal TotalBL { get; set; } = 0;
        public decimal DutyCalculated { get; set; } = 0;
        public decimal PermitFees { get; set; } = 0;
        public decimal TotalFees { get; set; } = 0;
        public decimal RateofPermit { get; set; } = 0;
        public string TransactionNo { get; set; } = "";
        public string RouteDetails { get; set; } = "";
        public string FL33Status { get; set; } = "U";
        public string FL33Status1 { get; set; } = "";
        public string PackagingType { get; set; } = "";
        public string EntryDate1 { get; set; } = "";
        public DateTime? TransactionDate { get; set; } = DateTime.Parse("01-Jan-1990");
        public string TransactionDate1 { get; set; } = "";
        public string Bankname { get; set; } = "";
        public DateTime FromPermitDate { get; set; } = DateTime.Parse("01-Jan-1990");
        public string FromPermitDate1 { get; set; } = "";
        public DateTime ToPermitDate { get; set; } = DateTime.Parse("01-Jan-1990");
        public string ToPermitDate1 { get; set; } = "";
        public int ChallanId { get; set; } = -1;
        public string Reason { get; set; } = "";
        public List<FL33BrandMapp> lstFL33 { get; set; } = new List<FL33BrandMapp>();
    }
    public class FL33BrandMapp
    {
        public long FL33BrandMappId { get; set; }
        public int SrNo { get; set; }
        public int BrandId { get; set; }
        public string Brand { get; set; }
        public int BoxSize { get; set; }
        public decimal Quantity { get; set; }
        public int TotalCase { get; set; }
        public int TotalBottle { get; set; }
        public decimal TotalBL { get; set; }
        public decimal DutyCalculated { get; set; }
        public decimal PermitFees { get; set; }
        public decimal TotalFees { get; set; }
        public decimal RateofPermit { get; set; }
    }
    public class FL2DChallan
    {
        public int ChallanId { get; set; } = -1;
        public string BankName { get; set; } = "";
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal TotalFees { get; set; } = 0;
        public string FL33Ids { get; set; } = "-1";
        public byte[] ChallanPhoto { get; set; }
        public string FileExt { get; set; } = "";
        public string TransactionNo { get; set; } = "";
    }
}