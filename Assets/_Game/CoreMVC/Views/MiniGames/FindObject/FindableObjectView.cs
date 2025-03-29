using UnityEngine;

public class FindableObjectView : MonoBehaviour
{
    [field: SerializeField] public Renderer Renderer { get; private set; }

    [SerializeField] Material findableMaterial;
    [SerializeField] Material normalMaterial;

    public void SetMaterial (bool isFindable) => Renderer.material = isFindable ? findableMaterial : normalMaterial;
}