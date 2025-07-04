using CallShield.UI.Models;
using CallShield.UI.ViewModels;

namespace CallShield.UI
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _mainPageViewModel;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            _mainPageViewModel = mainPageViewModel;
            BindingContext = mainPageViewModel;
        }

        // I hate code behind, but the LongPressCommandParameter is, somehow, incapable of passing in
        // a non-null parameter (even though that is its' purpose), so I have to use this.
        void OnLongPressCompleted(object sender, EventArgs e)
        {
            var view = (BindableObject)sender;
            var item = view.BindingContext as CallDetails;  // This is your actual item!
            _mainPageViewModel.LongPressCommand.Execute(item);
        }
    }

}
