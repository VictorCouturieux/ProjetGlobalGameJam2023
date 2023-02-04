using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (SeedGenerator))]
[CanEditMultipleObjects]
public class SeedGeneratorEditor : Editor
{
    private SeedGenerator _mapGen;
    private Transform _mapGenTransform;
    private Vector3 _currentTransformPosition;
    private Vector3 _currentTransformScale;

    void OnEnable () {
        _mapGen = (SeedGenerator)target;
        _mapGenTransform = _mapGen.transform;
    }
    
    public override void OnInspectorGUI() {
        serializedObject.Update();
        
        GUILayout.Space(10);
        GUILayout.Label("Initial point reference :", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("initialPointType"));
        if (serializedObject.FindProperty("initialPointType").intValue == (int)InitialPointType.FromAlgorithmReference) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("initialReferencesPoints"));
        } else if (serializedObject.FindProperty("initialPointType").intValue == (int)InitialPointType.FromTransformReference) {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("transformReference"));
        }
        
        GUILayout.Space(10);
        GUILayout.Label("Select Randomize Algorithm :", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("algo"));
        // mapGen.Algo = (RandomAlgoName) EditorGUILayout.EnumPopup("Algo", mapGen.Algo);
        
        GUILayout.Space(10);
        GUILayout.Label("Seed Generation :", EditorStyles.boldLabel);
        switch (serializedObject.FindProperty("algo").intValue) {
            case (int) RandomAlgoName.PoissonDisc:
                // mapGen.RejectionSamples = EditorGUILayout.IntSlider("Rejection Samples", mapGen.RejectionSamples, 1, 5);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("rejectionSamples"));
                
                // EditorGUILayout.PropertyField(serializedObject.FindProperty("radius"));
                using (new EditorGUILayout.HorizontalScope()) {
                    SerializedProperty property = serializedObject.FindProperty("range");
                    Vector2 range = property.vector2Value;
                    float min = 1f, max = 80;
                    EditorGUILayout.MinMaxSlider(
                        new GUIContent("Random range:"),
                        ref range.x, ref range.y,
                        min, max);
                    range.x = Mathf.Round(Mathf.Clamp(EditorGUILayout.FloatField(range.x, GUILayout.Width(50)), min ,max) * 1000f) / 1000f;
                    range.y = Mathf.Round(Mathf.Clamp(EditorGUILayout.FloatField(range.y, GUILayout.Width(50)), min ,max) * 1000f) / 1000f;
                    if (range.x > range.y) range.x = range.y;
                    property.vector2Value = range;
                }

                break;
            case (int) RandomAlgoName.Noise:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("xNbRegionSplit"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("zNbRegionSplit"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("radiusValidZone"));
                break;
            case (int) RandomAlgoName.FairyRing:
                using (new EditorGUILayout.HorizontalScope()) {
                    SerializedProperty property = serializedObject.FindProperty("ringRangeRadius");
                    Vector2 range = property.vector2Value;
                    float min = 0, max = 50;
                    EditorGUILayout.MinMaxSlider(
                        new GUIContent("Random ring range:"),
                        ref range.x, ref range.y,
                        min, max);
                    range.x = Mathf.Round(Mathf.Clamp(EditorGUILayout.FloatField(range.x, GUILayout.Width(50)), min ,max) * 1000f) / 1000f;
                    range.y = Mathf.Round(Mathf.Clamp(EditorGUILayout.FloatField(range.y, GUILayout.Width(50)), min ,max) * 1000f) / 1000f;
                    if (range.x > range.y) range.x = range.y;
                    property.vector2Value = range;
                }
                
                EditorGUILayout.PropertyField(serializedObject.FindProperty("nbMushroomsOnRing"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("isDoubleRing"));
                break;
        }

        GUILayout.Space(10);
        GUILayout.Label("Gizmos Param", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("showGizmos"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gizmosColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("gizmosRadius"));
        GUILayout.Space(10);
        GUILayout.Label("Update Params", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("autoUpdate"));

        if (serializedObject.ApplyModifiedProperties() 
            || _mapGen.transform.position != _currentTransformPosition
            || _mapGen.transform.lossyScale != _currentTransformScale) {
            if (_mapGen.autoUpdate) {
                _mapGen.GenerateSeed();
            }
        }

        if (GUILayout.Button ("Generate")) {
            _mapGen.GenerateSeed();
        }

        _currentTransformPosition = _mapGenTransform.position;
        _currentTransformScale = _mapGenTransform.lossyScale;
    }
    


    
}
