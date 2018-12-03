using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UPExciseLTE.Models
{
    public class GatePass
    {
        public GatePass()
        {
            DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
        }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; } //= DateTime.Now;

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ValidTill { get; set; }//= DateTime.Now.AddDays(7);

        public string FromRdoList { get; set; }

        public string SelectedFromRdo { get; set; }

        public string ToRdoList { get; set; }

        public string SelectedToRdo { get; set; }

        public string RouteDetails { get; set; }

        public string VehicleNo { get; set; }

        public string VehicleDriverName { get; set; }

        public string AgencyNameAndAddress { get; set; }

        public string LicenceNo { get; set; }

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

        public string LicenseeLicenseNo1 { get; set; }

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

        public int TotalBox { get; set; }

        public int TotalBottles { get; set; }

        public decimal TotalLitres { get; set; }

        public decimal ConsiderationFees { get; set; }

        public string Encrypt_GatePassID { get; set; }

        public decimal TotalLitresQuantity { get; set; }

        public decimal TotalBottleQuantity { get; set; }

    }



    public class DistrictWholeSaleToRetailorModel
    {
        [Display(Name = "S.No")]
        public int SerialNo { get; set; }

        [Display(Name = "Kind Of Liquor")]
        public string KindOfLiquor { get; set; }

        [Display(Name = "Bottles,Flask Quantity Contained in each")]
        public decimal Quantity { get; set; }

        [Display(Name = "BL")]
        public decimal TotalLitresOfBL { get; set; }
        [Display(Name = "AL")]
        public decimal TotalLitresOfAL { get; set; }

        public string TotalBottle { get; set; }

        public decimal TotalLitresBL { get; set; }

        public decimal TotalLitresAL { get; set; }

        public string BrewaryName { get; set; }
        public string Revenue { get; set; }

        public string Date { get; set; }

        public string FromDate { get; set; }
        public string VehicleNo { get; set; }
        public string Receiver { get; set; }

        public string PassType { get; set; }

        public string PassTypeInformation { get; set; }
        [Display(Name = "Size")]
        public int Size { get; set; }
        [Display(Name = "Available Bottle")]
        public int AvailableBottle { get; set; }

        [Display(Name = "Available Box")]
        public int AvailableBox { get; set; }
        [Display(Name = "Dispatch Box")]
        public int DispatchBox { get; set; }
        [Display(Name = "Dispatch Bottle")]
        public int DispatchBottle { get; set; }
        [Display(Name = "Duty")]
        public decimal Duty { get; set; }
        [Display(Name = "Add Duty")]
        public decimal AddDuty { get; set; }
        [Display(Name = "Calculated Duty")]
        public decimal CalculatedDuty { get; set; }

        [Display(Name = "Calculated Additional Duty")]
        public decimal CalculatedAdditionalDuty { get; set; }

        public int RowNum { get; set; }

        public long TransitGatePassID { get; set; }

        [Display(Name = "Brand")]
        public string Brand { get; set; }

        [Display(Name = "Batch No.")]
        public string BatchNo { get; set; }

        public string BottleQuantity { get; set; }

        [Display(Name = "Alcoholic Strength")]
        public decimal AlcoholicStrength { get; set; }

        public decimal TotalLitres { get; set; }
    }


    public class BrewerytToManufacturerGatePass
    {
        public BrewerytToManufacturerGatePass()
        {
            DistrictWholeSaleToRetailorList = new List<DistrictWholeSaleToRetailorModel>();
            Message = new Message();
        }

        public string ToRdoList { get; set; }

        public string SelectedToRdo { get; set; }

        public string LicenceNo { get; set; }

        public string LicenceName { get; set; }

        public string MajorDistrictsInRoute { get; set; }

        public string ShopName { get; set; }

        public int ShopId { get; set; }

        public string GatePassNo { get; set; }

        public string LicenseeName { get; set; }

        public string LicenseeAddress { get; set; }

        public IEnumerable<DistrictWholeSaleToRetailorModel> DistrictWholeSaleToRetailorList { get; set; }

        public string PassTypeInformation { get; set; }

        public Message Message { get; set; }

        public long GatePassId { get; set; }

        public DateTime FromDate { get; set; } = DateTime.Now;

        public DateTime ToDate { get; set; } = DateTime.Now.AddDays(7);

        public string To { get; set; } = "";

        public string From { get; set; } = "";

        public int TotalBox { get; set; }

        public int TotalBottles { get; set; }

        public decimal TotalLitres { get; set; }

        public decimal ConsiderationFees { get; set; }

        public string Encrypt_GatePassID { get; set; }

        public decimal TotalLitresQuantity { get; set; }

        public decimal TotalBottleQuantity { get; set; }


        // Brewary To Manufacturer WholeSale Gate Pass

        [Display(Name = "Date")]
        public string Date { get; set; } //= DateTime.Now;

        [Display(Name = "Valid Till")]
        public string ValidTill { get; set; }//= DateTime.Now.AddDays(7);

        [Display(Name = "From")]
        public string FromLicenseType { get; set; }

        [Display(Name = "To")]
        public string ToLicenseType { get; set; }

        [Display(Name = "Brand ")]
        public IEnumerable<SelectListItem> Brands { get; set; }

        public long? BrandId { get; set; }

        [Display(Name = "District ")]
        public IEnumerable<SelectListItem> Districts { get; set; }
        [Display(Name = "Consinor Licensee Nos ")]

        public IEnumerable<SelectListItem> ConsinorLicenseeNos { get; set; }

        [Display(Name = "Consinee Licensee Nos ")]
        public IEnumerable<SelectListItem> ConsineeLicenseeNos { get; set; }

        [Display(Name = "Consignor Name")]
        public string ConsignorName { get; set; }

        [Display(Name = "Consignee Name ")]
        public string ConsigneeName { get; set; }
        [Display(Name = "Consinor LicenseNo")]

        public string ConsinorLicenseNo { get; set; }
        [Display(Name = "Consinee LicenseNo")]
        public string ConsineeLicenseNo { get; set; }

        [Display(Name = "Vehicle No")]
        public string VehicleNo { get; set; }

        [Display(Name = "Vehicle Driver Name")]
        public string VehicleDriverName { get; set; }

        [Display(Name = "Agency Name And Address")]
        public string AgencyNameAndAddress { get; set; }

        [Display(Name = "Route Details")]
        public string RouteDetails { get; set; }

        public int UploadValue { get; set; }

        public int SP_Type { get; set; } = 1;

        public string Status { get; set; }


        // Manufacturer To District WholeSale Gate Pass

        [Display(Name = "License Type")]
        public IEnumerable<SelectListItem> LicenseTypes { get; set; }

        public IEnumerable<SelectListItem> Shops { get; set; }
        [Display(Name = "From")]
        public short FromLicenseTypeId { get; set; }

        [Display(Name = "To")]
        public short ToLicenseTypeId { get; set; }



        [Display(Name = "Gross Weight")]
        public decimal GrossWeight { get; set; }
        [Display(Name = "Tare Weight")]
        public decimal TareWeight { get; set; }
        [Display(Name = "Net Weight")]
        public decimal NetWeight { get; set; }
        public decimal MDistrictId1 { get; set; }
        public decimal MDistrictId2 { get; set; }
        public decimal MDistrictId3 { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }

        //


    }
    public class InformationByLoggedInUserLevel
    {
        public string Receiver { get; set; }

        public string PassType { get; set; }

        public string PassTypeInformation { get; set; }

    }

}