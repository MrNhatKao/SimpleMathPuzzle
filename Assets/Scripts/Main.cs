using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  [Header("Game Elements")]
  [SerializeField] private int difficulty = 1;
  [SerializeField] private Transform gameHolder;

  [Header("UI Elements")]
  [SerializeField] private Transform levelSelectPanel;
  [SerializeField] private Image levelSelectPrefab;
  [SerializeField] private GameObject playButton;
  [SerializeField] private Transform mathGrid;
  [SerializeField] private Transform answerOptions;
  [SerializeField] private Button operationFieldPrefab;
  [SerializeField] private Button answerButtonPrefab;
  [SerializeField] private Image background;
  [SerializeField] private TextMeshProUGUI timerText;
  [SerializeField] private TextMeshProUGUI levelText;
  [SerializeField] private GameObject resetButton;

  private int piecesCorrect;
  private Button selectedAnswerButton;

  private List<KeyValuePair<string, int>> operations;

  private float timerDuration = 600f;
  private float timer;
  private bool isGameOver;
  private int currentLevel = 1;
  private int maxLevel = 3;

  void Start()
  {
    InitializeBackground();
    levelSelectPanel.gameObject.SetActive(true);
    piecesCorrect = 0;
    timerText.gameObject.SetActive(false);
    playButton.SetActive(true);
    playButton.GetComponent<Button>().onClick.AddListener(StartGame);
    resetButton.GetComponent<Button>().onClick.AddListener(PlayAgain);
    resetButton.SetActive(false);
    levelText.gameObject.SetActive(false);
  }

  void Update()
  {
    if (!isGameOver && timer > 0)
    {
      timer -= Time.deltaTime;
      UpdateTimerUI();

      if (timer <= 0)
      {
        GameOver();
      }
    }
  }

  private void UpdateLevelUI()
  {
    levelText.text = "Level " + currentLevel;
  }

  private void ShowLevelUpMessage()
  {
    levelText.text = "Level Up!";
    levelText.gameObject.SetActive(true);
    Invoke("UpdateLevelUI", 2f);
  }

  private void showYouWOnMessage()
  {
    levelText.text = "You Won!";
    levelText.gameObject.SetActive(true);
  }

  private void SetDifficultyForLevel(int level)
  {
    difficulty = level;
  }

  private void UpdateTimerUI()
  {
    timerText.text = Mathf.Ceil(timer).ToString();
  }

  private void GameOver()
  {
    isGameOver = true;
    timerText.text = "Time's Up!";
    playButton.SetActive(true);
    levelSelectPanel.gameObject.SetActive(true);
    resetButton.SetActive(false);
    timerText.gameObject.SetActive(false);
    currentLevel = 1;
    piecesCorrect = 0;
  }

  private void InitializeBackground()
  {
    Image mathGridImage = mathGrid.GetComponent<Image>();
    if (mathGridImage != null)
    {
      Color color = mathGridImage.color;
      color.a = 0;
      mathGridImage.color = color;
    }
  }

  private void InitializeMathPuzzleUI()
  {
    foreach (Transform child in mathGrid)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in answerOptions)
    {
      Destroy(child.gameObject);
    }

    RectTransform backgroundRectTransform = mathGrid.GetComponent<RectTransform>();
    float backgroundWidth = backgroundRectTransform.rect.width;
    float backgroundHeight = backgroundRectTransform.rect.height;

    float buttonWidth = operationFieldPrefab.GetComponent<RectTransform>().rect.width;
    float buttonHeight = operationFieldPrefab.GetComponent<RectTransform>().rect.height;

    int columns = Mathf.FloorToInt(backgroundWidth / buttonWidth);
    int rows = Mathf.FloorToInt(backgroundHeight / buttonHeight);

    GenerateOperations(columns * rows);

    int operationIndex = 0;

    for (int i = 0; i < rows; i++)
    {
      for (int j = 0; j < columns; j++)
      {
        if (operationIndex >= operations.Count)
          break;

        var operation = operations[operationIndex];

        Button operationButton = Instantiate(operationFieldPrefab, mathGrid);
        operationButton.GetComponentInChildren<TextMeshProUGUI>().text = operation.Key;
        operationButton.gameObject.AddComponent<DropHandler>();
        operationButton.onClick.AddListener(() => OnOperationFieldClick(operationButton));

        operationIndex++;
      }
    }

    ScatterAnswerButtons();
  }

  private void ScatterAnswerButtons()
  {
    RectTransform answerOptionsRectTransform = answerOptions.GetComponent<RectTransform>();
    float containerWidth = answerOptionsRectTransform.rect.width;
    float containerHeight = answerOptionsRectTransform.rect.height;
    float left = answerOptionsRectTransform.anchoredPosition.x - (answerOptionsRectTransform.rect.width * answerOptionsRectTransform.pivot.x);
    float right = answerOptionsRectTransform.anchoredPosition.x + (answerOptionsRectTransform.rect.width * (1 - answerOptionsRectTransform.pivot.x));
    float top = answerOptionsRectTransform.anchoredPosition.y + (answerOptionsRectTransform.rect.height * (1 - answerOptionsRectTransform.pivot.y));
    float bottom = answerOptionsRectTransform.anchoredPosition.y - (answerOptionsRectTransform.rect.height * answerOptionsRectTransform.pivot.y);

    foreach (var answer in operations)
    {
      Button answerButton = Instantiate(answerButtonPrefab, answerOptions);
      answerButton.GetComponentInChildren<TextMeshProUGUI>().text = answer.Value.ToString();
      answerButton.gameObject.AddComponent<DragHandler>();
      answerButton.gameObject.AddComponent<CanvasGroup>();
      answerButton.onClick.AddListener(() => OnAnswerButtonClick(answerButton));

      // Scatter buttons within the container
      RectTransform buttonRectTransform = answerButton.GetComponent<RectTransform>();
      float buttonWidth = buttonRectTransform.rect.width;
      float buttonHeight = buttonRectTransform.rect.height;

      float randomX = Random.Range(left, right);
      float randomY = Random.Range(buttonHeight / 2, containerHeight - buttonHeight / 2);

      buttonRectTransform.anchoredPosition = new Vector2(randomX, randomY - containerHeight / 2);
    }
  }

  private void GenerateOperations(int count)
  {
    operations = new List<KeyValuePair<string, int>>();
    System.Random random = new System.Random();

    for (int i = 0; i < count; i++)
    {
      int a = random.Next(1, 50);
      int b = random.Next(1, 50);
      string operation = $"{a}+{b}";
      int result = a + b;

      // Add different operations based on level
      if (currentLevel == 1)
      {
        // Level 1: Addition and Subtraction
        if (random.Next(0, 2) == 0)
        {
          operation = $"{a}-{b}";
          result = a - b;
        }
      }
      else if (currentLevel == 2)
      {
        // Level 2: Addition, Subtraction, and Multiplication
        int opType = random.Next(0, 3);
        if (opType == 1)
        {
          operation = $"{a}-{b}";
          result = a - b;
        }
        else if (opType == 2)
        {
          operation = $"{a}*{b}";
          result = a * b;
        }
      }
      else if (currentLevel == 3)
      {
        // Level 3: Addition, Subtraction, Multiplication, and Division
        int opType = random.Next(0, 4);
        if (opType == 1)
        {
          operation = $"{a}-{b}";
          result = a - b;
        }
        else if (opType == 2)
        {
          operation = $"{a}*{b}";
          result = a * b;
        }
        else if (opType == 3 && b != 0)
        {
          operation = $"{a}/{b}";
          result = a / b;
        }
      }

      operations.Add(new KeyValuePair<string, int>(operation, result));
    }
  }

  public void CheckAnswer(Button targetButton, Button draggedButton, int draggedValue, string targetOperation)
  {
    var operation = operations.Find(op => op.Key == targetOperation);
    if (operation.Value == draggedValue)
    {
      CorrectAnswer(targetButton, draggedButton);
    }
    else
    {
      IncorrectAnswer(draggedButton);
    }
  }

  private void CorrectAnswer(Button targetButton, Button draggedButton)
  {
    draggedButton.gameObject.SetActive(false);
    targetButton.gameObject.SetActive(false);
    piecesCorrect++;

    if (piecesCorrect == operations.Count)
    {
      if (currentLevel < maxLevel)
      {
        currentLevel++;
        SetDifficultyForLevel(currentLevel);
        levelSelectPanel.gameObject.SetActive(true);
        playButton.SetActive(true);
        resetButton.SetActive(false);
        timerText.gameObject.SetActive(false);
        ShowLevelUpMessage();
      }
      else
      {
        playButton.SetActive(true);
        levelSelectPanel.gameObject.SetActive(true);
        resetButton.SetActive(false);
        levelText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        currentLevel = 1;
        showYouWOnMessage();
      }
    }
  }

  private void IncorrectAnswer(Button draggedButton)
  {
    Debug.Log("Incorrect answer");
  }

  private void OnOperationFieldClick(Button button)
  {
    if (selectedAnswerButton != null)
    {
      int answer = int.Parse(selectedAnswerButton.GetComponentInChildren<TextMeshProUGUI>().text);
      string operation = button.GetComponentInChildren<TextMeshProUGUI>().text;

      CheckAnswer(button, selectedAnswerButton, answer, operation);
    }
  }

  private void OnAnswerButtonClick(Button button)
  {
    selectedAnswerButton = button;
  }

  public void PlayAgain()
  {
    levelSelectPanel.gameObject.SetActive(true);
    playButton.SetActive(true);
    isGameOver = false;
    levelText.gameObject.SetActive(false);
    timerText.gameObject.SetActive(false);
    resetButton.SetActive(false);
    currentLevel = 1;
    piecesCorrect = 0;
  }

  private void StartGame()
  {
    levelSelectPanel.gameObject.SetActive(false);
    playButton.SetActive(false);
    piecesCorrect = 0;
    levelText.gameObject.SetActive(true);
    timerText.gameObject.SetActive(true);
    resetButton.SetActive(true);
    InitializeMathPuzzleUI();
    timer = timerDuration;
    levelText.text = "Level " + currentLevel;
    isGameOver = false;
  }
}
