using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiEx1Repository.Data
{
    public  class User
    {
        public int UserId { get; set; }
        [DisplayName("使用者名稱")]
        public string? UserName { get; set; }
        public int? Age { get; set; }
        public eSex Sex { get; set; }
        public string? Phone { get; set; }
        public string? CityName { get; set; }

        public enum eSex
        {
            male = 1,
            female = 2

        }
    }

}
