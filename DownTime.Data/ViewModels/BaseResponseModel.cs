using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace DownTime.Data.ViewModels
{
    public class BaseResponseModel<T>
    {
        public BaseResponseModel()
        {
            Errors = new List<string>();
        }

        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public int Total { get; set; }
        public T Data { get; set; }
    }
}
