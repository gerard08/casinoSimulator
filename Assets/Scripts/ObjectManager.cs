using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum ObjectState
{
    NoTaked,
    Taked,
};
public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    protected int id;
    [SerializeField]
    public ObjectState state;
    [SerializeField]
    public bool objTaken;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void ObjectNoTaked() {    }
    public virtual void ObjectTaked() {    }
    public virtual void Outline() {    }
    public virtual void NotOutline() {    }
    public virtual bool IsTaked() { return state == ObjectState.Taked; ; }
}
