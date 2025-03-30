using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.UI;
#endif

interface IInteractable
{
    public void Outline();
    public void NotOutline();

    public void ObjectTaked();
    public void ObjectNoTaked();

    bool IsTaked();
}

namespace StarterAssets
{
    public class MouseManager : MonoBehaviour
    {
        //Raycast//
        public Camera mainCamera;
        float distanceMax = 5; //Distancia del Raycast al impactar un objeto
        [SerializeField] private LayerMask placementLayerMask;
        [SerializeField] private IInteractable interactObj;

        //TakeObj//
        public bool objectSelect; //bool para saber si hay algun objeto en la mano
        [SerializeField]
        private GameObject objectHand; //obj que esta en la mano
        public GameObject poisitionHand; //posicion donde estara el objeto


        //Manager//
        private GameState gameState;

        //Builder
        Vector3 lastPositiom;
        public event Action OnClicked,OnExit;

        void Awake()
        {
            GameManager.StateChanged += GameManager_StateChanged;
            mainCamera = Camera.main;

        }

        void OnDestroy() //Buena practis quitar el evento del buffer 
        {
            GameManager.StateChanged -= GameManager_StateChanged;
        }

        private void GameManager_StateChanged(GameState state)
        {
            if (objectHand == true && state != GameState.Play) { throwObject(); }
            gameState = state;
        }
        void Update()
        {
            if (gameState == GameState.Play || gameState == GameState.Builder)
            {
                if (objectSelect) { ObjectMove(); }
                if (gameState == GameState.Builder)
                {
                    if (Input.GetMouseButtonDown(0)) OnClicked?.Invoke();
                    if (Input.GetKeyDown(KeyCode.Escape)) { OnExit?.Invoke(); GameManager.Instance.UpdateGameState(GameState.Play); }
                }
                CheckRayCast();
                if (objectSelect)
                    distanceMax = Mathf.Infinity;
                else
                    distanceMax = 5;
            }
        }

        public Vector3 CheckRayCast()
        {
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.nearClipPlane;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, distanceMax, placementLayerMask))
            {
                if (objectSelect && Input.GetMouseButtonDown(0))
                {
                    if (interactObj.IsTaked())
                    {
                        throwObject();
                        return new Vector3(0, 0, 0);
                    }
                }
                if (gameState == GameState.Builder)
                {
                    lastPositiom = hit.point;
                    return lastPositiom;
                }
                if (hit.collider.gameObject.TryGetComponent(out IInteractable prob) && gameState == GameState.Play)
                {
                    interactObj = prob;
                    interactObj.Outline();
                    if (Input.GetMouseButtonDown(0))
                    {
                        SwitchList(hit.collider.tag, hit.point, hit);
                    }
                    return new Vector3(0, 0, 0);
                }
                else if (interactObj != null)
                {
                    interactObj.NotOutline();
                }
            }
            return new Vector3(0,0,0);//Es null
        }

        public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        void ObjectMove()
        {
            Vector3 newPosition2 = Vector3.Lerp(objectHand.transform.position, poisitionHand.transform.position, Time.deltaTime * 50);
            objectHand.transform.position = newPosition2;
        }

            public Vector3 SwitchList(string _tag, Vector3 pos, RaycastHit hit)
            {
                switch (_tag)
                {
                    case "Interacteable":
                        if (!objectSelect && !interactObj.IsTaked())
                        {
                            objectHand = hit.collider.gameObject;
                            interactObj.NotOutline();
                            interactObj.ObjectTaked();
                            objectSelect = true;
                        }
                    return pos;
                case "Terrain":

                    return pos;
                default:
                    return pos;
                }
            }
            void throwObject()
            {
                interactObj.ObjectNoTaked();
                objectSelect = false;
                objectHand = null;
                interactObj = null;
            }
    }
}
