using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Sprite[] my_sprite;
    
    [SerializeField]
    GameObject[] card;
    
    [SerializeField]
    GameObject[] target;

    [SerializeField]
    Text info_text;

    [SerializeField]
    Text info_text2;


    Rigidbody2D[] card_rigid= new Rigidbody2D[8];
    SpriteRenderer[] sr_array = new SpriteRenderer[8];
    Sprite[] card_sprite_info= new Sprite[8];


    bool isstart = true;
    bool isopencard = false;
    bool isbatting = false;
    float timer = 0;

    List<int> cardnum = new List<int>();

    List<string> player0 = new List<string>();
    List<string> computer1 = new List<string>();
    List<string> computer2 = new List<string>();
    List<string> computer3 = new List<string>();

    [SerializeField]
    List<string> player_card_lst = new List<string>();
    [SerializeField]
    List<int> player_value_lst = new List<int>();

    //ÇöÀç´Â ¶¯ÀâÀÌ °ª¸¸ µé¾î°¡ ÀÖ½À´Ï´Ù. ·£´ý°ªÀ» ³Ö¾î¼­ ¼öÁ¤ÇØº¸¼¼¿ä
    void Start()
    {
        card_sprite_info[0] = my_sprite[5];
        card_sprite_info[1] = my_sprite[13];

        card_sprite_info[2] = my_sprite[6];
        card_sprite_info[3] = my_sprite[7];

        card_sprite_info[4] = my_sprite[10];
        card_sprite_info[5] = my_sprite[13];

        card_sprite_info[6] = my_sprite[17];
        card_sprite_info[7] = my_sprite[18];


        for (int i=0; i < sr_array.Length; i++)
        {
            sr_array[i] = card[i].GetComponent<SpriteRenderer>();

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //card[0].transform.position = Vector3.MoveTowards(card[0].transform.position, target[0].transform.position, 5 * Time.deltaTime);

    }

    public void GameStart()
    {   
        //todo.. ·£´ý ¸¸µé±â, ·£´ý °ª³Ö±â
        //¿©±â¼­ startCorutine ÇÔ.
        if (isstart)
        {
            isstart = false;
            for (int i = 0; i < card.Length; i++)
            {
                StartCoroutine(MoveCard(card[i], target[i]));

                if (i == 6 || i == 7)
                {
                    card[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                if (i == 2 || i == 3)
                {
                    card[i].transform.Rotate(Vector3.back * 90);
                }

                if (i == 4 || i == 5)
                {
                    card[i].transform.Rotate(Vector3.back * 180);
                }

            }

        }


    }



    IEnumerator MoveCard(GameObject my_card, GameObject my_target)
    {
        while (timer<15)
        {
            timer = timer + Time.deltaTime;
            
            my_card.transform.position=Vector3.MoveTowards(my_card.transform.position, my_target.transform.position, 5 * Time.deltaTime);

            yield return null;

        }
        isopencard = true;

    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }

    //Ä«µå¸¦ ÇÑÀå¾¿ º¸¿©ÁØ´Ù.
    public void OpenCard()
    {
        if (isopencard)
        {
            sr_array[0].sprite = card_sprite_info[0];
            sr_array[1].sprite = card_sprite_info[1];

            sr_array[3].sprite = card_sprite_info[3];
            sr_array[5].sprite = card_sprite_info[5];
            sr_array[7].sprite = card_sprite_info[7];

            player0.Add(sr_array[0].sprite.name);
            player0.Add(sr_array[1].sprite.name);

            player0.Sort();
            string player0_st = player0[0] + player0[1];

            player_card_lst.Add(player0_st);

            string player0_card_name = "";


            if (jokbo_string.ContainsKey(player_card_lst[0]))
            {
                player0_card_name = jokbo_string[player_card_lst[0]];
            }
            else
            {
                player0_card_name = not_in_jokbo_string[(not_in_jokbo[player_card_lst[0][0].ToString()] + not_in_jokbo[player_card_lst[0][1].ToString()]) % 10];
            }
            info_text.text = "´ç½ÅÀÇ Ä«µå´Â " + player0_card_name + "ÀÔ´Ï´Ù";
            info_text2.text = "¹èÆÃÇÏ½Ã°Ú½À´Ï±î?";

            isbatting = true;

            isopencard = false;

        }
        




    }

    public void Batting()
    {

        if (isbatting)
        {
            isbatting = false;
            for (int i = 2; i < sr_array.Length; i++)
            {
                sr_array[i].sprite = card_sprite_info[i];
            }

            computer1.Add(sr_array[2].sprite.name);
            computer1.Add(sr_array[3].sprite.name);

            computer2.Add(sr_array[4].sprite.name);
            computer2.Add(sr_array[5].sprite.name);

            computer3.Add(sr_array[6].sprite.name);
            computer3.Add(sr_array[7].sprite.name);

            computer1.Sort();
            computer2.Sort();
            computer3.Sort();

            Debug.Log(player_card_lst.Count);
            player_card_lst.Add(computer1[0] + computer1[1]);
            player_card_lst.Add(computer2[0] + computer2[1]);
            player_card_lst.Add(computer3[0] + computer3[1]);

            for (int i = 0; i < player_card_lst.Count; i++)
            {
                if (jokbo.ContainsKey(player_card_lst[i]))
                {
                    player_value_lst.Add(jokbo[player_card_lst[i]]);
                }
                else
                {
                    player_value_lst.Add((not_in_jokbo[player_card_lst[i][0].ToString()] + not_in_jokbo[player_card_lst[i][1].ToString()]) % 10);
                }

            }
            Debug.Log("¤Ñ¤Ñ¤ÑÄ«µå°ª ºÎ¿©¤Ñ¤Ñ¤Ñ");
            for (int i = 0; i < player_value_lst.Count; i++)
            {
                Debug.Log("player" + i + " ÀÇ Ä«µå°ªÀº" + player_value_lst[i]);
            }

            //¶¯ÀâÀÌÈ®ÀÎ
            bool isDDang = false;
            for (int i = 0; i < ddang_list.Count; i++)
            {

                if (player_card_lst.Contains(ddang_list[i]))
                {
                    //Console.WriteLine("¶¯ÀÌ Á¸ÀçÇÕ´Ï´Ù.");
                    isDDang = true;
                    break;
                }
                else
                {
                    //Console.WriteLine("¶¯ÀÌ Á¸ÀçÇÏÁö ¾Ê½À´Ï´Ù.");
                }
            }

            if (isDDang)
            {
                for (int i = 0; i < player_card_lst.Count; i++)
                {
                    if (ddangkiller_list.Contains(player_card_lst[i]))
                    {
                        Debug.Log(i + " ¹øÂ° DDangÀâÀÌÀÇ °ªÀ» º¯°æÇÕ´Ï´Ù.");
                        player_value_lst[i] = 500;
                    }
                    else
                    {
                        //Console.WriteLine("º¯°æ°ªÀÌ ¾ø½À¤¤µð¤¿..");
                    }

                }

            }

            //±¤ÀâÀÌ È®ÀÎ
            bool isGyangDDang = false;

            for (int i = 0; i < gyangddang_list.Count; i++)
            {
                if (player_card_lst.Contains(gyangddang_list[i]))
                {

                    isGyangDDang = true;
                    //Console.WriteLine("±¤¶¯ÀÌ Á¸ÀçÇÕ´Ï´Ù.");
                    break;

                }
                else
                {
                    //Console.WriteLine("±¤¶¯ÀÌ Á¸ÀçÇÏÁö ¾Ê½À´Ï´Ù..");
                }

            }

            if (isGyangDDang)
            {
                for (int i = 0; i < player_card_lst.Count; i++)
                {
                    if (player_card_lst[i] == "dg")
                    {
                        player_value_lst[i] = 1800;
                    }

                }

            }
            Debug.Log("¤Ñ¤Ñ¤ÑÄ«µå°ª ÀçºÎ¿© ÈÄ È®ÀÎ¤Ñ¤Ñ¤Ñ");
            for (int i = 0; i < player_value_lst.Count; i++)
            {
                Debug.Log("player" + i + " ÀÇ Ä«µå°ªÀº" + player_value_lst[i]);
            }

            int max_index = 0;
            int max = 0;

            for(int i =0; i < player_value_lst.Count; i++)
            {
                if (max < player_value_lst[i])
                {
                    max = player_value_lst[i];
                    max_index = i;
                }
            }

            info_text.text = "player" + max_index + "°¡ ¿ì½ÂÇß½À´Ï´Ù!";
            info_text2.text = "";

        }
    }


    /// <summary>
    /// Á·º¸µé
    /// </summary>
    Dictionary<string, int> jokbo = new Dictionary<string, int>()
            {

                {"aA",200 }, {"bB", 220 }, {"cC", 240 }, {"dD", 260 }, {"eE", 280 }, {"fF", 300 },
                {"gG", 320 }, {"hH", 340 }, {"iI", 360 }, {"jJ", 380 },

                {"df", 40 }, {"dF", 40 }, {"Df", 40 }, {"DF", 40 }, //¼¼·ú
                {"ai", 50 }, {"aI", 50 }, {"Ai", 50 }, {"AI", 50 }, //±¸»æ
                {"dj", 60 }, {"dJ", 60 }, {"Dj", 60 }, {"DJ", 60 }, //Àå»ç
                {"ad", 70 }, {"aD", 70 }, {"Ad", 70 }, {"AD", 70 }, //µ¶»ç
                {"aj", 80 }, {"aJ", 80 }, {"Aj", 80 }, {"AJ", 80 }, //Àå»æ
                {"ab", 90 }, {"aB", 90 }, {"Ab", 90 }, {"AB", 90 }, //¾Ë¸®

                {"AC", 1300 }, {"AH", 1300 }, {"CH", 3800 } //±¤¶¯

            };

    Dictionary<string, int> not_in_jokbo = new Dictionary<string, int>()
            {
                {"a", 1 }, {"b", 2 }, {"c", 3 }, {"d", 4 }, {"e", 5 }, {"f", 6 }
                , {"g", 7 }, {"h", 8 }, {"i", 9 }, {"j", 10 },
                {"A", 1 }, {"B", 2 }, {"C", 3 }, {"D", 4 }, {"E", 5 }, {"F", 6 }
                , {"G", 7 }, {"H", 8 }, {"I", 9 }, {"J", 10 }
            };

    Dictionary<string, string> jokbo_string = new Dictionary<string, string>()  
    {

            {"aA","ÀÏ¶¯" }, {"bB", "ÀÌ¶¯" }, {"cC", "»ï¶¯" }, {"dD", "»ç¶¯" }, {"eE", "¿À¶¯" }, {"fF", "À°¶¯" },
            {"gG", "Ä¥¶¯" }, {"hH", "ÆÈ¶¯" }, {"iI", "±¸¶¯" }, {"jJ", "½Ê¶¯" },

            {"df", "¼¼·ú" }, {"dF", "¼¼·ú" }, {"Df", "¼¼·ú" }, {"DF", "¼¼·ú" }, //¼¼·ú
            {"ai", "±¸»æ" }, {"aI", "±¸»æ" }, {"Ai", "±¸»æ" }, {"AI", "±¸»æ" }, //±¸»æ
            {"dj", "Àå»ç" }, {"dJ", "Àå»ç" }, {"Dj", "Àå»ç" }, {"DJ", "Àå»ç" }, //Àå»ç
            {"ad", "µ¶»ç" }, {"aD", "µ¶»ç" }, {"Ad", "µ¶»ç" }, {"AD", "µ¶»ç" }, //µ¶»ç
            {"aj", "Àå»æ" }, {"aJ", "Àå»æ" }, {"Aj", "Àå»æ" }, {"AJ", "Àå»æ" }, //Àå»æ
            {"ab", "¾Ë¸®" }, {"aB", "¾Ë¸®" }, {"Ab", "¾Ë¸®" }, {"AB", "¾Ë¸®" }, //¾Ë¸®

            {"AC", "ÀÏ»ï±¤¶¯" }, {"AH", "ÀÏÆÈ±¤¶¯" }, {"CH", "»ïÆÈ±¤¶¯" }, //±¤¶¯

            {"cg", "¶¯ÀâÀÌ or ¸ÁÅë" }, {"Cg", "¶¯ÀâÀÌ or ¸ÁÅë" }, {"cG", "¶¯ÀâÀÌ or ¸ÁÅë" },
            {"CG", "¶¯ÀâÀÌ or ¸ÁÅë" },

            {"DG", "¾ÏÇà¾î»ç or ÀÏ²ý" }


    };

    Dictionary<int, string> not_in_jokbo_string = new Dictionary<int, string>()
    {
        {0, "¸ÁÅë"},
        {1, "ÀÏ²ý"}, {2, "µÎ²ý"}, {3, "¼¼²ý"}, {4, "³×²ý"}, {5, "´Ù¼¸²ý"}, {6, "¿©¼¸²ý"}, {7, "ÀÏ°ö²ý"},
        {8, "¿©´ü²ý"}, {9, "°©¿À"}


    };

    List<string> ddang_list = new List<string>()
            {
                "aA", "bB", "cC", "dD", "eE", "fF", "gG", "hH", "iI", "jJ"
            };

    List<string> ddangkiller_list = new List<string>()
            {
                "cg", "Cg", "cG", "CG"
            };

    List<string> gyangddang_list = new List<string>()
            {
                "AC", "AH"
            };




}
