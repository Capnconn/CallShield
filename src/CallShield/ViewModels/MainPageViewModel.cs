using System.Collections.ObjectModel;
using CallShield.Common.Models;
using CallShield.DataAccess.Repositories;
using CallShield.UI.Messages;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CallShield.UI.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private CallDetails _selectedCallDetails = null!;
        private bool _showPopup = false;

        public MainPageViewModel(ICallShieldRepository callShieldRepository)
        {
            ListOfBlockedCalls = callShieldRepository.GetAllBlockedCalls().Take(100).ToObservableCollection();

            WeakReferenceMessenger.Default.Register<CallReceivedMessage>(this, (recipient, message) =>
            {
                callShieldRepository.TryInsertCallDetails(message.CallDetails);

                var orderedBlockedCalls = ListOfBlockedCalls.FirstOrDefault(x => x.Date.Equals(message.CallDetails.Date));

                var newBlockedCall = new BlockedCall(message.CallDetails.Date);

                if (orderedBlockedCalls != null)
                {
                    ListOfBlockedCalls.Insert(0,newBlockedCall);
                }
                else
                {
                    ListOfBlockedCalls.Insert(0, newBlockedCall);
                }
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
