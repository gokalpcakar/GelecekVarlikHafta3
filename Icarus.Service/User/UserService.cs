using AutoMapper;
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
        private readonly IMapper mapper;
        public UserService(IMapper _mapper)
        {
            mapper = _mapper;
        }
        public General<UserViewModel> Login(UserViewModel user)
        {
            var result = new General<UserViewModel>();
            var model = mapper.Map<Icarus.DB.Entities.User>(user);

            using (var context = new IcarusContext())
            {
                result.Entity = mapper.Map<UserViewModel>(model);
                result.IsSuccess = context.User.Any(
                    x => x.Id == user.Id && x.IsActive && !x.IsDeleted &&
                    x.Name == user.Name && x.Surname == user.Surname &&
                    x.UserName == user.UserName && x.Password == user.Password);
            }

            return result;
        }
        public General<UserViewModel> GetUsers()
        {
            var result = new General<UserViewModel>();

            using (var context = new IcarusContext())
            {
                var data = context.User
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .OrderBy(x => x.Id);
                result.List = mapper.Map<List<UserViewModel>>(data);
                result.IsSuccess = true;
            }

            return result;
        }
        public General<UserViewModel> Insert(UserViewModel newUser)
        {
            var result = new General<UserViewModel>();
            var model = mapper.Map<Icarus.DB.Entities.User>(newUser);

            using (var context = new IcarusContext())
            {
                model.Idate = DateTime.Now;
                context.User.Add(model);
                context.SaveChanges();

                result.Entity = mapper.Map<UserViewModel>(model);
                result.IsSuccess = true;
            }

            return result;
        }
        public General<UserViewModel> Update(UserViewModel user)
        {
            var result = new General<UserViewModel>();

            using (var context = new IcarusContext())
            {
                var updateUser = context.User.SingleOrDefault(i => i.Id == user.Id);

                updateUser.Name = user.Name;
                updateUser.Surname = user.Surname;
                updateUser.UserName = user.UserName;
                updateUser.Email = user.Email;
                updateUser.Password = user.Password;

                context.SaveChanges();

                result.Entity = mapper.Map<UserViewModel>(updateUser);
                result.IsSuccess = true;
            }

            return result;
        }
        public General<UserViewModel> Delete(int id)
        {
            var result = new General<UserViewModel>();

            using (var context = new IcarusContext())
            {
                var user = context.User.SingleOrDefault(i => i.Id == id);
                context.User.Remove(user);
                context.SaveChanges();

                result.Entity = mapper.Map<UserViewModel>(user);
                result.IsSuccess = true;
            }

            return result;
        }
    }
}
