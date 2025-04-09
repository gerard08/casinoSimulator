using StarterAssets;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class ObjectManager : Singleton<ObjectManager>
{
    [SerializeField] private GameObject positionBuilder;

    [SerializeField] public GameObject lastObject;

    Objects _LastObjectsScript;
    public List<ObjectType> builderObjects { get; private set; }

    //Aqui se spawnea el objeto a partir de una ID + la posicion
    public void SpawnObjects(int id)
    {
        SpawnObject(id, positionBuilder.transform.position);
    }

    //Se envia el objeto y se Spawnea
    void SpawnObject(int id, Vector3 pos)
    {
        var _objectScripteable = ResourceSystem.Instance.GetObject(id);

        var spawned = Instantiate(_objectScripteable._Objects, pos, Quaternion.identity);

        var stats = _objectScripteable.BaseStats;

        lastObject = spawned.gameObject;

        _LastObjectsScript = spawned;

        var statsObject = _objectScripteable.stateObject;

        var statsType = _objectScripteable.objectType;

        spawned.SetStats(stats);

        spawned.SetObjectState(statsObject);

        spawned.SetObjectType(statsType);
    }

    public GameObject GetObj()
    {
        return lastObject;
    }

    public void SetObjType()
    {
        _LastObjectsScript.SetObjectType(ObjectType.Environment);
        _LastObjectsScript.ObjectPlace();
    }
}


