using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private Cell whiteCell;
    [SerializeField] private Cell blackCell;
    [SerializeField] private Checker[] checkers;
    public static readonly Cell[,] BoardGrid = new Cell[11, 11];
    private int _rows;
    private int _columns;
    private bool _isBlack;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        _rows = BoardGrid.GetUpperBound(0) + 1;
        _columns = BoardGrid.Length / _rows;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _isBlack = !_isBlack;
                var cell = _isBlack ? Instantiate(blackCell) : Instantiate(whiteCell);
                cell.transform.SetParent(transform);
                cell.transform.localPosition = new Vector3(j, 0, i);
                cell.name = $"Cell ({i},{j})";
                BoardGrid[i, j] = cell;
                cell.i = i;
                cell.j = j;
            }
        }

        GenerateCheckers();
    }

    private void GenerateCheckers()
    {
        int a = 10;
        int offset = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = offset; j < a; j += 2)
            {
                var newChecker = Instantiate(checkers[0], BoardGrid[i, j].transform, true);
                BoardGrid[i, j].GetComponent<Cell>().isOccupied = true;
                newChecker.transform.position = new Vector3(j, 0, i);
                newChecker.currentCell = BoardGrid[i, j];
            }

            a--;
            offset++;
        }

        a = 10;
        offset = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = offset; j < a; j += 2)
            {
                var newChecker = Instantiate(checkers[1], BoardGrid[j, i].transform, true);
                BoardGrid[j, i].GetComponent<Cell>().isOccupied = true;
                newChecker.transform.position = new Vector3(i, 0, j);
                newChecker.currentCell = BoardGrid[j, i];
            }

            a--;
            offset++;
        }

        a = 10;
        offset = 1;
        for (int i = _columns - 1; i > _columns - 4; i--)
        {
            for (int j = offset; j < a; j += 2)
            {
                var newChecker = Instantiate(checkers[2], BoardGrid[i, j].transform, true);
                BoardGrid[i, j].GetComponent<Cell>().isOccupied = true;
                newChecker.transform.position = new Vector3(j, 0, i);
                newChecker.currentCell = BoardGrid[i, j];
            }

            a--;
            offset++;
        }

        a = 10;
        offset = 1;
        for (int i = _rows - 1; i > _rows - 4; i--)
        {
            for (int j = offset; j < a; j += 2)
            {
                var newChecker = Instantiate(checkers[3], BoardGrid[j, i].transform, true);
                BoardGrid[j, i].GetComponent<Cell>().isOccupied = true;
                newChecker.transform.position = new Vector3(i, 0, j);
                newChecker.currentCell = BoardGrid[j, i];
            }

            a--;
            offset++;
        }
    }
}