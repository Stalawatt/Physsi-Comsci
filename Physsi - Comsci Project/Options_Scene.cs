using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
namespace Physsi___Comsci_Project
{
    public class Options_Scene
    {

        // For options :

        // Gravitational Constant     (integer)
        // Air Resistance             (boolean)

        public static class Last_Scene
        {
            public static string last_Scene = "";
        }

        private static class Background
        {
            public static Texture2D Sprite; // background sprite for the scene editor area
            public static Vector2 Position = new Vector2(0, 0); // so covers the entire background;
        }

        private static class Menu_Button
        {
            public static Texture2D Sprite; // sprite for button
            public static Vector2 Position = new Vector2(0, 0); // position the button;
        }

        private static class Option_change_BG
        {
            public static Texture2D Sprite; // background sprite for each option
            public static Vector2 Position = new Vector2(0, 0); // to position each one
        }

        private class Radial
        {
            public Texture2D Sprite; // Radial sprite for the texture to be ON
            public Vector2 Position = new Vector2(0, 0); // so covers the entire background;
            public bool state = false;
        }
        private static class numeric_Enter // enter for any alphanumeric options (but will only be numeric as options are only numeric)
        {
            public static Texture2D Sprite; // background sprite for the scene editor area
            public static Vector2 Position = new Vector2(0, 0); // so covers the entire background;
            private static int value = -1;
        }

        private static class Exit_Button
        {
            public static Texture2D Sprite;
            public static Vector2 Position = new Vector2(50, 50);

            public static void clicked()
            {
                System.Environment.Exit(0);
            }
        }

        public static void Load_Options_Scene(ContentManager Content)
        {
            Background.Sprite = Content.Load<Texture2D>("Options_Scene/Background"); // background for the scene
            Menu_Button.Sprite = Content.Load<Texture2D>("Options_Scene/Menu_Button"); // menu button sprite at the bottom
            Option_change_BG.Sprite = Content.Load<Texture2D>("Options_Scene/Option_change_BG"); // background for each option that can be changed
            Exit_Button.Sprite = Content.Load<Texture2D>("Options_Scene/Exit_Button"); // Exit button back to last scene
            Radial Radial_LEFT = new Radial(); // create radial options for the air resistance option
            Radial Radial_RIGHT = new Radial();

            Texture2D Radial_ON_SPRITE = Content.Load<Texture2D>("Options_Scene/Radial_ON"); // load into a variable the ON sprite for radial
            Texture2D Radial_OFF_SPRITE = Content.Load<Texture2D>("Options_Scene/Radial_OFF"); // load into a variable the OFF sprite for radial

            if (Options_Settings.AirResistance.state == false)
            {
                Radial_LEFT.Sprite = Radial_OFF_SPRITE; // Radial_LEFT is the sprite to have air resistance ON
                Radial_LEFT.state = false;

                Radial_RIGHT.Sprite = Radial_ON_SPRITE; // Radial_Right is the sprite to have air resistance OFF
                Radial_LEFT.state = true; // starts on NO air resistance
            } else if (Options_Settings.AirResistance.state == true)
            {
                Radial_LEFT.Sprite = Radial_ON_SPRITE; // Radial_LEFT is the sprite to have air resistance ON
                Radial_LEFT.state = true;

                Radial_RIGHT.Sprite = Radial_OFF_SPRITE; // Radial_Right is the sprite to have air resistance OFF
                Radial_LEFT.state = false; // starts on NO air resistance

            } 



        }

        public static void Draw_Options_Scene(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Background.Sprite,Background.Position,Color.White);
        }


    }
}
       




