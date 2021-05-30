using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackAgain.Dto;
using BackAgain.Model;

namespace BackAgain.Data
{
    public class SubscriptionRepo : ISubscriptionRepo
    {
        private readonly ProjContext _ctx;

        public SubscriptionRepo(ProjContext ctx)
        {
            _ctx = ctx;
        }

        // The SubscriptionType is a foreignkey to a primary key found in Subscriptions model
        // the Subscriptions model has the diffrent Subscription "Types"
        public async Task<ClientResponseManager<bool>> CreateSubscription(string UserId, int SubscriptionType)
        {
            UserSubscription userSub = new UserSubscription {
                UserId = UserId,
                SubscriptionId = SubscriptionType,
                SubscriptionDate = DateTime.UtcNow
            };
            try
            {
                await _ctx._UserSubscriptions.AddAsync(userSub);
            }catch (Exception e)
            {
                return new ClientResponseManager<bool>
                {
                    Message = e.Message,
                    IsSuccessfull = false
                };
            }
            return new ClientResponseManager<bool>
            {
                IsSuccessfull = true
            };

        }

        public IEnumerable<SubscriptionReadDto> GetUserAllSubscriptions(string UserId)
        {
            return _ctx._UserSubscriptions.Where(e => e.UserId == UserId).Select(getSubDetails).Select(toSubReadDto);
        }


        public SubscriptionReadDto GetUserCurrentSubscription(string UserId)
        {
            return toSubReadDto(getSubDetails(_ctx._UserSubscriptions.Where(e => e.UserId == UserId).LastOrDefault()));
        }

        //Functional methods
        private UserSubscription getSubDetails(UserSubscription userSub)
        {
            userSub.Sub = _ctx._subscriptions.Where(e => userSub.SubscriptionId == e.Id).FirstOrDefault();
            return userSub;
        }

        private SubscriptionReadDto toSubReadDto(UserSubscription userSub)
        {

            return new SubscriptionReadDto
            {
                SubscriptionId = userSub.Sub.Id,
                Cost = userSub.Sub.Cost,
                Duration = userSub.Sub.Duration,
                Name = userSub.Sub.Name,
                SubscriptionDate = userSub.SubscriptionDate,
                UserId = userSub.UserId
            };
        }

        public void SaveChanges()
        {
            _ctx.SaveChangesAsync();
        }
    }
}
