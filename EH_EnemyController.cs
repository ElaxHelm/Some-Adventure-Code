using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EH_EnemyController : MonoBehaviour
{
    //navmesh variables v
    public NavAttach[] patrolPoints;
    public NavAttach1[] patrolPoints1;
    public NavAttach2[] patrolPoints2;
    public NavMeshAgent agent;
    public int patrolpointIndex = -1;
    public int patrolpointIndex1 = -1;
    public int patrolpointIndex2 = -1;
    //navmesh variables ^

    public float speed;
    public float followDistance;
    private float idleTime;
    public float startIdleTime;
    public float enemyVision;

    private int randomPoint;
    private int randomPoint1;
    private int randomPoint2;

    public bool follow;
    public bool patrol;


    public GameObject enemy;
    public GameObject enemyArrow;
    public float fireRate;
    public float nextFire;
    public GameObject followWarning;

    public bool canMove;


    private Transform playerLoc;
    // public Transform[] patrolPoints;

    //public Transform[] patrolPointsScene3;
    //public AR_PatrolPoint[] points3;

    //public AR_PatrolPoint[] pointsSample;

    public int randomPoint3;

    public string sceneName;
    public Scene theScene;

    public bool canShoot;

    public float arrowTime;
    public float arrowIdle;

    public bool levelOne;
    public bool levelTwo;
    public bool levelThree;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    // Use this for initialization
    void Start()
    {
        if (levelOne)
        {
            patrolPoints = FindObjectsOfType<NavAttach>();
            randomPoint = Random.Range(0, patrolPoints.Length);
        }

        if (levelTwo)
        {
            patrolPoints1 = FindObjectsOfType<NavAttach1>();
            randomPoint1 = Random.Range(0, patrolPoints1.Length);
        }

        if (levelThree)
        {
            patrolPoints2 = FindObjectsOfType<NavAttach2>();
            randomPoint2 = Random.Range(0, patrolPoints2.Length);
        }





        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        theScene = SceneManager.GetActiveScene();
        sceneName = theScene.name;
        fireRate = 3f;

        nextFire = fireRate;
        canShoot = true;

        if (patrol == true)
        {
            Patrol();
        }

        canMove = true;

        //Ash added!
    }

    // Update is called once per frame
    void Update()
    {
        theScene = SceneManager.GetActiveScene();
        sceneName = theScene.name;
        if (agent.remainingDistance < 0.2f)
        {
            Patrol();
        }
        Follow();
        EnemyDecision();
    }

    void Patrol()
    {
        //if the patrol bool is true
        if (patrol == true)
        {
            if (canMove)
            {
                // our index will increase if it's less than the index's length
                if (levelOne)
                {
                    patrolpointIndex = ++patrolpointIndex < patrolPoints.Length ? patrolpointIndex : 0;
                    agent.SetDestination(patrolPoints[patrolpointIndex].transform.position);

                }

                if (levelTwo)
                {
                    patrolpointIndex1 = ++patrolpointIndex1 < patrolPoints1.Length ? patrolpointIndex1 : 0;
                    agent.SetDestination(patrolPoints1[patrolpointIndex1].transform.position);
                }

                if (levelThree)
                {
                    patrolpointIndex2 = ++patrolpointIndex2 < patrolPoints2.Length ? patrolpointIndex2 : 0;
                    agent.SetDestination(patrolPoints2[patrolpointIndex2].transform.position);
                }


                //Set the agents destination to our patrol points with the index (which are points with NavAttach script on them) position

                if (followWarning != null)
                {
                    followWarning.SetActive(false);
                }

                if (idleTime <= 0)
                {
                    if (levelOne)
                    {
                        GoToNextPoint();
                    }

                    if (levelTwo)
                    {
                        GoToNextPoint1();
                    }

                    if (levelThree)
                    {
                        GoToNextPoint2();
                    }


                    idleTime = startIdleTime;
                }
                else
                {
                    idleTime -= Time.deltaTime;
                }
            }
        }

    }

    //Enemy's follow event.  Make sure Enemy's who are following have their vision greater than 1 or else they will never see the player.  
    void Follow()
    {

        // If the follow bool is true
        if (follow == true)
        {
            if (canMove)
            {
                //If our distance is less than or equal to the enemy's vision range
                if (Vector2.Distance(transform.position, playerLoc.position) <= enemyVision)
                {
                    //If the players location is greater than our follow distance
                    if (Vector2.Distance(transform.position, playerLoc.position) > followDistance)
                    {
                        // Enemy's new position will move towards the players position based one the speed input.
                        agent.SetDestination(playerLoc.position);
                        EnemyAttack();
                        if (followWarning != null)
                        {
                            followWarning.SetActive(true);
                        }
                    }

                }
            }

        }
    }

    void EnemyDecision()
    {

        // If the players location is less than or equal to our vision range and follow was checked for the enemy
        if (Vector2.Distance(transform.position, playerLoc.position) <= enemyVision)
        {
            // we should follow them and not patrol!
            follow = true;
            patrol = false;

        }
        // or if their location is greater than our vision and we can't see them and patrol was checked for the enemy
        else if (Vector2.Distance(transform.position, playerLoc.position) >= enemyVision)
        {
            //we should patrol and not follow them!
            patrol = true;
            follow = false;
        }


    }

    void EnemyAttack()
    {
        if (Time.time > nextFire && canShoot == true)
        {
            GameObject newEnemyArrow = Instantiate(enemyArrow, enemy.transform.position, enemy.transform.rotation);
            Vector3 difference = playerLoc.position - newEnemyArrow.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            newEnemyArrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            newEnemyArrow.transform.position = Vector2.MoveTowards(enemy.transform.position, playerLoc.position, Time.deltaTime * 2);
            nextFire = Time.time + fireRate;
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;
        agent.destination = patrolPoints[patrolpointIndex].position;
        patrolpointIndex = (patrolpointIndex + 1) % patrolPoints.Length;
    }

    void GoToNextPoint1()
    {
        if (patrolPoints1.Length == 0)
            return;
        agent.destination = patrolPoints1[patrolpointIndex1].position;
        patrolpointIndex1 = (patrolpointIndex1 + 1) % patrolPoints1.Length;
    }

    void GoToNextPoint2()
    {
        if (patrolPoints2.Length == 0)
            return;
        agent.destination = patrolPoints2[patrolpointIndex2].position;
        patrolpointIndex2 = (patrolpointIndex2 + 1) % patrolPoints2.Length;
    }
}
