public class LostGameEvent : IEvent
{
    public int AmmountHelth;

    public LostGameEvent(int ammountHelth)
    {
        AmmountHelth = ammountHelth;
    }
}
