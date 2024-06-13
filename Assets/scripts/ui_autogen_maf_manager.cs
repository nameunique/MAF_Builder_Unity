using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ui_autogen_maf_manager : MonoBehaviour
{
    [SerializeField] public TMP_InputField tbCost;
    [SerializeField] public TMP_Dropdown ddManufacturer;
    [SerializeField] public string ID_Area;



    public void Button_Refresh_Data(string url)
    {
        
    }

    
    IEnumerator Refresh_Data(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        Debug.Log(request.error);

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url}]: {request.error}");
        }
        else
        {
            // request.downloadHandler.text !!!!!!!!!!!!!!!!!!!!!!!!!!!
            
        }

        request.Dispose();
    }

}
