using System;

using HealthyWork.APP_MOBILE.Xamarin_App.Models;

namespace HealthyWork.APP_MOBILE.Xamarin_App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
