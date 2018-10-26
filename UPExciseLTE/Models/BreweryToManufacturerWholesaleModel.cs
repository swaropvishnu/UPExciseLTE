using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class BreweryToManufacturerWholesaleModel
    {

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ValidTill { get; set; }

        public string FromRdoList { get; set; }

        public string SelectedFromRdo { get; set; }

        public string ToRdoList { get; set; }

        public string SelectedToRdo { get; set; }

        public string    RouteDetails { get; set; }

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


        public IEnumerable<DistrictWholeSaleToRetailorModel> DistrictWholeSaleToRetailorList { get; set; }

    }



    public class DistrictWholeSaleToRetailorModel
    {
        [Display(Name = "S.No")]
        public int SerialNo { get; set; }

        //[Display(Name = "Kind Of Liquor")]
        //public string KindOfLiquor { get; set; }

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

        public string Brand { get; set; }

    }

}