public  class RemoveGoldEvent: IEvent
{
    public uint Remove { get; private set; }
    public RemoveGoldEvent(uint remove)
    {
        Remove = remove;
    }
}

