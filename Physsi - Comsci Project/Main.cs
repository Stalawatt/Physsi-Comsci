using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Physsi___Comsci_Project
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private MouseState prevState;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; // make mouse visible while in the program
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d/60d); // limit framerate to 60fps

        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            

            // CHANGE TO FULLSCREEN FOR ALL SCENES

            _graphics.ToggleFullScreen();// make so that it isnt a fullscreen window, but rather fullscreen
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;// fullscreen width
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;// fullscreen height
            _graphics.ApplyChanges(); // Apply these changes


            base.Initialize();
        }

        // LOAD CONTENT --------------------------------------------------------------------------------------------------------------------

        protected override void LoadContent()
        {
            

            // use Content to load game content here

            if (SceneLoaded.scene_Loaded == "HOME")
            {
                HomeScreen.Load_Home(Content); // Loads the textures for the homescreen
                
            } else if (SceneLoaded.scene_Loaded == "OPTIONS")
            {
                Options_Scene.Load_Options_Scene(Content);
            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR") 
            {
                RigidBody_Editor.Load_RigidBody_Editor(Content);
            } else if (SceneLoaded.scene_Loaded == "SCENEPLAYER")
            {
                ScenePlayer.load_Scene(Content);
            }
            


        }

        
        
        protected override void Update(GameTime gameTime) // Controls (Runs every frame)
        {

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Released)
            {
                // single click
                LeftClick();

            } else if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevState.LeftButton == ButtonState.Pressed)
            {
                // drag click
                if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR")
                {
                    RB_LOGIC.DragCLick(prevState);
                }
            }



            prevState = Mouse.GetState();
            

            base.Update(gameTime);
        }
        
       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // set background colour to Color.CornflowerBlue
            


            _spriteBatch.Begin();
            if (SceneLoaded.scene_Loaded == "HOME")
            {
                HomeScreen.Draw_Home_Screen(_spriteBatch); // draw the home screen

            } else if (SceneLoaded.scene_Loaded == "OPTIONS")
            {
                Options_Scene.Draw_Options_Scene(_spriteBatch);

            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR")
            {
                RB_LOGIC.Click.LastClick = Mouse.GetState();
                RigidBody_Editor.Draw_RigidBody(_spriteBatch);

            } else if (SceneLoaded.scene_Loaded == "SCENEPLAYER")
            {
                ScenePlayer.draw_scene(_spriteBatch);
            } else if (SceneLoaded.scene_Loaded == "SOFTBODY_EDITOR")
            {
                SoftBody_Editor.Draw_SoftBody_Sprite(_spriteBatch);
            }



            _spriteBatch.End();

            base.Draw(gameTime); // draw the code to the screen
        }


        // MOUSECLICK HANDLING ============================================================================================


        protected void LeftClick()
        {
            
             
            if (SceneLoaded.scene_Loaded == "HOME")
            {
                HomeScreen.handle_button_click(Content, MouseCoords());       // handles button clicks and what they should do

            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR")
            {
                RigidBody_Editor.handle_button_click_left(Content, MouseCoords());

            } else if (SceneLoaded.scene_Loaded == "OPTIONS")
            {
                Options_Scene.Handle_button_click(Content,MouseCoords());
            } else if (SceneLoaded.scene_Loaded == "SCENEPLAYER")
            {
                ScenePlayer.handle_Click(Content, MouseCoords());
            }
            else if (SceneLoaded.scene_Loaded == "SOFTBODY_EDITOR")
            {
                SoftBody_Editor.handle_button_click_left(Content, MouseCoords());

            }

        }


        public static Vector2 MouseCoords() // finds and returns the cursor position as a Vector2
        {
            MouseState mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

        

    }

   
}
