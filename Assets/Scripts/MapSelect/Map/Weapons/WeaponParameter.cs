using UnityEngine;

public class WeaponParameter : MonoBehaviour
{
    static public WeaponParameter instance;

    private void Awake()
    {
        instance = this;
    }

    public WeaponParn GetWeaponParameter(Weapon Weapon)
    {
        WeaponParn GetParameter = Weapon.GetParameter();
        WeaponParn InitWeaponParn = new WeaponParn
        {
            damage = GetParameter.damage,
            range = GetParameter.range,
            atk_spd = GetParameter.atk_spd,
            atk_count = GetParameter.atk_count,
            atk_time = GetParameter.atk_time,
            cooldown = GetParameter.cooldown
        };

        InitWeaponParn = UpdateWeaponParameter(Weapon, InitWeaponParn);
        return UpdateWeaponParameterFromItem(InitWeaponParn);
    }

    private WeaponParn UpdateWeaponParameterFromItem(WeaponParn UpdateWeaponParn)
    {
        // 武器のダメージ
        int ItemDamageUp = ItemStatus.instance.GetAllStatusPhase(1);
        UpdateWeaponParn.damage += UpdateWeaponParn.damage * ItemDamageUp * 0.1f;

        // 武器の大きさ
        int ItemRange = ItemStatus.instance.GetAllStatusPhase(6);
        UpdateWeaponParn.range += UpdateWeaponParn.range * ItemRange * 0.1f;

        // 武器の攻撃速度
        int ItemAttackSpeed = ItemStatus.instance.GetAllStatusPhase(7);
        UpdateWeaponParn.atk_spd -= UpdateWeaponParn.atk_spd * ItemAttackSpeed * 0.1f;

        // 武器の攻撃回数
        int ItemAttackCount = ItemStatus.instance.GetAllStatusPhase(9);
        UpdateWeaponParn.atk_count += ItemAttackCount;

        // 武器の持続時間
        int ItemAttackTime = ItemStatus.instance.GetAllStatusPhase(8);
        UpdateWeaponParn.atk_time += UpdateWeaponParn.atk_time * ItemAttackTime * 0.1f;

        // 武器のクールダウン
        int ItemCoolTime = ItemStatus.instance.GetAllStatusPhase(5);
        UpdateWeaponParn.cooldown -= UpdateWeaponParn.cooldown * ItemCoolTime * 0.08f;

        UpdateWeaponParn.AddSpeed = (1f + 1f - UpdateWeaponParn.atk_spd);
        return UpdateWeaponParn;
    }

    private WeaponParn UpdateWeaponParameter(Weapon Weapon, WeaponParn DefaultWeaponParn)
    {
        int WeaponID = Weapon.GetId();
        int WeaponLv = WeaponStatus.instance.GetStatusPhase(WeaponID);

        if (WeaponID == 1) return UpdateWeaponID1(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 3) return UpdateWeaponID3(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 5) return UpdateWeaponID5(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 7) return UpdateWeaponID7(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 9) return UpdateWeaponID9(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 11) return UpdateWeaponID11(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 13) return UpdateWeaponID13(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 15) return UpdateWeaponID15(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 17) return UpdateWeaponID17(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 19) return UpdateWeaponID19(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 21) return UpdateWeaponID21(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 23) return UpdateWeaponID23(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 25 || WeaponID == 26) return UpdateWeaponID25_26(WeaponLv, DefaultWeaponParn);
        else if (WeaponID == 27) return UpdateWeaponID27(WeaponLv, DefaultWeaponParn);
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID1(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.damage += 5;
            DefaultWeaponParn.range += 0.1f;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.damage += 5;
            DefaultWeaponParn.range += 0.1f;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.damage += 5;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID3(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.cooldown -= 0.2f;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.penetrate += 1;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.damage += 10;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID5(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.penetrate += 1;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.penetrate += 1;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID7(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.damage += 20;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.penetrate += 2;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.damage += 20;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.penetrate += 2;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.damage += 20;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID9(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.range += 0.1f;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.25f;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.range += 0.1f;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.25f;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.damage += 10;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID11(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.range += 0.25f;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.3f;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.damage += 10;
            DefaultWeaponParn.atk_time += 0.5f;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.range += 0.25f;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.3f;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.damage += 10;
            DefaultWeaponParn.atk_time += 0.5f;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID13(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.damage += 10;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.2f;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.damage += 10;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.2f;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.damage += 10;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.2f;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.damage += 10;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID15(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.range += 0.4f;
            DefaultWeaponParn.damage += 2;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.cooldown -= 0.1f;
            DefaultWeaponParn.damage += 1;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.damage += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.cooldown -= 0.1f;
            DefaultWeaponParn.damage += 2;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.damage += 1;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.cooldown -= 0.1f;
            DefaultWeaponParn.damage += 2;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.damage += 1;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID17(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.atk_time += 0.5f;
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.atk_time += 0.3f;
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.atk_time += 0.3f;
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.range += 0.2f;
            DefaultWeaponParn.damage += 5;
        }
        return DefaultWeaponParn;
    }
    
    private WeaponParn UpdateWeaponID19(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.damage += 5;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.2f;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.atk_time += 0.3f;
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.damage += 5;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.2f;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.atk_time += 0.3f;
            DefaultWeaponParn.damage += 5;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.atk_time += 0.5f;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID21(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.range += 1f;
            DefaultWeaponParn.damage += 10;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.range += 1f;
            DefaultWeaponParn.damage += 20;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.range += 1f;
            DefaultWeaponParn.damage += 20;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.atk_count += 1;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID23(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.cooldown -= 10;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.DropItem = 25;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.cooldown -= 10;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.DropItem = 45;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.cooldown -= 5;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.DropItem = 65;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.cooldown -= 5;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID25_26(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.range += 0.4f;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.damage = 10;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.cooldown -= 0.3f;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.range += 0.4f;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.damage = 10;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.cooldown -= 0.3f;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.range += 0.4f;
        }
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID27(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
        {
            DefaultWeaponParn.range += 0.2f;
        }
        if (WeaponLv >= 3)
        {
            DefaultWeaponParn.cooldown -= 0.3f;
        }
        if (WeaponLv >= 4)
        {
            DefaultWeaponParn.range += 0.2f;
        }
        if (WeaponLv >= 5)
        {
            DefaultWeaponParn.cooldown -= 0.3f;
        }
        if (WeaponLv >= 6)
        {
            DefaultWeaponParn.range += 0.2f;
        }
        if (WeaponLv >= 7)
        {
            DefaultWeaponParn.cooldown -= 0.3f;
        }
        if (WeaponLv >= 8)
        {
            DefaultWeaponParn.range += 0.2f;
        }
        return DefaultWeaponParn;
    }
}
