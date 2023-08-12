using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Physsi___Comsci_Project
{
    public class HomeScreen : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // HOMESCREEN SPRITES LOADING TEXTURE2D
        private Texture2D RigidAndSoft_Button; // Sprite for Rigid/Soft -Body buttons
        private Texture2D Options_Button; // Sprite for Options button
        private Texture2D Quit_Button;    // Sprite for Quit Button

        // SPRITEFONTS FOR TEXT - Fonts to use on different parts of text
        private SpriteFont Logo_Font;
        private SpriteFont Large_Button_Font; // for Rigid-Body and Soft-Body buttons
        private SpriteFont Small_Button_Font; // For use on Options and Quit buttons

        // TEXT 
        private string Physsi_Logo_RawText = "Physsi"; // TITLE
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
            public static float width;
            public static float height;
            public static Vector2 Position = new Vector2(680, 50); // Centre in screen horizontally, but high on screen
        }

        static class Rigid_Button_Text_Attr
        {
            public static float width;
            public static float height;
            public static Vector2 Position = new Vector2(650,400); // Center in the middle of the button
        }

        static class Soft_Button_Text_Attr
        {
            public static float width;
            public static float height;
            public static Vector2 Position = new Vector2(650, 600); // Center in the middle of the button
        }

        static class Options_Button_Text_Attr
        {
            public static float width;
            public static float height;
            public static Vector2 Position = new Vector2(775, 825); // Center in the middle of the button
        }

        static class Quit_Button_Text_Attr
        {
            public static float width;
            public static float height;
            public static Vector2 Position = new Vector2(1700,875); // Center in the middle of the button
        }

        public HomeScreen()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            

            // CHANGE TO FULLSCREEN

            _graphics.ToggleFullScreen();// make so that it isnt a fullscreen window, but rather fullscreen
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;// fullscreen width
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;// fullscreen height
            _graphics.ApplyChanges(); // Apply these changes


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // INITIALISE TEXTURES FROM PASSED FILENAME (PNG IMAGES)
            RigidAndSoft_Button = Content.Load<Texture2D>("1000x174 Rigidbody Button"); // Button sprite for the Large buttons (Rigid-Body and Soft-Body buttons)
            Options_Button = Content.Load<Texture2D>("Options button"); // Button sprite for the Options button
            Quit_Button = Content.Load<Texture2D>("Quit_Button"); // Button sprite for Quit button

            // Initialise Fonts
            Logo_Font = Content.Load<SpriteFont>("Physsi"); // Load font from Physsi.spritefont to use for the title
            Large_Button_Font = Content.Load<SpriteFont>("Large_Button"); // Load font from Large_Button.spritefont to use for Rigid/Soft -Body buttons
            Small_Button_Font = Content.Load<SpriteFont>("Small_Button"); // Load font from Small_Button.spritefont to use for Quit and Options buttons

           


            
        }
        
        protected override void Update(GameTime gameTime) // Controls (Runs every frame)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        protected void Draw_Home_Screen_Text( GameTime gameTime)
        {
            _spriteBatch.DrawString(Logo_Font, Physsi_Logo_RawText, Physsi_Text_Attr.Position, Color.White); // "Physsi" Logo
            _spriteBatch.DrawString(Large_Button_Font, Rigid_Button_RawText, Rigid_Button_Text_Attr.Position, Color.White); // Rigid-Body button label
            _spriteBatch.DrawString(Large_Button_Font, Soft_Button_RawText, Soft_Button_Text_Attr.Position, Color.White); // Soft-Body button label
            _spriteBatch.DrawString(Small_Button_Font, Options_Button_RawText, Options_Button_Text_Attr.Position, Color.White); // Options button label
            _spriteBatch.DrawString(Small_Button_Font, Quit_Button_RawText, Quit_Button_Text_Attr.Position, Color.White); // Quit button label
        }

        protected void Draw_Home_Screen_Sprite (GameTime gameTime )
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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}