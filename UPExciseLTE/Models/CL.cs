using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class StorageVATCL
    {
        [Display(Name = "Storage VAT Id")]
        public int StorageVATId { get; set; } = -1;
        public string Enc_StorageVATId { get; set; } = "";
        [Display(Name = "Unit Id")]
        public short UnitId { get; set; }
        [Display(Name = "Unit")]
        public string Brewery { get; set; }
        [Display(Name = "Storage VAT Name")]
        public string StorageVATName { get; set; } = "";
        [Display(Name = "Storage VAT Capacity")]
        public decimal StorageVATCapacity { get; set; } = 0;
        [Display(Name = "Storage VAT Open Balance")]
        public decimal StorageVATBulkLitre { get; set; } = 0;
        public decimal StorageVATAlcoholicLiter { get; set; } = 0;
        
        [Display(Name = "Storage VAT Strength")]
        public decimal StorageVATStrength { get; set; } = 0;
        [Display(Name = "Spirit Type ID")]
        public short SpiritTypeID { get; set; } = -1;
        [Display(Name = "Spirit Type")]
        public string SpiritType { get; set; } = "";
        [Display(Name = "Storage VAT Status")]
        public string Status { get; set; } = "";
        public int Type { get; set; } = 1;

    }
    public class TankTransferDetail
    {
        public int TransferId { get; set; } = -1;
        public int IssuedFromSVId { get; set; } = -1;
        public int IssuedFromBlendingId { get; set; } = -1;
        public int BottlingVATId { get; set; } = -1;
        public int SpiritTypeId { get; set; } = -1;
        public int BrandID { get; set; } = -1;
        public string TransactionType { get; set; } = "";
        public int SourceSV { get; set; } = -1;
        public decimal PrevBalanceSV { get; set; } = 0;
        public decimal PrevBalanceBlendingV { get; set; } = 0;
        public decimal PrevBalanceBottlingV { get; set; } = 0;
        public decimal IssueBL { get; set; } = 0;
        public decimal CurrentBalanceSV { get; set; } = 0;
        public decimal CurrentBalanceBledingV { get; set; } = 0;
        public decimal CurrentBalanceBottlingV { get; set; } = 0;
        public decimal PrevALSV { get; set; } = 0;
        public decimal PrevALBlendingV { get; set; } = 0;
        public decimal PrevALBottlingV { get; set; } = 0;
        public decimal IssueAL { get; set; } = 0; 
        public decimal CurrentALSV { get; set; } = 0;
        public decimal CurrentALBledingV { get; set; } = 0;
        public decimal CurrentALBottlingV { get; set; } = 0;
        public decimal dipForm { get; set; } = 0;
        public decimal dipTo { get; set; } = 0;
        public decimal Wastage { get; set; } = 0;
        public DateTime TransferDate { get; set; } = DateTime.Now;
        public bool IsFinal { get; set; } = true;
        public decimal Dip { get; set; } = 0;
        public decimal Temperature { get; set; } = 0;
        public decimal Indication { get; set; } = 0;
        public string Remark { get; set; } = "";    
    }
    public class BlendingVATCL
    {
        [Display(Name = "Blending VAT Id")]
        public int BlendingVATId { get; set; } = -1;
        public string Enc_BlendingVATId { get; set; } = "";
        public int BrandId { get; set; } = -1;
        public int SpiritTypeId { get; set; } = -1;
        public string BrandName { get; set; } = "";
        public string SpiritType { get; set; } = "";
        [Display(Name = "Blending VAT Alcoholic Litre")]
        public decimal BlendingVATAlcoholicLiter { get; set; } = 0;
        [Display(Name = "Brewery Id")]
        public short UnitId { get; set; }
        [Display(Name = "Brewery")]
        public string Brewery { get; set; }
        [Display(Name = "Blending VAT Name")]
        public string BlendingVATName { get; set; } = "";
        [Display(Name = "Blending VAT Capacity")]
        public decimal BlendingVATCapacity { get; set; } = 0;
        [Display(Name = "Blending VAT Bulk Litre")]
        public decimal BlendingVATBulkLitre { get; set; } = 0;
        
        [Display(Name = "Blending VAT Strength")]
        public decimal BlendingVATStrength { get; set; } = 0;
        [Display(Name = "Blending VAT Status")]
        public string Status { get; set; } = "";
        public int Type { get; set; } = 1;
    }
    public class BottelingVATCL
    {
        [Display(Name = "Botteling VAT Id")]
        public int BottelingVATId { get; set; } = -1;
        public string Enc_BottelingVATId { get; set; } = "";
        [Display(Name = "Brewery Id")]
        public short UnitId { get; set; }
        [Display(Name = "Brewery")]
        public string Brewery { get; set; }
        [Display(Name = "Botteling VAT Name")]
        public string BottelingVATName { get; set; } = "";
        [Display(Name = "Botteling VAT Capacity")]
        public decimal BottelingVATCapacity { get; set; } = 0;
        [Display(Name = "Botteling VAT Open Balance")]
        public decimal BottelingVATBulkLitre { get; set; } = 0;
        [Display(Name = "Botteling VAT Strength")]
        public decimal BottelingVATStrength { get; set; } = 0;
        [Display(Name = "Botteling VAT Status")]
        public int BrandId { get; set; } = -1;
        public int SpiritTypeId { get; set; } = -1;
        public string BrandName { get; set; } = "";
        public string SpiritType { get; set; } = "";
        [Display(Name = "Blending VAT Alcoholic Litre")]
        public decimal BottelingVATAlcoholicLiter { get; set; } = 0;
        public string Status { get; set; } = "";
        public int Type { get; set; } = 1;
    }
    public class BottlingLineCL
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

    public class BottelingPlanCL
    {
        public int Type { get; set; } = 1;
        [Display(Name = "Plan Id")]
        public int PlanId { get; set; } = -1;
        public string EncPlanId { get; set; } = "";
        public int BVId { get; set; }
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
        public decimal BVBulkLitre { get; set; } = 0;
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
        public string BeforeBVBal { get; set; } = "";
        public decimal AfterBVBal { get; set; } = 0;
        public Message Msg { get; set; }
    }
    
    public class UTTransferToBBTCL
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

    public class GatePassDetailsCL
    {
        public long GatePassId { get; set; } = -1;
        public short GatePassType { get; set; } = -1;
        public string DispatchType { get; set; } = "";
        public string EncPassId { get; set; } = "";
        public string EncGatePassId { get; set; } = "";
        public DateTime FromDate { get; set; } = DateTime.Now;
        public string FromDate1 { get; set; } = "";
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
        public string CheckPostVia { get; set; } = "";
        public decimal InBondValue { get; set; } = 0;
        public decimal ExportDuty { get; set; } = 0;
        public decimal AdditionalConsiFees { get; set; } = 0;
    }
    public class GatePassBrandMappingCL
    {
        public short Srno { get; set; } = 1;
        public string BrandName { get; set; } = "";
        public string BatchNo { get; set; } = "";
        public string DetailsDesc { get; set; } = "";
        public decimal TotalBL { get; set; } = 0;
        public decimal Strength { get; set; } = 0;
    }
}














