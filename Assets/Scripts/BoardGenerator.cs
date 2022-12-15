using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject whiteCell;
    [SerializeField] private GameObject blackCell;
    [SerializeField] private GameObject[] checkers;
    private readonly GameObject[,] _boardGrid = new GameObject[11, 11];
    private int _rows;
    private int _columns;
    private bool _isBlack;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        _rows = _boardGrid.GetUpperBound(0) + 1;
        _columns = _boardGrid.Length / _rows;
        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _isBlack = !_isBlack;
                GameObject cell = _isBlack ? Instantiate(blackCell) : Instantiate(whiteCell);
                cell.transform.SetParent(transform);
                cell.transform.localPosition = new Vector3(j, 0, i);
                cell.name = $"Cell ({i},{j})";
                _boardGrid[i, j] = cell;
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
                GameObject newChecker = Instantiate(checkers[0], _boardGrid[i, j].transform, true);
                newChecker.transform.position = new Vector3(j, 0, i);
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
                GameObject newChecker = Instantiate(checkers[1], _boardGrid[j, i].transform, true);
                newChecker.transform.position = new Vector3(i, 0, j);
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
                GameObject newChecker = Instantiate(checkers[2], _boardGrid[i, j].transform, true);
                newChecker.transform.position = new Vector3(j, 0, i);
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
                GameObject newChecker = Instantiate(checkers[3], _boardGrid[j, i].transform, true);
                newChecker.transform.position = new Vector3(i,0, j);
            }

            a--;
            offset++;
        }
    }
}