using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class UIController : MonoBehaviour
{
    [Header("Componenti madre")]
    [SerializeField] PlayerController playerController;

    [Header("GameRef")]
    public TextMeshProUGUI CoinCounter;
    public TextMeshProUGUI EndGameCounter;
    public TextMeshProUGUI CheckPoint;
    public TextMeshProUGUI StartText;
    public TextMeshProUGUI MaxPossibilities;
    public Scrollbar LifeBar;



    private int coins = 0;
    private float EndGametimer = 180;
    private int CoinToWin;
    private bool IsShowingChePointUI = false;


    void Awake()
    {
        CheckPoint.gameObject.SetActive(false);
        StartText.gameObject.SetActive(false);
    }
    void Start()
    {
        StartCoroutine(StartTextUI());
    }
    void Update()
    {
        EndGameCountDownUI();
        LIfeBarUI();
        MaxPossibilitiesUI();
        CoinsToWinUI();
        if (playerController.CheckPointSet == true && IsShowingChePointUI == false)
        {
            StartCoroutine(CheckPointUI());
        }
    }
    public void AddCoin()
    {
        coins++;
        CoinToWin++;
        UpdateUI();
    }

    void UpdateUI()
    {
        CoinCounter.text = "Flower: " + coins;
    }
    void EndGameCountDownUI()
    {
        EndGametimer -= Time.deltaTime;
        EndGameCounter.text = "Remain: " + EndGametimer.ToString("F0") + "s";
        if (EndGametimer <= 0)
        {
            SceneManager.LoadScene("LoosePanel");
        }
    }

    void LIfeBarUI()
    {
        float _barFill = (float)playerController.currentHealth / playerController.maxHealth;
        LifeBar.image.fillAmount = _barFill;
    }
    void MaxPossibilitiesUI()
    {
        MaxPossibilities.text = playerController.maxPossibilities.ToString();
    }
    void CoinsToWinUI()
    {
        if (CoinToWin >= 20)
        {
            SceneManager.LoadScene("WinPanel");
        }
    }
    private IEnumerator CheckPointUI()
    {
        CheckPoint.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        CheckPoint.gameObject.SetActive(false);
        IsShowingChePointUI = true;
    }
    private IEnumerator StartTextUI()
    {
        StartText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        StartText.gameObject.SetActive(false);
    }
}
