using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropableCurrency : MonoBehaviour, ICollectable
{
    private bool collected;
    private void OnEnable()
    {
        collected = false;
    }
    public void Collect(Player player)
    {
        if (collected)
            return;
        collected = true;
        StartCoroutine(MoveTowardsPlayer(player));
    }
    IEnumerator MoveTowardsPlayer(Player playerTranform)
    {
        float timer = 0;
        Vector2 initialPositon = transform.position;

        while (timer < 1)
        {
            Vector2 targetPositon = playerTranform.GetCenter();
            transform.position = Vector2.Lerp(initialPositon, targetPositon, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        Collected();
    }

    protected abstract void Collected();

}
