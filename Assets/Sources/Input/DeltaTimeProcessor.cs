using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class DeltaTimeProcessor : InputProcessor<float>
{
#if UNITY_EDITOR
    static DeltaTimeProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<DeltaTimeProcessor>();
    }

    public override float Process(float value, InputControl control)
    {
        return value * Time.deltaTime;
    }
}