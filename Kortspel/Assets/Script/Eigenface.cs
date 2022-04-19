using System;
using UnityEngine;
using UnityEngine.UI;

using OpenCvSharp;

unsafe public class Eigenface : MonoBehaviour
{

    // Class variables
    private Mat trainingImage;
    private Mat currentWebCamTexture;
    private Mat temp;
    private Mat eigenSingle;
    private Mat finalDiff;
    private Mat reshapedImages;
    private Mat[] currentMeanDiff;
    private Mat[] mean;
    private Mat[] eigenVectors;
    private Mat[] combinedTraining;
    private Mat[] phi;
    private Size scaledImageSize;
    private Size imageVectorSize;
    private Size eigenVectorMultiplier;
    private int numberOfCards;
    private int numberOfTraining;
    private int index;
    private double[] result;
    private double min;
    private string path;
    private PCA[] pca;

    public Eigenface( Texture2D[][] images)
    {
        // Initialize sizes and constants
        scaledImageSize = new Size(images[0][0].width*0.1, images[0][0].height*0.1);
        imageVectorSize = new Size(1, scaledImageSize.Width * scaledImageSize.Height);
        eigenVectorMultiplier = new Size(imageVectorSize.Height, 1);
        numberOfCards = images.Length;
        numberOfTraining = images[0].Length;
        

        /*********************************************************
        * Convert all images to matricies and grayscale them     *
        **********************************************************/

        // Initialize all matricies used for calculations
        pca = new PCA[numberOfCards];
        eigenVectors = new Mat[numberOfCards];
        phi = new Mat[numberOfCards];
        currentMeanDiff = new Mat[numberOfCards];
        mean = new Mat[numberOfCards];
        combinedTraining = new Mat[numberOfCards];
        currentWebCamTexture = new Mat();
        result = new double[numberOfCards];

        for (int n = 0; n < numberOfCards; ++n)
        {
            combinedTraining[n] = new Mat();

            for (int i = 0; i < numberOfTraining; ++i)
            {

                trainingImage = OpenCvSharp.Unity.TextureToMat(images[n][i]);

                // Resize the image so Unity allows us to allocate memory for the images
                trainingImage = trainingImage.Resize(scaledImageSize);

                // Grayscale image
                trainingImage = trainingImage.CvtColor(ColorConversionCodes.BGR2GRAY);

                // Convert to correct MatType
                trainingImage.ConvertTo(trainingImage, MatType.CV_64FC1);

                // Reshape images to once long column vector for PCA calculations
                reshapedImages = trainingImage.Reshape(0, trainingImage.Rows*trainingImage.Cols).Clone();
                
                // Combine all training images
                combinedTraining[n].PushBack(reshapedImages.T());
                
            }

            // Calculate PCA(Principal Component Analysis) which gives mean values and eigenvectors
            eigenVectors[n] = new Mat();
            pca[n] = new PCA(combinedTraining[n].T(), new Mat(), PCA.Flags.DataAsCol);
            eigenVectors[n] = pca[n].Eigenvectors; //.Normalize(255, 0, NormTypes.L2);
            mean[n] = pca[n].Mean;

            //for (int i = 0; i < numberOfTraining; ++i)
            //{
            //    A[i] = reshapedImages[i] - averageImage; // Apply average image to all images
            //}

            //for(int i = 0; i < numberOfTraining; ++i)
            //{
            //    for(int j = 0; j < numberOfTraining; ++j)
            //    {
            //        L[n].PushBack(A[j].T() * A[i]);
            //    }
            //}

            //L[n] = L[n].Resize(sizu);
            //Debug.Log(L[n]);

            //for (int i = 0; i < numberOfCards; ++i)
            //{
            //    Cv2.Eigen(L[n], eigenValues, eigenVectors);
            //    Debug.Log(eigenVectors);
            //    for (int k = 0; k < numberOfTraining; ++k)
            //    {
            //        eigenVectorsFinal[n] += eigenVectors.At<double>(k);
            //    }
            //}
        }

        // We use foreach here to access each Texture2D in images
        //for (int i = 0; i < numberOfImages; ++i)
        //{
        //    // Convert from Texture2D to Mat
        //    queryImage[i] = OpenCvSharp.Unity.TextureToMat(images[i]);


        //    // Resize the image to the same size as webCam
        //    queryImage[i] = queryImage[i].Resize(size);

        //    // Grayscale image
        //    queryImage[i] = queryImage[i].CvtColor(ColorConversionCodes.BGR2GRAY);

        //    // Convert to correct MatType
        //    queryImage[i].ConvertTo(queryImage[i], MatType.CV_64F);

        //    averageImage += queryImage[i];

        //}

        /********************************************************
        * Calculate Eigenvector and eigenvalues for all cards *
        *********************************************************/

        // Initialize Eigenvector(EV) to get the correct MatType
        //Mat sum = Mat.Zeros(covarianceSize, MatType.CV_64F);

        //// Calculate Eigenvector(EV)
        //for (int i = 0; i < numberOfCards; ++i)
        //{
        //    sum += covariance[i];
        //}
        //EV = sum / numberOfCards;

        //// Calculate the normalized eigenvalues of all images
        //eigenImage = new Mat[numberOfCards];

        //for (int i = 0; i < numberOfCards; ++i)
        //{
        //    tmp = covariance[i] - EV;
        //    eigenImage[i] = tmp.Normalize(255 * 64, 0, NormTypes.L2);
        //}
        Resources.UnloadUnusedAssets();

    }

    public string matchImage(WebCamTexture webCam, cards[] allCards)
    {

        // Convert the WebCamTexture to Mat type
        currentWebCamTexture = OpenCvSharp.Unity.TextureToMat(Resources.Load<Texture2D>("Images/VineSprout/VineSprout"));

        // Resize of image for comparasion, might not be needed
        currentWebCamTexture = currentWebCamTexture.Resize(scaledImageSize);

        // Gray scale image
        currentWebCamTexture = currentWebCamTexture.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Convert to correct type for comparasion
        currentWebCamTexture.ConvertTo(currentWebCamTexture, MatType.CV_64FC1);
        
        // Reshape WebCamTexture for comparasion
        currentWebCamTexture = currentWebCamTexture.Reshape(0, currentWebCamTexture.Rows * currentWebCamTexture.Cols).Clone();

        // Calculate the euclidian distance to the eigenfaces
        for (int n = 0; n < numberOfCards; ++n)
        {
            // Calculate the difference between the WebCamTexture and the mean image for
            currentMeanDiff[n] = currentWebCamTexture - mean[n];
            phi[n] = Mat.Zeros(eigenVectorMultiplier, MatType.CV_64FC1);

            for (int i = 0; i < numberOfTraining; ++i)
            {
                // Use the eigenvectors for the training images to calculate phi
                eigenSingle = eigenVectors[n].RowRange(i,i+1);
                temp = (currentMeanDiff[n] * eigenSingle);
                phi[n] += eigenSingle * temp;
            }

            // Get resulting euclidian distance
            finalDiff = currentMeanDiff[n] - phi[n].T();
            result[n] = finalDiff.Norm();
        }

        // Array to store the minimum value in
        min = result[0];
        index = 0;

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

        // Store the resulting path from the index
        path = allCards[index].getPath();

        // Clean up memory of unused assets
        Resources.UnloadUnusedAssets();

        return path;
    }
}