using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isOccupied;
    public int i;
    public int j;
    public GameObject checker;
    // public Cell[] leftTopDiagonal;
    // public Cell[] rightTopDiagonal;
    // public Cell[] leftBottomDiagonal;
    // public Cell[] rightBottomDiagonal;
    //
    // private void Start()
    // {
    //     leftTopDiagonal[0] = BoardGenerator.BoardGrid[i + 1, j - 1]?? BoardGenerator.BoardGrid[i, j];
    //     rightTopDiagonal[0] = BoardGenerator.BoardGrid[i + 1, j + 1] ?? BoardGenerator.BoardGrid[i, j];
    //     rightBottomDiagonal[0] = BoardGenerator.BoardGrid[i - 1, j + 1] ?? BoardGenerator.BoardGrid[i, j];
    //     leftBottomDiagonal[0] = BoardGenerator.BoardGrid[i - 1, j - 1] ?? BoardGenerator.BoardGrid[i, j];
    // }
}