using UPExciseLTE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace UPExciseLTE
{
    /// <summary>
    /// Summary description for MenuHandler
    /// </summary>
    public class MenuHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)  
        {  
            
            var list = new List<Menu>();  
            var dr = UserDtl.GetMenuData2(UserSession.LoggedInUser.UserId);
                while (dr.Read())  
                {  
                    var menu = new Menu  
                    {  
                        ID = Convert.ToInt32(dr["MenuID"]),  
                        Text = dr["Text"].ToString(),
                        parentId = dr["parentId"] != DBNull.Value ? Convert.ToInt32(dr["parentId"]) : (int?)null, 
                        //parentId = Convert.ToInt32(dr["orderby"]),// != DBNull.Value ? Convert.ToInt32(dr["parentId"]) : (int?)null,  
                        NavURL = dr["NavURL"].ToString(),
                        Icon = dr["Icon"].ToString(),
                        isActive = Convert.ToBoolean(dr["orderby"])  
                    };  
                    list.Add(menu);  
                }  
             
            var mainList = GetMenuTree(list, null);  
   
            var js = new JavaScriptSerializer();  
            context.Response.Write(js.Serialize(mainList));  
   
        }  
   
        private List<Menu> GetMenuTree(List<Menu> list, int? parent)  
        {  
            return list.Where(x => x.parentId == parent).Select(x => new Menu  
            {  
                ID = x.ID,  
                Text = x.Text,  
                parentId = x.parentId,  
                NavURL=x.NavURL,
                isActive = x.isActive,  
                Icon = x.Icon,
                List = GetMenuTree(list, x.ID)  
            }).ToList();  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        } 
      
    }
}