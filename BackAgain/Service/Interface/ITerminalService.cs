using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service.Interface
{
    public interface ITerminalService
    {
        public Task<ClientResponseManager> CreateTerminal(string UserId, TerminalWriteDto model);

        public ClientResponseManager<IEnumerable<TerminalReadDto>> getAllTerminalsByPosSerial(string userId, string PosSerial);
        public ClientResponseManager<Terminal> GetTerminalBySerial(string userId, string Terminalserial);
        public ClientResponseManager<TerminalReadDto> getTerminalReadDtoBySerial(string userId, string TerminalSerial);

        ClientResponseManager UpdateTerminal(string userId, TerminalUpdateDto model);

        ClientResponseManager removeTerminal(string userId,string Serial);
    }
}
