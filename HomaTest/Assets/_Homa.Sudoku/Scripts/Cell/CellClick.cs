using System;
using UnityEngine;

namespace _Homa.Sudoku.Scripts.Cell
{
    public abstract class CellClick : MonoBehaviour
    {
        public event Action OnCellClicked;
        public static event Action OnAnyCellClicked;

        protected static float CellClickCooldown = 8f;
        protected static float currentClickCooldown;
        protected static bool canClick;

        private void Awake()
        {
            currentClickCooldown = CellClickCooldown;
        }
        
        private void Update()
        {
            if(canClick)
                return;
            currentClickCooldown -= Time.deltaTime;
            if (currentClickCooldown <= 0)
            {
                canClick = true;
                currentClickCooldown = CellClickCooldown;
            }
        }

        public virtual void Click()
        {
            if(!canClick)
                return;
            canClick = false;
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