
# Pre-Requisites

You must install [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0https://dotnet.microsoft.com/en-us/download/dotnet/9.0) to run this project.

## Run Instructions

Once .NET 9.0 is installed, you should be able to clone this respository and run the command below. 

```bash
dotnet run --project SoftwareTestingProject
```

Running this will set the the tests in `1.txt` and `2.txt` to failing tests in the test data, which will in turn be used to generate the SBFL results.

# Output File
Running the command above will generate an `output.csv` file in the root directory of the repository that you can examine that contains all of the test methods names, their SBFL results, and the inputs that were used to generate the SBFL that can be used for manual checking.
