using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace ASCIIImage
{
    class ASCIIArt
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============[IMAGE TO ASCII]==============");
            Console.WriteLine("[*] Version: 1.0");
            Console.WriteLine("[*] by Lenge");
            Console.WriteLine("==============[IMAGE TO ASCII]==============\n");
            while (true)
            {
                s_convert();
            }
        }

        private static string[] s_grading = { "@", "%", "#", "*", "+", "=", "-", ":", ".", " " };
        private static void s_convert()
        {
            try
            {
                Console.WriteLine("[1] Enter the path to the image file you want to convert, including file name and extension. (.png, .jpg):");
                string path = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("[1] Enter the path to the image file you want to convert, including file name and extension. (.png, .jpg):\n"+path);
                Console.WriteLine("\n[2] Specify the path to the directory where you want to save the artwork.");
                string savePath = Console.ReadLine();
                Console.WriteLine("\n[3] Specify the name of the artwork");
                string name = Console.ReadLine();
                Bitmap bitmap = new Bitmap(path);
                if(bitmap.Height + bitmap.Width > 1000)
                {
                    Console.WriteLine("\n[!] Warning: Images of that size might take a really long time to convert! Consider rescaling them first.");
                }
                string asciiArt = "";
                Stopwatch rtWatch = Stopwatch.StartNew();
                for (int i = 0; i < bitmap.Height; i++)
                {
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        Color pixelColor = bitmap.GetPixel(j, i);
                        double step = 1.0 / s_grading.Length;
                        double gradingMult = pixelColor.GetBrightness() / step;
                        int index = (int)Math.Round(gradingMult);
                        asciiArt += s_grading[index == 0 ? index : index - 1] + " ";
                    }
                    asciiArt += "\n";
                }
                rtWatch.Stop();
                var ms = rtWatch.ElapsedMilliseconds;
                Console.WriteLine($"DONE! Took {ms}ms.");

                string saveFile = savePath + $"/ascii_{name.ToUpper()}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Year} - {DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.txt";
                Console.WriteLine($"File output to: " + saveFile + "\n");
                File.Create(saveFile).Close();
                File.WriteAllText(saveFile, asciiArt);
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("An unknown error occurred. Restart the application and try again.");
            }
        }
    }
}
