using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    [SerializeField] private TMP_Text scoreText;
    public int score;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}
