
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Action GameIsStarting;
    protected static bool runOnce = false;


    [SerializeField] bool skipStartMenu;
    [SerializeField] bool paused = false;

    [Header("Game Menus")]
    [SerializeField] protected GameObject startMenu;
    [SerializeField] protected GameObject pauseMenu;
    [SerializeField] protected GameObject gameOverMenu;
    [SerializeField] protected GameObject winMenu;

    [Header("In Game UI")]
    [SerializeField] protected GameObject inGameUI;
    [SerializeField] protected GameObject theDumbFucks;
    [SerializeField] bool allowToDisableGameUI;



    public enum GameState
    {
        Start,
        Playing,
        Paused,
        GameOver,
        Win,
    }

    public static GameState StartState = GameState.Start;
    public static GameState PlayingState = GameState.Playing;
    public static GameState PausedState = GameState.Paused;
    public static GameState GameOverState = GameState.GameOver;
    public static GameState WinState = GameState.Win;

    public GameState CurrentGameState = GameState.Start;

    

    private void OnEnable()
    {
        PlayerDeath.pleaseYouShouldDie += TransitionToGameOverState;
        Timer.YouLostBitch += TransitionToGameOverState;
        PlayerWinCondition.IdiotHasWon += WinGame;
    }


    private void OnDisable()
    {
        PlayerDeath.pleaseYouShouldDie -= TransitionToGameOverState;
        PlayerWinCondition.IdiotHasWon -= WinGame;
        Timer.YouLostBitch -= TransitionToGameOverState;
    }



    protected virtual void Start()
    {
        StartGame();
    }

    protected virtual void Update()
    {
        PlayGameState();
        TransitionToPauseState();
        PauseGameOrContinueGame();
        OnInputDisableGameUI(allowToDisableGameUI);

    }
    protected virtual bool StartGame()
    {
        GameIsStarting?.Invoke();
        DisableAllUI();
        theDumbFucks = FindObjectOfType<DumbFuck>().gameObject;
        EnableOrDisableUI(ref theDumbFucks,false);
        if (!skipStartMenu)
        {
            EnableOrDisableUI(ref startMenu, true);
            
            return false;
        }
        TransitionToPlayState();

        return true;
    }


    public virtual void TransitionToPlayState()
    {
        UpdateCurrentGameState(PlayingState);
    }

    protected virtual bool PlayGameState()
    {
        if (runOnce) return false;

        if (IsCurrentState(PlayingState))
        {
            EnableOrDisableUI(ref inGameUI, true);
            EnableOrDisableUI(ref startMenu, false);
            EnableOrDisableUI(ref theDumbFucks,true);
            Time.timeScale = 1;
            return true;
        }
        return false;
    }

    protected virtual void TransitionToPauseState()
    {
        if (!IsCurrentState(PlayingState)) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateCurrentGameState(PausedState);
        }
    }

    protected virtual void PauseGameOrContinueGame()
    {
        // later on this needs to have a customised condtion because mouse hover is not disabled
        if (pauseMenu == null) return;
        if (IsCurrentState(PausedState) && !runOnce)
        {
            EnableOrDisableUI(ref pauseMenu, true);
            Time.timeScale = 0;
            runOnce = true;
            return;
        }
        ResumeGameThroughInput();
    }

    public virtual void ResumeGame()
    {
        TransitionToPlayState();
        EnableOrDisableUI(ref pauseMenu, false);
        Time.timeScale = 1;
    }

    void ResumeGameThroughInput()
    {
        if (!IsCurrentState(PausedState) && !runOnce) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
            Debug.Log("Pause");
        }
    }

    protected virtual void  TransitionToGameOverState()
    {
        if (IsCurrentState(GameOverState)) return;

        if (IsCurrentState(PlayingState))
        {
            UpdateCurrentGameState(GameOverState);
            GameOver();
        }

    }
    protected virtual bool GameOver()
    {
        if (runOnce) return false;

        if (IsCurrentState(GameOverState))
        {
            EnableOrDisableUI(ref inGameUI, false);
            EnableOrDisableUI(ref gameOverMenu, true);
            EnableOrDisableUI(ref theDumbFucks, false);
            Debug.Log("Game Over");
            return true;
        }
        return false;
    }

    protected virtual void  WinGame()
    {
        if (runOnce) return;

        if (IsCurrentState(PlayingState))
        {
            EnableOrDisableUI(ref inGameUI, false);
            EnableOrDisableUI(ref winMenu, true);
            EnableOrDisableUI(ref theDumbFucks, false);
            Debug.Log("Game Over");
            return ;
        }
        return;
    }

    #region Game Manager Helper Functions
    public  bool IsCurrentState(GameState _state)
    {
        if (CurrentGameState == _state)
        {
            return true;
        }
        return false;
    }

    public  void UpdateCurrentGameState(GameState _state)
    {
        CurrentGameState = _state;
        runOnce = false;
    }

    protected void EnableOrDisableUI(ref GameObject _menu, bool _enabled)
    {
        if (_menu == null) return;
        _menu.SetActive(_enabled);
    }

    public void DisableAllUI()
    {
        if (startMenu != null) startMenu?.SetActive(false);
        if (pauseMenu != null) pauseMenu?.SetActive(false);
        if (gameOverMenu != null) gameOverMenu?.SetActive(false);
        if (winMenu != null) winMenu?.SetActive(false);
        if (inGameUI != null) inGameUI?.SetActive(false);
    }

    public void RestartGame(bool _skipStartScreen)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        skipStartMenu = _skipStartScreen;
        theDumbFucks = null;
        UpdateCurrentGameState(StartState);
        Start();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void OnInputDisableGameUI(bool _disableGameUI)
    {
        if (Input.GetKeyDown(KeyCode.U) && allowToDisableGameUI)
        {
            inGameUI.SetActive(!inGameUI.activeInHierarchy);
        }
    }
    #endregion
}
