using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorConverter.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			var vc = new VectorConverter();
			
			vc.GhostscriptDir = @"C:\Program Files (x86)\gs\gs9.14\";
			vc.ConvertEpsToPdf(@"..\..\..\Example\graphics.eps", @"..\..\..\Example\graphics.pdf");

			vc.Pdf2SvgDir = @"C:\pdf2svg-0.2.2\";
			vc.ConvertPdfToSvg(@"..\..\..\Example\graphics.pdf", @"..\..\..\Example\graphics-from-pdf.svg");
			
			vc.ConvertEpsToSvg(@"..\..\..\Example\graphics.eps", @"..\..\..\Example\graphics-from-eps.svg");

			vc.InkscapeDir = @"C:\Program Files (x86)\Inkscape\";
			vc.ConvertSvgToPdf(@"..\..\..\Example\graphics-from-eps.svg", @"..\..\..\Example\graphics-from-svg.pdf");
			vc.ConvertSvgToEps(@"..\..\..\Example\graphics-from-eps.svg", @"..\..\..\Example\graphics-from-svg.eps");
		}
	}
}
