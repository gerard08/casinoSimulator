using StarterAssets;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class ObjectManager : Singleton<ObjectManager>
{
    [SerializeField] private GameObject positionBuilder;

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

        var spawned = Instantiate(_objectScripteable.prefab, pos, Quaternion.identity, transform);

        var stats = _objectScripteable.BaseStats;

        var statsObject = _objectScripteable.stateObject;

        var statsType = _objectScripteable.objectType;

        spawned.SetStats(stats);

        spawned.SetObjectState(statsObject);

        spawned.SetObjectType(statsType);
    }

    public GameObject SeacrhObj()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Environment");

        Debug.Log(objetos);

        foreach (GameObject obj in objetos)
        {

            Objects _objetos = GetComponent<Objects>();
            if(_objetos.IsBuilder())
            {
                return obj;
            }
        }

        return null;
    }

}


