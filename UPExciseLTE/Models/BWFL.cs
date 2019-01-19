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
        public string FL21Status { get; set; } = "U";
        public string FL21Status1 { get; set; } = "";
        public string PackagingType { get; set; } = "";
        public string EntryDate1 { get; set; } = "";
        public DateTime? TransactionDate { get; set; } = DateTime.Parse("01-Jan-1990");
        public string TransactionDate1 { get; set; } = "";
        public string Bankname { get; set; } = "";
        public DateTime FromPermitDate { get; set; }= DateTime.Parse("01-Jan-1990");
        public string FromPermitDate1 { get; set; } = "";
        public DateTime ToPermitDate { get; set; } = DateTime.Parse("01-Jan-1990");
        public string ToPermitDate1 { get; set; } = "";
        public int ChallanId { get; set; } = -1;
        public string Reason { get; set; } = "";
        public List<FL21BrandMapp> lstFL21 { get; set; } = new List<FL21BrandMapp>();
        public string UnderBondYesNo { get; set; } = "";
        public string BondExecutedYesNo { get; set; } = "";
    }
    public class FL21BrandMapp 
    {
        public long FL21BrandMappId { get; set; }
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
        public string UnderBondYesNo { get; set; } = "";
        public string BondExecutedYesNo { get; set; } = "";
    }
    public class Challan
    {
        public int ChallanId { get; set; } = -1;
        public string BankName { get; set; } = "";
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal TotalFees { get; set; } = 0;
        public string FL21Ids { get; set; } = "-1";
        public byte[] ChallanPhoto { get; set; }
        public string FileExt { get; set; } = "";
        public string TransactionNo { get; set; } = "";
    }
    #region FL2BGatePass
    public class FL2BGatePassDetails
    {
        public long GatePassId { get; set; } = -1;
        public short GatePassType { get; set; } = -1;
        public string GatepassLicenseNo { get; set; }
        public string GPType { get; set; } = "";
        public string DispatchType { get; set; } = "";
        public string EncPassId { get; set; } = "";
        public string EncGatePassId { get; set; } = "";
        public DateTime FromDate { get; set; } = DateTime.Now;
        public string FromDate1 { get; set; } = DateTime.Now.Day.ToString().Trim().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().Trim().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString().Trim();
        public DateTime ToDate { get; set; } = DateTime.Now;
        public string ToDate1 { get; set; } = "";
        public string ToLicenseType { get; set; } = "";

        public string ToLicenceNo { get; set; } = "";
        public string ToConsigeeName { get; set; } = "";
        public string FromLicenseType { get; set; } = "";
        public string FromLicenceNo { get; set; } = "";
        public string FromConsignorName { get; set; } = "";
        public string ShopName { get; set; } = "";
        public int ShopId { get; set; } = -1;
        public string GatePassNo { get; set; } = "";
        public string LicenseeNo { get; set; } = "";
        public string VehicleNo { get; set; } = "";
        public string DriverName { get; set; } = "";
        public string LicenseeName { get; set; } = "";
        public string LicenseeAddress { get; set; } = "";
        public string AgencyNameAndAddress { get; set; } = "";
        public string ConsignorAddress { get; set; } = "";
        public string ImportPermitNo { get; set; } = "";
        public string ConsigneeAddress { get; set; } = "";
        public decimal GrossWeight { get; set; } = 0;
        public decimal TareWeight { get; set; } = 0;
        public decimal NetWeight { get; set; } = 0;
        public int district_code_census1 { get; set; } = -1;
        public int district_code_census2 { get; set; } = -1;
        public int district_code_census3 { get; set; } = -1;
        public string RouteDetails { get; set; } = "";
        public long GatePassSourceId { get; set; } = -1;
        public string Receiver { get; set; } = "";
        public int UploadValue { get; set; } = -1;
        public int SP_Type { get; set; } = 1;
        public string Status { get; set; } = "P";
        public int TotalCase { get; set; } = 0;
        public int TotalBottle { get; set; } = 0;
        public decimal TotalBL { get; set; } = 0;
        public decimal TotalConsiderationFees { get; set; } = 0;
        public string CheckPostVia { get; set; } = "NA";
        public string DispatchedBy { get; set; } = "Road";
        public decimal InBondValue { get; set; } = 0;
        public decimal ExportDuty { get; set; } = 0;
        public decimal AdditionalConsiFees { get; set; } = 0;
    }
    #endregion
}