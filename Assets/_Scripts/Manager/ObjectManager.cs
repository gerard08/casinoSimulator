using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectManager : Singleton<ObjectManager>
{
    //Aqui se spawnea el objeto a partir de una ID + la posicion
    public void SpawnObjects(int id)
    {
        SpawnObject(id, new Vector3(0, 1, 0));
    }

    //Se envia el objeto y se Spawnea
    void SpawnObject(int t,Vector3 pos)
    {
        var _objectScripteable = ResourceSystem.Instance.GetObject(t);

        var spawned = Instantiate(_objectScripteable.prefab, pos, Quaternion.identity, transform);

        var stats = _objectScripteable.BaseStats;

        var statsObject = _objectScripteable.stateObject;

        var statsType = _objectScripteable.objectType;

        spawned.SetStats(stats);

        spawned.SetObjectState(statsObject);

        spawned.SetObjectType(statsType);

    }

}


