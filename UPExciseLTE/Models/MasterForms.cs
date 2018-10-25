using System;
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
        public int BrandId { get; set; }
        [Display(Name = "Brewery Id")]
        public short BreweryId { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        [Display(Name = "Brand Reg No")]
        public string BrandRegistrationNumber { get; set; }
        [Display(Name = "Strength")]
        public float Strength { get; set; }
        [Display(Name = "Liquor Type")]
        public string LiquorType { get; set; }
        [Display(Name = "Licence Type")]
        public string LicenceType { get; set; }
        [Display(Name = "Licence No")]
        public string LicenceNo { get; set; }
        [Display(Name = "Exicise Tin")]
        public string ExiciseTin { get; set; }
        [Display(Name = "MRP")]
        public float MRP { get; set; }
        [Display(Name = "XFactory Price")]
        public float XFactoryPrice { get; set; }
        [Display(Name = "Additional Duty")]
        public float AdditionalDuty { get; set; }
        [Display(Name = "Quantity (In Case)")]
        public int QuantityInCase { get; set; }
        [Display(Name = "Quantity (In Bottle ML)")]
        public int QuantityInBottleML { get; set; }
        [Display(Name = "Excise Duty")]
        public float ExciseDuty { get; set; }
        [Display(Name = "Export Box Size")]
        public int ExportBoxSize { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public int SPType { get; set; }
        public string dbName { get; set; }
    }
    public class Brewery
    {
        public short BreweryId { get; set; }
        public short DistrictCode { get; set; }
        public int TehsilCode { get; set; }
        public string BreweryName { get; set; }
        public string BreweryLicenseno { get; set; }
        public string BreweryAddress { get; set; }
        public string BreweryContactPerson { get; set; }
        public string BreweryContactPersonMobile { get; set; }
        public float BreweryCapacity { get; set; }
        public string BreweryPhone { get; set; }
        public string BreweryFax { get; set; }
        public string BreweryEmail { get; set; }
        public string Remark { get; set; }
    }
    public class BottelingPlan
    {
        public string dbName { get; set; }
        public int Type { get; set; }
        [Display (Name="Plan Id")]
        public int PlanId { get; set; }
        public string EncPlanId { get; set; }
        [Display(Name = "Brewery Id")]
        public short BreweryId { get; set; }
        [Display(Name = "Production for State Code")]
        public short ProductionforStateCode { get; set; }
        [Display(Name = "Is State Same")]
        public bool IsStateSame { get; set; }
        [Display(Name = "Brand Id")]
        public int BrandId { get; set; }
        [Display(Name = "Number Of Cases")]
        public int NumberOfCases { get; set; }
        [Display(Name = "Qunatity In Case Export")]
        public int QunatityInCaseExport { get; set; }
        [Display(Name = "Bulk Liter")]
        public float BulkLiter { get; set; }
        [Display(Name = "Alcoholic Liter")]
        public float AlcoholicLiter { get; set; }
        [Display(Name = "Total Unit Quantity")]
        public int TotalUnitQuantity { get; set; }
        [Display(Name = "Plan Date")]
        public DateTime DateOfPlan { get; set; }
        [Display(Name = "Plan Date")]
        public string DateOfPlan1 { get; set; }
        [Display(Name = "Batch No")]
        public string BatchNo { get; set; }
        [Display(Name = "Mapped Unmapped")]
        public short MappedOrNot { get; set; }
        [Display(Name = "Is Plan Final")]
        public short IsPlanFinal { get; set; }
        
        [Display(Name = "Liquor Type")]  
        public string LiquorType { get; set; }
        [Display(Name = "Licence Type")]   
        public string LicenceType { get; set; }
        public string Brand { get; set; }
        public string Capacity { get; set; }
        [Display(Name = "Plan No of Bottling")]        
        public string PlanNoofBottling { get; set; }
        public string Quantity { get; set; }
        public string LicenseNo { get; set; }
        public string Status { get; set; }
        public string QuantityInBottleML { get; set; }
        public string Strength { get; set; }
        // Production Plan Data
        [Display(Name = "Produced Number of cases")]
        public int ProducedNumberOfCases { get; set; }
        [Display(Name = "Produced Quantity in case Export")]
        public float ProducedQunatityInCaseExport { get; set; }
        [Display(Name = "Produced Bulk Liter")]
        public float ProducedBulkLiter { get; set; }
        [Display(Name = "Produced Alcoholic Liter")]
        public int ProducedAlcoholicLiter { get; set; }
        [Display(Name = "Produced Total Unit Quantity")]
        public int ProducedTotalUnitQuantity { get; set; }
        [Display(Name = "Wastage in Number")]
        public int WastageInNumber { get; set; }
        [Display(Name = "Wastage BL")]
        public float WastageBL { get; set; }
        [Display(Name = "Is Production Final")]
        public short IsProductionFinal { get; set; }
    }

}













