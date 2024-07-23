using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CoinEffects;
using CoinEffects.UI;
using System;
using CoinEffects.Helpers;
using System.Configuration;
using CoinEffects.Configs;

namespace CoinEffects 
{
    public class GlobalPlayer : ModPlayer
    {
        private float _coinDamageMultiplier;

        public override void UpdateEquips()
        {
            base.UpdateEquips();

            this.UpdateCoinStore();

            this._coinDamageMultiplier = CEMathHelper.GetCoinDamageMulti();

            Player.GetDamage(DamageClass.Generic) *= _coinDamageMultiplier;
        }

        public string CoinDamagePercentage => $"{(this._coinDamageMultiplier * 100).ToString("0.00")}%";

        private void UpdateCoinStore() 
        {
            bool overFlowing;
            CoinsHelper.InInventory = Utils.CoinsCount(out overFlowing, Player.inventory, 58, 57, 56, 55, 54);
            CoinsHelper.InBank1 = Utils.CoinsCount(out overFlowing, Player.bank.item);
            CoinsHelper.InBank2 = Utils.CoinsCount(out overFlowing, Player.bank2.item);
            CoinsHelper.InBank3 = Utils.CoinsCount(out overFlowing, Player.bank3.item);
            CoinsHelper.InBank4 = Utils.CoinsCount(out overFlowing, Player.bank4.item);
        }
    }
}