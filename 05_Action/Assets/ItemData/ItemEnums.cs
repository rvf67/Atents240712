public enum ItemCode : byte
{
    Misc = 0,
    Ruby,
    Emerald,
    Sapphire, 
    CopperCoin, 
    SilverCoin,
    GoldCoin,
    Apple,
    Bread,
    Beer,
    Water,
    HealingPotion,
    ManaPotion,
    IronSword,
    SilverSword,
    OldSword,
    KiteShield,
    RoundShield,
}

public enum ItemSortCriteria : byte
{
    Code,   // 코드 기준
    Name,   // 이름 기준
    Price   // 가격 기준
}

public enum EquipType : byte
{
    Weapon,
    Shield,
}