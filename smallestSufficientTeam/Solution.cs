using Xunit.Abstractions;

namespace smallestSufficientTeam;

public class Solution
{
    List<int> resultedTeam = new();
    private int _reqSkills;
    private int[] personSkills;
    private List<int> currentTeam;

    public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people)
    {
        var requiredSkillsDictionary 
            = req_skills.Select((s, i) => (s, i)).ToDictionary(tuple => tuple.s, tuple => tuple.i);
        
        personSkills = GetPeopleSkillsMask(people, requiredSkillsDictionary);
        _reqSkills = (1<<req_skills.Length) -1;
        currentTeam = new List<int>();
        FindTeam(0, 0);
        return resultedTeam.ToArray();
    }

    void FindTeam(int teamSkills, int personIndex)
    {
        if (resultedTeam.Count > 0 
            && currentTeam.Count >= resultedTeam.Count - 1 
            || personIndex == personSkills.Length) 
            //early stopping
        {
            return;
        }

        //taking current person into team
        currentTeam.Add(personIndex);

        if( (teamSkills|personSkills[personIndex]) == _reqSkills)
        {
            resultedTeam = new List<int>(currentTeam);
            currentTeam.RemoveAt(currentTeam.Count-1);
            return;
        }

        if( (teamSkills|personSkills[personIndex]) > teamSkills){
            FindTeam(teamSkills|personSkills[personIndex], personIndex+1 );
        }

        currentTeam.RemoveAt(currentTeam.Count-1);

        FindTeam(teamSkills, personIndex+1 );
    }

    private int[] GetPeopleSkillsMask(IList<IList<string>> people, Dictionary<string,int> dictionary)
    {
        return people.Select(list => GetSkillMask(list, dictionary)).ToArray();
    }

    private int GetSkillMask(IList<string> list, Dictionary<string,int> dictionary)
    {
        return list.Where(dictionary.ContainsKey)
            .Aggregate(0, (current, s) => current | 1 << dictionary[s]);
    }
}