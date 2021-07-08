using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class TerminalRepo: ITerminalRepo
    {
        private readonly ProjContext _ctx;

        public TerminalRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreateTerminal(Terminal model)
        {
            await _ctx._Terminals.AddAsync(model);
        }

        public IEnumerable<Terminal> GetTerminalsByUserId(string UserId)
        {
            return _ctx._Terminals.Where(e => e.UserId == UserId).ToList();
        }

        public Terminal GetTerminalBySerial(string Serial)
        {
            return _ctx._Terminals.Where(e => e.Serial == Serial).FirstOrDefault();
        }

        public IEnumerable<Terminal> GetTerminalsByPOSSerial(string PosSerial)
        {
            return _ctx._Terminals.Where(e => e.PosSerial == PosSerial).ToList();
        }

        public void RemoveTerminal(string Serial)
        {
            _ctx._Terminals.Remove(GetTerminalBySerial(Serial));
        }

        public void UpdateTerminal(Terminal user)
        {
            _ctx._Terminals.Update(user);
        }

        public SocketConnection updateTerminalConnId(string terminalGuid,string connId)
        {
            var socketConnection = _ctx._SocketConnection.Where( SC => SC.TerminalId == _ctx._Terminals.Where(T => T.Serial == terminalGuid).FirstOrDefault().Id).FirstOrDefault();
            if (socketConnection == null)
            {
                socketConnection = new SocketConnection
                {
                    ConnectionID = connId,
                    TerminalId = _ctx._Terminals.Where(T => T.Serial == terminalGuid).FirstOrDefault().Id
                };
                _ctx._SocketConnection.Add(socketConnection);
            }
            else
            {
                socketConnection.ConnectionID = connId;
                _ctx._SocketConnection.Update(socketConnection);
            }
           

            return socketConnection;
        }

        public IEnumerable<SocketConnection> GetTerminalsConnIDByUserId(string UserId)
        {
            return _ctx._Terminals.Where( p => p.UserId == UserId).ToList().Select(TerminalSocket).Where(e => e != null);
        }

        public SocketConnection GetConnIDByTerminalSerial(string TerminalSerial)
        {
            return _ctx._Terminals.Where(T => T.Serial == TerminalSerial).ToList().Select(TerminalSocket).FirstOrDefault();
        }

        //functional 
        private SocketConnection TerminalSocket(Terminal ter)
        {
            var x = 0;
            return _ctx._SocketConnection.Where(sc => sc.TerminalId == ter.Id).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public TerminalReadDto getState(TerminalReadDto Ter)
        {
            Ter.TerminalState = _ctx._TerminalState.Where(ts=> ts.Id == Ter.state).FirstOrDefault().State;
            return Ter;
        }

        public TerminalReadDto ConvertToReadDto(Terminal ter)
        {
            return new TerminalReadDto
            {
                Id = ter.Id,
                state = ter.state,
                Serial = ter.Serial,
                Table = ter.Table,
                PosSerial = ter.PosSerial
            };
        }
    }
}
