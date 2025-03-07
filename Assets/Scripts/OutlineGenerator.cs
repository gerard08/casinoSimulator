using UnityEngine;

public class OutlineGenerator : MonoBehaviour, IInteractable
{
    [SerializeField] 
    Material materialToUse;

    private float scale = 0.03f;
    public void Interact() {

        materialToUse = GetComponent<MeshRenderer>().materials[1];
        materialToUse.SetFloat("_Outline_Thickness", scale);
    }

    public void setter() {
    }
}
