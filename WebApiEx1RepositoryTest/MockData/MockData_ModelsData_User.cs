using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WebApiEx1Repository.Data.User;
using WebApiEx1Repository.Data;

namespace WebApiEx1RepositoryTest.MockData
{
    public static class MockData_ModelsData_User
    {
        public static User GetUser(string sUserId)
        {
            return GetUserAll().Where(x => x.UserId == int.Parse(sUserId)).SingleOrDefault();
        }

        public static List<User> GetUserAll()
        {
            return new List<User>()
            {
                new User()
                {
                    UserId=1,
                    UserName="sam",
                    Age=25,
                    Sex=eSex.male,
                    Phone="0935213231",
                    CityName="新北市",
                },
                new User()
                {
                    UserId=2,
                    UserName="ken",
                    Age=32,
                     Sex=eSex.male,
                    Phone="0977713231",
                    CityName="高雄市",
                },
                new User()
                {
                    UserId=6,
                    UserName="danny",
                    Age=66,
                    Sex=eSex.male,
                    Phone="0977993231",
                    CityName="台南市",
                },
                new User()
                {
                    UserId=7,
                    UserName="jokson",
                    Age=88,
                     Sex=eSex.male,
                    Phone="0973333231",
                    CityName="高雄市",
                },
                 new User()
                {
                    UserId=11,
                    UserName="amy",
                    Sex=eSex.female,
                    Age=36,
                    Phone="0999913231",
                    CityName="新北市",
                },
                new User()
                {
                    UserId=14,
                    UserName="dora",
                    Sex=eSex.female,
                    Age=35,
                     Phone="0977713231",
                    CityName="台北市",
                },
                new User()
                {
                    UserId=17,
                    UserName="tammy",
                    Sex=eSex.female,
                    Age=28,
                    Phone="0977713231",
                    CityName="高雄市",
                },
            };
        }

        public static User AddUser()
        {
            return new User()
            {
                UserId = 18,
                UserName = "ladygaga",
                Age = 55,
                Sex = eSex.female,
                Phone = "0977555231",
                CityName = "高雄市",
            };
        }

        public static User UpdateUserResult()
        {
            return new User()
            {
                UserId = 2,
                UserName = "ladygaga",
                Age = 55,
                Sex = eSex.female,
                Phone = "0977336631",
                CityName = "高雄市",
            };
        }


    }
}
