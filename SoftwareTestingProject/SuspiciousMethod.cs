namespace SoftwareTestingProject;

public class SuspiciousMethod
{
    public required MethodResult MethodResult { get; init; }
    public int TotalOverallFailingTests { get; set; }
    public int TotalOverallPasssingTests { get; set; }
}