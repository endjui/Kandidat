using UnityEngine;
using OpenCvSharp;

unsafe public class Eigenface : MonoBehaviour
{

    // Class variables
    private Mat trainingImage;
    private Mat[] mean;
    private Mat[] eigenVectors;
    private Size scaledImageSize;
    private Size eigenVectorMultiplier;
    private int numberOfCards;
    private int[] numberOfTraining;

    public Eigenface(Texture2D[][] images)
    {
        // Initialize sizes and constants
        numberOfCards = images.Length;
        numberOfTraining = new int[numberOfCards];
        

        Resources.UnloadUnusedAssets();


        /*********************************************************
        * Convert all images to matricies and grayscale them     *
        **********************************************************/

        // Initialize all matricies used for calculations
        PCA[] pca = new PCA[numberOfCards];
        eigenVectors = new Mat[numberOfCards];
        mean = new Mat[numberOfCards];
        Mat[] combinedTraining = new Mat[numberOfCards];

        Resources.UnloadUnusedAssets();


        for (int n = 0; n < numberOfCards; ++n)
        {
            combinedTraining[n] = new Mat();
            numberOfTraining[n] = images[n].Length;

            if (numberOfTraining[n] < 2) continue;

            scaledImageSize = new Size(images[n][1].width * 0.05, images[n][1].height * 0.05);
            eigenVectorMultiplier = new Size(scaledImageSize.Width * scaledImageSize.Height, 1);

            for (int i = 1; i < numberOfTraining[n]; ++i)
            {

                trainingImage = OpenCvSharp.Unity.TextureToMat(images[n][i]);

                convertImage(ref trainingImage);
                //// Resize the image so Unity allows us to allocate memory for the images
                //trainingImage = trainingImage.Resize(scaledImageSize);

                //// Grayscale image
                //trainingImage = trainingImage.CvtColor(ColorConversionCodes.BGR2GRAY);

                //// Convert to correct MatType
                //trainingImage.ConvertTo(trainingImage, MatType.CV_64FC1);

                //// Reshape images to once long column vector for PCA calculations
                //reshapedImages = trainingImage.Reshape(0, trainingImage.Rows*trainingImage.Cols).Clone();
                
                // Combine all training images
                combinedTraining[n].PushBack(trainingImage.T());

                Resources.UnloadUnusedAssets();

            }
            
            // Calculate PCA(Principal Component Analysis) which gives mean values and eigenvectors
            pca[n] = new PCA(combinedTraining[n].T(), new Mat(), PCA.Flags.DataAsCol);
            eigenVectors[n] = pca[n].Eigenvectors; //.Normalize(255, 0, NormTypes.L2);
            mean[n] = pca[n].Mean;
            Resources.UnloadUnusedAssets();

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

    public string matchImage(WebCamTexture webCam, Card[] allCards)
    {
        

    // Convert the WebCamTexture to Mat type
        Mat currentWebCamTexture = OpenCvSharp.Unity.TextureToMat(webCam);

        convertImage(ref currentWebCamTexture);
        Resources.UnloadUnusedAssets();

        //// Resize of image for comparasion, might not be needed
        //currentWebCamTexture = currentWebCamTexture.Resize(scaledImageSize);

        //// Gray scale image
        //currentWebCamTexture = currentWebCamTexture.CvtColor(ColorConversionCodes.BGR2GRAY);

        //// Convert to correct type for comparasion
        //currentWebCamTexture.ConvertTo(currentWebCamTexture, MatType.CV_64FC1);

        //// Reshape WebCamTexture for comparasion
        //currentWebCamTexture = currentWebCamTexture.Reshape(0, currentWebCamTexture.Rows * currentWebCamTexture.Cols).Clone();

        double[] result = euclidianDistance(currentWebCamTexture);
        Resources.UnloadUnusedAssets();

        double min = result[0];
        string path = "";
        int index = 0;
        int minimumThreshold = 2600;

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

        
        if(min < minimumThreshold)
        {
            // Store the resulting path from the index
            path = allCards[index].getPath();
        }

        // Clean up memory of unused assets
        Resources.UnloadUnusedAssets();

        return path;
    }

    private void convertImage(ref Mat img)
    {
        // Resize image
        img = img.Resize(scaledImageSize);

        // Gray scale image
        img = img.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Convert to correct type for comparasion
        img.ConvertTo(img, MatType.CV_64FC1);

        // Reshape WebCamTexture for comparasion
        img = img.Reshape(0, img.Rows * img.Cols).Clone();

    }

    private double[] euclidianDistance(Mat tex)
    {
        Mat[] phi = new Mat[numberOfCards];
        Mat[] currentMeanDiff = new Mat[numberOfCards];
        Mat finalDiff;
        double[] result = new double[numberOfCards];
        Mat eigenSingle;


        // Calculate the euclidian distance to the eigenfaces
        for (int n = 0; n < numberOfCards; ++n)
        {
            // initialize result incase we don't calculate anything
            result[n] = int.MaxValue;
            // if there are less than 2 images, we skip the current iteration
            if (numberOfTraining[n] < 3) continue;

            // Calculate the difference between the WebCamTexture and the mean image for
            currentMeanDiff[n] = tex - mean[n];
            phi[n] = Mat.Zeros(eigenVectorMultiplier, MatType.CV_64FC1);
            

            for (int i = 1; i < numberOfTraining[n]-1; ++i)
            {
                // Use the eigenvectors for the training images to calculate phi
                Debug.Log(eigenVectors[n].Size());
                eigenSingle = eigenVectors[n].RowRange(i, i + 1);
                Mat temp = (currentMeanDiff[n] * eigenSingle);
                phi[n] += eigenSingle * temp;
            }

            // Get resulting euclidian distance
            finalDiff = currentMeanDiff[n] - phi[n].T();
            result[n] = finalDiff.Norm();
        }

        return result;
    }
}