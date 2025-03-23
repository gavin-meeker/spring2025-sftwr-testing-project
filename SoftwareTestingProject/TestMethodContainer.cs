namespace SoftwareTestingProject;

public class TestMethodContainer
{
    private readonly Dictionary<string, MethodResult> _testMethods = new();

    public void CalculateAllMethodSBFLs(int totalPassingTests, int totalFailingTests)
    {
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