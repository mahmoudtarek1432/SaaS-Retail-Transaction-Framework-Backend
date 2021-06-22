using AutoMapper;
using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Model;
using BackAgain.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Implementation
{
    public class TerminalService : ITerminalService
    {
        private readonly ITerminalRepo _TerminalRepo;
        private readonly IMapper _mapper;

        public TerminalService(ITerminalRepo terminalRepo, IMapper mapper)
        {
            _TerminalRepo = terminalRepo;
            _mapper = mapper;
        }

        public async Task<ClientResponseManager> CreateTerminal(string UserId, TerminalWriteDto model)
        {
            try
            {
                var terminalData = _mapper.Map<Terminal>(model);
                terminalData.Serial = Guid.NewGuid().ToString();
                terminalData.UserId = UserId;
                await _TerminalRepo.CreateTerminal(terminalData);
                _TerminalRepo.SaveChanges();
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
                Message = "Terminal successfully created"
            };
        }

        public ClientResponseManager<IEnumerable<TerminalReadDto>> getAllTerminalsByPosSerial(string userId, string posSerial)
        {
            var Ters = _TerminalRepo.GetTerminalsByPOSId(posSerial);

            if(Ters != null)
            {
                if (Ters.FirstOrDefault().UserId == userId)
                {
                    var TerReadDto = Ters.Select(_TerminalRepo.ConvertToReadDto)
                                         .Select(_TerminalRepo.getState);
                    return new ClientResponseManager<IEnumerable<TerminalReadDto>>
                    {
                        IsSuccessfull = true,
                        ResponseObject = TerReadDto
                    };
                }
                return new ClientResponseManager<IEnumerable<TerminalReadDto>>
                {
                    IsSuccessfull = false,
                    Message = "No Terminals Were found."
                };
            }            return new ClientResponseManager<IEnumerable<TerminalReadDto>>
            {
                IsSuccessfull = false,
                Message = "PosId Does not belong to user."
            };
        }

        public ClientResponseManager<TerminalReadDto> getTerminalReadDtoBySerial(string userId, string TerminalSerial)
        {
            var Ters = _TerminalRepo.GetTerminalBySerial(TerminalSerial);

            if (Ters != null)
            {
                if (Ters.UserId == userId)
                {
                    var TerReadDto = _TerminalRepo.getState(_TerminalRepo.ConvertToReadDto(Ters));
                    return new ClientResponseManager<TerminalReadDto>
                    {
                        IsSuccessfull = true,
                        ResponseObject = TerReadDto
                    };
                }
                return new ClientResponseManager<TerminalReadDto>
                {
                    IsSuccessfull = false,
                    Message = "Terminal not found."
                };
            }
            return new ClientResponseManager<TerminalReadDto>
            {
                IsSuccessfull = false,
                Message = "Terminal Does not belong to user."
            };
        }

        public ClientResponseManager<Terminal> GetTerminalBySerial(string serial, string userId)
        {
            var Ters = _TerminalRepo.GetTerminalBySerial(serial);

            if (Ters != null)
            {
                if (Ters.UserId == userId)
                {
                    return new ClientResponseManager<Terminal>
                    {
                        IsSuccessfull = true,
                        ResponseObject = Ters
                    };
                }
                return new ClientResponseManager<Terminal>
                {
                    IsSuccessfull = false,
                    Message = "Terminal not found."
                };
            }
            return new ClientResponseManager<Terminal>
            {
                IsSuccessfull = false,
                Message = "Terminal Does not belong to user."
            };
        }

        public ClientResponseManager removeTerminal(string userId, string Serial)
        {
            var ter = _TerminalRepo.GetTerminalBySerial(Serial);

            if (ter.UserId != userId)
            {
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "Terminal does not belong to user"
                };
            }

            try
            {
                _TerminalRepo.RemoveTerminal(Serial);
                _TerminalRepo.SaveChanges();
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
                Message = "Terminal deleted successfully"
            };
        }

        public ClientResponseManager UpdateTerminal(string userId, TerminalUpdateDto model)
        {
            var ter = _TerminalRepo.GetTerminalBySerial(model.Serial);

            if(ter.UserId != userId)
            {
                return new ClientResponseManager
                {
                    IsSuccessfull = false,
                    Message = "Terminal does not belong to user"
                };
            }

            if (!string.IsNullOrEmpty(model.Serial))
            {
                ter.Serial = model.Serial;
            }

            ter.Table = model.Table;
            ter.state = model.state;


            try
            {
                _TerminalRepo.UpdateTerminal(ter);
                _TerminalRepo.SaveChanges();
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
                Message = "Terminal updated successfully"
            };
        }
    }
}
