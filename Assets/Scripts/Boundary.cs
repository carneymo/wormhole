using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Boundary
{
    public static float
        xMin = -34.5f,
        xMax = 34.5f,
        zMin = -16.5f,
        zMax = 16.5f,
        buffer = 0.0f;

    static float GetMinXBounds()
    {
        return xMin - buffer;
    }

    static float GetMaxXBounds()
    {
        return xMax + buffer;
    }

    static float GetMinZBounds()
    {
        return zMin - buffer;
    }

    static float GetMaxZBounds()
    {
        return zMax + buffer;
    }

    public static Rigidbody UpdateRigidbodyPosition(Rigidbody rig)
    {
        // Clamp to the Y Plane
        if( rig.position.y != 0.0f)
        {
            rig.position = new Vector3(
                rig.position.x,
                0.0f,
                rig.position.z
            );
        }
        if (rig.position.x < GetMinXBounds())
        {
            rig.position = new Vector3(
                GetMaxXBounds(),
                0.0f,
                rig.position.z
            );
        }

        if (rig.position.x > GetMaxXBounds())
        {
            rig.position = new Vector3(
                GetMinXBounds(),
                0.0f,
                rig.position.z
            );
        }

        if (rig.position.z < GetMinZBounds())
        {
            rig.position = new Vector3(
                rig.position.x,
                0.0f,
                GetMaxZBounds()
            );
        }

        if (rig.position.z > GetMaxZBounds())
        {
            rig.position = new Vector3(
                rig.position.x,
                0.0f,
                GetMinZBounds()
            );
        }
        return rig;
    }
}