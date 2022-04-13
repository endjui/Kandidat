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
    public Size covarianceSize;
    public int numberOfCards;
    public int numberOfTraining;
    public double[] result;
    public PCA[] pCA;
    public Mat[] testImage;
    public Mat mean;
    public Mat[] averageImage;
    public Mat[] eigenVectors;
    public Mat eig;
    public Mat[] phi;
    public Size imageVectorSize;
    public Mat tmp;

    public Eigenface(WebCamTexture webCam, Texture2D[][] images)
    {
        // Optional, can delete later. Gets canvas objects so we can display the images
        //gameObject = GameObject.Find("webCam");
        //gameObject1 = GameObject.Find("queryImage");

        size = new Size(images[0][0].width, images[0][0].height);
        imageVectorSize = new Size(1, size.Width * size.Height);
        //Size sizu = new Size(numberOfTraining, numberOfTraining);
        //Size sizy = new Size(1, numberOfTraining * numberOfTraining);
        //covarianceSize = new Size(size.Height, size.Height);
        numberOfCards = images.Length;
        numberOfTraining = images[0].Length;
        result = new double[numberOfCards];

        /*********************************************************
        * Convert all images to matricies and grayscale them     *
        **********************************************************/

        // Initialize all matricies used for calculations
        pCA = new PCA[numberOfCards];
        averageImage = new Mat[numberOfCards];
        eigenVectors = new Mat[numberOfCards];
        phi = new Mat[numberOfCards];
        Mat[] final = new Mat[numberOfCards];
        Mat eigenValues = new Mat();
        Mat[] A = new Mat[numberOfTraining];
        Mat[] reshapedImages = new Mat[numberOfTraining];
        Mat[] L = new Mat[numberOfCards];
        Mat[] eigenVectorsFinal = new Mat[numberOfCards];
        mean = new Mat();
        queryImage = new Mat[numberOfCards];
        currentImage = new Mat();
        temp = new Mat();
        testImage = new Mat[numberOfCards];

        for (int n = 0; n < numberOfCards; ++n)
        {
            averageImage[n] = Mat.Zeros(imageVectorSize, MatType.CV_64F);
            //L[n] = Mat.Zeros(sizy, MatType.CV_64F);
            final[n] = new Mat();

            for (int i = 0; i < numberOfTraining; ++i)
            {
                queryImage[i] = OpenCvSharp.Unity.TextureToMat(images[n][i]);

                // Grayscale image
                queryImage[i] = queryImage[i].CvtColor(ColorConversionCodes.BGR2GRAY);

                // Convert to correct MatType
                queryImage[i].ConvertTo(queryImage[i], MatType.CV_64F);

                reshapedImages[i] = queryImage[i].Reshape(0, queryImage[i].Rows*queryImage[i].Cols).Clone();
                averageImage[n] += reshapedImages[i];
                
                final[n].PushBack(reshapedImages[i].T());
                //A.col( 0 ).copyTo( B.col(0) ); // that's fine
            }

            averageImage[n] /= numberOfCards;

            eigenVectors[n] = new Mat();

            pCA[n] = new PCA(final[n], mean, PCA.Flags.DataAsCol);
            eigenVectors[n] = pCA[n].Eigenvectors.Normalize(255, 0, NormTypes.L2);

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
    }

    public string matchImage(WebCamTexture webCam, cards[] allCards)
    {

        // Convert the WebCamTexture to Mat type
        currentImage = OpenCvSharp.Unity.TextureToMat(Resources.Load<Texture2D>("Images/VineSkeleton/VineSkeleton"));

        // Resize of image for comparasion, might not be needed
        //currentImage = currentImage.Resize(covarianceSize);

        // Gray scale image
        currentImage = currentImage.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Convert to correct type for comparasion
        currentImage.ConvertTo(currentImage, MatType.CV_64F);

        currentImage = currentImage.Reshape(0, currentImage.Rows * currentImage.Cols).Clone();

        for (int n = 0; n < numberOfCards; ++n)
        {
            testImage[n] = currentImage - averageImage[n];
            phi[n] = Mat.Zeros(imageVectorSize, MatType.CV_64F);

            for (int i = 0; i < numberOfTraining; ++i)
            {
                eig = eigenVectors[n].ColRange(i, i+1);
                Mat temp = eig * testImage[n].T(); // Det här är fel
                Mat temp2 = temp.T() * eig; // Och det här är fel
                
                phi[n] += temp2; // Den här blir fel 
            }//aaa

            tmp = testImage[n] - phi[n];
            result[n] = tmp.Norm();
        }

        
        

        // Normalized Eigenvalue of test image
        //test = currentImage - EV;
        //test = test.Normalize(255 * 64, 0, NormTypes.L2);

        //// Calculate the euclidian distance for each image
        //for (int i = 0; i < numberOfCards; ++i)
        //{
        //    Cv2.Pow((eigenImage[i] - test), 2, temp); // temp is a temporary Mat
        //    result[i] = (double)temp.Sum(); // Explicit cast from type Scalar to double
        //    result[i] = Math.Sqrt(result[i]);
        //}

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