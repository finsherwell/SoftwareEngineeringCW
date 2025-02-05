using UnityEngine;

public class TestScript
{
    public int testInt = 3;

    public int ReturnTestInt()
    {
        return testInt;
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