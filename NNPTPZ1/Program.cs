using System;
using System.Drawing;
using NNPTPZ1.Fractals;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// Hlavní vstupní bod aplikace pro generování Newtonova fraktálu.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Spouští program s parametry pro generování fraktálu.
        /// </summary>
        /// <param name="args">
        /// Očekávané argumenty: 
        /// <list type="number">
        /// <item><description>šířka obrázku</description></item>
        /// <item><description>výška obrázku</description></item>
        /// <item><description>minimální X</description></item>
        /// <item><description>maximální X</description></item>
        /// <item><description>minimální Y</description></item>
        /// <item><description>maximální Y</description></item>
        /// <item><description>výstupní soubor (např. fractal.png)</description></item>
        /// </list>
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Usage: NNPTPZ1 <width> <height> <xmin> <xmax> <ymin> <ymax> <output>");
                return;
            }

            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string output = args[6];

            // Ukázkový polynom: x^3 + 1
            var p = new Polynomial();
            p.Add(new ComplexNumber { Re = 1 });
            p.Add(ComplexNumber.Zero);
            p.Add(ComplexNumber.Zero);
            p.Add(new ComplexNumber { Re = 1 });

            var generator = new NewtonFractalGenerator(p);
            var bitmap = generator.Generate(width, height, xmin, xmax, ymin, ymax);

            bitmap.Save(output);
            Console.WriteLine($"Fractal saved to {output}");
        }
    }
}
