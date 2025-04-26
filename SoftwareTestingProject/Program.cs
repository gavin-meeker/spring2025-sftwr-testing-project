using System.Globalization;
using System.IO.Compression;
using CsvHelper;

namespace SoftwareTestingProject;

internal class Program
{
    public const string TestDirectoryName = "NewCoverageData";

    public static readonly string WorkingDirectory =
        Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));

    public static void Main(string[] args)
    {
        Setup();

        var testDataDirectory = Path.Combine(WorkingDirectory, "NewCoverageData");

        ChangeTestResultTo(Path.Combine(testDataDirectory, "1.txt"), false);

        var testReader = new TestReader();

        var sbflContainer =
            testReader.ReadAllTestsFromDirectory(testDataDirectory);

        sbflContainer.PerformTestAnalysis();

        var suspiciousMethods = sbflContainer.GetAllSuspiciousMethods();
        Console.WriteLine(suspiciousMethods.Count);

        var csvPath = Path.Combine(WorkingDirectory, "output.csv");
        Console.WriteLine($"Writing to CSV file: {csvPath}");

        using (var writer = new StreamWriter(csvPath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(suspiciousMethods);
        }
    }

    private static void ChangeTestResultTo(string filePath, bool shouldPass)
    {
        var lines = File.ReadAllLines(filePath);
        var firstLine = lines[0];
        var firstPartOfLine = firstLine.Split(' ')[0];
        lines[0] = firstPartOfLine += shouldPass ? " true" : " false";
        File.WriteAllLines(filePath, lines);

        // RenameFile(filePath);
    }

    private static void RenameFile(string filePath)
    {
        var currentFileName = Path.GetFileName(filePath);

        if (currentFileName.Contains("passed_") || currentFileName.Contains("failed_"))
            currentFileName = Path.GetFileName(filePath).Split("_")[1];


        using (var file = File.OpenText(filePath))
        {
            var parentDirectoryName = Path.GetFullPath(Path.Combine(filePath, ".."));
            var testResultBool = Convert.ToBoolean(file.ReadLine()?.Split(" ")[1]);
            var testResult = testResultBool ? "passed" : "failed";

            var newFileName = Path.GetFullPath(Path.Combine(parentDirectoryName, $"{testResult}_{currentFileName}"));

            File.Move(filePath, newFileName);
        }
    }

    private static void Setup()
    {
        var pathToZipFile = Path.GetFullPath(Path.Combine(WorkingDirectory, "CoverageData.zip"));
        if (!File.Exists(pathToZipFile)) throw new FileNotFoundException("CoverageData.zip file could not be found");

        var pathToTestDataDirectory = Path.GetFullPath(Path.Combine(WorkingDirectory, TestDirectoryName));
        if (Directory.Exists(pathToTestDataDirectory)) return;


        ZipFile.ExtractToDirectory(pathToZipFile, WorkingDirectory);
    }
}