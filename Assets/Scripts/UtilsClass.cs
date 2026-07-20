using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public static  class UtilsClass 
{
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseWorldposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldposition.z = 0f;
        return mouseWorldposition;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
    }

   public static float GetAngerFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

}
