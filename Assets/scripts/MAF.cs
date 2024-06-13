using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MAFs
{
    public List<MAF> list;
}

[System.Serializable]
public class MAF
{
    public string ID;
    public float Cost;
    public string Name;
    public string Provider;
    public string ImagePath;
    public string Dimensions;
    

    public System.Action<Texture2D> ImageChange;
    private Texture2D _Image; 
    public Texture2D Image
    {
        get { return _Image; }
        set { _Image = value; ImageChange?.Invoke(_Image); }
    }


    public MAF(string _ID, string _ImagePath, float _Cost, string _Name, string _Provider)
    {
        ID = _ID;
        ImagePath = _ImagePath;
        Cost = _Cost;
        Name = _Name;
        Provider = _Provider;
    }

    public override string ToString()
    {
        return $"Name = {Name}, ID = {ID}, Cost = {Cost}, Provider = {Provider}, ImagePath = {ImagePath};";
    }
}
