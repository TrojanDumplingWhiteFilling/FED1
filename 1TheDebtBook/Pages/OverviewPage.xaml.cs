using _1TheDebtBook.ViewModels;
namespace _1TheDebtBook.Pages;

public partial class OverviewPage : ContentPage
{
	private void UpdateList()
	{
		var debtorId = MainViewModel.Instance.DebtorId;
		// Use debtorId to load relevant transactions...
	}
	public OverviewPage(OverviewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		var uri = Shell.Current.CurrentState.Location.OriginalString;
		var query = System.Web.HttpUtility.ParseQueryString(new Uri(uri).Query);
		var debtorId = query.Get("DebtorId");

		if (int.TryParse(debtorId, out int id))
		{
			// Assuming your ViewModel is already bound as the BindingContext
			var viewModel = this.BindingContext as OverviewViewModel;
			if (viewModel != null)
			{
				await viewModel.InitializeWithDebtor(id);
			}
		}
	}


}