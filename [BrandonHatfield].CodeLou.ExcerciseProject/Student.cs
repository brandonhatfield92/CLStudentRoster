using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Student
{
  
        
    public static List<Student> load()
    {
        if (!File.Exists("students.json")) return new List<Student>();

        List<Student> students = JsonConvert.DeserializeObject<List<Student>>(File.ReadAllText(@"students.json"));
        return students;
    }

    public static void saveToFile(List<Student> students)
    {
        var json = JsonConvert.SerializeObject(students);
        File.WriteAllText("students.json", json);
    }
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ClassName { get; set; }
    public DateTimeOffset StartDate{ get; set; }
    public string LastClassCompleted { get; set; }
    public DateTimeOffset LastClassCompletedOn { get; set; }


    

}