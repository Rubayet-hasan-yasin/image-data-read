using Tesseract;

namespace imageDataRead
{
    class Program
    {

        static void Main()
        {
            string imagePath = @"H:\\Download\\B R 2-min (1).jpg";
            string text = ReadTextFromImage(imagePath);

            Console.WriteLine("Text extracted from the image:");
            Console.WriteLine(text);
        }


        static string ReadTextFromImage(string imagePath)
        {
            using (var engine = new TesseractEngine(@"G:\C#\image data read\image data read\tessdata\", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }
    }
}