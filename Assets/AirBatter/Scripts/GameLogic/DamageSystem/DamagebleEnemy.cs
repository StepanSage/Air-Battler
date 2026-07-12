public class DamagebleEnemy : Damageable
{
    protected override void Start()
    {
        base.Start();
        _eventBus.Subscribe<EnemyUpdateEvent>(UpdateHealth);
        UpdateHealth(new(4));
        ResetHealth();
    }
    private void UpdateHealth(EnemyUpdateEvent enemyUpdateEvent)
    {
        MaxHealth = enemyUpdateEvent.Health;
        
        
    }
}
