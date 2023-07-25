using Xunit.Abstractions;

namespace smallestSufficientTeam;

public class Person
{
    public Person(IList<string> skills, int index)
    {
        Skills = skills;
        Index = index;
    }

    public IList<string> Skills { get; }
    public int Index { get; }
}

public class Team
{
    public IList<Person> Persons = new List<Person>();
    public HashSet<string> Skills = new();

    public Team(Team currentTeam)
    {
        Persons = new List<Person>(currentTeam.Persons);
        ComputeSkills();
    }

    public Team()
    {
    }

    private void ComputeSkills()
    {
        Skills = new HashSet<string>();
        foreach (var p in Persons)
        {
            foreach (var skill in p.Skills)
            {
                Skills.Add(skill);
            }
        }
    }

    public int PersonsCount => Persons.Count;

    public void Add(Person person)
    {
        Persons.Add(person);
        foreach (var skill in person.Skills)
        {
            Skills.Add(skill);
        }
    }

    public int[] ToArray()
    {
        return this.Persons.Select(person => person.Index).ToArray();
    }

    public bool HaveTheRequiredSkills(HashSet<string> reqSkills)
    {
        return reqSkills.All(Skills.Contains);
    }

    public void RemoveLastPerson()
    {
        Persons.RemoveAt(Persons.Count - 1);
        ComputeSkills();
    }

    public bool IsOnePersonLessThen(Team resultedTeam)
    {
        if (resultedTeam.PersonsCount == 0)
        {
            return false;
        }

        return PersonsCount + 1 == resultedTeam.PersonsCount;
    }
}

public class Solution
{
    Team resultedTeam = new();
    private HashSet<string> _reqSkills;
    private Person[] persons;
    private Team currentTeam;

    public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people)
    {
        persons = people.Select((list, index) => new Person(list, index)).ToArray();
        _reqSkills = req_skills.ToHashSet();
        currentTeam = new Team();
        FindTeam(0);
        
        return resultedTeam.ToArray();
    }

    void FindTeam(int personIndex)
    {
        if (persons.Length == personIndex)
        {
            return;
        }

        if (currentTeam.IsOnePersonLessThen(resultedTeam))
        {
            return;
        }

        currentTeam.Add(persons[personIndex]);
        
        if(currentTeam.HaveTheRequiredSkills(_reqSkills))
        {
            resultedTeam = new Team(currentTeam);
            currentTeam.RemoveLastPerson();
            return;
        }

        FindTeam(personIndex+1);
        currentTeam.RemoveLastPerson();
        FindTeam(personIndex+1);
    }
}