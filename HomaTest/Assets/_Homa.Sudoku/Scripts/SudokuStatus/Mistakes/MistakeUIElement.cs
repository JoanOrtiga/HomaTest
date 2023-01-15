using UnityEngine;
using UnityEngine.UI;

namespace _Homa.Sudoku.Scripts.SudokuStatus.Mistakes
{
    public class MistakeUIElement : MonoBehaviour
    {
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color mistakeColor;

        [SerializeField] private Image image;

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void MarkAsMistake()
        {
            image.color = mistakeColor;
        }
    
        public void ResetStatus()
        {
            image.color = defaultColor;
        }
    }
}
