    /*Set Method*/
    private static void set_true_or_false(string boolean_stirng_key, bool true_or_false)
    {
        if (!true_or_false)
        {
            PlayerPrefs.SetInt(boolean_stirng_key, 0);
        }
        if (true_or_false)
        {
            PlayerPrefs.SetInt(boolean_stirng_key, 1);
        }
    }
    /*Get Method*/
    private static bool get_true_or_false(string boolean_stirng_key)
    {
        if (PlayerPrefs.GetInt(boolean_stirng_key) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
