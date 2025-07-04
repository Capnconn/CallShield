using CallShield.DataAccess.Models;
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
        void ProcessCall(CallDetails callDetails);
    }

    public class CallProcessor() : ICallProcessor
    {
        public void ProcessCall(CallDetails callDetails)
            => WeakReferenceMessenger.Default.Send(new CallReceivedMessage(callDetails));
    }
}
