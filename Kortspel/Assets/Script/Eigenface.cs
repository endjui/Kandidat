using UnityEngine;
using OpenCvSharp;

unsafe public class Eigenface : MonoBehaviour
{

    // Class variables
    private Mat trainingImage;
    private Mat currentWebCamTexture;
    private Mat[] mean;
    private Mat[] eigenVectors;
    private Mat[] eigenValues;
    private Mat[] phi;
    private Mat[] currentMeanDiff;
    private double[] result;
    private Size scaledImageSize;
    private Size eigenVectorMultiplier;
    private const int numberOfCards = 40;
    private const int numberOfTraining = 11;

    public Eigenface(ref Texture2D[][] images)
    {
        // Initialize sizes and constants
        scaledImageSize = new Size(images[0][1].width * 0.04, images[0][1].height * 0.04);
        eigenVectorMultiplier = new Size(scaledImageSize.Width * scaledImageSize.Height, 1);


        /*********************************************************
        * Convert all images to matricies and grayscale them     *
        **********************************************************/

        // Initialize all matricies used for calculations
        //PCA[] pca = new PCA[numberOfCards];
        Mat[] combinedTraining = new Mat[numberOfCards];
        eigenVectors = new Mat[numberOfCards];
        mean = new Mat[numberOfCards];
        phi = new Mat[numberOfCards];
        currentMeanDiff = new Mat[numberOfCards];
        result = new double[numberOfCards];
        eigenValues = new Mat[numberOfCards];
        trainingImage = new Mat();
        currentWebCamTexture = new Mat();


        // Initialize Mat objects in arrays
        for (int i = 0; i < numberOfCards; i++)
        {
            eigenVectors[i] = new Mat();
            eigenValues[i] = new Mat();
            mean[i] = new Mat();
            combinedTraining[i] = new Mat();
            phi[i] = Mat.Zeros(eigenVectorMultiplier, MatType.CV_32FC1);
            result[i] = int.MaxValue;
            currentMeanDiff[i] = new Mat();
        }

        for (int n = 0; n < numberOfCards; ++n)
        {
            for (int i = 1; i < numberOfTraining; ++i)
            {
                // Convert image texture to Mat object
                OpenCvSharp.Unity.TextureToMat(images[n][i]).CopyTo(trainingImage);

                // Convert image to correct size and type
                convertImage(ref trainingImage);

                // Combine all training images
                combinedTraining[n].PushBack(trainingImage);

            }

            // Calculate PCA(Principal Component Analysis) which gives mean values and eigenvectors
            Cv2.PCACompute(combinedTraining[n], mean[n], eigenVectors[n]);
            //pca[n] = new PCA(combinedTraining[n], new Mat(), PCA.Flags.DataAsRow);
            //pca[n].Eigenvectors.Normalize(1, 0, NormTypes.MinMax).CopyTo(eigenVectors[n]);
            //pca[n].Mean.CopyTo(mean[n]);
            Cv2.Normalize(eigenVectors[n], eigenVectors[n], 1, 0, NormTypes.MinMax);
        }

        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
    }

    public string matchImage(ref WebCamTexture webCam, ref Card[] allCards)
    {
        // Convert the WebCamTexture to Mat type
        OpenCvSharp.Unity.TextureToMat(webCam).CopyTo(currentWebCamTexture);
        
        convertImage(ref currentWebCamTexture);
        
        euclidianDistance(ref currentWebCamTexture);
        

        double min = result[0];
        int index = 0;
        const int minimumThreshold = 2600;

        // Calculate the minimum distance and it's index
        for (int i = 0; i < numberOfCards; ++i)
        {
            if (min > result[i])
            {
                min = result[i];
                index = i;
            }
        }

        // For debugging, can delete later
        Debug.Log("Minimum distance: " + min.ToString() + " Index: " + index.ToString());

        if (min < minimumThreshold)
        {
            // Store the resulting path from the index
            return allCards[index].getPath();
        }
        else
        {
            return "";
        }
    }

    private void convertImage(ref Mat img)
    {
        // Resize image
        Cv2.Resize(img, img, scaledImageSize);

        // Gray scale image
        Cv2.CvtColor(img, img, ColorConversionCodes.BGR2GRAY);

        // Convert to correct type for comparasion
        img.ConvertTo(img, MatType.CV_32FC1);

        // Reshape WebCamTexture for comparasion
        img.Reshape(0, img.Rows * img.Cols).CopyTo(img);

        Cv2.Transpose(img, img);
    }

    private void euclidianDistance(ref Mat tex)
    {
        // Calculate the euclidian distance to the eigenfaces
        for (int n = 0; n < numberOfCards; ++n)
        {
            Cv2.Subtract(tex, mean[n], currentMeanDiff[n]);

            for (int i = 0; i < numberOfTraining/2; ++i)
            {
                
                // Use the eigenvectors for the training images to calculate phi
                Cv2.Add(phi[n], (eigenVectors[n].RowRange(i, i + 1) * (currentMeanDiff[n].T() * eigenVectors[n].RowRange(i, i + 1))), phi[n]);
                
            }

            // Get resulting euclidian distance
            Cv2.Subtract(currentMeanDiff[n], phi[n], currentMeanDiff[n]);
            result[n] = currentMeanDiff[n].Norm();
            System.GC.Collect();
        }
    }
}