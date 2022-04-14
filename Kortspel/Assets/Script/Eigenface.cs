using System;
using UnityEngine;
using UnityEngine.UI;

using OpenCvSharp;

unsafe public class Eigenface : MonoBehaviour
{

    // Class variables
    private Mat queryImage;
    private Mat currentImage;
    private Mat temp;
    private Size size;
    private int numberOfCards;
    private int numberOfTraining;
    private double[] result;
    private PCA[] pCA;
    private Mat[] testImage;
    private Mat[] mean;
    private Mat[] eigenVectors;
    private Mat eig;
    private Mat[] phi;
    private Size imageVectorSize;
    private Mat tmp;

    public Eigenface(WebCamTexture webCam, Texture2D[][] images)
    {
        // Optional, can delete later. Gets canvas objects so we can display the images
        //gameObject = GameObject.Find("webCam");
        //gameObject1 = GameObject.Find("queryImage");

        size = new Size(images[0][0].width*0.1, images[0][0].height*0.1);
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
        eigenVectors = new Mat[numberOfCards];
        phi = new Mat[numberOfCards];
        Mat[] final = new Mat[numberOfCards];
        Mat reshapedImages;
        currentImage = new Mat();
        testImage = new Mat[numberOfCards];
        mean = new Mat[numberOfCards];


        for (int n = 0; n < numberOfCards; ++n)
        {
            final[n] = new Mat();

            for (int i = 0; i < numberOfTraining; ++i)
            {
                queryImage = OpenCvSharp.Unity.TextureToMat(images[n][i]);

                queryImage = queryImage.Resize(size);
                // Grayscale image
                queryImage = queryImage.CvtColor(ColorConversionCodes.BGR2GRAY);

                // Convert to correct MatType
                queryImage.ConvertTo(queryImage, MatType.CV_64FC1);

                reshapedImages = queryImage.Reshape(0, queryImage.Rows*queryImage.Cols).Clone();
                
                final[n].PushBack(reshapedImages.T());
                //A.col( 0 ).copyTo( B.col(0) ); // that's fine
            }

            eigenVectors[n] = new Mat();
            pCA[n] = new PCA(final[n].T(), new Mat(), PCA.Flags.DataAsCol);
            //pCA[n] = new PCA(final[n], mean, PCA.Flags.DataAsRow);
            eigenVectors[n] = pCA[n].Eigenvectors; //.Normalize(255, 0, NormTypes.L2);
            mean[n] = pCA[n].Mean;
            Debug.Log(eigenVectors[n]);

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
        currentImage = OpenCvSharp.Unity.TextureToMat(Resources.Load<Texture2D>("Images/VineSlinger/VineSlinger"));

        // Resize of image for comparasion, might not be needed
        currentImage = currentImage.Resize(size);

        // Gray scale image
        currentImage = currentImage.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Convert to correct type for comparasion
        currentImage.ConvertTo(currentImage, MatType.CV_64FC1);
        Size ss = new Size(imageVectorSize.Height,1);
        currentImage = currentImage.Reshape(0, currentImage.Rows * currentImage.Cols).Clone();

        for (int n = 0; n < numberOfCards; ++n)
        {
            testImage[n] = currentImage - mean[n];
            phi[n] = Mat.Zeros(ss, MatType.CV_64FC1);

            for (int i = 0; i < numberOfTraining; ++i)
            {
                eig = eigenVectors[n].RowRange(i,i+1);
                temp = (testImage[n] * eig);
                phi[n] += eig * temp; // Den här blir fel 
            }

            tmp = testImage[n] - phi[n].T();
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