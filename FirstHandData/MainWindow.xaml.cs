using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace FirstHandData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public ObservableCollection<string> Events
        {
            get { return (ObservableCollection<string>)GetValue(EventsProperty); }
            set { SetValue(EventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Events.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventsProperty =
            DependencyProperty.Register("Events", typeof(ObservableCollection<string>), typeof(MainWindow));



        public string selectedEvent
        {
            get { return (string)GetValue(selectedEventProperty); }
            set { SetValue(selectedEventProperty, value); }
        }

        // Using a DependencyProperty as the backing store for selectedEvent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedEventProperty =
            DependencyProperty.Register("selectedEvent", typeof(string), typeof(MainWindow));
        

        public MainWindow()
        {
           
            InitializeComponent();
            FirstHandDM Eventdata = new FirstHandDM();

        }

        private void First_Hand_Data_Loaded(object sender, RoutedEventArgs e)
        { 
            //Initializing objects and populating collections
            Events = new ObservableCollection<string>();
            Events.Add("Add New Event.....");
           
            selectedEvent = Events[0];
             var loadEvents = new FirstHandDM();
             var EventData = (from m in loadEvents.Events select m.event_name);
            

            if (EventData != null)
            { 
                foreach (var location in EventData)
                {
                    Events.Add(location.ToString());
                    
                }
            }


        }
        private void proceed_Click(object sender, RoutedEventArgs e)
        {
            if(selectedEvent == "Add New Event.....")
            {
                selectEvent.Visibility = Visibility.Collapsed;

                createEvent.Visibility = Visibility.Visible;
            }

            else
            {
                selectEvent.Visibility = Visibility.Collapsed;

                dataGather.Visibility = Visibility.Visible;

                
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            eventTitle.Text = string.Empty;
            eventLocation.Text = string.Empty;
            selectEvent.Visibility = Visibility.Visible;

            createEvent.Visibility = Visibility.Collapsed;

        }

        private void Commit_Click(object sender, RoutedEventArgs e)
        {



            //TODO: Add Validation so empty strings cannot be entered

            //Creates and instance of FirstHandDM entity
            var DBCtx = new FirstHandDM();

            //Creates a new object Event
            var newEvent = new Event()
                    {
                        event_name = eventTitle.Text,
                        location = eventLocation.Text,
                    };

            //Adds newly created object to entity framework
            DBCtx.Events.Add(newEvent);

            //Commits to database
            DBCtx.SaveChanges();
            DBCtx.Dispose();
            
            //Adds new event to event list
            Events.Add(eventTitle.Text);


            //resets textboxes
            eventTitle.Text = string.Empty;
            eventLocation.Text = string.Empty;


            //Changes visiblity of grids
            selectEvent.Visibility = Visibility.Visible;
            createEvent.Visibility = Visibility.Collapsed;

         

        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Add Validation, no empty strings, and phone and email address must be valid

            //Creates and in scope instance of FirstHandDM entity
            var DBCtx = new FirstHandDM();

            //Creates an object of Contact from FirstHandDN
            var newContact = new Contact();

            //Assigns values from the textboxes to the entity properties
            newContact.first_name = firstName.Text;
            newContact.last_name = lastName.Text;
            newContact.company = companyName.Text;
            newContact.phone_number = phone.Text;
            newContact.email = emailAddress.Text;

            DBCtx.Contacts.Add(newContact);
            DBCtx.SaveChanges();

         
            // resets textboxes
            firstName.Text = string.Empty;
            lastName.Text = string.Empty;
            companyName.Text = string.Empty;
            phone.Text = string.Empty;
            emailAddress.Text = string.Empty;

        }


    }
}
