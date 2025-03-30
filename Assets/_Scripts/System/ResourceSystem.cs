using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class ResourceSystem : Singleton<ResourceSystem>
{

    public List<ScripteableObject> scripteableObjects {  get; private set; }
    private Dictionary<ObjectType, ScripteableObject> _ObjectType;
    private Dictionary<int, ScripteableObject> _ObjectID;


    protected override void Awake()
    {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources()
    {
        scripteableObjects = Resources.LoadAll<ScripteableObject>("Objects").ToList();
        //_ObjectType = scripteableObjects.ToDictionary(r => r.objectType, r => r);
        _ObjectID = scripteableObjects.ToDictionary(r => r.BaseStats.ID, r => r);
    }

    public ScripteableObject GetObject(ObjectType _objectType) => _ObjectType[_objectType];
    public ScripteableObject GetObject(int id) => _ObjectID[id];

}
