namespace SoftwareTestingProject;

public class MethodResult
{
    public int PassingTestCount { get; set; }
    public int FailingTestCount { get; set; }
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
}