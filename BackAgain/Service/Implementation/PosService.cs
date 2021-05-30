using AutoMapper;
using BackAgain.Data;
using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class PosService : IPosService
    {
        private readonly IPOSRepo _posRepo;
        private readonly IMapper _mapper;

        public PosService(IPOSRepo posRepo, IMapper mapper)
        {
            _posRepo = posRepo;
            _mapper = mapper;
        }

        public ClientResponseManager CreatePOS(string UserId, POSWriteDto model)
        {
            try
            {
                var PosData = _mapper.Map<POS>(model);
                PosData.Serial = Guid.NewGuid().ToString();
                PosData.UserId = UserId;
                _posRepo.CreatePOS(PosData);
                _posRepo.SaveChanges();
            }
            catch(Exception e)
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
                Message = "client successfully created"
            };
        }

        public ClientResponseManager<IEnumerable<POSReadDto>> getAllPOSsByUserId(string userId)
        {
            var POSs = _posRepo.GetPOSsById(userId)
                               .Select(_posRepo.ConvertToReadDto)
                               .Select(_posRepo.getState);

            return new ClientResponseManager<IEnumerable<POSReadDto>>
            {
                IsSuccessfull = true,
                ResponseObject = POSs
            };
        }

        public ClientResponseManager<POS> GetPOSBySerial(string serial)
        {
            var POS = _posRepo.GetPOSBySerial(serial);           

            return new ClientResponseManager<POS>
            {
                IsSuccessfull = true,
                ResponseObject = POS
            };
        }

        public ClientResponseManager<POSReadDto> GetPOSReadDtoBySerial(string serial)
        {
            var POS = _posRepo.getState(_posRepo.ConvertToReadDto(_posRepo.GetPOSBySerial(serial)));

            return new ClientResponseManager<POSReadDto>
            {
                IsSuccessfull = true,
                ResponseObject = POS
            };
        }

        public ClientResponseManager UpdatePos(POSUpdateDto model)
        {
            var pos = _posRepo.GetPOSBySerial(model.Serial);

            if (!string.IsNullOrEmpty(model.Name))
            {
                pos.Name = model.Name;
            }

                pos.state = model.state;

            try
            {
                _posRepo.UpdatePOS(pos);
                _posRepo.SaveChanges();
            }
            catch(Exception e)
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
                Message = "pos updated successfully"
            };
        }

        public ClientResponseManager removePos(string Serial)
        {
            try
            {
                _posRepo.RemovePOS(Serial);
                _posRepo.SaveChanges();
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
                Message = "pos removed successfully"
            };
        }
    }
}
