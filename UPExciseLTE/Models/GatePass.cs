using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class GatePass
    {
        public GatePass()
        {
            DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidTill { get; set; }= DateTime.Now.AddDays(7);

        public string FromRdoList { get; set; }

        public string SelectedFromRdo { get; set; }

        public string ToRdoList { get; set; }

        public string SelectedToRdo { get; set; }

        public string RouteDetails { get; set; }

        public string VehicleNo { get; set; }

        public string VehicleDriverName { get; set; }

        public string VehicleAgencyNameAndAddress { get; set; }

        public string  LicenceNo { get; set; }
        public string LicenceName { get; set; }

        public string Address { get; set; }

        public string MajorDistrictsInRoute { get; set; }

        public decimal GrossWeight { get; set; }

        public decimal TareWeight { get; set; }

        public decimal NetWeight { get; set; }

        public string ShopName { get; set; }

        public int ShopId { get; set; }

        public string GatePassNo { get; set; }

        public string LicenseeLicenseNo { get; set; }

        public string LicenseeName { get; set; }

        public string ConsignorName { get; set; }

        public string ConsigneeName { get; set; }


        public string LicenseeAddress { get; set; }

        public int BrandId { get; set; }

        public IEnumerable<DistrictWholeSaleToRetailorModel> DistrictWholeSaleToRetailorList { get; set; }

        public string PassTypeInformation { get; set; }
        public Message Message { get; set; }

        public decimal MDistrictId1 { get; set; }
        public decimal MDistrictId2 { get; set; }
        public decimal MDistrictId3 { get; set; }

        public long GatePassId { get; set; }
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(7);
        public string To { get; set; } = "";
        public string From { get; set; } = "";
        public int SP_Type { get; set; } = 1;

    }



    public class DistrictWholeSaleToRetailorModel
    {
        [Display(Name = "S.No")]
        public int SerialNo { get; set; }

        [Display(Name = "Kind Of Liquor")]
        public string KindOfLiquor { get; set; }

        [Display(Name = "Batch No.")]
        public string BatchNo { get; set; }

        [Display(Name = "Bottles,Flask Quantity Contained in each")]
        public decimal Quantity { get; set; }

        [Display(Name = "BL")]
        public decimal TotalLitresOfBL { get; set; }
        [Display(Name = "AL")]
        public decimal TotalLitresOfAL { get; set; }

        [Display(Name = "Alcoholic Strength")]
        public string AlcohalicStrength { get; set; }

        public int RowNum { get; set; }

        public long TransitGatePassID { get; set; }
        public string BottleQuantity { get; set; }

        public string TotalBottle { get; set; }

        public decimal TotalLitresBL { get; set; }

        public decimal TotalLitresAL { get; set; }

        public decimal AlcoholicStrength { get; set; }

        public string BrewaryName { get; set; }
        public string Revenue { get; set; }
     
        public string Date { get; set; }
        public string VehicleNo { get; set; }
        public string Receiver { get; set; }
        [Display(Name ="Brand")]
        public string Brand { get; set; }

        public string PassType { get; set; }

        public string PassTypeInformation { get; set; }
        [Display(Name ="Size")]
        public int Size { get; set; }
        [Display(Name = "Available Bottle")]
        public int AvailableBottle { get; set; }

        [Display(Name = "Available Box")]
        public int AvailableBox { get; set; }
        [Display(Name ="Dispatch Box")]
        public int DispatchBox { get; set; }
        [Display(Name ="Dispatch Bottle")]
        public int DispatchBottle { get; set; }
        [Display(Name ="Duty")]
        public decimal Duty { get; set; }
        [Display(Name ="Add Duty")]
        public decimal AddDuty { get; set; }
        [Display(Name ="Calculated Duty")]
        public decimal CalculatedDuty { get; set; }

        [Display(Name ="Calculated Additional Duty")]
        public decimal CalculatedAdditionalDuty { get; set; }



    }



    public class InformationByLoggedInUserLevel
    {
        public string Receiver { get; set; }

        public string PassType { get; set; }

        public string PassTypeInformation { get; set; }

    }
     
}