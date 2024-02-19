using UnityEngine;
using TMPro;

public class GameStateChanger : MonoBehaviour
{
  public Score Score;              // Скрипт ведения счёта
  public GameObject GameScreen;    // Экран игры
  public GameObject GameEndScreen; // Экран конца игры
  public TextMeshProUGUI GameEndScoreText; // Надпись о конце игры
  public TextMeshProUGUI BestScoreText;    // Надпись о рекорде игрока

  private AppleSpawner[] _appleSpawners;   // Скрипты появления яблок
  private SnakeMoveControll _snake;        // Скрипт движения змейки
  private GameField _gameField;            // Скрипт игрового поля

  private bool _isGameStarted;             // Флаг состояния игры (начата или нет)

  public void StartGame() {
    _isGameStarted = true;        // НОВОЕ: Устанавливаем флаг начала игры
    _snake.StartGame();           // Начинаем движение змейки
    
    for (int i = 0; i < _appleSpawners.Length; i++) { // НОВОЕ: Проходим по всем объектам AppleSpawner в массиве
      _appleSpawners[i].CreateApple();                // НОВОЕ: Создаём яблоко в каждом объекте AppleSpawner
    }
    SwitchScreens(true);          // НОВОЕ: Переключаемся на экран игры
  }

  public void RestartGame() {
    _isGameStarted = true;        // НОВОЕ: Устанавливаем флаг начала игры
    _snake.RestartGame();         // Перезапускаем змейку
    for (int i = 0; i < _appleSpawners.Length; i++) { // НОВОЕ: Проходим по всем объектам AppleSpawner в массиве
      _appleSpawners[i].Restart();                    // НОВОЕ: Перезапускаем каждый объект AppleSpawner
    }
    Score.Restart();              // Обнуляем очки
    SwitchScreens(true);          // Переключаемся на экран игры
  }

  public void EndGame() {

    // НОВОЕ: Если игра не начата
    if (!_isGameStarted) { return; } // НОВОЕ: Выходим из метода
    _isGameStarted = false;          // НОВОЕ: Устанавливаем флаг конца игры

    _snake.StopGame(); // Останавливаем движение змейки
    
    RefreshScores();      // НОВОЕ: Обновляем счёт
    SwitchScreens(false); // НОВОЕ: Переключаемся на экран конца игры
  }

  private void Start()
  {
    InitValues();     // Инициализируем переменные
    FirstStartGame(); // Вызываем метод FirstStartGame() при запуске игры
  }

  private void InitValues() {
    _snake        = GameObject.Find("Snake").GetComponent<SnakeMoveControll>();
    _gameField    = FindObjectOfType<GameField>();
    _appleSpawners = GetComponents<AppleSpawner>();
    for (int i = 0; i < _appleSpawners.Length; i++) { 
      _appleSpawners[i].InitGameVariables(this, _gameField, _snake);
    }
  }

  private void FirstStartGame() {
    _gameField.FillCellsPositions();  // Вызываем метод FillCellsPositions() из скрипта GameField, чтобы заполнить позиции ячеек
    StartGame();
  }

  private void SwitchScreens(bool isGame)
  {
    GameScreen.SetActive(isGame);     // Активируем экран игры
    GameEndScreen.SetActive(!isGame); // Скрываем экран завершения игры
  }

  private void RefreshScores()
  {
    int  score          = Score.GetScore();     // Получаем текущий счёт
    int  oldBestScore   = Score.GetBestScore(); // Получаем прежний лучший счёт
    bool isNewBestScore = CheckNewBestScore(score, oldBestScore); // Проверяем, побил ли игрок рекорд

    // В зависимости от результата показываем или скрываем текст про новый рекорд
    SetActiveGameEndScoreText(!isNewBestScore);

    if (isNewBestScore) {                // Если игрок побил рекорд
      Score.SetBestScore(score);         // Устанавливаем новый лучший счёт
      SetNewBestScoreText(score);        // Задаём текст о новом рекорде
    } else {                             // Иначе
      SetGameEndScoreText(score);        // Устанавливаем текст о текущем счёте
      SetOldBestScoreText(oldBestScore); // Задаём текст о прежнем рекорде
    }
  }

  // Возвращаем результат проверки того, что текущий счёт выше лучшего (true или false)
  private bool CheckNewBestScore(int score, int oldBestScore) { return score > oldBestScore; }

  
  private void SetGameEndScoreText(int value) { GameEndScoreText.text = $"Game over!\nYour Score: {value}"; } // Обновляем надпись конца игры
  private void SetOldBestScoreText(int value) { BestScoreText.text    = $"Best Score: {value}";             } // Обновляем надпись лучшего счёта
  private void SetNewBestScoreText(int value) { BestScoreText.text    = $"NEW Best Score: {value}!";        } // Обновляем надпись нового рекорда

  // Устанавливаем активность текстового поля счёта в конце игры в зависимости от значения value
  private void SetActiveGameEndScoreText(bool value) { GameEndScoreText.gameObject.SetActive(value); }
}