using UnityEngine;

public class GameFieldObject : MonoBehaviour
{
  private Vector2Int _cellId;   // Позиция ячейки с частью змейки

  public void SetCellPosition(Vector2Int cellId, Vector2 position)
  {
    _cellId = cellId;                // Задаём переменной _cellId полученное значение
    transform.position = position;   // Устанавливаем позицию части змейки на сцене
  }

  public Vector2Int GetCellId()
  {
    return _cellId;  // Возвращаем значение переменной _cellId
  } 
}