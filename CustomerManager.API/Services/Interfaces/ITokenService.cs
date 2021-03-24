using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManager.API.Models;

namespace CustomerManager.API.Services.Interfaces
{
    public interface ITokenService
    {
        //Todo
        //should take in a customer
        string CreateToken();
    }
}
