using UnityEngine;

public class CloseRangeEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    public void Hurt(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
