public interface IKillable 
{
    int Health { get; set; }
    void TakeDamage(int damage);
    void Die();
}
