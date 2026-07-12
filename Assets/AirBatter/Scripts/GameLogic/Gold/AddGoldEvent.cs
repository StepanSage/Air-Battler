public class AddGoldEvent: IEvent
{
    public uint Add { get; private set; }

    public AddGoldEvent(uint add)
    {
        Add = add;
       
    }
}

