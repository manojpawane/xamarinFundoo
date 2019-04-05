using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Reminders : PopupPage
    {
		public Reminders ()
		{
			InitializeComponent ();
		}

        [Obsolete]
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}