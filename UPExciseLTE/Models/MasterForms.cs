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
        [Display(Name ="Menu Id")]
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
        public string Icon { get; set; }= "<i class='fa fa-plus-square'></i>";
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
        public int StateId { get; set; } = 27;
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = "";
        [Display(Name = "Brand Registration Number")]
        public string BrandRegistrationNumber { get; set; } = "";
        [Display(Name = "Strength of Alcohol(%)")]
        public float Strength { get; set; } = 0;
        [Display(Name = "Strength of Alcohol")]
        public string AlcoholType { get; set; } = "";
        [Display(Name = "Liquor Type")]
        public string LiquorType { get; set; } = "";
        [Display(Name = "Licence Type")]
        public string LicenceType { get; set; } = "";
        [Display(Name = "Licence No")]
        public string LicenceNo { get; set; } = "";
        [Display(Name = "MRP")]
        public float MRP { get; set; } = 0;
        [Display(Name = "XFactory Price")]
        public float XFactoryPrice { get; set; } = 0;
        [Display(Name = "Consideration Fees")]
        public float AdditionalDuty { get; set; } = 0;
        [Display(Name = "Box-Size (Number of Bottles)")]
        public int QuantityInCase { get; set; } = 0;
        [Display(Name = "Quantity (In Bottle, In ml.)")]
        public int QuantityInBottleML { get; set; } = 0;
        [Display(Name = "Packaging Type")]
        public string PackagingType { get; set; } = "";
        [Display(Name = "Excise Duty")]
        public float ExciseDuty { get; set; } = 0;
        [Display(Name = "Remark")]
        public string Remark { get; set; } = "";
        public int SPType { get; set; } = 1;
        public bool IsFinal { get; set; } = false;
        
    }
    public class BottelingPlan
    {
        public int Type { get; set; } = 1;
        [Display(Name = "Plan Id")]
        public int PlanId { get; set; } = -1;
        public string EncPlanId { get; set; } = "";
        public int BBTId { get; set; }
        [Display(Name = "Brand Id")]
        public int BrandId { get; set; } = -1;
        [Display(Name = "Plan Date")]
        public DateTime DateOfPlan { get; set; } = DateTime.Now;
        [Display(Name = "Plan Date")]
        public string DateOfPlan1 { get; set; }
        [Display(Name = "Batch No")]
        public string BatchNo { get; set; } = "";
        [Display(Name = "Number Of Cases")]
        public int NumberOfCases { get; set; } = 0;
        [Display(Name = "Mapped Unmapped")]
        public short MappedOrNot { get; set; } = 0;
        [Display(Name = "Is Plan Final")]
        public short IsPlanFinal { get; set; } = 0;
        [Display(Name = "Bulk Liter")]
        public float BulkLiter { get; set; } = 0;
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
        public string Status { get; set; }
        public string State { get; set; }
        [Display(Name = "Bottle/ Can Capacity")]
        public string BottleCapacity { get; set; }
        public bool IsQRGenerated { get; set; }
        // Production Plan Data
        [Display(Name = "Produced Number of cases")]
        public int ProducedNumberOfCases { get; set; }
        [Display(Name = "Produced Total Unit (Can/Bottles)")]
        public float ProducedBoxQuantity { get; set; }
        [Display(Name = "Produced Bulk Liter")]
        public float ProducedBulkLiter { get; set; }
        [Display(Name = "Produced Total Unit")]
        public int ProducedTotalUnit { get; set; }
        [Display(Name = "Wastage in Number")]
        public int WastageInNumber { get; set; }
        [Display(Name = "Wastage BL")]
        public float WastageBL { get; set; }
        [Display(Name = "Is Production Final")]  
        public short IsProductionFinal { get; set; }
        public string TotalRevenue { get; set; }
        public Message Msg { get; set; }
    }
    public class UnitTank
    {
        [Display (Name ="Unit Tank Id")]
        public int UnitTankId { get; set; } = -1;
        public string Enc_UnitTankId { get; set; } = "";
        [Display(Name = "Brewery Id")]
        public short BreweryId { get; set; }   
        [Display(Name = "Brewery")]
        public string Brewery { get; set; }
        [Display(Name = "Unit Tank Name")]
        public string UnitTankName { get; set; } = "";
        [Display(Name = "Unit Tank Capacity")]
        public float UnitTankCapacity { get; set; } = 0;
        [Display(Name = "Unit Tank Open Balance")]
        public float UnitTankBulkLiter { get; set; } = 0;
        [Display(Name = "Unit Tank Strength")]
        public float UnitTankStrength { get; set; } = 0;
        [Display(Name = "Unit Tank Status")]
        public string Status { get; set; } = "";
        public int Type { get; set; } = 1;
    }
    public class UTTransferToBBT
    {
        [Display(Name ="Sno")]
        public int Srno { get; set; }
        [Display(Name = "Transfer Id")]
        public int TransferId { get; set; } = -1;
        [Display(Name = "Issued From UT Id")]
        public int IssuedFromUTId { get; set; } = -1;
        [Display(Name = "Unit Tank")]
        public string UnitTank { get; set; } = "";
        [Display(Name = "Issued To BBT Id")]
        public int BBTID { get; set; } = -1;
        [Display(Name = "BBT Name")]
        public string BBTName { get; set; } = "";
        public string TransactionType { get; set; } = "R";
        [Display(Name = "Issue BL")]
        public float IssueBL { get; set; } = 0;
        [Display(Name = "Wastage")]
        public float Wastage { get; set; } = 0;
        [Display(Name = "Remark")]
        public string Remark { get; set; } = "";
        [Display(Name = "Unit Tank Capacity")]
        public string UnitTankCapacity { get; set; } = "";
        [Display(Name = "Unit Tank Strength")]
        public string Strength { get; set; } = "";
        [Display(Name = "Transfer Date")]
        public DateTime TransferDate { get; set; }
        [Display(Name = "Transfer Date")]
        public string TransferDate1 { get; set; }
        [Display(Name = "Previous UT Balance")]
        public string PrevBalanceUT { get; set; }
        [Display(Name = "Previous BBT Balance")]
        public string PrevBalanceBBT { get; set; }
        [Display(Name = "Current UT Balance")]
        public string CurrentBalanceUT { get; set; }
        [Display(Name = "Previous BBT Balance")]
        public string CurrentBalanceBBT { get; set; }
        public Message Msg { get; set; }
    }
     
}













