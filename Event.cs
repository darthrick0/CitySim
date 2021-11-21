namespace CitySim
{
    class Event
    {
        public string name = "Generic Event";
        public string eventDescription = "This is an event of generic type!!!";
        //public Resource_Category event_type;
        public int number_of_buttons;

        public float order_chaos_prerequisite;

        public int healthChange = 0;
        public int wealthChange = 0;
        public int industryChange = 0;
        public int foodEvent = 0;
        public int orderEvent = 0;
    }
}
