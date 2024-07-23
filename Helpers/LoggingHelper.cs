using System;
using System.Collections;
using Terraria;

namespace CoinEffects.Helpers 
{
    public static class LoggingHelper 
    {
        public static void LogError(string message)
        {
            Main.NewText($"CoinEffects - Error: {message}", 250, 150, 150);
        }

        public static void LogWarning(string message)
        {
            Main.NewText($"CoinEffects - Warning: {message}", 250, 250, 150);
        }

        public static void Log(string message)
        {
            Main.NewText($"CoinEffects: {message}");
        }
    }
}