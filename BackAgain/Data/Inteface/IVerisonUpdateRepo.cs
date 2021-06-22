using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data.Inteface
{
    public interface IVerisonUpdateRepo
    {
        Task CreateVersion(VersionUpdateLog log);
        VersionUpdateLog GetLastVersioning(string userId);
        IEnumerable<VersionUpdateLog> GetNotUpdatedVersions(string userId, int MenuVersion, int SettingsVersion);
        public void UpdateVersion(VersionUpdateLog log);
        void SaveChanges();
    }
}
