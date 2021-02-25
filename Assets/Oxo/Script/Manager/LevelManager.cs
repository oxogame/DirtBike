using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public static LevelManager levelManager;

    public int level;

    int totalTutorialStep;
    int currentTutorialStep;

    public GameObject startPlatformPrefab;
    public GameObject finishPlatformPrefab;

    public List<GameObject> obstaclePrefabList = new List<GameObject>();
    public SpawnType obstacleSpawnType;
    public int obstacleSpawnDelay;
    public int minObstacleCountInLevel;
    public int maxObstacleCountInLevel;
    List<GameObject> obstacleList = new List<GameObject>();

    public GameObject tutorialSteps;
    public GameObject obstacleSpawnPositions;

    List<GameObject> obstacleSpawnPositionList = new List<GameObject>();
    List<GameObject> tutorialStepList = new List<GameObject>();

    private void Awake()
    {
        if (levelManager == null)
            levelManager = this;
        else if (levelManager != this)
            Destroy(gameObject);

        currentTutorialStep = PlayerPrefs.GetInt("TutorialStep");

        if (tutorialSteps != null)
            totalTutorialStep = tutorialSteps.transform.childCount;

        FillList(tutorialStepList, tutorialSteps);
        FillList(obstacleSpawnPositionList, obstacleSpawnPositions);
    }

    private void Start()
    {
        SetLevel();
    }

    public void SetLevel()
    {

        if (startPlatformPrefab != null)
        {
            GameObject go = Instantiate(startPlatformPrefab);
            go.transform.position = Vector3.zero;
            go.transform.rotation = Quaternion.identity;
            go.SetActive(true);
        }

        if (obstacleSpawnType == SpawnType.Awake)
        {
            if (maxObstacleCountInLevel > obstacleSpawnPositionList.Count - 1)// for no null check // -1 for at least one finish position return 
            {
                maxObstacleCountInLevel = obstacleSpawnPositionList.Count - 1;
            }

            int obstacleCount;

            if (minObstacleCountInLevel + level / totalTutorialStep > maxObstacleCountInLevel)
                obstacleCount = maxObstacleCountInLevel;
            else
                obstacleCount = minObstacleCountInLevel + level / totalTutorialStep;

            for (int i = 0; i < obstacleCount; i++)
            {
                SpawnObstacle();
            }

            if (finishPlatformPrefab != null)
            {
                GameObject go = Instantiate(finishPlatformPrefab);
                go.transform.SetParent(obstacleSpawnPositionList[obstacleCount + 1].transform); // +1 return 64 -1 
                go.transform.localPosition = Vector3.zero;
                go.transform.rotation = Quaternion.identity;
                go.SetActive(true);
            }
        }
        else if (obstacleSpawnType == SpawnType.ByTime)
        {
            StartCoroutine(SpawnObstacleByTime());
        }
        else if (obstacleSpawnType == SpawnType.ByMethod)
        {
            SpawnObstacle();
        }
    }

    public void SpawnObstacle()
    {
        GameObject spawnPoint = obstacleSpawnPositionList.Where(x => x.transform.childCount == 0).FirstOrDefault();

        int minRandomIndex = level % totalTutorialStep;

        int maxRandomIndex;
        if (level >= obstaclePrefabList.Count)
            maxRandomIndex = obstaclePrefabList.Count;
        else
            maxRandomIndex = level % obstaclePrefabList.Count;

        GameObject go = Instantiate(obstaclePrefabList[Random.Range(minRandomIndex, maxRandomIndex)]);
        go.transform.SetParent(spawnPoint.transform);
        go.transform.localPosition = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.SetActive(true);
        obstacleList.Add(go);
    }

    IEnumerator SpawnObstacleByTime()
    {
        while (obstacleList.Count < maxObstacleCountInLevel)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(obstacleSpawnDelay);
        }
    }

    void FillList(List<GameObject> list, GameObject parentObject)
    {
        foreach (Transform child in parentObject.transform)
        {
            list.Add(child.gameObject);

        }
    }

    public void ComplateTutorialAction()
    {
        currentTutorialStep++;
        PlayerPrefs.SetInt("TutorialStep", currentTutorialStep);
    }

    public void OpenTutorialAction()
    {
        tutorialStepList[currentTutorialStep].SetActive(true);
    }

    public enum SpawnType { Awake, ByTime, ByMethod };
}
