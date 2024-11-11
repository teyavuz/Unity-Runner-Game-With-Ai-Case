using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        startCountdown,
        race,
        painting,
        end
    }

    public GameStates gameState;
    [SerializeField] private TextMeshProUGUI countDownText;

    [SerializeField] private GameObject[] allCanvases;

    public static GameManager Instance;

    private void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        gameState = GameStates.startCountdown;
        StartCoroutine(StartCountdown(3));
    }

    private IEnumerator StartCountdown(int startValue)
    {
        int countdownValue = startValue;

        AudioManager.Instance.PlayMusic(0);
        while (countdownValue > 0)
        {
            countDownText.text = countdownValue.ToString();
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        // cd -> 0
        countDownText.text = "0 \n RACE!";
        gameState = GameStates.race;
        AudioManager.Instance.PlayMusic(1);
        yield return new WaitForSeconds(1f);
        Destroy(countDownText);
    }


    public void OnCompleteGame()
    {
        gameState = GameStates.end;
    }

    public void OpenSpecificCanvas(int i)
    {
        StartCoroutine(WaitMe(4f));
        allCanvases[i].SetActive(true);
    }

    public void CloseAllCanvases()
    {
        foreach (var item in allCanvases)
        {
            item.SetActive(false);
        }
    }

    public void EndGameButton()
    {
         Application.OpenURL("https://www.panteon.games/en/");

         StartCoroutine(WaitMe(2f));

         Application.Quit();
    }

    private IEnumerator WaitMe(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
