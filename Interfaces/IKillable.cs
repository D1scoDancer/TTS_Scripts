/// <summary>
/// Interface representing a mortal object
/// </summary>
public interface IKillable 
{
    void TakeDamage(int damage);
    void Die();
}