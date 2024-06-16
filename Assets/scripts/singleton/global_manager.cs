using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class global_manager : MonoBehaviour
{
    public static List<MAF> MAFs = new List<MAF>();


    public static List<ddd_MAF> ddd_MAFs = new List<ddd_MAF>();

    public static Action<List<ddd_MAF>> ddd_MAFs_Change;


    private void Start()
    {
        ddd_MAFs_Change += ddd_MAFs_Change_Handler;
    }



    public static void CallMAFsChange()
    {
        ddd_MAFs_Change?.Invoke(ddd_MAFs);
    }



    public static void AddNewdddMAF(ddd_MAF new_ddd_MAF)
    {
        if (!ddd_MAFs.Contains(new_ddd_MAF))
            ddd_MAFs.Add(new_ddd_MAF);

        ddd_MAFs_Change?.Invoke(ddd_MAFs);
    }

    public static void RemovedddMAF(ddd_MAF new_ddd_MAF)
    {
        if (ddd_MAFs.Contains(new_ddd_MAF))
            ddd_MAFs.Remove(new_ddd_MAF);

        ddd_MAFs_Change?.Invoke(ddd_MAFs);
    }






    private void ddd_MAFs_Change_Handler(List<ddd_MAF> ddd_MAFs)
    {
        string total_text = "";
        total_text += $"Общее количество МАФов = {ddd_MAFs.Count}\n";

        // some stats
        float total_cost = 0;
        foreach (ddd_MAF ddd_MAF in ddd_MAFs)
        {
            total_cost += ddd_MAF.connected_maf.Cost;
        }
        total_text += $"Стоимость всех МАФов = {total_cost}";


        // refresh visual
        stats_label.RefreshText(total_text);
        ui_MAFContainer_Curr.ClearMAFs();
        foreach (ddd_MAF ddd_MAF in ddd_MAFs)
        {
            ui_MAFContainer_Curr.AddNewMaf(ddd_MAF.connected_maf);
        }
    }
}
