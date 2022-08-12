using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;
using static MetaInfo;


public class ShopPanel : MonoBehaviour
{
    
    [SerializeField] private Text textLimfo, textGranulo, textEritro;
    [SerializeField] private Text textMoneyBase;

    private static ShopPanel instance = null;
    public static ShopPanel Instance
    {
        get
        {
            if (instance == null)
            {
                // instance = new ShopPanel();
                // return instance;
                throw new System.Exception("zxc");
            }
            else
                return instance;
        }
    }


    private int money;
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();

    public int Money
    {
        set { money = value; }
    }

    public Dictionary<EntityType, int> Prices
    {
        set { prices = value; }
    }
    
    public void IncreaseMoney()
    {
        ++money;
        textMoneyBase.text = $"{money}";
    }
    public void BuyUnit(string str)
    {
        int price = 0;
        EntityType entityType = (EntityType)System.Enum.Parse(typeof(EntityType), str);

        //method returns price by reference
        prices.TryGetValue(entityType, out price);
        if (money >= price)
        {
            money -= price;
            textMoneyBase.text = $"{money}";

            ActionTimer actionTimer = new GameObject(entityType.ToString() + "Timer").AddComponent<ActionTimer>();
            actionTimer.Timer = sceneManager.TimeToSpawn[entityType];
            actionTimer.SomeAction = (() => sceneManager.SpawnEntity(entityType));
            actionTimer.SomeAction = (() => Destroy(actionTimer.gameObject));
            actionTimer.SomeAction = (() => SoundManager.Instance.PlaySound(SoundManager.SoundType.UnitSpawn));
        }

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        textMoneyBase.text = $"{money}";
        
        //add price for units to screen
        int price;
        //method returns price by reference
        prices.TryGetValue(EntityType.Erythrocyte, out price); textEritro.text = $"{price}";
        prices.TryGetValue(EntityType.Granulocyte, out price); textGranulo.text = $"{price}";
        prices.TryGetValue(EntityType.Lymphocyte, out price); textLimfo.text = $"{price}"; 
    }


    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         Debug.Log("shop panel created");
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else if (instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
