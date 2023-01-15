using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Cell
{
    public abstract class CellClick : MonoBehaviour
    {
        public event Action OnCellClicked;
        public static event Action OnAnyCellClicked;

        public virtual void Click()
        {
            RaiseOnCellClickedEvent();
            RaiseOnAnyCellClickedEvent();
        }

        protected void RaiseOnCellClickedEvent()
        {
            OnCellClicked?.Invoke();
        }
        
        protected void RaiseOnAnyCellClickedEvent()
        {
            OnAnyCellClicked?.Invoke();
        }
    }
}