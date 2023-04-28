using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suvorov.LNU.TwitterClone.Script;
using System.IO;

namespace Suvorov.LNU.TwitterClone.Script.Test
{
    [TestClass]
    public class OpenAITest
    {
        [TestMethod]
        public async Task TestRunPythonScript()
        {
            ConvertToCsharp converter = new ConvertToCsharp();
            string pythonPath = "C:\\Users\\olegs\\AppData\\Local\\Programs\\Python\\Python311\\python.exe";             
            string scriptPath = "D:\\twitter-clone-new\\Suvorov.LNU.TwitterClone.Script\\script.py";
            string arguments = "Funny joke about dotnet developer.";

            var result = await converter.RunPythonScript(pythonPath, scriptPath, arguments);
            Console.WriteLine(result);
        }
    }
}
