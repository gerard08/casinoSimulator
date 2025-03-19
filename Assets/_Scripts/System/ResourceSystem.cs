using UnityEngine;
using System.Linq;
using System.Collections.Generic;
public class ResourceSystem : Singleton<ResourceSystem>
{

    public List<ScripteableObject> scripteableObjects {  get; private set; }
    private Dictionary<int, ScripteableObject> _ObjectDict;

    protected override void Awake()
    {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources()
    {
        scripteableObjects = Resources.LoadAll<ScripteableObject>("Objects").ToList();
        _ObjectDict = scripteableObjects.ToDictionary(r => r.BaseStats.id, r => r);

      
        listObjectsID();
    }

    public ScripteableObject GetObject(int t) => _ObjectDict[t];

    void listObjectsID()
    {
        for (int i = 0; i < scripteableObjects.Count; i++) {

            Debug.Log(i);
        }
    }
}
