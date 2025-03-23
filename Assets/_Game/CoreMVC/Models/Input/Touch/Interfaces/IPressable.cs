public interface IPressable
{
    string Name { get; }
    
    void OnTapped ();
    void OnLongPressedBegan ();
    void OnLongPressedEnded ();
    void OnLongPressedCancelled ();
}