using UnityEngine;

public class FindableObjectView : PoolableView
{
    [field: SerializeField] public Renderer Renderer { get; private set; }

    [SerializeField] Material findableMaterial;
    [SerializeField] Material normalMaterial;

    public void Setup (bool isFindable)
    {
        Renderer.receiveShadows = !isFindable;
        Renderer.material = isFindable ? findableMaterial : normalMaterial;
    }
}