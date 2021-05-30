using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class POSAuthService : IPOSAuthService
    {
        private readonly IPOSRepo _repo;
        private readonly IConfiguration _config;

        public POSAuthService(IPOSRepo repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public ClientResponseManager<string> POSLogin(string serial)
        {
            var pos = _repo.GetPOSBySerial(serial);
            if(pos != null)
            {
                try
                {
                    //update pos state to connected
                    pos.state = 1;// connected
                    _repo.UpdatePOS(pos);
                    _repo.SaveChanges();
                }
                catch (Exception e)
                {
                    return new ClientResponseManager<string> { 
                    IsSuccessfull = false,
                    Message = e.Message
                    };

                }
                //make the jwt token
                var claims = new []{
                    new Claim(ClaimTypes.SerialNumber, serial),
                    new Claim(ClaimTypes.NameIdentifier, pos.UserId)
                };

                var token = JWTService.MakeJwtToken(_config,claims,90);

                return new ClientResponseManager<string>
                {
                    IsSuccessfull = true,
                    Message = "POS logged in successfully"
                };
            }
            return new ClientResponseManager<string>
            {
                IsSuccessfull = false,
                Message = "serial not correct"
            };
        }

        public ClientResponseManager POSLogout(string serial)
        {
            try
            {
                var pos = _repo.GetPOSBySerial(serial);
                //update pos state to disconnected
                pos.state = 2;// connected
                _repo.UpdatePOS(pos);
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = e.Message
                };
            }
            return new ClientResponseManager
            {
                IsSuccessfull = true,
                Message = "POSLogged Out"
            };
        }
    }
}
