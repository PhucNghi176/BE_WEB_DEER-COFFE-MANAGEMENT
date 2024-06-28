using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.AspNetCore.Http;
using System.Drawing;


namespace DeerCoffeeShop.Infrastructure.Repositories;

public class FaceDetectionRepository : IFaceDetectionRepository
{


    public async Task<string> DetectFaceFromImage(IFormFile image, string[] directories)
    {
        string cascadePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml");
        CascadeClassifier faceClassifier = new(cascadePath);
        string recognizedNames = "";
        List<Image<Gray, byte>> trainingImages = [];
        List<string> labels = [];
        int ContTrain = 0;

        try
        {
            // Load previously trained faces and labels for each image
            foreach (string dir in directories)
            {
                string label = Path.GetFileNameWithoutExtension(dir); // Get the label from the directory name
                string[] files = Directory.GetFiles(dir, "*.bmp");

                foreach (string file in files)
                {
                    trainingImages.Add(new Image<Gray, byte>(file));
                    labels.Add(label);
                }
            }

            ContTrain = labels.Count;
            string filePath = await SaveImage(image, "UnknowEmployee", "UnknowEmployeeFolder");

            // Take the latest image from the UnknowEmployeeFolder
            Image<Gray, byte> grayImage = new Image<Bgr, byte>(filePath).Convert<Gray, byte>();
            // Convert the IFormFile to Image<Bgr, Byte>


            // Face Detector
            Rectangle[] facesDetected = faceClassifier.DetectMultiScale(
                grayImage,
                1.2,
                10,
                new Size(20, 20),
                Size.Empty);

            foreach (Rectangle faceRect in facesDetected)
            {
                Image<Gray, byte> result = grayImage.Copy(faceRect).Resize(100, 100, Inter.Cubic);

                if (trainingImages.Count != 0)
                {
                    // Create the Eigen face recognizer
                    EigenFaceRecognizer recognizer = new();
                    using (VectorOfMat imagesVector = new(trainingImages.Select(img => img.Mat).ToArray()))
                    using (VectorOfInt labelsVector = new(Enumerable.Range(0, labels.Count).ToArray()))
                    {
                        recognizer.Train(imagesVector, labelsVector);
                    }

                    FaceRecognizer.PredictionResult resultRecognized = recognizer.Predict(result);
                    if (resultRecognized.Label != -1)
                    {
                        string name = labels[resultRecognized.Label];
                        recognizedNames = name;
                    }
                }
            }

            // Print recognized names
            if (string.IsNullOrEmpty(recognizedNames))
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

        return recognizedNames;
    }

    public async Task<string> SaveImage(IFormFile image, string employeeID, string employeeFolderPath, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(employeeFolderPath))
        {
            _ = Directory.CreateDirectory(employeeFolderPath);
        }

        // Check if the image is not empty
        if (image.Length == 0)
        {
            throw new NotFoundException("Image is empty");
        }

        // Construct the path to save the image inside the employee's folder
        string uniqueFileName = $"{Guid.NewGuid()}.bmp";
        string filePath = Path.Combine(employeeFolderPath, uniqueFileName);

        try
        {
            using FileStream stream = new(filePath, FileMode.Create);
            await image.CopyToAsync(stream, cancellationToken);
            return filePath;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}
