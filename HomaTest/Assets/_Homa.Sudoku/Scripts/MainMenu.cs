using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Homa.Sudoku.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup mainMenuCanvasGroup;
        [SerializeField] private float transitionDuration;

        [Header("Game")] 
        [SerializeField] private GameManager gameManager;
        
        public void LoadGame()
        {
            PlayTransition();
            gameManager.StartGame();
        }

        public void PlayTransition()
        {
            mainMenuCanvasGroup.DOFade(0f, transitionDuration).onComplete += () =>
            {
                mainMenuCanvasGroup.interactable = false;
                mainMenuCanvasGroup.blocksRaycasts = false;
                gameObject.SetActive(false);
            };
        }
    }
}
