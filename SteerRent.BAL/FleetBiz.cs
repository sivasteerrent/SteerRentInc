using SteerRent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dal = SteerRent.DAL;

namespace SteerRent.BAL
{
    public class FleetBiz
    {
        dal.FleetData objDal = new dal.FleetData();

        /// <summary>
        /// Insert/Update of Vehicle Purchase
        /// </summary>
        /// <param name="objPurchase"></param>
        /// <returns></returns>
        public int VehiclePurchaseInsertUpdate(VehiclePurchase objPurchase)
        {
            return objDal.VehiclePurchaseInsertUpdate(objPurchase);
        }

                /// <summary>
        /// Insert/Update of Vehicle Registration
        /// </summary>
        /// <param name="objReg"></param>
        /// <returns></returns>
        public int VehicleRegistrationInsertUpdate(VehicleRegistration objReg)
        {
            return objDal.VehicleRegistrationInsertUpdate(objReg);
        }

            /// <summary>
        /// Insert/Update of Vehicle Insurance
        /// </summary>
        /// <param name="objIns"></param>
        /// <returns></returns>
        public int VehicleInsuranceInsertUpdate(VehicleInsurance objIns)
        {
            return objDal.VehicleInsuranceInsertUpdate(objIns);
        }

            /// <summary>
        /// Insert/Update of Vehicle Service
        /// </summary>
        /// <param name="objService"></param>
        /// <returns></returns>
        public int VehicleServiceInsertUpdate(VehicleService objService)
        {
            return objDal.VehicleServiceInsertUpdate(objService);
        }
    }
}
