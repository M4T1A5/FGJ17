using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
[CanEditMultipleObjects]
public class PlayerEditor : Editor
{
    private void OnSceneGUI()
    {
        var player = target as PlayerMovement;

        var angle = player.transform.rotation *
            Quaternion.AngleAxis(-player.JumpAngle, new Vector3(1, 0, 0));
        var position = player.transform.position;

        var handleSize = HandleUtility.GetHandleSize(position);
        Handles.color = Color.magenta;

        Handles.ArrowCap(0, player.transform.position, angle, handleSize);
        //Handles.ConeCap(0, position, angle, handleSize/3);
    }
}
