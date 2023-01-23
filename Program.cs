// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

public class Payment:IComparable<Payment>
{
    public DateTime PaidOn { get; set; }
    public int amount { get; set; }

    public int CompareTo(Payment? other)
    {
        return PaidOn.CompareTo(other.PaidOn);
    }
}




public class Program1
{ 
    public static void Main()
    {

        string incomePath = @"C:\Payments2\income.txt"; //Directory where the file will be created
        string outcomePath = @"C:\Payments2\outcome.txt"; //Directory where the file will be created

        string fileName = @"C:\Payments2\payments.json"; //Path for the json
        string jsonString = File.ReadAllText(fileName);
        var list = JsonConvert.DeserializeObject<List<Payment>>(jsonString);
        list.Sort(); //Sort works :)

        var incomeTask = Task.Run(() =>
        {
            int count = 0;
            int i = 0;
            foreach (var item in list)
            {
                if (item.amount > 0)
                {
                    count++;
                }
            }
            string[] incomeLines = new string[count];
            foreach (var item in list)
            {
                if (item.amount > 0)
                {
                    incomeLines[i] = item.amount.ToString() + " " + item.PaidOn.ToString(); //I Added the date for testing purposes
                    i++;
                }
            }
            File.WriteAllLines(incomePath, incomeLines);
        });

        var outcomeTask = Task.Run(() =>
        {
            int count = 0;
            int i = 0;
            foreach (var item in list)
            {
                if (item.amount < 0)
                {
                    count++;
                }
            }
            string[] outcomeLines = new string[count];
            foreach (var item in list)
            {
                if (item.amount < 0)
                {
                    outcomeLines[i] = item.amount.ToString() + " " + item.PaidOn.ToString(); ////I Added the date for testing purposes
                    i++;
                }
            }
            File.WriteAllLines(outcomePath, outcomeLines);

        });

        Task.WaitAll(incomeTask, outcomeTask);
        Console.WriteLine("Files Created! Please Check Your Selected Directory!");

    }
}




