using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class InputManager
{

    public static Vector3 MouseRaycastPlane()
    {
        return MouseRaycastPlane(new Plane(new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 0f)));
    }

    public static Vector3 MouseRaycastPlane(Plane plane)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float distance);

        return ray.GetPoint(distance);
    }


}

