using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebApiEx1Repository.Data.User;

namespace WebApiEx1Repository.Input
{
    public class UserInput 
    {
        public int? UserId { get; set; }

        public string? UserName { get; set; }

        public int? Age { get; set; }

        public eSex? Sex { get; set; }

        public int? CountryId { get; set; }
        public string? CityId { get; set; }
    }
}
