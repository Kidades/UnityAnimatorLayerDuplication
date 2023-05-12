using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class LayerDuplicator : EditorWindow
{
    private AnimatorController _controller;
    private int _selectedLayer;
    private string _suffix = " Copy";
    
    [MenuItem("Tools/Layer Duplicator")]
    public static void ShowWindow()
    {
        GetWindow<LayerDuplicator>("Layer Duplicator");
    }

    private void OnGUI()
    {
        _controller = (AnimatorController)EditorGUILayout.ObjectField("Animator Controller", _controller, typeof(AnimatorController), false);
        _suffix = EditorGUILayout.TextField("Suffix", _suffix);

        if (_controller != null)
        {
            string[] layerNames = new string[_controller.layers.Length];
            for (int i = 0; i < _controller.layers.Length; i++)
            {
                layerNames[i] = i + ": " + _controller.layers[i].name;
            }

            _selectedLayer = EditorGUILayout.Popup("Source Layer", _selectedLayer, layerNames);
        }

        if (GUILayout.Button("Duplicate Layer"))
        {
            if (_controller == null)
            {
                Debug.LogError("No Animator Controller selected.");
                return;
            }

            if (_selectedLayer < 0 || _selectedLayer >= _controller.layers.Length)
            {
                Debug.LogError("Invalid layer index.");
                return;
            }

            AnimatorControllerLayer sourceLayer = _controller.layers[_selectedLayer];
            AnimatorControllerLayer newLayer = new AnimatorControllerLayer()
            {
                name = sourceLayer.name + _suffix,
                avatarMask = sourceLayer.avatarMask,
                blendingMode = sourceLayer.blendingMode,
                defaultWeight = sourceLayer.defaultWeight,
                iKPass = sourceLayer.iKPass,
                syncedLayerAffectsTiming = sourceLayer.syncedLayerAffectsTiming,
                syncedLayerIndex = sourceLayer.syncedLayerIndex,
                stateMachine = sourceLayer.stateMachine,
            };

            var layers = new AnimatorControllerLayer[_controller.layers.Length + 1];
            _controller.layers.CopyTo(layers, 0);
            layers[^1] = newLayer;
            _controller.layers = layers;

            AssetDatabase.SaveAssets();
        }
    }
}
