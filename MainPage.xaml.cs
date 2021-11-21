using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CitySim
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int Order = 50;
        int Wealth = 50;
        int Population = 50;
        int Industry = 50;
        int Food = 50;
        int Health = 50;

        int growthThreshold = 30;
        Random randomNumber = new Random();

        

        static System.Timers.Timer myTimer;
        List<Citizen> list_of_citizens = new List<Citizen>();
        List<Citizen> backlog_of_citizens = new List<Citizen>();
        int number_of_citizens = 0;
        int timeCounter = 0;

        int timerIntervalMilli = 1000;


        DateTime date = DateTime.Today;
        int currentMonth;
        int currentYear;
        DateTime currentTime = DateTime.Now;
        DateTime lastTime = DateTime.Now;


        public MainPage()
        {
            this.InitializeComponent();

            myTimer = new System.Timers.Timer();
            myTimer.Interval = timerIntervalMilli;
            myTimer.Elapsed += OnTimedEvent;
            myTimer.AutoReset = true;

            currentMonth = date.Month;
            currentYear = date.Year;
        }

        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            currentTime = DateTime.Now;
            lastTime = currentTime;

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                looping_text_block.Text = timeCounter.ToString();
                date = date.AddDays(1);
                date_text_block.Text = "Current Date: " + date.ToString("MM/dd/yyyy");
                CitizenNumberTextBlock.Text = "Number of Citizens: " + list_of_citizens.Count().ToString();

                if (currentMonth != date.Month)
                {
                    //execute monthly stuff
                    //myTimer.Enabled = false;
                    currentMonth = date.Month;
                    //Nav_Pop.IsOpen = true;
                    //Refresh_Table();
                }
                if (currentYear != date.Year)
                {
                    myTimer.Enabled = false;
                    ShowAMessage();
                    currentYear = date.Year;
                }

                PollPossibleBirthChances();
                
            });

            timeCounter++;
        }

        private async void ShowAMessage()
        {
            AppWindow appWindow = await AppWindow.TryCreateAsync();
            Frame appWindowContentFrame = new Frame();
            appWindowContentFrame.Navigate(typeof(EventPopup));
            ElementCompositionPreview.SetAppWindowContent(appWindow, appWindowContentFrame);
            await appWindow.TryShowAsync();
        }

        private void Add_Pop_Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime birthday = date.AddDays(-1 * 365 * 20);
            AddPop(birthday);
        }

        private void PollPossibleBirthChances()
        {
            //Reasoning for not having TimeSinceLast birth grow if growthThreshold is not achieved:
            //The body is unable to recover because there are health or food issues
            //Basically, body cant heal if it cannot get nutrients and medical attention
            if (Food > growthThreshold && Health > growthThreshold)
            {
                int numberOfBirths = 0;
                foreach (Citizen pop in list_of_citizens)
                {

                    if (pop.gender == "Female" && pop.TimeSinceLastBirth >= 365)
                    {
                        float popAge = GetAge(pop);
                        if (randomNumber.Next(0, 1000) == 998 && popAge >= 18f && popAge <= 60f)
                        {
                            //Birth
                            pop.TimeSinceLastBirth = 0;
                            numberOfBirths++;
                        }
                    }
                    else if (pop.gender == "Female" && pop.TimeSinceLastBirth < 365)
                    {
                        pop.TimeSinceLastBirth++;
                    }
                }
                for (int i = 0; i < numberOfBirths; i++)
                {
                    AddPop(date);
                }
            }
        }

        public void AddPop(DateTime popBirthday)
        {
            Citizen newPop = new Citizen(randomNumber.Next(1, 3), number_of_citizens, popBirthday);
            list_of_citizens.Add(newPop);
            number_of_citizens++;
        }

        private float GetAge(Citizen pop)
        {
            float age = date.Subtract(pop.Birthday).Days / 365;
            return age;
        }

        private void Refresh_Table()
        {
            citizen_dataGrid.ItemsSource = null;
            citizen_dataGrid.ItemsSource = list_of_citizens;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Enabled = false;
        }

        private void Speed1Button_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Enabled = true;
            myTimer.Interval = 1000;
        }

        private void Speed10Button_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Enabled = true;
            myTimer.Interval = 100;
        }

        private void Speed100Button_Click(object sender, RoutedEventArgs e)
        {
            myTimer.Enabled = true;
            myTimer.Interval = 10;
        }

    }
}
