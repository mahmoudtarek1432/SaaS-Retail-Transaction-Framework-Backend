using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Service.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackAgain.Service.Implementation
{
    public class TerminalAuthService : ITerminalAuthService
    {
        private readonly ITerminalRepo _TerRepo;
        private readonly IConfiguration _config;

        public TerminalAuthService(ITerminalRepo TerRepo, IConfiguration config)
        {
            _TerRepo = TerRepo;
            _config = config;
        }

        public ClientResponseManager<string> TerminalLogin(string serial)
        {
            var ter = _TerRepo.GetTerminalBySerial(serial);
            if (ter != null)
            {
                try
                {
                    //update pos state to connected
                    ter.state = 1;// connected
                    _TerRepo.UpdateTerminal(ter);
                    _TerRepo.SaveChanges();
                }
                catch (Exception e)
                {
                    return new ClientResponseManager<string>
                    {
                        IsSuccessfull = false,
                        Message = e.Message
                    };

                }
                //make the jwt token
                var claims = new[]{
                    new Claim(ClaimTypes.SerialNumber, serial),
                    new Claim(ClaimTypes.NameIdentifier, ter.UserId)
                };

                var token = JWTService.MakeJwtToken(_config, claims, 90);

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

        public ClientResponseManager TerminalLogout(string serial)
        {
            try
            {
                var ter = _TerRepo.GetTerminalBySerial(serial);
                //update pos state to disconnected
                ter.state = 2;// connected
                _TerRepo.UpdateTerminal(ter);
                _TerRepo.SaveChanges();
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
