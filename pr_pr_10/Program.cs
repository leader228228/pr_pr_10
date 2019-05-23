using System;
using System.Collections.Generic;
using System.Text;

namespace pr_pr_10
{
    class Program
    {
        static void Main(string[] args)
        {
            task_2();
            Console.Read();
        }

        private static void task_1()
        {
            Bus[] buses = new Bus[3];
            buses[0] = new Bus("Bohdan", 40, 150, 3000000);
            buses[1] = new Bus("LIAZ", 45, 165, 5000000);
            buses[2] = buses[0] + buses[1];
            for (int i = 0; i < buses.Length - 1; i++)
            {
                Console.WriteLine(buses[i + 1] + " is " + ((buses[i + 1] >= buses[i] ? "greater than or equal to" : "less than or equal to")) + " " + buses[i]);
            }
        }

        private static void task_2()
        {
            Student student = new Student(new Person("Vasya", "Pupkin", System.DateTime.Now), Education.Specialist);
            Console.WriteLine("\nStudent ToShortString():");
            Console.WriteLine(student.ToShortString());
            Console.WriteLine("\nEducation enum:");
            Console.WriteLine(Education.Specialist);
            Console.WriteLine(Education.Bachelor);
            Console.WriteLine(Education.SecondEducation);
            student.Education = Education.Bachelor;
            student.GroupID = 100;
            student.Person = new Person("Ivan", "Schmidt", System.DateTime.Now.AddYears(100));

            List<Exam> exs = new List<Exam>();
            exs.Add(new Exam("Math", 5, new DateTime(2010, 9, 1)));
            exs.Add(new Exam("History", 4, new DateTime(2010, 9, 3)));
            exs.Add(new Exam("Geography", 5, new DateTime(2010, 9, 5)));

            student.Exams = exs;
            Console.WriteLine("\nStudent:");
            Console.WriteLine(student.ToString());
            student.AddExams(new Exam[] { new Exam("Philosophy", 5, new DateTime(2010, 9, 11))});
            Console.WriteLine("\nStudent after adding exams:");
            Console.WriteLine(student.ToString());
        }
    }

    class Person
    {
        private string name;
        private string surname;
        private System.DateTime birthDate;

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public System.DateTime BirhtDate { get => birthDate; set => birthDate = value; }
        public int BirthYear { get => BirhtDate.Year; set => birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }

        public Person()
        {
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
        }

        public override string ToString() => "Person{Name=" + name + ", Surname=" + surname + ", birthDate=" + birthDate.ToString() + '}';

        public string ToShortString() => "Person{Name=" + name + ", Surname=" + surname + '}';
    }

    class Bus
    {

        public Bus(string model, int maxPassengers, double maxSpeed, double price)
        {
            Model = model;
            MaxPassengers = maxPassengers;
            MaxSpeed = maxSpeed;
            Price = price;
        }

        public Bus()
        {
        }

        public string Model { get; set; }

        public int MaxPassengers { get; set; }

        public double MaxSpeed { get; set; }

        public double Price { get; set; }

        public override string ToString() => "Bus{Model=" + Model + ", MaxPassengers=" + MaxPassengers + ", MaxSpeed=" + MaxSpeed + ", Price=" + Price + '}';

        public static bool operator ==(Bus a, Bus b) => (!(a > b)) && (!(a < b));

        public static bool operator !=(Bus a, Bus b) => !(a == b);

        public static Bus operator +(Bus a, Bus b) => new Bus(a.Model + "/" + b.Model, a.MaxPassengers + b.MaxPassengers, (a.MaxSpeed + b.MaxSpeed) / 2, (a.Price + b.Price) / 2);

        public static bool operator >(Bus a, Bus b) => (string.Compare(a.Model, b.Model) > 0) && a.Price > b.Price && a.MaxSpeed > b.MaxSpeed && a.MaxPassengers > b.MaxPassengers;

        public static bool operator <(Bus a, Bus b) => (string.Compare(a.Model, b.Model) < 0) && a.Price < b.Price && a.MaxSpeed < b.MaxSpeed && a.MaxPassengers < b.MaxPassengers;

        public static bool operator >=(Bus a, Bus b) => a > b || a == b;

        public static bool operator <=(Bus a, Bus b) => a < b || a == b;

    }

    enum Education
    {
        Specialist, Bachelor, SecondEducation
    }

    class Exam
    {
        public string SubjectName { get; set; }
        public int Mark { get; set; }
        public System.DateTime ExamDate { get; set; }

        public override string ToString() => "Exam{SubjectName=" + SubjectName + ", Mark=" + Mark + ", ExamDate=" + ExamDate.ToString() + '}';

        public Exam()
        {
        }

        public Exam(string subjectName, int mark, DateTime examDate)
        {
            SubjectName = subjectName;
            Mark = mark;
            ExamDate = examDate;
        }
    }

    class Student
    {
        private Person person;
        private Education education;
        private int groupID;
        private List<Exam> exams = new List<Exam>();

        public Student(Person person, Education education)
        {
            this.person = person;
            this.education = education;
        }

        public Student()
        {
        }

        public Person Person { get => person; set => person = value; }
        public Education Education { get => education; set => education = value; }
        public int GroupID { get => groupID; set => groupID = value; }
        public List<Exam> Exams { get => exams; set => exams = value; }
        public double AVG
        {
            get
            {
                double sum = 0;
                foreach(Exam exam in exams)
                {
                    sum += exam.Mark;
                }
                return sum / Math.Max(1, exams.Count);
            }
        }

        public void AddExams(Exam [] exams)
        {
            foreach(Exam exam in exams)
            {
                this.exams.Add(exam);
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("Student{person=" + person.ToString() + ", education=" + education + ", groupID=" + groupID + ", exams=");
            foreach(Exam exam in exams)
            {
                stringBuilder.Append(exam.ToString()).Append(", ");
            }
            return stringBuilder.ToString().Substring(0, stringBuilder.Length - 2) + '}';
        }

        public string ToShortString() => "Student{person=" + person.ToString() + ", education=" + education + ", groupID=" + groupID + ", AVG=" + AVG + '}';
    }
}
