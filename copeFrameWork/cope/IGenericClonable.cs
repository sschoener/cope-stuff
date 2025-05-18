namespace cope
{
    public interface IGenericClonable<out T>
    {
        T GClone();
    }
}