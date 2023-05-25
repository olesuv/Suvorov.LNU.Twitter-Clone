using System.Diagnostics;

namespace Suvorov.LNU.TwitterClone.Script
{
    public class ConvertToCsharp
    {
        public async Task<string> RunPythonScript(string pythonPath, string scriptPath, string arguments)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = $"{pythonPath}";
            start.Arguments = $"{scriptPath} {string.Join(" ", arguments)}";
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = await reader.ReadToEndAsync();
                    return result;
                }

                //using (StreamReader errorReader = process.StandardError)
                //{
                //    string errorMessage = errorReader.ReadToEnd();
                //    Console.Write(errorMessage);
                //}
            }
        }

    }
}