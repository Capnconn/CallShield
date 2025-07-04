using System.Collections.ObjectModel;
using CallShield.UI.Messages;
using CallShield.UI.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CallShield.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private CallDetails _selectedCallDetails = null!;
        private bool _showPopup = false;

        public MainPageViewModel()
        {
            WeakReferenceMessenger.Default.Register<CallReceivedMessage>(this, (recipient, message) =>
            {
                var blockedCallGroup = ListOfBlockedCalls.FirstOrDefault(x => x.Date.Equals(message.CallDetails.Date));

                if (blockedCallGroup != null)
                {
                    blockedCallGroup.Insert(0, message.CallDetails);
                }
                else
                {
                    ListOfBlockedCalls.Insert(0, new BlockedCalls(message.CallDetails.Date, [message.CallDetails]));
                }
            });
        }

        public ObservableCollection<BlockedCalls> ListOfBlockedCalls { get; set; } = [];

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
        public void LongPress(CallDetails callDetails)
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
