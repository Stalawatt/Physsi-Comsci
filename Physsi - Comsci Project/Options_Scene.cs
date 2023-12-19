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

        // Gravitational Constant     (double)
        // Air Resistance             (boolean)

        private static Radial Radial_LEFT;
        private static Radial Radial_RIGHT;

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
            public static Vector2 Position = new Vector2(560, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 170); // position the button in centre and 50px from bottom
        }

        private static class Option_change_BG
        {
            public static Texture2D Sprite; // background sprite for each option
            public static Vector2 Position = new Vector2(560, 200); // center each one in X, Y is to be added
        }

        private class Radial
        {
            public Texture2D Sprite; // Radial sprite to be defined later, once object is instantiated
            public Vector2 Position; // position for the radial will be defined later, once object is instantiated
            public bool state;

            public bool isClicked(Vector2 mouseCoords)
            {
                Vector2 center = new Vector2(this.Position.X + 30, this.Position.Y + 30);

                if (pythagoras(mouseCoords, center) <= 30.5)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

            private double pythagoras(Vector2 point1, Vector2 point2) // uses pythagoras to find how far the click was from the center
            {
                double floatX = Math.Pow(Math.Abs(point1.X - point2.X), 2);
                double floatY = Math.Pow(Math.Abs(point1.Y - point2.Y), 2);

                return Math.Sqrt(floatX + floatY);

            }
        }
        private static class numeric_Enter // enter for any alphanumeric options (but will only be numeric as options are only numeric)
        {
            public static Texture2D Sprite; // background sprite for the scene editor area
            public static Vector2 Position = new Vector2(0, 0); // so covers the entire background;
            public static int value = -1;
        }

        private static class Exit_Button
        {
            public static Texture2D Sprite;
            public static Vector2 Position = new Vector2(25, 25); // position the button in the top left

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
            Radial_LEFT = new Radial(); // create radial options for the air resistance option
            Radial_RIGHT = new Radial();

            Radial_RIGHT.Sprite = Content.Load<Texture2D>("Options_Scene/Radial_ON"); // load into a variable the ON sprite for radial
            Radial_LEFT.Sprite = Content.Load<Texture2D>("Options_Scene/Radial_OFF"); // load into a variable the OFF sprite for radial

            Radial_LEFT.Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - 30, 230); // position in the middle of the option background
            Radial_RIGHT.Position = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) + 170, 230); // position 200px to the right of the other radial
            
            
            if (Options_Settings.AirResistance.state == false)
            {
                // Radial_LEFT is the sprite to have air resistance ON
                Radial_LEFT.state = false;

                // Radial_Right is the sprite to have air resistance OFF
                Radial_RIGHT.state = true; // starts on NO air resistance
            }
            else if (Options_Settings.AirResistance.state == true)
            {
                // Radial_LEFT is the sprite to have air resistance ON
                Radial_LEFT.state = true;

                // Radial_Right is the sprite to have air resistance OFF
                Radial_RIGHT.state = false; // starts on NO air resistance

            }



        }

        public static void Draw_Options_Scene(SpriteBatch _spriteBatch) // draw the scene to the screen
        {
            _spriteBatch.Draw(Background.Sprite, Background.Position, Color.White);
            _spriteBatch.Draw(Exit_Button.Sprite, Exit_Button.Position, Color.White);
            _spriteBatch.Draw(Menu_Button.Sprite, Menu_Button.Position, Color.White);
            _spriteBatch.Draw(Option_change_BG.Sprite, Option_change_BG.Position, Color.White);

            if (Radial_LEFT.state && !Radial_RIGHT.state)
            {
                _spriteBatch.Draw(Radial_LEFT.Sprite, Radial_RIGHT.Position, Color.White); // swaps the positions, so the dot moves to the selected one
                _spriteBatch.Draw(Radial_RIGHT.Sprite, Radial_LEFT.Position, Color.White); // dot is on the left
            }
            else if (!Radial_LEFT.state && Radial_RIGHT.state)
            {
                _spriteBatch.Draw(Radial_LEFT.Sprite, Radial_LEFT.Position, Color.White); // swaps the positions, so the dot moves to the selected one
                _spriteBatch.Draw(Radial_RIGHT.Sprite, Radial_RIGHT.Position, Color.White); // dot is on right
            }
            
        }

        public static void Handle_button_click(ContentManager Content, Vector2 mouse_Coords)
        {
            

            if (mouse_Coords.X >= Exit_Button.Position.X && mouse_Coords.X <= Exit_Button.Position.X + 100 && mouse_Coords.Y >= Exit_Button.Position.Y && mouse_Coords.Y <= Exit_Button.Position.Y + 100) // if clicked the exit button
            {
                if (Last_Scene.last_Scene == "HOME") // change scene to Homescreen
                {
                    HomeScreen.Load_Home(Content);
                    ChangeScene.changeTo("HOME");
                }
                else if (Last_Scene.last_Scene == "RIGIDBODY_EDITOR") // change scene to rigidbody editor
                {
                    RigidBody_Editor.Load_RigidBody_Editor(Content);
                    ChangeScene.changeTo("RIGIDBODY_EDITOR");
                }

            }
            else if (mouse_Coords.X >= Menu_Button.Position.X && mouse_Coords.X <= Menu_Button.Position.X + 800 && mouse_Coords.Y >= Menu_Button.Position.Y && mouse_Coords.Y <= Menu_Button.Position.Y + 120) // if the menu button is clicked
            {
                // change scene to homescreen
                if (Last_Scene.last_Scene == "RIGIDBODY_EDITOR")
                {
                    RB_LOGIC.ResetScene();
                }
                HomeScreen.Load_Home(Content);
                ChangeScene.changeTo("HOME");
            }
            else if (Radial_LEFT.isClicked(mouse_Coords) == true && Radial_LEFT.state == false)
            {

                Radial_LEFT.state = true;
                Radial_RIGHT.state = false;
                Options_Settings.AirResistance.state = true;// changes the air resistance setting to true
                

            }
            else if (Radial_RIGHT.isClicked(mouse_Coords) == true && Radial_RIGHT.state == false)
            {
                Radial_LEFT.state = false;
                Radial_RIGHT.state = true;
                Options_Settings.AirResistance.state = false;// changes air resistance setting to false
                
            }
            
        }
    }
}
       




