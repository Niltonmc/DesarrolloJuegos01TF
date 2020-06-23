using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemiesContainerControl : MonoBehaviour{

    [Header("Enemy Prefab Variables")]
	public GameObject enemyPref;
    private EnemyControl enemyCreated;

    [Header("Enemy Position Variables")]
    public float minPosY;
    public float maxPosY;
    private float ySelected;
    public float minPosX;
    public float maxPosX;
    private float xSelected;

    [Header("Enemy Position Variables")]
    public float minSpeedEnemyMovement;
    public float maxSpeedEnemyMovement;

    [Header("Enemy Time Creation Variables")]
    public float minTimeToCreateEnemy;
    public float maxTimeToCreateEnemy;
    public float currentTimeToCreateEnemy;
    private float timeSelected = 0;

    [Header("Enemy List Variables")]
    public List<EnemyControl> allEnemiesList;

    [Header("Enemy Types Variables")]
    public List<Sprite> allEnemiesSprites;
    public List<string> allEnemiesTypes;

    [Header("SFX Variables")]
    public SFXControl sfxShoot;
    public SFXControl sfxDie;

    [Header("Game Manager Variables")]
    public GameManagerControl gmManager;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        currentTimeToCreateEnemy = currentTimeToCreateEnemy + Time.deltaTime;
        if(currentTimeToCreateEnemy >= timeSelected){
             timeSelected = GetRandomTime();
             currentTimeToCreateEnemy = 0;
             CreateEnemy();
        }
    }

    void CreateEnemy(){
        Vector2 enemyPos = GetRandomPos();
        int enemyTpe = SelectEnemyType();
        enemyCreated = Instantiate(enemyPref,enemyPos,
        enemyPref.transform.rotation).GetComponent<EnemyControl>();
        enemyCreated.SelectSpeed(minSpeedEnemyMovement,maxSpeedEnemyMovement);
        enemyCreated.SetEnemyContainer(this.gameObject.GetComponent<AllEnemiesContainerControl>());
        enemyCreated.SetSFXs(sfxShoot,sfxDie);
        enemyCreated.SetEnemyType(allEnemiesSprites[enemyTpe],enemyTpe,allEnemiesTypes[enemyTpe]);
        enemyCreated.transform.SetParent(this.gameObject.transform);
        allEnemiesList.Add(enemyCreated);
    }

    int SelectEnemyType(){
        int enemyType = Random.Range(0,allEnemiesSprites.Count);
        return enemyType;
    }

    float GetRandomTime(){
        float result = Random.Range(minTimeToCreateEnemy,maxTimeToCreateEnemy);
        return result;
    }
    Vector2 GetRandomPos(){
        xSelected = Random.Range(minPosX,maxPosX);
        ySelected = Random.Range(minPosY,maxPosY);
        Vector2 newPos = new Vector2(xSelected,ySelected);
        return newPos;
    }

    public void RemoveFromList(EnemyControl enemy){
        allEnemiesList.Remove(enemy);
    }

    public void DestroyAllEnemies(){
        for(int i = 0; i < allEnemiesList.Count;++i){
            if(allEnemiesList[i] != null){
                allEnemiesList[i].Die();
            }
        }
        allEnemiesList.Clear();
    }

    public void IncreaseDifficulty(int difficulty){
        switch(difficulty){
            case 3:
                minSpeedEnemyMovement = minSpeedEnemyMovement + 1;
                maxSpeedEnemyMovement = maxSpeedEnemyMovement + 1;
                minTimeToCreateEnemy = minTimeToCreateEnemy - 1;
                maxTimeToCreateEnemy = maxTimeToCreateEnemy - 1;
            break;

              case 6:
                minSpeedEnemyMovement = minSpeedEnemyMovement + 1;
                maxSpeedEnemyMovement = maxSpeedEnemyMovement + 1;
                maxTimeToCreateEnemy = maxTimeToCreateEnemy - 1;
            break;

            default:
                minSpeedEnemyMovement = minSpeedEnemyMovement + 1;
                maxSpeedEnemyMovement = maxSpeedEnemyMovement + 1;
            break;
        }
    }
}
