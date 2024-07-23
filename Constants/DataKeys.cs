using System;

namespace CoinEffects.Constants
{
    public static class DataKeys 
    {
        public static string Namespace(string val) => $"CoinEffects.{val}";
        
        public static class UI 
        {
            public static readonly string PanelX = Namespace("PanelX");
            public static readonly string PanelY = Namespace("PanelY");
        }
    }
}