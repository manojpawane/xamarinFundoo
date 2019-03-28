namespace NoteApp.Pages
{
    using Firebase.Database;
    using Firebase.Database.Query;
    using NoteApp.Interfaces;
    using NoteApp.Models;
    using NoteApp.Repository;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// Collections of method for Note View in Grid format
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteView : ContentPage
    {
        NotesRepository noteRepository = new NotesRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteView"/> class.
        /// </summary>
        public NoteView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            /// On click goes to Create Note Page
            await Navigation.PushAsync(new CreateNote());
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            /// Calls run time service from respective device (Android, iOS, UWP) to get current user 
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();

            /// Gets the notes from Database with respect to user
            var notes = await noteRepository.GetNotesAsync(uid);

            /// if response is not null it will go to method where it will render as a Grid view
            if (notes != null)
            {
                NoteGridView(notes);
            }
        }


        /// <summary>
        /// Notes the grid view.
        /// </summary>
        /// <param name="list">The list.</param>
        public void NoteGridView(IList<Note> list)
        {
            /// adds two column for grid view
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            gridLayout.Margin = 5;
            int rowCount = 0;
            /// depending upon number of notes it will create number of rows which will be divided in two columns
            for (int row = 0; row < list.Count; row++)
            {
                /// to create a one row for two notes
                if (row % 2 == 0)
                {
                    /// create a row with height depends on note size
                    gridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                    rowCount++;
                }
            }

            //ListView listView = new ListView() { HasUnevenRows = true };

            var productIndex = 0;
            var indexe = -1;

            /// Iterate a single row at a time to add two notes in one row
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                /// iterating column to add per note in each column in a single row
                for (int columnIndex = 0; columnIndex < 2; columnIndex++)
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

                    /// label for title
                    var label = new Label
                    {
                        Text = data.Title,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,
                    };

                    var labelKey = new Label
                    {
                        Text = data.Key,
                        IsVisible = false
                    };


                    /// label for content of the notes
                    var Content = new Label
                    {
                        Text = data.Content,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Start,

                    };
                    StackLayout layout = new StackLayout()
                    {
                        Spacing = 2,
                        Margin = 2,
                        BackgroundColor = Color.White
                    };

                    var tapGestureRecognizer = new TapGestureRecognizer();

                    layout.Children.Add(labelKey);
                    layout.Children.Add(label);
                    layout.Children.Add(Content);

                    layout.GestureRecognizers.Add(tapGestureRecognizer);

                    /// adding StackLayout to frame to display it in card view
                    var frame = new Frame();
                    frame.Content = layout;

                    /// adding tap event
                  tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                    {
                        StackLayout layout1 = (StackLayout)sender;
                        IList<View> item = layout1.Children;
                        Label keyValue = (Label)item[0];
                        var keyVal = keyValue.Text;
                        Navigation.PushAsync(new UpdateNote(keyVal));
                    };

                    gridLayout.Children.Add(frame, columnIndex, rowIndex);
                }
            }
        }

        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ListViewNote());
        }
    }
}