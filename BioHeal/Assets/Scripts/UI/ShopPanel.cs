using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneManager;
using static MetaInfo;
using static LogFactory;


public class ShopPanel : MonoBehaviour
{
    
    private const float TRANSPARENT = 0.5f;
    private const float NON_TRANSPARENT = 1.0f;
    private static readonly Log log = LogFactory.GetLog(typeof(Base));

    [SerializeField] private Text textLimfo, textGranulo, textEritro;
    [SerializeField] private Text textMoneyBase;
    [SerializeField] private GameObject buttonLimfo, buttonEritro, buttonGranulo;


    private static ShopPanel instance = null;
    public static ShopPanel Instance
    {
         get
        {
            if (instance == null)
            {
                log.Error(new System.Exception("ShopPanel does not exist")); 
            }
            
            return instance;
        }
    }

    private int money;
    private Dictionary<EntityType, int> prices = new Dictionary<EntityType, int>();
    private Dictionary<EntityType, GameObject> buttons  = new Dictionary<EntityType, GameObject>();

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
        
        UpdateButtonsTransparency();
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
            UpdateButtonsTransparency();

            ActionTimer actionTimer = new GameObject(entityType.ToString() + "Timer").AddComponent<ActionTimer>();
            actionTimer.Timer = sceneManager.TimeToSpawn[entityType];
            actionTimer.SomeAction = (() => sceneManager.SpawnEntity(entityType));
            actionTimer.SomeAction = (() => Destroy(actionTimer.gameObject));
            actionTimer.SomeAction = (() => SoundManager.Instance.PlaySound(SoundManager.SoundType.UnitSpawn));
        }
        

        SoundManager.Instance.PlaySound(SoundManager.SoundType.AnyTap);
    }

    private void UpdateButtonsTransparency()
    {
         
        foreach(KeyValuePair<EntityType, int> entry in prices)
        {
            if (entry.Value <= money)
            {
                ChangeButtonTransparency(entry.Key, NON_TRANSPARENT);
            }
            else
            {
                ChangeButtonTransparency(entry.Key, TRANSPARENT);
            }
        } 
    }
    
    public void ChangeButtonTransparency(EntityType buttonsEntity, float value)
    {
        GameObject button;
        buttons.TryGetValue(buttonsEntity, out button);
        var image = button.GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = value;
        image.color = tempColor;
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

        buttons.Add(EntityType.Erythrocyte, buttonEritro);
        buttons.Add(EntityType.Granulocyte, buttonGranulo);
        buttons.Add(EntityType.Lymphocyte, buttonLimfo);
        UpdateButtonsTransparency(); 
    }
}
