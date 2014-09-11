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
		public string InkscapeDir { get; set; }


		private void RunProcess(string fileName, string arguments, string workingDirectory = null)
		{
			var psi = new ProcessStartInfo(fileName, arguments);
			psi.WindowStyle = ProcessWindowStyle.Hidden;
			if (!String.IsNullOrEmpty(workingDirectory))
			{
				psi.WorkingDirectory = workingDirectory;
			}

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


		private void ConvertSvgUsingInkscape(string inputPath, string ouputPath, string param)
		{
			if (String.IsNullOrEmpty(InkscapeDir))
			{
				throw new InvalidOperationException("Inkscape directory is undefined");
			}

			RunProcess(Path.Combine(InkscapeDir + "inkscape"),
				"\"" + Path.GetFullPath(inputPath) + "\" " + param + "\"" + Path.GetFullPath(ouputPath) + "\"");
		}


		public void ConvertEpsToPdf(string inputPath, string outputPath)
		{
			if (String.IsNullOrEmpty(GhostscriptDir))
			{
				throw new InvalidOperationException("Ghostscript directory is undefined");
			}

			RunProcess(Path.Combine(GhostscriptDir + "lib/ps2pdf.bat"),
				"\"" + Path.GetFullPath(inputPath) + "\" \"" + Path.GetFullPath(outputPath) + "\"",
				Path.Combine(GhostscriptDir + "bin"));
		}
 

		public void ConvertPdfToSvg(string inputPath, string ouputPath)
		{ 
			if (String.IsNullOrEmpty(Pdf2SvgDir))
			{
				throw new InvalidOperationException("pdf2svg directory is undefined");
			}

			RunProcess(Path.Combine(Pdf2SvgDir + "pdf2svg"),
				"\"" + Path.GetFullPath(inputPath) + "\" \"" + Path.GetFullPath(ouputPath) + "\"");
		}

 
		public void ConvertEpsToSvg(string inputPath, string outputPath)
		{
			string tempPdfFileName = System.IO.Path.GetTempFileName();
			
			try
			{
				ConvertEpsToPdf(inputPath, tempPdfFileName);
				ConvertPdfToSvg(tempPdfFileName, outputPath);
			}
			finally
			{
				if (File.Exists(tempPdfFileName))
				{
					File.Delete(tempPdfFileName);
				}
			}			
		}


		public void ConvertSvgToPdf(string inputPath, string ouputPath)
		{
			ConvertSvgUsingInkscape(inputPath, ouputPath, "--export-pdf=");
		}


		public void ConvertSvgToEps(string inputPath, string ouputPath)
		{
			ConvertSvgUsingInkscape(inputPath, ouputPath, "--export-eps=");
		}
			
	}
}
