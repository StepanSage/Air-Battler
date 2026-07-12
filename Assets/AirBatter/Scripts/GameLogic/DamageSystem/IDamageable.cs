public interface IDamageable
{
    public void TakeDamage(int damage, WhoDamage whoDamage);
}
public enum WhoDamage
{
    None,
    Player,
    Enemy
}
