using Newtonsoft.Json;
using UnityEngine;

public class MetaInfo
{
    private const string configPath = "meta_info";
    private static MetaInfo instance;
    private readonly Data data;
    public static MetaInfo Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MetaInfo();
            }
            return instance;
        }
    }

    public string HowToPlayText { get { return data.howToPlayText; } }

    public string GetEntityInfo(EntityType entityType)
    {
        switch (entityType)
        {
            case EntityType.Erythrocyte: return data.erythrocyteInfoText;
            case EntityType.Granulocyte: return data.granulocyteInfoText;
            case EntityType.Lymphocyte: return data.lymphocyteInfoText;
        }

        return "";
    }
    private MetaInfo()
    {
        // Loading meta from resource
        string json = (Resources.Load<TextAsset>(configPath)).ToString();
        data = JsonConvert.DeserializeObject<Data>(json);

    }

    private class Data
    {
        public string howToPlayText;
        public string erythrocyteInfoText;
        public string granulocyteInfoText;
        public string lymphocyteInfoText;
    }
}
