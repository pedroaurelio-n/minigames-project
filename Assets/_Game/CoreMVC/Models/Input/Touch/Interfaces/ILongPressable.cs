public interface ILongPressable
{
    string Name { get; }
    
    void OnLongPressBegan ();
    void OnLongPressEnded ();
    void OnLongPressCancelled ();
}