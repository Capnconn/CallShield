using System.Collections.ObjectModel;
using CallShield.Common.Services;
using CallShield.UI.Common.Models;
using CallShield.UI.Messages;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CallShield.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private BlockedCall _selectedCallDetails = null!;
        private bool _showPopup = false;

        public MainPageViewModel(ICallShieldService callShieldService)
        {
            ListOfBlockedCalls = callShieldService.GetAllBlockedCalls().Take(250).OrderByDescending(x => x.Date).ToObservableCollection();
           
            WeakReferenceMessenger.Default.Register<CallReceivedMessage>(this, (recipient, message) =>
            {
                callShieldService.InsertBlockedCall(message.CallDetails);
                var newBlockedCall = new CallDetails(message.CallDetails.Name, message.CallDetails.PhoneNumber, message.CallDetails.Date);
                var correspondingCall = ListOfBlockedCalls.FirstOrDefault(x => x.Date.Equals(message.CallDetails.Date));
                ListOfBlockedCalls.Insert(0, new BlockedCall(message.CallDetails.Date, [new CallDetails(message.CallDetails.Name, message.CallDetails.PhoneNumber, message.CallDetails.Date)]));
                
            });
        }

        public ObservableCollection<BlockedCall> ListOfBlockedCalls { get; set; } = [];

        public bool ShowPopup
        {
            get => this._showPopup;
            set => this.SetProperty(ref this._showPopup, value);
        }

        [RelayCommand]
        public void BlockNumber()
        {
            // Should consider adding some kind of loading mechanism to disply to the user while this works
            // Otherwise the user could overload it with clicks and cause the commands to execute multiple times.
            var t = _selectedCallDetails;
        }

        [RelayCommand]
        public void LongPress(BlockedCall callDetails)
        {
            _selectedCallDetails = callDetails;
            ShowPopup = true;
        }

        [RelayCommand]
        public void SaveContact()
        {
            var t = _selectedCallDetails;
        }
    }
}
