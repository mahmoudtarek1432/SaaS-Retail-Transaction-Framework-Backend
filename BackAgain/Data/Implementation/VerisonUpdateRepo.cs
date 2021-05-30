using BackAgain.Data.Inteface;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Implementation
{
    public class VerisonUpdateRepo : IVerisonUpdateRepo
    {
        private readonly ProjContext _ctx;

        public VerisonUpdateRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        public async void CreateVersion(VersionUpdateLog log)
        {
           await _ctx._VersionUpdateLog.AddAsync(log);
        }

        public VersionUpdateLog GetLastVersioning(string userId)
        {
            return _ctx._VersionUpdateLog.Where(v => v.UserId == userId).LastOrDefault();
        }

        public IEnumerable<VersionUpdateLog> GetNotUpdatedVersions(string userId, int MenuVersion, int SettingsVersion)
        {
            return _ctx._VersionUpdateLog.Where(v => v.UserId == userId && (v.MenuVersion > MenuVersion || v.SettingsVersion > SettingsVersion));
        }

        public void UpdateVersion(VersionUpdateLog log)
        {
            _ctx._VersionUpdateLog.Update(log);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
