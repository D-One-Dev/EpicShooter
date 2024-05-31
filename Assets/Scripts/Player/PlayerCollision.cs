using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private AudioClip healSound;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Aid"))
        {
            _playerHealth.ChangeHealth(1);
            SoundController.instance.PlaySound(healSound);
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Level End"))
        {
            PlayerPrefs.SetInt("Score", ScoreController.instance.score);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Win Scene");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerHealth.ChangeHealth(-1);
        }
    }
}