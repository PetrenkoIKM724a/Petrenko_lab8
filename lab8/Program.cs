using System;
using System.Collections.Generic;
using System.Linq;

public interface IProject
{
    string Name { get; set; }
    DateTime Deadline { get; set; }
    bool IsCompleted { get; set; }
}

public class BuildingProject : IProject
{
    public string Name { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }

    public BuildingProject(string name, DateTime deadline, bool isCompleted)
    {
        Name = name;
        Deadline = deadline;
        IsCompleted = isCompleted;
    }

    public override string ToString()
    {
        string status = IsCompleted ? "Завершено" : "В процесі";
        return $"Проєкт: {Name}, Дедлайн: {Deadline.ToShortDateString()}, Статус: {status}";
    }
}

public class ConstructionCompany<T> where T : IProject
{
    private List<T> projects = new List<T>();

    public void AddProject(T project)
    {
        projects.Add(project);
    }

    public void SortByDeadline()
    {
        projects.Sort((p1, p2) => p1.Deadline.CompareTo(p2.Deadline));
        Console.WriteLine("\n--- Проєкти відсортовано за термінами ---");
    }

    public void PrintProjectsByStatus(bool isCompleted)
    {
        string statusText = isCompleted ? "завершені" : "незавершені";
        Console.WriteLine($"\n--- Проєкти зі статусом: {statusText} ---");

        var filteredProjects = projects.Where(p => p.IsCompleted == isCompleted).ToList();

        if (filteredProjects.Count == 0)
        {
            Console.WriteLine("Проєктів з таким статусом не знайдено.");
        }
        else
        {
            foreach (var project in filteredProjects)
            {
                Console.WriteLine(project.ToString());
            }
        }
    }

    public void PrintAllProjects()
    {
        Console.WriteLine("\n--- Всі проєкти компанії ---");
        foreach (var project in projects)
        {
            Console.WriteLine(project.ToString());
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;
        ConstructionCompany<BuildingProject> company = new ConstructionCompany<BuildingProject>();

        company.AddProject(new BuildingProject("Житловий комплекс 'Мрія'", new DateTime(2027, 5, 20), false));
        company.AddProject(new BuildingProject("Торговий центр", new DateTime(2025, 12, 1), true));
        company.AddProject(new BuildingProject("Ремонт школи", new DateTime(2026, 11, 15), false));

        company.PrintAllProjects();

        company.SortByDeadline();
        company.PrintAllProjects();

        company.PrintProjectsByStatus(true);
        company.PrintProjectsByStatus(false);
    }
}