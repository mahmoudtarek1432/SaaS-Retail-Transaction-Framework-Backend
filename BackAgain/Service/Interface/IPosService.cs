using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IPosService
    {
        public Task<ClientResponseManager> CreatePOS(string UserId, POSWriteDto model);

        public ClientResponseManager<IEnumerable<POSReadDto>> getAllPOSsByUserId(string userId);

        public ClientResponseManager<POS> GetPOSBySerial(string serial);

        public ClientResponseManager<POSReadDto> GetPOSReadDtoBySerial(string serial);

        ClientResponseManager UpdatePos(POSUpdateDto model);

        ClientResponseManager removePos(string Serial);
    }
}
