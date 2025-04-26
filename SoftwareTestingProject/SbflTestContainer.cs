namespace SoftwareTestingProject;

public class SbflTestContainer
{
    private readonly Dictionary<string, MethodResult> _testMethodDict = new();
    public int TotalFailingTests { get; set; }
    public int TotalPasssingTests { get; set; }

    public void IncrementTotalTestCounts(bool isTestResultPassing)
    {
        if (isTestResultPassing)
            TotalPasssingTests++;
        else
            TotalFailingTests++;
    }

    public List<SuspiciousMethod> GetAllSuspiciousMethods()
    {
        List<SuspiciousMethod> suspiciousMethods = new();

        foreach (var method in _testMethodDict)
        {
            if (method.Value.TarantulaResult != 0)
                suspiciousMethods.Add(new SuspiciousMethod
                {
                    MethodName = method.Key, Formula = SblfEnum.Tarantula, Suspicion = method.Value.TarantulaResult
                });

            if (method.Value.SbiResult != 0)
                suspiciousMethods.Add(new SuspiciousMethod
                    { MethodName = method.Key, Formula = SblfEnum.Sbi, Suspicion = method.Value.SbiResult });

            if (method.Value.JaccardResult != 0)
                suspiciousMethods.Add(new SuspiciousMethod
                    { MethodName = method.Key, Formula = SblfEnum.Jaccard, Suspicion = method.Value.JaccardResult });

            if (method.Value.OchiaiResult != 0)
                suspiciousMethods.Add(new SuspiciousMethod
                    { MethodName = method.Key, Formula = SblfEnum.Ochiai, Suspicion = method.Value.OchiaiResult });
        }

        return suspiciousMethods;
    }


    public void LogSpecificMethodSbflResult(string methodName)
    {
        if (_testMethodDict.TryGetValue(methodName, out var foundMethod))
        {
            Console.WriteLine($"Method: {methodName}");
            Console.WriteLine($"{nameof(foundMethod.PassingTestCount)}: {foundMethod.PassingTestCount}");
            Console.WriteLine($"{nameof(foundMethod.FailingTestCount)}: {foundMethod.FailingTestCount}");
            Console.WriteLine($"{nameof(foundMethod.TarantulaResult)}: {foundMethod.TarantulaResult}");
            Console.WriteLine($"{nameof(foundMethod.JaccardResult)}: {foundMethod.JaccardResult}");
            Console.WriteLine($"{nameof(foundMethod.OchiaiResult)}: {foundMethod.OchiaiResult}");
            Console.WriteLine($"{nameof(foundMethod.SbiResult)}: {foundMethod.SbiResult}");
        }

        Console.WriteLine($"Method {methodName} was not found.");
    }

    public void PerformTestAnalysis()
    {
        foreach (var method in _testMethodDict.Values) method.CalculateAllSBFLs(TotalPasssingTests, TotalFailingTests);
    }

    public void IncrementSpecificMethodCounts(string methodName, bool isTestResultPassing)
    {
        if (!_testMethodDict.TryAdd(methodName, new MethodResult(methodName, isTestResultPassing)))
            _testMethodDict[methodName].IncrementTestCounts(isTestResultPassing);
    }
}