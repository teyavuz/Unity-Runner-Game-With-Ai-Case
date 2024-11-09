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
    [SerializeField] TextMeshProUGUI countDownText;

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

        while (countdownValue > 0)
        {
            countDownText.text = countdownValue.ToString();
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        // cd -> 0
        countDownText.text = "0 \n RACE!";
        gameState = GameStates.race;
        yield return new WaitForSeconds(1f);
        Destroy(countDownText);
    }
}
