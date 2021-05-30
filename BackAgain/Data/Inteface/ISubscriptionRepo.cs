using BackAgain.Dto;
using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Data
{
    public interface ISubscriptionRepo
    {
        Task<ClientResponseManager<bool>> CreateSubscription(string UserId, int SubscriptionType);
        SubscriptionReadDto GetUserCurrentSubscription(string UserId);
        IEnumerable<SubscriptionReadDto> GetUserAllSubscriptions(string UserId);
        void SaveChanges();
    }
}
