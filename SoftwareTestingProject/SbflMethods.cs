namespace SoftwareTestingProject;

public sealed class SbflMethods
{
    private SbflMethods()
    {
    }

    public static decimal CalculateOchiai(MethodResult method, int totalFailingTests)
    {
        decimal passFailSum = method.PassingTestCount + method.FailingTestCount;
        var radicand = (double)(totalFailingTests * passFailSum);
        return (decimal)(method.FailingTestCount / Math.Sqrt(radicand));
    }

    public static decimal CalculateJaccard(MethodResult method, int totalFailingTests)
    {
        return (decimal)method.FailingTestCount / (totalFailingTests + method.PassingTestCount);
    }

    public static decimal CalculateSbi(MethodResult method)
    {
        return (decimal)method.FailingTestCount / (method.FailingTestCount + method.PassingTestCount);
    }

    public static decimal CalculateTarantula(MethodResult method, int totalPassingTests, int totalFailingTests)
    {
        var failingTestCountRatio = (decimal)method.FailingTestCount / totalFailingTests;
        var passingTestCountRatio = (decimal)method.PassingTestCount / totalPassingTests;

        return failingTestCountRatio / (failingTestCountRatio + passingTestCountRatio);
    }
}