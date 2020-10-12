using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
// ScriptableObject Does not get reset when you reload the scene

//public class FloatValue : ScriptableObject
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    
    [HideInInspector]
    public float RuntimeValue;


    public void OnAfterDeserialize(){
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize(){}

    


}
