using UnityEngine;

[CreateAssetMenu(fileName = "Enums", menuName = "ScriptableObjects/Enums")]
public class EnumsScriptableObject : ScriptableObject {
    public override string ToString()
    {
        return name;
    }
}