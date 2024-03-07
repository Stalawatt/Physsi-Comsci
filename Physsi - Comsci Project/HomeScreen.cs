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
    public class HomeScreen
    {
        // HOMESCREEN SPRITES LOADING TEXTURE2D AND OTHER VARIABLES


        // --------------------------------------------------------------------------------------------------------------------------------------------------
        public static Texture2D RigidAndSoft_Button; // Sprite for Rigid/Soft -Body buttons
        public static Texture2D Options_Button; // Sprite for Options button
        public static Texture2D Quit_Button;    // Sprite for Quit Button

        // SPRITEFONTS FOR TEXT - Fonts to use on different parts of text
        public static SpriteFont Logo_Font;
        public static SpriteFont Large_Button_Font; // for Rigid-Body and Soft-Body buttons
        public static SpriteFont Small_Button_Font; // For use on Options and Quit buttons

        // TEXT 
        public static string Physsi_Logo_RawText = "Physsi"; // TITLE
                                                             // Text for use on buttons (like labels)
        public static string Rigid_Button_RawText = "Rigid-Body";  // Rigid-Body text           
        public static string Options_Button_RawText = "Options";   // Options button text
        public static string Soft_Button_RawText = "Soft-Body";    // Soft-Body text
        public static string Quit_Button_RawText = "Quit";         // Quit text


        // HOMESCREEN BUTTON POSITIONS
        public static Vector2 Rigid_Body_Pos = new Vector2(460, 400); // From top left of screen
        public static Vector2 Soft_Body_Pos = new Vector2(460, 600); // From top left of screen
        public static Vector2 Options_Button_Pos = new Vector2(585, 800); // From top left of screen
        public static Vector2 Quit_Button_Pos = new Vector2(1700, 850); // From top left of screen

        //Vector2 position is from top left of screen (which is 0,0) to top left of TextBox
        public static class Physsi_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(680, 50); // Centre in screen horizontally, but high on screen
        }

        public static class Rigid_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(650, 400); // Center in the middle of the button
        }

        public static class Soft_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(650, 600); // Center in the middle of the button
        }

        public static class Options_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(775, 825); // Center in the middle of the button
        }

        public static class Quit_Button_Text_Attr
        {
            //public static float width;
            //public static float height;
            public static Vector2 Position = new Vector2(1700, 875); // Center in the middle of the button
        }


        public static void Load_Home(ContentManager Content)
        {
            // INITIALISE TEXTURES FROM PASSED FILENAME (PNG IMAGES)
            RigidAndSoft_Button = Content.Load<Texture2D>("Homescreen/1000x174 Rigidbody Button"); // Button sprite for the Large buttons (Rigid-Body and Soft-Body buttons)
            Options_Button = Content.Load<Texture2D>("Homescreen/Options button"); // Button sprite for the Options button
            Quit_Button = Content.Load<Texture2D>("Homescreen/Quit_Button"); // Button sprite for Quit button

            if (RigidAndSoft_Button == null)
            {
                throw new Exception("RigidAndSoft_Button in Draw_Home_Screen_Sprite() is null");
            }

            // Initialise Fonts
            Logo_Font = Content.Load<SpriteFont>("Homescreen/Physsi"); // Load font from Physsi.spritefont to use for the title
            Large_Button_Font = Content.Load<SpriteFont>("Homescreen/Large_Button"); // Load font from Large_Button.spritefont to use for Rigid/Soft -Body buttons
            Small_Button_Font = Content.Load<SpriteFont>("Homescreen/Small_Button"); // Load font from Small_Button.spritefont to use for Quit and Options buttons
        }

        // Draw text onto homescreen scene
        public static void Draw_Home_Screen_Text(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(Logo_Font, Physsi_Logo_RawText, Physsi_Text_Attr.Position, Color.White); // "Physsi" Logo
            _spriteBatch.DrawString(Large_Button_Font, Rigid_Button_RawText, Rigid_Button_Text_Attr.Position, Color.White); // Rigid-Body button label
            _spriteBatch.DrawString(Large_Button_Font, Soft_Button_RawText, Soft_Button_Text_Attr.Position, Color.White); // Soft-Body button label
            _spriteBatch.DrawString(Small_Button_Font, Options_Button_RawText, Options_Button_Text_Attr.Position, Color.White); // Options button label
            _spriteBatch.DrawString(Small_Button_Font, Quit_Button_RawText, Quit_Button_Text_Attr.Position, Color.White); // Quit button label
        }

        // draw sprites for homescreen scene

        public static void Draw_Home_Screen_Sprite( SpriteBatch _spriteBatch)
        {
            try
            {
                _spriteBatch.Draw(RigidAndSoft_Button, Rigid_Body_Pos, Color.White); // draw rigidbody sprite
                _spriteBatch.Draw(RigidAndSoft_Button, Soft_Body_Pos, Color.White); // draw softbody sprite
                _spriteBatch.Draw(Options_Button, Options_Button_Pos, Color.White); // draw options sprite
                _spriteBatch.Draw(Quit_Button, Quit_Button_Pos, Color.White); // draw quit sprite
            }
            catch (ArgumentNullException ex)
            {
                if (RigidAndSoft_Button == null || Options_Button == null || Quit_Button == null)
                {
                    throw new Exception("Something in Draw_Home_Screen_Sprite() is null", ex);
                }
            }


        }

        public static void Draw_Home_Screen(SpriteBatch spriteBatch)

        {
            Draw_Home_Screen_Sprite(spriteBatch); // draw sprites FIRST so that text is on top
            Draw_Home_Screen_Text(spriteBatch); // draw labels for buttons


        }




        // HANDLE CLICKS



        
        public static void handle_button_click(ContentManager Content, Vector2 mouse_Coords) // handles left clicks for the homescreen
        {
            
            int clicked_code = Clicked_Code(mouse_Coords); // finds what button is clicked

            if (clicked_code == -1) 
            {
                // no button was clicked
            }else if (clicked_code == 0)
            {
                // send user to rigid-body editor scene
                RigidBody_Editor.Load_RigidBody_Editor(Content);
                ChangeScene.changeTo("RIGIDBODY_EDITOR");
                

            }
            else if (clicked_code == 1)
            {
                // send user to soft-body editor scene
                SoftBody_Editor.Load_SoftBody_Editor(Content);
                ChangeScene.changeTo("SOFTBODY_EDITOR");
            }
            else if (clicked_code == 2)
            {
                // send user to options scene
                Options_Scene.Last_Scene.last_Scene = "HOME";
                Options_Scene.Load_Options_Scene(Content);
                ChangeScene.changeTo("OPTIONS");
            }
            else if (clicked_code == 3)
            {
                System.Environment.Exit(0); // exit button kills the process, and returns exit code '0' ( no problem )
            }
        }

        private static int Clicked_Code(Vector2 mouseCoords) // determines which button was clicked
        {


            if (mouseCoords.X > 460 && mouseCoords.Y > 400 && mouseCoords.X < 1460 && mouseCoords.Y < 573)
            {

                return 0; // the code for the 'Rigid-body' button is 0.
            }
            else if (mouseCoords.X > 460 && mouseCoords.Y > 600 && mouseCoords.X < 1460 && mouseCoords.Y < 773)
            {
                return 1; // the code for the 'Soft-body' button is 1.
            }
            else if (mouseCoords.X > 585 && mouseCoords.Y > 800 && mouseCoords.X < 1335 && mouseCoords.Y < 975)
            {
                return 2; // the code for the 'Options' button is 2. 
            }
            else if (mouseCoords.X > 1700 && mouseCoords.Y > 850 && mouseCoords.X < 1900 && mouseCoords.Y < 1250)
            {
                return 3; // the code for 'Quit' button is 3
            }
            return -1; // no button was clicked
        }

        

    }
}
