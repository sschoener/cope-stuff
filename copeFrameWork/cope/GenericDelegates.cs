namespace cope
{
    public delegate void GenericHandler<in T>(object sender, T t);

    public delegate void NotifyEventHandler();
}