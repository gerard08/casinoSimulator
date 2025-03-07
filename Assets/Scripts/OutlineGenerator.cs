using UnityEngine;

public class OutlineGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] 
    Material materialToUse;

    private float scale = 1.05f;
    public void Interact(Material mat) {

        MeshRenderer materialToUse = GetComponent<MeshRenderer>();
        materialToUse.materials[1] = mat;
        materialToUse.materials[1].SetFloat("_Scale", scale);
    }

    public void setter() {
    }
}
