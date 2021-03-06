﻿using Assets._Scripts.Abstract;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Board.Control
{
    internal class OnBoardMovementLogic : MonoBehaviour
    {
        private List<(Color, Square)> _defaultSquareColor;

        private void Start()
        {
            _defaultSquareColor = new List<(Color, Square)>();
        }

        internal IEnumerable<Square> GetPossiblePieceMovement(GameObject selectedPiece)
        {
            var squareUnderPiece = selectedPiece.GetComponentInParent<Square>();

            if(squareUnderPiece is null)
            {
                Debug.LogError($"Piece {selectedPiece.name} is not attached to any {nameof(Square)} component");
                return Enumerable.Empty<Square>();
            }

            var possiblePieceMovement = selectedPiece.GetComponent<IPieceMovement>().GetPossibleMovementSquares(squareUnderPiece);

            return possiblePieceMovement;
        }

        #region To relocate

        // TODO Need to create class to handle all selection/deselection color changes (also for pieces)
        internal void SetBacklightColorToSquare(IEnumerable<Square> possibleSqueresToMove)
        {
            foreach (var square in possibleSqueresToMove)
            {
                _defaultSquareColor.Add((square.GetComponent<Renderer>().material.color, square));
                square.GetComponent<Renderer>().material.color = Color.green;
            }
        }

        // TODO Need to create class to handle all selection/deselection color changes (also for pieces)
        internal void RemoveBacklightFromSquares()
        {
            foreach (var squareColorPair in _defaultSquareColor)
            {
                squareColorPair.Item2.GetComponent<Renderer>().material.color = squareColorPair.Item1;
            }

            _defaultSquareColor.Clear();
        }

        #endregion

    }
}
