using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float _moveTimer;
    private float _losePlayerTimer;
    private float _shotTimer;


    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer()) // player can be seen
        {
            // lock the lose player timer and increment the move ans shot timers
            _losePlayerTimer = 0.0f;
            _moveTimer += Time.deltaTime;
            _shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            // if shot timer > fireRate
            if(_shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            // move the enemy to a random position after a random time
            if (_moveTimer > Random.Range(5, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                _moveTimer = 0.0f;
            }

            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else // lost sight of player 
        {
            _losePlayerTimer += Time.deltaTime;
            if (_losePlayerTimer > 8.0f) // 8.0f is 8sec time before change to PatrolState
            {
                // change to the search state
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        // store reference to the gun barrel
        Transform gunbarrel = enemy.gunBarrel;

        // instantiate a new bullet
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Projectiles/FarmerHat_LOD0") as GameObject, gunbarrel.position, Quaternion.identity);
        // calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        // add force rigidbody of the bullet
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * 40;
        Debug.Log("Shoot");
        _shotTimer = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
