using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    Item draggingItem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = new Vector3(0, 0, 1);
            RaycastHit2D hit2D = Physics2D.Raycast(origin, direction, 100);

            if (hit2D.CheckRaycastHit("Item"))
            {
                draggingItem = hit2D.collider.GetComponent<Item>();
                draggingItem.SetDragging(true);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(draggingItem != null)
            {
                draggingItem.SetDragging(false);
                draggingItem = null;
            }
        }
    }
}
