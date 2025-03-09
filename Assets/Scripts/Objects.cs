using UnityEngine;

public class OutlineGenerator : ObjectManager, IInteractable
{
    //Components Object//
    public BoxCollider boxCollider;
    public Rigidbody rb;


    //Outline//
    [SerializeField] 
    Material outline;

    private float scale = 0.03f;
    private float Noscale = 0.0f;

    //ObjectTake//



    private void Start()
    {
        outline = GetComponent<MeshRenderer>().materials[1];

        boxCollider = GetComponent<BoxCollider>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;

        state = ObjectState.NoTaked;
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
        state = ObjectState.NoTaked;
    }

    public override void ObjectTaked()
    {
        boxCollider.enabled = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        state = ObjectState.Taked;
    }

    public override bool IsTaked() {  return state == ObjectState.Taked; }

}
