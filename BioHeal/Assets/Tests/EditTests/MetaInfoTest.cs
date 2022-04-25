using NUnit.Framework;

public class MetaInfoTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MetaInfoTestSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.IsNotEmpty(MetaInfo.Instance.GetEntityInfo(EntityType.Erythrocyte));
        Assert.IsNotEmpty(MetaInfo.Instance.GetEntityInfo(EntityType.Granulocyte));
        Assert.IsNotEmpty(MetaInfo.Instance.GetEntityInfo(EntityType.Lymphocyte));

        Assert.IsEmpty(MetaInfo.Instance.GetEntityInfo(EntityType.Toxin));
    }
}
