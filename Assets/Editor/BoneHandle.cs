using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bone))]
[CanEditMultipleObjects]
public class BoneHandle : Editor {

    protected virtual void OnSceneGUI() {
        GameObject root = ((Bone)target).root;
        GameObject spine = ((Bone)target).spine;

        if (Event.current.type == EventType.Repaint) {
            connect(root, spine, Color.red);
        }

        //Give Spine a Rotation
        Bone t = (target as Bone);
        EditorGUI.BeginChangeCheck();
        Quaternion rot = Handles.FreeRotateHandle(t.rot, spine.transform.position, 0.75f);
        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(target, "Free Rotate");
            Debug.Log("CHANGE");
            //t.rot = rot;
            t.Update();
            
        }
    }

    protected virtual void connect(GameObject a, GameObject b, Color col) {
        Transform transform = ((Bone)target).transform;

        Transform aT = a.transform;
        Transform bT = b.transform;

        Handles.color = col;

        float size = Vector3.Distance(bT.position, aT.position);
        Vector3 centering = Vector3.Normalize(bT.position - aT.position) * (size / 2);

        Handles.ConeHandleCap(0, aT.position + centering, aT.rotation * Quaternion.LookRotation(bT.position - aT.position),
            size, EventType.Repaint);
    }
}
