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
        private Texture2D RigidAndSoft_Button;
        private Texture2D Options_Button;
        private Texture2D Quit_Button;

        // SPRITEFONTS FOR TEXT
        private SpriteFont Logo_Font;
        private SpriteFont Large_Button_Font;
        private SpriteFont Small_Button_Font;

        // TEXT 
        private string Physsi_Logo_RawText = "Physsi"; // TITLE

        private string Rigid_Button_RawText = "Rigid-Body";
        private string Options_Button_RawText = "Options";
        private string Soft_Button_RawText = "Soft-Body";
        private string Quit_Button_RawText = "Quit";


        // HOMESCREEN BUTTON POSITIONS
        Vector2 Rigid_Body_Pos = new Vector2(460, 400); // From top left of screen
        Vector2 Soft_Body_Pos = new Vector2(460, 600); // From top left of screen
        Vector2 Options_Button_Pos = new Vector2(585, 800); // From top left of screen
        Vector2 Quit_Button_Pos = new Vector2(1700, 850); // From top left of screen


        static class Physsi_Text_Attr
        {
            public static float width;
            public static Vector2 Position = new Vector2(680, 50); // From top left of screen
        }

        static class Rigid_Button_Text_Attr
        {
            public static float width;
            public static Vector2 Position = new Vector2(650,400); // Center in the middle of the button
        }

        static class Soft_Button_Text_Attr
        {
            public static float width;
            public static Vector2 Position = new Vector2(650, 600); // Center in the middle of the button
        }

        static class Options_Button_Text_Attr
        {
            public static float width;
            public static Vector2 Position = new Vector2(775, 825); // Center in the middle of the button
        }

        static class Quit_Button_Text_Attr
        {
            public static float width;
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

            _graphics.ToggleFullScreen();
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // INITIALISE TEXTURES
            RigidAndSoft_Button = Content.Load<Texture2D>("1000x174 Rigidbody Button");
            Options_Button = Content.Load<Texture2D>("Options button");
            Quit_Button = Content.Load<Texture2D>("Quit_Button");
            Logo_Font = Content.Load<SpriteFont>("Physsi");
            Large_Button_Font = Content.Load<SpriteFont>("Large_Button");
            Small_Button_Font = Content.Load<SpriteFont>("Small_Button");

            Physsi_Text_Attr.width = Logo_Font.MeasureString("Physsi").X * 150;


            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            _spriteBatch.Begin();
            _spriteBatch.Draw(RigidAndSoft_Button, Rigid_Body_Pos, Color.White);
            _spriteBatch.Draw(RigidAndSoft_Button, Soft_Body_Pos, Color.White);
            _spriteBatch.Draw(Options_Button, Options_Button_Pos, Color.White);
            _spriteBatch.Draw(Quit_Button, Quit_Button_Pos, Color.White);
            _spriteBatch.DrawString(Logo_Font, Physsi_Logo_RawText, Physsi_Text_Attr.Position, Color.White);
            _spriteBatch.DrawString(Large_Button_Font, Rigid_Button_RawText, Rigid_Button_Text_Attr.Position, Color.White);
            _spriteBatch.DrawString(Large_Button_Font, Soft_Button_RawText, Soft_Button_Text_Attr.Position, Color.White);
            _spriteBatch.DrawString(Small_Button_Font, Options_Button_RawText, Options_Button_Text_Attr.Position, Color.White);
            _spriteBatch.DrawString(Small_Button_Font, Quit_Button_RawText, Quit_Button_Text_Attr.Position, Color.White);


            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}