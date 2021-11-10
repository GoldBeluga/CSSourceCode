using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    void not_enough(Text not_enough_text, int hide_not_enough_time)
    {
        StartCoroutine(wait(hide_not_enough_time, not_enough_text));
    }

    IEnumerator wait(int sec, Text hide_not_enough_time)
    {
        not_enough_text.gameObject.SetActive(true);
        yield return new WaitForSeconds(sec);
        hide_not_enough_time.gameObject.SetActive(false);
    }

    public void player_shop_calculate(ref float player_property, float player_get_value, float player_give_value, float player_minimum_value, Text not_enough_text, int hide_not_enough_time)
    {
        not_enough_text.gameObject.SetActive(false);
        if (player_property >= player_minimum_value)
        {
            player_property -= player_give_value;
            player_property += player_get_value;
        }
        else if (player_property < player_minimum_value)
        {
            not_enough(not_enough_text, hide_not_enough_time);
        }
    }

    public void player_shop_calculate(ref float player_property, ref float player_second_property, float player_get_value, float player_give_value, float player_minimum_value, Text not_enough_text, int hide_not_enough_time)
    {
        not_enough_text.gameObject.SetActive(false);
        if (player_property >= player_minimum_value)
        {
            player_property -= player_give_value;
            player_second_property += player_get_value;
        }
        else if (player_property < player_minimum_value)
        {
            not_enough(not_enough_text, hide_not_enough_time);
        }

    }
}

