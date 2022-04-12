using System;
using UnityEngine;
using UnityEngine.UI;

using OpenCvSharp;

unsafe public class Eigenface : MonoBehaviour
{

    // Class variables
    //private Texture2D webCamOutput;
    //private GameObject gameObject;
    //private GameObject gameObject1;
    
    
    public Texture2D imagine;
    public Mat[] queryImage;
    public Mat[] eigenImage;
    public Mat EV;
    public Mat currentImage;
    public Mat test;
    public Mat temp;
    public Size size;
    public int numberOfCards;
    public int numberOfTraining;
    public double[] result;
    public Size covarianceSize;

    public Eigenface(WebCamTexture webCam, Texture2D[][] images)
    {
        // Optional, can delete later. Gets canvas objects so we can display the images
        //gameObject = GameObject.Find("webCam");
        //gameObject1 = GameObject.Find("queryImage");

        size = new Size(images[0][0].width, images[0][0].height);
        covarianceSize = new Size(size.Height, size.Height);
        numberOfCards = images.Length;
        numberOfTraining = images[0].Length;
        result = new double[numberOfCards];

        /*********************************************************
        * Convert all images to matricies and grayscale them     *
        **********************************************************/

        // Initialize all matricies used for calculations
        Mat averageImage;
        Mat tmp;
        Mat[] covariance = new Mat[numberOfCards];
        Mat[] diffImage = new Mat[numberOfTraining];
        Mat[] reshapedImages = new Mat[numberOfTraining];
        queryImage = new Mat[numberOfCards];
        currentImage = new Mat();
        temp = new Mat();

        for (int n = 0; n < numberOfCards; ++n)
        {
            covariance[n] = Mat.Zeros(covarianceSize, MatType.CV_64F);
            averageImage = Mat.Zeros(size.Width,1, MatType.CV_64F);
            // hejhopp gummisnopp

            for (int i = 0; i < numberOfTraining; ++i)
            {
                queryImage[i] = OpenCvSharp.Unity.TextureToMat(images[n][i]);

                queryImage[i].Resize(covarianceSize);

                // Grayscale image
                queryImage[i] = queryImage[i].CvtColor(ColorConversionCodes.BGR2GRAY);

                // Convert to correct MatType
                queryImage[i].ConvertTo(queryImage[i], MatType.CV_64F);

                reshapedImages[i] = queryImage[i].Reshape(0, queryImage[i].Rows*queryImage[i].Cols).Clone();

                averageImage += queryImage[i];
            }

            averageImage /= numberOfCards;

            Debug.Log(averageImage);
            Debug.Log(reshapedImages[0]);


            // TASK: Transpose diffImage matrix, transpose will transpose both the inside and outside array
            // Calculate the covariance matrix for each card
            for (int i = 0; i < numberOfTraining; ++i)
            {
                diffImage[i] = reshapedImages[i] - averageImage;
                covariance[n] += diffImage[i] * diffImage[i].T();
            }
            
            covariance[n] /= (numberOfCards);
        }
        Debug.Log(covariance);

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
        Mat sum = Mat.Zeros(covarianceSize, MatType.CV_64F);

        // Calculate Eigenvector(EV)
        for (int i = 0; i < numberOfCards; ++i)
        {
            sum += covariance[i];
        }
        EV = sum / numberOfCards;

        // Calculate the normalized eigenvalues of all images
        eigenImage = new Mat[numberOfCards];

        for (int i = 0; i < numberOfCards; ++i)
        {
            tmp = covariance[i] - EV;
            eigenImage[i] = tmp.Normalize(255 * 64, 0, NormTypes.L2);
        }
    }

    public string matchImage(WebCamTexture webCam, cards[] allCards)
    {

        // Convert the WebCamTexture to Mat type
        currentImage = OpenCvSharp.Unity.TextureToMat(Resources.Load<Texture2D>("Images/VineSkeleton/VineSkeleton"));

        // Resize of image for comparasion, might not be needed
        currentImage = currentImage.Resize(covarianceSize);

        // Gray scale image
        currentImage = currentImage.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Convert to correct type for comparasion
        currentImage.ConvertTo(currentImage, MatType.CV_64F);



        // Normalized Eigenvalue of test image
        test = currentImage - EV;
        test = test.Normalize(255 * 64, 0, NormTypes.L2);

        // Calculate the euclidian distance for each image
        for (int i = 0; i < numberOfCards; ++i)
        {
            Cv2.Pow((eigenImage[i] - test), 2, temp); // temp is a temporary Mat
            result[i] = (double)temp.Sum(); // Explicit cast from type Scalar to double
            result[i] = Math.Sqrt(result[i]);
        }

        double min = result[0];
        int index = 0;

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

        // String for storing the resulting path in
        string path = allCards[index].getPath();

        // Convert to correct type for displaying
        //currentImage.ConvertTo(currentImage, MatType.CV_8U);

        // Convert image Mat back to Texture2D
        //webCamOutput = Unity.MatToTexture(currentImage);

        //gameObject.GetComponent<RawImage>().texture = webCamOutput;
        //gameObject1.GetComponent<RawImage>().texture = imagine;

        // Clean up memory of unused assets
        Resources.UnloadUnusedAssets();

        return path;
    }
}



