using TMPro;
using UnityEngine;

public class EndGameScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    void Start()
    {
        scoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
    }
}
