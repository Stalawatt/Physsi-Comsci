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

      

        

        



        //  -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // INITIALISE RIGIDBODY EDITOR VARIABLES

        private Texture2D Background_RB; // background for the scene editor area
        private Texture2D Background_Selector_RB; // background for the items selector
        private Texture2D Option_Button_RB; // texture for button that opens the options page
        private Texture2D Start_Button_RB; // texture for button that starts the simulation
        private Dictionary<string,Texture2D> Items_Dict_RB = new Dictionary<string,Texture2D>(); // contains a dictionary for the name, and texture/sprite of each shape
        



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
                Load_Options();
            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR") 
            {
                Load_RigidBody_Editor();
            }


        }

        // Load content for the options page

        protected void Load_Options()
        {
            // Load content for all the options]
            


        }
        // RIGIDBODY SCENE --------------------------------------------------------------------------------------------------
        protected void Load_RigidBody_Editor() 
        {
            // Load content for the rigidbody editor

            Background_RB = Content.Load<Texture2D>("RigidBody_Editor/Background");
            Option_Button_RB = Content.Load<Texture2D>("RigidBody_Editor/Options_Button");
            Start_Button_RB = Content.Load<Texture2D>("RigidBody_Editor/Start_Button");
            Items_Dict_RB.Add("square",Content.Load<Texture2D>("RigidBody_Editor/Square_Item"));
        }

        protected void Draw_RigidBody()
        {
            Draw_RigidBody_Sprite();
        }

        protected void Draw_RigidBody_Sprite()
        {
            _spriteBatch.Draw(Background_RB, new Vector2(0,0), Color.White);
        }

        protected override void Update(GameTime gameTime) // Controls (Runs every frame)
        {

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                //Physsi_Logo_RawText = "Done";
                LeftClick();

            }


            // TODO: Add your update logic here

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

            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR")
            {
                Draw_RigidBody();
            }
            


            _spriteBatch.End();

            base.Draw(gameTime); // draw the code to the screen
        }


        // MOUSECLICK HANDLING ============================================================================================


        protected void LeftClick()
        {
            Vector2 mouseCoords = MouseCoords(); // finds coordinates of the mouse from top left of window on frame it was clicked. 
             

            handle_button_click(Clicked_Code(mouseCoords)); // handles button clicks and what they should do, Clicked_Code() determines what button was clicked.
        }

        protected void handle_button_click(int clicked_code) // handles what should happen when a button is clicked
        {
            if (clicked_code == 0)
            {
                // send user to rigid-body editor scene
                Load_RigidBody_Editor();
                ChangeScene.changeTo("RIGIDBODY_EDITOR");

            } else if (clicked_code == 1)
            {
                // send user to soft-body editor scene
            }else if (clicked_code == 2)
            {
                // send user to options scene
            }else if (clicked_code == 3)
            {
                System.Environment.Exit(0); // exit button kills the process, and returns exit code '0' ( no problem )
            }
        }
        protected int Clicked_Code(Vector2 mouseCoords) // determines which button was clicked
        {
           

            if (mouseCoords.X > 460 && mouseCoords.Y > 400 && mouseCoords.X < 1460 && mouseCoords.Y < 573) {

                return 0; // the code for the 'Rigid-body' button is 0.
            }  else if (mouseCoords.X > 460 && mouseCoords.Y > 600 && mouseCoords.X < 1460 && mouseCoords.Y < 773)
            {
                return 1; // the code for the 'Soft-body' button is 1.
            } else if (mouseCoords.X > 585 && mouseCoords.Y > 800 && mouseCoords.X < 1335 && mouseCoords.Y < 975)
            {
                return 2; // the code for the 'Options' button is 2. 
            } else if (mouseCoords.X > 1700 && mouseCoords.Y > 850 && mouseCoords.X < 1900 && mouseCoords.Y < 1250)
            {
                return 3; // the code for 'Quit' button is 3
            }
                return -1;
        }

        protected Vector2 MouseCoords() // finds and returns the cursor position as a Vector2
        {
            MouseState mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }



    }

   
}