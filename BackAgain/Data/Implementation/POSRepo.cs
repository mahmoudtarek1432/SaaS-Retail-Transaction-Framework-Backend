using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class POSRepo : IPOSRepo
    {
        private readonly ProjContext _ctx;

        public POSRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async Task CreatePOS(POS model)
        {
            try
            {
                await _ctx._POSs.AddAsync(model);
            }
            catch (Exception e) {

            }
        }

        public IEnumerable<POS> GetPOSsById(string UserId)
        {
            return _ctx._POSs.Where(e => e.UserId == UserId).ToList();
        }

        public POS GetPOSBySerial(string Serial)
        {
            return _ctx._POSs.Where(e => e.Serial == Serial).FirstOrDefault();
        }

        public void RemovePOS(string Serial)
        {
            _ctx._POSs.Remove(GetPOSBySerial(Serial));
        }

        public void UpdatePOS(POS user)
        {
            _ctx.Update(user);
        }

        public SocketConnection updatePOSConnId(string PosGuid, string connId)
        {
            var socketConnection = _ctx._SocketConnection.Where(SC => SC.PosID == _ctx._POSs.Where(T => T.Serial == PosGuid).FirstOrDefault().Id).FirstOrDefault();
            if (socketConnection == null)
            {
                socketConnection = new SocketConnection
                {
                    ConnectionID = connId,
                    PosID = _ctx._POSs.Where(T => T.Serial == PosGuid).FirstOrDefault().Id
                };
                _ctx._SocketConnection.Add(socketConnection);
            }
            socketConnection.ConnectionID = connId;
            _ctx._SocketConnection.Update(socketConnection);
            return socketConnection;
        }

        public IEnumerable<SocketConnection> GetPOSConnIDByUserId(string UserId)
        {
            
            return _ctx._POSs.Where(p => p.UserId == UserId).Select(GetPOSSocketConnection);
        }

        public SocketConnection GetPOSConnIDByPOSSerial(string POSSerial)
        {
            return _ctx._POSs.Where(p => p.Serial == POSSerial).Select(GetPOSSocketConnection).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        //functional 
        public POSReadDto getState(POSReadDto pos) {
            pos.PosState = _ctx._POSState.Where(ps => ps.Id == pos.state).FirstOrDefault().State;
            return pos;
        }

        public POSReadDto ConvertToReadDto(POS pos)
        {
            return new POSReadDto
            {
                Id = pos.Id,
                state = pos.state,
                Serial = pos.Serial,
                Name = pos.Name,
            };
        }

        public SocketConnection GetPOSSocketConnection(POS pos)
        {
            return _ctx._SocketConnection.Where(sc => sc.PosID == pos.Id).FirstOrDefault();
        }
    }
}
