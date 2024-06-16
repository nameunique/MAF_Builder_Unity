using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class export_mafs_xls : MonoBehaviour
{
    [System.Serializable]
    public class AreaData
    {
        public string ID_Area;
        public List<string> ID_MAFs;
    }


    [DllImport("__Internal")]
    private static extern void ExportMAFsToXLS(string js_req);



    [SerializeField] public TMP_Dropdown ddArea;



    public void Button_ExportMAFs()
    {
        string curr_id_area = ddArea.options[ddArea.value].text.Split(";")[0];


        AreaData areaData = new AreaData();
        areaData.ID_Area = curr_id_area;
        areaData.ID_MAFs = new List<string>();
        for (int i = 0; i < ddd_MAFContainer.dddMAFs.Count; i++)
            areaData.ID_MAFs.Add(ddd_MAFContainer.dddMAFs[i].GetComponent<ddd_MAF>().connected_maf.ID);
        
        string jsonString = JsonUtility.ToJson(areaData);
        
        ExportMAFsToXLS(jsonString);
    }
}
