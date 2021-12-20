using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginAndRegister : MonoBehaviour
{
    int account_quantity = 0;
    const string string_key = "A";
    [SerializeField] private Button Create_new_account;
    [SerializeField] private Button Login_account;
    [SerializeField] private Button Delete_all;
    [SerializeField] private InputField new_account_username;
    [SerializeField] private InputField new_account_password;
    [SerializeField] private InputField login_account_username;
    [SerializeField] private InputField login_account_password;

    string[] username;
    string[] password;
    void Start()
    {
        Create_new_account.onClick.AddListener(press);
        account_quantity = PlayerPrefs.GetInt(string_key);
        Delete_all.onClick.AddListener(Delete_all_m);
        Login_account.onClick.AddListener(Login_m);
        Debug.Log(account_quantity);
    }

    void press()
    {
        if (new_account_password.text.Length > 0 && new_account_username.text.Length > 0)
        {
            account_quantity += 1;
            PlayerPrefs.SetInt(string_key, account_quantity);
            Debug.Log(account_quantity);
            username = new string[account_quantity];
            password = new string[account_quantity];
            username[account_quantity - 1] = "Username" + account_quantity;
            password[account_quantity - 1] = "Password" + account_quantity;
            PlayerPrefs.SetString(username[account_quantity - 1], new_account_username.text);
            PlayerPrefs.SetString(password[account_quantity - 1], new_account_password.text);
            Debug.Log("Username : " + PlayerPrefs.GetString(username[account_quantity - 1]));
            Debug.Log("Username : " + PlayerPrefs.GetString(password[account_quantity - 1]));
        }
        else
        {
            Debug.Log("Pls insert some input");
        }
    }

    void Login_m()
    {
        for (int x = 0; x < account_quantity; x++)
        {
            if (login_account_username.text == PlayerPrefs.GetString(username[x]))
            {
                Debug.Log("Username is correct");
                if (login_account_password.text == PlayerPrefs.GetString(password[x]))
                {
                    Debug.Log("Password is correct");
                    break;
                }
                else
                {
                    Debug.Log("password is wrong");
                }
            }
            else if (x == account_quantity - 1)
            {
                Debug.Log("Username is wrong");
            }
        }
    }


    void Delete_all_m()
    {
        PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        username = new string[account_quantity];
        password = new string[account_quantity];
        for (int x = 0; x < account_quantity; x++)
        {
            username[x] = "Username" + (x + 1);
            password[x] = "Password" + (x + 1);
        }
    }

}
