using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_MAFContainer : MonoBehaviour
{
    [SerializeField] private GameObject uiMAFTemplate_get;
    [SerializeField] private Scrollbar scrollbar_get;


    private static GameObject uiMAFTemplate;
    private static Transform uiMAFParent;

    private static Scrollbar scrollbar;

    private static List<Transform> uiMAFs = new List<Transform>();


    void Awake()
    {
        uiMAFTemplate = uiMAFTemplate_get;
        scrollbar = scrollbar_get;
    }

    void Start()
    {
        uiMAFParent = uiMAFTemplate.transform.parent;
        uiMAFTemplate.SetActive(false);
    }

    public static void ClearMAFs()
    {
        int count_child = uiMAFParent.childCount;
        for (int i = 1; i < count_child; i++)
            Destroy(uiMAFParent.GetChild(i).gameObject);

        uiMAFs.Clear();
    }

    public static void AddNewMaf(MAF maf)
    {
        GameObject item_gameobj = Instantiate(uiMAFTemplate, uiMAFParent);
        item_gameobj.SetActive(true);
        
        ui_MAF item = item_gameobj.GetComponent<ui_MAF>();
        item.connected_maf = maf;
        item.rawImage.texture = maf.Image;
    }

    public static void SetBarValue(float value)
    {
        if(value < 0 || value > 1) throw new System.Exception("value can be only in range [0, 1]");
        scrollbar.value = value;
    }
}
