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
    [SerializeField] private GameObject buttonLimfo, buttonEritro, buttonGranulo;


    private static ShopPanel instance = null;
    public static ShopPanel Instance
    {
        get
        {
            if (instance == null)
            {
                throw new System.Exception("ShopPanel doesnt exist");
            }
            else
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
            if (entry.Value < money)
            {
                ChangeButtonTransparency(entry.Key, 1.0f);
            }
            else
            {
                ChangeButtonTransparency(entry.Key, 0.5f);
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
