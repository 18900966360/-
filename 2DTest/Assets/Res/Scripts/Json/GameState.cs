using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState{}
public class GunsState : GameState
{
     public List<GunsProperty> PropertyList;
    //  public Dictionary<string,GunsProperty> DG = new Dictionary<string,GunsProperty>();
}
public class GunsProperty
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string GunEffet { get; set; }
    public string Bullet { get; set; }
    public int Atk { get; set; }
}

public class BulletState : GameState
{
    //  public List<BUlletProperty> PropertyList;
      public Dictionary<int,BUlletProperty> DB = new Dictionary<int,BUlletProperty>();
}
public class BUlletProperty
{
    public string Name;
    public string Path;
    public string GunEffet;
    public string Bullet;
    public int Atk;
}
