using System;
using System.Collections.Generic;
using RockScissorPaper.DAL;
using System.Linq;
using System.Web;
using RockScissorPaper.Domain;

namespace RockScissorPaper.Models
{
    public class WebPlayerSessionRepository : IPlayerSessionRepository
    {
    
        public UserInfo GetCurrentUserInfo()
        {
 	        return (UserInfo)HttpContext.Current.Session["UserInfo"];
        }

        public void SetCurrentUserInfo(UserInfo item)
        {
            HttpContext.Current.Session["UserInfo"] = item;
        }
}
}