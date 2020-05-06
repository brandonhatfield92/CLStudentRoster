using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace _BrandonHatfield_.CodeLou.ExcerciseProject
{
    class Program
    {


        private static Dictionary<int, Student> students;
        

        static void Main(string[] args)
        {
            students = Student.load().ToDictionary(s => s.StudentId, s => s);

            int userInput = 0;

            do 
            {

                try
                {
                    userInput = MainMenu();

                    switch (userInput)
                    {
                        case 1:
                            AddStudent();
                            break;

                        case 2:
                            ListStudents();
                            break;

                        case 3:
                            FindStudents();
                            break;

                        case 4:
                            Console.WriteLine("Exiting. Hit any key now.");
                            break;


                        default:
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine(" Error: Invalid Choice");
                            System.Threading.Thread.Sleep(1000);
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(" Unexpected Error:");
                    Console.WriteLine(e);
                    System.Threading.Thread.Sleep(2000);
                }

            }
            while (userInput != 4);

            Student.saveToFile(students.Values.ToList());
    }

    private static void AddStudent()
    {
            int studentId;
            do
            {
                Console.WriteLine("Enter Student Id");
                studentId = Convert.ToInt32(Console.ReadLine());
                if(students.ContainsKey(studentId))
                    Console.WriteLine("Students cannot have the same ID number, please try again.");

            }
            while (students.ContainsKey(studentId));
            
        Console.WriteLine("Enter First Name");
        var studentFirstName = Console.ReadLine();
        Console.WriteLine("Enter Last Name");
        var studentLastName = Console.ReadLine();
        Console.WriteLine("Enter Class Name");
        var className = Console.ReadLine();
        Console.WriteLine("Enter Last Class Completed");
        var lastClass = Console.ReadLine();
        DateTimeOffset lastCompletedOn;
        DateTimeOffset startDate;
        do
        {
            Console.WriteLine("Enter Last Class Completed Date in format MM/DD/YYYY");
            lastCompletedOn = DateTimeOffset.Parse(Console.ReadLine());
            Console.WriteLine("Enter Start Date in format MM/DD/YYYY");
            startDate = DateTimeOffset.Parse(Console.ReadLine());
                if (startDate < lastCompletedOn)
                    Console.WriteLine("Start date is before date of last completed class, please try again.");
        } while (startDate < lastCompletedOn);


        var studentRecord = new Student();

        studentRecord.StudentId = studentId;
        studentRecord.FirstName = studentFirstName;
        studentRecord.LastName = studentLastName;
        studentRecord.ClassName = className;
        studentRecord.StartDate = startDate;
        studentRecord.LastClassCompleted = lastClass;
        studentRecord.LastClassCompletedOn = lastCompletedOn;


        students.Add(studentRecord.StudentId, studentRecord);
    }

    public static void ListStudents()
    {
        Console.WriteLine($"Student Id | Name |  Class ");
        foreach (var studentRecord in students.Values)
        {
            Console.WriteLine($"{studentRecord.StudentId} | {studentRecord.FirstName} {studentRecord.LastName}");

        }

    }

    private static void FindStudents()
    {
            var test = students.Values.ToList();

            Console.WriteLine("Enter Student Name: ");
            var userEntry = Console.ReadLine();
            foreach (var match in test.Where(s => userEntry.Equals(s.FirstName,StringComparison.CurrentCultureIgnoreCase)))
            {
                Console.WriteLine($"{match.StudentId} | {match.FirstName} {match.LastName}");
                Console.WriteLine(match.FirstName);
            }
           



    }
    private static int MainMenu()
    {
        Console.WriteLine("1. New Student");
        Console.WriteLine("2. List Students");
        Console.WriteLine("3. Find Students");
        Console.WriteLine("4. Exit");
        Console.WriteLine();
        Console.Write("Enter your selection ");
        var selection = Console.ReadLine();

        Console.WriteLine($" You typed {selection}:");
        return Int32.Parse(selection);
    }
}
}