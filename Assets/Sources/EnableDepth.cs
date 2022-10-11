using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnableDepth : MonoBehaviour
{
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        camera.depthTextureMode = DepthTextureMode.None;

#if UNITY_EDITOR
        // Force depth render in the editor.
        camera.forceIntoRenderTexture = false;
#endif
    }
}