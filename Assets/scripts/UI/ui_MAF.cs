using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ui_MAF : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RawImage rawImage;

    private MAF _connected_maf;
    public MAF connected_maf
    {
        get { return _connected_maf; }
        set
        {
            if(_connected_maf != null) _connected_maf.ImageChange -= OnImageChange;
            _connected_maf = value;
            _connected_maf.ImageChange += OnImageChange;
        }
    }


    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }
    void OnDestroy()
    {
        _connected_maf.ImageChange -= OnImageChange;
    }

    public void SetTexture(Texture2D texture2D)
    {
        rawImage.texture = texture2D;
    }



    bool able_drag = true;
    private GameObject dd_obj_temp;

    public void OnDrag(PointerEventData pointerEventData)
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if still UI
        {
            if (dd_obj_temp != null) { ddd_MAFContainer.DestroyMAF(dd_obj_temp.GetComponent<ddd_MAF>()); dd_obj_temp = null; able_drag = true; }
        }
        else
        {
            if (able_drag)
            {
                dd_obj_temp = ddd_MAFContainer.AddNewMaf(connected_maf);
                able_drag = false;
            }
            else
            {
                dd_obj_temp.GetComponent<drugdrop_scene_obj>().MouseDrugEventHandler();
                dd_obj_temp.GetComponent<ddd_MAF>().ResetOfSizeMAF();
            }
        }
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        able_drag = true;
        dd_obj_temp = null;
    }



    private void OnImageChange(Texture2D texture2D)
    {
        rawImage.texture = texture2D;
    }
}
