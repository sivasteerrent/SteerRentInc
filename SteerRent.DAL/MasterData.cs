
using SteerRent.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteerRent.DAL
{
   public class MasterData
   {
       static string connectionString = Helper.Helper.GetConnectionString();
       //static string conStrUser = Helper.Helper.GetConnectionString_aspnetDB();

       #region "Lookup"
       public List<GLookupDataModel> GetGLookupDataByLookup(string str, int isGlookup,int gLookupId)
        {
            string connectionString = Helper.Helper.GetConnectionString();
            List<GLookupDataModel> lstOfData = new List<GLookupDataModel>();
           string queryString =string.Empty;
            if (gLookupId > 0)
                queryString = "select GL.HLookupID ID,GL.HLookupDesc [Desc],LC.LookupCategoryID  from HLookup GL join LookupCategories LC on LC.LookupCategoryID = GL.LookupCategoryID AND IsGLookup=0 AND GL.IsActive=1 AND LC.IsActive=1 WHERE UPPER(LookupCategoryCode) LIKE '%" + str.ToUpper() + "%' and GlookupID=" + gLookupId + " ORDER BY HLookupDesc ASC";
           else
                queryString = "select GL.GLookupID ID,GL.GLookupDesc [Desc],LC.LookupCategoryID from GLookup GL join LookupCategories LC on LC.LookupCategoryID = GL.LookupCategoryID AND IsGLookup=" + isGlookup + "AND GL.IsActive=1 AND LC.IsActive=1 WHERE UPPER(LookupCategoryCode) LIKE '%" + str.ToUpper() + "%' ORDER BY GLookupDesc ASC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = con.CreateCommand();
                command.CommandText = queryString;

                try
                {
                    con.Open();

                    //SqlDataReader reader = command.ExecuteReader();

                    //while (reader.Read())
                    //{
                    //    Console.WriteLine("\t{0}\t{1}",
                    //        reader[0], reader[1]);
                    //}
                    command.CommandType = CommandType.Text;
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    lstOfData = ds.Tables[0].AsEnumerable().Select(row =>
                    new GLookupDataModel
                    {
                        LookupCategoryID = row.Field<decimal>("LookupCategoryID"),
                        GLookupDesc = row.Field<string>("Desc"),
                        GLookupID = row.Field<decimal>("ID")

                    }).ToList();

                    //reader.Close();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }

            //type 1
            //var q = from role in ds.Role
            //        select role;

            //DataTable dtRole = q.CopyToDataTable();

            //type2
            //var query = from p in dtRole.AsEnumerable()
            //            where p.Field<string>("RoleDescription")
            //            == "Manager"
            //            select p;

            //foreach (var record in query)
            //{
            //    Console.WriteLine("Role: {0} {1}",
            //      record.Field<int>("ID"),
            //      record.Field<string>("RoleDescription"));
            //}

            //Type3
        //        IList<Class1> items = dt.AsEnumerable().Select(row =>
        //new Class1
        //{
        //    id = row.Field<string>("id"),
        //    name = row.Field<string>("name")
        //}).ToList();

            //Dynamic - type4
            //to convert entity collections to a dataset?
            //forums.asp.net/t/1559861.aspx

            return lstOfData;

        }

       public LookupCategoryModel GetLookupData(LookupCategoryModel obj)
        {
            string connectionString = Helper.Helper.GetConnectionString();
            LookupCategoryModel objLookup = new LookupCategoryModel();
            bool ReturnValue = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    //DATAREADER EXAMPLE : BEGIN
                    //SqlDataReader reader = command.ExecuteReader();
                    //string GetOutputFileInfoCmd = "SELECT Content.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() FROM tableName WHERE containerID = @ContainerID";
                    //SqlTransaction txn = null;
                    //SqlCommand cmd = new SqlCommand(GetOutputFileInfoCmd, con, txn);
                    //cmd.Parameters.Add("@ContainerID", SqlDbType.UniqueIdentifier).Value = fileID;
                    //using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    //{
                    //    rdr.Read();
                    //    txnToken = rdr.GetSqlBinary(1).Value;
                    //    filePath = rdr.GetSqlString(0).Value;
                    //    rdr.Close();
                    //}
                    //DATAREADER EXAMPLE : END

                    SqlCommand cmd = con.CreateCommand();
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();

                    //if (obj.ActionMode == GlobalEnum.Flag.Select && (obj.PageMode == GlobalEnum.MasterPages.Lookup || obj.PageMode == GlobalEnum.MasterPages.GAndLookup || obj.PageMode == GlobalEnum.MasterPages.HAndLookup))
                    {
                        cmd.CommandText = "SELECT LookupCategoryID,LookupCategoryCode,LookupCategoryDesc,IsActive,IsGLookup FROM LookupCategories";// "usp_LookupCategoriesSelect";
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddWithValue("@LookupCategoryID", DBNull.Value);
                        ds = new DataSet();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        objLookup.LookupCategoryList = ds.Tables[0].AsEnumerable().Select(row =>
                        new LookupCategoryModel
                        {
                            LookupCategoryID = row.Field<decimal>("LookupCategoryID"),
                            LookupCategoryCode = row.Field<string>("LookupCategoryCode"),
                            LookupCategoryDesc = row.Field<string>("LookupCategoryDesc"),
                            IsActive = row.Field<bool>("IsActive"),
                            IsGLookup = row.Field<bool>("IsGLookup")
                        }).ToList();
                    }

                    if ((obj.ActionMode == GlobalEnum.Flag.Select || obj.ActionMode == GlobalEnum.Flag.Insert) && (obj.PageMode == GlobalEnum.MasterPages.GLookup || obj.PageMode == GlobalEnum.MasterPages.GAndLookup))
                    {
                        if (obj.ActionMode == GlobalEnum.Flag.Insert)
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "usp_GLookupInsert";
                            cmd.Parameters.AddWithValue("@LookupCategoryID", obj.LookupCategoryID);
                            cmd.Parameters.AddWithValue("@GLookupDesc", obj.LookupCategoryCode);
                            cmd.Parameters.AddWithValue("@CreatedBy", obj.UserId);
                            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                            SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                            output.Direction = ParameterDirection.ReturnValue;
                            cmd.Parameters.Add(output);
                            cmd.ExecuteNonQuery();
                            ReturnValue = Convert.ToInt32(output.Value) > 0 ? false : true;

                        }

                        cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GLookupSelect";
                        if (obj.LookupCategoryID > 0)
                            cmd.Parameters.AddWithValue("@GLookupID", obj.LookupCategoryID);
                        else
                            cmd.Parameters.AddWithValue("@GLookupID", DBNull.Value);
                        ds = new DataSet();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                         objLookup.GLookupList = ds.Tables[0].AsEnumerable().Select(row =>
                         new GLookupDataModel
                         {
                             GLookupID = row.Field<decimal>("GLookupID"),
                             LookupCategoryID = row.Field<decimal>("LookupCategoryID"),
                             GLookupDesc = row.Field<string>("GLookupDesc"),
                             IsActive = row.Field<bool>("IsActive"),
                             isGlookExist = ReturnValue
                         }).ToList();
                    }

                    if (obj.PageMode == GlobalEnum.MasterPages.GLookup && obj.ActionMode == GlobalEnum.Flag.StatusUpdate)
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "usp_GLookupUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GLookupID", obj.LookupCategoryID);
                        cmd.Parameters.AddWithValue("@GLookupDesc", obj.LookupCategoryDesc);
                        cmd.Parameters.AddWithValue("@UpdatedBy", obj.UserId);
                        cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                        cmd.Parameters.AddWithValue("@FlexField1", "StatusUpdate");
                        cmd.ExecuteNonQuery();
                    }

                    if ((obj.ActionMode == GlobalEnum.Flag.Select || obj.ActionMode == GlobalEnum.Flag.Insert) && obj.PageMode == GlobalEnum.MasterPages.HLookup)
                    {
                        if (obj.ActionMode == GlobalEnum.Flag.Insert)
                        {
                            cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "usp_HLookupInsert";
                            cmd.Parameters.AddWithValue("@LookupCategoryID", obj.LookupCategoryID);
                            cmd.Parameters.AddWithValue("@GLookupID", obj.HLookupList[0].GLookupID);
                            cmd.Parameters.AddWithValue("@HLookupDesc", obj.HLookupList[0].HLookupDesc);
                            cmd.Parameters.AddWithValue("@GLookupDesc", obj.HLookupList[0].GLookupDesc);
                            cmd.Parameters.AddWithValue("@CreatedBy", obj.UserId);
                            cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);

                            SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                            output.Direction = ParameterDirection.ReturnValue;
                            cmd.Parameters.Add(output);
                            cmd.ExecuteNonQuery();
                            ReturnValue = Convert.ToInt32(output.Value) > 0 ? false : true;
                        }

                        string qry = "select HL.HLookupID,HL.HLookupDesc,GL.GLookupID,GL.GLookupDesc,LC.LookupCategoryID,HL.IsActive from HLookup HL join GLookup GL on HL.GlookupID=GL.GLookupID join LookupCategories LC on LC.LookupCategoryID = GL.LookupCategoryID AND IsGLookup=0";
                        if (obj.LookupCategoryID > 0)
                            qry = qry + " where GL.LookupCategoryID=" + obj.LookupCategoryID;
                        cmd.CommandText = qry;
                        cmd.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        objLookup.HLookupList = ds.Tables[0].AsEnumerable().Select(row =>
                        new HLookupDataModel
                        {
                            LookupCategoryID = row.Field<decimal>("LookupCategoryID"),
                            HLookupID = row.Field<decimal>("HLookupID"),
                            GLookupID = row.Field<decimal>("GLookupID"),
                            GLookupDesc = row.Field<string>("GLookupDesc"),
                            HLookupDesc = row.Field<string>("HLookupDesc"),
                            isHLookExist = ReturnValue,
                            IsActive = row.Field<string>("IsActive")=="1"?true:false
                            
                        }).ToList();
                    }

                    if (obj.PageMode == GlobalEnum.MasterPages.HLookup && obj.ActionMode == GlobalEnum.Flag.StatusUpdate)
                    {
                        cmd = con.CreateCommand();
                        cmd.CommandText = "usp_HLookupUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HLookupID", obj.LookupCategoryID);
                        cmd.Parameters.AddWithValue("@HLookupDesc", obj.LookupCategoryDesc);
                        cmd.Parameters.AddWithValue("@UpdatedBy", obj.UserId);
                        cmd.Parameters.AddWithValue("@IsActive", obj.IsActive);
                        cmd.Parameters.AddWithValue("@FlexField1", "StatusUpdate");
                        cmd.ExecuteNonQuery();
                    }

                    //reader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return objLookup;

        }

       /// <summary>
       /// Insert General lookup category data
       /// </summary>
       /// <param name="objData"></param>
       /// <returns></returns>
       public LookupCategoryModel LookupInsertUpdate(LookupCategoryModel objData)
       {
           LookupCategoryModel objReturn = new LookupCategoryModel();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandType = CommandType.StoredProcedure;
                   if (objData.ActionMode == GlobalEnum.Flag.Insert)
                   {
                       cmd.CommandText = "usp_GLookupInsert";
                       cmd.Parameters.AddWithValue("@LookupCategoryID", objData.LookupCategoryID);
                       cmd.Parameters.AddWithValue("@GLookupDesc", objData.LookupCategoryDesc);
                       cmd.Parameters.AddWithValue("@CreatedBy", objData.UserId);
                       cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);
                       SqlParameter output = new SqlParameter("@Return_Value",SqlDbType.Int);
                       output.Direction = ParameterDirection.ReturnValue;
                       cmd.Parameters.Add(output);
                       int id = cmd.ExecuteNonQuery();
                       objReturn.PageMode = GlobalEnum.MasterPages.GLookup;
                       objReturn = GetLookupData(objReturn);
                   }
                   //if (objData.Mode == GlobalEnum.Flag.Update)
                   //{
                   //    cmd.CommandText = "usp_LocationsUpdate";
                   //    cmd.Parameters.AddWithValue("@LocationId", objData.LocationId);
                   //    cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);
                   //    cmd.ExecuteNonQuery();
                   //}

               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           return objReturn;
       }

       #endregion

       #region "Location"
       /// <summary>
       /// Get location data
       /// </summary>
       /// <returns></returns>
       public LocationModel GetLocationData(decimal LocId)
       {
           LocationModel objData = new LocationModel();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandText = "usp_LocationsSelect";
                   cmd.CommandType = CommandType.StoredProcedure;
                   if(LocId>0)
                    cmd.Parameters.AddWithValue("@LocationId", LocId);
                   else
                       cmd.Parameters.AddWithValue("@LocationId", DBNull.Value);
                   DataSet ds = new DataSet();
                   SqlDataAdapter da = new SqlDataAdapter(cmd);
                   da.Fill(ds);
                   List<LocationModel> lstData = new List<LocationModel>();
                   foreach (DataRow item in ds.Tables[0].Rows)
                   {
                       objData = new LocationModel();
                       objData.LocationId = Convert.ToInt32(item["LocationId"]);
                       objData.LocationCode = item["LocationCode"].ToString();
                       objData.LocationName = item["LocationName"].ToString();
                       objData.ListedInWeb = item["ListedInWeb"].ToString()== ""? false : Convert.ToBoolean(item["ListedInWeb"]);
                       objData.WorkingHrs = item["WorkingHrs"].ToString()== ""? 0 :Convert.ToDecimal(item["WorkingHrs"]);
                       objData.WorkFrom = item["WorkFrom"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(item["WorkFrom"].ToString());
                       objData.WorksTill = item["WorksTill"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(item["WorksTill"].ToString());
                       objData.Phone = item["Phone"].ToString();
                       objData.Fax = item["Fax"].ToString();
                       objData.Email = item["Email"].ToString();
                       objData.AddressLine1 = item["AddressLine1"].ToString();
                        objData.AddressLine2 = item["AddressLine2"].ToString();
                        objData.AddressLine3 = item["AddressLine3"].ToString();
                        objData.Designation = item["Designation"].ToString();
                        objData.City = item["City"].ToString();
                        objData.CountryId = Convert.ToInt32(item["CountryId"]);
                        objData.EmirateId = Convert.ToInt32(item["EmirateId"]);
                        objData.Country = item["Country"].ToString();
                        objData.Emirate = item["Emirate"].ToString();
                        objData.Zip = item["Zip"].ToString();
                       objData.LocationInChargeId= Convert.ToInt32(item["LocationInChargeId"]);
                       objData.LocationInCharge= item["LocationInCharge"].ToString();
                       objData.ReciptNoStart = Convert.ToInt32(item["ReciptNoStart"]);
                       objData.ReceiptNoCurrent = Convert.ToInt32(item["ReceiptNoCurrent"]);
                       objData.RentalAgreementNoStart = Convert.ToInt32(item["RentalAgreementNoStart"]);
                       objData.RentalAgreementNoCurrent = Convert.ToInt32(item["RentalAgreementNoCurrent"]);
                       objData.LeaseAgreementNoStart = Convert.ToInt32(item["LeaseAgreementNoStart"]);
                       objData.LeaseAgreementNoCurrent = Convert.ToInt32(item["LeaseAgreementNoCurrent"]);
                       objData.IsARevenue = item["IsARevenue"].ToString() == "" ? false : Convert.ToBoolean(item["IsARevenue"]);
                       objData.IsACounter = item["IsACounter"].ToString() == "" ? false : Convert.ToBoolean(item["IsACounter"]);
                       objData.IsAWorkShop = item["IsAWorkShop"].ToString() == "" ? false : Convert.ToBoolean(item["IsAWorkShop"]);
                       objData.IsAVirtual = item["IsAVirtual"].ToString() == "" ? false : Convert.ToBoolean(item["IsAVirtual"]);
                       objData.LeasingAllowed = item["LeasingAllowed"].ToString() == "" ? false : Convert.ToBoolean(item["LeasingAllowed"]);
                       objData.RentingAllowed = item["RentingAllowed"].ToString() == "" ? false : Convert.ToBoolean(item["RentingAllowed"]);
                       objData.BuId = Convert.ToInt32(item["BuId"]);
                       objData.UserId = Convert.ToInt32(item["CreatedBy"]);
                       objData.IsActive = Convert.ToBoolean(item["IsActive"]);
                       lstData.Add(objData);
                       objData.lstLocation = lstData;
                   }
                   //objGetData = ds.Tables[0].AsEnumerable().Select(row =>
                   //new LocationModel
                   //{
                   //    LocationId = row.Field<decimal>("LocationId"),
                   //    LocationCode = row.Field<string>("LocationCode"),
                   //    LocationName = row.Field<string>("LocationName")
                       //ListedInWeb = row.Field<bool>("ListedInWeb"),
                       //WorkingHrs = row.Field<int>("WorkingHrs"),
                       //WorkFrom = row.Field<DateTime>("WorkFrom"),
                       //WorksTill = row.Field<DateTime>("WorksTill"),
                       //Phone = row.Field<string>("Phone"),
                       //Fax = row.Field<string>("Fax"),
                       //Email = row.Field<string>("Email"),
                       //ReciptNoStart = row.Field<int>("ReciptNoStart"),
                       //ReceiptNoCurrent = row.Field<int>("ReceiptNoCurrent"),
                       //RentalAgreementNoStart = row.Field<int>("RentalAgreementNoStart"),
                       //RentalAgreementNoCurrent = row.Field<int>("RentalAgreementNoCurrent"),
                       //LeaseAgreementNoStart = row.Field<int>("LeaseAgreementNoStart"),
                       //LeaseAgreementNoCurrent = row.Field<int>("LeaseAgreementNoCurrent"),
                       //IsARevenue = row.Field<bool>("IsARevenue"),
                       //IsACounter = row.Field<bool>("IsACounter"),
                       //IsAWorkShop = row.Field<bool>("IsAWorkShop"),
                       //IsAVirtual = row.Field<bool>("IsAVirtual"),
                       //LeasingAllowed = row.Field<bool>("LeasingAllowed"),
                       //RentingAllowed = row.Field<bool>("RentingAllowed"),
                       //BuId = row.Field<int>("BuId"),
                       //UserId = row.Field<int>("CreatedBy"),
                       //IsActive = row.Field<bool>("IsActive")

                   //}).AsQueryable<LocationModel>();

                   //reader.Close();
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           return objData;
       }

       /// <summary>
       /// Insert location data
       /// </summary>
       /// <param name="objData"></param>
       /// <returns></returns>
       public LocationModel LocationInsertUpdate(LocationModel objData)
       {
           LocationModel objReturn = new LocationModel();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandType = CommandType.StoredProcedure;
                   int returnData = 0;
                   if (objData.ActionMode == GlobalEnum.Flag.Select)
                   {
                       objReturn = GetLocationData(objData.LocationId);
                   }
                   if (objData.ActionMode == GlobalEnum.Flag.Insert)
                   {
                       cmd.CommandText = "usp_LocationsInsert";
                       returnData = ExecuteLocationInsertUpdate(cmd, objData);
                       objReturn = GetLocationData(returnData);
                   }
                   if (objData.ActionMode == GlobalEnum.Flag.Update)
                   {
                       cmd.CommandText = "usp_LocationsUpdate";
                       returnData = ExecuteLocationInsertUpdate(cmd, objData);
                       objReturn = GetLocationData(returnData);
                   }
                   if (objData.ActionMode == GlobalEnum.Flag.StatusUpdate)
                   {
                       cmd.CommandText = "usp_LocationsStatusUpdate";
                       cmd.Parameters.AddWithValue("@LocationID", objData.LocationId);
                       cmd.Parameters.AddWithValue("@BuId", objData.BuId);
                       cmd.Parameters.AddWithValue("@UpdatedBy", objData.UserId);
                       cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);
                       cmd.ExecuteNonQuery();
                       objReturn = GetLocationData(objData.LocationId);
                   }
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           return objReturn;
       }

       private int ExecuteLocationInsertUpdate(SqlCommand cmd, LocationModel objData)
       {
           if (objData.ActionMode == GlobalEnum.Flag.Update)
           {
               cmd.Parameters.AddWithValue("@LocationID", objData.LocationId);
               cmd.Parameters.AddWithValue("@UpdatedBy", objData.UserId);
           }
           else
           { cmd.Parameters.AddWithValue("@CreatedBy", objData.UserId); }
           cmd.Parameters.AddWithValue("@LocationCode", objData.LocationCode);
           cmd.Parameters.AddWithValue("@LocationName", objData.LocationName);
           cmd.Parameters.AddWithValue("@ListedInWeb", objData.ListedInWeb);
           cmd.Parameters.AddWithValue("@WorkingHrs", objData.WorkingHrs);
           cmd.Parameters.AddWithValue("@WorkFrom", objData.WorkFrom);
           cmd.Parameters.AddWithValue("@WorksTill", objData.WorksTill);
           cmd.Parameters.AddWithValue("@Phone", objData.Phone);
           cmd.Parameters.AddWithValue("@Fax", objData.Fax);
           cmd.Parameters.AddWithValue("@Email", objData.Email);
           cmd.Parameters.AddWithValue("@AddressLine1", objData.AddressLine1);
           cmd.Parameters.AddWithValue("@AddressLine2", objData.AddressLine2);
           cmd.Parameters.AddWithValue("@AddressLine3", objData.AddressLine3);
           cmd.Parameters.AddWithValue("@Designation", objData.Designation);
           cmd.Parameters.AddWithValue("@City", objData.City);
           cmd.Parameters.AddWithValue("@CountryId", objData.CountryId);
           cmd.Parameters.AddWithValue("@EmirateId", objData.EmirateId);
           cmd.Parameters.AddWithValue("@Zip", objData.Zip);
           cmd.Parameters.AddWithValue("@LocationInChargeId", objData.LocationInChargeId);
           cmd.Parameters.AddWithValue("@ReciptNoStart", objData.ReciptNoStart);
           cmd.Parameters.AddWithValue("@ReceiptNoCurrent", objData.ReceiptNoCurrent);
           cmd.Parameters.AddWithValue("@RentalAgreementNoStart", objData.RentalAgreementNoStart);
           cmd.Parameters.AddWithValue("@RentalAgreementNoCurrent", objData.RentalAgreementNoCurrent);
           cmd.Parameters.AddWithValue("@LeaseAgreementNoStart", objData.LeaseAgreementNoStart);
           cmd.Parameters.AddWithValue("@LeaseAgreementNoCurrent", objData.LeaseAgreementNoCurrent);
           cmd.Parameters.AddWithValue("@IsARevenue", objData.IsARevenue);
           cmd.Parameters.AddWithValue("@IsACounter", objData.IsACounter);
           cmd.Parameters.AddWithValue("@IsAWorkShop", objData.IsAWorkShop);
           cmd.Parameters.AddWithValue("@IsAVirtual", objData.IsAVirtual);
           cmd.Parameters.AddWithValue("@LeasingAllowed", objData.LeasingAllowed);
           cmd.Parameters.AddWithValue("@RentingAllowed", objData.RentingAllowed);
           cmd.Parameters.AddWithValue("@BuId", objData.BuId);
           cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);

           int id = cmd.ExecuteNonQuery();
           return id;
       }
       #endregion

       #region "Company Setup"
       /// <summary>
       /// Getting the company details
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public CompanySetup GetCompanyDetails(int id)
       {
           string connectionString = Helper.Helper.GetConnectionString();
           CompanySetup  compData = new CompanySetup();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               SqlCommand command = con.CreateCommand();
               command.CommandText = "usp_OrganisationSelect";
               try
               {
                   LocationModel locData = GetLocationData(0);
                   if (locData.LocationId>0)
                   {
                       locData.lstLocation = (from item in locData.lstLocation where (item.IsActive == true) select item).ToList();
                   }
                   con.Open();
                   command.CommandType = CommandType.StoredProcedure;
                   if (id > 0)
                       command.Parameters.AddWithValue("@OrgId", id);
                   else
                       command.Parameters.AddWithValue("@OrgId", DBNull.Value);
                   DataSet ds = new DataSet();
                   SqlDataAdapter da = new SqlDataAdapter(command);
                   da.Fill(ds);
                   compData= ds.Tables[0].AsEnumerable().Select(row =>
                   new CompanySetup
                   {
                       OrgID = row.Field<decimal?>("OrgId"),
                        OrgCode = row.Field<string>("OrgCode"),
                        OrgName = row.Field<string>("OrgName"),
                        OrgLogoPath = row.Field<string>("OrgLogoPath"),
	                    OrgAddress1 = row.Field<string>("OrgAddress1"),
                        OrgAddress2 = row.Field<string>("OrgAddress2"),
                        OrgAddress3 = row.Field<string>("OrgAddress3"),
                        OrgCity = row.Field<string>("OrgCity"),
                        OrgCountryId = row.Field<decimal?>("OrgCountryId"),
                        OrgCountry = row.Field<string>("OrgCountry"),
                        OrgEmirate = row.Field<decimal?>("OrgEmirate"),
                        OrgEmirateName = row.Field<string>("OrgEmirateName"),
                        OrgPostBoxNo = row.Field<string>("OrgPostBoxNo"),
                        OrgPhoneNo = row.Field<string>("OrgPhoneNo"),
                        OrgFaxNo = row.Field<string>("OrgFaxNo"),
                        OrgEmailID = row.Field<string>("OrgEmailID"),
                        CMobileNo = row.Field<string>("CMobileNo"),
                        CPersonDesignation = row.Field<string>("CPersonDesignation"),
                        OrgZip = row.Field<string>("OrgZip"),
	                    CPersonName = row.Field<string>("CPersonName"),
                        CEMailID = row.Field<string>("CEMailID"),
                        BaseCurrencyId = row.Field<decimal?>("BaseCurrencyId"),
                        BaseCurrency = row.Field<string>("BaseCurrency"),
                        DateFormat = row.Field<string>("DateFormat"),
                        UserId = row.Field<string>("UserId"),
                        IsActive=row.Field<bool?>("IsActive"),
                        lstLocation = locData.LocationId>0?locData.lstLocation.OrderBy(i=>i.LocationName).ToList():new List<LocationModel>()
                   }).SingleOrDefault();
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
           return compData;
       }

       /// <summary>
       /// Inserting/Updating company data
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public int CompanyInsertUpdate(CompanySetup objData)
       {
           CompanySetup objReturn = new CompanySetup();
           int returnData = 0;
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.CommandText = "usp_OrganisationInsertUpdate";
                   if (objData.OrgID > 0)
                       cmd.Parameters.AddWithValue("@OrgId", objData.OrgID);
                   else
                       cmd.Parameters.AddWithValue("@OrgId", 0);

                   cmd.Parameters.AddWithValue("@OrgCode", objData.OrgCode);
                   cmd.Parameters.AddWithValue("@OrgName", objData.OrgName);
                    cmd.Parameters.AddWithValue("@OrgLogoPath", objData.OrgLogoPath);
	                cmd.Parameters.AddWithValue("@OrgAddress1", objData.OrgAddress1);
                    cmd.Parameters.AddWithValue("@OrgAddress2", objData.OrgAddress2);
                    cmd.Parameters.AddWithValue("@OrgAddress3", objData.OrgAddress3);
                    cmd.Parameters.AddWithValue("@OrgCity", objData.OrgCity);
                    cmd.Parameters.AddWithValue("@OrgCountryId", objData.OrgCountryId);
                    cmd.Parameters.AddWithValue("@OrgEmirate", objData.OrgEmirate);
                    cmd.Parameters.AddWithValue("@OrgPostBoxNo", objData.OrgPostBoxNo);
                    cmd.Parameters.AddWithValue("@OrgPhoneNo", objData.OrgPhoneNo);
                    cmd.Parameters.AddWithValue("@OrgFaxNo", objData.OrgFaxNo);
                    cmd.Parameters.AddWithValue("@OrgEmailID", objData.OrgEmailID);
                    cmd.Parameters.AddWithValue("@CMobileNo", objData.CMobileNo);
                    cmd.Parameters.AddWithValue("@CPersonDesignation", objData.CPersonDesignation);
                    cmd.Parameters.AddWithValue("@OrgZip", objData.OrgZip);
                    cmd.Parameters.AddWithValue("@CPersonName", objData.CPersonName);
                    cmd.Parameters.AddWithValue("@CEMailID", objData.CEMailID);
                    cmd.Parameters.AddWithValue("@BaseCurrencyId", objData.BaseCurrencyId);
                    cmd.Parameters.AddWithValue("@DateFormat", objData.DateFormat);
                    cmd.Parameters.AddWithValue("@UserId", objData.UserId);
                    cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);
                    cmd.ExecuteNonQuery();
                    returnData = 1;
               }
               catch (Exception ex)
               {
                   returnData = 0;
                   throw ex;
               }
           }

           return returnData;
       }

       /// <summary>
       /// Getting the company details
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       //public CompanySetup GetCompanyDetails(int id)
       //{
       //    string connectionString = Helper.Helper.GetConnectionString();
       //    CompanySetup compData = new CompanySetup();
       //    using (SqlConnection con = new SqlConnection(connectionString))
       //    {
       //        SqlCommand command = con.CreateCommand();
       //        command.CommandText = "usp_BusinessUnitsSelect";
       //        try
       //        {
       //            LocationModel locData = GetLocationData(0);
       //            if (locData.LocationId > 0)
       //            {
       //                locData.lstLocation = (from item in locData.lstLocation where (item.IsActive == true) select item).ToList();
       //            }
       //            con.Open();
       //            command.CommandType = CommandType.StoredProcedure;
       //            DataSet ds = new DataSet();
       //            SqlDataAdapter da = new SqlDataAdapter(command);
       //            da.Fill(ds);
       //            compData = ds.Tables[0].AsEnumerable().Select(row =>
       //            new CompanySetup
       //            {
       //                BuId = row.Field<decimal>("BuId"),
       //                OrgId = row.Field<decimal>("OrgId"),
       //                BuCode = row.Field<string>("BuCode"),
       //                BUName = row.Field<string>("BUName"),
       //                BuAddress1 = row.Field<string>("BuAddress1"),
       //                BuAddress2 = row.Field<string>("BuAddress2"),
       //                BuAddress3 = row.Field<string>("BuAddress3"),
       //                BuPostBox = row.Field<string>("BuPostBox"),
       //                BuPhoneNo = row.Field<string>("BuPhoneNo"),
       //                BuFax = row.Field<string>("BuFax"),
       //                BuEmailId = row.Field<string>("BuEmailId"),
       //                BuMobile = row.Field<string>("BuMobile"),
       //                BuZip = row.Field<string>("BuZip"),
       //                BuContactPerson = row.Field<string>("BuContactPerson"),
       //                BuBaseCurrency = row.Field<decimal>("BuBaseCurrency"),
       //                BuDecimals = row.Field<byte>("BuDecimals"),
       //                lstLocation = locData.LocationId > 0 ? locData.lstLocation.OrderBy(i => i.LocationName).ToList() : new List<LocationModel>()
       //            }).SingleOrDefault();
       //        }
       //        catch (Exception ex)
       //        {
       //            throw ex;
       //        }
       //    }
       //    return compData;
       //}

       //public int CompanyInsertUpdate(CompanySetup objData)
       //{
       //    CompanySetup objReturn = new CompanySetup();
       //    int returnData = 0;
       //    using (SqlConnection con = new SqlConnection(connectionString))
       //    {
       //        try
       //        {
       //            con.Open();
       //            SqlCommand cmd = con.CreateCommand();
       //            cmd.CommandType = CommandType.StoredProcedure;
       //            if (objData.BuId > 0)
       //            {
       //                cmd.CommandText = "usp_BusinessUnitsUpdate";
       //                cmd.Parameters.AddWithValue("@UpdatedBy", objData.UserId);
       //                cmd.Parameters.AddWithValue("@BuId", objData.BuId);
       //            }
       //            else
       //            {
       //                cmd.CommandText = "usp_BusinessUnitsInsert";
       //                cmd.Parameters.AddWithValue("@OrgId", objData.OrgId);
       //                cmd.Parameters.AddWithValue("@CreatedBy", objData.UserId);
       //            }
       //            cmd.Parameters.AddWithValue("@BuCode", objData.BuCode);
       //            cmd.Parameters.AddWithValue("@BUName", objData.BUName);
       //            cmd.Parameters.AddWithValue("@BuAddress1", objData.BuAddress1);
       //            cmd.Parameters.AddWithValue("@BuAddress2", objData.BuAddress2);
       //            cmd.Parameters.AddWithValue("@BuAddress3", objData.BuAddress3);
       //            cmd.Parameters.AddWithValue("@BuPostBox", objData.BuPostBox);
       //            cmd.Parameters.AddWithValue("@BuPhoneNo", objData.BuPhoneNo);
       //            cmd.Parameters.AddWithValue("@BuFax", objData.BuFax);
       //            cmd.Parameters.AddWithValue("@BuEmailId", objData.BuEmailId);
       //            cmd.Parameters.AddWithValue("@BuMobile", objData.BuMobile);
       //            cmd.Parameters.AddWithValue("@BuZip", objData.BuZip);
       //            cmd.Parameters.AddWithValue("@BuContactPerson", objData.BuContactPerson);
       //            cmd.Parameters.AddWithValue("@BuBaseCurrency", objData.BuBaseCurrency);
       //            cmd.Parameters.AddWithValue("@BuDecimals", objData.BuDecimals);
       //            cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);
       //            cmd.ExecuteNonQuery();
       //            returnData = 1;
       //        }
       //        catch (Exception ex)
       //        {
       //            returnData = 0;
       //            throw ex;
       //        }
       //    }

       //    return returnData;
       //}


       #endregion

       #region ChargeCodes

       /// <summary>
       /// Get all the charge codes
       /// </summary>
       /// <param name="ccID">Charge code id</param>
       /// <returns></returns>
       public List<ChargeCodeModel> GetChargeCodes(int id)
       {
           ChargeCodeModel objCC = new ChargeCodeModel();
           List<ChargeCodeModel> lstOfCCs = new List<ChargeCodeModel>();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               SqlCommand command = con.CreateCommand();
               command.CommandText = "usp_ChargeCodeMasterSelect";
               try
               {
                   con.Open();
                   if(id>0)
                        command.Parameters.AddWithValue("@ChargeCodeID", id);
                   else
                       command.Parameters.AddWithValue("@ChargeCodeID", DBNull.Value);

                   command.CommandType = CommandType.StoredProcedure;
                   DataSet ds = new DataSet();
                   SqlDataAdapter da = new SqlDataAdapter(command);
                   da.Fill(ds);
                   lstOfCCs = ds.Tables[0].AsEnumerable().Select(row =>
                   new ChargeCodeModel
                   {
                      BuId = row.Field<decimal>("BuId"),
                      ChargeCodeID = row.Field<decimal>("ChargeCodeID"),
                      ChargeCode = row.Field<string>("ChargeCode"),
                      ChargeCodeDesc = row.Field<string>("ChargeCodeDesc"),
                      GroupDriven = row.Field<bool>("GroupDriven"),
                      UnitDriven = row.Field<bool>("UnitDriven"),
                      AdhocValue = row.Field<bool>("AdhocValue"),
                      IsInsurance = row.Field<bool>("IsInsurance"),
                      IsRental = row.Field<bool>("IsRental"),
                      IsNonRental = row.Field<bool>("IsNonRental"),
                      IsTrafficViolation = row.Field<bool>("IsTrafficViolation"),
                      IsOtherCompliance = row.Field<bool>("IsOtherCompliance"),
                      IsVasWhileRenting = row.Field<bool>("IsVasWhileRenting"),
                      IsVasWhileClosing = row.Field<bool>("IsVasWhileClosing"),
                      IsOtherVas = row.Field<bool>("IsOtherVas"),
                      ServiceChargeApplicable = row.Field<bool>("ServiceChargeApplicable"),
                      ServiceChargeType = row.Field<string>("ServiceChargeType"),
                      ServiceCharge = row.Field<decimal>("ServiceCharge"),
                      IsDeductible = row.Field<bool>("IsDeductible"),
                      IsDeductibleWaiver = row.Field<bool>("IsDeductibleWaiver"),
                      //WaivingPercentage = row.Field<decimal>("WaivingPercentage"),
                      IsSecured = row.Field<bool>("IsSecured"),
                      CreatedOn = row.Field<DateTime>("CreatedOn"),
                      UserID =Convert.ToInt32(row.Field<string>("CreatedBy")),
                      IsActive = row.Field<bool>("IsActive"),
                   }).ToList();
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
           return lstOfCCs;
       }

       /// <summary>
       /// Insert/Update charge codes
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public List<ChargeCodeModel> ChargeCodesInsertUpdate(ChargeCodeModel objData)
       {
           List<ChargeCodeModel> lstObjReturn = new List<ChargeCodeModel>();
           bool ReturnValue=false;
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandType = CommandType.StoredProcedure;
                   if (objData.ActionMode == GlobalEnum.Flag.Insert)
                   {
                       cmd.CommandText = "usp_ChargeCodeMasterInsert";
                       cmd.Parameters.AddWithValue("@CreatedBy", objData.UserID);
                   }else{cmd.CommandText = "usp_ChargeCodeMasterUpdate";
                   cmd.Parameters.AddWithValue("@ChargeCodeID", objData.ChargeCodeID);
                   cmd.Parameters.AddWithValue("@UpdatedBy", objData.UserID);
                   }
                   cmd.Parameters.AddWithValue("@BuId", objData.BuId);
                   cmd.Parameters.AddWithValue("@ChargeCode", objData.ChargeCode);
                   cmd.Parameters.AddWithValue("@ChargeCodeDesc", objData.ChargeCodeDesc);
                   cmd.Parameters.AddWithValue("@GroupDriven", objData.GroupDriven);
                   cmd.Parameters.AddWithValue("@UnitDriven", objData.UnitDriven);
                   cmd.Parameters.AddWithValue("@AdhocValue", objData.AdhocValue);
                   cmd.Parameters.AddWithValue("@IsInsurance", objData.IsInsurance);
                   cmd.Parameters.AddWithValue("@IsRental", objData.IsRental);
                   cmd.Parameters.AddWithValue("@IsNonRental", objData.IsNonRental);
                   cmd.Parameters.AddWithValue("@IsTrafficViolation", objData.IsTrafficViolation);
                   cmd.Parameters.AddWithValue("@IsOtherCompliance", objData.IsOtherCompliance);
                   cmd.Parameters.AddWithValue("@IsVasWhileRenting", objData.IsVasWhileRenting);
                   cmd.Parameters.AddWithValue("@IsVasWhileClosing", objData.IsVasWhileClosing);
                   cmd.Parameters.AddWithValue("@IsOtherVas", objData.IsOtherVas);
                   cmd.Parameters.AddWithValue("@ServiceChargeApplicable", objData.ServiceChargeApplicable);
                   cmd.Parameters.AddWithValue("@ServiceChargeType", objData.ServiceChargeType);
                   cmd.Parameters.AddWithValue("@ServiceCharge", objData.ServiceCharge);
                   cmd.Parameters.AddWithValue("@IsDeductible", objData.IsDeductible);
                   cmd.Parameters.AddWithValue("@IsDeductibleWaiver", objData.IsDeductibleWaiver);
                   cmd.Parameters.AddWithValue("@WaivingPercentage", objData.WaivingPercentage);
                   cmd.Parameters.AddWithValue("@IsSecured", objData.IsSecured);
                   cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);

                   SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                   output.Direction = ParameterDirection.ReturnValue;
                   cmd.Parameters.Add(output);
                   cmd.ExecuteNonQuery();
                   ReturnValue = Convert.ToInt32(output.Value)>0?false:true;

                   lstObjReturn = GetChargeCodes(0);
                   lstObjReturn[0].isCCExist = ReturnValue;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }


           return lstObjReturn;
       }
       #endregion

       #region "Roles"

       /// <summary>
       /// Insert/Update Roles
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public List<RoleModel> RolesInsertUpdate(RoleModel objData)
       {
           List<RoleModel> lstObjReturn = new List<RoleModel>();
           bool ReturnValue=false;
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandType = CommandType.StoredProcedure;
                   if (objData.ActionMode == GlobalEnum.Flag.Insert)
                   {
                       cmd.CommandText = "usp_aspnet_RolesInsert";
                       cmd.Parameters.AddWithValue("@CreatedBy", objData.UserID);
                   }
                   else
                   {
                       cmd.CommandText = "usp_aspnet_RolesUpdate";
                       cmd.Parameters.AddWithValue("@RoleId", objData.RoleId);
                       cmd.Parameters.AddWithValue("@UserId", objData.UserID);
                       cmd.Parameters.AddWithValue("@IsActivated", objData.IsActivated);
                   }

                   cmd.Parameters.AddWithValue("@RoleName", objData.RoleName);
                   cmd.Parameters.AddWithValue("@LoweredRoleName", objData.LoweredRoleName);
                   cmd.Parameters.AddWithValue("@Description", objData.Description);
                   cmd.Parameters.AddWithValue("@IsActive", objData.IsActive);
                   if (objData.ActionMode == GlobalEnum.Flag.Insert)
                   {
                       SqlParameter output = new SqlParameter("@Return_Value", SqlDbType.Int);
                       output.Direction = ParameterDirection.ReturnValue;
                       cmd.Parameters.Add(output);
                       cmd.ExecuteNonQuery();
                       ReturnValue = Convert.ToInt32(output.Value) > 0 ? false : true;
                   }
                   else
                   { cmd.ExecuteNonQuery(); }

                   lstObjReturn = getAllRoles(Guid.Empty);
                   if (lstObjReturn.Count > 0)
                   { lstObjReturn[0].isRoleExist = ReturnValue; }
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }


           return lstObjReturn;
       }

       /// <summary>
       /// Get all the roles by role id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public List<RoleModel> getAllRoles(Guid id)
       {
           List<RoleModel> lstOfRoles = new List<RoleModel>();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               SqlCommand command = con.CreateCommand();
               command.CommandText = "usp_aspnet_RolesSelect";
               try
               {
                   con.Open();
                   if (id != Guid.Empty)
                       command.Parameters.AddWithValue("@RoleId", id);
                   else
                       command.Parameters.AddWithValue("@RoleId", DBNull.Value);

                   command.CommandType = CommandType.StoredProcedure;
                   DataSet ds = new DataSet();
                   SqlDataAdapter da = new SqlDataAdapter(command);
                   da.Fill(ds);
                   lstOfRoles = ds.Tables[0].AsEnumerable().Select(row =>
                   new RoleModel
                   {
                       RoleId = row.Field<Guid>("RoleId"),
                       ApplicationId = row.Field<Guid>("ApplicationId"),
                       RoleName = row.Field<string>("RoleName"),
                       LoweredRoleName = row.Field<string>("LoweredRoleName"),
                       Description = row.Field<string>("Description"),
                       CreatedOn = row.Field<DateTime?>("CreatedOn"),
                       IsActive = row.Field<bool>("IsActive"),
                       IsActivated = row.Field<bool?>("IsActivated")
                   }).ToList();
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
           return lstOfRoles;
       }

       #endregion

       #region Privilege

       /// <summary>
       /// Assign access to the role
       /// </summary>
       /// <param name="objData"></param>
       /// <returns></returns>
       public int PrivilegeInsertUpdate(PrivilegeModel objData, string formCollection)
       {
           List<PrivilegeModel> lstObjReturn = new List<PrivilegeModel>();
           int ReturnValue = 0;
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   var objfrmData = formCollection.Split('|');
                   for(int i=0;i<objfrmData.Length;i++)
                   {
                       var temp = objfrmData[i].Split('-');
                       if (temp.Length > 0)
                       {
                           SqlCommand cmd = con.CreateCommand();
                           cmd.CommandType = CommandType.StoredProcedure;
                           cmd.CommandText = "usp_RoleXPage_Insert_Update";
                           cmd.Parameters.AddWithValue("@RoleId", objData.RoleId);
                           cmd.Parameters.AddWithValue("@PageId", temp[0]);
                           cmd.Parameters.AddWithValue("@IsDelete", temp[1]);
                           cmd.ExecuteNonQuery();
                       }
                   }
                   ReturnValue = 1;
               }
               catch (Exception ex)
               {
                   ReturnValue = 0;
                   throw ex;
               }
           }

           return ReturnValue;
       }

       /// <summary>
       /// Get privileges to the role assigned
       /// </summary>
       /// <returns></returns>
       public List<PrivilegeModel> GetPrivileges(Guid roleId)
       {
           List<PrivilegeModel> lstOfPrivileges = new List<PrivilegeModel>();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               SqlCommand command = con.CreateCommand();
               command.CommandText = "usp_PrivilegesSelect";
               try
               {
                   con.Open();
                   //command.Parameters.AddWithValue("@RoleId", id);
                   command.CommandType = CommandType.StoredProcedure;
                   command.Parameters.AddWithValue("@RoleId", roleId);
                   DataSet ds = new DataSet();
                   SqlDataAdapter da = new SqlDataAdapter(command);
                   da.Fill(ds);
                   
                   foreach (var item in ds.Tables[0].AsEnumerable())
                   { 
                       PrivilegeModel temp = new PrivilegeModel();
                       temp.RoleId =  new Guid(item["ROLEID"].ToString());
                       temp.PageId = Convert.ToInt32(item["PAGEID"]);
                       temp.PageCode = item["PAGECODE"].ToString();
                       temp.PageDetails = item["PAGEDETAILS"].ToString();
                       temp.PageName = item["PAGENAME"].ToString();
                       temp.ModuleName = item["MODULENAME"].ToString();
                       temp.isActive = Convert.ToBoolean(item["ISACTIVE"]);
                       lstOfPrivileges.Add(temp);
                   }

                   //lstOfPrivileges = ds.Tables[0].AsEnumerable().Select(row =>
                   //new PrivilegeModel
                   //{
                   //    //RoleId = row.Field<Guid>("ROLEID"),
                   //    PageId = row.Field<int>("PAGEID"),
                   //    PageCode = row.Field<string>("PAGECODE"),
                   //    PageDetails = row.Field<string>("PAGEDETAILS"),
                   //    PageName = row.Field<string>("PAGENAME"),
                   //    ModuleName = row.Field<string>("MODULENAME"),
                   //    //isActive = row.Field<bool>("ISACTIVE"),
                   //}).ToList();
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }
           return lstOfPrivileges;
       }

       #endregion

       #region "Employee Master"

       /// <summary>
       /// Get Employee Master data
       /// </summary>
       /// <returns></returns>
       public List<EmployeeModel> GetEmployeeMasterData(decimal EmpId)
       {
           EmployeeModel objData = new EmployeeModel();
           List<EmployeeModel> lstData = new List<EmployeeModel>();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                   SqlCommand cmd = con.CreateCommand();
                   cmd.CommandText = "usp_EmployeeMasterSelect";
                   cmd.CommandType = CommandType.StoredProcedure;
                   if (EmpId > 0)
                       cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                   else
                       cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                   DataSet ds = new DataSet();
                   SqlDataAdapter da = new SqlDataAdapter(cmd);
                   da.Fill(ds);
                  
                   foreach (DataRow item in ds.Tables[0].Rows)
                   {
                       objData = new EmployeeModel();
                       objData.EmployeeId = Convert.ToInt32(item["EmployeeId"]);
                       objData.UserId = new Guid(item["UserId"].ToString());
                       objData.FirstName = item["FirstName"].ToString();
                       objData.LastName = item["LastName"].ToString();
                       objData.MiddleName = item["MiddleName"].ToString();
                       objData.EmployeeCode = item["EmployeeCode"].ToString();
                       objData.Gender = Convert.ToInt16(item["Gender"]);
                       objData.DOB = Convert.ToDateTime(item["DOB"]);
                       objData.DesignationId = Convert.ToInt32(item["DesignationId"]);
                       //objData.Designation = item["Designation"].ToString();
                       objData.BUId = Convert.ToInt32(item["BUId"]);
                       //objData.BU = item["BU"].ToString();
                       objData.LocationId = Convert.ToInt32(item["LocationId"]);
                       //objData.Location = item["Location"].ToString();
                       objData.DOJ = Convert.ToDateTime(item["DOJ"].ToString());
                       objData.LeavingDate = Convert.ToDateTime(item["LeavingDate"]);
                       objData.Address1 = item["Address1"].ToString();
                       objData.Address2 = item["Address2"].ToString();
                       objData.Address3 = item["Address3"].ToString();
                       objData.City = item["City"].ToString();
                       objData.State = item["State"].ToString();
                       objData.CountryName = item["CountryName"].ToString();
                       objData.CountryId = Convert.ToInt32(item["CountryId"]);
                       objData.EmergencyContactName = item["EmergencyContactName"].ToString();
                       objData.EmergencyContactPhone = item["EmergencyContactPhone"].ToString();
                       objData.IsActive = Convert.ToBoolean(item["IsActive"]);
                       lstData.Add(objData);
                   }
                   
                   //cmd = con.CreateCommand();
                   //cmd.CommandText = "usp_LocationsSelect";
                   //cmd.CommandType = CommandType.StoredProcedure;
                   //cmd.Parameters.AddWithValue("@LocationId", DBNull.Value);
                   //ds = new DataSet();
                   //da = new SqlDataAdapter(cmd);
                   //da.Fill(ds);
                   //LocationModel objLoc;
                   //foreach (var item in ds.Tables[0].AsEnumerable())
                   //{
                   //    objLoc = new LocationModel();
                   //    objLoc.LocationId = Convert.ToInt32(item["LocationId"]);
                   //    objLoc.LocationCode = item["LocationCode"].ToString();
                   //    objLoc.LocationName = item["LocationName"].ToString();
                   //    lstData[0].LocationList.Add(objLoc);
                   //}
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           return lstData;
       }

       /// <summary>
       /// Employee master data insertion/update
       /// </summary>
       /// <param name="objEmp"></param>
       /// <returns></returns>
       public List<EmployeeModel> EmployeeInsertUpdate(EmployeeModel objEmp)
       {
           List<EmployeeModel> ReturnValue = new List<EmployeeModel>();
           using (SqlConnection con = new SqlConnection(connectionString))
           {
               try
               {
                   con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_EmployeeMasterInsertUpdate";
                    cmd.Parameters.AddWithValue("@EmployeeId", objEmp.EmployeeId);
                    cmd.Parameters.AddWithValue("@UserId", objEmp.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", objEmp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", objEmp.LastName);
                    cmd.Parameters.AddWithValue("@MiddleName", objEmp.MiddleName);
                    cmd.Parameters.AddWithValue("@EmployeeCode", objEmp.EmployeeCode);
                    cmd.Parameters.AddWithValue("@Gender", objEmp.Gender);
                    cmd.Parameters.AddWithValue("@DOB", objEmp.DOB);
                    cmd.Parameters.AddWithValue("@DesignationId", objEmp.DesignationId);
                    cmd.Parameters.AddWithValue("@BUId", objEmp.BUId);
                    cmd.Parameters.AddWithValue("@LocationId", objEmp.LocationId);
                    cmd.Parameters.AddWithValue("@DOJ", objEmp.DOJ);
                    cmd.Parameters.AddWithValue("@LeavingDate", objEmp.LeavingDate);
                    cmd.Parameters.AddWithValue("@Address1", objEmp.Address1);
                    cmd.Parameters.AddWithValue("@Address2", objEmp.Address2);
                    cmd.Parameters.AddWithValue("@Address3", objEmp.Address3);
                    cmd.Parameters.AddWithValue("@City", objEmp.City);
                    cmd.Parameters.AddWithValue("@State", objEmp.State);
                    cmd.Parameters.AddWithValue("@CountryId", objEmp.CountryId);
                    cmd.Parameters.AddWithValue("@EmergencyContactName", objEmp.EmergencyContactName);
                    cmd.Parameters.AddWithValue("@EmergencyContactPhone", objEmp.EmergencyContactPhone);
                    cmd.Parameters.AddWithValue("@IsActive", objEmp.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedBy", objEmp.CreatedBy);
                    cmd.ExecuteNonQuery();
                    ReturnValue=GetEmployeeMasterData(0);
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           return ReturnValue;
       }
#endregion

   }
}
