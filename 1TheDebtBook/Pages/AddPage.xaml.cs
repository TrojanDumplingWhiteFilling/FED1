using _1TheDebtBook.ViewModels;
using _1TheDebtBook.Helpers;
namespace _1TheDebtBook.Pages;


public partial class AddPage : ContentPage
{
	public AddPage(AddViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}