using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObject = new();
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        prefab.transform.position = position;
        placedGameObject.Add(prefab);
        return placedGameObject.Count - 1;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObject.Count <= gameObjectIndex)
            return;
        placedGameObject.Remove(placedGameObject[gameObjectIndex]);      // Delete the List item 
        //placedGameObject[gameObjectIndex] = null;
    }
}
