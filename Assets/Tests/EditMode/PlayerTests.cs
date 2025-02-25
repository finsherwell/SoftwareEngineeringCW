using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NotStartInJail()
    {
        Player tPlayer = new Player();
        Assert.AreEqual(tPlayer.isInJail(), false);
    }

    [Test]
    public void IdWorks()
    {
        Player tplayer = new Player();
        tplayer.setID(5);
        Assert.AreEqual(tplayer.getID(), 5);
    }


}
