using System.Data;
using System.Data.SqlClient;
using TSR_Backend.Models.Tools;
using static TSR_Backend.Models.City;

namespace TSR_Backend.Models.DAO
{
    public class TSR_DAO
    {
        #region City
        public static List<CityGet> GetCities(int uid)
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
                                CityId = Convert.ToInt32(item["Base_Id"]),
                                CityName = item["Base_Name"].ToString(),
                                StateName = item["Base_Location"].ToString(),
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
                            if (bl.GetParamValue("@MessageOut").ToString() != null)
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
        #endregion

        #region Customer

        #endregion

        #region OperationType

        #endregion

        #region ServiceRequest

        #endregion

        #region State

        #endregion

        #region Status

        #endregion

        #region Stop

        #endregion

        #region User

        #endregion

        #region CustomerUser

        #endregion

        #region UserType

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
                    us.CustomerName = dtHeader.Rows[0]["customer_name"].ToString();
                    us.CustomerId = Convert.ToInt32(dtHeader.Rows[0]["customer_id"]);
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
    }
}
