using Firebase.Database;
using Firebase.Database.Query;
using NoteApp.Interfaces;
using NoteApp.Models;
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
	public partial class ListViewNote : ContentPage
	{
		public ListViewNote ()
		{
			InitializeComponent ();
		}

        FirebaseClient firebaseClient = new FirebaseClient("https://todonotes-b0960.firebaseio.com/");

        protected async override void OnAppearing()
        {
            /// Calls run time service from respective device (Android, iOS, UWP) to get current user 
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();

            /// Gets the Note of respective user
            IList<Note> noteData = (await firebaseClient.Child("User").Child(uid).Child("Note").OnceAsync<Note>()).Select(item => new Note
            {
                Title = item.Object.Title,
                Content = item.Object.Content
            }).ToList();

            /// if response is not null it will go to method where it will render as a Grid view
            if (noteData != null)
            {
                NoteListView(noteData);
            }
        }

        public void NoteListView(IList<Note> list)
        {
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(280) });
            gridLayout.Margin = 5;
            int rowCount = list.Count;

            

            //ListView listView = new ListView() { HasUnevenRows = true };

            var productIndex = 0;
            var title = "";
            var content = "";
            var indexe = -1;

            /// Iterate a single row at a time to add two notes in one row
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                /// iterating column to add per note in each column in a single row
                for (int columnIndex = 0; columnIndex < 1; columnIndex++)
                {
                    Note data = null;
                    indexe++;

                    /// to maintain the size of array to avoid exception
                    if (indexe < list.Count)
                    {
                        data = list[indexe];
                    }

                    /// Once every note is added in respective column and row than it will break
                    if (productIndex >= list.Count)
                    {
                        break;
                    }

                    productIndex += 1;
                    var index = rowIndex * columnIndex + columnIndex;

                    title = data.Title;
                    content = data.Content;

                    var layout = new StackLayout();

                    /// label for title
                    var label = new Label
                    {
                        Text = title,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                    };

                    /// label for content of the notes
                    var Content = new Label
                    {
                        Text = content,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,

                    };

                    layout.Children.Add(label);
                    layout.Children.Add(Content);
                    layout.Spacing = 2;
                    layout.Margin = 2;
                    layout.BackgroundColor = Color.White;

                    /// adding StackLayout to frame to display it in card view
                    var frame = new Frame();
                    frame.Content = layout;
                    gridLayout.Children.Add(frame, columnIndex, rowIndex);
                }
            }
        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NoteView());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNote());
        }
    }
}