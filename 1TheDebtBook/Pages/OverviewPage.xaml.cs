using _1TheDebtBook.ViewModels;
namespace _1TheDebtBook.Pages;

public partial class OverviewPage : ContentPage
{
    public OverviewPage(OverviewViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}