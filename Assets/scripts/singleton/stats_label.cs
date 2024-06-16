using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class stats_label : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    public static TMP_Text label;

    void Start()
    {
        label = _label;
        
        RefreshText("");
    }

    public static void RefreshText(string text)
    {
        label.text = text;
    }
}
