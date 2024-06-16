
using System.Collections.Generic;


[System.Serializable]
public class ListString
{
    public List<string> list;
}






[System.Serializable]
public class AddressItem
{
    public string ID;
    public string Address;
}
[System.Serializable]
public class AddressList
{
    public List<AddressItem> list;
}






[System.Serializable]
public class MafListResponse
{
    public List<int> list;
}