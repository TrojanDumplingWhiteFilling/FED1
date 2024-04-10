using _1TheDebtBook.Pages;

namespace _1TheDebtBook
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(OverviewPage), typeof(OverviewPage));
        }
    }
}
