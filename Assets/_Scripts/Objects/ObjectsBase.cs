using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsBase : MonoBehaviour
{
    [SerializeField] public Stats _stats {  get; private set; } 
    [SerializeField] public ObjectState _objectState { get; private set; }

    public ScripteableObjectBase _object;

    public virtual void SetStats(Stats stats) => _stats = stats;
    public virtual void SetObjectState(ObjectState _stats) => _objectState = _stats;

    public virtual void ObjectNoTaked() { }
    public virtual void ObjectTaked() { }
    public virtual void Outline() { }
    public virtual void NotOutline() { }
    public virtual bool IsTaked() { return _objectState == ObjectState.Taked;}
}
