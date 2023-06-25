using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;
    [SerializeField] Transform gridLayoutGroupTransform;
    private Dictionary<int, GameObject> uploadableImages = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> uploadingImages = new Dictionary<int, GameObject>();
    private const string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
    public int maxSimultaneousRequests = 5;
    private int failedRequestsInRow = 0;
    private int totalImages = 0;
    private int currentIndex = 0;
    int pendingDetections;
    bool finishedDetecting;
    bool finishedLoading;

    [SerializeField] Scrollbar scrollbar;

    // Start is called before the first frame update
    private void Start()
    {
        DetectImages();
    }


    public void UpdateScrollView()
    {
        UnityEngine.Debug.Log("updating scroll view, current value is " + scrollbar.value);
        

        if (finishedDetecting && !finishedLoading)
        {
            if (scrollbar.value == 0) { finishedLoading = true; return; }
            int preuploadRowsCount = 6;
            // This is a very basic example of "virtual scrolling"
            // You might need to adjust this to suit your actual UI layout and the size of your images
            int uploadableRows = ((int) ((1 - scrollbar.value) * (totalImages / 2))) + preuploadRowsCount;

            UnityEngine.Debug.Log("preuploaded rows are " + uploadableRows);

            List<int> keysToStartDownload = uploadableImages.Keys
                                          .Where(key => key <= uploadableRows * 2)
                                          .OrderBy(key => key)
                                          .ToList(); // Creating a list of keys to download

            foreach (int key in keysToStartDownload)
            {
                UnityEngine.Debug.Log(key + " will be started ");
                if (uploadableImages.ContainsKey(key)) // Check if the key is still in 'uploadableImages'
                {
                    // Move the image from 'uploadableImages' to 'uploadingImages' and start the download.
                    GameObject value = uploadableImages[key];
                    uploadableImages.Remove(key);
                    uploadingImages.Add(key, value);
                    StartCoroutine(GetImageCoroutine(key, value));
                }
            }
        }
    }


    void DetectImages()
    {
        for (int i = 0; i < maxSimultaneousRequests; i++)
        {
            StartCoroutine(DetectImageCoroutine());
        }
    }

    private IEnumerator DetectImageCoroutine()
    {
        while (failedRequestsInRow < 10)
        {
            pendingDetections++;
            //string url = baseUrl + currentIndex + ".jpg";
            string url = baseUrl + currentIndex + ".jpg";
            int cachedIndex = currentIndex;
            currentIndex++;
            using (UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbHEAD))
            {
                Stopwatch newTimer = new Stopwatch();
                newTimer.Start();
                yield return webRequest.SendWebRequest();
                newTimer.Stop();
                UnityEngine.Debug.Log("Time taken for the web request: " + newTimer.ElapsedMilliseconds + " ms");

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    UnityEngine.Debug.Log($"Failed to detected image at {url}");
                    failedRequestsInRow++;
                }
                else
                {
                    UnityEngine.Debug.Log($"Successfully detected image at {url}");
                    //failedRequestsInRow = 0;
                    totalImages++;

                    // Do something with the texture here
                    //Texture2D texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

                    GameObject newImage = Instantiate(imagePrefab, gridLayoutGroupTransform);
                    newImage.SetActive(true);
                    newImage.GetComponent<UploadedImage>().ImageIndex = totalImages;
                    uploadableImages.Add(totalImages, newImage);
                    //newImage.GetComponent<Image>().sprite = SpriteFromTexture2D(texture);
                    //Debug.Log("Image downloaded");
                }
            }

            pendingDetections--;

            if (pendingDetections == 0)
            {
                finishedDetecting = true;
                UpdateScrollView();
                UnityEngine.Debug.Log("all finished");
            }
        }
        

        UnityEngine.Debug.Log($"Total images detected: {totalImages}");
    }

    private IEnumerator GetImageCoroutine(int index, GameObject imageHolder)
    {
        string url = baseUrl + index + ".jpg";
        
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            Stopwatch newTimer = new Stopwatch();
            newTimer.Start();
            yield return webRequest.SendWebRequest();
            newTimer.Stop();
            UnityEngine.Debug.Log("Time taken for the web texture request: " + newTimer.ElapsedMilliseconds + " ms");

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                UnityEngine.Debug.Log($"Failed to download image at {url}");
            }
            else
            {
                Texture2D texture;

                if (TexturesCache.Contains(url))
                {
                    texture = TexturesCache.Get(url);
                } else
                {
                    texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                    TexturesCache.Add(url, texture);
                }

                UnityEngine.Debug.Log($"Successfully downloaded image at {url}");

                // Do something with the texture here

                imageHolder.GetComponent<UploadedImage>().EnableImage();
                imageHolder.GetComponent<Image>().sprite = SpriteFromTexture2D(texture);
                uploadingImages.Remove(index);
                //Debug.Log("Image downloaded");
            }
        }


        UnityEngine.Debug.Log($"Total images downloaded: {totalImages}");
    }

    public void StopUploading()
    {
        StopAllCoroutines();
    }

    public void RestartUploading()
    {
        UnityEngine.Debug.Log("restarting uploading for " + uploadingImages.Count);
        foreach (var element in uploadingImages)
        {
            StartCoroutine(GetImageCoroutine(element.Key, element.Value));
        }
    }

    private Sprite SpriteFromTexture2D(Texture2D texture)
    {
        
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
}
