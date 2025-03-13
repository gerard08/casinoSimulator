using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable
{
    public void Outline();
    public void NotOutline();

    public void ObjectTaked();
    public void ObjectNoTaked();

    bool IsTaked();
}


public class MouseManager : MonoBehaviour
{
    //Raycast//
    public Camera mainCamera;
    float distanceMax = 5; //Distancia del Raycast al impactar un objeto
    [SerializeField]
    private IInteractable interactObj;

    //TakeObj//
    public bool objectSelect; //bool para saber si hay algun objeto en la mano
    [SerializeField]
    private GameObject objectHand; //obj que esta en la mano
    public GameObject poisitionHand; //posicion donde estara el objeto


    void Start()
    {
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckRayCast();
        if (objectSelect)
            distanceMax = Mathf.Infinity;
        else
            distanceMax = 5;

    }

    void CheckRayCast()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, distanceMax, LayerMask.NameToLayer("NoInteractable")))
        {
            if (hit.collider.tag == "Interacteable" && hit.collider.gameObject.TryGetComponent(out IInteractable prob) && !objectSelect)
            {
                Debug.Log("enter1");
                interactObj = prob;
                interactObj.Outline();
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("enter2");
                    string _tag = hit.collider.tag;
                    objectHand = hit.collider.gameObject;
                    SwitchList(objectHand, _tag);
                }
            }
            else if ((hit.collider.tag != "Interactable" && interactObj != null))
            {
                interactObj.NotOutline();
            }

            if (objectSelect)
            {
                ObjectMove();
                if (Input.GetMouseButtonDown(0))
                {
                    string _tag = hit.collider.tag;
                    objectHand = hit.collider.gameObject;
                    SwitchList(objectHand, _tag);
                }
            }
        }
    }

    void ObjectMove()
    {
        Vector3 newPosition2 = Vector3.Lerp(objectHand.transform.position, poisitionHand.transform.position, Time.deltaTime * 50);
        objectHand.transform.position = newPosition2;
    }
    void SwitchList(GameObject _object, string _tag)
    {
        switch (_tag)
        {
            case "Interacteable":
                if (!objectSelect)
                {
                    interactObj.NotOutline();
                    interactObj.ObjectTaked();
                    objectSelect = true;
                }
                break;
            default:
                if (objectSelect)
                {
                    if(interactObj.IsTaked())
                    {
                        objectHand.transform.position = new Vector3(objectHand.transform.position.x, objectHand.transform.position.y, objectHand.transform.position.z);
                        interactObj.ObjectNoTaked();
                        objectSelect = false;
                        objectHand = null;
                    }
                }
                break;
        }
    }
}
