using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    private GameObject _player;
    private Vector3 _lastKnowPos;

    public NavMeshAgent Agent { get => _agent; }
    public GameObject Player { get => _player; }
    public Vector3 LastKnowPos {  get => _lastKnowPos; set => _lastKnowPos = value; }

    
    public Path path;
    public GameObject debugSphere;

    [Header("Sight Values")]
    public float sightDistance = 20.0f;
    public float fieldOfView = 85.0f;
    public float eyeHeight = 0.6f;

    [Header("WeaponValues")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)] public float fireRate = 2.0f;

    // just for debugging purposes
    [SerializeField] private string _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Initialise();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        _currentState = _stateMachine.activeState.ToString();
        debugSphere.transform.position = _lastKnowPos;
    }

    public bool CanSeePlayer()
    {
        if (_player != null)
        {
            // is the player close enough to be seen ?
            if (Vector3.Distance(transform.position, _player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = _player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                if(angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if(hitInfo.transform.gameObject == _player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}