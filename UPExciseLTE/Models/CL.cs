using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPExciseLTE.Models
{
    public class StorageVAT
    {
        [Display(Name = "Storage VAT Id")]
        public int StorageVATId { get; set; } = -1;
        public string Enc_StorageVATId { get; set; } = "";
        [Display(Name = "Brewery Id")]
        public short BreweryId { get; set; }
        [Display(Name = "Brewery")]
        public string Brewery { get; set; }
        [Display(Name = "Storage VAT Name")]
        public string StorageVATName { get; set; } = "";
        [Display(Name = "Storage VAT Capacity")]
        public float StorageVATCapacity { get; set; } = 0;
        [Display(Name = "Storage VAT Open Balance")]
        public float StorageVATBulkLitre { get; set; } = 0;
        [Display(Name = "Storage VAT Strength")]
        public float StorageVATStrength { get; set; } = 0;
        [Display(Name = "Storage VAT Status")]
        public string Status { get; set; } = "";
        public int Type { get; set; } = 1;
    }
}