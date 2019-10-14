using DownTime.CommonModel.Models;
using System.Collections.Generic;

namespace DownTime.Core.Helper
{
    public static class GetStatusList
    {
        public static List<StatusList> GetList()
        {
            return new List<StatusList>
            {
                new StatusList { Title = "Active", Value = true },
                new StatusList { Title = "Passive", Value = false }
            };
        }
         
    }
}
