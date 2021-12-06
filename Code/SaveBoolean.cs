    /*Set*/
    private static void set_boolean(string key, bool value)
    {      
            PlayerPrefs.SetInt(key, value ? 1 : 0);
    }
    /*Get*/
    private static bool get_boolean(string key) => PlayerPrefs.GetInt(key) == 1;
