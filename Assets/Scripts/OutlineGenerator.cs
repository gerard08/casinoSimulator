using UnityEngine;

public class OutlineGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] 
    Material materialToUse;

    private float scale = 1.05f;
    public void Interact() {
    
        materialToUse = GetComponent<MeshRenderer>().materials[1];
        materialToUse.SetFloat("_Scale", scale);
    }

    public void setter() {
    }
}
