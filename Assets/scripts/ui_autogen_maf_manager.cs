using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ui_autogen_maf_manager : MonoBehaviour
{
    [SerializeField] public TMP_InputField tbCost;
    [SerializeField] public TMP_Dropdown ddManufacturer;
    [SerializeField] public TMP_Dropdown ddArea;
    
    



    public void Button_Refresh_Data()
    {
        StartCoroutine(Refresh_Data_Manuf(@"http://127.0.0.1:8000/get_manufacturer"));
        StartCoroutine(Refresh_Data_Area(@"http://127.0.0.1:8000/get_areas"));
    }

    public void Button_Generate_MAFs()
    {
        float cost = float.Parse(tbCost.text);
        
        string manuf = ddManufacturer.options[ddManufacturer.value].text;
        string id_area = ddArea.options[ddArea.value].text.Split(";")[0];

        string req = @"http://127.0.0.1:8000/generate_maf_for_id_cost_manuf?" + 
            $"cost={cost}&manufacturer={UnityWebRequest.EscapeURL(manuf)}&id_area={UnityWebRequest.EscapeURL(id_area)}";

        StartCoroutine(GenerateMAFs(req));
    }



    IEnumerator Refresh_Data_Manuf(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url}]: {request.error}");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;

            List<string> ListStringTmp = new List<string>(JsonUtility.FromJson<ListString>(jsonResponse).list);

            ddManufacturer.ClearOptions();
            ddManufacturer.AddOptions(ListStringTmp);
        }

        request.Dispose();
    }
    IEnumerator Refresh_Data_Area(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url}]: {request.error}");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;

            List<AddressItem> temp = new List<AddressItem>(JsonUtility.FromJson<AddressList>(jsonResponse).list);

            List<string> temp_string = temp.Select(x => $"{x.ID};{x.Address}").ToList();

            ddArea.ClearOptions();
            ddArea.AddOptions(temp_string);
        }

        request.Dispose();
    }

    IEnumerator GenerateMAFs(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.DataProcessingError ||
                request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error requesting [{url}]: {request.error}");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            // Debug.Log($"jsonResponse = {jsonResponse}");

            
            List<int> temp = new List<int>(JsonUtility.FromJson<MafListResponse>(jsonResponse).list);

            // Debug.Log(String.Join("; ", temp));

            ui_MAFContainer_Curr.ClearMAFs();
            ddd_MAFContainer.ClearMAFs();
            for(int i = 0; i < temp.Count; i++)
            {
                MAF maf = global_manager.MAFs.Where(x => x.ID == temp[i].ToString()).ToList()[0];
                ddd_MAFContainer.AddNewMaf(maf);
                ui_MAFContainer_Curr.AddNewMaf(maf);
            }
        }

        request.Dispose();
    }
}
