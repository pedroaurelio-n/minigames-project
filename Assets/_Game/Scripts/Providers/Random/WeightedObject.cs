public class WeightedObject<T>
{
    public T Obj;
    public float Weight;
    
    public WeightedObject (T obj, float weight)
    {
        Obj = obj;
        Weight = weight;
    }
}