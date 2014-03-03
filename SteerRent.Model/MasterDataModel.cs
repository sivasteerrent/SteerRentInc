using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SteerRent.Model
{
    public class MasterDataModel
    {
        public decimal LookupCategoryID { get; set; }
        public string LookupCategoryCode { get; set; }
        public string LookupCategoryDesc { get; set; }
        public List<LookupCategoryModel> LookupCategoryList { get; set; }
    }

    public class LookupCategoryModel
    {
        public decimal LookupCategoryID { get; set; }
        public string LookupCategoryCode { get; set; }
        public string LookupCategoryDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsGLookup { get; set; }
        public int UserId { get; set; }
        public bool isLookupExist { get; set; }
        public GlobalEnum.MasterPages PageMode { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
        public List<GLookupDataModel> GLookupList { get; set; }
        public List<HLookupDataModel> HLookupList { get; set; }
        public List<LookupCategoryModel> LookupCategoryList { get; set; }
    }

    public class GLookupDataModel
    {
        public decimal GLookupID { get; set; }
        public decimal LookupCategoryID { get; set; }
        public string GLookupDesc { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
        public bool isGlookExist { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }

    public class HLookupDataModel
    {
        public decimal HLookupID { get; set; }
        public string HLookupDesc { get; set; }
        public decimal GLookupID { get; set; }
        public string GLookupDesc { get; set; }
        public decimal LookupCategoryID { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
        public GlobalEnum.MasterPages PageMode { get; set; }
        public bool isHLookExist { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }

    public class LocationModel
    {
        public decimal LocationId { get; set; }
        public string LocationCode { get; set; }
	    public string LocationName { get; set; }
	    public bool ListedInWeb { get; set; }
	    public decimal WorkingHrs { get; set; }
	    public DateTime? WorkFrom { get; set; }
	    public DateTime? WorksTill { get; set; }
	    public string Phone { get; set; }
	    public string Fax { get; set; }
	    public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Designation { get; set; }
        public string City { get; set; }
        public decimal CountryId { get; set; }
        public decimal EmirateId { get; set; }
        public string Country { get; set; }
        public string Emirate { get; set; }
        public string Zip { get; set; }
        public decimal LocationInChargeId { get; set; }
        public string LocationInCharge { get; set; }
	    public decimal ReciptNoStart { get; set; }
	    public decimal ReceiptNoCurrent { get; set; }
	    public decimal RentalAgreementNoStart { get; set; }
	    public decimal RentalAgreementNoCurrent { get; set; }
	    public decimal LeaseAgreementNoStart { get; set; }
	    public decimal LeaseAgreementNoCurrent { get; set; }
	    public bool IsARevenue { get; set; }    
	    public bool IsACounter { get; set; }
	    public bool IsAWorkShop { get; set; }
	    public bool IsAVirtual { get; set; }
	    public bool LeasingAllowed { get; set; }
	    public bool RentingAllowed { get; set; }
	    public decimal BuId { get; set; }
        public int UserId { get; set; }
	    public bool IsActive { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
        public IList<LocationModel> lstLocation { get; set; }
    }
    
    public class EmployeeModel
    {
        public List<LocationModel> LocationList { get; set; }
        public decimal EmployeeId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmployeeCode { get; set; }
        public Int16 Gender { get; set; }
        public DateTime DOB { get; set; }
        public decimal DesignationId { get; set; }
        public string Designation { get; set; }
        public decimal BUId { get; set; }
        public string BU { get; set; }
        public decimal LocationId { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime LeavingDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal CountryId { get; set; }
        public string CountryName { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
    }

    public class CompanySetup
    {
        public decimal? OrgID { get; set; }
        public string OrgCode { get; set; }
        public string OrgLogoPath { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress1 { get; set; }
        public string OrgAddress2 { get; set; }
        public string OrgAddress3 { get; set; }
        public string OrgCity { get; set; }
        public decimal? OrgCountryId { get; set; }
        public string OrgCountry { get; set; }
        public decimal? OrgEmirate { get; set; }
        public string OrgEmirateName { get; set; }
        public string OrgPostBoxNo { get; set; }
        public string CPersonName { get; set; }
        public string CEMailID { get; set; }
        public string CMobileNo { get; set; }
        public string CPersonDesignation { get; set; }
        public string OrgZip { get; set; }
        public string OrgPhoneNo { get; set; }
        public string OrgFaxNo { get; set; }
        public string OrgEmailID { get; set; }
        public decimal? BaseCurrencyId { get; set; }
        public string BaseCurrency { get; set; }
        public string DateFormat { get; set; }
        public string UserId { get; set; }
        public bool? IsActive { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
        public IList<LocationModel> lstLocation { get; set; }
    }

    public class BuSetup
    { 
        public decimal BuId { get; set; }
        public decimal OrgId { get; set; }
          public string BuCode { get; set; }
          public string BUName { get; set; }
          public string BuAddress1 { get; set; }
          public string BuAddress2 { get; set; }
          public string BuAddress3 { get; set; }
          public string BuPostBox { get; set; }
          public string BuPhoneNo { get; set; }
          public string BuFax { get; set; }
          public string BuEmailId { get; set; }
          public string BuMobile { get; set; }
          public string BuZip { get; set; }
          public string BuContactPerson { get; set; }
          public decimal BuBaseCurrency { get; set; }
          public decimal BuDecimals { get; set; }
          public DateTime CreatedOn { get; set; }
          public DateTime UpdatedOn { get; set; }
          public int UserId { get; set; }
          public bool IsActive { get; set; }
          public GlobalEnum.Flag ActionMode { get; set; }
          public IList<LocationModel> lstLocation { get; set; }
          public string FlexField1 { get; set; }
          public string FlexField2 { get; set; }
          public string FlexField3 { get; set; }
    }

    public class ChargeCodeModel
    {
        public decimal BuId { get; set; }
        public decimal ChargeCodeID { get; set; }
	    public string ChargeCode { get; set; }
	    public string ChargeCodeDesc { get; set; }
	    public bool GroupDriven { get; set; }
	    public bool UnitDriven { get; set; }
	    public bool AdhocValue { get; set; }
	    public bool IsInsurance { get; set; }
	    public bool IsRental { get; set; }
	    public bool IsNonRental { get; set; }
	    public bool IsTrafficViolation { get; set; }
	    public bool IsOtherCompliance { get; set; }
	    public bool IsVasWhileRenting { get; set; }
	    public bool IsVasWhileClosing { get; set; }
	    public bool IsOtherVas { get; set; }
	    public bool ServiceChargeApplicable { get; set; }
	    public string ServiceChargeType { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal WaivingPercentage { get; set; }
	    public bool IsDeductible { get; set; }
	    public bool IsDeductibleWaiver { get; set; }
        public bool isCCExist { get; set; }
	    public bool IsSecured { get; set; }
	    public DateTime CreatedOn { get; set; }
	    public int UserID { get; set; }
	    public DateTime UpdatedOn { get; set; }
	    public bool IsActive { get; set; }
        public List<ChargeCodeModel> lstOfChargeCodes { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
    }

    /// <summary>
    /// User Role  model
    /// </summary>
    public class RoleModel
    {
        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UndatedOn { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        public bool isRoleExist { get; set; }
        public bool? IsActivated { get; set; }
        public GlobalEnum.Flag ActionMode { get; set; }
    }

    /// <summary>
    /// Privileges for users and roles
    /// </summary>
    public class PrivilegeModel
    {
        public List<RoleModel> roleList { get; set; }
        public int ModuleId { get; set; }
        public int PageId { get; set; }
        public string PageIds { get; set; }
        public Guid RoleId { get; set; }
        public string ModuleName { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string PageDetails { get; set; }
        public bool isActive { get; set; }
        public int UserID { get; set; }
    }
}
