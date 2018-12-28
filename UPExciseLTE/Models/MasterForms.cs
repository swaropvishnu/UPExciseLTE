﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace UPExciseLTE.Models
{
    #region DynamicMenu
    public class MenuMst
    {
        [Display(Name = "Menu Id")]
        public int MenuId { get; set; } = -1;
        [Display(Name = "Text")]
        public string Text { get; set; } = "";
        [Display(Name = "Description")]
        public string Description { get; set; } = "";
        [Display(Name = "Parent Id")]
        public int ParentId { get; set; } = -1;
        [Display(Name = "Nav Url")]
        public string NavUrl { get; set; } = "#";
        [Display(Name = "Enable")]
        public string Enable { get; set; } = "1";
        [Display(Name = "Show To Post")]
        public string ShowToPost { get; set; } = null;
        [Display(Name = "Show To All")]
        public string ShowToAll { get; set; } = "0";
        [Display(Name = "Show To Menu")]
        public bool ShowInMenu { get; set; } = false;
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; } = null;
        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; } = null;
        [Display(Name = "Created On")]
        public string CreatedOn1 { get; set; } = null;
        [Display(Name = "Order By")]
        public int OrderBy { get; set; } = 1;
        [Display(Name = "Icon")]
        public string Icon { get; set; } = "<i class='fa fa-plus-square'></i>";
        [Display(Name = "Yojana code")]
        public short yojana_code { get; set; } = -1;
        [Display(Name = "Is Print")]
        public bool IsPrint { get; set; } = false;
    }
    public class MenuRightMapping
    {
        public int Id { get; set; } = -1;
        public int MenuID { get; set; } = -1;
        public int RightsId { get; set; } = -1;
    }
    #endregion
    public class BrandMaster
    {
        [Display(Name = "Brand Id")]
        public int BrandId { get; set; } = -1;
        public string brandID_incrpt { get; set; } = "";
        [Display(Name = "Brewery Id")]
        public short BreweryId { get; set; } = -1;
        [Display(Name = "Brewery Name")]
        public string BreweryName { get; set; } = "";
        [Display(Name = "State / Location of Parent Unit")]
        public int StateId { get; set; } = 27;
        [Display(Name = "State Name")]
        public string StateName { get; set; } = "";
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = "";
        [Display(Name = "Brand Registration Number")]
        public string BrandRegistrationNumber { get; set; } = "";
        [Display(Name = "Strength of Alcohol(%)")]
        public decimal Strength { get; set; } = 0;
        [Display(Name = "Strength of Alcohol")]
        public string AlcoholType { get; set; } = "";
        [Display(Name = "Liquor Type")]
        public string LiquorType { get; set; } = "";
        [Display(Name = "Licence Type")]
        public string LicenceType { get; set; } = "";
        [Display(Name = "Licence No")]
        public string LicenceNo { get; set; } = "";
        [Display(Name = "Excise Tin")]
        public string ExciseTin { get; set; } = "";
        [Display(Name = "XFactory Price")]
        public decimal XFactoryPrice { get; set; } = 0;
        [Display(Name = "Consideration Fees")]
        public decimal ConsiderationFees { get; set; } = 0;
        [Display(Name = "WholeSale Margin")]
        public decimal WHMargin { get; set; } = 0;
        [Display(Name = "WholeSale Price")]
        public decimal WHPrice { get; set; } = 0;
        [Display(Name = "Retailer Margin")]
        public decimal RetMargin { get; set; } = 0;
        [Display(Name = "Max Retailer  Price")]
        public decimal MaxRetPrice { get; set; } = 0;
        [Display(Name = "Additional Duty")]
        public decimal AdditionalDuty { get; set; } = 0;
        [Display(Name = "Actual Maximum WholeSale Price")]
        public decimal OriginalRetPrice { get; set; } = 0;
        [Display(Name = "Total Consideration Fees")]
        public decimal ExciseDuty { get; set; } = 0;
        [Display(Name = "MRP")]
        public decimal MRP { get; set; } = 0;
        [Display(Name = "Box-Size (Number of Bottles / Cans)")]
        public int QuantityInCase { get; set; } = 0;
        [Display(Name = "Capacity of Bottle/ Can (in ml.)")]
        public int QuantityInBottleML { get; set; } = 650;
        [Display(Name = "Packaging Type")]
        public string PackagingType { get; set; } = "";
        [Display(Name = "Remark")]
        public string Remark { get; set; } = "";
        [Display(Name = "Reason")]
        public string Reason { get; set; } = "";
        public int SPType { get; set; } = 1;
        [Display(Name = "Brand Status")]
        public string BrandStatus { get; set; } = "P";
        public string Status { get; set; } = "";
    }
    public class BottelingPlan
    {
        public int Type { get; set; } = 1;
        [Display(Name = "Plan Id")]
        public int PlanId { get; set; } = -1;
        public string EncPlanId { get; set; } = "";
        public int BBTId { get; set; }
        public int BottlingLineId { get; set; } = -1;
        [Display(Name = "Brand Id")]
        public int BrandId { get; set; } = -1;
        [Display(Name = "Plan Date")]
        public DateTime DateOfPlan { get; set; } = DateTime.Now;
        [Display(Name = "Plan Date")]
        public string DateOfPlan1 { get; set; } = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        [Display(Name = "Batch No")]
        public string BatchNo { get; set; } = "";
        [Display(Name = "Number Of Cases")]
        public int NumberOfCases { get; set; } = 0;
        [Display(Name = "Mapped Unmapped")]
        public short MappedOrNot { get; set; } = 1;
        [Display(Name = "Is Plan Final")]
        public short IsPlanFinal { get; set; } = 0;
        [Display(Name = "Bulk Litre")]
        public decimal BulkLitre { get; set; } = 0;
        [Display(Name = "Liquor Type")]
        public string LiquorType { get; set; } = "";
        [Display(Name = "Licence Type")]
        public string LicenceType { get; set; }
        [Display(Name = "Box-Quantity (In Case)")]
        public int QunatityInCaseExport { get; set; } = 0;
        [Display(Name = "Total Unit (Cans/Bottles)")]
        public int TotalUnitQuantity { get; set; } = 0;
        [Display(Name = "Licence No")]
        public string LicenseNo { get; set; }
        [Display(Name = "Brand Name")]
        public string Brand { get; set; }
        [Display(Name = "Strength of Alcohol")]
        public string Strength { get; set; }
        [Display(Name = "BBT Bulk Litre")]
        public decimal BBTBulkLitre { get; set; } = 0;
        public string Status { get; set; }
        public string State { get; set; }
        [Display(Name = "Bottle/ Can Capacity")]
        public string BottleCapacity { get; set; }
        public bool IsQRGenerated { get; set; }
        // Production Plan Data
        [Display(Name = "Produced Number of cases")]
        public int ProducedNumberOfCases { get; set; }
        [Display(Name = "Produced Total Unit (Can/Bottles)")]
        public decimal ProducedBoxQuantity { get; set; }
        [Display(Name = "Produced Bulk Litre")]
        public decimal ProducedBulkLitre { get; set; }
        [Display(Name = "Produced Total Unit")]
        public int ProducedTotalUnit { get; set; }
        [Display(Name = "Wastage in Number")]
        public int WastageInNumber { get; set; }
        [Display(Name = "Wastage BL")]
        public decimal WastageBL { get; set; }
        [Display(Name = "Is Production Final")]
        public short IsProductionFinal { get; set; }
        public string TotalRevenue { get; set; }
        public string BeforeBBTBal { get; set; } = "";
        public decimal AfterBBTBal { get; set; } = 0;
        public Message Msg { get; set; }
    }
    public class UnitTank
    {
        [Display(Name = "Unit Tank Id")]
        public int UnitTankId { get; set; } = -1;
        public string Enc_UnitTankId { get; set; } = "";
        [Display(Name = "Brewery Id")]
        public short BreweryId { get; set; }
        [Display(Name = "Brewery")]
        public string Brewery { get; set; }
        [Display(Name = "Unit Tank Name")]
        public string UnitTankName { get; set; } = "";
        [Display(Name = "Unit Tank Capacity")]
        public decimal UnitTankCapacity { get; set; } = 0;
        [Display(Name = "Unit Tank Open Balance")]
        public decimal UnitTankBulkLitre { get; set; } = 0;
        [Display(Name = "Unit Tank Strength")]
        public decimal UnitTankStrength { get; set; } = 0;
        [Display(Name = "Unit Tank Status")]
        public string Status { get; set; } = "";
        public int Type { get; set; } = 1;
    }
    public class UTTransferToBBT
    {
        [Display(Name = "Sno")]
        public int Srno { get; set; }
        [Display(Name = "Transfer Id")]
        public int TransferId { get; set; } = -1;
        [Display(Name = "Issued From UT Id")]
        public int IssuedFromUTId { get; set; } = -1;
        [Display(Name = "Unit Tank")]
        public string UnitTank { get; set; } = "";
        [Display(Name = "Issued To BT Id")]
        public int BBTID { get; set; } = -1;
        [Display(Name = "Bottling Tank Name")]
        public string BBTName { get; set; } = "";
        public string TransactionType { get; set; } = "R";
        [Display(Name = "Issue BL")]
        public decimal IssueBL { get; set; } = 0;
        [Display(Name = "Wastage")]
        public decimal Wastage { get; set; } = 0;
        [Display(Name = "Remark")]
        public string Remark { get; set; } = "";
        [Display(Name = "Unit Tank Capacity")]
        public string UnitTankCapacity { get; set; } = "";
        [Display(Name = "Unit Tank Strength")]
        public string Strength { get; set; } = "";
        [Display(Name = "Transfer Date")]
        public DateTime TransferDate { get; set; } = DateTime.Now;
        public decimal NetTransfer { get; set; } = 0;
        [Display(Name = "Transfer Date")]
        public string TransferDate1 { get; set; } = DateTime.Now.Day.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year.ToString();
        [Display(Name = "Previous UT Balance")]
        public string PrevBalanceUT { get; set; }
        [Display(Name = "Previous BT Balance")]
        public string PrevBalanceBBT { get; set; }
        [Display(Name = "Current UT Balance")]
        public string CurrentBalanceUT { get; set; }
        [Display(Name = "Current BT Balance")]
        public string CurrentBalanceBBT { get; set; }
        public Message Msg { get; set; }
    }
    public class BBTMaster
    {
        [Display(Name = "Bottling Tank Id")]
        public int BBTId { get; set; } = -1;
        public int UnitId { get; set; }
        [Display(Name = "Bottling Tank Name")]
        public string BBTName { get; set; } = "";
        [Display(Name = "Bottling Tank Capacity")]
        public decimal BBTCapacity { get; set; }
        [Display(Name = "Bottling Balance (in B.L.)")] 
        public decimal BBTBulkLitre { get; set; }
        public int SP_Type { get; set; } = 1;
        [Display(Name = "Status")]
        public string Status { get; set; } = "A";
        [Display(Name = "Status")]
        public string Status1 { get; set; } = "";
        public Message Message { get; set; }
        public string BBTId_Encript { get; set; } = "";
    }
    public class BottlingLine
    {
        public int BottlingLineId { get; set; } = -1;
        public string LineType { get; set; } = "A";// A for Automatic S for Semi Automatic M for Manual
        public string LineType1 { get; set; } = "A";// A for Automatic S for Semi Automatic M for Manual
        public int CapacityNoOfCasePerHour { get; set; } = 0;
        public string EncBottlingLineId { get; set; } = "";
        public int BBTId { get; set; } = -1;
        public string BBT { get; set; } = "";
        public short UnitId { get; set; } = -1;
        public string BottlingLineName { get; set; } = "";
        public string BottlingLineStatus { get; set; } = "A";
        public int Type { get; set; } = 1;
    }
    public class GatePassDetails
    {
        public long GatePassId { get; set; } = -1;
        public short GatePassType { get; set; } = -1;
        public string EncPassId { get; set; } = "";
        public string EncGatePassId { get; set; } = "";
        public DateTime FromDate { get; set; } = DateTime.Now;
        public string FromDate1 { get; set; } = "";
        public DateTime ToDate { get; set; } = DateTime.Now;
        public string ToDate1 { get; set; } = "";
        public string ToLicenseType { get; set; } = "";
        public string ToLicenseType1 { get; set; } = "";
        public string FromLicenseType1 { get; set; } = "";

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
        public string Address { get; set; } = "";
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
        public int  TotalCase { get; set; } = 0;
        public int TotalBottle { get; set; } = 0;
        public decimal TotalBL { get; set; } = 0;
        public decimal TotalConsiderationFees { get; set; } = 0;
        public string CheckPostVia { get; set; } = "";
        public decimal InBondValue { get; set; } = 0;
        public decimal ExportDuty { get; set; } = 0;
        public decimal AdditionalConsiFees { get; set; } = 0;
    }
    public class GatePassBrandMapping
    {
        public short Srno { get; set; } = 1;
        public string BrandName { get; set; } = "";
        public string BatchNo { get; set; } = "";
        public string DetailsDesc { get; set; } = "";
        public decimal TotalBL { get; set; } = 0;
        public decimal Strength { get; set; } = 0;
    }
}













