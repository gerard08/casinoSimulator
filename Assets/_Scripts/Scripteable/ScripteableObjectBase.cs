using UnityEngine;
using System;

public abstract class ScripteableObjectBase : ScriptableObject
{
    public ObjectState stateObject;

    [SerializeField] private Stats _stats;
    public Stats BaseStats => _stats;

    //prefab del objeto
    public Objects prefab;

    //Used in menus
    public string Description;
    public Sprite MenuSprite;
}

//No se que mas stats poner
[Serializable]
public struct Stats
{
    public int id;
    public float price;

}


[SerializeField]
public enum ObjectState
{
    NoTaked,
    Taked,
};