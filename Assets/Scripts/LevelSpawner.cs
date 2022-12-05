using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.UIElements;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [Header("Blocks")]
    [SerializeField] private GameObject[] BlockPoints;
    [SerializeField] private LeanGameObjectPool BlockPool;
    [Header("Tower")]
    [SerializeField] private GameObject[] TowerPoints;
    [SerializeField] private int TowerHeight = 7;
    [Header("Coins")]
    [SerializeField] private GameObject[] CoinsPoints;
    [SerializeField] private LeanGameObjectPool CoinsPool;
    [Header("Simple Hurdles")]
    [SerializeField] private GameObject[] HurdlePoints;
    [SerializeField] private LeanGameObjectPool FulllanePool;
    [SerializeField] private LeanGameObjectPool DoubleFulllanePool;
    [SerializeField] private LeanGameObjectPool SquarePool;
    [Header("wall")]
    [SerializeField] private GameObject[] WallPoints;
    [SerializeField] private LeanGameObjectPool WallPool;
    private GameObject block;
    private int ran;
    void Awake()
    {
        SpawnBlocksOnLevel();
        SpawnCoinsOnLevel();
        SpawnSimplehurdlesOnLevel();
        SpawnWallsOnLevel();

    }

    /// <summary>
    /// Spawn levels items from poolers on any new level start. 
    /// </summary>
    public void InitializeLevel() {
        LeanPool.DespawnAll();
        SpawnBlocksOnLevel();
        SpawnCoinsOnLevel();
        SpawnSimplehurdlesOnLevel();
        SpawnWallsOnLevel();
    }
    /*private void SpawnBlocksOnLevel()
    {
        int length = BlockPoints.Length;

        for (int i = 0; i < length; i++)
        {
            ran = Random.Range(-2, 3);
            block = BlockPool.Spawn(new Vector3(ran,
                    BlockPoints[i].transform.position.y,
                    BlockPoints[i].transform.position.z), Quaternion.identity, BlockPoints[i].transform) as GameObject;
            block.GetComponent<CubeController>().OnCollide = _playerController.IncreaseTower;
            block = null;
        }
    }*/

    private void SpawnBlocksOnLevel()
    {
        int countInBlocks = 0;
        bool isRan = false;
        int A, B = 0;
        A = 0;
        B = 0;
        List<int> numbers = new List<int>();
       
        isRan = false;
        int length = BlockPoints.Length;
        countInBlocks = 0;
        for (int i = 0; i < length; i++)
        {
            if (!isRan)
            {
                isRan = true;
                numbers.Add(0);
                numbers.Add(1);
                numbers.Add(2);
                numbers.Add(3);
                A = Random.Range(0,numbers.Count);
                numbers.RemoveAt(A);
             /*   B = Random.Range(0, numbers.Count);
                numbers.RemoveAt(B);*/
            }
            if (countInBlocks < 4)
            {
                
                if (countInBlocks != numbers[0]/* || countInBlocks == numbers[1]*/)
                {
                    ran = Random.Range(-2, 3);
                    block = BlockPool.Spawn(new Vector3(ran,
                            BlockPoints[i].transform.position.y,
                            BlockPoints[i].transform.position.z), Quaternion.identity, BlockPoints[i].transform) as GameObject;
                    block.GetComponent<CubeController>().OnCollide = _playerController.IncreaseTower;
                    block = null;
                }
                countInBlocks++;
            }
            else
            {
                countInBlocks = 0;
                isRan = false;
            }

           
        }
    }
    private void SpawnWallsOnLevel()
    {
       int length = WallPoints.Length;
       int rd = Random.Range(0,100);
       bool add = rd % 2 == 0;
       for (int i = 0; i < length; i++)
       {
            if (add)
            { 
                block = WallPool
                    .Spawn(new Vector3(0,
                  WallPoints[i].transform.position.y,
                  WallPoints[i].transform.position.z), Quaternion.identity) as GameObject;
         
                if (Random.Range(0,49) % 2 == 0)
                {
                    block.transform.localScale = new Vector3(-1,1,1);
                }
                block = null;
                SpawnTowerOnLevel(i);
            }
            add = !add;
        }
    }
    private void SpawnTowerOnLevel(int i)
    {
        ran = Random.Range(-2, 3);
        int cbs = Random.Range(4, TowerHeight);
        CubeController tower;
        block = BlockPool.Spawn(new Vector3(ran,
                TowerPoints[i].transform.position.y,
                TowerPoints[i].transform.position.z), Quaternion.identity, BlockPoints[i].transform) as GameObject;
        block.GetComponent<CubeController>().OnCollide = _playerController.IncreaseTower;
        tower = block.GetComponent<CubeController>();
        block = null;
        for (int j = 1; j < cbs; j++)
        {
            block = BlockPool.Spawn(tower.nextCubePoint.transform.position, Quaternion.identity, BlockPoints[i].transform) as GameObject;
            block.GetComponent<CubeController>().OnCollide = _playerController.IncreaseTower;
            tower = block.GetComponent<CubeController>();
            block = null;
        }
        
    }

    //Spawn coins on level
    private void SpawnCoinsOnLevel()
    {
        int SameSideLimit, sideCount = 0;
        bool IsLimitCreated = false;
        int length = CoinsPoints.Length;
        SameSideLimit = 0; sideCount = 0;
        for (int i = 0; i < length; i++)
        {
            if (!IsLimitCreated)
            {
                SameSideLimit = Random.Range(2, 5);
                ran = Random.Range(-2, 3);
                IsLimitCreated = true;
            }
            if (sideCount < SameSideLimit)
            {
                sideCount++;
            }
            else 
            {
                IsLimitCreated = false;
                sideCount = 0;
            }
            CoinsPool.Spawn(new Vector3(ran,
                CoinsPoints[i].transform.position.y,
                CoinsPoints[i].transform.position.z), Quaternion.identity);  
        }
    }

    //Spawn simple hurdles on level
    private void SpawnSimplehurdlesOnLevel()
    {
        int HurdleType = 0;
        int length = HurdlePoints.Length;
        for (int i = 0; i < length; i++)
        {
            HurdleType = Random.Range(1,4);
            switch (HurdleType)
            {
                case 1:
                    FulllanePool.Spawn(new Vector3(0,
                        HurdlePoints[i].transform.position.y,
                        HurdlePoints[i].transform.position.z), Quaternion.identity);
                    break;
                case 2:
                  
                    DoubleFulllanePool.Spawn(new Vector3(0,
                        HurdlePoints[i].transform.position.y,
                        HurdlePoints[i].transform.position.z), Quaternion.identity);
                    break;
                case 3:
                    ran = Random.Range(-1, 2);
                    SquarePool.Spawn(new Vector3(ran,
                        HurdlePoints[i].transform.position.y,
                        HurdlePoints[i].transform.position.z), Quaternion.identity);
                    break;
                default:
                    break;
            }
  
        }
    }
}
