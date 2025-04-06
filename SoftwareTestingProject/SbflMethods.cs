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

        if (radicand == 0) return 0;

        return (decimal)(method.FailingTestCount / Math.Sqrt(radicand));
    }

    public static decimal CalculateJaccard(MethodResult method, int totalFailingTests)
    {
        if (totalFailingTests + method.PassingTestCount == 0) return 0;

        return (decimal)method.FailingTestCount / (totalFailingTests + method.PassingTestCount);
    }

    public static decimal CalculateSbi(MethodResult method)
    {
        if (method.FailingTestCount + method.PassingTestCount == 0) return 0;

        return (decimal)method.FailingTestCount / (method.FailingTestCount + method.PassingTestCount);
    }

    public static decimal CalculateTarantula(MethodResult method, int totalPassingTests, int totalFailingTests)
    {
        if (totalPassingTests == 0 || totalFailingTests == 0) return 0;

        var failingTestCountRatio = (decimal)method.FailingTestCount / totalFailingTests;
        var passingTestCountRatio = (decimal)method.PassingTestCount / totalPassingTests;

        return failingTestCountRatio / (failingTestCountRatio + passingTestCountRatio);
    }
}