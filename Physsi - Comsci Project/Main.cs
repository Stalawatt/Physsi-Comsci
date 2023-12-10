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

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; // make mouse visible while in the program

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
            } 
            


        }

        private int x = 10;
        protected override void Update(GameTime gameTime) // Controls (Runs every frame)
        {

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                LeftClick();

            }
            



            

            base.Update(gameTime);
        }
        
       

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // set background colour to Color.CornflowerBlue
            


            _spriteBatch.Begin();
            if (SceneLoaded.scene_Loaded == "HOME")
            {
                HomeScreen.Draw_Home_Screen(gameTime,_spriteBatch); // draw the home screen

            } else if (SceneLoaded.scene_Loaded == "OPTIONS")
            {
                Options_Scene.Draw_Options_Scene(_spriteBatch);

            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR")
            {
                RigidBody_Editor.Draw_RigidBody(_spriteBatch);


            }



            _spriteBatch.End();

            base.Draw(gameTime); // draw the code to the screen
        }


        // MOUSECLICK HANDLING ============================================================================================


        protected void LeftClick()
        {
            
             
            if (SceneLoaded.scene_Loaded == "HOME")
            {
                HomeScreen.handle_button_click(Content); // handles button clicks and what they should do, Clicked_Code() determines what button was clicked.

            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR")
            {
                RigidBody_Editor.handle_button_click_left(Content);

            } else if (SceneLoaded.scene_Loaded == "OPTIONS")
            {
                
            }
            
        }

        
       

        



    }

   
}