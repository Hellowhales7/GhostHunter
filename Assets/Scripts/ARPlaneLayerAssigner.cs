using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ARPlaneLayerAssigner : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager _arPlaneManager;

    [SerializeField]
    private LayerMask _ignoreLayer;

    private void OnEnable()
    {
        if (_arPlaneManager != null)
        {
            _arPlaneManager.planesChanged += OnPlanesChanged;
        }
    }

    private void OnDisable()
    {
        if (_arPlaneManager != null)
        {
            _arPlaneManager.planesChanged -= OnPlanesChanged;
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        foreach (var plane in eventArgs.added)
        {
            SetLayerRecursive(plane.gameObject, _ignoreLayer);
        }
    }

    private void SetLayerRecursive(GameObject obj, LayerMask layer)
    {
        obj.layer = layer.value;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursive(child.gameObject, layer);
        }
    }
}
