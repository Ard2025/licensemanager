namespace Test;
using LicenseManager;

public class Tests
{
    public static string TestLicenseKey = "ABCD-EFGH-IJKL-MNOP-QRST";
    public static string TestConsumerKey = "ck_7a56198bdd7993bb7d28ae4286a702eed155b215";
    public static string TestConsumerSecret = "cs_2064bf4266f6b0367bd3b3a4f5ff48932d2abf0d";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestPredefinedKey()
    {
        var manager = new LicenseManager("http://localhost:80", TestConsumerKey, TestConsumerSecret, TestLicenseKey);
        var isValid = manager.IsValid();
        Assert.That(isValid, Is.EqualTo(true));
    }
    
    [Test]
    public async Task TestLaterDefinedKey()
    {
        var manager = new LicenseManager("http://localhost:80", TestConsumerKey, TestConsumerSecret, String.Empty);
        await manager.setLicenseKey(TestLicenseKey);
        var isValid = manager.IsValid();
        Assert.That(isValid, Is.EqualTo(true));
    }
}