using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using OpenCvSharp;

    public class Eigenface : MonoBehaviour
    {
        
        // Class variables
        //private Texture2D webCamOutput;
        //private GameObject gameObject;
        //private GameObject gameObject1;
        private WebCamTexture webCam;
        private Texture2D[] images;
        private Texture2D imagine;
        private Mat[] queryImage;
        private Mat[] eigenImage;
        private Mat EV;
        private Mat currentImage;
        private Mat test;
        private Mat temp;
        private Size size;
        private int numberOfImages;
        private double[] result;

        public Eigenface()
        {
            /*******************************************************
            * Initialize camera and load images                    *
            ********************************************************/

            // Get all camera devices and play the correct Camera
            WebCamDevice[] devices = WebCamTexture.devices;
            if(devices.Length > 1)
            {
                webCam = new WebCamTexture(devices[1].name);
            }
            else
            {
                webCam = new WebCamTexture(devices[0].name);
            }
            
            webCam.Play();

            // Optional, can delete later. Gets canvas objects so we can display the images
            //gameObject = GameObject.Find("webCam");
            //gameObject1 = GameObject.Find("queryImage");

            // Load all images as objects with Texture2Ds
            images = Resources.LoadAll<Texture2D>("Images") as Texture2D[];



            //StartCoroutine(DownloadImage("https://drive.google.com/uc?export=download&id=1YBJnwk_kGLlgzbyQPFpG8XeQpWv60mIX"));

            //IEnumerator DownloadImage(string MediaUrl)
            //{
            //    UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            //    yield return request.SendWebRequest();
            //    if (request.isNetworkError || request.isHttpError)
            //        Debug.Log(request.error);
            //    else
            //        imagine = ((DownloadHandlerTexture)request.downloadHandler).texture;
            //}


            // Get size of camera and number of images
            size = new Size(webCam.width, webCam.height);
            numberOfImages = images.Length;
            result = new double[numberOfImages];



            /*********************************************************
            * Convert all images to matricies and grayscale them     *
            **********************************************************/

            // Initialize all matricies used for calculations
            Size covarianceSize = new Size(size.Height, size.Height);
            Mat averageImage = Mat.Zeros(size, MatType.CV_64F);
            Mat matSum = Mat.Zeros(covarianceSize, MatType.CV_64F);
            Mat tempMat = new Mat();
            Mat tmp;
            queryImage = new Mat[numberOfImages];
            currentImage = new Mat();
            temp = new Mat();

            // We use foreach here to access each Texture2D in images
            for (int i = 0; i < numberOfImages; ++i)
            {
                // Convert from Texture2D to Mat
                queryImage[i] = OpenCvSharp.Unity.TextureToMat(images[i]);
                

                // Resize the image to the same size as webCam
                queryImage[i] = queryImage[i].Resize(size);

                // Grayscale image
                queryImage[i] = queryImage[i].CvtColor(ColorConversionCodes.BGR2GRAY);

                // Convert to correct MatType
                queryImage[i].ConvertTo(queryImage[i], MatType.CV_64F);

                averageImage += queryImage[i];

            }

            averageImage /= numberOfImages;

            for (int i = 0; i < numberOfImages; ++i)
            {
                tempMat = queryImage[i] - averageImage;
                matSum += tempMat * tempMat.T();
            }
            matSum = matSum / numberOfImages;


            /*******************************************************
            * Calculate Eigenvector and eigenvalues for all images *
            ********************************************************/

            // Initialize Eigenvector(EV) to get the correct MatType
            Mat sum = Mat.Zeros(size, MatType.CV_64F);

            // Calculate Eigenvector(EV)
            for (int i = 0; i < numberOfImages; ++i)
            {
                sum += queryImage[i];
            }
            EV = sum / numberOfImages;

            // Calculate the normalized eigenvalues of all images
            eigenImage = new Mat[numberOfImages];

            for (int i = 0; i < numberOfImages; ++i)
            {
                tmp = queryImage[i] - EV;
                eigenImage[i] = tmp.Normalize(255 * 64, 0, NormTypes.L2);
            }
        }

        public string getId()
        {
            // String for storing the resulting Id in
            string id = "Vine Skeleton";

            // Convert the WebCamTexture to Mat type
            currentImage = OpenCvSharp.Unity.TextureToMat(webCam);

            // Resize of image for comparasion, might not be needed
            currentImage = currentImage.Resize(size);

            // Gray scale image
            currentImage = currentImage.CvtColor(ColorConversionCodes.BGR2GRAY);

            // Convert to correct type for comparasion
            currentImage.ConvertTo(currentImage, MatType.CV_64F);

            // Normalized Eigenvalue of test image
            test = currentImage - EV;
            test = test.Normalize(255 * 64, 0, NormTypes.L2);

            // Calculate the euclidian distance for each image
            for (int i = 0; i < numberOfImages; ++i)
            {
                Cv2.Pow((eigenImage[i] - test), 2, temp); // temp is a temporary Mat
                result[i] = (double)temp.Sum(); // Explicit cast from type Scalar to double
                result[i] = Math.Sqrt(result[i]);
            }

            double min = result[0];
            int index = 0;

            for (int i = 0; i < numberOfImages; ++i)
            {
                if (min > result[i])
                {
                    min = result[i];
                    index = i;
                }
            }

            // For debugging, can delete later
            Debug.Log("Minimum distance: " + min.ToString() + " Index: " + index.ToString());




            /************************************
             * TASK: Find card Id and return it *
             ************************************/




            // Convert to correct type for displaying
            //currentImage.ConvertTo(currentImage, MatType.CV_8U);

            // Convert image Mat back to Texture2D
            //webCamOutput = Unity.MatToTexture(currentImage);

            //gameObject.GetComponent<RawImage>().texture = webCamOutput;
            //gameObject1.GetComponent<RawImage>().texture = imagine;

            // Clean up memory of unused assets
            Resources.UnloadUnusedAssets();

            return id;
        }


    }



