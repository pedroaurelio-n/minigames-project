[System.Flags]
public enum TouchInputType
{
    //TODO pedro: assign joystick and package/asset input types here?
    None = 0,
    Tap = 1 << 0,
    Swipe = 1 << 1,
    LongPress = 1 << 2,
    TwoPointMove = 1 << 3,
    TwoPointZoom = 1 << 4,
    Drag = 1 << 5
}