using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private Image[] healthUI;
    [SerializeField] private float damageRecoilTime;
    [SerializeField] private SpriteRenderer playerSprite;
    private bool isProtected;
    private Coroutine hurtAnimation;

    [SerializeField] private Animator _animator;
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
            }
            if(currentHealth <= 0)
            {
                //Death
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
