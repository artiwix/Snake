using UnityEngine;

public class GameField : MonoBehaviour
{
  public Vector2 CellSize;       // Размер ячейки (по X и Y)
  public int CellsInRow = 12;    // Количество ячеек в одном ряду

  private Transform   _firstCellPoint = null;     // Позиция первой ячейки
  private GameFieldCell[,] _cells;                // Двумерный массив из позиций каждой ячейки

  public void FillCellsPositions()
  {
    _firstCellPoint = transform.GetChild(0);
    _cells = new GameFieldCell[CellsInRow, CellsInRow]; // Создаём двумерный массив размером CellsInRow x CellsInRow

    for (int x = 0; x < CellsInRow; x++) {     // Проходим по первым координатам всех ячеек (x)
      for (int y = 0; y < CellsInRow; y++) {   // Проходим по вторым координатам всех ячеек (y)
                                               
        Vector2 cellPosition = (Vector2)_firstCellPoint.position + Vector2.right * x * CellSize.x + Vector2.up * y * CellSize.y; // Вычисляем позицию текущей ячейки
        GameFieldCell newCell = new GameFieldCell(cellPosition);                                                                // Создаём новую ячейку
        _cells[x, y] = newCell;                                                                                                 // Записываем эту ячейку в массив _cells
      }
    }
  }

  public Vector2 GetCellPosition(uint x, uint y)
  {
    GameFieldCell cell = GetCell(x, y);        // Получаем ячейку по заданным координатам

    if (cell == null) { return Vector2.zero; } // Если ячейка не была найдена, возвращаем (0, 0)
    
    return _cells[x, y].GetPosition();         // Возвращаем позицию найденной ячейки
  }

  public void SetObjectCell(GameFieldObject obj, Vector2Int newCellId)
  {
    Vector2 cellPosition = GetCellPosition((uint)newCellId.x, (uint)newCellId.y); // Получаем позицию ячейки по заданным координатам
    obj.SetCellPosition(newCellId, cellPosition);                     // Устанавливаем объект на найденную ячейку
    SetCellIsEmpty((uint)newCellId.x, (uint)newCellId.y, false);                  // Задаём значение занятости ячейки
  }

  public bool GetCellIsEmpty(uint x, uint y)
  {
    GameFieldCell cell = GetCell(x, y); // Получаем ячейку по указанным координатам

    // Если ячейка не была найдена
    if (cell == null) {  return false; } // Возвращаем false
    
    return cell.GetIsEmpty(); // Возвращаем значение занятости найденной ячейки
  }

  public void SetCellIsEmpty(uint x, uint y, bool value)
  {
    GameFieldCell cell = GetCell(x, y); // Получаем ячейку по указанным координатам

    // Если ячейка не была найдена
    if (cell == null) { return; }  // Выходим из метода
    
    _cells[x, y].SetIsEmpty(value); // Устанавливаем значение занятости ячейки
  }

  private GameFieldCell GetCell(uint x, uint y)
  {
    // Если координаты выходят за границы игрового поля
    if (x >= CellsInRow || y >= CellsInRow) { return null; } // Возвращаем null
    
    return _cells[x, y]; // Возвращаем ячейку с заданными координатами
  }
}
