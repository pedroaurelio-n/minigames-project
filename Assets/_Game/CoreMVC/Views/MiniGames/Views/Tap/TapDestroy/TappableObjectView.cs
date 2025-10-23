public class TappableObjectView : PoolableView, ITappable
{
    public string Name => gameObject.name;
    
    public void OnTapped ()
    {
    }
}