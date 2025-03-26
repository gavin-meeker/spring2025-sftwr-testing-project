namespace SoftwareTestingProject;

public class TestReader
{
    public SbflTestContainer ReadAllTestsFromDirectory(string directoryPath)
    {
        SbflTestContainer testContainer = new();
        var testFiles = Directory.GetFiles(directoryPath);
        foreach (var file in testFiles) ReadTestResult(file, testContainer);
        return testContainer;
    }

    private void ReadTestResult(string fileName, SbflTestContainer testContainer)
    {
        var fileLines = File.ReadLines(fileName);
        var firstLine = fileLines.First();
        var overallTestResultIsPassing = Convert.ToBoolean(firstLine.Split(' ')[1]);

        testContainer.IncrementTotalTestCounts(overallTestResultIsPassing);

        foreach (var line in fileLines.Skip(1))
            testContainer.IncrementSpecificMethodCounts(line, overallTestResultIsPassing);
    }
}