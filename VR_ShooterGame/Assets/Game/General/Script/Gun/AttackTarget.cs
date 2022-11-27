using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTarget : MonoBehaviour
{
    public abstract void ReceiveAttack(int damage, GameObject damagesDealer);
}
