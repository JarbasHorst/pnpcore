using System.Collections.Generic;
using System.Diagnostics;

namespace PnP.Core
{
    internal static class Shell
    {
        internal static List<string> Bash(string cmd)
        {
            var result = Run("/bin/bash", $@"-c ""{cmd}""");
            return result;
        }

        internal static List<string> Bat(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var result = Run("cmd.exe", $@"/c ""{escapedArgs}""");
            return result;
        }


        private static List<string> Run(string filename, string arguments)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                    RedirectStandardError = true
                }
            };
            var lines = new List<string>();
            process.Start();
            while (!process.StandardOutput.EndOfStream)
            {
                lines.Add(process.StandardOutput.ReadLine());
            }
            process.WaitForExit();
            return lines;
        }
    }
}
