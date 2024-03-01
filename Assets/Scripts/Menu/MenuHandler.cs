using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highscore, _nickname;
    void Start()
    {
        _highscore.text = DataManager.Data.Username + " Highscore: " + DataManager.Data.Highscore;
        _nickname.text = DataManager.Data.Username;
    }
    
    public void StartGame()
    {
        //DataManager.Data.Username = _nickname.text;
        DataManager.Data.SaveUserData();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //DataManager.Data.Username = _nickname.text;
        DataManager.Data.SaveUserData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveName()
    {
        DataManager.Data.Username = _nickname.text;
    }
}
