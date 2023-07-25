using Xunit.Abstractions;

namespace smallestSufficientTeam;

public class SmallestSufficientTeamTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly IList<IList<string>> _people = new List<IList<string>>();
    private readonly IList<string> _reqSkills = new List<string>();
    private readonly Solution _solution;


    public SmallestSufficientTeamTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _solution = new Solution();
    }

    [Fact]
    public void Test_only_a_skill_and_one_person()
    {
        _reqSkills.Add("csharp");
        AddPersonWith("csharp");

        var result = _solution.SmallestSufficientTeam(_reqSkills.ToArray(), _people);
        Assert.Equal(new[] { 0 }, result);
    }

    [Fact] public void Test_only_2_skill_and_2_persons_everyone_with_one_of_the_skill()
    {
        _reqSkills.Add("csharp");
        _reqSkills.Add("javascript");
        
        
        AddPersonWith("csharp");
        AddPersonWith("javascript");

        var result = _solution.SmallestSufficientTeam(_reqSkills.ToArray(), _people);
        Assert.Equal(new[] { 0,1 }, result);
    }
    
    [Fact] public void Test_only_2_skill_and_3_persons_with_one_persons_without_the_skill()
    {
        _reqSkills.Add("csharp");
        _reqSkills.Add("javascript");
        
        AddPersonWith("csharp");
        AddPersonWith("javascript");
        AddPersonWith("somethingelse");

        var result = _solution.SmallestSufficientTeam(_reqSkills.ToArray(), _people);
        Assert.Equal(new[] { 0,1 }, result);
    }
    
    [Fact] public void Test_only_2_skill_first_person_has_both_skills_and_the_third_one_has_another()
    {
        _reqSkills.Add("csharp");
        _reqSkills.Add("javascript");
        
        AddPersonWith("csharp", "javascript");
        AddPersonWith("javascript");
        AddPersonWith("somethingelse");

        var result = _solution.SmallestSufficientTeam(_reqSkills.ToArray(), _people);
        Assert.Equal(new[] { 0 }, result);
    }


    [Fact] public void Test_only_2_skill_first_and_second_person_has_the_skills_but_a_third_person_has_all_skills()
    {
        _reqSkills.Add("csharp");
        _reqSkills.Add("javascript");
        
        AddPersonWith("csharp");
        AddPersonWith("javascript");
        AddPersonWith("csharp","javascript");

        var result = _solution.SmallestSufficientTeam(_reqSkills.ToArray(), _people);
        Assert.Equal(new[] { 2 }, result);
    }

    private void AddPersonWith(params string[] skills)
    {
        _people.Add(skills.ToList());
    }

    [Fact]
    public void AcceptanceTest1()
    {
        var solution = new Solution();
        string[] req_skills = { "java", "nodejs", "reactjs" };
        IList<IList<string>> people = new List<IList<string>>
        {
            new List<string> { "java" },
            new List<string> { "nodejs" },
            new List<string> { "nodejs", "reactjs" }
        };
        var smallestSufficientTeam = solution.SmallestSufficientTeam(req_skills, people);
        Assert.Equal(new[] { 0, 2 }, smallestSufficientTeam);
    }
    
    [Fact]
    public void AcceptanceTest2()
    {
        var solution = new Solution();
        string[] req_skills = { "algorithms", "math", "java", "reactjs", "csharp", "aws" };
        var people = new List<IList<string>>
        {
            new List<string> { "algorithms", "math", "java" },
            new List<string> { "algorithms", "math", "reactjs" },
            new List<string> { "java", "csharp", "aws" },
            new List<string> { "reactjs", "csharp" },
            new List<string> { "csharp", "math" },
            new List<string> { "aws", "java" }
        };
        var smallestSufficientTeam = solution.SmallestSufficientTeam(req_skills, people);
        Assert.Equal(new[] { 1, 2 }, smallestSufficientTeam);
    }
    
    [Fact]
    public void AcceptanceTest3()
    {
        var solution = new Solution();
        string[] req_skills = { "gvp","jgpzzicdvgxlfix","kqcrfwerywbwi","jzukdzrfgvdbrunw","k" };
        var people = new List<IList<string>>
        {
            /*  0 */ new List<string>(),
            /*  1 */ new List<string>(),
            /*  2 */ new List<string>(),
            /*  3 */ new List<string>(),
            /*  4 */ new List<string> { "jgpzzicdvgxlfix" },
            /*  5 */ new List<string> { "jgpzzicdvgxlfix", "k" },
            /*  6 */ new List<string> { "jgpzzicdvgxlfix", "k", "kqcrfwerywbwi" },
            /*  7 */ new List<string> { "gvp" },
            /*  8 */ new List<string> { "jzukdzrfgvdbrunw" },
            /*  9 */ new List<string> { "gvp", "kqcrfwerywbwi" }
        };
        
        var smallestSufficientTeam = solution.SmallestSufficientTeam(req_skills, people);
        Assert.Equal(new[] { 5, 8, 9 }, smallestSufficientTeam);
    }
}