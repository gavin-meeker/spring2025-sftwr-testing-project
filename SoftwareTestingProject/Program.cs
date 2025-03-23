using System.IO.Compression;

namespace SoftwareTestingProject;

internal class Program
{
    public const string TestDirectoryName = "NewCoverageData";

    public static readonly string WorkingDirectory =
        Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."));

    public static void Main(string[] args)
    {
        Setup();

        // var testFiles = Directory.GetFiles(testDataDirectoryPath);
        // Array.Sort(testFiles);
        // foreach (var file in testFiles) ChangeTestResultTo(file, false);
    }

    private static void ChangeTestResultTo(string filePath, bool shouldPass = true)
    {
        var lines = File.ReadAllLines(filePath);
        var firstLine = lines[0];
        var firstPartOfLine = firstLine.Split(' ')[0];
        lines[0] = firstPartOfLine += shouldPass ? " true" : " false";
        File.WriteAllLines(filePath, lines);

        RenameFile(filePath);
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
        var zipFilePath = Path.GetFullPath(Path.Combine(WorkingDirectory, "CoverageData.zip"));
        if (!File.Exists(zipFilePath)) throw new FileNotFoundException("CoverageData.zip file could not be found");

        var testDataDirectoryPath = Path.GetFullPath(Path.Combine(WorkingDirectory, TestDirectoryName));
        if (Directory.Exists(testDataDirectoryPath)) return;


        ZipFile.ExtractToDirectory(zipFilePath, WorkingDirectory);
    }
}