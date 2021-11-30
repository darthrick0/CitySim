using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using Windows.Storage;


namespace CitySim
{
    public class GameEvent
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public object AdditionalEffect { get; set; }
        public object EventPreReq1 { get; set; }
        public object EventPreReq2 { get; set; }
        public object EventPreReq3 { get; set; }
        public int FoodEffect { get; set; }
        public int HealthEffect { get; set; }
        public int IndustryEffect { get; set; }
        public int OrderEffect { get; set; }
        public int WealthEffect { get; set; }
        public string FoodPreReq { get; set; }
        public string HealthPreReq { get; set; }
        public string IndustryPreReq { get; set; }
        public string OrderPreReq { get; set; }
        public string WealthPreReq { get; set; }
    }

    public static class GameEventHandler
    {
        /*public static List<GameEvent> LoadGameEventJson(string jsonString)
        {
            

            return listOfGameEvents;
        }*/


        public static bool CheckLogic(string s, int value)
        {
            //current max number of boolean statements is 2. Maybe increase later, but probably not
            if (s == null || s.Length <= 0) return true;
            char[] delimiters = { '|', '&' };
            string[] words = s.Split(delimiters);
            if (s.Contains("|"))
            {
                Console.WriteLine("| Detected");
                if (CompareInts(words[0], value) || CompareInts(words[1], value)) return true;
                return false;
            }
            else if (s.Contains("&"))
            {
                Console.WriteLine("& Detected");
                if (CompareInts(words[0], value) && CompareInts(words[1], value)) return true;
                return false;
            }
            return CompareInts(s, value);
        }

        public static bool CompareInts(string s, int value)
        {
            //True = does meet criteria
            //False = does not meet criteria
            int stringValAsInt = Int32.Parse(Regex.Match(s, @"\d+").Value);
            switch (s)
            {
                case string a when a.Contains("<="):
                    //Console.WriteLine("Property is: <=");
                    if (value <= stringValAsInt)
                    {
                        return true;
                    }
                    return false;
                case string b when b.Contains("<"):
                    //Console.WriteLine("Property is: <");
                    if (value < stringValAsInt)
                    {
                        return true;
                    }
                    return false;
                case string c when c.Contains("=="):
                    //Console.WriteLine("Property is: ==");
                    if (value == stringValAsInt)
                    {
                        return true;
                    }
                    return false;
                case string d when d.Contains(">="):
                    //Console.WriteLine("Property is: >=");
                    if (value >= stringValAsInt)
                    {
                        return true;
                    }
                    return false;
                case string e when e.Contains(">"):
                    //Console.WriteLine("Property is: >");
                    if (value > stringValAsInt)
                    {
                        return true;
                    }
                    return false;
                default:
                    Console.WriteLine("Something fucked up");
                    return false;
            }

        }

    }
}
