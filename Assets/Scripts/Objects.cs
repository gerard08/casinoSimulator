using UnityEngine;

public class OutlineGenerator : MonoBehaviour, IInteractable
{
    //Outline//
    [SerializeField] 
    Material materialToUse;

    private float scale = 0.03f;
    private float Noscale = 0.0f;

    //ObjectTake//
    [SerializeField]
    private bool objTaken;
    private void Start()
    {
        materialToUse = GetComponent<MeshRenderer>().materials[1];
    }

    public void Outline() {

        materialToUse.SetFloat("_Outline_Thickness", scale);
    }

    public void NotOutline()
    {

        materialToUse.SetFloat("_Outline_Thickness", Noscale);
    }
}
