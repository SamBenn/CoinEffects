using System.ComponentModel;
using CoinEffects.Helpers;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CoinEffects.Configs
{
	public class CoinDamageConfig : ModConfig, ICoinConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;

		[Header("GoldToDamage")] 
        [DefaultValue("1")] 
		public string GoldToDamagePercent;
        private string _erroredGTDPVal = string.Empty;

        [DefaultValue("100")] 
		public string BasePercent;
        private string _erroredBPVal = string.Empty;

        [DefaultValue("0")] 
		public string DiminishingReturnsPercentage;
        private string _erroredDRPVal = string.Empty;
        
        [DefaultValue(false)]
		public bool Bank1 { get; set; }

        [DefaultValue(false)]
		public bool Bank2 { get; set; }
		
        [DefaultValue(false)]
		public bool Bank3 { get; set; }
		
        [DefaultValue(false)]
		public bool Bank4 { get; set; }

        public float GetCoinToDamageMultiplier()
        {
            float Calc(float val) => val / 1000000f;

            this.GoldToDamagePercent.Replace("%", "");

            if(!string.IsNullOrEmpty(this._erroredGTDPVal) && this._erroredGTDPVal == this.GoldToDamagePercent)
            {
                return Calc(1f);
            }
            this._erroredGTDPVal = string.Empty;

            var success = float.TryParse(this.GoldToDamagePercent, out float num);

            if(!success)
            {
                LoggingHelper.LogError("Coin Damage gold multiplier config value was unparsable, setting to default (1)");
                this._erroredGTDPVal = this.GoldToDamagePercent;
                return Calc(1f);
            }

            return Calc(num);
        }

        public float GetBaseMultiplier()
        {
            float Calc(float val) => val / 100f;

            this.BasePercent.Replace("%", "");

            if(!string.IsNullOrEmpty(this._erroredBPVal) && this._erroredBPVal == this.BasePercent)
            {
                return Calc(100f);
            }
            this._erroredBPVal = string.Empty;

            var success  = float.TryParse(this.BasePercent, out float num);

            if(!success)
            {
                LoggingHelper.LogError("Coin Damage base multiplier config value was unparsable, setting to default (100)");
                this._erroredBPVal = this.BasePercent;
                return Calc(100f);
            }

            return Calc(num);
        }

        public float GetDRMultiplier() 
        {
            float Calc(float val) => 1 - (val / 100);

            this.DiminishingReturnsPercentage.Replace("%", "");

            if(!string.IsNullOrEmpty(this._erroredDRPVal) && this._erroredDRPVal == this.DiminishingReturnsPercentage)
            {
                return Calc(0);
            }
            this._erroredBPVal = string.Empty;

            var success  = float.TryParse(this.DiminishingReturnsPercentage, out float num);

            if(num > 100 || num < 0)
                success = false;

            if(!success)
            {
                LoggingHelper.LogError("Diminishing returns percentage config value was unparsable, setting to default (100)");
                this._erroredDRPVal = this.DiminishingReturnsPercentage;
                return Calc(0);
            }

            return Calc(num);
        }
        
        private static CoinDamageConfig _config;
        public static CoinDamageConfig Get() 
        {
            if(_config == null)
            {
                _config = ModContent.GetInstance<CoinDamageConfig>();
            }

            return _config;
        }
	}
}