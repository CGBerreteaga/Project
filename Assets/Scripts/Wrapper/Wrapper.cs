using UnityEngine;

public abstract class Wrapper
{
    [SerializeField] protected AttributeScriptableObject attribute;
    public AttributeScriptableObject Attribute => attribute;
    [SerializeField] protected float amount;
    public float Amount => amount;

}
