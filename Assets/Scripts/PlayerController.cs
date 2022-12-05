using UnityEngine;
using Hellmade.Sound;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject Actor;
    [SerializeField] [Range(1f, 1.7f)] private float ActorJumpUpSteps = 1.0f;
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private CubeController _cubeContoller;
    [SerializeField] private AudioClip cubeAudioClip;
    [SerializeField] private bool IsMoveForwardInZ;
    [SerializeField] [Range(1f, 5f)] private float speedForZ = 1.0f;
    public float SpeedForZ {
        //for camera movement
        get { return speedForZ; }
    }
    private int TowerHeight = 1;
    //moving object forward with given speed. 
    void Update()
    {
        if (IsMoveForwardInZ) { transform.Translate(0, 0, Time.deltaTime * speedForZ, Space.World); }
    }
    private void Start()
    {
        CubeController.DecreaseTower = DecreaseTowerHeight;
        TowerHeight = 1;
    }
    /// <summary>
    ///  Add cubes in player stack in game play. Can call anywhere with instance
    /// </summary>
    public void IncreaseTower(GameObject block)
    {
        block.tag = "Untagged";
        TowerHeight++;
        if (Actor) { Actor.transform.position += new Vector3(0, ActorJumpUpSteps, 0);}
        block.transform.parent = transform;
        block.transform.position = _cubeContoller.nextCubePoint.transform.position;
       // GameObject cube = block;//Instantiate(CubePrefab, _cubeContoller.nextCubePoint.transform.position, Quaternion.identity);
        if (block.TryGetComponent(out CubeController cb))
        {
            _cubeContoller = cb;
        }
        block = null;
        //play audio
        EazySoundManager.PlayMusic(cubeAudioClip, 1, false, false, 0, 0);
    }
    private void DecreaseTowerHeight() {
        TowerHeight--;
        if (TowerHeight <= 0)
        {
            _gameManager.OnGameComplete();
        }
    }
}
