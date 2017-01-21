using UnityEngine;
using System.Collections;

public static class Utility
{
    public static Transform GetRootObject(Collider collider)
    {
        // Collider referes to the child object the collider is part of
        // not the parent gameobject
        var parent = collider.transform;
        while (parent.parent)
        {
            parent = parent.parent;
        }

        return parent;
    }

    public static Transform GetRootObject(Collision collision)
    {
        return GetRootObject(collision.collider);
    }
}
