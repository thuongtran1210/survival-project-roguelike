using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
    private bool collected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Collect(Player playerTranform)
    {
        if (collected)
            return;
        collected = true;
        StartCoroutine(MoveTowardsPlayer(playerTranform));
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

    private void Collected()
    {
        gameObject.SetActive(false);
    }
}
