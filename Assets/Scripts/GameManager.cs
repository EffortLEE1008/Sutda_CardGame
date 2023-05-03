using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [SerializeField]
    GameObject[] card;

    Rigidbody2D[] card_rigid= new Rigidbody2D[8];

    [SerializeField]
    GameObject[] target;

    bool isstart = true;    

    float timer=0;
    void Start()
    {
     
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //card[0].transform.position = Vector3.MoveTowards(card[0].transform.position, target[0].transform.position, 5 * Time.deltaTime);

    }

    public void GameStart()
    {
        for(int i =0; i < card.Length; i++)
        {
            StartCoroutine(MoveCard(card[i], target[i]));
            
            if(i==6 || i == 7)
            {
                card[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
        }

    }



    IEnumerator MoveCard(GameObject my_card, GameObject my_target)
    {
        while (timer<15)
        {
            timer = timer + Time.deltaTime;
            Debug.Log("´­¸²");
            my_card.transform.position=Vector3.MoveTowards(my_card.transform.position, my_target.transform.position, 5 * Time.deltaTime);

            yield return null;

        }
        Debug.Log("½ÇÇàµÊ");


    }

    IEnumerator Waittime()
    {
        yield return new WaitForSeconds(2f);
    }



}
