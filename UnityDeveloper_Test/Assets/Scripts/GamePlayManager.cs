using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;

    [Header("UI Elements")]
    public TMP_Text timerText;
    public TMP_Text ConclusionText;
    public TMP_Text BoxesLeftText;

    [Header("Game Settings")]
    private float timeLimit = 120f; // Timer limit in seconds (2 minutes)
    private int boxCount = 5;

    [Header("References")]
    public Transform playerTransform;
    public Transform placeHolderTransform;
    public Button StartButton;
    public Button RestartButton;
    public GameObject[] Cubes = new GameObject[5];

    public enum State
    {
        Start,
        Gameplay,
        End
    }

    public State gameState = State.Start;

    private void Awake()
    {
        instance = this;
        StartButton.onClick.AddListener(StartGameplay);
        RestartButton.onClick.AddListener(StartGameplay);
    }

    private void Update()
    {
        if (gameState == State.Gameplay)
        {
            timeLimit -= Time.deltaTime;
            UpdateTimerText();

            if (boxCount == 0)
            {
                EndGame("WIN");
            }

            if (timeLimit <= 0)
            {
                EndGame("LOSE");
            }
        }
    }

    private void StartGameplay()
    {
        ConclusionText.gameObject.SetActive(false);
        gameState = State.Gameplay;
        RestartButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
    }

    public void GameplayEnded()
    {
        ResetGame();
        Physics.gravity = new Vector3(0, -200, 0);
        playerTransform.position = placeHolderTransform.position;
        playerTransform.rotation = placeHolderTransform.rotation;
        RestartButton.gameObject.SetActive(true);
        timerText.text = $"Timer - 02:00";
    }

    private void EndGame(string result)
    {
        ConclusionText.gameObject.SetActive(true);
        ConclusionText.text = result;
        GameplayEnded();
    }

    private void ResetGame()
    {
        timeLimit = 120f;
        gameState = State.End;
        boxCount = 5;
        BoxesLeftText.text = $"Cubes Left: {boxCount}";

        foreach (GameObject cube in Cubes)
        {
            cube.SetActive(true);
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLimit / 60f);
        int seconds = Mathf.FloorToInt(timeLimit % 60f);
        timerText.text = $"Timer - {minutes:00}:{seconds:00}";
    }

    public void UpdateBoxes()
    {
        boxCount--;
        BoxesLeftText.text = $"Cubes Left: {boxCount}";
    }
}
