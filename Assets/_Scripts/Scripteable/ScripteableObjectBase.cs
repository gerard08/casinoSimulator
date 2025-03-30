using UnityEngine;
using System;
using System.Collections;
using NUnit.Framework;


[Serializable]
public abstract class ScripteableObjectBase : ScriptableObject
{
    public ObjectType objectType;

    public ObjectState stateObject;

    [SerializeField] private Stats _stats;
    [SerializeField] public Stats BaseStats => _stats;

    //prefab del objeto
    public Objects _Objects;

    //Used in menus
    public string Description;
    public Sprite MenuSprite;
}

[Serializable]
public struct Stats
{
    [field: SerializeField] public int ID;
    [field: SerializeField] public float price { get; private set; }
    [field: SerializeField] public Vector2Int Size { get; private set; }
    [field: SerializeField] public GameObject Prefab {  get; private set; }

}



[SerializeField]
public enum ObjectState
{
    NoTaked,
    Taked,
};

public enum ObjectType
{
    Interactuable,
    Environment,
    Builder
};