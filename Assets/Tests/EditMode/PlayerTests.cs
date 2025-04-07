using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void NotStartInJail()
    {
        Player tPlayer = new Player();
        Assert.Greater(tPlayer.GetJailTime(), 3);
    }

    [Test]
    public void IdWorks()
    {
        Player tplayer = new Player();
        tplayer.setID(5);
        Assert.AreEqual(tplayer.getID(), 5);
    }
 
    [Test]
    public void CanAddMoney()
    {
        Player tPlayer = new Player();
        tPlayer.addMoney(100);
        Assert.AreEqual(tPlayer.getMoney(), 100);
    }

    [Test]
    public void CanLoseMoney()
    {
        Player tPlayer = new Player();
        tPlayer.addMoney(200);
        tPlayer.takeMoney(100);
        Assert.AreEqual(tPlayer.getMoney(), 100);
    }
    
    [Test]
    public void NeverNegativeMoney()
    {
        Player tPlayer = new Player();
        tPlayer.takeMoney(100);
        Assert.AreEqual(tPlayer.getMoney(), 0);
        //could be changed to check whether the player enters a bankrupt state
    }

    //will add tests for all the tile logic as well 

    /*
    public void StoodOnTile()
    {
        Tile testTile = new Tile();
        Player tPlayer = new Player();
        testTile.GetLandedOn(tPlayer);
        Assert.AreEqual(tPlayer.getCurrentTile().GetID(), testTile.GetID());
    }
    */
}
