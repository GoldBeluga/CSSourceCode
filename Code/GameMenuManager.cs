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
    private bool quitButtonClicked = true;
    void Start()
    {
        main.SetActive(false);
        welcomePanel.SetActive(true);
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
        Debug.Log(money + "" + gem);
        this.Gem = shopGem;
        this.Money = shopMoney;
        Debug.Log("Shop" + shopMoney + "" + shopGem);
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
            SceneManager.LoadScene(2);
        }
    }

}
