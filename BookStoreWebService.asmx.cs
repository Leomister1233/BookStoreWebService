using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace BookStoreWebService
{
    /// <summary>
    /// Summary description for BookStoreWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BookStoreWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<string> GetUserInfoByCondition(string username, string password, string phone, string gender, string email,string address)
        {
            string sqlstr = "";
            if (username != "")
                sqlstr += "username='" + username + "' and ";
            if (password != "")
                sqlstr += "password like'" + password + "%' and ";
            if (phone != "")
                sqlstr += "phone ='" + phone + "' and ";
            if (gender != "")
                sqlstr += "gender='" + gender + "' and ";
            if (email != "")
                sqlstr += "email='" + email + "' and ";
            if (address != "")
                sqlstr += "address='" + address + "' and ";
            if (sqlstr != "")
            {
                sqlstr = sqlstr.Substring(0, sqlstr.Length - 4);
                sqlstr = "select [username],[password] ,[phone] ,[gender],[email],[address] from Username where " + sqlstr;
            }
            else
                sqlstr = "select [username],[password] ,[phone] ,[gender],[email],[address] from  Username";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["username"].ToString());
                    list.Add(dt.Rows[i]["password"].ToString());
                    list.Add(dt.Rows[i]["phone"].ToString());
                    list.Add(dt.Rows[i]["gender"].ToString());
                    list.Add(dt.Rows[i]["email"].ToString());
                    list.Add(dt.Rows[i]["address"].ToString());
                }
                return list;
            }
            return null;
        }

        [WebMethod]
        public List<string> GetUserInfoByUsername(string username)
        {
            string sql = "select * from UserName where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                list.Add(dt.Rows[0]["username"].ToString());
                list.Add(dt.Rows[0]["password"].ToString());
                list.Add(dt.Rows[0]["phone"].ToString());
                list.Add(dt.Rows[0]["gender"].ToString());
                list.Add(dt.Rows[0]["email"].ToString());
                list.Add(dt.Rows[0]["address"].ToString());
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetUserInfoByIdPassWord(string username)
        {
            string sql = "select username ,password from UserName where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                list.Add(dt.Rows[0]["username"].ToString());
                list.Add(dt.Rows[0]["password"].ToString());
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetUserOrderInfoByUsername(string username)
        {
            string sql = "select username,phone,email,address from UserName where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                list.Add(dt.Rows[0]["username"].ToString());
                list.Add(dt.Rows[0]["phone"].ToString());
                list.Add(dt.Rows[0]["email"].ToString());
                list.Add(dt.Rows[0]["address"].ToString());
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetUserPassWordInfoReset(string username)
        {
            string sql = "select username ,email from UserName where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                list.Add(dt.Rows[0]["username"].ToString());
                list.Add(dt.Rows[0]["email"].ToString());
                return list;
            }
            return null;
        }

        [WebMethod]
        public List<string> GetAllUserInfo()
        {
            string sql = "select * from UserName ";
            DataTable dt = OperatorDb.GetDataTable(sql);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[0]["username"].ToString());
                    list.Add(dt.Rows[0]["password"].ToString());
                    list.Add(dt.Rows[0]["phone"].ToString());
                    list.Add(dt.Rows[0]["gender"].ToString());
                    list.Add(dt.Rows[0]["email"].ToString());
                    list.Add(dt.Rows[0]["address"].ToString());
                }
                return list;
            }
            return null;
        }

        [WebMethod]
        public bool InsertUserInfo(string username, string password, string phone, string gender, string email, string address)
        {
            string sqlstr = "INSERT INTO UserName  (username,password,phone,gender,email,address) VALUES ('" + username + "','" + password + "','" + phone + "','" + gender + "','" + email + "','" + address + "')";
            return OperatorDb.ExecCmd(sqlstr);
        }

        [WebMethod]
        public bool DeleteUserInfo(string username)
        {
            string sqlstr = "DELETE FROM UserName  WHERE username = '" + username + "'";

            return OperatorDb.ExecCmd(sqlstr);
        }

        [WebMethod]
        public bool UpdateUserInfoByUsername(string username, string password, string phone, string gender, string email, string address)
        {
            string sqlstr = "UPDATE   UserName  SET password='" + password + "',phone='" + phone + "',gender='" + gender + "',email='" + email + "', address='" + address + "' WHERE username='" + username + "'";
            return OperatorDb.ExecCmd(sqlstr);
        }

        [WebMethod]
        public bool UpdateUserPassword(string username, string password)
        {
            string sqlstr = "UPDATE   UserName  SET password='" + password + "' WHERE username='" + username + "'";
            return OperatorDb.ExecCmd(sqlstr);
        }
        [WebMethod]
        public bool InsertBookInfoById(string BookId, string BookName, string BookCategory,  string BookAuthor,string BookPrice,string BookSummary,string BookImage)
        {
            string sqlstr = "INSERT INTO ManagerInventory  (BookId,BookName,BookCategory,BookAuthor,BookPrice,BookSummary,BookImage) VALUES ('" + BookId + "','" + BookName + "','" + BookCategory + "','" + BookAuthor + "','" + BookPrice + "','" + BookSummary + "','"+ BookImage + "')";
            return OperatorDb.ExecCmd(sqlstr);
        }

        [WebMethod]
        public bool UpdateBookInfoById(string BookId, string BookName, string BookCategory, string BookAuthor,string BookPrice ,string BookSummary, string newBookImage)
        {
            string sql = "";
            if (newBookImage == "")
            {
                sql = "UPDATE   ManagerInventory  SET BookName='" + BookName + "',BookCategory='" + BookCategory + "',BookAuthor='" + BookAuthor + "', BookPrice='" + BookPrice + "', BookSummary='" + BookSummary+ "' WHERE BookId='" + BookId + "'";

            }
            else
            {
                sql = "UPDATE   ManagerInventory  SET BookName='" + BookName + "',BookCategory='" + BookCategory + "',BookAuthor='" + BookAuthor + "', BookPrice='" + BookPrice + "', BookSummary='" + BookSummary+"', BookImage='" + newBookImage + "' WHERE BookId='" + BookId + "'";
            }     
            return OperatorDb.ExecCmd(sql);
        }
        [WebMethod]
        public bool UpdateQuantityByBookId(string username ,string BookId,string BookQuantity)
        {
            string sql;
            sql="Update ShoppingCart Set BookQuantity='"+BookQuantity+"'Where username='"+username+"'"+"And BookId='"+BookId+"'";
            return OperatorDb.ExecCmd(sql);
        }
        [WebMethod]
        public List<string> GetBookInfoById(string BookId) {
            string sqlstr = "select * from ManagerInventory where BookId='" + BookId + "'";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookId"].ToString());
                    list.Add(dt.Rows[i]["BookName"].ToString());
                    list.Add(dt.Rows[i]["BookCategory"].ToString());
                    list.Add(dt.Rows[i]["BookAuthor"].ToString());
                    list.Add(dt.Rows[i]["BookPrice"].ToString());
                    list.Add(dt.Rows[i]["BookSummary"].ToString());
                    list.Add(dt.Rows[i]["BookImage"].ToString());
                }
                return list;
            }
            return null;
        }

        [WebMethod]
        public bool DeleteBookInfoById(string BookId)
        {
            string sql = "DELETE FROM  [dbo].[ManagerInventory]  WHERE BookId='" + BookId + "'";

            return OperatorDb.ExecCmd(sql);
        }

        [WebMethod]
        public List<string> GetBookInfoByCondition(string BookId, string BookName, string BookCategory,string BookAuthor ,string BookPrice,string BookSummary )
        {
            string sqlstr = "";
            if (BookId != "")
                sqlstr += "BookId='" + BookId + "' and ";
            if (BookName != "")
                sqlstr += "BookName like'" + BookName + "%' and ";
            if (BookCategory != "")
                sqlstr += "BookCategory ='" + BookCategory + "' and ";
            if (BookAuthor != "")
                sqlstr += "BookAuthor='" + BookAuthor + "' and ";
            if (BookPrice != "")
                sqlstr += "BookPrice='" + BookPrice + "' and ";
            if (BookSummary != "")
                sqlstr += "BookSummary='" + BookSummary + "' and ";
            if (sqlstr != "")
            {
                sqlstr = sqlstr.Substring(0, sqlstr.Length - 4);
                sqlstr = "select [BookId],[BookName] ,[BookCategory] ,[BookAuthor],[BookPrice],[BookSummary] from ManagerInventory where " + sqlstr;
            }
            else
                sqlstr = "select [BookId],[BookName] ,[BookCategory] ,[BookAuthor],[BookPrice],[BookSummary] from  ManagerInventory";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookId"].ToString());
                    list.Add(dt.Rows[i]["BookName"].ToString());
                    list.Add(dt.Rows[i]["BookCategory"].ToString());
                    list.Add(dt.Rows[i]["BookAuthor"].ToString());
                    list.Add(dt.Rows[i]["BookPrice"].ToString());
                    list.Add(dt.Rows[i]["BookSummary"].ToString());
                }
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetBookInfoByConditionClient(string BookId, string BookName, string BookCategory, string BookAuthor, string BookPrice)
        {
            string sqlstr = "";
            if (BookId != "")
                sqlstr += "BookId='" + BookId + "' and ";
            if (BookName != "")
                sqlstr += "BookName like'" + BookName + "%' and ";
            if (BookCategory != "")
                sqlstr += "BookCategory ='" + BookCategory + "' and ";
            if (BookAuthor != "")
                sqlstr += "BookAuthor='" + BookAuthor + "' and ";
            if (BookPrice != "")
                sqlstr += "BookPrice='" + BookPrice + "' and ";
            if (sqlstr != "")
            {
                sqlstr = sqlstr.Substring(0, sqlstr.Length - 4);
                sqlstr = "select [BookId],[BookName] ,[BookCategory] ,[BookAuthor],[BookPrice] from ManagerInventory where " + sqlstr;
            }
            else
                sqlstr = "select [BookId],[BookName] ,[BookCategory] ,[BookAuthor],[BookPrice] from  ManagerInventory";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookId"].ToString());
                    list.Add(dt.Rows[i]["BookName"].ToString());
                    list.Add(dt.Rows[i]["BookCategory"].ToString());
                    list.Add(dt.Rows[i]["BookAuthor"].ToString());
                    list.Add(dt.Rows[i]["BookPrice"].ToString());
                }
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetBookPhotoStringByStuNo(string BookId)
        {
            string sql = "select [BookImage] from ManagerInventory where BookId='" + BookId + "'";
            DataTable dt = OperatorDb.GetDataTable(sql);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookImage"].ToString());
                }
                return list;
            }
            return null;
        }

        [WebMethod]
        public bool InsertBookCartInfoById(string username,string BookId, string BookName, string BookCategory, string BookAuthor, string BookPrice,string BookQuantity)
        {
            string sqlstr = "INSERT INTO [ShoppingCart]  (username,BookId,BookName,BookCategory,BookAuthor,BookPrice,BookQuantity) VALUES ('" + username + "','" + BookId + "','" + BookName + "','" + BookCategory + "','" + BookAuthor + "','" + BookPrice + "','" + BookQuantity + "')";
            return OperatorDb.ExecCmd(sqlstr);
        }

        [WebMethod]
        public bool DeleteBookCartInfoById(string username)
        {
            string sql = "DELETE FROM  [dbo].[ShoppingCart]  WHERE username='" + username + "'";

            return OperatorDb.ExecCmd(sql);
        }
        [WebMethod]
        public bool DeleteBookCartItemById(string BookId)
        {
            string sql = "DELETE FROM  [dbo].[ShoppingCart]  WHERE BookId='" + BookId + "'";

            return OperatorDb.ExecCmd(sql);
        }

        [WebMethod]
        public List<string> GetBookCartInfoById(string username)
        {
            string sqlstr = "select * from ShoppingCart where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookId"].ToString());
                    list.Add(dt.Rows[i]["BookName"].ToString());
                    list.Add(dt.Rows[i]["BookCategory"].ToString());
                    list.Add(dt.Rows[i]["BookAuthor"].ToString());
                    list.Add(dt.Rows[i]["BookPrice"].ToString());
                    list.Add(dt.Rows[i]["BookQuantity"].ToString());
                }
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetBookPriceById(string username)
        {
            string sqlstr = "select * from ShoppingCart where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookPrice"].ToString());
                }
                return list;
            }
            return null;
        }
        [WebMethod]
        public List<string> GetBookQuantityById(string username)
        {
            string sqlstr = "select * from ShoppingCart where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["BookQuantity"].ToString());
                }
                return list;
            }
            return null;
        }
        [WebMethod]
        public bool InsertPhotoInfoByUsername(string username,string photo)
        {
            string sqlstr = "INSERT INTO ProfileFoto (username,photo) VALUES ('" + username + "','" + photo + "')";
            return OperatorDb.ExecCmd(sqlstr);
        }

        [WebMethod]
        public List<string> GetProfilePhotobyUsername(string username)
        {
            string sqlstr = "select * from ProfileFoto where username='" + username + "'";
            DataTable dt = OperatorDb.GetDataTable(sqlstr);
            List<string> list = new List<string>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["photo"].ToString());
                }
                return list;
            }
            return null;
        }
        [WebMethod]
        public bool DeleteProfilePhotoByUsername(string username)
        {
            string sql = "DELETE FROM  [dbo].[ProfileFoto]  WHERE username='" + username + "'";

            return OperatorDb.ExecCmd(sql);
        }
        [WebMethod]
        public bool UpdateProfilePhotoByUsername(string username, string photo)
        {
            string sqlstr = "UPDATE   [ProfileFoto]  SET photo='" + photo +"' WHERE username='" + username + "'";
            return OperatorDb.ExecCmd(sqlstr);
        }
    }
}
