using System.Diagnostics;
using UnityEngine;

public class BasicCardScript : MonoBehaviour
{
    bool isHPUp;//Hacer
    bool isSpeedUp;
    bool isFireRateUp;
    bool isProjectileSizeUp;
    bool isRangeUp;

    void Awake()
    {
        float randomBasicCard = Random.Range(1, 5);
        if (randomBasicCard == 1)
        {
            isHPUp = true;
        }
        else if (randomBasicCard == 2)
        {
            isSpeedUp = true;
        }
        else if (randomBasicCard == 3)
        {
            isFireRateUp = true;
        }
        else if (randomBasicCard == 4)
        {
            isProjectileSizeUp = true;
        }
        else if(randomBasicCard == 5)
        {
            isRangeUp = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerController target = other.GetComponent<PlayerController>();
        if(target != null)
        {
         if (isHPUp == true)
            {

            }
            else if (isSpeedUp == true)
            {
                target.playerSpeed += 30;
                Destroy(this);
            }
            else if (isFireRateUp == true)
            {
                target.shootTime -= 1;
                Destroy(this);
            }
            else if (isProjectileSizeUp == true)
            {
                Destroy(this);
            }
            else if (!isRangeUp == true) 
            {
                //RangeUp
                Destroy(this);
            }
        }

    }
}
