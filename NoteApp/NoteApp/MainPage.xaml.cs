using NoteApp.Models;
using NoteApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteApp
{
        public partial class MainPage : MasterDetailPage
        {
            public List<MasterPageItem> menuList { get; set; }

            public MainPage()
            {
                InitializeComponent();

                menuList = new List<MasterPageItem>();

                // Creating our pages for menu navigation
                // Here you can define title for item, 
                // icon on the left side, and page that you want to open after selection
                var page1 = new MasterPageItem() { Title = "Note", Icon = "note.png", TargetType = typeof(NoteView) };
                var page2 = new MasterPageItem() { Title = "Reminder", Icon = "reminder.png", TargetType = typeof(Reminders) };
                var page3 = new MasterPageItem() { Title = "Archive", Icon = "archive.png", TargetType = typeof(Archive) };
                var page4 = new MasterPageItem() { Title = "Delete", Icon = "trash.png", TargetType = typeof(Trash) };
                var page5 = new MasterPageItem() { Title = "Logout", TargetType = typeof(SignOut) };

            // Adding menu items to menuList
                menuList.Add(page1);
                menuList.Add(page2);
                menuList.Add(page3);
                menuList.Add(page4);
                menuList.Add(page5);
                // Setting our list to be ItemSource for ListView in MainPage.xaml
                navigationDrawerList.ItemsSource = menuList;

                // Initial navigation, this can be used for our home page
                Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(NoteView)));
            }

            // Event for Menu Item selection, here we are going to handle navigation based
            // on user selection in menu ListView
            private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
            {

                var item = (MasterPageItem)e.SelectedItem;
                Type page = item.TargetType;
                Detail = new NavigationPage((Page)Activator.CreateInstance(page));
                IsPresented = false;
            }
        }
    }


