using CallShield.Common.Models;
using CallShield.UI.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace CallShield.UI.Processors
{
    public interface ICallProcessor
    {
        /// <summary>
        /// Processes the call details.
        /// </summary>
        /// <param name="blockedCall">The details of the blocked.</param>
        void NotifyOfBlockedCall(CallDetails blockedCall);
    }

    public class CallProcessor() : ICallProcessor
    {
        public void NotifyOfBlockedCall(CallDetails blockedCall)
            => WeakReferenceMessenger.Default.Send(new CallReceivedMessage(blockedCall));
    }
}
