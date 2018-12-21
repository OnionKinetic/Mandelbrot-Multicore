using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using mandelbrot_MultiCore;

namespace mandelbrot_master
{
    class Program
    {

        public static List<ClusterHost> Hosty = new List<ClusterHost>();

        static void Main(string[] args)
        {
            bool Running = false;

            while (!Running)
            {
                DetectHosts();
                Console.Clear();
                Console.WriteLine("2018 Klastrowany Generator Fraktali Zbioru Mandelbrota" + Environment.NewLine);
                Console.WriteLine("Wykryto " + Hosty.Count + " Węzłów" + Environment.NewLine + "Łącznie " + Hosty.Sum(a => a.Cores) + " rdzeni" + Environment.NewLine);
                foreach (ClusterHost CH in Hosty)
                {
                    Console.WriteLine(string.Format("{0} IP {1} Cores: {2}", CH.Name, CH.IP, CH.Cores));
                }

                Console.WriteLine("[Skanuj hosty    ]");
                Console.WriteLine("[Generuj Fraktal ]");
                Console.WriteLine("[Pokaż Fraktal   ]");
                Console.WriteLine("[Wyjdź           ]");

                Console.WriteLine();
                Console.ReadKey();

                RunCluster();
            }
        }

        static void RunCluster()
        { }

        static void DetectHosts()
        { }
    }
}
