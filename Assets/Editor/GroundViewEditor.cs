using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GroundView))]
public class GroundViewEditor : Editor
{
    private void OnSceneGUI()
    {
        GroundView view = (GroundView)target;
        Color color = Color.red;
        Handles.color = color;
        float thickness = 4;

        Vector3 direction = (Quaternion.AngleAxis(-view.OffsetAngle, view.transform.forward) * view.transform.right).normalized;
        Handles.DrawLine(view.transform.position, view.transform.position + direction * view.Distance, thickness);
    }
}