using _Homa.Library.Scripts.DOTween;
using _Homa.Sudoku.Scripts.Cell;
using UnityEngine;

namespace _Homa.Sudoku.Scripts
{
    public class SudokuShakeController : MonoBehaviour
    {
        private TransformShake_DOTween _transformShake;
    
        private void Awake()
        {
            _transformShake = GetComponent<TransformShake_DOTween>();
            CellController.OnWrongNumberOnCell += _transformShake.PlayAnimation;
        }
    
        public void OnDestroy()
        {
            CellController.OnWrongNumberOnCell -= _transformShake.PlayAnimation;
        }
    }
}
