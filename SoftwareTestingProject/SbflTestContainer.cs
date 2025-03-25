namespace SoftwareTestingProject;

public class SbflTestContainer
{
    public SbflTestContainer()
    {
        ReadOverallTestResults();
        TestMethodContainer.CalculateAllMethodSBFLs(TotalPasssingTests, TotalFailingTests);
    }

    public TestMethodContainer TestMethodContainer { get; } = new();

    public int TotalFailingTests { get; set; }
    public int TotalPasssingTests { get; set; }

    public void IncrementTotalTestCounts(bool isTestResultPassing)
    {
        if (isTestResultPassing)
            TotalPasssingTests++;
        else
            TotalFailingTests++;
    }


    private void ReadOverallTestResults()
    {
        var testDirectoryName = "NewCoverageData";
        var workingDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
        var testFiles = Directory.GetFiles(Path.GetFullPath(Path.Combine(workingDirectory, testDirectoryName)));

        foreach (var file in testFiles) ReadTestResults(file);
    }

    private void ReadTestResults(string fileName)
    {
        var fileLines = File.ReadLines(fileName);
        var firstLine = fileLines.First();
        var overallTestResultIsPassing = Convert.ToBoolean(firstLine.Split(' ')[1]);

        if (overallTestResultIsPassing)
            TotalPasssingTests++;
        else
            TotalFailingTests++;

        foreach (var line in fileLines.Skip(1))
            TestMethodContainer.IncrementTestMethod(line, overallTestResultIsPassing);
    }
}