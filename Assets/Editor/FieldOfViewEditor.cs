using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView view = (FieldOfView)target;
        Color color = Color.red;
        color.a = 0.2f;

        Vector3 angleLower = Quaternion.AngleAxis(-view.Angle / 2, view.transform.forward) * view.transform.right;

        Handles.color = color;
        Handles.DrawSolidArc(view.transform.position, view.transform.forward, angleLower, view.Angle, view.Radius);
    }
}