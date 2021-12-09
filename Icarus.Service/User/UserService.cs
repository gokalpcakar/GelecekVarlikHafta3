using Icarus.DB.Entities.DataContext;
using Icarus.Model;
using Icarus.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Service.User
{
    public class UserService : IUserService
    {
        public General<UserViewModel> Login(UserViewModel user)
        {
            using(var context = new IcarusContext())
            {

            }
        }
        public General<UserViewModel> GetUsers()
        {
            throw new NotImplementedException();
        }
        public General<UserViewModel> Insert(UserViewModel newUser)
        {
            throw new NotImplementedException();
        }
        public General<UserViewModel> Update(UserViewModel user)
        {
            throw new NotImplementedException();
        }
        public General<UserViewModel> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
