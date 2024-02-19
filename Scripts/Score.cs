using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
  private const string BestScoreKey = "BestScore";  // Константа с ключом лучшего счёта — так игра сможет быстрее находить его в памяти устройства

  private TextMeshProUGUI _scoreText;  // Переменная надписи о счёте

  private int _score;     // Переменная для хранения счёта
  private int _bestScore; // Переменная для хранения лучшего счёта
  
  public void AddScore(int value) { SetScore(_score + value); } // Передаём в метод SetScore() сумму _score и value
  public void Restart ()          { SetScore(0);              } // Передаём в метод SetScore() значение 0 (сбрасываем счёт)

  public int GetScore    () { return _score;     }  // Возвращаем текущий счёт
  public int GetBestScore() { return _bestScore; }  // Возвращаем лучший счёт

  public void SetBestScore(int value) {
    _bestScore = value;     // Присваиваем лучшему счёту значение value
    SaveBestScore(value);   // Передаём в метод SaveBestScore() значение value
  }

  private void Start() {
    FillComponents(); // Вызываем метод FillComponents()
    SetScore(0);      // Передаём в метод SetScore() значение 0 (сбрасываем счёт)
    LoadBestScore();  // Вызываем метод LoadBestScore()
  }

  private void FillComponents() {
    // Находим компонент TextMeshProUGUI у дочерних объектов того объекта, на котором висит скрипт (у нас это будет Score),
    // и присваиваем значение компонента переменной _scoreText
    _scoreText = GetComponentInChildren<TextMeshProUGUI>();
  }

  private void SetScore(int value) {
    _score = value;      // Присваиваем текущему счёту значение value
    SetScoreText(value); // Передаём в метод SetScoreText() значение value
  }

  private void SetScoreText(int value) {
    _scoreText.text = value.ToString();   // Преобразуем value в строку и присваиваем его свойству text компонента _scoreText
  }

  private void LoadBestScore() {
    _bestScore = PlayerPrefs.GetInt(BestScoreKey); // Присваиваем _bestScore значение, сохранённое в PlayerPrefs с ключом BestScoreKey
  }

  private void SaveBestScore(int value) {
    PlayerPrefs.SetInt(BestScoreKey, value); // Сохраняем value в PlayerPrefs с ключом BestScoreKey
  }
}
