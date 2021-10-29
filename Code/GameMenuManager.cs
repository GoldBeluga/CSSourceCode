using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    /*Main Code*/

    /*Main variable*/
    [SerializeField] private GameObject main;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text gemText;
    [SerializeField] private Text welcomeText;
    [SerializeField] private Text confirmQuit;
    [SerializeField] private Button quitYesButton;
    [SerializeField] private Button quitNoButton;
    [HideInInspector] public int Money;
    [HideInInspector] public int Gem;
    public GameObject welcomePanel;
    private const string gemStringKey = "gem";
    private const string moneyStirngKey = "money";
    private const string isDoneStringKey = "isDone";
    private const string welcomeStringKey = "welcome";
    private bool quitButtonClicked = true;
    private Shop shop;
    private int? welcomeInt;
    void Start()
    {
        welcomeInt = PlayerPrefs.GetInt(welcomeStringKey);
        if ((welcomeInt ?? 0) == 0)
        {
            welcomePanel.SetActive(true);
        }
        else if (welcomeInt == 1)
        {
            main.SetActive(true);
            welcomePanel.SetActive(false);
        }
        bool quitButtonClicked = false;
        this.quitButtonClicked = quitButtonClicked;
        confirmQuit.gameObject.SetActive(false);
        quitNoButton.gameObject.SetActive(false);
        quitYesButton.gameObject.SetActive(false);
        int isDone = PlayerPrefs.GetInt(isDoneStringKey);
        int money = PlayerPrefs.GetInt(moneyStirngKey);
        int gem = PlayerPrefs.GetInt(gemStringKey);
        int shopMoney = PlayerPrefs.GetInt(moneyStirngKey);
        int shopGem = PlayerPrefs.GetInt(gemStringKey);
        this.Gem = shopGem;
        this.Money = shopMoney;
        Debug.Log("shop" + shopMoney + "" + shopGem);
        moneyText.text = "Gold :" + this.Money.ToString();
        gemText.text = "Gem : " + this.Gem.ToString();
        switch (isDone)
        {
            case 0:
                gem = 10;
                money = 100;
                isDone = 1;
                PlayerPrefs.SetInt(isDoneStringKey, isDone);
                PlayerPrefs.SetInt(gemStringKey, gem);
                PlayerPrefs.SetInt(moneyStirngKey, money);
                this.Gem = gem;
                this.Money = money;
                moneyText.text = "Gold :" + this.Money.ToString();
                gemText.text = "Gem : " + this.Gem.ToString();
                welcomeText.text = "Welcome to our game, thank you for playing this game, we'll give you 10 gems and 100 gold.";
                break;

            case 1:
                moneyText.text = "Gold :" + this.Money.ToString();
                gemText.text = "Gem : " + this.Gem.ToString();
                welcomeText.text = "Back to game, good luck";
                break;
        }
    }

    void Update()
    {
        moneyText.text = "Gold :" + this.Money.ToString();
        gemText.text = "Gem : " + this.Gem.ToString();
    }

    public void quitGameNo()
    {
        quitButtonClicked = false;
        confirmQuit.gameObject.SetActive(false);
        quitNoButton.gameObject.SetActive(false);
        quitYesButton.gameObject.SetActive(false);
    }

    public void quitGameYes()
    {
        PlayerPrefs.SetInt(gemStringKey, Money);
        PlayerPrefs.SetInt(moneyStirngKey, Gem);
        PlayerPrefs.SetInt(welcomeStringKey, 0);
        Application.Quit();
    }


    public void quitGameMethod()
    {
        quitButtonClicked = true;
        confirmQuit.gameObject.SetActive(true);
        quitYesButton.gameObject.SetActive(true);
        quitNoButton.gameObject.SetActive(true);
    }

    public void welcomeOK()
    {
        welcomePanel.SetActive(false);
        main.SetActive(true);
    }

    public void goToShop()
    {
        if (!quitButtonClicked)
        {
            PlayerPrefs.SetInt(welcomeStringKey, 1);
            SceneManager.LoadScene(2);
        }
    }

}
