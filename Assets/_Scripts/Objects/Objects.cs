using UnityEngine;

public class Objects : ObjectsBase, IInteractable
{
    //Components Object//
    public BoxCollider boxCollider;
    public Rigidbody rb;

    //Outline//
    private Material outline;

    private float scale = 0.03f;
    private float Noscale = 0.0f;

    //network//
    Transform positionX;

    //GameManager//
    bool isPlaying;

    private void Start()
    {
        outline = GetComponent<MeshRenderer>().materials[1];

        /*rb = GetComponent<Rigidbody>();

        if (_objectType == ObjectType.Environment) {rb.constraints = RigidbodyConstraints.FreezeAll; }
        else if (_objectType == ObjectType.Interactuable || _objectType == ObjectType.Builder) { rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None; }

        //stateObject = ObjectState.NoTaked;*/
    }
    void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;
        ObjectPlace();
    }

    public void OnDestroy() //Buena practis quitar el evento del buffer 
    {
        GameManager.StateChanged -= GameManager_StateChanged;
    }

    private void GameManager_StateChanged(GameState state)
    {
        if(state == GameState.Remove) 
        { 
            if (_objectType == ObjectType.Environment)
            {
                boxCollider.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
    public override void Outline() {
        if(_objectType == ObjectType.Interactuable)
        outline.SetFloat("_Outline_Thickness", scale);
    }
    
    public override void ObjectPlace()
    {
        if (_objectType == ObjectType.Environment)
        {
            boxCollider = GetComponent<BoxCollider>();
            boxCollider.GetComponent<BoxCollider>().enabled = true;
        }
        else if (_objectType == ObjectType.Builder)
            boxCollider.GetComponent<BoxCollider>().enabled = false;
    }

    public override void NotOutline()
    {
        if (_objectType == ObjectType.Interactuable)
            outline.SetFloat("_Outline_Thickness", Noscale);
    }

    public override void ObjectNoTaked()
    {
        if (_objectType == ObjectType.Interactuable)
        {
            boxCollider.enabled = true;
            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
            outline.SetFloat("_Outline_Thickness", 0.01f);
            SetObjectState(ObjectState.NoTaked);
        }    
    }

    public override void ObjectTaked()
    {
        if (_objectType == ObjectType.Interactuable)
        {
            boxCollider.enabled = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            SetObjectState(ObjectState.Taked);
        }
    }

    /*public Vector3 objectHost()
    {
        Vector3 vector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        return vector3;
    }*/

}
