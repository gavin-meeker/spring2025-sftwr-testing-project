namespace SoftwareTestingProject;

public class TestMethodContainer
{
    private readonly Dictionary<string, MethodResult> _testMethods = new();
    private bool _ranSBFLs { get; set; }

    public void PrintMethodResult(string methodName)
    {
        if (!_testMethods.ContainsKey(methodName))
        {
            Console.WriteLine($"Method {methodName} was not found.");
            return;
        }

        var foundMethod = _testMethods[methodName];

        Console.WriteLine($"Method: {methodName}");
        Console.WriteLine($"{nameof(foundMethod.PassingTestCount)}: {foundMethod.PassingTestCount}");
        Console.WriteLine($"{nameof(foundMethod.FailingTestCount)}: {foundMethod.FailingTestCount}");
        Console.WriteLine($"{nameof(foundMethod.TarantulaResult)}: {foundMethod.TarantulaResult}");
        Console.WriteLine($"{nameof(foundMethod.JaccardResult)}: {foundMethod.JaccardResult}");
        Console.WriteLine($"{nameof(foundMethod.OchiaiResult)}: {foundMethod.OchiaiResult}");
        Console.WriteLine($"{nameof(foundMethod.SbiResult)}: {foundMethod.SbiResult}");
    }

    public List<(string methodName, decimal suspicion)> GetSuspiciousMethods(SblfEnum sblfEnum)
    {
        List<(string methodName, decimal suspicion)> suspiciousMethods = new();
        foreach (var method in _testMethods)
            if (method.Value.TarantulaResult != 0)
                suspiciousMethods.Add((method.Key, method.Value.TarantulaResult));
        return suspiciousMethods;
    }

    public void CalculateAllMethodSBFLs(int totalPassingTests, int totalFailingTests)
    {
        if (_ranSBFLs) throw new InvalidOperationException("Method container is already ran");
        _ranSBFLs = true;
        foreach (var method in _testMethods.Values) method.CalculateAllSBFLs(totalPassingTests, totalFailingTests);
    }

    public void IncrementTestMethod(string methodName, bool testResultIsPassing)
    {
        if (_testMethods.ContainsKey(methodName))
        {
            if (testResultIsPassing)
                _testMethods[methodName].PassingTestCount++;
            else
                _testMethods[methodName].FailingTestCount++;
        }
        else
        {
            if (testResultIsPassing)
                _testMethods.Add(methodName, new MethodResult { PassingTestCount = 1, FailingTestCount = 0 });
            else
                _testMethods.Add(methodName, new MethodResult { PassingTestCount = 0, FailingTestCount = 1 });
        }
    }
}