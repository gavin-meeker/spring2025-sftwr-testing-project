namespace SoftwareTestingProject;

public class MethodResult
{
    public MethodResult(string methodName, bool isTestPassing)
    {
        MethodName = methodName;
        IncrementTestCounts(isTestPassing);
    }

    public string MethodName { get; init; }

    public int PassingTestCount { get; private set; }
    public int FailingTestCount { get; private set; }
    public decimal TarantulaResult { get; private set; }
    public decimal SbiResult { get; private set; }
    public decimal JaccardResult { get; private set; }
    public decimal OchiaiResult { get; private set; }

    public void CalculateAllSBFLs(int totalPassingTests, int totalFailingTests)
    {
        TarantulaResult = SbflMethods.CalculateTarantula(this, totalPassingTests, totalFailingTests);
        SbiResult = SbflMethods.CalculateSbi(this);
        JaccardResult = SbflMethods.CalculateJaccard(this, totalFailingTests);
        OchiaiResult = SbflMethods.CalculateOchiai(this, totalFailingTests);
    }

    public void IncrementTestCounts(bool isTestPassing)
    {
        if (isTestPassing)
            PassingTestCount++;
        else
            FailingTestCount++;
    }
}