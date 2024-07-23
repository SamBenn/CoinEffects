using CoinEffects.Configs;
using Terraria;

namespace CoinEffects
{
    public static class CoinsHelper
    {
        public static long InInventory;
        public static long InBank1;
        public static long InBank2;
        public static long InBank3;
        public static long InBank4;

        public static long GetCoins(ICoinConfig config, CoinType asType = CoinType.AsCopper)
        {
            var stores = new long[5];
            stores[0] = InInventory;

            if(config.Bank1)
                stores[1] = InBank1;

            if(config.Bank2)
                stores[2] = InBank2;

            if(config.Bank3)
                stores[3] = InBank3;

            if(config.Bank4)
                stores[4] = InBank4;
                
            bool overFlowing;
            var coins = Utils.CoinsCombineStacks(out overFlowing, stores);

            switch(asType)
            {
                case CoinType.AsSilver:
                    return coins / 100;

                case CoinType.AsGold:
                    return coins / 10000;

                case CoinType.AsPlatinum:
                    return coins / 1000000;
                    
                default:
                    return coins;
            }
        }
    }

    public enum CoinType 
    {
        AsCopper,
        AsSilver,
        AsGold,
        AsPlatinum
    }
}