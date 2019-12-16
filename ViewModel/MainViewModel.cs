using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Cars.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Page cur_Page;
        public Page Cur_Page
        {
            get { return cur_Page; }
            set
            {
                cur_Page = value;
                RaisePropertyChanged();
            }
        }


        public MainViewModel()
        {

            Cur_Page = Page.Authentication;
            Messenger.Default.Register<string>(this, "GO", (obj) => Cur_Page = Page.Game);
            Messenger.Default.Register<string>(this, "ChangeUser", (obj) => Cur_Page = Page.Authentication);
        }

        public enum Page
        {
            Authentication,
            Game
        }
    }
}