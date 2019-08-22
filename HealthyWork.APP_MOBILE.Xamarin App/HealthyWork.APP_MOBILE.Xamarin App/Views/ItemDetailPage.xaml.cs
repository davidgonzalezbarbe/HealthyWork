using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using HealthyWork.APP_MOBILE.Xamarin_App.Models;
using HealthyWork.APP_MOBILE.Xamarin_App.ViewModels;

namespace HealthyWork.APP_MOBILE.Xamarin_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}