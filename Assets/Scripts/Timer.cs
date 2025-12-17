using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] float remainingTime = 300f;

    bool timeUp = false;

    void Update()
    {
        if (timeUp) return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                TimeUp();   // ⬅ ЭНД
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void TimeUp()
    {
        timeUp = true;

        // ⬇⬇⬇ ЯГ ЭНД Л БАЙРЛУУЛНА ⬇⬇⬇
        if (GameManager.instance != null)
            GameManager.instance.GameOver();

        Time.timeScale = 0f; // хэрвээ pause хийх бол
    }
}
