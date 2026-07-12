public struct EnemyUpdateEvent: IEvent
{
    public int Health { get; private set; }

    public EnemyUpdateEvent(int health)
    {
        Health = health;
    }
}

