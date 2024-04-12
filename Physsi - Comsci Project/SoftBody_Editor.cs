﻿using Microsoft.Xna.Framework;
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
using static Physsi___Comsci_Project.RB_LOGIC;

namespace Physsi___Comsci_Project
{
    public class SoftBody_Editor
    {
        // INITIALISE SOFTBODY EDITOR VARIABLES



        private static class Background
        {
            public static Texture2D Sprite; // background sprite for the scene editor area
            public static Vector2 Position = new Vector2(0, 0); // so covers the entire background;
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
        

        private static class Top_Border
        {
            public static Texture2D Sprite;
            public static Vector2 Position = new Vector2(0, 0);
        }

        public static class SpawnCircle
        {
            public static Texture2D Sprite;
            public static Vector2 Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 250, 300);
        }

        // Initialise SpriteFonts

        public static class ItemText
        {
            public static string Text = "Items";
            public static SpriteFont Font;
            public static Vector2 positon = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 225, 10);

        }

        public static class SceneName
        {
            public static string Text = "SoftBody";
            public static SpriteFont Font;
            public static Vector2 positon = new Vector2(50, 10);
        }



        public static void Load_SoftBody_Editor(ContentManager Content)
        {
            // Load content for the rigidbody editor

            // Load the sprites for the scene

            Background.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Background");
            Selector_Background.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Background_Selector");
            Option_Button.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Options_Button");
            Start_Button.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Start_Button");
            Top_Border.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Top_Border");
            SpawnCircle.Sprite = Content.Load<Texture2D>("SoftBody_Editor/NodeTexture");

            // Load the fonts for the rigidbody editor 
            ItemText.Font = Content.Load<SpriteFont>("RigidBody_Editor/Item");
            SceneName.Font = Content.Load<SpriteFont>("RigidBody_Editor/Item");

        }

        public static void Draw_SoftBody_Sprite(SpriteBatch _spriteBatch)
        {



            // Draw sprites and objects

            _spriteBatch.Draw(Background.Sprite, Background.Position, Color.White); // Draw the background to the screen
            _spriteBatch.Draw(Selector_Background.Sprite, Selector_Background.Position, Color.White); // Draw the background for the right side of screem (item selector)
            _spriteBatch.Draw(Top_Border.Sprite, Top_Border.Position, Color.White); // Draw the top border in the top right


            // draw spawn items

            _spriteBatch.Draw(SpawnCircle.Sprite, SpawnCircle.Position, Color.White);

            // Draw text

            _spriteBatch.DrawString(ItemText.Font, ItemText.Text, ItemText.positon, Color.Black); // Draw text saying 'Items' at the top of the selector bar
            _spriteBatch.DrawString(SceneName.Font, SceneName.Text, SceneName.positon, Color.Black); // Draw text saying 'SOFTBODY' at top of screen

            // draw the items in the preview

            // draw all circles

            foreach (SB_LOGIC.Circle circle in SB_LOGIC.Items.Circles)
            {
                circle.drawCircle(_spriteBatch);
            }

            // draw the items first so that can still see the buttons if the buttons and items overlap

            _spriteBatch.Draw(Start_Button.Sprite, Start_Button.Position, Color.White); // Draw the start button in the bottom right of the scene area (but not over the selector bar)
            _spriteBatch.Draw(Option_Button.Sprite, Option_Button.Position, Color.White); // Draw the options button in the bottom right of scene area ( above start button) 
        }

        public static void handle_button_click_left(ContentManager Content, Vector2 mouse_Coords)
        {


            if (mouse_Coords.X >= Start_Button.Position.X && mouse_Coords.X <= Start_Button.Position.X + 100 && mouse_Coords.Y >= Start_Button.Position.Y && mouse_Coords.Y <= Start_Button.Position.Y + 100)
            {
                // start button was clicked    
                ScenePlayer.prevScene = "SOFTBODY_EDITOR";
                ScenePlayer.load_Scene(Content);
                ChangeScene.changeTo("SCENEPLAYER");

            }
            else if (mouse_Coords.X >= Option_Button.Position.X && mouse_Coords.X <= Option_Button.Position.X + 100 && mouse_Coords.Y >= Option_Button.Position.Y && mouse_Coords.Y <= Option_Button.Position.Y + 100)
            {
                // options button was clicked -> send user to options scene
                Options_Scene.Last_Scene.last_Scene = "SOFTBODY_EDITOR";
                Options_Scene.Load_Options_Scene(Content);
                ChangeScene.changeTo("OPTIONS");

            }
           
            else if (mouse_Coords.X >= SpawnCircle.Position.X && mouse_Coords.X <= SpawnCircle.Position.X + 25 && mouse_Coords.Y >= SpawnCircle.Position.Y && mouse_Coords.Y <= SpawnCircle.Position.Y + 25)
            {

                SB_LOGIC.createCircleObj(Content);

            }
            
        }

        public static void DragClick(MouseState prevState)
        {
            MouseState currentState = Mouse.GetState();

            int DeltaPosX = currentState.Position.X - prevState.Position.X;
            int DeltaPosY = currentState.Position.Y - prevState.Position.Y;

            Vector2 deltaPos = new Vector2(DeltaPosX, DeltaPosY);

            foreach (SB_LOGIC.Circle circle in SB_LOGIC.Items.Circles)
            {
                foreach(SB_LOGIC.Node node in circle.CircleNodes)
                {
                    if (pythagoras(new Vector2(currentState.Position.X, currentState.Position.Y), node.Position + new Vector2(12.5f,12.5f)) < 25)
                    {
                        foreach (SB_LOGIC.Node Node in circle.CircleNodes)
                        {
                            Node.Position += deltaPos;
                            Node.InitialPosition += deltaPos;
                        }
                    }
                }
            }

        }



        private static double pythagoras(Vector2 positionA, Vector2 positionB)
        {
            double deltaX = positionA .X - positionB.X;
            double deltaY = positionA .Y - positionB.Y;

            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return distance;

        }





    }
}
