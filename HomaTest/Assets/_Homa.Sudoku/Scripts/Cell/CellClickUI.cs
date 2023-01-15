using UnityEngine.EventSystems;

namespace _Homa.Sudoku.Scripts.Cell
{
    public class CellClickUI : CellClick , IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Click();
        }
    }
}