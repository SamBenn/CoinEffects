using CoinEffects.Helpers;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace CoinEffects.Extensions
{
    public static class UIExtensions {
        public static UIElement SetRectangle(this UIElement uiElement, float left, float top, float width, float height)
        {
            if(uiElement == null)
                return null;

			uiElement.SetPos(left, top);
            uiElement.SetSize(width, height);
            
            return uiElement;
        }

        public static UIElement SetPos(this UIElement uiElement, float? left = null, float? top = null)
        {
            if(uiElement == null)
                return null;

            if(left != null && left.HasValue)
    			uiElement.Left.Set(left.Value, 0f);
            
            if(top != null && top.HasValue)
			    uiElement.Top.Set(top.Value, 0f);

            return uiElement;
        }

        public static UIElement SetPos(this UIElement uiElement, Vector2 pos) 
        {
            if(uiElement == null)
                return null;

            return uiElement.SetPos(pos.X, pos.Y);
        }

        public static UIElement SetSize(this UIElement uiElement, float? width = null, float? height = null)
        {
            if(uiElement == null)
                return null;

            if(width != null && width.HasValue)
			    uiElement.Width.Set(width.Value, 0f);

            if(height != null && height.HasValue)
			    uiElement.Height.Set(height.Value, 0f);

            return uiElement;
        }
    }
}