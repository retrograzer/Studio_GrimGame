using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyManager : MonoBehaviour
{
    public enum EnemyType
    {
        accepting,
        fleeing,
        melee,
        ranged
    }

    public EnemyType enemyType;

    [HideInInspector] public Rigidbody2D rbd;
    [HideInInspector] public EnemyHealth eh;
    [HideInInspector] public AudioSource src;
    [HideInInspector] public SpriteRenderer rend;

    private float tempMaxSpeed = 0f;

    private void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        eh = GetComponent<EnemyHealth>();
        src = GetComponent<AudioSource>();
        rend = GetComponentInChildren<SpriteRenderer>();

        switch (enemyType)
        {
            case EnemyType.accepting:
                break;
            case EnemyType.fleeing:
                tempMaxSpeed = GetComponent<RunningEnemy>().speed;
                break;
            case EnemyType.melee:
                tempMaxSpeed = GetComponent<MeleeEnemy>().speed;
                break;
            case EnemyType.ranged:
                tempMaxSpeed = GetComponent<RangedEnemy>().speed;
                break;
        }
    }

    public void ChangeEnemyMovement (float newSpeed)
    {
        switch (enemyType)
        {
            case EnemyType.accepting:
                break;
            case EnemyType.fleeing:
                GetComponent<RunningEnemy>().speed = newSpeed;
                break;
            case EnemyType.melee:
                GetComponent<MeleeEnemy>().speed = newSpeed;
                break;
            case EnemyType.ranged:
                GetComponent<RangedEnemy>().speed = newSpeed;
                break;
        }
    }

    public void FlashColor (Color newColor, float duration)
    {
        rend.color = newColor;
        Invoke("EndColorChange", duration);
    }

    void EndColorChange()
    {
        rend.color = Color.white;
    }

    public void StunEnemy (float duration)
    {
        ChangeEnemyMovement(0);
        Invoke("StunEnd", duration);
    }

    void StunEnd ()
    {
        ChangeEnemyMovement(tempMaxSpeed);
    }
}
