using UnityEngine;

public class LongRangeEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health;
    public void Hurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            ScoreController.instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}
