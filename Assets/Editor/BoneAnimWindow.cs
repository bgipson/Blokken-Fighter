using UnityEngine;
using UnityEditor;

//[CustomEditor( typeof( BoneHandler))]
[CanEditMultipleObjects]
public class BoneAnimWindow : EditorWindow {
    GameObject selectedFighter;
    int selectedAction = 0;
    float xVal = 0;
    float yVal = 0;
    float zVal = 0;
    [MenuItem ("Window/Bone Animator")]
    static void Init() {
        BoneAnimWindow window = (BoneAnimWindow)EditorWindow.GetWindow(typeof(BoneAnimWindow));
        //window.maxSize = new Vector2(500, 800);
        window.Show();
        
    }

	void OnGUI() {
        //selectedFighter = GameObject.FindObjectOfType<FighterController>().gameObject;
        float width = position.width - 5;
        float height = 30;

        //GUILayout.Label(selectedFighter.name);

        GUILayout.Label("X-Value");
        xVal = GUILayout.HorizontalSlider(xVal, 0.0f, 360.0f);

        GUILayout.Label("Y-Value");
        yVal = GUILayout.HorizontalSlider(yVal, 0.0f, 360.0f);

        GUILayout.Label("Z-Value");
        zVal = GUILayout.HorizontalSlider(zVal, 0.0f, 360.0f);

        //Switch Buttons
        string[] actionLabels = new string[] { "ROTATION", "POSITION" };
        selectedAction = GUILayout.SelectionGrid(selectedAction, actionLabels, 2, GUILayout.Width(width), GUILayout.Height(height));
    }
}
