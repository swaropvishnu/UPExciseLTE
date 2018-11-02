﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace UPExciseLTE.Models
{
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
        [Display(Name = "Additional Duty")]
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
    }
    public class UnitTank
    {
        public int UnitTankId { get; set; } = -1;
        public short BreweryId { get; set; } = -1;
        public string UnitTankName { get; set; } = "";
        public float UnitTankCapacity { get; set; } = 0;
        public float UnitTankBulkLiter { get; set; } = 0;
        public float UnitTankStrength { get; set; } = 0;
        public string Status { get; set; } = "";
    }
}













