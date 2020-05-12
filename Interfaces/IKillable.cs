/// <summary>
/// Интерфейс представляющий смертный объект
/// </summary>
public interface IKillable 
{
    void TakeDamage(int damage);
    void Die();
}