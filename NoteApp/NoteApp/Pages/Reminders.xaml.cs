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
            var dateAndTime = DateTime.Now;
            reminderDate.MinimumDate = dateAndTime.Date;
		}

        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(true);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var date = reminderDate.Date;
            var time = reminderTime.Time;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder reminderInstance = stringBuilder.Append(date);
            reminderInstance = stringBuilder.Append(time);
        }
    }
}