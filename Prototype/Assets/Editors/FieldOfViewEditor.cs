using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(FieldOfView)), CanEditMultipleObjects]
public class FieldOfViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.green;
        Handles.DrawWireArc(fov.eyeTransform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.eyeTransform.position, fov.eyeTransform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.eyeTransform.position, fov.eyeTransform.position + viewAngleB * fov.viewRadius);

        Handles.color = Color.red;
        for (int i = 0; i < fov.visibleTargets.Count; i++)
        {
            Transform target = fov.visibleTargets[i];
            Vector3 targetPos = new Vector3(target.position.x, fov.eyeTransform.position.y, target.position.z);
            Handles.DrawLine(fov.eyeTransform.position, targetPos);
        }
    }
}
