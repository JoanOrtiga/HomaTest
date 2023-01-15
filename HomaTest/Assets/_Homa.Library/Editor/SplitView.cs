using UnityEngine.UIElements;

namespace _Homa.Library.Editor
{
    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits> { }
        public SplitView()
        {
            
        }
    }
}