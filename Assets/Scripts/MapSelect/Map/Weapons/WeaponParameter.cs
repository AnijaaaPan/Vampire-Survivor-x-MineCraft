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
            cooldown = GetParameter.cooldown,
            penetrate = GetParameter.penetrate
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
        UpdateWeaponParn.range += ItemRange * 0.1f;

        // 武器の攻撃速度
        int ItemAttackSpeed = ItemStatus.instance.GetAllStatusPhase(7);
        UpdateWeaponParn.atk_spd -= UpdateWeaponParn.atk_spd * ItemAttackSpeed * 0.1f;

        // 武器の攻撃回数
        int ItemAttackCount = ItemStatus.instance.GetAllStatusPhase(9);
        UpdateWeaponParn.atk_count += ItemAttackCount;

        // 武器の持続時間
        int ItemAttackTime = ItemStatus.instance.GetAllStatusPhase(8);
        UpdateWeaponParn.atk_time += ItemAttackTime * 0.1f;

        // 武器のクールダウン
        int ItemCoolTime = ItemStatus.instance.GetAllStatusPhase(5);
        UpdateWeaponParn.cooldown -= UpdateWeaponParn.cooldown * ItemCoolTime * 0.08f;
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
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID1(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 3)
            DefaultWeaponParn.damage += 5;
        if (WeaponLv >= 4)
            DefaultWeaponParn.damage += 5;
            DefaultWeaponParn.range += 0.1f;
        if (WeaponLv >= 5)
            DefaultWeaponParn.damage += 5;
        if (WeaponLv >= 6)
            DefaultWeaponParn.damage += 5;
            DefaultWeaponParn.range += 0.1f;
        if (WeaponLv >= 7)
            DefaultWeaponParn.damage += 5;
        if (WeaponLv >= 8)
            DefaultWeaponParn.damage += 5;
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID3(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 3)
            DefaultWeaponParn.cooldown -= 0.2f;
        if (WeaponLv >= 4)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 5)
            DefaultWeaponParn.damage += 10;
        if (WeaponLv >= 6)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 7)
            DefaultWeaponParn.penetrate += 1;
        if (WeaponLv >= 8)
            DefaultWeaponParn.damage += 10;
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID5(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 3)
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.damage += 5;
        if (WeaponLv >= 4)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 5)
            DefaultWeaponParn.penetrate += 1;
        if (WeaponLv >= 6)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 7)
            DefaultWeaponParn.atk_count += 1;
            DefaultWeaponParn.damage += 5;
        if (WeaponLv >= 8)
            DefaultWeaponParn.penetrate += 1;
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID7(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 3)
            DefaultWeaponParn.damage += 20;
        if (WeaponLv >= 4)
            DefaultWeaponParn.penetrate += 2;
        if (WeaponLv >= 5)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 6)
            DefaultWeaponParn.damage += 20;
        if (WeaponLv >= 7)
            DefaultWeaponParn.penetrate += 2;
        if (WeaponLv >= 8)
            DefaultWeaponParn.damage += 20;
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID9(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
            DefaultWeaponParn.damage += 10;
        if (WeaponLv >= 3)
            DefaultWeaponParn.range += 0.1f;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.25f;
        if (WeaponLv >= 4)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 5)
            DefaultWeaponParn.damage += 10;
        if (WeaponLv >= 6)
            DefaultWeaponParn.range += 0.1f;
            DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.25f;
        if (WeaponLv >= 7)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 8)
            DefaultWeaponParn.damage += 10;
        return DefaultWeaponParn;
    }

    private WeaponParn UpdateWeaponID11(int WeaponLv, WeaponParn DefaultWeaponParn)
    {
        if (WeaponLv >= 2)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 3)
            DefaultWeaponParn.range += 0.25f;
        DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.3f;
        if (WeaponLv >= 4)
            DefaultWeaponParn.damage += 10;
        DefaultWeaponParn.atk_time += 0.5f;
        if (WeaponLv >= 5)
            DefaultWeaponParn.atk_count += 1;
        if (WeaponLv >= 6)
            DefaultWeaponParn.range += 0.25f;
        DefaultWeaponParn.atk_spd -= DefaultWeaponParn.atk_spd * 0.3f;
        if (WeaponLv >= 7)
            DefaultWeaponParn.damage += 10;
        DefaultWeaponParn.atk_time += 0.5f;
        if (WeaponLv >= 8)
            DefaultWeaponParn.atk_count += 1;
        return DefaultWeaponParn;
    }
}
