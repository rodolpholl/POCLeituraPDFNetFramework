//using BarcodeLib.BarcodeReader;
using PDFtoImage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using ZXing;
//using ZXing.Windows.Compatibility;




namespace POCLeituraPDFNetFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => { return ExecuteZxing(); }).GetAwaiter().GetResult();
            Console.ReadKey();
        }

        /*public static async Task Execute()
        {
            Console.WriteLine("Iniciando processo de leitura do código de barras.\n");

            Console.WriteLine("Convertendo PDF em imagem PNG");

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF", "exemplo002.pdf");

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var pngFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempPNG", "png0001.png");
                Conversion.SavePng(pngFileName, fs, false, null, 0, new PDFtoImage.RenderOptions(Dpi: 350));
            }

            var files = Directory.GetFiles("TempPNG");
            Console.WriteLine("Image Count: {0}", files.Length);


            Console.WriteLine("Carregando a imagem gerada");

            //BarcodeReader reader = new BarcodeReader();
            foreach (var file in files)
            {
                var filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempPNG", Path.GetFileName(file));

                //var image = (Bitmap)Bitmap.FromFile(filename);

                //var barcode = reader.Decode(image);

                //if (barcode != null)
                //    Console.WriteLine($"Barcode resgatado: formato [{barcode.BarcodeFormat.ToString()}] - Valor [{barcode.Text}]");

                string[] data25 = BarcodeReader.read(filename, BarcodeReader.INTERLEAVED25);
                string[] data39 = BarcodeReader.read(filename, BarcodeReader.CODE39);
                string[] dataCB = BarcodeReader.read(filename, BarcodeReader.CODABAR);
                if (data25 != null)
                {
                    Console.WriteLine("Código de barras lidos para EAN25:");
                    foreach (var s in data25)
                        Console.WriteLine($"{s}");

                }

                if (data39 != null)
                {
                    Console.WriteLine("Código de barras lidos para EAN39:");
                    foreach (var s in data39)
                        Console.WriteLine($"{s}");

                }

                if (dataCB != null)
                {
                    Console.WriteLine("Código de barras lidos para CODEBAR:");
                    foreach (var s in dataCB)
                        Console.WriteLine($"{s}");
                }

            }
        }*/


        public static async Task ExecuteZxing()
        {
            Console.WriteLine("Iniciando processo de leitura do código de barras Usando o ZXing.\n");

            Console.WriteLine("Convertendo PDF em imagem PNG");

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PDF", "exemplo003.pdf");

            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var pngFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempPNG", "png0001.png");
                Conversion.SavePng(pngFileName, fs, false, null, 0, new PDFtoImage.RenderOptions(Dpi: 350));
            }

            var files = Directory.GetFiles("TempPNG");
            Console.WriteLine("Image Count: {0}", files.Length);

            Console.WriteLine("Carregando a imagem gerada");

            var listResult = new List<Result>();

            foreach (var file in files)
            {

                var filename = Path.GetFullPath(file);
                var imagem = (Bitmap)Bitmap.FromFile(filename);
                BarcodeReader reader = new BarcodeReader();

                var barcode = reader.Decode(imagem);

                if (barcode != null)
                    listResult.Add(barcode);


            }

        }
    }
}
