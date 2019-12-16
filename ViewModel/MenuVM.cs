using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Cars.ViewModel
{
    public class MenuVM : ViewModelBase
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
        public MenuVM()
        {
           
            Cur_Page = Page.Upgrade;
        }


        private ICommand by_CarPage;
        public ICommand By_CarPage => by_CarPage ?? (by_CarPage = new RelayCommand(() => { Cur_Page = Page.By_Car; }, () => Cur_Page != Page.By_Car));

        private ICommand by_TrackPage;
        public ICommand By_TrackPage => by_TrackPage ?? (by_TrackPage = new RelayCommand(() => { Cur_Page = Page.By_Track; }, () => Cur_Page != Page.By_Track));

        private ICommand upgrade_Page;
        public ICommand Upgrade_Page => upgrade_Page ?? (upgrade_Page = new RelayCommand(() => { Cur_Page = Page.Upgrade; },()=> Cur_Page != Page.Upgrade));

        public enum Page
        {
            By_Car,
            By_Track,
            Upgrade
        }
    }
}
