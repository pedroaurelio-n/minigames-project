using UnityEngine;

//TODO pedro: refactor this Pressable implementation
public class FloatingObjectView : PoolableView, ITappable
{
    [SerializeField] GameObject[] meshes;
    
    public string Name => gameObject.name;
    public bool IsObjective { get; private set; }

    float _speed;
    float _delayTimer;

    public void Setup (bool isObjective, float speed, float delay)
    {
        IsObjective = isObjective;
        _speed = speed;
        _delayTimer = delay;

        meshes[0].SetActive(isObjective);
        meshes[1].SetActive(!isObjective);
    }
    
    public void MoveUpwards ()
    {
        if (_delayTimer > 0f)
        {
            _delayTimer -= Time.deltaTime;
            return;
        }
        
        transform.position += _speed * Time.deltaTime * Vector3.up;
    }
    
    public void OnTapped ()
    {
    }
}