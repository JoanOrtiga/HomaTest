using System;
using System.Collections;
using System.Collections.Generic;
using _Homa.Library.Scripts.DOTween;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.GameController
{
    public class SudokuTableAnimator : MonoBehaviour
    {
        private static readonly Vector2Int UpDir = new(-1, 0);
        private static readonly Vector2Int DownDir = new(1, 0);
        private static readonly Vector2Int RightDir = new(0, 1);
        private static readonly Vector2Int LeftDir = new(0, -1);

        [SerializeField] private float delay = 0.2f;

        List<List<ScaleUI_DOTween>> layers = new();
        private ScaleUI_DOTween[,] _scaleUIMap;
        
        public void SetScaleUIMap(ScaleUI_DOTween[,] scaleUIMap)
        {
            _scaleUIMap = scaleUIMap;
        }

        public void StopAnimations()
        {
            StopCoroutine(OnCalculateAnimationEnded());
        }

        public void StartAnimation(Vector2Int position)
        {
            AnimateCells(_scaleUIMap, new Vector2Int(position.y, position.x));
        }
        
        private void AnimateCells(ScaleUI_DOTween[,] mapAnimatedCells, Vector2Int position)
        {
            layers.Clear();
            
            var layer = 0;
            
            if (layer >= layers.Count)
            {
                layers.Add(new List<ScaleUI_DOTween>());
            }

            layers[layer].Add(mapAnimatedCells[position.x, position.y]);

            layer++;
            AnimateCellsInDirection(mapAnimatedCells, UpDir, position,  layer);
            AnimateCellsInDirection(mapAnimatedCells, DownDir, position,  layer);
            AnimateCellsInDirection(mapAnimatedCells, LeftDir, position,  layer);
            AnimateCellsInDirection(mapAnimatedCells, RightDir, position,  layer);

            StartCoroutine(OnCalculateAnimationEnded());
        }

        private void AnimateCellsInDirection(ScaleUI_DOTween[,] mapAnimatedCells, Vector2Int direction, Vector2Int position, int layer)
        {
            position += direction;
            
            if (position.x < 0 || position.x >= mapAnimatedCells.GetLength(0) || position.y < 0 || position.y >= mapAnimatedCells.GetLength(1)) {
                return;
            }
            
            if (layer >= layers.Count)
            {
                layers.Add(new List<ScaleUI_DOTween>());
            }

            // Add current position to steps list
            layers[layer].Add(mapAnimatedCells[position.x, position.y]);

            layer++;
            if (direction == UpDir)
            {
                AnimateCellsInDirection(mapAnimatedCells, UpDir, position,  layer);
                AnimateCellsInDirection(mapAnimatedCells, LeftDir, position, layer);
                AnimateCellsInDirection(mapAnimatedCells, RightDir, position, layer);
            }
            else if (direction == DownDir)
            {
                AnimateCellsInDirection(mapAnimatedCells, DownDir, position,  layer);
                AnimateCellsInDirection(mapAnimatedCells, LeftDir, position,  layer);
                AnimateCellsInDirection(mapAnimatedCells, RightDir, position,  layer);
            }
            else if (direction == LeftDir)
            {
                AnimateCellsInDirection(mapAnimatedCells, LeftDir, position,  layer);
            }
            else if (direction == RightDir)
            {
                AnimateCellsInDirection(mapAnimatedCells, RightDir, position,  layer);
            }
        }

        private IEnumerator OnCalculateAnimationEnded()
        {
            var layersCopy = new List<List<ScaleUI_DOTween>>(layers);
            foreach (var listAnimatedCells in layersCopy)
            {
                foreach (var animatedCell in listAnimatedCells)
                {
                    animatedCell.PlayFullAnimation();
                }

                yield return new WaitForSeconds(delay);
            }
        }
    }
}