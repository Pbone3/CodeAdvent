using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace CodeAdvent
{
    static class Program
    {
        static string session;

        static void Main(string[] args)
        {
            int i = args.ToList().FindIndex(s => s == "-session");
            if (i == -1) throw new Exception("No session provided! Find the session id by downloading your cookies.");
            session = args[i + 1];

            Start:
                Console.WriteLine("Which day would you like to solve?");
                Console.Write("Type any number and press enter: ");

                bool num = int.TryParse(Console.ReadLine(), out int day);
                if (!num || day < 1 || day > 25)
                {
                    Console.WriteLine("Invalid number! Please try again.");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }

            Console.Clear();
            Console.WriteLine(BuildHeader(day));
            Console.WriteLine(Lang.DayDesc[day]);
            Console.WriteLine(CalcDay(day));
            Console.ReadKey();
        }

        static string BuildHeader(int day) =>
            $"========== DAY {day} ==========";

        static string CalcDay(int day)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://adventofcode.com/2020/day/" + day + "/input");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("session", session, "/", ".adventofcode.com"));
            request.Method = "GET";
            string input = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            string output = "";

            switch (day)
            {
                case 1:                    
                    string[] numbers = input.Split("\n");
                    int[] values = new int[numbers.Length - 1];

                    for (int i = 0; i < values.Length; i++)
                        values[i] = int.Parse(numbers[i]);

                    bool p1Completed = false;
                    bool p2Completed = false;
                    foreach (int a in values)
                    {
                        foreach (int b in values)
                        {
                            if (a + b == 2020)
                            {
                                if (!p1Completed)
                                    output += $"Part 1: {a * b} ";
                                p1Completed = true;
                            }

                            if (!p2Completed)
                            {
                                foreach (int c in values)
                                {
                                    if (a + b + c == 2020)
                                    {
                                        output += $"Part 2: {a * b * c} ";
                                        p2Completed = true;
                                    }
                                }
                            }

                            if (p1Completed && p2Completed) break;
                        }
                    }

                    break;

                case 2:
                    string[] passwords = input.Split("\n");
                    int matches = 0;
                    int matches2 = 0;

                    foreach (string s in passwords)
                    {
                        if (s.Length < 5) continue;
                        Password password = new Password(s);
                        if (password.Valid())
                            matches++;
                        if (password.Valid2())
                            matches2++;
                    }

                    output = $"Problem 2: {matches2} Problem 1: {matches}";
                    break;

            }
            return output;
        }
    }
}
