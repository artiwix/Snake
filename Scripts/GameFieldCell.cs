using UnityEngine;

public class GameFieldCell
{
  private Vector2 _position; // Позиция ячейки на игровом поле
  private bool _isEmpty;     // Флаг пустоты ячейки

  // Создаём объект класса GameFieldCell
  public GameFieldCell(Vector2 position)
  {
    _position = position; // Присваиваем позиции переданное значение
    _isEmpty = true;      // Делаем ячейку изначально пустой
  }

  // Получаем позицию ячейки
  public Vector2 GetPosition() { return _position; }       // Возвращаем значение _position
  
  // Делаем ячейку пустой или заполненной
  public void SetIsEmpty(bool value) { _isEmpty = value; } // Устанавливаем флаг пустоты или заполненности
  
  // Узнаём, пуста ли ячейка
  public bool GetIsEmpty() { return _isEmpty; }            // Возвращаем значение _isEmpty
}