[System.Serializable]
public class Stats 
{
    public float hp = 0;
    public float atk = 0;
    public float def = 0;
    public float speed = 0f;
    public float critRate = 0f; 
    public float critDamage = 0f;
    public float manaBonus =0f;

    public void Clear()
    {
        hp = 0;
        def = 0;
        atk = 0;
        speed = 0;
        critRate = 0;
        critDamage = 0;
        manaBonus = 0;
    }
}
