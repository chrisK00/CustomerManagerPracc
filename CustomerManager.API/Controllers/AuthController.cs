using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManager.API.DTOs;
using CustomerManager.API.Models;
using CustomerManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManager.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public ActionResult<UserDTO> Login(string user)
        {
            //Todo
            //setup identity framework 

            var token = _tokenService.CreateToken();
            return new UserDTO { Token = token };
        }
    }
}
