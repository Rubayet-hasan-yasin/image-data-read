using System.IO;
using System.Drawing;
using Tesseract;
using OpenCvSharp;

namespace imageDataRead
{
    class Program
    {

        static void Main()
        {
            var imagePath = "H:\\Download\\bg.png";

            byte[] imageArray = File.ReadAllBytes(imagePath);
            string base64Image = Convert.ToBase64String(imageArray);

            byte[] bytes = Convert.FromBase64String(base64Image);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                // Convert MemoryStream to Mat (OpenCV image format)
                Mat image = Mat.FromStream(ms, ImreadModes.Color);

                // Convert image to grayscale
                Mat grayImage = new Mat();
                Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);

                // Apply threshold
                Mat thresholdImage = new Mat();
                Cv2.Threshold(grayImage, thresholdImage, 128, 255, ThresholdTypes.Binary);

                // Save the thresholded image to a file
                string thresholdImagePath = "thresholded_image.png";
                thresholdImage.SaveImage(thresholdImagePath);

                // Perform OCR on the saved image using Tesseract
                using (var engine = new TesseractEngine(@"G:\Csharp\image data read\image data read\tessdata\", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(thresholdImagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            string text = page.GetText();
                            Console.WriteLine("OCR Result:");
                            Console.WriteLine(text);
                        }
                    }
                }
            }
        }
    }
}