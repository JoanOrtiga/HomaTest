using _Homa.Sudoku.Scripts.DataLoader;
using _Homa.Sudoku.Scripts.GameController;
using _Homa.Sudoku.Scripts.LevelData;
using UnityEngine;

namespace _Homa.Sudoku.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SudokuLevelsCollection _sudokuLevelsCollection;
        [SerializeField] private CurrentLevelIndexManager _currentLevelIndexManager;
        
        [SerializeField] private SudokuLevelManager _sudokuLevelManager;

        private int _currentLevel;
        
        public void StartGame()
        {
            _currentLevel = _currentLevelIndexManager.LoadLevelIndex();
            if (_currentLevel >= _sudokuLevelsCollection.Levels.Count)
            {
                _currentLevel = 0;
            }

            LoadNewLevel();
        }

        public void NextLevel()
        {
            _currentLevel++;
            _currentLevelIndexManager.SaveLevelIndex(_currentLevel);
            
            LoadNewLevel();
        }
        
        private void LoadNewLevel()
        {
            var level = _sudokuLevelsCollection.Levels[_currentLevel];
            _sudokuLevelManager.StartLevel(level, _currentLevel);
        }
    }
}
