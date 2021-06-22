using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface ITerminalRepo
    {
        Task CreateTerminal(Terminal model);
        IEnumerable<Terminal> GetTerminalsByUserId(string UserId);
        Terminal GetTerminalBySerial(string Serial);
        IEnumerable<Terminal> GetTerminalsByPOSId(string PosSerial);
        IEnumerable<SocketConnection> GetTerminalsConnIDByUserId(string UserId);
        void RemoveTerminal(string Serial);
        void UpdateTerminal(Terminal user);
        SocketConnection updateTerminalConnId(string terminalGuid, string connId);
        SocketConnection GetConnIDByTerminalSerial(string TerminalSerial);
        void SaveChanges();

        public TerminalReadDto getState(TerminalReadDto pos);
        public TerminalReadDto ConvertToReadDto(Terminal pos);
    }
}
