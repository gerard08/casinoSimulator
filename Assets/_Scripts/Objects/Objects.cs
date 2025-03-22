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

        boxCollider = GetComponent<BoxCollider>();

        rb = GetComponent<Rigidbody>();

        if (_objectType == ObjectType.Environment) {rb.constraints = RigidbodyConstraints.FreezeAll; }
        else if (_objectType == ObjectType.Interactuable) { rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None; }

        //stateObject = ObjectState.NoTaked;
    }
    void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;
    }

    void OnDestroy() //Buena practis quitar el evento del buffer 
    {
        GameManager.StateChanged -= GameManager_StateChanged;
    }

    private void GameManager_StateChanged(GameState state)
    {
        if(state != GameState.Play) { //stateObject = ObjectState.NoTaked
                                      ;}
    }
    public override void Outline() {

        outline.SetFloat("_Outline_Thickness", scale);
    }

    public override void NotOutline()
    {

        outline.SetFloat("_Outline_Thickness", Noscale);
    }

    public override void ObjectNoTaked()
    {

        boxCollider.enabled = true;
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        outline.SetFloat("_Outline_Thickness", 0.01f);
        SetObjectState(ObjectState.NoTaked);

    }

    public override void ObjectTaked()
    {
        boxCollider.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        SetObjectState(ObjectState.Taked);
        Debug.Log(_objectState);

    }

    public Vector3 objectHost()
    {
        Vector3 vector3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        return vector3;
    }

}
