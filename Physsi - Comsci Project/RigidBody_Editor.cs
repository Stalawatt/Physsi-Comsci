using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Physsi___Comsci_Project
{
    public class RigidBody_Editor
    {
        // INITIALISE RIGIDBODY EDITOR VARIABLES

       

        private static class Background
        {
            public static Texture2D Sprite; // background sprite for the scene editor area
            public static Vector2 Position  = new Vector2(0, 0); // so covers the entire background;
        }
        private static class Selector_Background
        {
            public static Texture2D Sprite; // background sprite for the selector bar
            public static Vector2 Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 281), 0); // centres and to the right of screen, as is 281 x 1080 pixels;
        }
        private static class Option_Button
        {
            public static Texture2D Sprite; // options button sprite
            public static Vector2 Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 281 - 125,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 250); // position above the start button;
        }
        private static class Start_Button
        {
            public static Texture2D Sprite; // start button sprite
            public static Vector2 Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 281 - 125,
            GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 125); // Position the start button;
        }
        private static class Square_Item
        {
            public static Texture2D Sprite; // square item for the selector bar
            public static Vector2 Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 281 + (281 - 150) / 2
            , 150); // position selector version of the square item;
        }

        // Initialise SpriteFonts

        public static class ItemText
        {
            public static string Text = "Items";
            public static SpriteFont Font;
            public static Vector2 positon = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 225,10);

        }

       

        public static void Load_RigidBody_Editor(ContentManager Content)
        {
            // Load content for the rigidbody editor

            // Load the sprites for the scene

            Background.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Background");
            Selector_Background.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Background_Selector");
            Option_Button.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Options_Button");
            Start_Button.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Start_Button");
            Square_Item.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Square_Item");

            // Load the fonts for the rigidbody editor 
            ItemText.Font = Content.Load<SpriteFont>("RigidBody_Editor/Item");
            
        }

        // RIGIDBODY SCENE --------------------------------------------------------------------------------------------------


        public static void Draw_RigidBody(SpriteBatch _spriteBatch)
        {
            Draw_RigidBody_Sprite(_spriteBatch); // Draw the sprites to the screen
        }

        public static void Draw_RigidBody_Sprite(SpriteBatch _spriteBatch)
        {
            // Draw sprites and objects

            _spriteBatch.Draw(Background.Sprite, Background.Position, Color.White); // Draw the background to the screen
            _spriteBatch.Draw(Selector_Background.Sprite, Selector_Background.Position, Color.White); // draw the background for the right side of screem (item selector)
            _spriteBatch.Draw(Start_Button.Sprite, Start_Button.Position, Color.White); // Draw the start button in the bottom right of the scene area (but not over the selector bar)
            _spriteBatch.Draw(Option_Button.Sprite, Option_Button.Position, Color.White); // Draw the options button in the bottom right of scene area ( above start button) 
            _spriteBatch.Draw(Square_Item.Sprite, Square_Item.Position, Color.White); // Draw the selector square item 

            // Draw text

            _spriteBatch.DrawString(ItemText.Font,ItemText.Text, ItemText.positon,Color.Black); // Draw text saying 'Items' at the top of the selector bar

        }

        public static void handle_button_click_left(ContentManager Content)
        {
            Vector2 mousePos = MouseCoords();

            if (mousePos.X >= Start_Button.Position.X && mousePos.X <= Start_Button.Position.X + 100)
            {
                
            }
        }

        private static Vector2 MouseCoords() // finds and returns the cursor position as a Vector2
        {
            MouseState mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

    }
}
