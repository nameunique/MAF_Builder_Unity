using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drugdrop_scene_obj : MonoBehaviour
{
    private int floorLayer;

    void Start()
    {
        floorLayer = LayerMask.NameToLayer("floor");
    }

    private void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        MouseDrugEventHandler();
    }
    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            ddd_MAFContainer.DestroyMAF(GetComponent<ddd_MAF>());
    }


    public void MouseDrugEventHandler()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit[] hits;
        float maxDistance = 50f;
        hits = Physics.RaycastAll(ray, maxDistance);
        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.layer == floorLayer)
                {
                    Vector3 curr_pos = transform.position;
                    Vector3 target_pos = hit.point;
                    curr_pos.x = target_pos.x;
                    curr_pos.z = target_pos.z;
                    transform.position = curr_pos;
                    break;
                }
            }
        }
    }
}
