using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UPExciseLTE.Models
{
    public class BBTFormation
    {
        public int BBTId { get; set; } = 0;
        public int BrandID { get; set; } = 0;
        public int LiquorTypeID { get; set; } = 0;
        public int LicenseTypeID { get; set; }
        [Display(Name = "BBT Name")]
        public string BBTName { get; set; } = "";
        [Display(Name = "BBT Capacity")]
        public decimal BBTCapacity { get; set; }
        [Display(Name = "BBT Bulk Liter")]
        public decimal BBTBulkLiter { get; set; }
        public string LiquorType { get; set; } = "";
        public string LicenseType { get; set; } = "";
        public int SP_Type { get; set; } = 1;
        [Display(Name = "Status")]
        public string Status { get; set; } = "";
        public string MacAddress { get; set; } = "";
        public Message Message { get; set; }
        [Display(Name ="Sr.No")]
        public int RowNum { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } = "";

        public string BBTId_Encript { get; set; } = "";
    }
}