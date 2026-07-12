

public class AbilityAddExperrinece : Ability
{
    public override void Useing()
    {
        _eventBus?.Publish(new AddBonusExperineceEvent(AbilityConfig.ExperienceMultiplier));
    }
}
