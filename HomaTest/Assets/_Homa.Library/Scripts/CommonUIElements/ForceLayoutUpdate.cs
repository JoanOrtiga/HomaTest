using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Homa.Library.Scripts.CommonUIElements
{
    public class ForceLayoutUpdate : MonoBehaviour
    {
        [SerializeField] private RectTransform layoutParent;
        [SerializeField] private int waitForFrames;
        private IEnumerable Start()
        {
            for (int i = 0; i < waitForFrames; i++)
            {
                yield return null;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutParent);
        }
    }
}
