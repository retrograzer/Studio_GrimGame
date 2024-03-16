using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HazardBehavior : MonoBehaviour
{
    public enum EffectType
    {
        slow, //Slows the player down but does not do damage
        cripple, //Stuns the player
        poison, //Slowly hurts player until at 1 health or finds water
        water, //cures poison
        fire //Does 1 damage
    }

    public EffectType effect;

    float[] wearOffTimes = {3f, 2f};
    PlayerInteraction pi;

    private void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ApplyEffectPlayer();
        }
        else if (collision.tag == "Enemy")
        {
            ApplyEffectEnemy(collision.GetComponent<EnemyHealth>());
        }
    }

    void ApplyEffectPlayer ()
    {
        switch (effect)
        {
            case EffectType.slow:
                pi.ApplySlowEffect();
                break;
            case EffectType.cripple:
                pi.ApplyCrippleEffect();
                break;
            case EffectType.poison:
                pi.ApplyPoisonEffect();
                break;
            case EffectType.water:
                pi.ApplyWaterEffect();
                break;
            case EffectType.fire:
                pi.ApplyFireEffect();
                break;
        }
    }

    void ApplyEffectEnemy (EnemyHealth eh)
    {
        switch (effect)
        {
            case EffectType.slow:
                break;
            case EffectType.cripple:
                break;
            case EffectType.poison:
                break;
            case EffectType.water:
                break;
            case EffectType.fire:
                eh.TakeDamage(1, transform.position);
                break;
        }
    }

    void EffectWearoff ()
    {

    }
}
