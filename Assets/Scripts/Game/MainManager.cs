using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    [SerializeField] private Text _highscoreText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _gameOverText, _newHighScoreText;
    
    private bool _isStarted = false;
    private int _points;
    
    private bool _isGameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        _highscoreText.text = DataManager.Data.Username + "'s Highscore: " + DataManager.Data.Highscore;
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!_isStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ExitToMenu();
            }
        }
    }

    void AddPoint(int point)
    {
        _points += point;
        _scoreText.text = $"Score : {_points}";
    }

    public void GameOver()
    {
        UpdateHighscore();
        DataManager.Data.SaveUserData();

        if(_points == DataManager.Data.Highscore)
            _newHighScoreText.SetActive(true);

        _isGameOver = true;
        _gameOverText.SetActive(true);
    }

    void UpdateHighscore()
    {
        if(_points > DataManager.Data.Highscore)
            DataManager.Data.Highscore = _points;
    }

    public void ExitToMenu()
    {
        GameOver();
        SceneManager.LoadScene(0);
    }
}
