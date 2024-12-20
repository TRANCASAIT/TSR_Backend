using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Data.SqlClient;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.City;
using static TSR_Backend.Models.Comment;
using static TSR_Backend.Models.Customer;
using static TSR_Backend.Models.CustomerUser;
using static TSR_Backend.Models.Document;
using static TSR_Backend.Models.OperationType;
using static TSR_Backend.Models.RecoverPassword;
using static TSR_Backend.Models.ServiceRequest;
using static TSR_Backend.Models.State;
using static TSR_Backend.Models.Status;
using static TSR_Backend.Models.User;
using static TSR_Backend.Models.UserType;

namespace TSR_Backend.Models.DAO
{
    public class TSR_DAO
    {
        #region City
        public static List<CityGet> GetCities(int uid, string dtkn)
        {
            int option = 3;
            List<CityGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@User_Logged", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[cities_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CityGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CityGet()
                            {
                                CityId = Convert.ToInt32(item["City_Id"]),
                                StateId = Convert.ToInt32(item["State_Id"]),
                                CityName = item["City_Name"].ToString()!,
                                StateName = item["State_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CityGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CityGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CityGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CityGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<CityGet>
                    {
                        new CityGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static List<CityGet> GetCitiesPerState(int uid, int stateId, string dtkn)
        {
            int option = 4;
            List<CityGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@User_Logged", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@State_Id", stateId)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[cities_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CityGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CityGet()
                            {
                                CityId = Convert.ToInt32(item["City_Id"]),
                                StateId = Convert.ToInt32(item["State_Id"]),
                                CityName = item["City_Name"].ToString()!,
                                StateName = item["State_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CityGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CityGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CityGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CityGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<CityGet>
                    {
                        new CityGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateCity(City _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@City_Name", _obj.CityName!)
                       .AddParam("@State_Id", _obj.StateId!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[cities_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateCity(CityUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@City_Name", _obj.CityName!)
                       .AddParam("@City_Id", _obj.CityId)
                       .AddParam("@State_Id", _obj.StateId)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[cities_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region Customer
        public static List<CustomerGet> GetCustomers(int uid, string dtkn)
        {
            int option = 3;
            List<CustomerGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[companies_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CustomerGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CustomerGet()
                            {
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                CityId = Convert.ToInt32(item["City_Id"]),
                                StateId = Convert.ToInt32(item["State_Id"]),
                                Name = item["Name"].ToString()!,
                                RFC = item["RFC"].ToString()!,
                                Street = item["Street"].ToString()!,
                                ExtNumber = item["Street_Ext_Number"].ToString()!,
                                IntNumber = item["Street_Int_Number"].ToString()!,
                                ZipCode = item["ZipCode"].ToString()!,
                                Suburb = item["Suburb"].ToString()!,
                                Phone = item["PhoneNumber"].ToString()!,
                                Email = item["Email"].ToString()!,
                                Status = Convert.ToBoolean(item["Status"]),
                                CityName = item["City_Name"].ToString()!,
                                StateName = item["State_Name"].ToString()!,
                                Address = item["Address"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CustomerGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CustomerGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CustomerGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CustomerGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<CustomerGet>
                    {
                        new CustomerGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateCustomer(Customer _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@Name", _obj.Name!)
                       .AddParam("@RFC", _obj.RFC!)
                       .AddParam("@Street", _obj.Street!)
                       .AddParam("@StreetExt", _obj.ExtNumber!)
                       .AddParam("@StreetInt", _obj.IntNumber!)
                       .AddParam("@ZipCode", _obj.ZipCode!)
                       .AddParam("@Suburb", _obj.Suburb!)
                       .AddParam("@City_Id", _obj.CityId!)
                       .AddParam("@State_Id", _obj.StateId!)
                       .AddParam("@PhoneNumber", _obj.Phone!)
                       .AddParam("@Email", _obj.Email!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[companies_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateCustomer(CustomerUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                       AddParam("@option", option)
                      .AddParam("@Customer_Id", _obj.CustomerId!)
                      .AddParam("@Name", _obj.Name!)
                      .AddParam("@RFC", _obj.RFC!)
                      .AddParam("@Street", _obj.Street!)
                      .AddParam("@StreetExt", _obj.ExtNumber!)
                      .AddParam("@StreetInt", _obj.IntNumber!)
                      .AddParam("@ZipCode", _obj.ZipCode!)
                      .AddParam("@Suburb", _obj.Suburb!)
                      .AddParam("@City_Id", _obj.CityId!)
                      .AddParam("@State_Id", _obj.StateId!)
                      .AddParam("@PhoneNumber", _obj.Phone!)
                      .AddParam("@Email", _obj.Email!)
                      .AddParam("@User_logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[companies_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateCustomerStatus(CustomerStatus _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                       AddParam("@option", option)
                      .AddParam("@Customer_Id", _obj.CustomerId!)
                      .AddParam("@Status", _obj.Status!)
                      .AddParam("@User_logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[companies_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static List<CustomerGet> GetCustomerForCustomers(int uid, string dtkn)
        {
            int option = 5;
            List<CustomerGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[companies_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CustomerGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CustomerGet()
                            {
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                CityId = Convert.ToInt32(item["City_Id"]),
                                StateId = Convert.ToInt32(item["State_Id"]),
                                Name = item["Name"].ToString()!,
                                RFC = item["RFC"].ToString()!,
                                Street = item["Street"].ToString()!,
                                ExtNumber = item["Street_Ext_Number"].ToString()!,
                                IntNumber = item["Street_Int_Number"].ToString()!,
                                ZipCode = item["ZipCode"].ToString()!,
                                Suburb = item["Suburb"].ToString()!,
                                Phone = item["PhoneNumber"].ToString()!,
                                Email = item["Email"].ToString()!,
                                Status = Convert.ToBoolean(item["Status"]),
                                CityName = item["City_Name"].ToString()!,
                                StateName = item["State_Name"].ToString()!,
                                Address = item["Address"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CustomerGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CustomerGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CustomerGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CustomerGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<CustomerGet>
                    {
                        new CustomerGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static List<CustomerGet> GetCustomerForAdm(int uid, string dtkn)
        {
            int option = 6;
            List<CustomerGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[companies_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CustomerGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CustomerGet()
                            {
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                CityId = Convert.ToInt32(item["City_Id"]),
                                StateId = Convert.ToInt32(item["State_Id"]),
                                Name = item["Name"].ToString()!,
                                RFC = item["RFC"].ToString()!,
                                Street = item["Street"].ToString()!,
                                ExtNumber = item["Street_Ext_Number"].ToString()!,
                                IntNumber = item["Street_Int_Number"].ToString()!,
                                ZipCode = item["ZipCode"].ToString()!,
                                Suburb = item["Suburb"].ToString()!,
                                Phone = item["PhoneNumber"].ToString()!,
                                Email = item["Email"].ToString()!,
                                Status = Convert.ToBoolean(item["Status"]),
                                CityName = item["City_Name"].ToString()!,
                                StateName = item["State_Name"].ToString()!,
                                Address = item["Address"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CustomerGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CustomerGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CustomerGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CustomerGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<CustomerGet>
                    {
                        new CustomerGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }
        #endregion

        #region OperationType
        public static List<OperationTypeGet> GetOperationTypes(int uid, string dtkn)
        {
            int option = 3;
            List<OperationTypeGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[operation_types_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<OperationTypeGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new OperationTypeGet()
                            {
                                OperationTypeId = Convert.ToInt32(item["OperationType_Id"]),
                                OperationTypeName = item["OperationType_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<OperationTypeGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new OperationTypeGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new OperationTypeGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new OperationTypeGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<OperationTypeGet>
                    {
                        new OperationTypeGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateOperationType(OperationType _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@OperationType_Name", _obj.OperationTypeName!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[operation_types_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateOperationType(OperationTypeUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@OperationType_Name", _obj.OperationTypeName)
                       .AddParam("@OperationType_Id", _obj.OperationTypeId)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[operation_types_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region ServiceRequest
        public static List<ServiceGet> GetServices(int uid, string dtkn)
        {
            int option = 1;
            List<ServiceGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_adm]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<ServiceGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new ServiceGet()
                            {
                                ServiceRequestId = Convert.ToInt32(item["ServiceRequest_Id"]),
                                Priority = Convert.ToBoolean(item["Priority"]),
                                InvoiceNumber = item["InvoiceNumber"].ToString()!,
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                CustomerName = item["CustomerName"].ToString()!,
                                BoxNumber = item["Box_Number"].ToString()!,
                                OperationTypeId = Convert.ToInt32(item["OperationType_Id"]),
                                StatusId = Convert.ToInt32(item["Status_Id"]),
                                StopsId = Convert.ToInt32(item["Stops_Id"]),
                                OperationTypeName = item["OperationType_Name"].ToString()!,
                                StopNumber = item["Stop_Number"].ToString()!,
                                StatusDescription = item["Status_Description"].ToString()!,
                                TmwOrder = item["TMWOrder"].ToString()!,
                                Reference = item["Reference"].ToString()!,
                                Uuid = item["uuid"].ToString()!,
                                InvMxStatus = Convert.ToBoolean(item["InvoiceMXStatus"]),
                                InvUSAStatus = Convert.ToBoolean(item["InvoiceUSAStatus"]),
                                BolStatus = Convert.ToBoolean(item["BOLStatus"]),
                                InwardStatus = Convert.ToBoolean(item["InwardStatus"]),
                                AceStatus = Convert.ToBoolean(item["ACEStatus"]),
                                LayoutStatus = Convert.ToBoolean(item["LayoutStatus"]),
                                TmwTime = item["tmw_time"].ToString()!,
                                CcpTime = item["ccp_time"].ToString()!,
                                XmlStatus = Convert.ToBoolean(item["XmlStatus"]),
                                PdfOriginalStatus = Convert.ToBoolean(item["OriginPdfStatus"]),
                                PdfOperationsStatus = Convert.ToBoolean(item["OPStatus"]),
                                CreatedAt = item["Creation_Date"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<ServiceGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new ServiceGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new ServiceGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new ServiceGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<ServiceGet>
                    {
                        new ServiceGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static List<Chart> GetServicesCount(int uid, string dtkn)
        {
            int option = 7;
            List<Chart> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_adm]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<Chart>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new Chart()
                            {
                                ServiceCount = Convert.ToInt32(item["service_count"]),
                                CustomerName = item["customer_name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<Chart>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new Chart()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new Chart()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new Chart()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<Chart>
                    {
                        new Chart()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result RemoveService(RemoveService _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateBox(UpdateBox _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@box_number", _obj.BoxNumber!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateReference(UpdateReference _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 3;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@reference", _obj.Reference!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateOperation(UpdateOperation _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@operation_type_id", _obj.OperationTypeId!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdatePriority(UpdatePriority _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 3;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@priority", _obj.Priority!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_adm]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateTmw(UpdateTmw _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@tmw", _obj.TmwOrder!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_adm]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static List<ServiceGet> GetServicesFiltered(Search _obj, int uid, string dtkn)
        {
            int option = 2;
            List<ServiceGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@customer_id", _obj.CustomerId!)
                        .AddParam("@status_id", _obj.StatusId!)
                        .AddParam("@operation_type_id", _obj.OperationTypeId!)
                        .AddParam("@box_number", _obj.BoxNumber!)
                        .AddParam("@invoice_number", _obj.InvoiceNumber!)
                        .AddParam("@start_date", _obj.Start!)
                        .AddParam("@end_date", _obj.End!)
                        .AddParam("@priority", _obj.Priority!)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_adm]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<ServiceGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new ServiceGet()
                            {
                                ServiceRequestId = Convert.ToInt32(item["ServiceRequest_Id"]),
                                Priority = Convert.ToBoolean(item["Priority"]),
                                InvoiceNumber = item["InvoiceNumber"].ToString()!,
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                CustomerName = item["CustomerName"].ToString()!,
                                BoxNumber = item["Box_Number"].ToString()!,
                                OperationTypeId = Convert.ToInt32(item["OperationType_Id"]),
                                StatusId = Convert.ToInt32(item["Status_Id"]),
                                StopsId = Convert.ToInt32(item["Stops_Id"]),
                                OperationTypeName = item["OperationType_Name"].ToString()!,
                                StopNumber = item["Stop_Number"].ToString()!,
                                StatusDescription = item["Status_Description"].ToString()!,
                                TmwOrder = item["TMWOrder"].ToString()!,
                                Uuid = item["uuid"].ToString()!,
                                Reference = item["Reference"].ToString()!,
                                InvMxStatus = Convert.ToBoolean(item["InvoiceMXStatus"]),
                                InvUSAStatus = Convert.ToBoolean(item["InvoiceUSAStatus"]),
                                BolStatus = Convert.ToBoolean(item["BOLStatus"]),
                                InwardStatus = Convert.ToBoolean(item["InwardStatus"]),
                                AceStatus = Convert.ToBoolean(item["ACEStatus"]),
                                LayoutStatus = Convert.ToBoolean(item["LayoutStatus"]),
                                XmlStatus = Convert.ToBoolean(item["XmlStatus"]),
                                PdfOriginalStatus = Convert.ToBoolean(item["OriginPdfStatus"]),
                                PdfOperationsStatus = Convert.ToBoolean(item["OPStatus"]),
                                CreatedAt = item["Creation_Date"].ToString()!,
                                TmwTime = item["tmw_time"].ToString()!,
                                CcpTime = item["ccp_time"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<ServiceGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new ServiceGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new ServiceGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new ServiceGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<ServiceGet>
                    {
                        new ServiceGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateRequest(Request _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 5;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@customer_id", _obj.CustomerId!)
                       .AddParam("@box_number", _obj.BoxNumber!)
                       .AddParam("@reference", _obj.Reference!)
                       .AddParam("@stop_id", _obj.StopId!)
                       .AddParam("@operation_type_id", _obj.OperationTypeId)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }


        public static Result RemoveFileAdmin(RemoveFile sr, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 6;
            using (var bl = new Business())
            {
                try
                {
                    bl
                      .AddParam("@option", option)
                      .AddParam("@service_request_id", sr.ServiceRequestId)
                      .AddParam("@document_id", sr.DocumentId)
                      .AddParam("@document_type", sr.DocumentType)
                      .AddParam("@User_Logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureQuery(Business.DBConn.ServidorLocal, "[Request].[documents_admin]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString();
                    }

                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        #endregion

        #region ServiceRequestReport

        public static List<ServiceRequestReport> GetServiceReports(int uid, string dtkn)
        {
            int option = 1;
            List<ServiceRequestReport> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_reports_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<ServiceRequestReport>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new ServiceRequestReport()
                            {
                                ServiceRequestId = Convert.ToInt32(item["InvoiceNumber"]),
                                DocumentId = Convert.ToInt32(item["Document_Id"]),
                                CustomerName = item["CustomerName"].ToString()!,
                                BoxNumber = item["Box_Number"].ToString()!,
                                OperationTypeName = item["OperationType_Name"].ToString()!,
                                Reference = item["Reference"].ToString()!,
                                StopNumber = Convert.ToInt32(item["Stop_Number"]),
                                CreatedAt = item["Creation_Date"].ToString()!,
                                
                                TmwOrder = item["TMWOrder"].ToString()!,
                                StatusId = Convert.ToInt32(item["Status_Id"]),
                                StatusDescription = item["Status_Description"].ToString()!,
                                Inward = item["Inward"].ToString()!,
                                Ace = item["ACE"].ToString()!,
                                Layout = item["Layout"].ToString()!,
                                AcceptedBy = item["Accepted_By"].ToString()!,
                                LayoutAccepteddtm = item["LayoutAccepteddtm"].ToString()!,
                                ConsignmentNote = item["Consignment_Note"].ToString()!,
                                Xml = item["XML"].ToString()!,
                                OriginalPdf = item["OriginalPDF"].ToString()!,
                                OperationsPdf = item["OperationsPDF"].ToString()!,
                                Uuid = item["uuid"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<ServiceRequestReport>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new ServiceRequestReport()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new ServiceRequestReport()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new ServiceRequestReport()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<ServiceRequestReport>
                    {
                        new ServiceRequestReport()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static List<ServiceRequestReport> GetServiceReportsFiltered(SearchReport _obj, int uid, string dtkn)
        {
            int option = 2;
            List<ServiceRequestReport> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@customer_id", _obj.CustomerId!)
                        .AddParam("@status_id", _obj.StatusId!)
                        .AddParam("@operation_type_id", _obj.OperationTypeId!)
                        .AddParam("@box_number", _obj.BoxNumber!)
                        .AddParam("@invoice_number", _obj.InvoiceNumber!)
                        .AddParam("@start_date", _obj.Start!)
                        .AddParam("@end_date", _obj.End!)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_reports_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<ServiceRequestReport>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new ServiceRequestReport()
                            {
                                ServiceRequestId = Convert.ToInt32(item["InvoiceNumber"]),
                                DocumentId = Convert.ToInt32(item["Document_Id"]),
                                CustomerName = item["CustomerName"].ToString()!,
                                BoxNumber = item["Box_Number"].ToString()!,
                                OperationTypeName = item["OperationType_Name"].ToString()!,
                                Reference = item["Reference"].ToString()!,
                                StopNumber = Convert.ToInt32(item["Stop_Number"]),
                                CreatedAt = item["Creation_Date"].ToString()!,
                                TmwOrder = item["TMWOrder"].ToString()!,
                                StatusId = Convert.ToInt32(item["Status_Id"]),
                                StatusDescription = item["Status_Description"].ToString()!,
                                Inward = item["Inward"].ToString()!,
                                Ace = item["ACE"].ToString()!,
                                Layout = item["Layout"].ToString()!,
                                AcceptedBy = item["Accepted_By"].ToString()!,
                                LayoutAccepteddtm = item["LayoutAccepteddtm"].ToString()!,
                                ConsignmentNote = item["Consignment_Note"].ToString()!,
                                Xml = item["XML"].ToString()!,
                                OriginalPdf = item["OriginalPDF"].ToString()!,
                                OperationsPdf = item["OperationsPDF"].ToString()!,
                                Uuid = item["uuid"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<ServiceRequestReport>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new ServiceRequestReport()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new ServiceRequestReport()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new ServiceRequestReport()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<ServiceRequestReport>
                    {
                        new ServiceRequestReport()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }
        #endregion

        #region Documents-Customer
        public static List<Document> GetDocuments(int uid, int serviceId, string dtkn)
        {
            int option = 1;
            List<Document> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@service_request_id", serviceId)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[documents_customer]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<Document>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new Document()
                            {
                                ServiceRequestId = Convert.ToInt32(item["ServiceRequest_Id"]),
                                DocumentId = Convert.ToInt32(item["Document_Id"]),
                                StopId = Convert.ToInt32(item["Stop_Id"]),
                                StopNumber = Convert.ToInt32(item["Stop_Number"]),
                                StatusId = Convert.ToInt32(item["Status_Id"]),
                                StatusDescription = item["Status_Description"].ToString()!,
                                InvoiceMXStatus = Convert.ToBoolean(item["InvoiceMXStatus"]),
                                InvoiceMXCompleted = Convert.ToBoolean(item["InvoiceMXCompleted"]),
                                InvoiceMX = item["InvoiceMX"].ToString()!,
                                InvMXFN = item["InvMXFN"].ToString()!,
                                InvMXdtm = item["InvMXdtm"].ToString()!,
                                InvoiceUSAStatus = Convert.ToBoolean(item["InvoiceUSAStatus"]),
                                InvoiceUSACompleted = Convert.ToBoolean(item["InvoiceUSACompleted"]),
                                InvoiceUSA = item["InvoiceUSA"].ToString()!,
                                InvUSAFN = item["InvUSAFN"].ToString()!,
                                InvUSAdtm = item["InvUSAdtm"].ToString()!,
                                BolCompleted = Convert.ToBoolean(item["BolCompleted"]),
                                BOLStatus = Convert.ToBoolean(item["BOLStatus"]),
                                BOL = item["BOL"].ToString()!,
                                BolFN = item["BolFN"].ToString()!,
                                Boldtm = item["Boldtm"].ToString()!,
                                InwardCompleted = Convert.ToBoolean(item["InwardCompleted"]),
                                InwardStatus = Convert.ToBoolean(item["InwardStatus"]),
                                Inward = item["Inward"].ToString()!,
                                InwFN = item["InwFN"].ToString()!,
                                Inwdtm = item["Inwdtm"].ToString()!,
                                AceCompleted = Convert.ToBoolean(item["AceCompleted"]),
                                ACEStatus = Convert.ToBoolean(item["ACEStatus"]),
                                ACE = item["ACE"].ToString()!,
                                AceFN = item["AceFN"].ToString()!,
                                Acedtm = item["Acedtm"].ToString()!,
                                LayoutCompleted = Convert.ToBoolean(item["LayoutCompleted"]),
                                LayoutStatus = Convert.ToBoolean(item["LayoutStatus"]),
                                Layout = item["Layout"].ToString()!,
                                LayoutFN = item["LayoutFN"].ToString()!,
                                Layoutdtm = item["Layoutdtm"].ToString()!,
                                Accepted_Layout = Convert.ToBoolean(item["Accepted_Layout"]),
                                XmlCompleted = Convert.ToBoolean(item["XmlCompleted"]),
                                XmlStatus = Convert.ToBoolean(item["XmlStatus"]),
                                XML = item["XML"].ToString()!,
                                XmlFN = item["XmlFN"].ToString()!,
                                Xmldtm = item["Xmldtm"].ToString()!,
                                OriginalPDFCompleted = Convert.ToBoolean(item["OriginalPDFCompleted"]),
                                OriginPdfStatus = Convert.ToBoolean(item["OriginPdfStatus"]),
                                OriginalPDF = item["OriginalPDF"].ToString()!,
                                OPdfFN = item["OPdfFN"].ToString()!,
                                OPdfdtm = item["OPdfdtm"].ToString()!,
                                OperationsPDFCompleted = Convert.ToBoolean(item["OperationsPDFCompleted"]),
                                OPStatus = Convert.ToBoolean(item["OPStatus"]),
                                OperationsPDF = item["OperationsPDF"].ToString()!,
                                OpPdfFN = item["OpPdfFN"].ToString()!,
                                OpPdfdtm = item["OpPdfdtm"].ToString()!,
                                ConsignmentNote = item["Consignment_Note"].ToString()!,
                                AcceptedLayoutDC = Convert.ToBoolean(item["Accepted_LayoutDC"]),
                                NotAcceptedLayoutDC = Convert.ToBoolean(item["NotAccepted_LayoutDC"]),
                            });
                        }
                    }
                    else
                    {
                        obj = new List<Document>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new Document()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new Document()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new Document()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<Document>
                    {
                        new Document()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result UploadDocumentCustomer(UploadFile _obj, int uid, string path, string filename, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@document_id", _obj.DocumentId!)
                       .AddParam("@service_request_id", _obj.ServiceRequestId)
                       .AddParam("@document_type", _obj.DocumentType)
                       .AddParam("@file_name", filename)
                       .AddParam("@path", path)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[documents_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result RollBackDocumentCustomer(UploadFile _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 3;
            using (var bl = new Business())
            {
                try
                {
                    bl
                        .AddParam("@option", option)
                        .AddParam("@service_request_id", _obj.ServiceRequestId)
                        .AddParam("@document_id", _obj.DocumentId)
                        .AddParam("@document_type", _obj.DocumentType)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureQuery(Business.DBConn.ServidorLocal, "[Request].[documents_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result RemoveFileCustomer(RemoveFile sr, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl
                      .AddParam("@option", option)
                      .AddParam("@service_request_id", sr.ServiceRequestId)
                      .AddParam("@document_id", sr.DocumentId)
                      .AddParam("@document_type", sr.DocumentType)
                      .AddParam("@User_Logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureQuery(Business.DBConn.ServidorLocal, "[Request].[documents_customer]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString();
                    }

                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        #endregion

        #region Documents-Admin
        public static Result RollBackDocumentAdmin(UploadFile _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl
                        .AddParam("@option", option)
                        .AddParam("@service_request_id", _obj.ServiceRequestId)
                        .AddParam("@document_id", _obj.DocumentId)
                        .AddParam("@document_type", _obj.DocumentType)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureQuery(Business.DBConn.ServidorLocal, "[Request].[documents_admin]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateConsigmentNote(UpdateConsignmentNote _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@document_id", _obj.DocumentId!)
                       .AddParam("@consignment_note", _obj.ConsignmentNote)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[documents_admin]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateUuid(UpdateUuid _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 5;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@uuid", _obj.Uuid)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[sr_procedures_adm]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UploadDocumentAdmin(UploadFile _obj, int uid, string path, string filename, string dtkn)
        {
            Result result = new Result();
            int option = 3;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@document_id", _obj.DocumentId!)
                       .AddParam("@service_request_id", _obj.ServiceRequestId)
                       .AddParam("@document_type", _obj.DocumentType)
                       .AddParam("@file_name", filename)
                       .AddParam("@path", path)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[documents_admin]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        //accept or reject layout
        public static Result UpdateLayoutStatus(UpdateLayoutStatus _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 5;
            using (var bl = new Business())
            {
                try
                {
                    bl
                       .AddParam("@option", option)
                       .AddParam("@document_id", _obj.DocumentId!)
                       .AddParam("@service_request_id", _obj.ServiceRequestId)
                       .AddParam("@accepted_layout", _obj.AcceptedLayout)
                       .AddParam("@not_accepted_layout", _obj.NotAcceptedLayout)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[documents_admin]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }


        public static Result ReturnToFileUpload(ChangeRequestStatus _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl
                        .AddParam("@option", option)
                        .AddParam("@service_request_id", _obj.ServiceRequestId)
                        .AddParam("@reason_id", _obj.ReasonId)
                        .AddParam("@other_reason", _obj.OtherReason)
                        .AddParam("@reason_input", _obj.Reason)
                        .AddParam("@user_id", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureQuery(Business.DBConn.ServidorLocal, "[Request].[decomplete_requests]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region Comments
        public static List<CommentGet> GetComments(int uid, int docId, string dtkn)
        {
            int option = 2;
            List<CommentGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@document_id", docId)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[comments_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CommentGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CommentGet()
                            {
                                ServiceRequestId = Convert.ToInt32(item["ServiceRequest_Id"]),
                                DocumentId = Convert.ToInt32(item["Document_Id"]),
                                CommentId = Convert.ToInt32(item["Comment_Id"]),
                                IsCustomer = Convert.ToBoolean(item["is_customer"]),
                                CommentBody = item["Comment_Body"].ToString()!,
                                Username = item["UserName"].ToString()!,
                                CreatedAt = item["Creation_Date"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CommentGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CommentGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CommentGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CommentGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<CommentGet>
                    {
                        new CommentGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result SaveComment(Comment _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@service_request_id", _obj.ServiceRequestId!)
                       .AddParam("@document_id", _obj.DocumentId!)
                       .AddParam("@comment_body", _obj.CommentBody!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[comments_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region State
        public static List<StateGet> GetStates(int uid, string dtkn)
        {
            int option = 3;
            List<StateGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@User_Logged", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[states_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<StateGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new StateGet()
                            {
                                StateId = Convert.ToInt32(item["State_Id"]),
                                StateName = item["State_Name"].ToString(),
                            });
                        }
                    }
                    else
                    {
                        obj = new List<StateGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new StateGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString() != null)
                            {
                                obj.Add(new StateGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new StateGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<StateGet>
                    {
                        new StateGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateState(State _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@State_Name", _obj.StateName!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[states_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateState(StateUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@State_Name", _obj.StateName!)
                       .AddParam("@State_Id", _obj.StateId!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[states_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        #endregion

        #region Status
        public static List<StatusGet> GetStatus(int uid, string dtkn)
        {
            int option = 3;
            List<StatusGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[status_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<StatusGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new StatusGet()
                            {
                                StatusId = Convert.ToInt32(item["Status_Id"]),
                                StatusName = item["Status_Description"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<StatusGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new StatusGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new StatusGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new StatusGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<StatusGet>
                    {
                        new StatusGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateStatus(Status _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@Customer_Id", _obj.StatusName!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[status_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateStatus(StatusUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@Status_Description", _obj.StatusName)
                       .AddParam("@Status_Id", _obj.StatusId)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[status_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region Stop
        public static List<Stop> GetStops(int uid, string dtkn)
        {
            int option = 3;
            List<Stop> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[stops_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<Stop>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new Stop()
                            {
                                StopId = Convert.ToInt32(item["Stop_Id"]),
                                StopNumber = Convert.ToInt32(item["Stop_Number"]),
                            });
                        }
                    }
                    else
                    {
                        obj = new List<Stop>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new Stop()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new Stop()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new Stop()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<Stop>
                    {
                        new Stop()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }
        #endregion

        #region User
        public static List<UserGet> GetUsers(int uid, string dtkn)
        {
            int option = 3;
            List<UserGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[users_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<UserGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new UserGet()
                            {
                                UserId = Convert.ToInt32(item["User_Id"]),
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                UserTypeId = Convert.ToInt32(item["UserType_Id"]),
                                Username = item["UserName"].ToString()!,
                                GivenName = item["Name"].ToString()!,
                                Surname = item["Last_Name"].ToString()!,
                                Email = item["Email"].ToString()!,
                                Status = Convert.ToBoolean(item["Status"]),
                                CustomerName = item["CustomerName"].ToString()!,
                                UserTypeName = item["UserType_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<UserGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new UserGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new UserGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new UserGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<UserGet>
                    {
                        new UserGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateUser(User _obj, int uid, string dtkn)
        {
            _obj.Password = Helpers.GetSHA256(_obj.Password!);
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@UserName", _obj.Username)
                       .AddParam("@Name", _obj.GivenName)
                       .AddParam("@Last_Name ", _obj.Surname)
                       .AddParam("@Email", _obj.Email)
                       .AddParam("@Password", _obj.Password!)
                       .AddParam("@UserType_Id", _obj.UserTypeId)
                       .AddParam("@Customer_Id", _obj.CustomerId)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[users_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateUser(UserUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl
                      .AddParam("@option", option)
                      .AddParam("@UserName", _obj.Username!)
                      .AddParam("@Name", _obj.GivenName!)
                      .AddParam("@Last_Name", _obj.Surname!)
                      .AddParam("@Email", _obj.Email!)
                      .AddParam("@UserType_Id", _obj.UserTypeId!)
                      .AddParam("@Customer_Id", _obj.CustomerId!)
                      .AddParam("@User_Id", _obj.UserId!)
                      .AddParam("@User_logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[users_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateUserStatus(UserUpdateStatus _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                       AddParam("@option", option)
                      .AddParam("@User_Id", _obj.UserId!)
                      .AddParam("@Status", _obj.Status!)
                      .AddParam("@User_logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[users_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region CustomerUser
        public static List<CustomerUserGet> GetCustomerUsers(int uid, string dtkn)
        {
            int option = 3;
            List<CustomerUserGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                        .AddParam("@option", option)
                        .AddParam("@User_Logged", uid)
                        .AddParam("@dynamic_token", dtkn)
                        .AddParam("@StatusOut", DBNull.Value, true, 100)
                        .AddParam("@MessageOut", DBNull.Value, true, 300)
                        .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[customer_users_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<CustomerUserGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new CustomerUserGet()
                            {
                                CustomerUserId = Convert.ToInt32(item["User_Id"]),
                                CustomerId = Convert.ToInt32(item["Customer_Id"]),
                                UserTypeId = Convert.ToInt32(item["UserType_Id"]),
                                Username = item["UserName"].ToString()!,
                                GivenName = item["Name"].ToString()!,
                                Surname = item["Last_Name"].ToString()!,
                                Email = item["Email"].ToString()!,
                                Status = Convert.ToBoolean(item["Status"]),
                                CustomerName = item["CustomerName"].ToString()!,
                                UserTypeName = item["UserType_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<CustomerUserGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new CustomerUserGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new CustomerUserGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new CustomerUserGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    obj = new List<CustomerUserGet>
                    {
                        new CustomerUserGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static Result CreateCustomerUser(CustomerUser _obj, int uid, string dtkn)
        {
            _obj.Password = Helpers.GetSHA256(_obj.Password!);
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@UserName", _obj.Username!)
                       .AddParam("@Name", _obj.GivenName!)
                       .AddParam("@Last_Name ", _obj.Surname!)
                       .AddParam("@Email", _obj.Email!)
                       .AddParam("@Password", _obj.Password!)
                       .AddParam("@UserType_Id", _obj.UserTypeId!)
                       .AddParam("@Customer_Id", _obj.CustomerId!)
                       .AddParam("@User_logged", uid)
                       .AddParam("@dynamic_token", dtkn)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[customer_users_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateCustomerUser(CustomerUserUpdate _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl
                      .AddParam("@option", option)
                      .AddParam("@UserName", _obj.Username!)
                      .AddParam("@Name", _obj.GivenName!)
                      .AddParam("@Last_Name", _obj.Surname!)
                      .AddParam("@Email", _obj.Email!)
                      .AddParam("@UserType_Id", _obj.UserTypeId!)
                      .AddParam("@Customer_Id", _obj.CustomerId!)
                      .AddParam("@User_Id", _obj.CustomerUserId!)
                      .AddParam("@User_logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[customer_users_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result UpdateCustomerUserStatus(CustomerUserUpdateStatus _obj, int uid, string dtkn)
        {
            Result result = new Result();
            int option = 4;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                       AddParam("@option", option)
                      .AddParam("@User_Id", _obj.CustomerUserId!)
                      .AddParam("@Status", _obj.Status!)
                      .AddParam("@User_logged", uid)
                      .AddParam("@dynamic_token", dtkn)
                      .AddParam("@StatusOut", DBNull.Value, true, 100)
                      .AddParam("@MessageOut", DBNull.Value, true, 300)
                      .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[customer_users_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region UserType
        public static List<UserTypeGet> GetUserTypes(int uid, string dtkn)
        {
            int option = 3;
            List<UserTypeGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@User_Logged", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Requests].[user_types_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<UserTypeGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new UserTypeGet()
                            {
                                UserTypeId = Convert.ToInt32(item["UserType_Id"]),
                                UserTypeName = item["UserType_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<UserTypeGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new UserTypeGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString() != null)
                            {
                                obj.Add(new UserTypeGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new UserTypeGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<UserTypeGet>
                    {
                        new UserTypeGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }

        public static List<UserTypeGet> GetUserTypesDrops(int uid, string dtkn)
        {
            int option = 4;
            List<UserTypeGet> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@User_Logged", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Requests].[user_types_procedures]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<UserTypeGet>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new UserTypeGet()
                            {
                                UserTypeId = Convert.ToInt32(item["UserType_Id"]),
                                UserTypeName = item["UserType_Name"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<UserTypeGet>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new UserTypeGet()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString() != null)
                            {
                                obj.Add(new UserTypeGet()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new UserTypeGet()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<UserTypeGet>
                    {
                        new UserTypeGet()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }
        #endregion

        #region Login
        public static UserModelLogin AuthenticateUser(LoginUser userLogin)
        {
            userLogin.Password = Helpers.GetSHA256(userLogin.Password!);
            Result result = new Result();
            UserModelLogin us = new UserModelLogin();
            using (var bl = new Business())
            {
                DataTable dtHeader = bl
                                   .AddParam("@username", userLogin.UserName!)
                                   .AddParam("@password", userLogin.Password!)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[session_procedures]");

                if (dtHeader.Rows.Count > 0)
                {
                    us.Uid = Convert.ToInt32(dtHeader.Rows[0]["user_id"]);
                    us.Role = dtHeader.Rows[0]["role_description"].ToString();
                    us.FirstName = dtHeader.Rows[0]["name"].ToString();
                    us.LastName = dtHeader.Rows[0]["surname"].ToString();
                    us.Username = dtHeader.Rows[0]["username"].ToString();
                    us.DynamicToken = dtHeader.Rows[0]["Dynamic_Token"].ToString();
                    us.CustomerName = dtHeader.Rows[0]["customer_name"].ToString();
                    us.CustomerId = Convert.ToInt32(dtHeader.Rows[0]["customer_id"]);
                    us.IsCustomer = Convert.ToBoolean(dtHeader.Rows[0]["is_customer"]);
                    us.StartDate = dtHeader.Rows[0]["start_date"].ToString()!;
                    us.EndDate = dtHeader.Rows[0]["end_date"].ToString()!;

                }
                else
                {
                    us.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                    us.Message = bl.GetParamValue("@MessageOut").ToString()!;
                }
            }
            return us;
        }
        #endregion

        #region LogOut
        public static Result LogOut(int uid)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@User_Logged", uid)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[log_out_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region recover-password
        /*check if email exists on DB*/
        public static Result CheckEmail(string email)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@email", email)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[recover_password_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result CheckCode(RecoverPassword _obj)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@email", _obj.Email!)
                       .AddParam("@code", _obj.Code!)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[recover_password_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result Reset_Password(ResetPassword _obj)
        {
            //encript password is pending
            _obj.Password = Helpers.GetSHA256(_obj.Password!);
            Result result = new Result();
            int option = 3;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@email", _obj.Email!)
                       .AddParam("@code", _obj.Code!)
                       .AddParam("@password", _obj.Password!)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[recover_password_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        #endregion

        #region reset-session
        public static Result CheckEmailSession(string email)
        {
            Result result = new Result();
            int option = 1;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@email", email)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[reset_session_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }

        public static Result CheckCodeSession(ResetSession _obj)
        {
            Result result = new Result();
            int option = 2;
            using (var bl = new Business())
            {
                try
                {
                    bl.
                        AddParam("@option", option)
                       .AddParam("@email", _obj.Email!)
                       .AddParam("@code", _obj.Code!)
                       .AddParam("@StatusOut", DBNull.Value, true, 100)
                       .AddParam("@MessageOut", DBNull.Value, true, 300)
                       .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[reset_session_procedures]");

                    if (bl.Exception != null)
                    {
                        result.State = 1;
                        result.Message = bl.Exception;
                    }
                    else
                    {
                        result.State = Convert.ToInt32(bl.GetParamValue("@StatusOut"));
                        result.Message = bl.GetParamValue("@MessageOut").ToString()!;
                    }
                }
                catch (SqlException ex)
                {
                    result.State = ex.State;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
        #endregion

        #region decomplete reason
        public static List<DecompleteReason> GetReasons(int uid, string dtkn)
        {
            int option = 1;
            List<DecompleteReason> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@user_id", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[decomplete_requests]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<DecompleteReason>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new DecompleteReason()
                            {
                                ReasonId = Convert.ToInt32(item["reason_id"]),
                                Reason = item["reason"].ToString()!,
                            });
                        }
                    }
                    else
                    {
                        obj = new List<DecompleteReason>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new DecompleteReason()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new DecompleteReason()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new DecompleteReason()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<DecompleteReason>
                    {
                        new DecompleteReason()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }


        public static List<DecompletedRequests> GetDecompletedRequests(int uid, string dtkn)
        {
            int option = 3;
            List<DecompletedRequests> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@user_id", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[decomplete_requests]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<DecompletedRequests>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new DecompletedRequests()
                            {
                                ServiceRequestId = Convert.ToInt32(item["ServiceRequest_Id"]),
                                Status = item["Status_Description"].ToString(),
                            });
                        }
                    }
                    else
                    {
                        obj = new List<DecompletedRequests>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new DecompletedRequests()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new DecompletedRequests()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new DecompletedRequests()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<DecompletedRequests>
                    {
                        new DecompletedRequests()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }



        public static List<DecompletedRequestsFullContext> GetDecompletedRequestsFullContext(int uid, string dtkn, int service)
        {
            int option = 4;
            List<DecompletedRequestsFullContext> obj = null!;
            using (var bl = new Business())
            {
                try
                {
                    DataTable dt = bl
                                   .AddParam("@option", option)
                                   .AddParam("@service_request_id", service)
                                   .AddParam("@user_id", uid)
                                   .AddParam("@dynamic_token", dtkn)
                                   .AddParam("@StatusOut", DBNull.Value, true, 100)
                                   .AddParam("@MessageOut", DBNull.Value, true, 300)
                                   .ProcedureDataTable(Business.DBConn.ServidorLocal, "[Request].[decomplete_requests]");
                    if (dt.Rows.Count > 0)
                    {
                        obj = new List<DecompletedRequestsFullContext>();
                        foreach (DataRow item in dt.Rows)
                        {
                            obj.Add(new DecompletedRequestsFullContext()
                            {
                                ServiceRequestId = Convert.ToInt32(item["ServiceRequest_Id"]),
                                Reason = item["reason"].ToString(),
                                LastModifiedBy = item["last_modified_by"].ToString(),
                                ModifiedAt = item["modified_at"].ToString(),
                            });
                        }
                    }
                    else
                    {
                        obj = new List<DecompletedRequestsFullContext>();
                        if (bl.Exception != null)
                        {
                            obj.Add(new DecompletedRequestsFullContext()
                            {
                                State = 1,
                                Message = bl.Exception
                            });
                        }
                        else
                        {
                            if (bl.GetParamValue("@MessageOut").ToString()!.Length > 0)
                            {
                                obj.Add(new DecompletedRequestsFullContext()
                                {
                                    State = Convert.ToInt32(bl.GetParamValue("@StatusOut")),
                                    Message = bl.GetParamValue("@MessageOut").ToString()!
                                });
                            }
                            else
                            {
                                obj.Add(new DecompletedRequestsFullContext()
                                {
                                    State = 1,
                                    Message = "Not records were found"
                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    obj = new List<DecompletedRequestsFullContext>
                    {
                        new DecompletedRequestsFullContext()
                        {
                            State = ex.State,
                            Message = ex.Message
                        }
                    };
                }
            }
            return obj;
        }
        #endregion
    }
}
