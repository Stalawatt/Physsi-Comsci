using Microsoft.Xna.Framework;
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

      

        

        // HOMESCREEN SPRITES LOADING TEXTURE2D AND OTHER VARIABLES
        

        // --------------------------------------------------------------------------------------------------------------------------------------------------
        private Texture2D RigidAndSoft_Button; // Sprite for Rigid/Soft -Body buttons
        private Texture2D Options_Button; // Sprite for Options button
        private Texture2D Quit_Button;    // Sprite for Quit Button

        // SPRITEFONTS FOR TEXT - Fonts to use on different parts of text
        private SpriteFont Logo_Font;
        private SpriteFont Large_Button_Font; // for Rigid-Body and Soft-Body buttons
        private SpriteFont Small_Button_Font; // For use on Options and Quit buttons

        // TEXT 
        public string Physsi_Logo_RawText = "Physsi"; // TITLE
                                                      // Text for use on buttons (like labels)
        private string Rigid_Button_RawText = "Rigid-Body";  // Rigid-Body text           
        private string Options_Button_RawText = "Options";   // Options button text
        private string Soft_Button_RawText = "Soft-Body";    // Soft-Body text
        private string Quit_Button_RawText = "Quit";         // Quit text


        // HOMESCREEN BUTTON POSITIONS
        Vector2 Rigid_Body_Pos = new Vector2(460, 400); // From top left of screen
        Vector2 Soft_Body_Pos = new Vector2(460, 600); // From top left of screen
        Vector2 Options_Button_Pos = new Vector2(585, 800); // From top left of screen
        Vector2 Quit_Button_Pos = new Vector2(1700, 850); // From top left of screen

        //Vector2 position is from top left of screen (which is 0,0) to top left of TextBox
        static class Physsi_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(680, 50); // Centre in screen horizontally, but high on screen
        }

        static class Rigid_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(650, 400); // Center in the middle of the button
        }

        static class Soft_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(650, 600); // Center in the middle of the button
        }

        static class Options_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(775, 825); // Center in the middle of the button
        }

        static class Quit_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(1700, 875); // Center in the middle of the button
        }



        //  -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // INITIALISE RIGIDBODY EDITOR VARIABLES

        private Texture2D Background; // background for the scene editor area
        private Texture2D Background_Selector; // background for the items selector
        private Texture2D Option_Button; // texture for button that opens the options page
        private Texture2D Start_Button; // texture for button that starts the simulation
        private Dictionary<string,Texture2D> Items_Dict = new Dictionary<string,Texture2D>(); // contains a dictionary for the name, and texture/sprite of each shape
        



        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; // make mouse visible while in the program

        }

        protected override void Initialize()
        {
           
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // use Content to load game content here

            if (SceneLoaded.scene_Loaded == "HOME")
            {
                Load_Home(); // Loads the textures for the homescreen
            } else if (SceneLoaded.scene_Loaded == "OPTIONS")
            {
                Load_Options();
            } else if (SceneLoaded.scene_Loaded == "RIGIDBODY_EDITOR") 
            {
                Load_RigidBody_Editor();
            }


        }

        // Load content for the homepage

        protected void Load_Home()
        {
            // INITIALISE TEXTURES FROM PASSED FILENAME (PNG IMAGES)
            RigidAndSoft_Button = Content.Load<Texture2D>("Homescreen/1000x174 Rigidbody Button"); // Button sprite for the Large buttons (Rigid-Body and Soft-Body buttons)
            Options_Button = Content.Load<Texture2D>("Homescreen/Options button"); // Button sprite for the Options button
            Quit_Button = Content.Load<Texture2D>("Homescreen/Quit_Button"); // Button sprite for Quit button

            // Initialise Fonts
            Logo_Font = Content.Load<SpriteFont>("Homescreen/Physsi"); // Load font from Physsi.spritefont to use for the title
            Large_Button_Font = Content.Load<SpriteFont>("Homescreen/Large_Button"); // Load font from Large_Button.spritefont to use for Rigid/Soft -Body buttons
            Small_Button_Font = Content.Load<SpriteFont>("Homescreen/Small_Button"); // Load font from Small_Button.spritefont to use for Quit and Options buttons
        }

        // Load content for the options page

        protected void Load_Options()
        {
            // Load content for all the options]
            


        }

        protected void Load_RigidBody_Editor() 
        {
            // Load content for the rigidbody editor
            
            // private Texture2D Background_Selector; // background for the items selector
            // private Texture2D Option_Button; // texture for button that opens the options page
            // private Texture2D Start_Button; // texture for button that starts the simulation
            // private Dictionary<string, Texture2D> Items_Dict = new Dictionary<string, Texture2D>(); // contains a dictionary for the name, and texture/sprite of each shape

            Background = Content.Load<Texture2D>("RigidBody_Editor/Background");
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
        protected void Draw_Home_Screen_Text(GameTime gameTime)
        {
            _spriteBatch.DrawString(Logo_Font, Physsi_Logo_RawText, Physsi_Text_Attr.Position, Color.White); // "Physsi" Logo
            _spriteBatch.DrawString(Large_Button_Font, Rigid_Button_RawText, Rigid_Button_Text_Attr.Position, Color.White); // Rigid-Body button label
            _spriteBatch.DrawString(Large_Button_Font, Soft_Button_RawText, Soft_Button_Text_Attr.Position, Color.White); // Soft-Body button label
            _spriteBatch.DrawString(Small_Button_Font, Options_Button_RawText, Options_Button_Text_Attr.Position, Color.White); // Options button label
            _spriteBatch.DrawString(Small_Button_Font, Quit_Button_RawText, Quit_Button_Text_Attr.Position, Color.White); // Quit button label
        }

        protected void Draw_Home_Screen_Sprite(GameTime gameTime)
        {
            _spriteBatch.Draw(RigidAndSoft_Button, Rigid_Body_Pos, Color.White); // draw rigidbody sprite
            _spriteBatch.Draw(RigidAndSoft_Button, Soft_Body_Pos, Color.White); // draw softbody sprite
            _spriteBatch.Draw(Options_Button, Options_Button_Pos, Color.White); // draw options sprite
            _spriteBatch.Draw(Quit_Button, Quit_Button_Pos, Color.White); // draw quit sprite
        }
        protected void Draw_Home_Screen(GameTime gameTime)

        {
            Draw_Home_Screen_Sprite(gameTime); // draw sprites FIRST so that text is on top
            Draw_Home_Screen_Text(gameTime); // draw labels for buttons


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // set background colour to Color.CornflowerBlue



            _spriteBatch.Begin();

            Draw_Home_Screen(gameTime); // draw the home screen


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