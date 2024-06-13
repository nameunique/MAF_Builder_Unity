using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate_floor : MonoBehaviour
{
    [SerializeField] private GameObject floorTemplate_get;

    private static GameObject floorTemplate;
    private static Transform floorParent;

    private static List<Transform> floor = new List<Transform>();


    void Awake()
    {
        floorTemplate = floorTemplate_get;
    }

    void Start()
    {
        floorParent = floorTemplate.transform.parent;
        floorTemplate.SetActive(false);

        GenerateFloor();


        // Vector2 vector2 = new Vector2(37.48918807210984f, 55.802817395493001f);
        // Vector3 pos = GPSEncoder.GPSToUCS(vector2);
        // Debug.Log($"point = {vector2}; xyz_pos = {pos}; recalt_back = {GPSEncoder.USCToGPS(pos)}");
    }

    public static void ClearFloor()
    {
        int count_child = floorParent.childCount;
        for (int i = 1; i < count_child; i++)
            Destroy(floorParent.GetChild(i).gameObject);

        floor.Clear();
    }

    public static void GenerateFloor(int meters_x = 200, int meters_z = 200)
    {
        ClearFloor();

        for (int x = -meters_x / 2; x < meters_x / 2; x++)
            for (int z = -meters_z / 2; z < meters_z / 2; z++)
            {
                GameObject floor_item_gameobj = Instantiate(floorTemplate, floorParent);
                floor_item_gameobj.SetActive(true);
                Transform floor_item = floor_item_gameobj.transform;
                
                Vector3 curr_pos = floor_item.position;
                curr_pos.x = x;
                curr_pos.z = z;
                floor_item.position = curr_pos;

                floor.Add(floor_item);
            }
    }
}
