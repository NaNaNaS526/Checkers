using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Controls _controls;
    private bool _isCheckerSelected;
    private Checker _selectedChecker;
    private readonly List<Cell> _possibleSteps = new();
    private string[] _steps = new string[] { "Red", "Green", "Blue", "Yellow" };
    private string _currentStep;
    private int _currentStepIndex;
    private Checker _eatenChecker;
    private Checker _eatenChecker2;
    private Cell _eatenCheckerCell;
    private Cell _eatenChecker2Cell;


    private void Start()
    {
        _controls = new Controls();
        _controls.Enable();
        _controls.Player.CheckerMoveOrSelect.performed += DetermineCurrentAction;
        _currentStepIndex = Random.Range(0, 4);
        _currentStep = _steps[_currentStepIndex];
    }

    private void Update()
    {
        if (_currentStepIndex == 0)
        {
            transform.eulerAngles = new Vector3(90, 0, 0);
        }

        if (_currentStepIndex == 1)
        {
            transform.eulerAngles = new Vector3(90, 90, 0);
        }

        if (_currentStepIndex == 2)
        {
            transform.eulerAngles = new Vector3(90, 180, 0);
        }

        if (_currentStepIndex == 3)
        {
            transform.eulerAngles = new Vector3(90, 270, 0);
        }
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void DetermineCurrentAction(InputAction.CallbackContext ctx)
    {
        if (_isCheckerSelected) MoveChecker();
        else SelectChecker();
    }

    private void SelectChecker()
    {
        Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000))
        {
            if (hit.collider.CompareTag("Checker") && hit.collider.GetComponent<Checker>().color == _currentStep)
            {
                var checkerPosition = hit.transform.position;
                hit.collider.transform.position =
                    new Vector3(checkerPosition.x, checkerPosition.y + 1, checkerPosition.z);
                _selectedChecker = hit.collider.gameObject.GetComponent<Checker>();
                _isCheckerSelected = true;
                DeterminePossibleSteps();
                foreach (var a in _possibleSteps)
                {
                    a.GetComponent<MeshRenderer>().material.color = Color.magenta;
                }
            }
        }
    }

    private void MoveChecker()
    {
        var ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit, 1000)) return;
        var selectedCell = hit.collider.GetComponent<Cell>();

        if (hit.collider != null && hit.collider.CompareTag("Checker") &&
            hit.collider.GetComponent<Checker>() == _selectedChecker)
        {
            _selectedChecker.transform.localPosition = Vector3.zero;
            _selectedChecker = null;
            _isCheckerSelected = false;
            foreach (var a in _possibleSteps)
            {
                a.GetComponent<MeshRenderer>().material.color = Color.white;
            }

            return;
        }

        if (!hit.collider.CompareTag("Cell") || hit.collider.GetComponent<Cell>().isOccupied) return;
        if (_selectedChecker == null) return;
        TryMove(selectedCell, hit);
    }

    private void DeterminePossibleSteps()
    {
        _possibleSteps.Clear();
        var currentCell = _selectedChecker.currentCell;
        if (!_selectedChecker.isKing)
        {
            if (_selectedChecker.color == "Red")
            {
                if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i + 1, currentCell.j + 1].GetComponentInChildren<Checker>().color !=
                         "Red")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j + 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j + 2]);
                        _eatenChecker = BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1]
                            .GetComponentInChildren<Checker>();
                        _eatenCheckerCell = BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j + 2];
                    }
                }

                if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i + 1, currentCell.j - 1].GetComponentInChildren<Checker>().color !=
                         "Red")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j - 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j - 2]);
                        _eatenChecker2 = BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1]
                            .GetComponentInChildren<Checker>();
                        _eatenChecker2Cell = BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j - 2];
                    }
                }
            }

            if (_selectedChecker.color == "Green")
            {
                if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i + 1, currentCell.j + 1].GetComponentInChildren<Checker>().color !=
                         "Green")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j + 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j + 2]);
                        _eatenChecker = BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j + 1]
                            .GetComponentInChildren<Checker>();
                        _eatenCheckerCell = BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j + 2];
                    }
                }

                if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i - 1, currentCell.j + 1].GetComponentInChildren<Checker>().color !=
                         "Green")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j + 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j + 2]);
                        _eatenChecker2 = BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1]
                            .GetComponentInChildren<Checker>();
                        _eatenChecker2Cell = BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j + 2];
                    }
                }
            }

            if (_selectedChecker.color == "Blue")
            {
                if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i - 1, currentCell.j + 1].GetComponentInChildren<Checker>().color !=
                         "Blue")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j + 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j + 2]);
                        _eatenChecker = BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j + 1]
                            .GetComponentInChildren<Checker>();
                        _eatenCheckerCell = BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j + 2];
                    }
                }

                if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i - 1, currentCell.j - 1].GetComponentInChildren<Checker>().color !=
                         "Blue")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j - 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j - 2]);
                        _eatenChecker2 = BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1]
                            .GetComponentInChildren<Checker>();
                        _eatenChecker2Cell = BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j - 2];
                    }
                }
            }

            if (_selectedChecker.color == "Yellow")
            {
                if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i + 1, currentCell.j - 1].GetComponentInChildren<Checker>().color !=
                         "Yellow")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j - 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j - 2]);
                        _eatenChecker = BoardGenerator.BoardGrid[currentCell.i + 1, currentCell.j - 1]
                            .GetComponentInChildren<Checker>();
                        _eatenCheckerCell = BoardGenerator.BoardGrid[currentCell.i + 2, currentCell.j - 2];
                    }
                }

                if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1].isOccupied == false)
                {
                    _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1]);
                }
                else if (BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1].isOccupied && BoardGenerator
                             .BoardGrid[currentCell.i - 1, currentCell.j - 1].GetComponentInChildren<Checker>().color !=
                         "Yellow")
                {
                    if (!BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j - 2].isOccupied)
                    {
                        _possibleSteps.Add(BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j - 2]);
                        _eatenChecker2 = BoardGenerator.BoardGrid[currentCell.i - 1, currentCell.j - 1]
                            .GetComponentInChildren<Checker>();
                        _eatenChecker2Cell = BoardGenerator.BoardGrid[currentCell.i - 2, currentCell.j - 2];
                    }
                }
            }
        }
    }

    private void TryMove(Cell selectedCell, RaycastHit hit)
    {
        foreach (var cell in _possibleSteps)
        {
            if (selectedCell == cell)
            {
                Move(hit);
            }
        }
    }


    private void Move(RaycastHit hit)
    {
        Transform selectedCheckerTransform;
        var lastPositionI = _selectedChecker.currentCell.i;
        var lastPositionJ = _selectedChecker.currentCell.j;
        (selectedCheckerTransform = _selectedChecker.transform).SetParent(hit.transform);
        selectedCheckerTransform.localPosition = Vector3.zero;
        _selectedChecker.currentCell.isOccupied = false;
        _selectedChecker.currentCell = hit.collider.GetComponent<Cell>();
        hit.collider.GetComponent<Cell>().isOccupied = true;
        _isCheckerSelected = false;
        // if (Mathf.Abs(_selectedChecker.currentCell.i - lastPositionI) > 1 ||
        //     Mathf.Abs(_selectedChecker.currentCell.j - lastPositionJ) > 1)
        // {
        //     _possibleSteps.Clear();
        //     DeterminePossibleSteps();
        //     MoveChecker();
        //     return;
        // }
        if (_eatenChecker)
        {
            if (_selectedChecker.currentCell == _eatenCheckerCell)
            {
                _eatenChecker.currentCell.isOccupied = false;
                Destroy(_eatenChecker.gameObject);
                _eatenCheckerCell = null;
            }
        }

        if (_eatenChecker2)
        {
            if (_selectedChecker.currentCell == _eatenChecker2Cell)
            {
                _eatenChecker2.currentCell.isOccupied = false;
                Destroy(_eatenChecker2.gameObject);
                _eatenChecker2Cell = null;
            }
        }

        if (_currentStepIndex < _steps.Length - 1) _currentStepIndex++;
        else _currentStepIndex = 0;
        _currentStep = _steps[_currentStepIndex];
        foreach (var a in _possibleSteps)
        {
            a.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}