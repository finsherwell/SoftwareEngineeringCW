using UnityEngine;

public class TestScript
{
    public int testInt = 3;
    public string test = "hello world";

    public int ReturnTestInt()

    {
        return testInt;
    }

    int yo()
    {
        return 6;
    }
}

public class TestScript2 : MonoBehaviour
{
    void Start()
    {
        TestScript test = new TestScript();
        Debug.Log(test.ReturnTestInt());
    }
}