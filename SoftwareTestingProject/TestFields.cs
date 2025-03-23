namespace SoftwareTestingProject;

public class TestFields
{
    public TestFields()
    {
        ReadOverallTestResults();
    }

    public Dictionary<string, MethodResult> TestMethods { get; set; } = new();

    public int TotalFailingTests { get; set; }
    public int TotalPasssingTests { get; set; }

    private void ReadOverallTestResults()
    {
        var testDirectoryName = "NewCoverageData";
        var workingDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));
        var testFiles = Directory.GetFiles(Path.GetFullPath(Path.Combine(workingDirectory, testDirectoryName)));

        foreach (var file in testFiles) ReadTestResult(file);
    }

    private void ReadTestResult(string fileName)
    {
        var fileLines = File.ReadLines(fileName);
        var firstLine = fileLines.First();
        var overallTestResultIsPassing = Convert.ToBoolean(firstLine.Split(' ')[1]);

        foreach (var line in fileLines.Skip(1)) IncrementTestMethod(line, overallTestResultIsPassing);

        if (overallTestResultIsPassing)
            TotalPasssingTests++;
        else
            TotalFailingTests++;
    }

    private void IncrementTestMethod(string line, bool overallTestResultIsPassing)
    {
        if (TestMethods.ContainsKey(line))
        {
            if (overallTestResultIsPassing)
                TestMethods[line].PassingTestCount++;
            else
                TestMethods[line].FailingTestCount++;
        }
        else
        {
            if (overallTestResultIsPassing)
                TestMethods.Add(line, new MethodResult { PassingTestCount = 1, FailingTestCount = 0 });
            else
                TestMethods.Add(line, new MethodResult { PassingTestCount = 0, FailingTestCount = 1 });
        }
    }
}