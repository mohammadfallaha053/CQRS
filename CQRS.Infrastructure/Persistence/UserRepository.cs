using CQRS.Application.Persistence;
using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Persistence;
public class UserRepository : IUserRepository
{
       // private readonly IUserRepository _userRepository;
        private static readonly List<User> _users=new ();


//public UserRepository(IUserRepository userRepository)
//{
//    _userRepository = userRepository;
//}

public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return  _users.SingleOrDefault(a => a.Email == email);
    }
}


