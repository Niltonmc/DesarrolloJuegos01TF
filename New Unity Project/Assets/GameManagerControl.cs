using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManagerControl : MonoBehaviour{

    [Header("Player Prefab Variables")]
    public GameObject playerPrefab;
    private PlayerControl currentPlayer;

    [Header("Player Position Variables")]
    public Vector3 playerPosToCreate;

    [Header("Player Variables")]
    public int playerLives;
    public List<Image> allImgPlayerLives;
    public float playerSpeedXMovement;
     public float playerSpeedYMovement;
    public float playerTimeToShoot;

    [Header("Score Variables")]
    public Text txtScore;
    public int currentScore;

    [Header("SFX Variables")]
    public SFXControl sfxShoot;
    public SFXControl sfxDie;
    public SFXControl sfxLives;

    [Header("Live Prefab Variables")]
    public GameObject livePrefab;

    [Header("Live Position Variables")]
    public float livePosX;
    public float minLivePosY;
    public float maxLivePosY;
    private Vector3 posPlayerLive;

    [Header("Live Time Variables")]
    public bool canCreateLive;
    public float timeToCreatePlayerLive;
    private float currentTimeToCreatePlayerLive;

    [Header("Pause Variables")]
    public Button btnPause;

    // Start is called before the first frame update
    void Start(){
        CreatePlayer();
        UpdateScore(0);
        btnPause.onClick.AddListener(() => ChangeGameState());
    }

    // Update is called once per frame
    void Update(){
        if(canCreateLive == false){
            currentTimeToCreatePlayerLive = currentTimeToCreatePlayerLive + Time.deltaTime;
            if(currentTimeToCreatePlayerLive > timeToCreatePlayerLive){
                canCreateLive = true;
            }
        }
        if(canCreateLive == true && playerLives < 3){
            canCreateLive = false;
            currentTimeToCreatePlayerLive = 0;
            CreateLive();
        }
       
    }

    void CreateLive(){
        float yPos = Random.Range(minLivePosY,maxLivePosY);
        posPlayerLive = new Vector3(livePosX,yPos,0);
        Instantiate(livePrefab, posPlayerLive, livePrefab.transform.rotation);
    }

    void CreatePlayer(){
        currentPlayer = Instantiate(playerPrefab,playerPosToCreate,
        playerPrefab.transform.rotation).GetComponent<PlayerControl>();
        currentPlayer.SetInitialVariables(this.gameObject.GetComponent<GameManagerControl>(),
        playerLives,playerSpeedXMovement,
        playerSpeedYMovement,playerTimeToShoot);
        currentPlayer.SetSFXs(sfxShoot,sfxDie,sfxLives);
    }

    public void UpdateScore(int score){
        currentScore = currentScore + score;
        txtScore.text = "SCORE: " + currentScore.ToString();
    }
    public void UpdateLives(int lives){
        allImgPlayerLives[playerLives-1].gameObject.SetActive(false);
        playerLives = lives;
        if(playerLives <= 0){
            GameOver();
        }
    }

    public void EarnLives(int lives){
        if(playerLives < 3){
            playerLives = lives;
            allImgPlayerLives[playerLives-1].gameObject.SetActive(true);
        }
    }

    void ChangeGameState(){
        float tmp = Time.timeScale;
        if(tmp == 1){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
    }

    void GameOver(){
        SceneManager.LoadScene("Gameplay");
    }
}
