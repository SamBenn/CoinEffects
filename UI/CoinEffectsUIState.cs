using System.Linq;
using CoinEffects.Extensions;
using CoinEffects.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;

namespace CoinEffects.UI
{
    public class CoinEffectsUIState : UIState
    {
        private CoinEffectsPanel _panel;
        private UIText _text;
        private float _resizeTimeout = 6f;

        private GlobalPlayer _player;
        private float _playerGetTimeout = 6f;

        private readonly float TIMEOUT_MAX = 5f;

        private Vector2 _panelPos;

        public override void OnInitialize()
        {
            _panel = new CoinEffectsPanel();

            _panel.SetPadding(0);
            // need a callback for pos change to save in this class https://github.com/tModLoader/tModLoader/wiki/Saving-and-loading-using-TagCompound
            _panel.SetRectangle(400f, 100f, 170f, 70f);
            _panel.BackgroundColor = Color.Transparent;
            _panel.BorderColor = Color.Transparent;

            _text = new UIText("Damage: 100%");
            _text.Top.Set(5f, 0f);
            _text.Left.Set(5f, 0f);
            _panel.Append(_text);

            Append(_panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.GetPlayer(gameTime);
            this.Resize(gameTime);

            this._text.SetText($"Damage: {this._player.CoinDamagePercentage}");
        }

        public void SetPos(Vector2 vector) => this._panel.SetPos(vector);
        public Vector2 GetPos() => new Vector2(this._panel.Left.Pixels, this._panel.Top.Pixels);

        private void GetPlayer(GameTime gameTime) 
        {
            if(_playerGetTimeout >= TIMEOUT_MAX)
            {
                this._player = Main.LocalPlayer.GetModPlayer<GlobalPlayer>();
                this._playerGetTimeout = 0f;
                return;
            }

            this._playerGetTimeout += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void Resize(GameTime gameTime)
        {
            if(this._resizeTimeout >= TIMEOUT_MAX)
            {
                var textDimensions = this._text.GetDimensions();

                this._panel.SetSize(textDimensions.Width + 20f, textDimensions.Height + 10f);
                this._resizeTimeout = 0f;

                return;
            }

            this._resizeTimeout += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}