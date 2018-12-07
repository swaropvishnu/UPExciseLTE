using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class FormFL21
    {
        public int FL21ID { get; set; } = -1;
        public int BrandId { get; set; } = -1;
        public string Brand { get; set; } = "";
        public short SPType { get; set; } = 1;
        public string EncFL21IDId { get; set; } = "";
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
        public string FL21Status { get; set; } = "P";
        public string FL21Status1 { get; set; } = "";
        public string PackagingType { get; set; } = "";
        public string EntryDate1 { get; set; } = "";
    }
}