using DeerCoffeeShop.Domain.Common.Exceptions;
using DeerCoffeeShop.Domain.Repositories;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
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
            var filePath = await SaveImage(image, "UnknowEmployee", "UnknowEmployeeFolder");

            // Take the latest image from the UnknowEmployeeFolder
            var inputImage = new Image<Bgr, byte>(filePath);
            // Convert the IFormFile to Image<Bgr, Byte>
            Image<Gray, byte> grayImage = inputImage.Convert<Gray, byte>();

            // Face Detector
            var facesDetected = faceClassifier.DetectMultiScale(
                grayImage,
                1.2,
                10,
                new Size(20, 20),
                Size.Empty);

            foreach (var faceRect in facesDetected)
            {
                Image<Gray, byte> result = grayImage.Copy(faceRect).Resize(100, 100, Inter.Cubic);

                if (trainingImages.Count != 0)
                {
                    // Create the Eigen face recognizer
                    var recognizer = new EigenFaceRecognizer();
                    using (var imagesVector = new VectorOfMat(trainingImages.Select(img => img.Mat).ToArray()))
                    using (var labelsVector = new VectorOfInt(Enumerable.Range(0, labels.Count).ToArray()))
                    {
                        recognizer.Train(imagesVector, labelsVector);
                    }

                    var resultRecognized = recognizer.Predict(result);
                    if (resultRecognized.Label != -1)
                    {
                        string name = "Employee ID: " + labels[resultRecognized.Label];
                        recognizedNames = name;
                    }
                }
            }

            // Print recognized names
            if (string.IsNullOrEmpty(recognizedNames))
            {
                return "No faces recognized.";
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
            Directory.CreateDirectory(employeeFolderPath);
        }

        // Check if the image is not empty
        if (image.Length == 0)
        {
            throw new NotFoundException("Image is empty");
        }

        // Construct the path to save the image inside the employee's folder
        var uniqueFileName = $"{Guid.NewGuid()}.bmp";
        var filePath = Path.Combine(employeeFolderPath, uniqueFileName);

        try
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream, cancellationToken);
            return filePath;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}
