using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteerRent.Model;
using System.Data.SqlClient;
using System.Data;

namespace SteerRent.DAL
{
    public class FleetData
    {
        static string connectionString = Helper.Helper.GetConnectionString();
        
        /// <summary>
        /// Insert/Update of Vehicle Purchase
        /// </summary>
        /// <param name="objPurchase"></param>
        /// <returns></returns>
        public int VehiclePurchaseInsertUpdate(VehiclePurchase objPurchase)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_VehiclePurchaseInsertUpdate";
                    cmd.Parameters.AddWithValue("@PurchaseID", objPurchase.PurchaseID);
                    cmd.Parameters.AddWithValue("@PurchaseDate", objPurchase.PurchaseDate);
		            cmd.Parameters.AddWithValue("@DeliveryDate", objPurchase.DeliveryDate);
		            cmd.Parameters.AddWithValue("@SupplierID", objPurchase.SupplierID);
		            cmd.Parameters.AddWithValue("@PONumber", objPurchase.PONumber);
		            cmd.Parameters.AddWithValue("@POValue", objPurchase.POValue);
		            cmd.Parameters.AddWithValue("@TradeLicenseID", objPurchase.TradeLicenseID);
		            cmd.Parameters.AddWithValue("@BuyBack", objPurchase.BuyBack);
                    cmd.Parameters.AddWithValue("@UserId", objPurchase.UserId);
                    cmd.Parameters.AddWithValue("@IsActive", objPurchase.IsActive);
                    //SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                    //output.Direction = ParameterDirection.ReturnValue;
                    //cmd.Parameters.Add(output);
                    cmd.ExecuteNonQuery();
                    i = 1;
                }
            }
            catch(Exception ex)
            {
                i =0;
                throw ex;
            }
            return i;
        }

        /// <summary>
        /// Insert/Update of Vehicle Registration
        /// </summary>
        /// <param name="objReg"></param>
        /// <returns></returns>
        public int VehicleRegistrationInsertUpdate(VehicleRegistration objReg)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_VehicleRegistrationInsertUpdate";
                    cmd.Parameters.AddWithValue("@RegId", objReg.RegId);
                    cmd.Parameters.AddWithValue("@VehicleID", objReg.VehicleID);
		            cmd.Parameters.AddWithValue("@RegNo", objReg.RegNo);
		            cmd.Parameters.AddWithValue("@RegCountryID", objReg.RegCountryID);
		            cmd.Parameters.AddWithValue("@RegStateID", objReg.RegStateID);
		            cmd.Parameters.AddWithValue("@RegDate", objReg.RegDate);
		            cmd.Parameters.AddWithValue("@RegExpiry", objReg.RegExpiry);
		            cmd.Parameters.AddWithValue("@RegCost", objReg.RegCost);
		            cmd.Parameters.AddWithValue("@RegPlateCodeID", objReg.RegPlateCodeID);
		            cmd.Parameters.AddWithValue("@RegPlateColorID", objReg.RegPlateColorID);
		            cmd.Parameters.AddWithValue("@UserID", objReg.UserId);
		            cmd.Parameters.AddWithValue("@IsActive", objReg.IsActive);
	  
                    //SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                    //output.Direction = ParameterDirection.ReturnValue;
                    //cmd.Parameters.Add(output);
                    cmd.ExecuteNonQuery();
                    i = 1;
                }
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        /// <summary>
        /// Insert/Update of Vehicle Insurance
        /// </summary>
        /// <param name="objIns"></param>
        /// <returns></returns>
        public int VehicleInsuranceInsertUpdate(VehicleInsurance objIns)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_VehicleInsuranceInsertUpdate";
                    cmd.Parameters.AddWithValue("@InsID", objIns.InsID);
                    cmd.Parameters.AddWithValue("@VehicleID", objIns.VehicleID);
		           cmd.Parameters.AddWithValue("@InsTypeID", objIns.InsTypeID);
		           cmd.Parameters.AddWithValue("@InsCompanyID", objIns.InsCompanyID);
		           cmd.Parameters.AddWithValue("@InsPolicyNo", objIns.InsPolicyNo);
		           cmd.Parameters.AddWithValue("@InsCost", objIns.InsCost);
		           cmd.Parameters.AddWithValue("@InsStart", objIns.InsStart);
		           cmd.Parameters.AddWithValue("@InsExpiry", objIns.InsExpiry);
		           cmd.Parameters.AddWithValue("@UserId", objIns.UserId);
                   cmd.Parameters.AddWithValue("@IsActive", objIns.IsActive);

                    //SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                    //output.Direction = ParameterDirection.ReturnValue;
                    //cmd.Parameters.Add(output);
                    cmd.ExecuteNonQuery();
                    i = 1;
                }
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        /// <summary>
        /// Insert/Update of Vehicle Service
        /// </summary>
        /// <param name="objService"></param>
        /// <returns></returns>
        public int VehicleServiceInsertUpdate(VehicleService objService)
        {
            int i = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_VehicleServiceInsertUpdate";
                    cmd.Parameters.AddWithValue("@ServiceID", objService.ServiceID);
                    cmd.Parameters.AddWithValue("@VehicleID", objService.VehicleID);
                    cmd.Parameters.AddWithValue("@WarrantlyKM", objService.WarrantlyKM);
                    cmd.Parameters.AddWithValue("@WarrantyUpto", objService.WarrantyUpto);
                    cmd.Parameters.AddWithValue("@ServiceWithin", objService.ServiceWithin);
                    cmd.Parameters.AddWithValue("@ServiceContract", objService.ServiceContract);
                    cmd.Parameters.AddWithValue("@AgencyID", objService.AgencyID);
                    cmd.Parameters.AddWithValue("@LastServiceDate", objService.LastServiceDate);
                    cmd.Parameters.AddWithValue("@NextServiceDate", objService.NextServiceDate);
                    cmd.Parameters.AddWithValue("@UserId", objService.UserId);
                    cmd.Parameters.AddWithValue("@IsActive", objService.IsActive);

                    //SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                    //output.Direction = ParameterDirection.ReturnValue;
                    //cmd.Parameters.Add(output);
                    cmd.ExecuteNonQuery();
                    i = 1;
                }
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }
    }
}
