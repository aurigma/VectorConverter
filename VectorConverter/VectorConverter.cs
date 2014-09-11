using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorConverter
{
	public class VectorConverter
    {
        public string GhostscriptDir { get; set; }
        public string Pdf2SvgDir { get; set; }
 

        public void Eps2Pdf(string inputPath, string outputPath)
        {
			if (String.IsNullOrEmpty(GhostscriptDir))
			{
				throw new InvalidOperationException("Ghostscript directory is undefined");
			}

            var psi = new ProcessStartInfo(Path.Combine(GhostscriptDir + "lib/ps2pdf.bat"),
				"\"" + Path.GetFullPath(inputPath) + "\" \"" + Path.GetFullPath(outputPath) + "\"");
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.WorkingDirectory = Path.Combine(GhostscriptDir + "bin");
 
            var process = new Process();
            process.StartInfo = psi;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
 
            if (process.ExitCode != 0)
			{
				throw new Exception(String.Format("Process '{0} {1}' exit code was {2}", psi.FileName, psi.Arguments, process.ExitCode));   
			}
        }
 

        public void Pdf2Svg(string inputPath, string ouputPath)
        { 
			if (String.IsNullOrEmpty(Pdf2SvgDir))
			{
				throw new InvalidOperationException("pdf2svg directory is undefined");
			}

            var psi = new ProcessStartInfo(Path.Combine(Pdf2SvgDir + "pdf2svg"),
				"\"" + Path.GetFullPath(inputPath) + "\" \"" + Path.GetFullPath(ouputPath) + "\"");
            psi.WindowStyle = ProcessWindowStyle.Hidden;
 
            var process = new Process();
            process.StartInfo = psi;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
 
            if (process.ExitCode != 0)
			{
				throw new Exception(String.Format("Process '{0} {1}' exit code was {2}", psi.FileName, psi.Arguments, process.ExitCode));   
			}
        }

 
        public void Eps2Svg(string inputPath, string outputPath)
        {
			string tempPdfFileName = System.IO.Path.GetTempFileName();
			
			try
			{
				Eps2Pdf(inputPath, tempPdfFileName);
				Pdf2Svg(tempPdfFileName, outputPath);
			}
			finally
			{
				if (File.Exists(tempPdfFileName))
				{
					File.Delete(tempPdfFileName);
				}
			}			
        }
    }
}
