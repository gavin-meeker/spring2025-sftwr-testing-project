namespace SoftwareTestingProject;

public class TestMethodContainer
{
    private readonly Dictionary<string, MethodResult> _testMethods = new();
    private bool _ranSBFLs { get; set; }

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