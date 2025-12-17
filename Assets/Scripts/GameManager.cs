using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private PlayerController playerController;

    private int coinCount = 0;
    private int gemCount = 0;
    private bool isGameOver = false;
    private Vector3 playerPosition;

    // Level Complete / Game Over Panel
    [SerializeField] GameObject levelCompletePanel;
    [SerializeField] TMP_Text leveCompletePanelTitle;
    [SerializeField] TMP_Text levelCompleteCoins;

    private int totalCoins = 0;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        UpdateGUI();

        if (UIManager.instance != null)
            UIManager.instance.fadeFromBlack = true;

        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();

        if (playerController != null)
            playerPosition = playerController.transform.position;
        else
            Debug.LogError("GameManager: playerController олдсонгүй!", this);

        FindTotalPickups();
    }

    // -------------------------
    // UI & Pickups
    // -------------------------
    public void IncrementCoinCount()
    {
        coinCount++;
        UpdateGUI();
    }

    public void IncrementGemCount()
    {
        gemCount++;
        UpdateGUI();
    }

    private void UpdateGUI()
    {
        if (coinText != null)
            coinText.text = coinCount.ToString();
    }

    public void FindTotalPickups()
    {
        pickup[] pickups = GameObject.FindObjectsOfType<pickup>();

        foreach (pickup pickupObject in pickups)
        {
            if (pickupObject.pt == pickup.pickupType.coin)
                totalCoins++;
        }
    }

    // -------------------------
    // DEATH (дахин эхэлдэг)
    // -------------------------
    public void Death()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (UIManager.instance != null)
        {
            UIManager.instance.DisableMobileControls();
            UIManager.instance.fadeToBlack = true;
        }

        if (playerController != null)
            playerController.gameObject.SetActive(false);

        StartCoroutine(DeathCoroutine());

        Debug.Log("Died");
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1f);

        if (playerController != null)
            playerController.transform.position = playerPosition;

        yield return new WaitForSeconds(1f);

        // ✅ Death үед л дахин эхэлнэ
        SceneManager.LoadScene(1);
    }

    // -------------------------
    // GAME OVER (Timer дуусахад)
    // -------------------------
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (UIManager.instance != null)
        {
            UIManager.instance.DisableMobileControls();
            UIManager.instance.fadeToBlack = true;
        }

        if (playerController != null)
            playerController.gameObject.SetActive(false);

        ShowGameOverPanel();
    }

    private void ShowGameOverPanel()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        if (leveCompletePanelTitle != null)
            leveCompletePanelTitle.text = "GAME OVER";

        if (levelCompleteCoins != null)
            levelCompleteCoins.text =
                "COINS COLLECTED: " + coinCount + " / " + totalCoins;
    }

    // -------------------------
    // LEVEL COMPLETE
    // -------------------------
    public void LevelComplete()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        if (leveCompletePanelTitle != null)
            leveCompletePanelTitle.text = "LEVEL COMPLETE";

        if (levelCompleteCoins != null)
            levelCompleteCoins.text =
                "COINS COLLECTED: " + coinCount + " / " + totalCoins;
    }
}
