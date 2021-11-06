    /*Set*/
    private static void set_boolean(string key, bool value)
    {
        if (!value)
        {
            PlayerPrefs.SetInt(key, 0);
        }
        if (value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
    }
    /*Get*/
    private static bool get_boolean(string key) => PlayerPrefs.GetInt(key) == 1;
