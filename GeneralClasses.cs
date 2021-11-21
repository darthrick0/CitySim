using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitySim
{
    class Citizen
    {
        public string name;
        public Citizen_Job job;
        public Wealth_Category wealth;
        public Health_Category health;

        public String Name { get; set; }
        public String Job { get; set; }
        public String Wealth { get; set; }
        public String Health { get; set; }
        public DateTime Birthday { get; set; }
        public String gender { get; }
        public Citizen Partner { get; set; }
        public int TimeSinceLastBirth { get; set; }

        public Citizen(int randGender, int current_number_of_citizens, DateTime birthday)
        {
            if (randGender == 1)
            {
                gender = "Male";
                TimeSinceLastBirth = -1;
            }
            else
            {
                gender = "Female";
                TimeSinceLastBirth = 0;
            }
            name = "citizen " + current_number_of_citizens++.ToString();
            job = Citizen_Job.Farmer;
            wealth = Wealth_Category.Middle_Class;
            health = Health_Category.Healthy;
            Birthday = birthday;
            Name = name;
            Job = job.ToString();
            Wealth = wealth.ToString();
            Health = health.ToString();
/*            job = Citizen_Job.Unavailable;
            wealth = Wealth_Category.Unavailable;
            health = Health_Category.Unavailable;*/
        }


        public void ActivateCitizen(int current_number_of_citizens, DateTime birthday) //Add Job, Health, Wealth and name inputs later
        {
            name = "citizen " + current_number_of_citizens++.ToString();
            job = Citizen_Job.Farmer;
            wealth = Wealth_Category.Middle_Class;
            health = Health_Category.Healthy;
            Birthday = birthday;
            Name = name;
            Job = job.ToString();
            Wealth = wealth.ToString();
            Health = health.ToString();
        }
        
/*        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string isAsigned = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(isAsigned));
        }*/

    }


    enum Health_Category
    {
        Dead,
        Sick,
        Healthy,
        Unavailable
    }

    enum Citizen_Job
    {
        Farmer,
        Miner,
        Smith,
        Builder,
        Healer,
        Banker,
        Unavailable
    }

    enum Wealth_Category
    {
        Poor,
        Middle_Class,
        Wealthy,
        Unavailable
    }

    enum Resource_Category
    {
        Food,
        Industry,
        Infrastructure,
        Population,
        Health,
        Money,
        Education
    }
}
