using System;

namespace UPExciseLTE.Models
{
    public class UnitMaster
    {
        public int UnitId { get; set; } = -1;
        public int UserLevel { get; set; } = 15;
        public string UnitType { get; set; } = "";
        public string LicenseType { get; set; } = "";
        public string UnitName { get; set; } = "";
        public string LicenseNo { get; set; } = "";
        public string UnitAddress { get; set; } = "";
        public string ContactPersonName { get; set; } = "";
        public string Mobile { get; set; } = "";
        public string Email { get; set; } = "";
        public string ValidityOfLicense { get; set; } = "";
        public DateTime ValidityOfLicense1 { get; set; }
        public string StateCode { get; set; } = "";
        public string DistrictCode { get; set; } = "";
        public string TehsilCode { get; set; } = "";
        public string LicenseHolderParentUnit { get; set; } = "";// In Case of FL3A Outside State
        public string UnitAddressofBottlingUnit { get; set; } = "";  // In Case of FL3A
        public string Remark { get; set; } = "";
        public string ProductionLiquorType { get; set; } = "";
        public byte[] LicensePhoto { get; set; }
        public string FileExt { get; set; }
        public string OwnerName { get; set; } = "";
        public string UnitCapacity { get; set; } = "";
        public string UnitPhone { get; set; } = "";
        public string UnitFax { get; set; } = "";
        public string ApproverUserID { get; set; } = "";
        public int ParentUnitId { get; set; } = 0;
        public string DECApprovalStatus { get; set; } = "P";
        public string Reason { get; set; } = "";
        public int SPType { get; set; } = 1;
    }
}