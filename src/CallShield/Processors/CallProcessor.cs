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
        /// <param name="callDetails">The details of the call to display.</param>
        void NotifyOfBlockedCall(CallDetails callDetails);
    }

    public class CallProcessor() : ICallProcessor
    {
        public void NotifyOfBlockedCall(CallDetails callDetails)
            => WeakReferenceMessenger.Default.Send(new CallReceivedMessage(callDetails));
    }
}
