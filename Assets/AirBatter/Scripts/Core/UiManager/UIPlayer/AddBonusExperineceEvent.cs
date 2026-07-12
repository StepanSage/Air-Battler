
public struct AddBonusExperineceEvent : IEvent
{
    public int BonusExperience { get; private set; }

    public AddBonusExperineceEvent(int bonusExperience = 1)
    {
        BonusExperience = bonusExperience;
    }
}
