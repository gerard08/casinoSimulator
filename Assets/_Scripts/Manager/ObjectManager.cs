using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectManager : Singleton<ObjectManager>
{
    //Aqui se spawnea el objeto a partir de una ID + la posicion
    public void SpawnObjects(int id)
    {
        SpawnObject(id, new Vector3(1, 0, 0));
    }

    //Se envia el objeto y se Spawnea
    void SpawnObject(int t,Vector3 pos)
    {
        var _objectScripteable = ResourceSystem.Instance.GetObject(t);

        var spawned = Instantiate(_objectScripteable.prefab, pos, Quaternion.identity, transform);

        var stats = _objectScripteable.BaseStats;

        spawned.SetStats(stats);
    }

}


/*
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
   public ObjectState stateObject;
   [SerializeField]
   public bool objTaken;

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   public virtual void ObjectNoTaked() {    }
   public virtual void ObjectTaked() {    }
   public virtual void Outline() {    }
   public virtual void NotOutline() {    }
   public virtual bool IsTaked() { return stateObject == ObjectState.Taked; ; }
}*/