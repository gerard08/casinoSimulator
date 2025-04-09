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
        [SerializeField] private IBuildingState interactObjRemove;
        [SerializeField] private PreviewSystem preview;
        //TakeObj//
        public bool objectSelect; //bool para saber si hay algun objeto en la mano
        [SerializeField]
        private GameObject objectHand; //obj que esta en la mano
        public GameObject poisitionHand; //posicion donde estara el objeto


        //Manager//
        private GameState gameState;

        //Builder
        Vector3 lastPosition;
        private bool isRemove;

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
            if (gameState != GameState.Tablet || gameState != GameState.Menu)
            {
                if (objectSelect) { /*ObjectMove();*/ }
                if (gameState != GameState.Play)
                {
                    if (Input.GetMouseButtonDown(0)) OnClicked?.Invoke();
                    if (Input.GetKeyDown(KeyCode.Escape)) { OnExit?.Invoke(); GameManager.Instance.UpdateGameState(GameState.Play); }
                }
                else
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
            if(mainCamera == null)
                mainCamera = Camera.main;
            mousePos.z = mainCamera.nearClipPlane;
            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out hit, distanceMax))
            {
                if (objectSelect && Input.GetMouseButtonDown(0)) //Dejar caer el objeto que llevas cogido
                {
                    if (interactObj.IsTaked())
                    {
                        throwObject();
                        return new Vector3(0, -2, 0);
                    }
                }
                if (gameState == GameState.Builder || gameState == GameState.Remove) //builder
                {
                    lastPosition = hit.point;
                    SwitchList(hit.collider.tag, lastPosition, hit);
                    return lastPosition;
                }
                if (hit.collider.gameObject.TryGetComponent(out IInteractable prob) && gameState == GameState.Play) //Coger objetos
                {
                    interactObj = prob;
                    return new Vector3(0, -2, 0);
                }
                SwitchList(hit.collider.tag, hit.point, hit);
            }
            return new Vector3(0, -2, 0);//Es null
        }

        public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

        void ObjectMove()
        {
            Vector3 newPosition2 = Vector3.Lerp(objectHand.transform.position, poisitionHand.transform.position, Time.deltaTime * 50);
            objectHand.transform.position = newPosition2;
        }

        public void SwitchList(string _tag, Vector3 pos, RaycastHit hit)
        {
            switch (_tag)
            {
            case "Interacteable":
                interactObj.Outline();
                if (Input.GetMouseButtonDown(0))
                {
                    SelectObject(hit.collider.gameObject);
                }
                break;
            case "Environment":
                if (gameState == GameState.Remove)
                {
                        Debug.Log("!");
                    preview.ShowPreviewObjRemove(hit.collider.gameObject);
                    isRemove = true;
                }
                break;
            default:
                if (interactObj != null)
                    interactObj.NotOutline();
                if(isRemove)
                {
                    //preview.StopPreviewObjRemove(hit.collider.gameObject);
                    //isRemove = false;
                }
                break;
            }
        }
        void throwObject()
        {
            interactObj.ObjectNoTaked();
            objectSelect = false;
            objectHand = null;
            interactObj = null;
        }

        void SelectObject(GameObject gameObject)
        {
            objectHand = gameObject;
            interactObj.NotOutline();
            interactObj.ObjectTaked();
            objectSelect = true;
        }
    }
}
