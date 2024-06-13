using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ddd_MAF : MonoBehaviour
{
    public MAF connected_maf;



    public void SetTexture(Texture2D texture2D)
    {
        Material cubeMaterial = new Material(Shader.Find("Standard"));
        cubeMaterial.mainTexture = texture2D;
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.material = cubeMaterial;
        }
    }


    public void ResetOfSizeMAF()
    {
        float[] splitted_size = new float[3]; 
        try
        {
            splitted_size = connected_maf.Dimensions.Split("x").Select(x => float.Parse(x)).ToArray();
        }
        catch
        {
            Debug.Log("something wronge with Dimensions (with split VVVxVVVxVVV)");
            return;
        }

        transform.localScale = new Vector3(splitted_size[0] / 1000f, splitted_size[2] / 1000f, splitted_size[1] / 1000f);
    }
}
