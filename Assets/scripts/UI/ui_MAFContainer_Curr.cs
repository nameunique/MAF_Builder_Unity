using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_MAFContainer_Curr : MonoBehaviour
{
    [SerializeField] private GameObject uiMAFTemplate_get;


    private static GameObject uiMAFTemplate;
    private static Transform uiMAFParent;

    private static List<Transform> uiMAFs = new List<Transform>();


    void Awake()
    {
        uiMAFTemplate = uiMAFTemplate_get;
        
        HideItself();
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
        
        ui_MAF_Curr item = item_gameobj.GetComponent<ui_MAF_Curr>();
        item.connected_maf = maf;
        item.rawImage.texture = maf.Image;
    }


    public void HideItself()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
    public void ShowItself()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
