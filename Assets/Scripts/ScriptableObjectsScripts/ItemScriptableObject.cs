using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Enums", menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
