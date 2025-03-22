using UnityEngine;

[CreateAssetMenu(fileName = "ObjectBase")]
public class ScripteableObject : ScripteableObjectBase
{
    public ObjectType objectType;
}

public enum ObjectType
{
    Interactuable,
    Environment,
};