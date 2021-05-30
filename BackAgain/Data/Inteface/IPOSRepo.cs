using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface IPOSRepo
    {
        Task CreatePOS(POS model);
        IEnumerable<POS> GetPOSsById(string UserId);
        POS GetPOSBySerial(string Serial);
        void RemovePOS(string Serial);
        void UpdatePOS(POS user);
        SocketConnection updatePOSConnId(string PosGuid, string connId);
        SocketConnection GetPOSConnIDByUserId(string UserId);
        void SaveChanges();

        public POSReadDto getState(POSReadDto pos);
        public POSReadDto ConvertToReadDto(POS pos);

    }
}
