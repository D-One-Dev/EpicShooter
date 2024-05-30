using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Image[] healthUI;
    [SerializeField] private float damageRecoilTime;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private AudioClip hitSound;
    private bool isProtected;
    private Coroutine hurtAnimation;

    [SerializeField] private Animator _animator;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void ChangeHealth(int value)
    {
        if(value > 0)
        {
            if(currentHealth < maxHealth) currentHealth += value;
        }
        else if (value < 0)
        {
            if (!isProtected)
            {
                StartCoroutine(DamageRecoil());
                currentHealth += value;
                _animator.SetTrigger("Hurt");
                CameraShake.instance.ShakeCamera();
                SoundController.instance.PlaySound(hitSound);
            }
            if(currentHealth <= 0)
            {
                //Death
                PlayerPrefs.SetInt("Score", ScoreController.instance.score);
                SceneManager.LoadScene("Death Scene");
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            if(i < currentHealth) healthUI[i].enabled = true;
            else healthUI[i].enabled = false;
        }
    }

    private IEnumerator DamageRecoil()
    {
        isProtected = true;
        hurtAnimation = StartCoroutine(DamageAnimation());

        yield return new WaitForSeconds(damageRecoilTime);
        isProtected = false;
        StopCoroutine(hurtAnimation);
    }

    private IEnumerator DamageAnimation()
    {
        playerSprite.enabled = false;
        yield return new WaitForSeconds(.125f);
        playerSprite.enabled = true;
        yield return new WaitForSeconds(.125f);
        hurtAnimation = StartCoroutine(DamageAnimation());
    }
}
