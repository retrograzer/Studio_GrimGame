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
        fire, //Does 1 damage
        crows //Gives 1 crow
    }

    public EffectType effect;

    float[] wearOffTimes = {3f, 2f};
    PlayerManager pm;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
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
                pm.pi.ApplySlowEffect();
                break;
            case EffectType.cripple:
                pm.pi.ApplyCrippleEffect();
                break;
            case EffectType.poison:
                pm.pi.ApplyPoisonEffect();
                break;
            case EffectType.water:
                pm.pi.ApplyWaterEffect();
                break;
            case EffectType.fire:
                pm.pi.ApplyFireEffect();
                break;
            case EffectType.crows:
                pm.pca.AddCrows(1);
                Destroy(gameObject);
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
