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
    public class ScenePlayer
    {
        public static class Background
        {
            public static Texture2D Sprite;
            public static Vector2 Position;
        }

        public static string prevScene = "";

        public static class EndButton
        {
            public static Texture2D Sprite;
            public static Vector2 Position;
        }

        public static void load_Scene(ContentManager Content) // loads the textures and positions for the content
        {
            Background.Sprite = Content.Load<Texture2D>("RigidBody_Editor/Background");
            Background.Position = new Vector2(0, 0);

            EndButton.Sprite = Content.Load<Texture2D>("ScenePlayer/End_Button");
            EndButton.Position = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 125, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 125);
        }

        public static void draw_scene(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Background.Sprite, Background.Position, Color.White);
            _spriteBatch.Draw(EndButton.Sprite, EndButton.Position, Color.White);
            draw_items(_spriteBatch);
        }

        public static void handle_Click(ContentManager Content, Vector2 mouseCoords)
        {
            if (mouseCoords.X >= EndButton.Position.X && mouseCoords.X <= EndButton.Position.X + 100 && mouseCoords.Y >= EndButton.Position.Y && mouseCoords.Y <= EndButton.Position.Y + 100)
            {
                // if end button was clicked
                // send user to rigid-body editor scene
                // load scene not needed as is already loaded
                RB_LOGIC.Reset_Squares();
                SB_LOGIC.resetCircleNodes();
                ChangeScene.changeTo(prevScene);

            } 

            foreach (SB_LOGIC.Circle Circle in SB_LOGIC.Items.Circles)
            {
                foreach(SB_LOGIC.Node node in Circle.CircleNodes)
                {
                    if (Vector2.Distance(mouseCoords, node.Position + new Vector2(12.5f,12.5f)) <= 12.5) // if a node is clicked
                    {

                    }
                }
            }
        }

        public static void draw_items(SpriteBatch _spriteBatch)
        {
            if (prevScene == "RIGIDBODY_EDITOR")
            {
                RB_LOGIC.Draw_RB(_spriteBatch);

            } else if (prevScene == "SOFTBODY_EDITOR")
            {
                SB_LOGIC.Draw_SB(_spriteBatch);
            }
        }
    }
}
