using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsBase : MonoBehaviour
{
    [SerializeField] public Stats _stats {  get; private set; } 
    [SerializeField] public ObjectState _objectState { get; private set; }
    [SerializeField] public ObjectType _objectType { get; private set; }


    public ScripteableObjectBase _object;

    public virtual void SetStats(Stats stats) => _stats = stats;
    public virtual void SetObjectState(ObjectState _stats) => _objectState = _stats;
    public virtual void SetObjectType(ObjectType _stats) => _objectType = _stats;

    public virtual void ObjectNoTaked() { }
    public virtual void ObjectTaked() { }
    public virtual void Outline() { }
    public virtual void NotOutline() { }
    public virtual bool IsTaked() { if(_objectState == ObjectState.Taked) return true; return false; }
    public virtual bool IsBuilder() { Debug.Log("!"); if(_objectType == ObjectType.Builder) return true; return false; }

}
