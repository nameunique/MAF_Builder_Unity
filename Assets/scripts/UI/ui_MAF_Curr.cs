using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ui_MAF_Curr : MonoBehaviour
{
    public RawImage rawImage;

    private MAF _connected_maf;
    public MAF connected_maf
    {
        get { return _connected_maf; }
        set
        {
            if (_connected_maf != null) _connected_maf.ImageChange -= OnImageChange;
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
        if (_connected_maf != null)
            _connected_maf.ImageChange -= OnImageChange;
    }

    public void SetTexture(Texture2D texture2D)
    {
        rawImage.texture = texture2D;
    }

    private void OnImageChange(Texture2D texture2D)
    {
        rawImage.texture = texture2D;
    }
}
