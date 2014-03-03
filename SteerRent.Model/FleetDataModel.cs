using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteerRent.Model
{
    public class FleetDataModel
    {

    }

    public class VehicleMaster
    {
        public decimal VehicleID { get; set; }
        public decimal VehicleCode { get; set; }
        public decimal BuId { get; set; }
        public decimal UsageType { get; set; }
        public decimal InductingLocationID { get; set; }
        public decimal CurrentLocationID { get; set; }
        public decimal StatusID { get; set; }
        public decimal GroupID { get; set; }
        public decimal SubGroupID { get; set; }
        public decimal MakeID { get; set; }
        public decimal BrandID { get; set; }
        public decimal YearModel { get; set; }
        public decimal VehicleTypeID { get; set; }
        public decimal VehicleSubTypeID { get; set; }
        public decimal TransmissionTypeID { get; set; }
        public bool AC { get; set; }
        public decimal EngineCapacityID { get; set; }
        public string VehicleClass { get; set; }
        public decimal SeatingCapacity { get; set; }
        public decimal ColorID { get; set; }
        public decimal SubColorID { get; set; }
        public decimal FuelTypeID { get; set; }
        public decimal FuelTankSize { get; set; }
        public bool FlaggedForSale { get; set; }
        public bool MarkedForSale { get; set; }
        public bool IsAvailableForRenting { get; set; }
        public int NoOfKeys { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
    }

    public class VehiclePurchase
    {
        public decimal PurchaseID { get; set; }
        public decimal VehicleID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal SupplierID { get; set; }
        public string PONumber { get; set; }
        public decimal POValue { get; set; }
        public decimal TradeLicenseID { get; set; }
        public int BuyBack { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }

    public class VehicleRegistration
    {
        public decimal RegId { get; set; }
        public decimal VehicleID { get; set; }
        public int RegNo { get; set; }
        public decimal RegCountryID { get; set; }
        public decimal RegStateID { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RegExpiry { get; set; }
        public decimal RegCost { get; set; }
        public decimal RegPlateCodeID { get; set; }
        public decimal RegPlateColorID { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }

    public class VehicleService
    {
        public decimal ServiceID { get; set; }
        public decimal VehicleID { get; set; }
        public decimal WarrantlyKM { get; set; }
        public DateTime WarrantyUpto { get; set; }
        public int ServiceWithin { get; set; }
        public bool ServiceContract { get; set; }
        public string AgencyID { get; set; }
        public DateTime LastServiceDate { get; set; }
        public DateTime NextServiceDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }

    public class VehicleInsurance
    {
        public decimal InsID { get; set; }
        public decimal VehicleID { get; set; }
        public decimal InsTypeID { get; set; }
        public string InsCompanyID { get; set; }
        public string InsPolicyNo { get; set; }
        public decimal InsCost { get; set; }
        public DateTime InsStart { get; set; }
        public DateTime InsExpiry { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
