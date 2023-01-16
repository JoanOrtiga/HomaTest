using _Homa.Library.Scripts.DOTween;
using UnityEngine;
using UnityEngine.UI;

namespace _Homa.Sudoku.Scripts.UI.Settings
{
    public class ToggleItem : MonoBehaviour
    {
        [field: SerializeField] public ScaleUI_DOTween ScaleUI_DOTween { get; private set; }
        [field: SerializeField] public ChangeImageColor_DOTween ChangeImageColor_DOTween{ get; private set; }
        [field: SerializeField] public ChangeTextMeshProColor_DOTween ChangeTextMeshProColor_DOTween{ get; private set; }
        [field: SerializeField] public Button Button{ get; private set; }
    }
}