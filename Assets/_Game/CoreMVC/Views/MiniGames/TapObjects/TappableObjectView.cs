public class TappableObjectView : PoolableView, IPressable
{
    public string Name => gameObject.name;
    
    public void OnTapped ()
    {
    }

    public void OnLongPressedBegan ()
    {
    }

    public void OnLongPressedEnded ()
    {
    }

    public void OnLongPressedCancelled ()
    {
    }
}