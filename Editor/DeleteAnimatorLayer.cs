using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class LayerDeletionWindow : EditorWindow
{
    AnimatorController _controller;
    int _selectedLayerIndex;
    
    [MenuItem("Tools/Duplicated Layer Deletion")]
    public static void ShowWindow()
    {
        GetWindow<LayerDeletionWindow>("Layer Deletion");
    }

    void OnGUI()
    {
        _controller = EditorGUILayout.ObjectField("Controller", _controller, typeof(AnimatorController), false) as AnimatorController;
        
        if(_controller == null)
        {
            EditorGUILayout.HelpBox("Please, select an Animator Controller", MessageType.Warning);
        }
        else
        {
            string[] layerNames = new string[_controller.layers.Length];
            for (int i = 0; i < _controller.layers.Length; i++)
            {
                layerNames[i] = _controller.layers[i].name;
            }

            _selectedLayerIndex = EditorGUILayout.Popup("Layer to Delete", _selectedLayerIndex, layerNames);

            if (GUILayout.Button("Delete Layer"))
            {
                DeleteLayer(_selectedLayerIndex);
            }
        }
    }

    void DeleteLayer(int layerIndex)
    {
        if (_controller == null || layerIndex < 0 || layerIndex >= _controller.layers.Length)
        {
            return;
        }
        
        // Remove the layer from the controller
        var layers = _controller.layers;
        var newLayers = new AnimatorControllerLayer[layers.Length - 1];
        int j = 0;
        for (int i = 0; i < layers.Length; i++)
        {
            if (i != layerIndex)
            {
                newLayers[j] = layers[i];
                j++;
            }
        }
        _controller.layers = newLayers;

        AssetDatabase.SaveAssets();
    }
}
