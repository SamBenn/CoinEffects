using System;
using Terraria;
using CoinEffects.Configs;

namespace CoinEffects 
{
    public static class CEMathHelper 
    {
        public static float GetCoinDamageMulti()
        {
            var config = CoinDamageConfig.Get();

            var cM = config.GetCoinToDamageMultiplier();
            var bM = config.GetBaseMultiplier();
            var drM = config.GetDRMultiplier();

            return CEMathHelper.ApplyDiminishingReturns(CoinsHelper.GetCoins(config) * cM, drM, 1) + bM;
        }

        public static float ApplyDiminishingReturns(float val, float drMulti, float threshold)
        {
            float baseValue = val;
            float effectiveValue = 0;
            float decrementFactor = drMulti;

            if(decrementFactor == 1f)
                return val;

            while (baseValue > 0)
            {
                float reductionThreshold = Math.Min(baseValue, threshold);
                effectiveValue += reductionThreshold * decrementFactor;
                baseValue -= reductionThreshold;
                decrementFactor *= drMulti;
            }

            return effectiveValue;
        }
    }
}