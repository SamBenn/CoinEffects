using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;
using CoinEffects.Constants;
using System.Windows.Markup;
using CoinEffects.Helpers;

namespace CoinEffects.UI 
{
    [Autoload(Side = ModSide.Client)]
    public class CoinEffectsUISystem : ModSystem
    {
        internal CoinEffectsUIState CoinDamageUIState;
        private UserInterface _coinEffectsUI;

        private Vector2 _panelPos;

        public override void Load()
        {
            this.CoinDamageUIState = new CoinEffectsUIState();
            this.CoinDamageUIState.SetPos(this._panelPos);
            this.CoinDamageUIState.Activate();
            this._coinEffectsUI = new UserInterface();
            this._coinEffectsUI.SetState(this.CoinDamageUIState);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            base.LoadWorldData(tag);

            var x = 400f;
            var y = 100f;

            if(tag.ContainsKey(DataKeys.UI.PanelX))
                x = tag.GetFloat(DataKeys.UI.PanelX);

            if(tag.ContainsKey(DataKeys.UI.PanelY))
                y = tag.GetFloat(DataKeys.UI.PanelY);

            this._panelPos = new Vector2(x, y);

            if(this.CoinDamageUIState != null)
                this.CoinDamageUIState.SetPos(this._panelPos);
        }

        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);

            if(this.CoinDamageUIState == null)
                return;

            this._panelPos = this.CoinDamageUIState.GetPos();

            tag.Set(DataKeys.UI.PanelX, _panelPos.X, replace: true);
            tag.Set(DataKeys.UI.PanelY, _panelPos.Y, replace: true);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            this._coinEffectsUI?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Coin Effects: Makes coins do things",
                    delegate
                    {
                        this._coinEffectsUI.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}