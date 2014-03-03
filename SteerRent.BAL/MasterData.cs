using SteerRent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal=SteerRent.DAL;

namespace SteerRent.BAL
{
    public class MasterData
    {

        dal.MasterData objDAL=new dal.MasterData();
        public LookupCategoryModel GetLookupData(LookupCategoryModel obj)
        {
            return objDAL.GetLookupData(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<GLookupDataModel> GetGLookupDataByLookup(string str,int isGlookup,int gLookupId)
        {
            return objDAL.GetGLookupDataByLookup(str, isGlookup, gLookupId);
        }

        #region Location

               /// <summary>
       /// Get location data
       /// </summary>
       /// <returns></returns>
        public LocationModel GetLocationData(int LocId)
 {
            return objDAL.GetLocationData(LocId);
        }
        /// <summary>
       /// Insert location data
       /// </summary>
       /// <param name="objData"></param>
       /// <returns></returns>
        public LocationModel LocationInsertUpdate(LocationModel objData)
       {
           return objDAL.LocationInsertUpdate(objData);
       }
        #endregion

         #region "Company Setup"

          /// <summary>
       /// Getting the company details
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public CompanySetup GetCompanyDetails(int id)
        { return objDAL.GetCompanyDetails(id); }

       /// <summary>
       /// Inserting/Updating company data
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
        public int CompanyInsertUpdate(CompanySetup objData)
        {
            return objDAL.CompanyInsertUpdate(objData);
        }
        #endregion

        #region ChargeCodes

       /// <summary>
       /// Get all the charge codes
       /// </summary>
       /// <param name="ccID">Charge code id</param>
       /// <returns></returns>
        public List<ChargeCodeModel> GetChargeCodes(int id)
        {
            return objDAL.GetChargeCodes(id);
        }

       /// <summary>
       /// Insert/Update charge codes
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
        public List<ChargeCodeModel> ChargeCodesInsertUpdate(ChargeCodeModel objData)
        { return objDAL.ChargeCodesInsertUpdate(objData); }
        #endregion

        #region "Roles"

       /// <summary>
       /// Insert/Update Roles
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
        public List<RoleModel> RolesInsertUpdate(RoleModel objData)
        { return objDAL.RolesInsertUpdate(objData); }

        /// <summary>
       /// Get all the roles by role id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public List<RoleModel> getAllRoles(Guid id)
        { return objDAL.getAllRoles(id); }

        #endregion

        #region Privilege
        /// <summary>
       /// Assign access to the role
       /// </summary>
       /// <param name="objData"></param>
       /// <returns></returns>
        public int PrivilegeInsertUpdate(PrivilegeModel objData, string formCollection)
        {
            return objDAL.PrivilegeInsertUpdate(objData, formCollection);
        }

        /// <summary>
       /// Get privileges to the role assigned
       /// </summary>
       /// <returns></returns>
       public List<PrivilegeModel> GetPrivileges(Guid roleId)
        {
            return objDAL.GetPrivileges(roleId);
        }
        #endregion

        #region "Employee Master"

        /// <summary>
       /// Get Employee Master data
       /// </summary>
       /// <returns></returns>
       public List<EmployeeModel> GetEmployeeMasterData(decimal EmpId)
       {
           return objDAL.GetEmployeeMasterData(EmpId);
       }

       /// <summary>
       /// Employee master data insertion/update
       /// </summary>
       /// <param name="objEmp"></param>
       /// <returns></returns>
       public List<EmployeeModel> EmployeeInsertUpdate(EmployeeModel objEmp)
       {
           return objDAL.EmployeeInsertUpdate(objEmp);
       }
        #endregion
    }
}
