namespace SoftwareTestingProject;

public class MethodResult
{
    public int PassingTestCount { get; set; } = 0;
    public int FailingTestCount { get; set; } = 0;
    public decimal TarantulaResult { get; }
    public decimal SBIResult { get; }
    public decimal JaccardResult { get; }
    public decimal OchiaiResult { get; }

    public void CalculateAllSBFLs(int totalPassingTests, int totalFailingTests)
    {
        CalculateTarantulaResult(totalPassingTests, totalPassingTests);
        CalculateSBIResult(totalPassingTests, totalPassingTests);
        CalculateJaccardResult(totalPassingTests, totalPassingTests);
        CalculateOchiaiResult(totalPassingTests, totalPassingTests);
    }

    private void CalculateOchiaiResult(int totalPassingTests, int totalFailingTests)
    {
        throw new NotImplementedException();
    }

    private void CalculateJaccardResult(int totalPassingTests, int totalFailingTests)
    {
        throw new NotImplementedException();
    }

    private void CalculateSBIResult(int totalPassingTests, int totalFailingTests)
    {
        throw new NotImplementedException();
    }

    private void CalculateTarantulaResult(int totalPassingTests, int totalFailingTests)
    {
        throw new NotImplementedException();
    }
}