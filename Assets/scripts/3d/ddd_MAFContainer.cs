using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ddd_MAFContainer : MonoBehaviour
{
    [SerializeField] private GameObject dddMAFTemplate_get;

    private static GameObject dddMAFTemplate;
    private static Transform dddMAFParent;

    public static List<Transform> dddMAFs = new List<Transform>();


    void Awake()
    {
        dddMAFTemplate = dddMAFTemplate_get;
    }

    void Start()
    {
        dddMAFParent = dddMAFTemplate.transform.parent;
        dddMAFTemplate.SetActive(false);
    }

    public static void ClearMAFs()
    {
        int count_child = dddMAFParent.childCount;
        for (int i = 1; i < count_child; i++)
            Destroy(dddMAFParent.GetChild(i).gameObject);

        dddMAFs.Clear();
    }

    public static GameObject AddNewMaf(MAF maf)
    {
        GameObject item_gameobj = Instantiate(dddMAFTemplate, dddMAFParent);
        item_gameobj.SetActive(true);
        
        ddd_MAF item = item_gameobj.GetComponent<ddd_MAF>();
        item.connected_maf = maf;
        item.SetTexture(maf.Image);

        dddMAFs.Add(item_gameobj.transform);
        global_manager.CallMAFsChange();

        return item_gameobj;
    }

    public static void DestroyMAF(ddd_MAF ddd_maf)
    {
        dddMAFs.Remove(ddd_maf.transform);
        Destroy(ddd_maf.gameObject);
    }
}
