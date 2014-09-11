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
			vc.Eps2Pdf(@"..\..\..\Example\graphics.eps", @"..\..\..\Example\graphics.pdf");

			vc.Pdf2SvgDir = @"C:\pdf2svg-0.2.2\";
			vc.Pdf2Svg(@"..\..\..\Example\graphics.pdf", @"..\..\..\Example\graphics.svg");

			vc.Eps2Svg(@"..\..\..\Example\graphics.eps", @"..\..\..\Example\graphics2.svg");
		}
	}
}
