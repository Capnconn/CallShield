using CallShield.DataAccess.Repositories;

namespace CallShield.Common.Services
{
    public interface ICallShieldService
    {
        List<UI.Common.Models.BlockedCall> GetAllBlockedCalls();

        void InsertBlockedCall(Models.CallDetails callDetails);
    }

    public class CallShieldService(ICallShieldRepository callShieldRepository) : ICallShieldService
    {
        public List<UI.Common.Models.BlockedCall> GetAllBlockedCalls()
        {
            var blockedCalls = callShieldRepository.GetAllBlockedCalls();

            var blockedCallsToReturn = new List<UI.Common.Models.BlockedCall>();

            blockedCalls.ForEach(x =>
            {
                var correspondingBlockedCall = blockedCallsToReturn.FirstOrDefault(y => x.Date.Date.Equals(y.Date.Date));
                var callDetailsToAdd = new UI.Common.Models.CallDetails(x.Name, x.PhoneNumber, x.Date);

                if (correspondingBlockedCall is null)
                {
                    blockedCallsToReturn.Add(new UI.Common.Models.BlockedCall(x.Date, [callDetailsToAdd]));
                }
                else
                {
                    correspondingBlockedCall.Add(callDetailsToAdd);
                }
            });

            return blockedCallsToReturn;
        }

        public void InsertBlockedCall(Models.CallDetails callDetails)
        {
            callShieldRepository.InsertBlockedCall(new DataAccess.Models.BlockedCall
            {
                Date = callDetails.Date,
                Name = callDetails.Name,
                PhoneNumber = callDetails.PhoneNumber
            });
        }
    }
}
