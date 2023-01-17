using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Cell
{
    public abstract class CellClick : MonoBehaviour
    {
        public event Action OnCellClicked;
        public static event Action OnAnyCellClicked;

        protected const float CellClickCooldown = 2f;
        protected static float CurrentClickCooldown;
        protected static bool CanClick;

        private void Awake()
        {
            CurrentClickCooldown = CellClickCooldown;
        }
        
        private void Update()
        {
            if(CanClick)
                return;
            CurrentClickCooldown -= Time.deltaTime;
            if (CurrentClickCooldown <= 0)
            {
                CanClick = true;
                CurrentClickCooldown = CellClickCooldown;
            }
        }

        public virtual void Click()
        {
            if(!CanClick)
                return;
            CanClick = false;
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