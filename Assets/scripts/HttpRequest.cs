using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour
{

    private Action<string> ResponseAct;
    private Action<Texture2D> ResponseAct_Image;






    private void Start()
    {
        ResponseAct += Test_Resp_Handler;
        ResponseAct_Image += Test_Resp_Handler_Image;
    }
    private void OnDestroy()
    {
        ResponseAct -= Test_Resp_Handler;
        ResponseAct_Image -= Test_Resp_Handler_Image;
    }



    public void Button_Test_Resp()
    {
        StartCoroutine(LoadTextFromServer(@"http://127.0.0.1:8000/test/with_values?item_id=123&another_item=456", ResponseAct));
    }
    public void Button_Test_Resp_Image()
    {
        StartCoroutine(LoadTextureFromServer(@"http://127.0.0.1:8000/test/image", ResponseAct_Image));
    }

    public void Button_Get_All_MAF()
    {
        StartCoroutine(LoadAllMAFs(@"http://127.0.0.1:8000/upload_mafs", @"http://127.0.0.1:8000/get_maf_image?img_name="));
    }


    private void Test_Resp_Handler(string text)
    {
        Debug.Log($"result of response = {text}");
    }
    private void Test_Resp_Handler_Image(Texture2D texture)
    {
        Debug.Log($"result of response image = {texture}");

        GameObject cube = GameObject.Find("Cube (1)");
        Renderer renderer = cube.GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
    }


    IEnumerator LoadTextFromServer(string url, Action<string> response)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        Debug.Log(request.error);

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url}]: {request.error}");
            response?.Invoke(null);
        }
        else
        {
            response?.Invoke(request.downloadHandler.text);
        }

        request.Dispose();
    }

    IEnumerator LoadTextureFromServer(string url, Action<Texture2D> response)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url}]: {request.error}");
            response?.Invoke(null);
        }
        else
        {
            response?.Invoke(DownloadHandlerTexture.GetContent(request));
        }


        request.Dispose();
    }






    IEnumerator LoadAllMAFs(string url_maf_data, string url_maf_image)
    {
        UnityWebRequest request = UnityWebRequest.Get(url_maf_data);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url_maf_data}]: {request.error}");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;

            global_manager.MAFs = new List<MAF>(JsonUtility.FromJson<MAFs>(jsonResponse).list);

            StartCoroutine(ResetMAFs());
            for (int i = 0; i < global_manager.MAFs.Count; i++)
            {
                yield return null;
                StartCoroutine(InstallImagForMAF(url_maf_image, global_manager.MAFs[i]));
            }
        }

        request.Dispose();
    }
    private IEnumerator ResetMAFs()
    {
        ui_MAFContainer.ClearMAFs();
        yield return null;
        for (int i = 0; i < global_manager.MAFs.Count; i++)
        {
            ui_MAFContainer.AddNewMaf(global_manager.MAFs[i]);
        }
        yield return null;
    }

    private IEnumerator InstallImagForMAF(string base_url, MAF maf)
    {
        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(base_url + maf.ImagePath);

        yield return imageRequest.SendWebRequest();

        if (imageRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error loading image [{maf.ImagePath}]: {imageRequest.error}");
        }
        else
        {
            maf.Image = DownloadHandlerTexture.GetContent(imageRequest);
        }

        imageRequest.Dispose();
    }
}
